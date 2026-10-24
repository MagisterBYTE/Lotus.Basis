#if UNITY_EDITOR
using System;
using System.Reflection;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Тип изменения размеров экрана
    /// </summary>
    public enum TGameViewSizeType
    {
        /// <summary>
        /// Соотношение сторон
        /// </summary>
        AspectRatio,

        /// <summary>
        /// Фиксированное разрешение
        /// </summary>
        FixedResolution
    }

    /// <summary>
    /// Статический класс реализующий методы для получение и управления параметрами окна игры редактора
    /// </summary>
    /// <remarks>
    /// Только для режима редактора Unity
    /// </remarks>
    public static class XEditorGameView
    {
        #region Fields
        /// <summary>
        /// Тип окна игры редактора
        /// </summary>
        private static Type _typeGameView;

        /// <summary>
        /// Экземпляр окна игры редактора
        /// </summary>
        private static object _gameViewWindow;

        /// <summary>
        /// Метод получения размеров окна игры редактора (статический метод)
        /// </summary>
        private static MethodInfo _methodGetSizeOfMainGameView;

        /// <summary>
        /// Свойство для получения текущих параметров размера окна игры редактора
        /// </summary>
        private static PropertyInfo _propertyCurrentGameViewSize;

        /// <summary>
        /// Текущие параметры размера окна игры редактора
        /// </summary>
        private static object _currentGameViewSize;

        /// <summary>
        /// Свойство для получения текущей группы размеров
        /// </summary>
        private static PropertyInfo _propertyCurrentSizeGroupType;

        /// <summary>
        /// Текущая группа размеров
        /// </summary>
        private static object _currentSizeGroupType;

        /// <summary>
        /// Тип объекта содержащего списки всех групп размеров
        /// </summary>
        private static Type _typeGameViewSizes;

        /// <summary>
        /// Объект содержащий списки всех групп размеров
        /// </summary>
        private static object _gameViewSizes;

        /// <summary>
        /// Метод для получения соответствующей группы размеров
        /// </summary>
        private static MethodInfo _methodGetGroup;
        #endregion

        #region Properties
        /// <summary>
        /// Ширина окна игры редактора
        /// </summary>
        public static int Width
        {
            get
            {
                Init();
                var res = (Vector2)_methodGetSizeOfMainGameView.Invoke(null, null);
                return (int)res.x;
            }
        }

        /// <summary>
        /// Высота окна игры редактора
        /// </summary>
        public static int Height
        {
            get
            {
                Init();
                var res = (Vector2)_methodGetSizeOfMainGameView.Invoke(null, null);
                return (int)res.y;
            }
        }

        /// <summary>
        /// Описание размера
        /// </summary>
        public static string Name
        {
            get
            {
                Init();

                var baseTextProp = _currentGameViewSize.GetType().GetProperty("baseText",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                var baseText = baseTextProp.GetValue(_currentGameViewSize);
                return baseText.ToString();
            }
        }

        /// <summary>
        /// Тип изменения размеров экрана
        /// </summary>
        public static TGameViewSizeType SizeType
        {
            get
            {
                Init();
                var sizeTypeProp = _currentGameViewSize.GetType().GetProperty("sizeType",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                var sizeType = sizeTypeProp.GetValue(_currentGameViewSize);
                var value = sizeType.ToString();
                if (value.IndexOf("Aspect", StringComparison.Ordinal) > -1)
                {
                    return TGameViewSizeType.AspectRatio;
                }

                return TGameViewSizeType.FixedResolution;
            }
        }

        /// <summary>
        /// Текущая группа размеров
        /// </summary>
        public static GameViewSizeGroupType SizeGroupType
        {
            get
            {
                Init();
                return (GameViewSizeGroupType)_currentSizeGroupType;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Конвертация в тип GameViewSizeGroupType
        /// </summary>
        /// <param name="buildTarget">Целевая платформа сборки</param>
        /// <returns>Объект GameViewSizeGroupType</returns>
        public static GameViewSizeGroupType FromBuildTarget(BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case BuildTarget.iOS:
                case BuildTarget.tvOS:
                    return GameViewSizeGroupType.iOS;
                case BuildTarget.Android:
                    return GameViewSizeGroupType.Android;
                default:
                    return GameViewSizeGroupType.Standalone;
            }
        }

        /// <summary>
        /// Инициализация данных
        /// </summary>
        private static void Init()
        {
            if (_typeGameView == null)
            {
                _typeGameView = Type.GetType("UnityEditor.GameView,UnityEditor");
            }

            if (_gameViewWindow == null)
            {
                _gameViewWindow = EditorWindow.GetWindow(_typeGameView, false, null, false);
            }

            if (_methodGetSizeOfMainGameView == null)
            {
                _methodGetSizeOfMainGameView = _typeGameView.GetMethod("GetSizeOfMainGameView",
                    BindingFlags.NonPublic | BindingFlags.Static);
            }

            if (_propertyCurrentGameViewSize == null)
            {
                _propertyCurrentGameViewSize = _typeGameView.GetProperty("currentGameViewSize",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }

            _currentGameViewSize = _propertyCurrentGameViewSize.GetValue(_gameViewWindow);

            if (_propertyCurrentSizeGroupType == null)
            {
                _propertyCurrentSizeGroupType = _typeGameView.GetProperty("currentSizeGroupType",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            _currentSizeGroupType = _propertyCurrentSizeGroupType.GetValue(null);

            if (_typeGameViewSizes == null)
            {
                var sizesType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSizes");
                _typeGameViewSizes = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
            }

            var instanceProp = _typeGameViewSizes.GetProperty("instance");
            _gameViewSizes = instanceProp.GetValue(null, null);

            if (_methodGetGroup == null)
            {
                _methodGetGroup = _gameViewSizes.GetType().GetMethod("GetGroup",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }
        }

        /// <summary>
        /// Получение группы размеров
        /// </summary>
        /// <remarks>
        /// Метод возвращает список размеров для соответствующей группы
        /// </remarks>
        /// <param name="buildTarget">Тип группы размеров</param>
        /// <returns>Группа размеров</returns>
        private static object GetGroup(BuildTarget buildTarget)
        {
            Init();
            return _methodGetGroup.Invoke(_gameViewSizes, new object[] { (int)FromBuildTarget(buildTarget) });
        }

        /// <summary>
        /// Проверка на существование указанного размера для указанного типа группы размеров
        /// </summary>
        /// <param name="buildTarget">Тип группы размеров</param>
        /// <param name="text">Описание размера</param>
        /// <returns>Статус проверки</returns>
        public static bool SizeExists(BuildTarget buildTarget, string text)
        {
            Init();
            return FindSize(buildTarget, text) != -1;
        }

        /// <summary>
        /// Проверка на существование указанного размера для указанного типа группы размеров
        /// </summary>
        /// <param name="buildTarget">Тип группы размеров</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <returns>Статус проверки</returns>
        public static bool SizeExists(BuildTarget buildTarget, int width, int height)
        {
            Init();
            return FindSize(buildTarget, width, height) != -1;
        }

        /// <summary>
        /// Поиск индекса размера для указанного типа группы размеров
        /// </summary>
        /// <param name="buildTarget">Тип группы размеров</param>
        /// <param name="text">Описание размера</param>
        /// <returns>Индекс размера</returns>
        public static int FindSize(BuildTarget buildTarget, string text)
        {
            Init();

            var group = GetGroup(buildTarget);
            var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
            var displayTexts = getDisplayTexts.Invoke(group, null) as string[];
            for (var i = 0; i < displayTexts.Length; i++)
            {
                var display = displayTexts[i];
                // the text we get is "Name (W:H)" if the size has a name, or just "W:H" e.g. 16:9
                var pren = display.IndexOf('(');
                if (pren != -1)
                {
                    display = display.Substring(0, pren - 1);
                }
                if (display == text)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Поиск индекса размера для указанного типа группы размеров
        /// </summary>
        /// <param name="buildTarget">Тип группы размеров</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <returns>Индекс размера</returns>
        public static int FindSize(BuildTarget buildTarget, int width, int height)
        {
            Init();

            var group = GetGroup(buildTarget);
            var groupType = group.GetType();
            var getBuiltinCount = groupType.GetMethod("GetBuiltinCount");
            var getCustomCount = groupType.GetMethod("GetCustomCount");
            var sizesCount = (int)getBuiltinCount.Invoke(group, null) + (int)getCustomCount.Invoke(group, null);
            var getGameViewSize = groupType.GetMethod("GetGameViewSize");
            var gvsType = getGameViewSize.ReturnType;
            var widthProp = gvsType.GetProperty("width");
            var heightProp = gvsType.GetProperty("height");
            var indexValue = new object[1];
            for (var i = 0; i < sizesCount; i++)
            {
                indexValue[0] = i;
                var size = getGameViewSize.Invoke(group, indexValue);
                var sizeWidth = (int)widthProp.GetValue(size, null);
                var sizeHeight = (int)heightProp.GetValue(size, null);
                if (sizeWidth == width && sizeHeight == height)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Установка размера по индексу
        /// </summary>
        /// <param name="index">Индекс размера</param>
        public static void SetSize(int index)
        {
            Init();
            var selectedSizeIndexProp = _typeGameView.GetProperty("selectedSizeIndex",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            selectedSizeIndexProp.SetValue(_gameViewWindow, index, null);
        }

        /// <summary>
        /// Добавление произвольного размера
        /// </summary>
        /// <param name="sizeType">Тип изменения размеров экрана</param>
        /// <param name="buildTarget">Тип группы размеров</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="text">Описание</param>
        public static void AddCustomSize(TGameViewSizeType sizeType, BuildTarget buildTarget,
            int width, int height, string text)
        {
            Init();

            var group = GetGroup(buildTarget);
            var addCustomSize = _methodGetGroup.ReturnType.GetMethod("AddCustomSize");
            var gvsType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSize");
            var viewSizeType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSizeType");
            var ctor = gvsType.GetConstructor(new Type[] { viewSizeType, typeof(int), typeof(int), typeof(string) });
            var newSize = ctor.Invoke(new object[] { (int)sizeType, width, height, text });
            addCustomSize.Invoke(group, new object[] { newSize });
        }

        /// <summary>
        /// Применить указанные параметры к окну игры редактора
        /// </summary>
        /// <remarks>
        /// Если указанные параметры будут найдены то они будут активированы,
        /// в противном случае добавится размер по указанным параметрам
        /// </remarks>
        /// <param name="sizeType">Тип изменения размеров экрана</param>
        /// <param name="buildTarget">Тип группы размеров</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="text">Описание</param>
        public static void ApplyCustomSize(TGameViewSizeType sizeType, BuildTarget buildTarget,
            int width, int height, string text)
        {
            var index = FindSize(buildTarget, width, height);
            if (index == -1)
            {
                AddCustomSize(sizeType, buildTarget, width, height, text);
                index = FindSize(buildTarget, width, height);
            }

            SetSize(index);
        }
        #endregion
    }
    /**@}*/
}
#endif
