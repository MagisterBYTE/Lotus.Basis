#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;

using Lotus.Core;
using Lotus.Core.Inspector;

using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/

    /// <summary>
    /// Управляемый список для расширенного управления элементами коллекций
    /// </summary>
    /// <remarks>
    /// Только для режима редактора Unity
    /// </remarks>
    public sealed class CReorderableList : IDisposable
    {
        #region Fields
        // Основные параметры
        internal Type _typeItem;
        internal IList _collection;
        internal UnityEditorInternal.ReorderableList _list;
        internal UnityEditor.SerializedProperty _property;
        internal UnityEditor.SerializedProperty _itemsProperty;
        internal UnityEditor.SerializedObject _serializedObject;
        internal string _propertyPath;
        internal bool _isStandardCollection;
        internal GUIContent _contentItem;

        // Работа с атрибутами
        internal LotusReorderableAttribute _reorderAttribute;
        internal List<LotusColumnAttribute> _columnsAttribute;
        internal bool _isDrawInline;

        // Обратные вызовы
        internal Action _onAddItem;
        internal Action<int> _onRemoveItem;
        internal Func<int, bool> _onCanRemoveItem;
        internal Action<int, int> _onReorderItem;
        internal Func<int, float> _onHeightItem;
        internal Action<Rect> _onDrawHeader;
        internal Func<bool, string> _onContentHeader;

        // Внутренние данные
        internal Action _internalContextMenu;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Статус раскрытия/закрытия списка
        /// </summary>
        public bool IsExpanded { get; set; }

        /// <summary>
        /// Связанное со списком сериализируемое свойство
        /// </summary>
        public UnityEditor.SerializedProperty Property
        {
            get { return _property; }
        }

        /// <summary>
        /// Статус стандартной коллекции
        /// </summary>
        public bool IsStandardCollection
        {
            get { return _isStandardCollection; }
        }

        /// <summary>
        /// Тип элемента коллекции
        /// </summary>
        public Type TypeItem
        {
            get
            {
                if (_typeItem == null)
                {
                    _typeItem = Collection.GetType().GetClassicCollectionItemType();
                }

                return _typeItem;
            }
        }

        /// <summary>
        /// Обобщенная коллекция
        /// </summary>
        public IList Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = _property.GetValue<IList>();
                }
                return _collection;
            }
        }

        /// <summary>
        /// Количество элементов в коллекции
        /// </summary>
        public int CountItems
        {
            get
            {
                if (IsStandardCollection)
                {
                    return _property.arraySize;
                }
                else
                {
                    if (_collection == null)
                    {
                        _collection = _property.GetValue<IList>();
                    }

                    return _collection.Count;
                }
            }
        }

        /// <summary>
        /// Список
        /// </summary>
        public UnityEditorInternal.ReorderableList List
        {
            get
            {
                return _list;
            }
        }

        //
        // ОБРАТНЫЕ ВЫЗОВЫ
        //
        /// <summary>
        /// Делегат для добавления элемента
        /// </summary>
        public Action OnAddItem
        {
            get { return _onAddItem; }
            set { _onAddItem = value; }
        }

        /// <summary>
        /// Делегат для удаления элемента
        /// </summary>
        public Action<int> OnRemoveItem
        {
            get { return _onRemoveItem; }
            set { _onRemoveItem = value; }
        }

        /// <summary>
        /// Функтор для определения возможности удаления элемента
        /// </summary>
        public Func<int, bool> OnCanRemoveItem
        {
            get { return _onCanRemoveItem; }
            set { _onCanRemoveItem = value; }
        }

        /// <summary>
        /// Делегат для информирования об изменение порядка элемента
        /// </summary>
        public Action<int, int> OnReorderItem
        {
            get { return _onReorderItem; }
            set { _onReorderItem = value; }
        }

        /// <summary>
        /// Делегат для вычисления высоты элемента
        /// </summary>
        public Func<int, float> OnHeightItem
        {
            get { return _onHeightItem; }
            set { _onHeightItem = value; }
        }

        //
        // РИСОВАНИЕ
        //
        /// <summary>
        /// Делегат рисования заголовка
        /// </summary>
        public Action<Rect> OnDrawHeader
        {
            get { return _onDrawHeader; }
            set { _onDrawHeader = value; }
        }

        /// <summary>
        /// Делегат предоставления надписи заголовка
        /// </summary>
        public Func<bool, string> OnContentHeader
        {
            get { return _onContentHeader; }
            set { _onContentHeader = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        public CReorderableList(UnityEditor.SerializedProperty property)
        {
            bool draggable = true, header = true, add = true, remove = true;
            IsExpanded = property.isExpanded;
            _property = property;

            // Это обычная коллекция Array или List
            if (_property.IsStandardCollection())
            {
                _list = new UnityEditorInternal.ReorderableList(_property.serializedObject, _property, draggable, header, add, remove);
                _isStandardCollection = true;
            }
            else
            {
                // Получаем коллекцию
                _itemsProperty = _property.GetLotusInnerCollection();
                _collection = _property.GetValue<IList>();
                _typeItem = _collection.GetType().GetClassicCollectionItemType();
                _list = new UnityEditorInternal.ReorderableList(_collection, _typeItem, draggable, header, add, remove);
                _isStandardCollection = false;
            }

            Init();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="list">Обобщенная коллекция</param>
        /// <param name="typeItem">Тип элемента коллекции</param>
        public CReorderableList(UnityEditor.SerializedProperty property, IList list, Type typeItem)
        {
            bool draggable = true, header = true, add = true, remove = true;
            IsExpanded = property.isExpanded;
            _property = property;

            _isStandardCollection = true;
            _collection = list;
            _typeItem = typeItem;

            _list = new UnityEditorInternal.ReorderableList(_collection, _typeItem, draggable, header, add, remove);

            Init();
        }
        #endregion

        #region IDisposable methods
        /// <summary>
        /// Освобождение управляемых ресурсов
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождение управляемых ресурсов
        /// </summary>
        /// <param name="disposing">Статус освобождения</param>
        private void Dispose(bool disposing)
        {
            // Освобождаем только управляемые ресурсы
            if (disposing)
            {
#if UNITY_EDITOR
                _list = null;
                _collection = null;
                _typeItem = null;
                if (_property != null)
                {
                    _property.Dispose();
                    _property = null;
                }

                if (_itemsProperty != null)
                {
                    _itemsProperty.Dispose();
                    _itemsProperty = null;
                }
#endif
            }

            // Освобождаем неуправляемые ресурсы
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Дополнительная инициализация списка
        /// </summary>
        private void Init()
        {
            _contentItem = new GUIContent(string.Empty);

            _list.onAddCallback = OnAddItemDefault;
            _list.onChangedCallback = OnChangedItemDefault;
            _list.onRemoveCallback = OnRemoveItemDefault;
            _list.onCanRemoveCallback = OnCanRemoveItemDefault;
            _list.onReorderCallbackWithDetails = OnReorderItemDefault;
            _list.drawHeaderCallback = OnDrawHeaderDefaultOpened;
            _list.drawElementCallback = OnDrawItemDefault;
            _list.elementHeightCallback = OnHeightItemDefault;

            if (_property != null)
            {
                _reorderAttribute = _property.GetAttribute<LotusReorderableAttribute>();
                if (_reorderAttribute != null)
                {
                    // Изменение
                    if (_reorderAttribute.ItemsChangedMethodName.IsExists())
                    {
                    }
                    if (_reorderAttribute.ContextMenuMethodName.IsExists())
                    {
                        _list.onAddDropdownCallback = OnContextMenuDefault;
                    }

                    // Сортировка
                    if (_reorderAttribute.SortAscendingMethodName.IsExists())
                    {
                    }
                    if (_reorderAttribute.SortDescendingMethodName.IsExists())
                    {
                    }
                    if (_reorderAttribute.ReorderItemChangedMethodName.IsExists())
                    {
                    }

                    // Рисование
                    if (_reorderAttribute.DrawItemMethodName.IsExists())
                    {
                    }
                    if (_reorderAttribute.HeightItemMethodName.IsExists())
                    {
                    }
                }

                if (_property.propertyType == UnityEditor.SerializedPropertyType.Generic)
                {
                    if (XEditorCachedData.GetAttribute<LotusInLineAttribute>(_property) != null)
                    {
                        _isDrawInline = true;
                    }
                }
            }
        }

        /// <summary>
        /// Получение высоты отображения списка
        /// </summary>
        /// <returns>Высота отображения списка</returns>
        public float GetHeight()
        {
            if (IsExpanded)
            {
                return _list.GetHeight();
            }
            else
            {
                return XInspectorViewParams.CONTROL_HEIGHT_SPACE + XInspectorViewParams.SPACE;
            }
        }

        /// <summary>
        /// Рисование управляющих кнопок
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        private void DrawControlButton(Rect position)
        {
            var count = 2;
            if (_reorderAttribute.SortAscendingMethodName.IsExists()) count++;
            if (_reorderAttribute.SortDescendingMethodName.IsExists()) count++;
            var widthDefault = (position.width - 2) / count;

            //
            // Ascending/descending
            //
            Rect rectButtonSortDescending = Rect.zero;
            rectButtonSortDescending.x = position.xMax;
            if (_reorderAttribute.SortDescendingMethodName.IsExists())
            {
                rectButtonSortDescending.x = position.xMax - widthDefault;
                rectButtonSortDescending.y = position.y;
                rectButtonSortDescending.width = widthDefault;
                rectButtonSortDescending.height = _list.headerHeight - 2;
                if (GUI.Button(rectButtonSortDescending, "Sort " + XStringHelper.TriangleDown, UnityEditor.EditorStyles.miniButtonMid))
                {
                    _property.Invoke(_reorderAttribute.SortDescendingMethodName);
                }
            }

            Rect rectButtonSortAscending = Rect.zero;
            rectButtonSortAscending.y = position.y;
            rectButtonSortAscending.width = widthDefault;
            rectButtonSortAscending.height = _list.headerHeight - 2;
            if (_reorderAttribute.SortAscendingMethodName.IsExists())
            {
                rectButtonSortAscending.x = rectButtonSortDescending.x - widthDefault;
                if (GUI.Button(rectButtonSortAscending, "Sort " + XStringHelper.TriangleUp, UnityEditor.EditorStyles.miniButtonMid))
                {
                    _property.Invoke(_reorderAttribute.SortAscendingMethodName);
                }
            }
        }

        /// <summary>
        /// Рисование списка в автоматическом макете
        /// </summary>
        public void DrawLayout()
        {
            if (IsExpanded)
            {
                _list.DoLayoutList();
            }
            else
            {
                XEditorInspector.PrepareContent(_property);

                Rect rect = UnityEditor.EditorGUILayout.BeginHorizontal();
                {
                    // Рисуем фон простым прямоугольником
                    GUI.Box(rect, string.Empty);

                    // Смещаем надпись свойства в область списка
                    GUILayout.Space(12);
                    var name = XEditorInspector.Content.text + " (" + CountItems.ToString() + ")";
                    _property.isExpanded = UnityEditor.EditorGUILayout.Foldout(_property.isExpanded, name, true);
                    IsExpanded = _property.isExpanded;
                }
                UnityEditor.EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// Рисование списка
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        public void Draw(Rect position)
        {
            if (IsExpanded)
            {
                _list.DoList(position);
            }
            else
            {
                XEditorInspector.PrepareContent(_property);

                // Рисуем фон простым прямоугольником
                GUI.Box(position, string.Empty);

                // Смещаем надпись свойства в область списка
                position.x += 12;
                var name = XEditorInspector.Content.text + " (" + CountItems.ToString() + ")";

                if (_onContentHeader != null)
                {
                    name = _onContentHeader(false);
                }

                _property.isExpanded = UnityEditor.EditorGUI.Foldout(position, _property.isExpanded, name, true);
                IsExpanded = _property.isExpanded;
            }
        }

        /// <summary>
        /// Рисование элемента по умолчанию
        /// </summary>
        /// <param name="item">Сериализируемое свойство</param>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="index">Индекс элемента</param>
        public void DrawItemDefault(UnityEditor.SerializedProperty item, Rect position, int index)
        {
            // Для сложного свойства используем более подробное описание 
            if (item.propertyType == UnityEditor.SerializedPropertyType.Generic)
            {
                position.xMin += 8;
                position.y += 2;

                _contentItem.text = "Item " + index.ToString();

                if (_reorderAttribute != null && _reorderAttribute.TitleFieldName.IsExists())
                {
                    UnityEditor.SerializedProperty innerProperty = item.FindPropertyRelative(_reorderAttribute.TitleFieldName);
                    if (innerProperty != null && innerProperty.propertyType == UnityEditor.SerializedPropertyType.String)
                    {
                        if (innerProperty.stringValue.IsExists())
                        {
                            _contentItem.text = innerProperty.stringValue + '[' + index.ToString() + ']';
                        }
                    }
                }
            }
            else
            {
                _contentItem.text = "Item " + index.ToString();
            }

            UnityEditor.EditorGUI.BeginChangeCheck();
            {
                UnityEditor.EditorGUI.PropertyField(position, item, _contentItem, item.isExpanded);
            }
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    //if (_collection is ILotusNotifyCollectionChanged)
                    //{
                    //  item.Save();
                    //  ILotusNotifyCollectionChanged collectionChanged = _collection as ILotusNotifyCollectionChanged;
                    //  collectionChanged.OnCollectionChanged?.Invoke(TNotifyCollectionChangedAction.Replace, _collection[index], index, 1);
                    //}
                }
            }
        }

        /// <summary>
        /// Рисование элемента (всех его дочерних элементов) в одну линию
        /// </summary>
        /// <param name="item">Сериализируемое свойство</param>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="index">Индекс элемента</param>
        public void DrawItemInLine(UnityEditor.SerializedProperty item, Rect position, int index)
        {
            UnityEditor.EditorGUI.BeginChangeCheck();
            {
                List<UnityEditor.SerializedProperty> childs = item.GetChildrenProperties();

                // Получаем информацию о столбцах
                if (_columnsAttribute == null)
                {
                    _columnsAttribute = new List<LotusColumnAttribute>();
                    for (var i = 0; i < childs.Count; i++)
                    {
                        LotusColumnAttribute columnAttribute = XEditorCachedData.GetAttribute<LotusColumnAttribute>(childs[i]);
                        _columnsAttribute.Add(columnAttribute);
                    }
                }

                // Считать будем от ширины с минусом по единицы на каждую колонку и минус ширина для вывода индекса
                float indexWidth = 12;
                if (CountItems > 9)
                {
                    indexWidth = 16;
                }
                var totalWidth = position.width - childs.Count - indexWidth;

                // На первом элементе отображаем заголовки
                if (index == 0)
                {
                    Rect rectHeader = position;
                    rectHeader.x += indexWidth;
                    rectHeader.height = XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    for (var i = 0; i < childs.Count; i++)
                    {
                        if (_columnsAttribute[i] != null)
                        {
                            rectHeader.width = Mathf.Ceil(_columnsAttribute[i].Percent / 100 * totalWidth);

                            XEditorInspector.PrepareContent(childs[i]);

                            UnityEditor.EditorGUI.LabelField(rectHeader, XEditorInspector.Content, UnityEditor.EditorStyles.boldLabel);

                            rectHeader.x += rectHeader.width + 1;
                        }
                    }

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                }

                // Выводим индекс
                Rect rectIndex = position;

                // При нулевом индексе корректируем высоту
                if (index == 0)
                {
                    rectIndex.y -= XInspectorViewParams.CONTROL_HEIGHT_SPACE / 2;
                }

                rectIndex.width = indexWidth;
                UnityEditor.EditorGUI.LabelField(rectIndex, index.ToString());
                position.x += indexWidth;

                for (var i = 0; i < childs.Count; i++)
                {
                    if (_columnsAttribute[i] != null)
                    {
                        position.height = UnityEditor.EditorGUI.GetPropertyHeight(childs[i], _contentItem, childs[i].isExpanded);
                        position.width = Mathf.Ceil(_columnsAttribute[i].Percent / 100 * totalWidth);

                        UnityEditor.EditorGUI.PropertyField(position, childs[i], GUIContent.none, childs[i].isExpanded);

                        position.x += position.width + 1;
                    }
                }
            }
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    //if (_collection is ILotusNotifyCollectionChanged)
                    //{
                    //  item.Save();
                    //  ILotusNotifyCollectionChanged collectionChanged = _collection as ILotusNotifyCollectionChanged;
                    //  collectionChanged.OnCollectionChanged?.Invoke(TNotifyCollectionChangedAction.Replace, _collection[index], index, 1);
                    //}
                }
            }
        }

        /// <summary>
        /// Переустановка параметров списка
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        public void Reset(UnityEditor.SerializedProperty property)
        {
            _property = property;

            // Это обычная коллекция Array или List
            if (property.IsStandardCollection())
            {
                _list.serializedProperty = _property;
                _isStandardCollection = true;

                if (property.serializedObject == null)
                {
                    Debug.Log("property.serializedObject == null");
                }
            }
            else
            {
                // Получаем коллекцию
                _itemsProperty = property.GetLotusInnerCollection();
                _collection = property.GetValue<IList>();
                _typeItem = _collection.GetType().GetClassicCollectionItemType();
                _list.list = _collection;
                _isStandardCollection = false;
            }

            Init();
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Добавление элемента в список
        /// </summary>
        /// <param name="list">Список</param>
        private void OnAddItemDefault(UnityEditorInternal.ReorderableList list)
        {
            if (_isStandardCollection)
            {
                if (_onAddItem != null)
                {
                    _onAddItem();
                }
                else
                {
                    _property.arraySize++;
                }
            }
            else
            {
                if (_onAddItem != null)
                {
                    _onAddItem();
                }
                else
                {
                    Collection.Add(XReflection.CreateInstance(TypeItem));
                }
            }
        }

        /// <summary>
        /// Изменение элемента списка
        /// </summary>
        /// <param name="list">Список</param>
        private void OnChangedItemDefault(UnityEditorInternal.ReorderableList list)
        {
            if (_reorderAttribute != null && _reorderAttribute.ItemsChangedMethodName.IsExists())
            {
                _property.Invoke(_reorderAttribute.ItemsChangedMethodName);
            }
        }

        /// <summary>
        /// Удаление элемента из списка
        /// </summary>
        /// <param name="list">Список</param>
        private void OnRemoveItemDefault(UnityEditorInternal.ReorderableList list)
        {
            if (_isStandardCollection)
            {
                if (_onRemoveItem != null)
                {
                    _onRemoveItem(list.index);
                }
                else
                {
                    _property.DeleteArrayElementAtIndex(list.index);
                }
            }
            else
            {
                if (_onRemoveItem != null)
                {
                    _onRemoveItem(list.index);
                }
                else
                {
                    Collection.RemoveAt(list.index);
                }
            }
        }

        /// <summary>
        /// Возможность удаление элемента из списка
        /// </summary>
        /// <param name="list">Список</param>
        /// <returns>Статус удаления</returns>
        private bool OnCanRemoveItemDefault(UnityEditorInternal.ReorderableList list)
        {
            if (_onCanRemoveItem != null)
            {
                return _onCanRemoveItem(list.index);
            }
            else
            {
                return list.count > 0;
            }
        }

        /// <summary>
        /// Изменение позиции элемента из списка
        /// </summary>
        /// <param name="list">Список</param>
        /// <param name="oldIndex">Старый индекс</param>
        /// <param name="newIndex">Новый индекс</param>
        private void OnReorderItemDefault(UnityEditorInternal.ReorderableList list, int oldIndex, int newIndex)
        {
            if (_onReorderItem != null)
            {
                _onReorderItem(oldIndex, newIndex);
            }

            if (_reorderAttribute != null && _reorderAttribute.ReorderItemChangedMethodName.IsExists())
            {
                _property.Invoke(_reorderAttribute.ReorderItemChangedMethodName, oldIndex, newIndex);
            }

            if (UnityEditor.EditorApplication.isPlaying)
            {
                //if (_collection is ILotusNotifyCollectionChanged)
                //{
                //  ILotusNotifyCollectionChanged collectionChanged = _collection as ILotusNotifyCollectionChanged;
                //  collectionChanged.OnCollectionChanged?.Invoke(TNotifyCollectionChangedAction.Move, null, oldIndex, newIndex);
                //}
            }
        }

        /// <summary>
        /// Отображение контекстного меню
        /// </summary>
        /// <param name="position">Позиция контекстного меню</param>
        /// <param name="list">Список</param>
        private void OnContextMenuDefault(Rect position, UnityEditorInternal.ReorderableList list)
        {
            var collection = (IList)_property.Invoke(_reorderAttribute.ContextMenuMethodName);
            if (collection != null)
            {
                _serializedObject = _property.serializedObject;
                _propertyPath = _property.propertyPath;
                var menu = new UnityEditor.GenericMenu();
                for (var i = 0; i < collection.Count; i++)
                {
                    var value = collection[i];
                    var title = collection[i].ToString();
                    menu.AddItem(new GUIContent(title), false, OnContextMenuItemSelect, value);
                }

                menu.ShowAsContext();
            }
        }

        /// <summary>
        /// Выбор элемента контекстного меню
        /// </summary>
        /// <param name="menuItem">Элемент контекстного меню</param>
        private void OnContextMenuItemSelect(object menuItem)
        {
            if (_isStandardCollection)
            {
                try
                {
                    _serializedObject.Update();
                    UnityEditor.SerializedProperty property = _serializedObject.FindProperty(_propertyPath);
                    if (property != null)
                    {
                        var index = property.arraySize;
                        property.arraySize++;
                        _serializedObject.ApplyModifiedProperties();

                        _list.index = index;
                        var item = property.GetArrayElementAtIndex(index);
                        if (item != null)
                        {
                            item.SetValue(menuItem);
                        }
                    }

                    _serializedObject.ApplyModifiedProperties();
                }
                catch (Exception exc)
                {
                    Debug.LogError(exc.Message);
                }
            }
            else
            {
                Collection.Add(XReflection.CreateInstance(TypeItem));
                _collection[_collection.Count - 1] = menuItem;
            }

            if (_reorderAttribute != null && _reorderAttribute.ItemsChangedMethodName.IsExists())
            {
                _property.Invoke(_reorderAttribute.ItemsChangedMethodName);
            }
        }

        /// <summary>
        /// Рисование заголовка списка (когда он раскрыт)
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        private void OnDrawHeaderDefaultOpened(Rect position)
        {
            if (_onDrawHeader != null)
            {
                _onDrawHeader(position);
            }
            else
            {
                XEditorInspector.PrepareContent(_property);
                position.x += 12;
                Vector2 size = UnityEditor.EditorStyles.label.CalcSize(XEditorInspector.Content);
                Rect rectLabel = position;
                rectLabel.width = size.x + 2;

                if (_onContentHeader != null)
                {
                    XEditorInspector.Content.text = _onContentHeader(true);
                }

                _property.isExpanded = UnityEditor.EditorGUI.Foldout(rectLabel, _property.isExpanded, XEditorInspector.Content, false);
                IsExpanded = _property.isExpanded;
            }

            if (_reorderAttribute != null)
            {
                if (_reorderAttribute.SortAscendingMethodName.IsExists() ||
                    _reorderAttribute.SortDescendingMethodName.IsExists())
                {
                    position.width -= 6;
                    DrawControlButton(position);
                }
            }
        }

        /// <summary>
        /// Получение высоты элемента по его индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Высота элемента</returns>
        private float OnHeightItemDefault(int index)
        {
            if (_onHeightItem != null)
            {
                return _onHeightItem(index);
            }
            else
            {
                if (_reorderAttribute != null && _reorderAttribute.HeightItemMethodName.IsExists())
                {
                    return (float)_property.Invoke(_reorderAttribute.HeightItemMethodName, index);
                }
                else
                {
                    UnityEditor.SerializedProperty item = null;
                    if (_isStandardCollection)
                    {
                        item = _property.GetArrayElementAtIndex(index);
                    }
                    else
                    {
                        if (_itemsProperty == null)
                        {
                            _itemsProperty = _property.GetLotusInnerCollection();
                        }
                        if (_itemsProperty != null)
                        {
                            if (index > _itemsProperty.arraySize - 1)
                            {
                                index = _itemsProperty.arraySize - 1;
                            }
                            item = _itemsProperty.GetArrayElementAtIndex(index);

                        }
                    }

                    if (item != null)
                    {
                        if (_isDrawInline)
                        {
                            var maxHeight = XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            List<UnityEditor.SerializedProperty> childs = item.GetChildrenProperties();

                            for (var i = 0; i < childs.Count; i++)
                            {
                                var height = UnityEditor.EditorGUI.GetPropertyHeight(childs[i]);
                                if (height > maxHeight)
                                {
                                    maxHeight = height + XInspectorViewParams.SPACE;
                                }
                            }

                            if (index == 0)
                            {
                                maxHeight += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            }

                            return maxHeight;
                        }
                        else
                        {
                            var defaultHeight = XInspectorViewParams.CONTROL_HEIGHT;
                            var computeHeight = UnityEditor.EditorGUI.GetPropertyHeight(item, GUIContent.none, true);

                            return Mathf.Max(defaultHeight, computeHeight) + XInspectorViewParams.SPACE;
                        }
                    }
                    else
                    {
                        return XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    }
                }
            }
        }

        /// <summary>
        /// Рисование элемента списка
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="index">Индекс элемента</param>
        /// <param name="active">Статус активности элемента</param>
        /// <param name="focused">Статус фокуса элемента</param>
        private void OnDrawItemDefault(Rect position, int index, bool active, bool focused)
        {
            if (_reorderAttribute != null && _reorderAttribute.DrawItemMethodName.IsExists())
            {
                _property.Invoke(_reorderAttribute.DrawItemMethodName, position, index);
            }
            else
            {
                UnityEditor.SerializedProperty item = null;
                if (_isStandardCollection)
                {
                    item = _property.GetArrayElementAtIndex(index);
                }
                else
                {
                    if (_itemsProperty == null)
                    {
                        _itemsProperty = _property.GetLotusInnerCollection();
                    }
                    if (_itemsProperty != null)
                    {
                        if (index > _itemsProperty.arraySize - 1)
                        {
                            index = _itemsProperty.arraySize - 1;
                        }
                        item = _itemsProperty.GetArrayElementAtIndex(index);
                    }
                }

                // Уменьшаем высоту области отображения до стандартного и вследствие это образуется отступы между элементами списка
                position.height -= UnityEditor.EditorGUIUtility.standardVerticalSpacing;

                if (item != null)
                {
                    if (_isDrawInline)
                    {
                        DrawItemInLine(item, position, index);
                    }
                    else
                    {
                        DrawItemDefault(item, position, index);
                    }
                }
                else
                {
                    GUI.Label(position, "NULL (" + TypeItem.Name + ")", UnityEditor.EditorStyles.boldLabel);
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Статический класс содержащий словарь всех управляемых списков
    /// </summary>
    public static class XReorderableList
    {
        #region Fields
        public static Dictionary<string, CReorderableList> _reorderableLists = new Dictionary<string, CReorderableList>();
        #endregion

        #region Properties
        /// <summary>
        /// Словарь списков
        /// </summary>
        public static Dictionary<string, CReorderableList> ReorderableLists
        {
            get
            {
                if (_reorderableLists == null)
                {
                    _reorderableLists = new Dictionary<string, CReorderableList>();
                }

                return _reorderableLists;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение управляемого списка для указанного свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Управляемый список список</returns>
        public static CReorderableList Get(UnityEditor.SerializedProperty property)
        {
            var key = property.GetHashNameInstance();
            if (ReorderableLists.TryGetValue(key, out var result))
            {
                result.Reset(property);
                return result;
            }

            result = new CReorderableList(property);
            ReorderableLists.Add(key, result);
            return result;
        }
        #endregion
    }

    /**@}*/
}
#endif