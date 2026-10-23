#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using Lotus.Core.Inspector;
using Lotus.Core.Serialization;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityTween
    *@{*/
    /// <summary>
    /// Класс для хранения спрайтов одной анимационной цепочки
    /// </summary>
    [Serializable]
    public class CTweenSpriteData
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [LotusSerializeMember]
        //[LotusColumn(35)]
        internal string _name;

        [SerializeField]
        [LotusSerializeMember]
        [HideInInspector]
        internal List<Sprite> _sprites;

        [SerializeField]
        [HideInInspector]
        internal List<Rect> _uvRects;

        [SerializeField]
        [LotusSerializeMember]
        //[LotusColumn(25)]
        internal float _timeAnimation;

        // Данные в режиме редактора
#if UNITY_EDITOR
        [SerializeField]
        [HideInInspector]
        internal int _currentIndex;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Имя анимационной цепочки (совпадает с именем текстуры спрайтов)
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Список спрайтов для анимационной цепочки
        /// </summary>
        public List<Sprite> Sprites
        {
            get { return _sprites; }
        }

        /// <summary>
        /// Список прямоугольников текстурных координат спрайтов
        /// </summary>
        public List<Rect> UVRects
        {
            get { return _uvRects; }
        }

        /// <summary>
        /// Количество спрайтов в анимационной цепочки
        /// </summary>
        public int Count
        {
            get { return _sprites.Count; }
        }

        /// <summary>
        /// Время проигрывания анимационной цепочки
        /// </summary>
        public float TimeAnimation
        {
            get { return _timeAnimation; }
            set { _timeAnimation = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CTweenSpriteData()
        {
            _name = string.Empty;
            _sprites = new List<Sprite>();
            _uvRects = new List<Rect>();
            _sprites.Add(null);
            _timeAnimation = 1f;
        }
        #endregion
    }

    /// <summary>
    /// Служебный класс для хранения списка анимаций
    /// </summary>
    [Serializable]
    public class CTweenListSpriteData : ListArray<CTweenSpriteData>
    {
    }

    /// <summary>
    /// Класс для группирования спрайтов
    /// </summary>
    [Serializable]
    public class CTweenSpriteGroup
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        //[LotusInspector(nameof(Name))]
        internal string _name;

        [SerializeField]
        [LotusDisplayName("SpritesData")]
        //[LotusReorderable(DrawItemMethodName = nameof(OnDrawItem), HeightItemMethodName = nameof(OnHeightItem))]
        //[LotusInLine]
        //[LotusInspector]
        internal CTweenListSpriteData _sprites;
        #endregion

        #region Properties
        /// <summary>
        /// Имя группы
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Список спрайтов
        /// </summary>
        public CTweenListSpriteData ListSprites
        {
            get { return _sprites; }
        }

        /// <summary>
        /// Количество анимационных цепочек
        /// </summary>
        public int Count
        {
            get { return _sprites.Count; }
        }

        /// <summary>
        /// Список имен анимационных цепочек
        /// </summary>
        public string[] ListNames
        {
            get
            {
                var names = new string[_sprites.Count];

                for (var i = 0; i < _sprites.Count; i++)
                {
                    names[i] = _sprites[i].Name;
                }

                return names;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CTweenSpriteGroup()
        {
            _name = string.Empty;
            _sprites = new CTweenListSpriteData();
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация анимационных цепочек
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Анимационная цепочка</returns>
        public CTweenSpriteData this[int index]
        {
            get
            {
                return _sprites[index];
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление новой анимационной цепочки спрайтов
        /// </summary>
        public void AddSprite()
        {
            _sprites.Add(new CTweenSpriteData());
        }

        /// <summary>
        /// Добавление новой анимационной цепочки спрайтов из указанной текстуры
        /// </summary>
        /// <param name="texture">Текстура</param>
        public void AddSpriteFromTexture(Texture2D texture)
        {
            Sprite[] sprites = texture.GetSpritesInDesign();
            if (sprites != null)
            {
                var spriteData = new CTweenSpriteData();
                spriteData.Sprites.Clear();

                spriteData.Name = texture.name.RemoveLastMatch("#");

                for (var i = 0; i < sprites.Length; i++)
                {
                    var sprite = sprites[i];
                    var uvRect = sprite.rect;
                    uvRect.x /= sprite.texture.width;
                    uvRect.y /= sprite.texture.height;
                    uvRect.width /= sprite.texture.width;
                    uvRect.height /= sprite.texture.height;
                    spriteData.Sprites.Add(sprite);
                    spriteData.UVRects.Add(uvRect);
                }

                _sprites.Add(spriteData);
            }
        }

        /// <summary>
        /// Устанока анимационной цепочки спрайтов из указанной текстуры
        /// </summary>
        /// <param name="texture">Текстура</param>
        /// <param name="index">Индекс анимационной цепочки</param>
        public void SetFromTexture(Texture2D texture, int index)
        {
            SetFromTexture(texture, _sprites[index]);
        }

        /// <summary>
        /// Устанока анимационной цепочки спрайтов из указанной текстуры
        /// </summary>
        /// <param name="texture">Текстура</param>
        /// <param name="spriteData">Анимационная цепочка</param>
        public void SetFromTexture(Texture2D texture, CTweenSpriteData spriteData)
        {
            Sprite[] sprites = texture.GetSpritesInDesign();
            if (sprites != null)
            {
                spriteData.Sprites.Clear();
                spriteData.Name = texture.name.RemoveLastMatch("#");

                for (var i = 0; i < sprites.Length; i++)
                {
                    var sprite = sprites[i];
                    var uvRect = sprite.rect;
                    uvRect.x /= sprite.texture.width;
                    uvRect.y /= sprite.texture.height;
                    uvRect.width /= sprite.texture.width;
                    uvRect.height /= sprite.texture.height;
                    spriteData.Sprites.Add(sprite);
                    spriteData.UVRects.Add(uvRect);
                }
            }

            spriteData.Name = texture.name.RemoveLastMatch("#");
        }

        /// <summary>
        /// Удаления анимационной цепочки спрайтов
        /// </summary>
        /// <param name="index">Индекс анимационной цепочки спрайтов</param>
        public void RemoveSprite(int index)
        {
            _sprites.RemoveAt(index);
        }

        /// <summary>
        /// Получение спрайта
        /// </summary>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Спрайт или null если такой цепочки не обнаружено</returns>
        public Sprite GetFrameSprite(int index, int numberFrame)
        {
#if UNITY_EDITOR
            if (index >= _sprites.Count)
            {
                Debug.LogErrorFormat("Index[{0}] > Sprites.Count[{1}]", index, _sprites.Count);
            }
            if (numberFrame >= _sprites[index].Sprites.Count)
            {
                Debug.LogErrorFormat("NumberFrame[{0}] > Sprites[{1}].Sprites.Count[{2}]",
                    numberFrame, index, _sprites[index].Sprites.Count);
            }
#endif

            return _sprites[index].Sprites[numberFrame];
        }

        /// <summary>
        /// Получение прямоугольника текстурных координат спрайта
        /// </summary>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Прямоугольник текстурных координат спрайта</returns>
        public Rect GetFrameUVRect(int index, int numberFrame)
        {
            return _sprites[index].UVRects[numberFrame];
        }

        /// <summary>
        /// Получение спрайта
        /// </summary>
        /// <param name="spriteName">Имя анимационной цепочки. Имя спрайта(текстуры) без префикса последовательности</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Спрайт или null если такой цепочки не обнаружено</returns>
        public Sprite GetFrameSprite(string spriteName, int numberFrame)
        {
            for (var i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].Name == spriteName)
                {
                    return _sprites[i].Sprites[numberFrame];
                }
            }

            return null;
        }

        /// <summary>
        /// Получение прямоугольника текстурных координат спрайта
        /// </summary>
        /// <param name="spriteName">Имя анимационной цепочки. Имя спрайта(текстуры) без префикса последовательности</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Прямоугольник текстурных координат спрайта</returns>
        public Rect GetFrameUVRect(string spriteName, int numberFrame)
        {
            for (var i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].Name == spriteName)
                {
                    return _sprites[i].UVRects[numberFrame];
                }
            }

            return Rect.zero;
        }

        /// <summary>
        /// Получение времени смены кадра при данном времени
        /// </summary>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <param name="time">Время анимации</param>
        /// <returns>Время смены кадра</returns>
        public float GetFrameTime(int index, float time)
        {
            return time / _sprites[index].Sprites.Count;
        }

        /// <summary>
        /// Получение количества спрайтов анимационной цепочки
        /// </summary>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <returns>Количество кадров</returns>
        public int GetFrameCount(int index)
        {
            return _sprites[index].Sprites.Count;
        }

        /// <summary>
        /// Получение количества спрайтов анимационной цепочки
        /// </summary>
        /// <param name="spriteName">Имя анимационной цепочки. Имя спрайта(текстуры) без префикса последовательности</param>
        /// <returns>Количество кадров</returns>
        public int GetFrameCount(string spriteName)
        {
            for (var i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].Name == spriteName)
                {
                    return _sprites[i].Sprites.Count;
                }
            }

            return 0;
        }
        #endregion

        #region Unity editor methods
        /// <summary>
        /// Возвращения высоты элемента для рисования
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Высота элемента</returns>
        public float OnHeightItem(int index)
        {
            return (XInspectorViewParams.CONTROL_HEIGHT_SPACE * 4) + XInspectorViewParams.SPACE;
        }

        /// <summary>
        /// Рисование полей объекта
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="index">Индекс элемента</param>
        public void OnDrawItem(Rect position, int index)
        {
#if UNITY_EDITOR
            var spriteData = _sprites[index];

            //
            // Имя
            //
            var rectNameLabel = position;
            rectNameLabel.width = 40;
            rectNameLabel.height = XInspectorViewParams.CONTROL_HEIGHT;

            var rectNameField = rectNameLabel;
            rectNameField.x = rectNameLabel.xMax;
            rectNameField.width = position.width - (XInspectorViewParams.CONTROL_HEIGHT * 3) - rectNameLabel.width;
            rectNameField.height = XInspectorViewParams.CONTROL_HEIGHT;

            UnityEditor.EditorGUI.LabelField(rectNameLabel, nameof(CTweenSpriteData.Name));
            spriteData.Name = UnityEditor.EditorGUI.TextField(rectNameField, spriteData.Name);

            //
            // Время
            //
            var rectTimeLabel = rectNameLabel;
            rectTimeLabel.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;

            var rectTimeField = rectTimeLabel;
            rectTimeField.x = rectTimeLabel.xMax;
            rectTimeField.width = position.width - (XInspectorViewParams.CONTROL_HEIGHT * 3) - rectTimeLabel.width;
            rectTimeField.height = XInspectorViewParams.CONTROL_HEIGHT;

            UnityEditor.EditorGUI.LabelField(rectTimeLabel, "Time");
            spriteData.TimeAnimation = UnityEditor.EditorGUI.Slider(rectTimeField, spriteData.TimeAnimation,
                0.1f, 6.0f);

            //
            // Проигрывание
            //
            var rectPlayLabel = rectTimeLabel;
            rectPlayLabel.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;

            var rectPlayField = rectTimeField;
            rectPlayField.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;

            UnityEditor.EditorGUI.LabelField(rectPlayLabel, "Index");
            if (spriteData.Sprites.Count > 1)
            {
                spriteData._currentIndex = UnityEditor.EditorGUI.IntSlider(rectPlayField, spriteData._currentIndex,
                    0, spriteData.Sprites.Count - 1);
            }
            else
            {
                UnityEditor.EditorGUI.LabelField(rectPlayField, "Count sprite = 1");
            }

            //
            // Спрайт
            //
            var rectSprite = rectNameField;
            rectSprite.x = rectNameField.xMax + 1;
            rectSprite.width = (XInspectorViewParams.CONTROL_HEIGHT * 3) - 2;
            rectSprite.height = rectSprite.width;

            spriteData.Sprites[spriteData._currentIndex] =
                UnityEditor.EditorGUI.ObjectField(rectSprite, spriteData.Sprites[spriteData._currentIndex], typeof(Sprite), false) as
                Sprite;

            //
            // Заполнить
            //
            var rectButton = rectSprite;
            rectButton.y += rectSprite.height + XInspectorViewParams.SPACE;
            rectButton.height = XInspectorViewParams.CONTROL_HEIGHT;

            if (GUI.Button(rectButton, "Fill", UnityEditor.EditorStyles.miniButton))
            {
                if (spriteData.Sprites[0] != null)
                {
                    SetFromTexture(spriteData.Sprites[0].texture, index);
                }
            }
#endif
        }
        #endregion
    }

    /// <summary>
    /// Хранилище (ресурс) анимационных спрайтов
    /// </summary>
    /// <remarks>
    /// Ресурс представляет методы добавления, удаления, получения анимационных спрайтов (последовательности спрайтов
    /// одной анимационной цепочки), а также заполнение анимационных цепочек на основе текстуры спрайта
    /// </remarks>
    [Serializable]
    [LotusDescriptionType("Хранилище(ресурс) анимационных спрайтов")]
    public class LotusTweenSpriteStorage : ScriptableObject, ILotusResourceable
    {
        #region Fields
        [LotusHeaderGroupBox("Sprite Data")]
        [SerializeField]
        [LotusDisplayName("GroupSprites")]
        //[LotusReorderable(TitleFieldName =nameof(CTweenSpriteGroup.Name))]
        internal List<CTweenSpriteGroup> _groupSprites;
        #endregion

        #region Properties
        /// <summary>
        /// Список групп анимационных цепочек
        /// </summary>
        public List<CTweenSpriteGroup> GroupSprites
        {
            get { return _groupSprites; }
        }

        /// <summary>
        /// Количество групп анимационных цепочек
        /// </summary>
        public int GroupCount
        {
            get { return _groupSprites.Count; }
        }

        /// <summary>
        /// Общие количество анимационных цепочек
        /// </summary>
        public int TotalCount
        {
            get
            {
                var total = 0;
                for (var i = 0; i < _groupSprites.Count; i++)
                {
                    total += _groupSprites[i].Count;
                }
                return total;
            }
        }

        /// <summary>
        /// Сгруппированный список имен анимационных цепочек
        /// </summary>
        public string[] ListNames
        {
            get
            {
                var names = new string[TotalCount];
                var index = 0;
                for (var ig = 0; ig < _groupSprites.Count; ig++)
                {
                    var spriteGroup = _groupSprites[ig];
                    for (var i = 0; i < spriteGroup.Count; i++)
                    {
                        names[index] = spriteGroup.Name + "/" + spriteGroup[i].Name;
                        index++;
                    }
                }

                return names;
            }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Включение хранилища
        /// </summary>
        public void OnEnable()
        {
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация анимационных цепочек на основе глобального индекса
        /// </summary>
        /// <param name="index">Глобальный индекс</param>
        /// <returns>Анимационная цепочка</returns>
        public CTweenSpriteData this[int index]
        {
            get
            {
                int groupIndex = 0, groupSpriteIndex = 0;
                GetGroupIndexAndSpriteIndex(index, ref groupIndex, ref groupSpriteIndex);
                return _groupSprites[groupIndex][groupSpriteIndex];
            }
        }

        /// <summary>
        /// Индексация анимационных цепочек на основе индекса группы и индекса спрайта
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="groupSpriteIndex">Индекс анимационной цепочки в группе</param>
        /// <returns>Анимационная цепочка</returns>
        public CTweenSpriteData this[int groupIndex, int groupSpriteIndex]
        {
            get
            {
                return _groupSprites[groupIndex][groupSpriteIndex];
            }
        }
        #endregion

        #region ILotusResourceable methods
        /// <summary>
        /// Первичное создание хранилища анимационных спрайтов
        /// </summary>
        public void Create()
        {
            if (_groupSprites == null)
            {
                _groupSprites = new List<CTweenSpriteGroup>();
            }
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

        #region Main methods
        /// <summary>
        /// Получить глобальный индекс анимационной цепочки в хранилище
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="groupSpriteIndex">Индекс анимационной цепочки в группе</param>
        /// <returns>Глобальный индекс анимационной цепочки в хранилище</returns>
        public int GetStorageSpriteIndex(int groupIndex, int groupSpriteIndex)
        {
            var storageSpriteIndex = 0;
            for (var i = 0; i < groupIndex; i++)
            {
                storageSpriteIndex += _groupSprites[i].Count;
            }

            storageSpriteIndex += groupSpriteIndex;
            return storageSpriteIndex;
        }

        /// <summary>
        /// Получить индекс группы анимационных цепочек и индекс анимационной цепочки из группы
        /// </summary>
        /// <param name="storageSpriteIndex">Глобальный индекс анимационной цепочки в хранилище</param>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="groupSpriteIndex">Индекс анимационной цепочки в группе</param>
        public void GetGroupIndexAndSpriteIndex(int storageSpriteIndex, ref int groupIndex,
            ref int groupSpriteIndex)
        {
            if (storageSpriteIndex < _groupSprites[0].Count)
            {
                groupIndex = 0;
                groupSpriteIndex = storageSpriteIndex;
                return;
            }

            var totalSpriteIndex = 0;
            for (var i = 0; i < _groupSprites.Count; i++)
            {
                totalSpriteIndex += _groupSprites[i].Count;
                if (totalSpriteIndex > storageSpriteIndex)
                {
                    groupIndex = i;
                    groupSpriteIndex = _groupSprites[i].Count - (totalSpriteIndex - storageSpriteIndex);
                    break;
                }
            }
        }

        /// <summary>
        /// Получение спрайта
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Спрайт или null если такой цепочки не обнаружено</returns>
        public Sprite GetFrameSprite(int groupIndex, int index, int numberFrame)
        {
            return _groupSprites[groupIndex].GetFrameSprite(index, numberFrame);
        }

        /// <summary>
        /// Получение прямоугольника текстурных координат спрайта
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Прямоугольник текстурных координат спрайта</returns>
        public Rect GetFrameUVRect(int groupIndex, int index, int numberFrame)
        {
            return _groupSprites[groupIndex].GetFrameUVRect(index, numberFrame);
        }

        /// <summary>
        /// Получение спрайта
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="spriteName">Имя анимационной цепочки. Имя спрайта(текстуры) без префикса последовательности</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Спрайт или null если такой цепочки не обнаружено</returns>
        public Sprite GetFrameSprite(int groupIndex, string spriteName, int numberFrame)
        {
            return _groupSprites[groupIndex].GetFrameSprite(spriteName, numberFrame);
        }

        /// <summary>
        /// Получение прямоугольника текстурных координат спрайта
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="spriteName">Имя анимационной цепочки. Имя спрайта(текстуры) без префикса последовательности</param>
        /// <param name="numberFrame">Номер спрайта</param>
        /// <returns>Прямоугольник текстурных координат спрайта</returns>
        public Rect GetFrameUVRect(int groupIndex, string spriteName, int numberFrame)
        {
            return _groupSprites[groupIndex].GetFrameUVRect(spriteName, numberFrame);
        }

        /// <summary>
        /// Получение времени смены кадра при данном времени
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <param name="time">Время анимации</param>
        /// <returns>Время смены кадра</returns>
        public float GetFrameTime(int groupIndex, int index, float time)
        {
            return _groupSprites[groupIndex].GetFrameTime(index, time);
        }

        /// <summary>
        /// Получение количества спрайтов анимационной цепочки
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="index">Индекс анимационной цепочки</param>
        /// <returns>Количество кадров</returns>
        public int GetFrameCount(int groupIndex, int index)
        {
            return _groupSprites[groupIndex].GetFrameCount(index);
        }

        /// <summary>
        /// Получение количества спрайтов анимационной цепочки
        /// </summary>
        /// <param name="groupIndex">Индекс группы анимационных цепочек</param>
        /// <param name="spriteName">Имя анимационной цепочки. Имя спрайта(текстуры) без префикса последовательности</param>
        /// <returns>Количество кадров</returns>
        public int GetFrameCount(int groupIndex, string spriteName)
        {
            return _groupSprites[groupIndex].GetFrameCount(spriteName);
        }

        /// <summary>
        /// Добавить группу спрайтов из директории
        /// </summary>
        [LotusHeaderGroupBox("Sprite Manager")]
        [LotusMethodCall]
        public void AddGroupFromDirectory()
        {
#if UNITY_EDITOR
            var path = UnityEditor.EditorUtility.OpenFolderPanel("Select folder",
                XCoreSettings.ASSETS_PATH, "");
            if (path.IsExists())
            {
                var spriteGroup = new CTweenSpriteGroup();
                spriteGroup.Name = System.IO.Path.GetFileName(path);

                path = path.Remove(0, path.IndexOf(XCoreSettings.ASSETS_PATH));
                var textures = XEditorAssetDatabase.GetAssetsFromFolder<Texture2D>(path);
                for (var i = 0; i < textures.Count; i++)
                {
                    spriteGroup.AddSpriteFromTexture(textures[i]);
                }

                _groupSprites.Add(spriteGroup);
            }
#endif
        }
        #endregion
    }

    /**@}*/
}
#endif