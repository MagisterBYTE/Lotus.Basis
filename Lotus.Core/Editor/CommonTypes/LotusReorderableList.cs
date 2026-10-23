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
        internal Type mTypeItem;
        internal IList mCollection;
        internal UnityEditorInternal.ReorderableList mList;
        internal UnityEditor.SerializedProperty mProperty;
        internal UnityEditor.SerializedProperty mItemsProperty;
        internal UnityEditor.SerializedObject mSerializedObject;
        internal String mPropertyPath;
        internal Boolean mIsStandardCollection;
        internal GUIContent mContentItem;

        // Работа с атрибутами
        internal LotusReorderableAttribute mReorderAttribute;
        internal List<LotusColumnAttribute> mColumnsAttribute;
        internal Boolean mIsDrawInline;

        // Обратные вызовы
        internal Action mOnAddItem;
        internal Action<Int32> mOnRemoveItem;
        internal Func<Int32, Boolean> mOnCanRemoveItem;
        internal Action<Int32, Int32> mOnReorderItem;
        internal Func<Int32, Single> mOnHeightItem;
        internal Action<Rect> mOnDrawHeader;
        internal Func<Boolean, String> mOnContentHeader;

        // Внутренние данные
        internal Action mInternalContextMenu;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Статус раскрытия/закрытия списка
        /// </summary>
        public Boolean IsExpanded { get; set; }

        /// <summary>
        /// Связанное со списком сериализируемое свойство
        /// </summary>
        public UnityEditor.SerializedProperty Property
        {
            get { return (mProperty); }
        }

        /// <summary>
        /// Статус стандартной коллекции
        /// </summary>
        public Boolean IsStandardCollection
        {
            get { return (mIsStandardCollection); }
        }

        /// <summary>
        /// Тип элемента коллекции
        /// </summary>
        public Type TypeItem
        {
            get
            {
                if (mTypeItem == null)
                {
                    mTypeItem = Collection.GetType().GetClassicCollectionItemType();
                }

                return (mTypeItem);
            }
        }

        /// <summary>
        /// Обобщенная коллекция
        /// </summary>
        public IList Collection
        {
            get
            {
                if (mCollection == null)
                {
                    mCollection = mProperty.GetValue<IList>();
                }
                return (mCollection);
            }
        }

        /// <summary>
        /// Количество элементов в коллекции
        /// </summary>
        public Int32 CountItems
        {
            get
            {
                if (IsStandardCollection)
                {
                    return (mProperty.arraySize);
                }
                else
                {
                    if (mCollection == null)
                    {
                        mCollection = mProperty.GetValue<IList>();
                    }

                    return (mCollection.Count);
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
                return (mList);
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
            get { return (mOnAddItem); }
            set { mOnAddItem = value; }
        }

        /// <summary>
        /// Делегат для удаления элемента
        /// </summary>
        public Action<Int32> OnRemoveItem
        {
            get { return (mOnRemoveItem); }
            set { mOnRemoveItem = value; }
        }

        /// <summary>
        /// Функтор для определения возможности удаления элемента
        /// </summary>
        public Func<Int32, Boolean> OnCanRemoveItem
        {
            get { return (mOnCanRemoveItem); }
            set { mOnCanRemoveItem = value; }
        }

        /// <summary>
        /// Делегат для информирования об изменение порядка элемента
        /// </summary>
        public Action<Int32, Int32> OnReorderItem
        {
            get { return (mOnReorderItem); }
            set { mOnReorderItem = value; }
        }

        /// <summary>
        /// Делегат для вычисления высоты элемента
        /// </summary>
        public Func<Int32, Single> OnHeightItem
        {
            get { return (mOnHeightItem); }
            set { mOnHeightItem = value; }
        }

        //
        // РИСОВАНИЕ
        //
        /// <summary>
        /// Делегат рисования заголовка
        /// </summary>
        public Action<Rect> OnDrawHeader
        {
            get { return (mOnDrawHeader); }
            set { mOnDrawHeader = value; }
        }

        /// <summary>
        /// Делегат предоставления надписи заголовка
        /// </summary>
        public Func<Boolean, String> OnContentHeader
        {
            get { return (mOnContentHeader); }
            set { mOnContentHeader = value; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>

        public CReorderableList(UnityEditor.SerializedProperty property)
        {
            Boolean dragable = true, header = true, add = true, remove = true;
            IsExpanded = property.isExpanded;
            mProperty = property;

            // Это обычная коллекция Array или List
            if (mProperty.IsStandardCollection())
            {
                mList = new UnityEditorInternal.ReorderableList(mProperty.serializedObject, mProperty, dragable, header, add, remove);
                mIsStandardCollection = true;
            }
            else
            {
                // Получаем коллекцию
                mItemsProperty = mProperty.GetLotusInnerCollection();
                mCollection = mProperty.GetValue<IList>();
                mTypeItem = mCollection.GetType().GetClassicCollectionItemType();
                mList = new UnityEditorInternal.ReorderableList(mCollection, mTypeItem, dragable, header, add, remove);
                mIsStandardCollection = false;
            }

            Init();
        }


        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="list">Обобщенная коллекция</param>
        /// <param name="type_item">Тип элемента коллекции</param>

        public CReorderableList(UnityEditor.SerializedProperty property, IList list, Type type_item)
        {
            Boolean dragable = true, header = true, add = true, remove = true;
            IsExpanded = property.isExpanded;
            mProperty = property;

            mIsStandardCollection = true;
            mCollection = list;
            mTypeItem = type_item;

            mList = new UnityEditorInternal.ReorderableList(mCollection, mTypeItem, dragable, header, add, remove);

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

        private void Dispose(Boolean disposing)
        {
            // Освобождаем только управляемые ресурсы
            if (disposing)
            {
#if UNITY_EDITOR
					mList = null;
					mCollection = null;
					mTypeItem = null;
					if (mProperty != null)
					{
						mProperty.Dispose();
						mProperty = null;
					}

					if (mItemsProperty != null)
					{
						mItemsProperty.Dispose();
						mItemsProperty = null;
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
            mContentItem = new GUIContent("");

            mList.onAddCallback = OnAddItemDefault;
            mList.onChangedCallback = OnChangedItemDefault;
            mList.onRemoveCallback = OnRemoveItemDefault;
            mList.onCanRemoveCallback = OnCanRemoveItemDefault;
            mList.onReorderCallbackWithDetails = OnReorderItemDefault;
            mList.drawHeaderCallback = OnDrawHeaderDefaultOpened;
            mList.drawElementCallback = OnDrawItemDefault;
            mList.elementHeightCallback = OnHeightItemDefault;

            if (mProperty != null)
            {
                mReorderAttribute = mProperty.GetAttribute<LotusReorderableAttribute>();
                if (mReorderAttribute != null)
                {
                    // Изменение
                    if (mReorderAttribute.ItemsChangedMethodName.IsExists())
                    {
                    }
                    if (mReorderAttribute.ContextMenuMethodName.IsExists())
                    {
                        mList.onAddDropdownCallback = OnContextMenuDefault;
                    }

                    // Сортировка
                    if (mReorderAttribute.SortAscendingMethodName.IsExists())
                    {
                    }
                    if (mReorderAttribute.SortDescendingMethodName.IsExists())
                    {
                    }
                    if (mReorderAttribute.ReorderItemChangedMethodName.IsExists())
                    {
                    }

                    // Рисование
                    if (mReorderAttribute.DrawItemMethodName.IsExists())
                    {
                    }
                    if (mReorderAttribute.HeightItemMethodName.IsExists())
                    {
                    }
                }

                if (mProperty.propertyType == UnityEditor.SerializedPropertyType.Generic)
                {
                    if (XEditorCachedData.GetAttribute<LotusInLineAttribute>(mProperty) != null)
                    {
                        mIsDrawInline = true;
                    }
                }
            }
        }


        /// <summary>
        /// Получение высоты отображения списка
        /// </summary>
        /// <returns>Высота отображения списка</returns>

        public Single GetHeight()
        {
            if (IsExpanded)
            {
                return (mList.GetHeight());
            }
            else
            {
                return (XInspectorViewParams.CONTROL_HEIGHT_SPACE + XInspectorViewParams.SPACE);
            }
        }


        /// <summary>
        /// Рисование управляющих кнопок
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>

        private void DrawControlButton(Rect position)
        {
            var count = 2;
            if (mReorderAttribute.SortAscendingMethodName.IsExists()) count++;
            if (mReorderAttribute.SortDescendingMethodName.IsExists()) count++;
            var width_default = (position.width - 2) / count;

            //
            // Ascending/descending
            //
            Rect rect_button_sort_descending = Rect.zero;
            rect_button_sort_descending.x = position.xMax;
            if (mReorderAttribute.SortDescendingMethodName.IsExists())
            {
                rect_button_sort_descending.x = position.xMax - width_default;
                rect_button_sort_descending.y = position.y;
                rect_button_sort_descending.width = width_default;
                rect_button_sort_descending.height = mList.headerHeight - 2;
                if (GUI.Button(rect_button_sort_descending, "Sort " + XString.TriangleDown, UnityEditor.EditorStyles.miniButtonMid))
                {
                    mProperty.Invoke(mReorderAttribute.SortDescendingMethodName);
                }
            }

            Rect rect_button_sort_ascending = Rect.zero;
            rect_button_sort_ascending.y = position.y;
            rect_button_sort_ascending.width = width_default;
            rect_button_sort_ascending.height = mList.headerHeight - 2;
            if (mReorderAttribute.SortAscendingMethodName.IsExists())
            {
                rect_button_sort_ascending.x = rect_button_sort_descending.x - width_default;
                if (GUI.Button(rect_button_sort_ascending, "Sort " + XString.TriangleUp, UnityEditor.EditorStyles.miniButtonMid))
                {
                    mProperty.Invoke(mReorderAttribute.SortAscendingMethodName);
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
                mList.DoLayoutList();
            }
            else
            {
                XEditorInspector.PrepareContent(mProperty);

                Rect rect = UnityEditor.EditorGUILayout.BeginHorizontal();
                {
                    // Рисуем фон простым прямоугольником
                    GUI.Box(rect, "");

                    // Смещаем надпись свойства в область списка
                    GUILayout.Space(12);
                    var name = XEditorInspector.Content.text + " (" + CountItems.ToString() + ")";
                    mProperty.isExpanded = UnityEditor.EditorGUILayout.Foldout(mProperty.isExpanded, name, true);
                    IsExpanded = mProperty.isExpanded;
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
                mList.DoList(position);
            }
            else
            {
                XEditorInspector.PrepareContent(mProperty);

                // Рисуем фон простым прямоугольником
                GUI.Box(position, "");

                // Смещаем надпись свойства в область списка
                position.x += 12;
                var name = XEditorInspector.Content.text + " (" + CountItems.ToString() + ")";

                if (mOnContentHeader != null)
                {
                    name = mOnContentHeader(false);
                }

                mProperty.isExpanded = UnityEditor.EditorGUI.Foldout(position, mProperty.isExpanded, name, true);
                IsExpanded = mProperty.isExpanded;
            }
        }


        /// <summary>
        /// Рисование элемента по умолчанию
        /// </summary>
        /// <param name="item">Сериализируемое свойство</param>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="index">Индекс элемента</param>

        public void DrawItemDefault(UnityEditor.SerializedProperty item, Rect position, Int32 index)
        {
            // Для сложного свойства используем более подробное описание 
            if (item.propertyType == UnityEditor.SerializedPropertyType.Generic)
            {
                position.xMin += 8;
                position.y += 2;

                mContentItem.text = "Item " + index.ToString();

                if (mReorderAttribute != null && mReorderAttribute.TitleFieldName.IsExists())
                {
                    UnityEditor.SerializedProperty inner_property = item.FindPropertyRelative(mReorderAttribute.TitleFieldName);
                    if (inner_property != null && inner_property.propertyType == UnityEditor.SerializedPropertyType.String)
                    {
                        if (inner_property.stringValue.IsExists())
                        {
                            mContentItem.text = inner_property.stringValue + '[' + index.ToString() + ']';
                        }
                    }
                }
            }
            else
            {
                mContentItem.text = "Item " + index.ToString();
            }

            UnityEditor.EditorGUI.BeginChangeCheck();
            {
                UnityEditor.EditorGUI.PropertyField(position, item, mContentItem, item.isExpanded);
            }
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    //if (mCollection is ILotusNotifyCollectionChanged)
                    //{
                    //	item.Save();
                    //	ILotusNotifyCollectionChanged collection_changed = mCollection as ILotusNotifyCollectionChanged;
                    //	collection_changed.OnCollectionChanged?.Invoke(TNotifyCollectionChangedAction.Replace, mCollection[index], index, 1);
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

        public void DrawItemInLine(UnityEditor.SerializedProperty item, Rect position, Int32 index)
        {
            UnityEditor.EditorGUI.BeginChangeCheck();
            {
                List<UnityEditor.SerializedProperty> childs = item.GetChildrenProperties();

                // Получаем информацию о столбцах
                if (mColumnsAttribute == null)
                {
                    mColumnsAttribute = new List<LotusColumnAttribute>();
                    for (var i = 0; i < childs.Count; i++)
                    {
                        LotusColumnAttribute column_attribute = XEditorCachedData.GetAttribute<LotusColumnAttribute>(childs[i]);
                        mColumnsAttribute.Add(column_attribute);
                    }
                }

                // Считать будем от ширины с минусом по единицы на каждую колонку и минус ширина для вывода индекса
                Single index_width = 12;
                if (CountItems > 9)
                {
                    index_width = 16;
                }
                var total_width = position.width - childs.Count - index_width;

                // На первом элементе отображаем заголовки
                if (index == 0)
                {
                    Rect rect_header = position;
                    rect_header.x += index_width;
                    rect_header.height = XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    for (var i = 0; i < childs.Count; i++)
                    {
                        if (mColumnsAttribute[i] != null)
                        {
                            rect_header.width = Mathf.Ceil(mColumnsAttribute[i].Percent / 100 * total_width);

                            XEditorInspector.PrepareContent(childs[i]);

                            UnityEditor.EditorGUI.LabelField(rect_header, XEditorInspector.Content, UnityEditor.EditorStyles.boldLabel);

                            rect_header.x += rect_header.width + 1;
                        }
                    }

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                }

                // Выводим индекс
                Rect rect_index = position;

                // При нулевом индексе корректируем высоту
                if (index == 0)
                {
                    rect_index.y -= XInspectorViewParams.CONTROL_HEIGHT_SPACE / 2;
                }

                rect_index.width = index_width;
                UnityEditor.EditorGUI.LabelField(rect_index, index.ToString());
                position.x += index_width;

                for (var i = 0; i < childs.Count; i++)
                {
                    if (mColumnsAttribute[i] != null)
                    {
                        position.height = UnityEditor.EditorGUI.GetPropertyHeight(childs[i], mContentItem, childs[i].isExpanded);
                        position.width = Mathf.Ceil(mColumnsAttribute[i].Percent / 100 * total_width);

                        UnityEditor.EditorGUI.PropertyField(position, childs[i], GUIContent.none, childs[i].isExpanded);

                        position.x += position.width + 1;
                    }
                }
            }
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    //if (mCollection is ILotusNotifyCollectionChanged)
                    //{
                    //	item.Save();
                    //	ILotusNotifyCollectionChanged collection_changed = mCollection as ILotusNotifyCollectionChanged;
                    //	collection_changed.OnCollectionChanged?.Invoke(TNotifyCollectionChangedAction.Replace, mCollection[index], index, 1);
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
            mProperty = property;

            // Это обычная коллекция Array или List
            if (property.IsStandardCollection())
            {
                mList.serializedProperty = mProperty;
                mIsStandardCollection = true;

                if (property.serializedObject == null)
                {
                    Debug.Log("property.serializedObject == null");
                }
            }
            else
            {
                // Получаем коллекцию
                mItemsProperty = property.GetLotusInnerCollection();
                mCollection = property.GetValue<IList>();
                mTypeItem = mCollection.GetType().GetClassicCollectionItemType();
                mList.list = mCollection;
                mIsStandardCollection = false;
            }

            Init();
        }
        #endregion

        #region Event handling

        /// <summary>
        /// Добавление элемента в список
        /// </summary>
        /// <param name="list">Список</param>

        private void OnAddItemDefault(UnityEditorInternal.ReorderableList list)
        {
            if (mIsStandardCollection)
            {
                if (mOnAddItem != null)
                {
                    mOnAddItem();
                }
                else
                {
                    mProperty.arraySize++;
                }
            }
            else
            {
                if (mOnAddItem != null)
                {
                    mOnAddItem();
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
            if (mReorderAttribute != null && mReorderAttribute.ItemsChangedMethodName.IsExists())
            {
                mProperty.Invoke(mReorderAttribute.ItemsChangedMethodName);
            }
        }


        /// <summary>
        /// Удаление элемента из списка
        /// </summary>
        /// <param name="list">Список</param>

        private void OnRemoveItemDefault(UnityEditorInternal.ReorderableList list)
        {
            if (mIsStandardCollection)
            {
                if (mOnRemoveItem != null)
                {
                    mOnRemoveItem(list.index);
                }
                else
                {
                    mProperty.DeleteArrayElementAtIndex(list.index);
                }
            }
            else
            {
                if (mOnRemoveItem != null)
                {
                    mOnRemoveItem(list.index);
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

        private Boolean OnCanRemoveItemDefault(UnityEditorInternal.ReorderableList list)
        {
            if (mOnCanRemoveItem != null)
            {
                return (mOnCanRemoveItem(list.index));
            }
            else
            {
                return (list.count > 0);
            }
        }


        /// <summary>
        /// Изменение позиции элемента из списка
        /// </summary>
        /// <param name="list">Список</param>
        /// <param name="old_index">Старый индекс</param>
        /// <param name="new_index">Новый индекс</param>

        private void OnReorderItemDefault(UnityEditorInternal.ReorderableList list, Int32 old_index, Int32 new_index)
        {
            if (mOnReorderItem != null)
            {
                mOnReorderItem(old_index, new_index);
            }

            if (mReorderAttribute != null && mReorderAttribute.ReorderItemChangedMethodName.IsExists())
            {
                mProperty.Invoke(mReorderAttribute.ReorderItemChangedMethodName, old_index, new_index);
            }

            if (UnityEditor.EditorApplication.isPlaying)
            {
                //if (mCollection is ILotusNotifyCollectionChanged)
                //{
                //	ILotusNotifyCollectionChanged collection_changed = mCollection as ILotusNotifyCollectionChanged;
                //	collection_changed.OnCollectionChanged?.Invoke(TNotifyCollectionChangedAction.Move, null, old_index, new_index);
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
            var collection = (IList)mProperty.Invoke(mReorderAttribute.ContextMenuMethodName);
            if (collection != null)
            {
                mSerializedObject = mProperty.serializedObject;
                mPropertyPath = mProperty.propertyPath;
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
        /// <param name="menu_item">Элемент контекстного меню</param>

        private void OnContextMenuItemSelect(System.Object menu_item)
        {
            if (mIsStandardCollection)
            {
                try
                {
                    mSerializedObject.Update();
                    UnityEditor.SerializedProperty property = mSerializedObject.FindProperty(mPropertyPath);
                    if (property != null)
                    {
                        var index = property.arraySize;
                        property.arraySize++;
                        mSerializedObject.ApplyModifiedProperties();

                        mList.index = index;
                        var item = property.GetArrayElementAtIndex(index);
                        if (item != null)
                        {
                            item.SetValue(menu_item);
                        }
                    }

                    mSerializedObject.ApplyModifiedProperties();
                }
                catch (Exception exc)
                {
                    Debug.LogError(exc.Message);
                }
            }
            else
            {
                Collection.Add(XReflection.CreateInstance(TypeItem));
                mCollection[mCollection.Count - 1] = menu_item;
            }

            if (mReorderAttribute != null && mReorderAttribute.ItemsChangedMethodName.IsExists())
            {
                mProperty.Invoke(mReorderAttribute.ItemsChangedMethodName);
            }
        }


        /// <summary>
        /// Рисование заголовка списка (когда он раскрыт)
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>

        private void OnDrawHeaderDefaultOpened(Rect position)
        {
            if (mOnDrawHeader != null)
            {
                mOnDrawHeader(position);
            }
            else
            {
                XEditorInspector.PrepareContent(mProperty);
                position.x += 12;
                Vector2 size = UnityEditor.EditorStyles.label.CalcSize(XEditorInspector.Content);
                Rect rect_label = position;
                rect_label.width = size.x + 2;

                if (mOnContentHeader != null)
                {
                    XEditorInspector.Content.text = mOnContentHeader(true);
                }

                mProperty.isExpanded = UnityEditor.EditorGUI.Foldout(rect_label, mProperty.isExpanded, XEditorInspector.Content, false);
                IsExpanded = mProperty.isExpanded;
            }

            if (mReorderAttribute != null)
            {
                if (mReorderAttribute.SortAscendingMethodName.IsExists() ||
                    mReorderAttribute.SortDescendingMethodName.IsExists())
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

        private Single OnHeightItemDefault(Int32 index)
        {
            if (mOnHeightItem != null)
            {
                return (mOnHeightItem(index));
            }
            else
            {
                if (mReorderAttribute != null && mReorderAttribute.HeightItemMethodName.IsExists())
                {
                    return ((Single)mProperty.Invoke(mReorderAttribute.HeightItemMethodName, index));
                }
                else
                {
                    UnityEditor.SerializedProperty item = null;
                    if (mIsStandardCollection)
                    {
                        item = mProperty.GetArrayElementAtIndex(index);
                    }
                    else
                    {
                        if (mItemsProperty == null) mItemsProperty = mProperty.GetLotusInnerCollection();
                        if (mItemsProperty != null)
                        {
                            if (index > mItemsProperty.arraySize - 1) index = mItemsProperty.arraySize - 1;
                            item = mItemsProperty.GetArrayElementAtIndex(index);

                        }
                    }

                    if (item != null)
                    {
                        if (mIsDrawInline)
                        {
                            var max_height = XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            List<UnityEditor.SerializedProperty> childs = item.GetChildrenProperties();

                            for (var i = 0; i < childs.Count; i++)
                            {
                                var height = UnityEditor.EditorGUI.GetPropertyHeight(childs[i]);
                                if (height > max_height)
                                {
                                    max_height = height + XInspectorViewParams.SPACE;
                                }
                            }

                            if (index == 0)
                            {
                                max_height += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            }

                            return (max_height);
                        }
                        else
                        {
                            var default_height = XInspectorViewParams.CONTROL_HEIGHT;
                            var compute_height = UnityEditor.EditorGUI.GetPropertyHeight(item, GUIContent.none, true);

                            return Mathf.Max(default_height, compute_height) + XInspectorViewParams.SPACE;
                        }
                    }
                    else
                    {
                        return (XInspectorViewParams.CONTROL_HEIGHT_SPACE);
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

        private void OnDrawItemDefault(Rect position, Int32 index, Boolean active, Boolean focused)
        {
            if (mReorderAttribute != null && mReorderAttribute.DrawItemMethodName.IsExists())
            {
                mProperty.Invoke(mReorderAttribute.DrawItemMethodName, position, index);
            }
            else
            {
                UnityEditor.SerializedProperty item = null;
                if (mIsStandardCollection)
                {
                    item = mProperty.GetArrayElementAtIndex(index);
                }
                else
                {
                    if (mItemsProperty == null) mItemsProperty = mProperty.GetLotusInnerCollection();
                    if (mItemsProperty != null)
                    {
                        if (index > mItemsProperty.arraySize - 1) index = mItemsProperty.arraySize - 1;
                        item = mItemsProperty.GetArrayElementAtIndex(index);
                    }
                }

                // Уменьшаем высоту области отображения до стандартного и вследствие это образуется отступы между элементами списка
                position.height -= UnityEditor.EditorGUIUtility.standardVerticalSpacing;

                if (item != null)
                {
                    if (mIsDrawInline)
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
        public static Dictionary<String, CReorderableList> mReorderableLists = new Dictionary<String, CReorderableList>();
        #endregion

        #region Properties
        /// <summary>
        /// Словарь списков
        /// </summary>
        public static Dictionary<String, CReorderableList> ReorderableLists
        {
            get
            {
                if (mReorderableLists == null)
                {
                    mReorderableLists = new Dictionary<String, CReorderableList>();
                }

                return (mReorderableLists);
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
            CReorderableList result = null;
            if (ReorderableLists.TryGetValue(key, out result))
            {
                result.Reset(property);
                return (result);
            }

            result = new CReorderableList(property);
            ReorderableLists.Add(key, result);
            return (result);
        }
        #endregion
    }

    /**@}*/
}
#endif