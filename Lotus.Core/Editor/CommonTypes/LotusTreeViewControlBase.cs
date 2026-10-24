#if UNITY_EDITOR
using System;

using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Базовый шаблон элемента для отображения дерева для редактора Unity
    /// </summary>
    public class TreeViewControlBase<TCollection, TViewItem, TData>
        where TData : class
        where TViewItem : class, ILotusViewModelHierarchy
        where TCollection : CollectionViewModelHierarchy<TViewItem, TData>
    {
        #region Fields
        // Основные параметры
        protected internal TCollection _root;
        protected internal object _rootData;
        protected internal TViewItem _selectedNode;

        // Параметры фильтрации
        protected internal string _searchString;
        protected internal TStringSearchOption _searchOption;

        // Параметры отображения
        protected internal bool _isCheckableView;
        protected internal bool _isIconableView;
        protected internal Func<TViewItem, Texture> _iconDelegate;

        // События
        protected internal Action<TViewItem> _onSelectedNode;

        // Служебные данные для отрисовки
        protected internal Rect _controlRect;
        protected internal float _startDrawY;
        protected internal float _height;
        protected internal int _controlID;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Корневой узел дерева
        /// </summary>
        public TCollection Root
        {
            get { return _root; }
            set
            {
                _root = value;
                if (_root != null)
                {
                    _rootData = _root.Source;
                }
            }
        }

        /// <summary>
        /// Текущий выбранный узел отображения
        /// </summary>
        public TViewItem SelectedNode
        {
            get { return _selectedNode; }
        }

        //
        // ПАРАМЕТРЫ ФИЛЬТРАЦИИ
        //
        /// <summary>
        /// Строка для фильтрования и поиска узлов по имени
        /// </summary>
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (_searchString != value)
                {
                    _searchString = value;
                    OnFiltered();
                }
            }
        }

        /// <summary>
        /// Опции поиска узлов по имени
        /// </summary>
        public TStringSearchOption SearchOption
        {
            get { return _searchOption; }
            set
            {
                if (_searchOption != value)
                {
                    _searchOption = value;
                    OnFiltered();
                }
            }
        }

        //
        // ПАРАМЕТРЫ ОТОБРАЖЕНИЯ
        //
        /// <summary>
        /// Отображение флажка выбора элемента узла
        /// </summary>
        public bool IsCheckableView
        {
            get { return _isCheckableView; }
            set
            {
                if (_isCheckableView != value)
                {
                    _isCheckableView = value;
                }
            }
        }

        /// <summary>
        /// Отображение иконки элемента узла
        /// </summary>
        public bool IsIconableView
        {
            get { return _isIconableView; }
            set
            {
                if (_isIconableView != value)
                {
                    _isIconableView = value;
                }
            }
        }

        /// <summary>
        /// Делегат для представления иконки в зависимости от типа узла отображения
        /// </summary>
        public Func<TViewItem, Texture> IconDelegate
        {
            get { return _iconDelegate; }
            set
            {
                if (_iconDelegate != value)
                {
                    _iconDelegate = value;
                }
            }
        }

        //
        // СОБЫТИЯ
        //
        /// <summary>
        /// Событие выбор элемента узла
        /// </summary>
        public Action<TViewItem> OnSelectedNode
        {
            get { return _onSelectedNode; }
            set
            {
                if (_onSelectedNode != value)
                {
                    _onSelectedNode = value;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="root">Корневой узел</param>
        public TreeViewControlBase(TCollection root)
        {
            Root = root;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="root">Корневой узел</param>
        /// <param name="data">Данные корневого узла</param>
        public TreeViewControlBase(TCollection root, TData data)
        {
            Root = root;
            Root.Source = data;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Рисование дерева в автоматическом макете
        /// </summary>
        public virtual void DrawTreeLayout()
        {
            if (_root != null)
            {
                _height = 0;
                _startDrawY = 0;
                _root.Visit(match: OnGetLayoutHeight);

                _controlRect = EditorGUILayout.GetControlRect(false, _height);
                _controlID = GUIUtility.GetControlID(FocusType.Passive, _controlRect);
                _root.Visit(match: OnDrawRow);
            }
        }

        /// <summary>
        /// Фильтрация узлов
        /// </summary>
        protected virtual void OnFiltered()
        {
        }

        /// <summary>
        /// Удаление текущего выбранного узла
        /// </summary>
        public virtual void Delete()
        {
            var deleted = _selectedNode;
            _selectedNode = default;

            deleted.IParent.DetachOwnedObject(deleted, true);
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Получение высоты узла дерева
        /// </summary>
        /// <param name="nodeView">Узел дерева</param>
        /// <returns>Высота</returns>
        protected virtual float GetRowHeight(ILotusViewModelHierarchy nodeView)
        {
            return EditorGUIUtility.singleLineHeight + 2;
        }

        /// <summary>
        /// Вычисление высоты дерева
        /// </summary>
        /// <param name="nodeView">Узел дерева</param>
        /// <returns>Статус продолжения вычисления высоты дерева</returns>
        protected virtual bool OnGetLayoutHeight(ILotusViewModelHierarchy nodeView)
        {
            if (nodeView is TViewItem nodeObject)
            {
                if (nodeObject.Model == null)
                {
                    return true;
                }
            }

            _height += GetRowHeight(nodeView);
            return nodeView.IsExpanded;
        }

        /// <summary>
        /// Рисование узла дерева
        /// </summary>
        /// <param name="nodeView">Узел дерева</param>
        /// <returns>Статус раскрытия данного узла</returns>
        protected virtual bool OnDrawRow(ILotusViewModelHierarchy nodeView)
        {
            if (nodeView is TViewItem nodeObject)
            {
                if (nodeObject.Model == null)
                {
                    return true;
                }
            }

            var rowIndent = 16 * nodeView.Level;
            var rowHeight = GetRowHeight(nodeView);

            var rowRect = new Rect(2, _controlRect.y + _startDrawY, _controlRect.width, rowHeight);
            var indentRect = new Rect(rowIndent, _controlRect.y + _startDrawY, _controlRect.width - rowIndent, rowHeight);

            // render
            if (_selectedNode == (nodeView as TViewItem))
            {
                GUI.Box(rowRect, GUIContent.none, XEditorStyles.BOX_FLOW_NODE_0_ON);
            }

            OnDrawTreeNode(indentRect, nodeView as TViewItem, _selectedNode == nodeView, false);

            // test for events
            var eventType = Event.current.GetTypeForControl(_controlID);
            if (eventType == EventType.MouseUp && rowRect.Contains(Event.current.mousePosition))
            {
                _selectedNode = nodeView as TViewItem;
                _root.SelectedViewModel = _selectedNode;

                _onSelectedNode?.Invoke(_selectedNode);

                GUI.changed = true;
                Event.current.Use();
            }

            _startDrawY += rowHeight;
            return nodeView.IsExpanded;
        }

        /// <summary>
        /// Рисование данных узла дерева
        /// </summary>
        /// <param name="rect">Прямоугольник вывода</param>
        /// <param name="nodeView">Узел дерева</param>
        /// <param name="selected">Статус выбора узла</param>
        /// <param name="focus">Статус фокуса узла</param>
        protected virtual void OnDrawTreeNode(Rect rect, TViewItem nodeView, bool selected, bool focus)
        {
            var labelContent = new GUIContent(nodeView.Model.ToString());

            if (_isIconableView && _iconDelegate != null)
            {
                labelContent.image = _iconDelegate(nodeView);
            }
            else
            {
                // Специальные случае
                if (nodeView.Model is CFileSystemDirectory)
                {
                    labelContent.image = EditorGUIUtility.IconContent(XEditorStyles.ICON_FOLDER).image;
                }
                else if (nodeView.Model is CFileSystemFile systemFile)
                {
                    labelContent.image = XEditorInspector.GetIconForFile(systemFile.Info.Extension);
                }
            }

            if (!nodeView.IsLeaf)
            {
                var rectFoldout = new Rect(rect.x, rect.y, 12, rect.height);
                nodeView.IsExpanded = EditorGUI.Foldout(rectFoldout, nodeView.IsExpanded, GUIContent.none);
            }

            if (_isCheckableView)
            {
                var rectChecked = new Rect(rect.x + 12, rect.y + 1, 12, rect.height);
                if (nodeView.IsChecked.HasValue)
                {
                    nodeView.IsChecked = EditorGUI.ToggleLeft(rectChecked, GUIContent.none, nodeView.IsChecked.Value);
                }
                else
                {
                    EditorGUI.showMixedValue = true;
                    EditorGUI.ToggleLeft(rectChecked, GUIContent.none, false);
                    EditorGUI.showMixedValue = false;
                }

                var rectValue = new Rect(rect.x + 26, rect.y, rect.width - 24, rect.height);
                EditorGUI.LabelField(rectValue, labelContent, selected ? EditorStyles.whiteBoldLabel : EditorStyles.label);
            }
            else
            {
                var rectValue = new Rect(rect.x + 14, rect.y, rect.width - 14, rect.height);
                EditorGUI.LabelField(rectValue, labelContent, selected ? EditorStyles.whiteBoldLabel : EditorStyles.label);
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
