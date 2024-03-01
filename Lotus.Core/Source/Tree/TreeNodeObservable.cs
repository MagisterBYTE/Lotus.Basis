using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Lotus.Core
{
    /// <summary>
    /// Реализация базового объекта для расположения в дереве.
    /// </summary>
    public class TreeNodeObservable : ITreeNode, INotifyPropertyChanged
    {
        #region Fields
        protected internal string _text = string.Empty;
        protected internal bool _isSelected;
        protected internal bool _isExpanded;
        protected internal bool? _isChecked = false;
        protected internal int _order = -1;
        protected internal readonly ObservableCollection<TreeNodeObservable> _childNodes;
        protected internal readonly Dictionary<string, object> _attributes;
        #endregion

        #region Properties
        /// <summary>
        /// Ключ сущности
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текст заголовка
        /// </summary>
        public string TextNode
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статус выбора узла
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статус выбора узла
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                UpdateParentsCheckedStatus();
                UpdateChildsCheckedStatus();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статус раскрытия узла
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Порядок следования узла(индекс его расположения)
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Родительский узел
        /// </summary>
        public ITreeNode? IParentTreeNode { get; set; }

        /// <summary>
        /// Список дочерних узлов
        /// </summary>
        public IEnumerable<ITreeNode>? IChildNodes
        {
            get { return _childNodes; }
        }

        /// <summary>
        /// Список дочерних узлов
        /// </summary>
        public ObservableCollection<TreeNodeObservable>? ChildNodes
        {
            get { return _childNodes; }
        }

        /// <summary>
        /// Набор произвольной информации связанной с данным узлом
        /// </summary>
        public Dictionary<string, object> Attributes { get { return _attributes; } }
        #endregion

        public TreeNodeObservable()
        {
            _childNodes = new ObservableCollection<TreeNodeObservable>();
            _childNodes.CollectionChanged += Items_CollectionChanged;
            _attributes = new Dictionary<string, object>();
        }

        #region TreeNodes methods 
        private void Items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            var parent = this;

            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (ITreeNode item in args.NewItems!)
                        item.IParentTreeNode = parent;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (ITreeNode item in args.OldItems!)
                        item.IParentTreeNode = null;
                    break;
            }
        }

        /// <summary>
        /// Проверка узла на удовлетворение указанного предиката.
        /// </summary>
        /// <remarks>
        /// Объект удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката.
        /// </remarks>
        /// <param name="match">Предикат проверки.</param>
        /// <returns>Статус проверки.</returns>
        public virtual bool CheckOne(Predicate<ITreeNode?> match)
        {
            if (match(this))
            {
                return true;
            }
            else
            {
                for (var i = 0; i < _childNodes.Count; i++)
                {
                    if (_childNodes[i].CheckOne(match))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Установка статуса выбора флажком и вызов события.
        /// </summary>
        /// <param name="isChecked">Статус выбора флажком.</param>
        public virtual void SetCheckedStatus(bool? isChecked)
        {
            _isChecked = isChecked;
            OnPropertyChanged(nameof(IsChecked));
        }

        /// <summary>
        /// Рекурсивно обновляет <see cref="ITreeNode.IsChecked"/> всех родителей текущего узла. 
        /// Устанавливает <see cref="ITreeNode.IsChecked"/> в true, если все вложенные узлы отмечены. <br></br>
        /// Null - если отмечен хотя бы один узел. <br></br>
        /// False - если не отмечен ни один узел.<br></br>
        /// Рекурсивно проходит вверх по дереву.
        /// </summary>
        public virtual void UpdateParentsCheckedStatus()
        {
            var parent = this.IParentTreeNode;
            if (parent == null || parent == this || parent.IChildNodes == null)
            {
                return;
            }

            if (parent.IChildNodes.Any(c => c.IsChecked != false))
            {
                if (parent.IChildNodes.All(c => c.IsChecked == true))
                {
                    if (parent is TreeNodeObservable parentVm)
                    {
                        parentVm.SetCheckedStatus(true);
                    }
                }
                else
                {
                    if (parent is TreeNodeObservable parentVm1)
                    {
                        parentVm1.SetCheckedStatus(null);
                    }
                }
            }
            else
            {
                if (parent is TreeNodeObservable parentVm2)
                {
                    parentVm2.SetCheckedStatus(false);
                }
            }

            if (parent is TreeNodeObservable parentVm3)
            {
                parentVm3.UpdateParentsCheckedStatus();
            }
        }

        /// <summary>
        /// Рекурсивное обновление дочерних узлов и их потомок по статус выбора флажком.
        /// </summary>
        public virtual void UpdateChildsCheckedStatus()
        {
            if (IChildNodes == null || _isChecked == null)
            {
                return;
            }

            foreach (var child in IChildNodes)
            {
                if (child is TreeNodeObservable nodeVm)
                {
                    nodeVm.SetCheckedStatus(_isChecked);
                    nodeVm.UpdateChildsCheckedStatus();
                }
            }
        }
        #endregion

        #region Attributes methods 
        /// <summary>
        /// Установить атрибут целочисленного типа.
        /// </summary>
        /// <param name="key">Ключ атрибута.</param>
        /// <param name="value">Значение атрибута.</param>
        public void SetCustomAttributeInt(string key, int value)
        {
            _attributes[key] = value;
        }

        /// <summary>
        /// Прочитать значение атрибута целочисленного типа.
        /// </summary>
        /// <param name="key">Ключ атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию если указанный атрибут не найден.</param>
        /// <returns>Значение атрибута или значение по умолчанию если указанный атрибут не найден.</returns>
        public int GetCustomAttributeInt(string key, int defaultValue = -1)
        {
            if (_attributes.TryGetValue(key, out var value))
            {
                return Convert.ToInt32(value);
            }

            return defaultValue;
        }
        #endregion

        #region Interface INotifyPropertyChanged
        /// <summary>
        /// Событие срабатывает ПОСЛЕ изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        public void OnPropertyChanged([CallerMemberName] String? propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="args">Аргументы события.</param>
        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }
        #endregion
    }
}
