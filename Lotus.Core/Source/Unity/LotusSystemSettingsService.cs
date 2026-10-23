#if UNITY_2017_1_OR_NEWER
using System;
using System.Reflection;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnity
    *@{*/
    /// <summary>
    /// Сервис для хранения глобальных системных и пользовательских настроек системы Lotus
    /// </summary>
    [Serializable]
    public class LotusSystemSettingsService : ScriptableObject
    {
        #region Constants
        /// <summary>
        /// Имя файла сервиса
        /// </summary>
        public const string SYSTEM_SETTINGS_SERVICE_NAME = "SystemSettingsService";
        #endregion

        #region Static fields
        // Глобальный экземпляр
        internal static LotusSystemSettingsService _instance;

#if UNITY_EDITOR
        internal static Type _typeGameView;
        internal static MethodInfo _getSizeOfMainGameView;
#endif
        #endregion

        #region Static properties
        /// <summary>
        /// Глобальный экземпляр сервиса
        /// </summary>
        public static LotusSystemSettingsService Instance
        {
            get
            {
                if (_instance == null)
                {
                    var path = XCoreSettings.ResourcesData + SYSTEM_SETTINGS_SERVICE_NAME;
                    _instance = Resources.Load<LotusSystemSettingsService>(path);
                }

                return _instance;
            }
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Получение размера окна игры редактора
        /// </summary>
        /// <returns>Размера окна</returns>
        internal static Vector2 GetSizeOfMainGameView()
        {
#if UNITY_EDITOR
            if (_typeGameView == null)
            {
                _typeGameView = Type.GetType("UnityEditor.GameView,UnityEditor");
            }

            if (_typeGameView == null)
            {
                return new Vector2(Screen.width, Screen.height);
            }

            if (_getSizeOfMainGameView == null)
            {
                _getSizeOfMainGameView = _typeGameView.GetMethod("GetSizeOfMainGameView",
                    BindingFlags.NonPublic | BindingFlags.Static);
            }

            if (_getSizeOfMainGameView == null)
            {
                return new Vector2(Screen.width, Screen.height);
            }

            var size = _getSizeOfMainGameView.Invoke(null, null);
            if (size is Vector2 vectorSize)
            {
                return vectorSize;
            }

            return new Vector2(Screen.width, Screen.height);
#else
            return new Vector2(Screen.width, Screen.height);
#endif
        }
        #endregion

        #region Fields
        // Основные параметры
        [SerializeField]
        internal int _designWidth;
        [SerializeField]
        internal int _designHeight;

        // Данные в режиме редактора
#if UNITY_EDITOR
        [SerializeField]
        internal bool _expandedDesign;
#endif
        #endregion

        #region Properties
        //
        // ПАРАМЕТРЫ РАЗРАБОТКИ
        //
        /// <summary>
        /// Текущая ширина экрана
        /// </summary>
        public float ScreenWidth
        {
            get
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    return Screen.width;
                }

                return _designWidth;
#else
                return Screen.width;
#endif
            }
        }

        /// <summary>
        /// Текущая высота экрана
        /// </summary>
        public float ScreenHeight
        {
            get
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    return Screen.height;
                }

                return _designHeight;
#else
                return Screen.height;
#endif
            }
        }

        /// <summary>
        /// Ширина экрана при разработке
        /// </summary>
        /// <remarks>
        /// Применяется для последующего вычисления актуального масштаба экран и корректировки
        /// местоположения двухмерных элементов графического интерфейса
        /// </remarks>
        public int DesignWidth
        {
            get { return _designWidth; }
            set { _designWidth = value; }
        }

        /// <summary>
        /// Высота экрана при разработке
        /// </summary>
        /// <remarks>
        /// Применяется для последующего вычисления актуального масштаба экран и корректировки
        /// местоположения двухмерных элементов графического интерфейса
        /// </remarks>
        public int DesignHeight
        {
            get { return _designHeight; }
            set { _designHeight = value; }
        }

        /// <summary>
        /// Масштаб ширины экрана по отношению к ширине экрана при разработке
        /// </summary>
        public float ScaledScreenX
        {
            get
            {
                if (_designWidth <= 0)
                {
                    return 1.0f;
                }

#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    return GetSizeOfMainGameView().x / _designWidth;
                }

                return 1.0f;
#else
                return (float)Screen.width / _designWidth;
#endif
            }
        }

        /// <summary>
        /// Масштаб высоты экрана по отношению к высоте экрана при разработке
        /// </summary>
        public float ScaledScreenY
        {
            get
            {
                if (_designHeight <= 0)
                {
                    return 1.0f;
                }

#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    return GetSizeOfMainGameView().y / _designHeight;
                }

                return 1.0f;
#else
                return (float)Screen.height / _designHeight;
#endif
            }
        }

        /// <summary>
        /// Средний коэффициент масштаба
        /// </summary>
        public float ScaledScreenAverage
        {
            get { return (ScaledScreenX + ScaledScreenY) / 2f; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Включение сервиса
        /// </summary>
        public void OnEnable()
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Первичное создание сервиса
        /// </summary>
        public void Create()
        {
        }

        /// <summary>
        /// Первичная безопасная инициализация несериализуемых данных
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// Принудительный сброс записанных данных на диск
        /// </summary>
        public void Flush()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        #endregion
    }
    /**@}*/
}
#endif
