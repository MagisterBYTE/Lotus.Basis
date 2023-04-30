//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема отображения данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewItem.cs
*		Определение интерфейса элемента отображения.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreViewItem
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейса элемента отображения
		/// </summary>
		/// <remarks>
		/// Элемент отображения представляет собой концепцию промежуточного элемента, который хранит ссылку на данные
		/// и основные параметры просмотра и управления
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusViewItem : ILotusNameable, ILotusOwnedObject
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Данные
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом отображения
			/// </remarks>
			System.Object DataContext { get; set; }

			/// <summary>
			/// Выбор элемента
			/// </summary>
			/// <remarks>
			/// Подразумевает выбор элемента пользователем для просмотра свойств.
			/// По умолчанию может быть активировано только для одного элемента списка
			/// </remarks>
			Boolean IsSelected { get; set; }

			/// <summary>
			/// Доступность элемента
			/// </summary>
			/// <remarks>
			/// Подразумевается некая логическая доступность элемента.
			/// Активировано может быть для нескольких элементов списка
			/// </remarks>
			Boolean IsEnabled { get; set; }

			/// <summary>
			/// Выбор элемента
			/// </summary>
			/// <remarks>
			/// Подразумевает выбор элемента пользователем для каких-либо действий.
			/// Активировано может быть для нескольких элементов списка
			/// </remarks>
			Boolean? IsChecked { get; set; }

			/// <summary>
			/// Отображение элемента
			/// </summary>
			/// <remarks>
			/// Подразумевает отображение элемента в отдельном контексте
			/// По умолчанию может быть активировано только для одного элемента списка
			/// </remarks>
			Boolean IsPresented { get; set; }

			/// <summary>
			/// Статус элемента находящегося в режиме редактирования
			/// </summary>
			/// <remarks>
			/// Реализация зависит от конкретной платформы
			/// </remarks>
			Boolean IsEditMode { get; set; }

			/// <summary>
			/// Элемент пользовательского интерфейса который непосредственно представляет данный элемент отображения
			/// </summary>
			System.Object UIElement { get; set; }

			/// <summary>
			/// Контекстное меню
			/// </summary>
			CUIContextMenu UIContextMenu { get; }

			/// <summary>
			/// Возможность перемещать элемент отображения в элементе пользовательского интерфейса
			/// </summary>
			Boolean UIDraggableStatus { get; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			ILotusViewItem CreateViewItem();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Открытие контекстного меню
			/// </summary>
			/// <param name="context_menu">Контекстное меню</param>
			//---------------------------------------------------------------------------------------------------------
			void OpenContextMenu(System.Object context_menu);
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейса элемента отображения с конкретным типом данных
		/// </summary>
		/// <typeparam name="TData">Тип данных</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusViewItemData<TData> : ILotusViewItem
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Данные
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом отображения
			/// </remarks>
			new TData DataContext { get; set; }
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Шаблон определяющий минимальный элемент отображения, который реализует основные параметры просмотра и управления
		/// </summary>
		/// <typeparam name="TData">Тип данных</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class ViewItem<TData> : CNameable, ILotusViewItemData<TData>
			where TData : class
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusNameable"/>
			/// </summary>
			public static readonly Boolean IsSupportNameable = typeof(TData).IsSupportInterface<ILotusNameable>();

			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusIdentifierId"/>
			/// </summary>
			public static readonly Boolean IsSupportIdentifierId = typeof(TData).IsSupportInterface<ILotusIdentifierId>();

			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusViewSelected"/>
			/// </summary>
			public static readonly Boolean IsSupportViewSelected = typeof(TData).IsSupportInterface<ILotusViewSelected>();

			/// <summary>
			/// Статус реализации типом интерейса <see cref="ILotusViewEnabled"/>
			/// </summary>
			public static readonly Boolean IsSupportViewEnabled = typeof(TData).IsSupportInterface<ILotusViewEnabled>();
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			protected static readonly PropertyChangedEventArgs PropertyArgsDataContext = new PropertyChangedEventArgs(nameof(DataContext));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsSelected = new PropertyChangedEventArgs(nameof(IsSelected));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsEnabled = new PropertyChangedEventArgs(nameof(IsEnabled));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsChecked = new PropertyChangedEventArgs(nameof(IsChecked));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsPresented = new PropertyChangedEventArgs(nameof(IsPresented));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsEditMode = new PropertyChangedEventArgs(nameof(IsEditMode));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal ILotusOwnerObject mOwner;
			protected internal TData mDataContext;
			protected internal Boolean mIsEnabled;
			protected internal Boolean mIsSelected;
			protected internal Boolean? mIsChecked;
			protected internal Boolean mIsPresented;
			protected internal Boolean mIsEditMode;

			// Элементы интерфейса
			protected internal System.Object mUIElement;
			protected internal CUIContextMenu mUIContextMenu;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Владелец объекта
			/// </summary>
			public ILotusOwnerObject IOwner
			{
				get { return (mOwner); }
				set { mOwner = value; }
			}

			/// <summary>
			/// Данные
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом отображения
			/// </remarks>
			Object ILotusViewItem.DataContext
			{
				get { return (mDataContext); }
				set
				{
					if(mDataContext != null && mDataContext != value)
					{
						UnsetDataContext();
					}

					mDataContext = (TData)value;
					NotifyPropertyChanged(PropertyArgsDataContext);
					SetDataContext();
					RaiseDataContextChanged();
				}
			}

			/// <summary>
			/// Данные
			/// </summary>
			/// <remarks>
			/// Ссылка на данные которые связаны с данным элементом отображения
			/// </remarks>
			public TData DataContext 
			{
				get { return (mDataContext); }
				set
				{
					if (mDataContext != null && mDataContext != value)
					{
						UnsetDataContext();
					}

					mDataContext = value;
					NotifyPropertyChanged(PropertyArgsDataContext);
					SetDataContext();
					RaiseDataContextChanged();
				}
			}

			/// <summary>
			/// Выбор элемента отображения
			/// </summary>
			public Boolean IsSelected
			{
				get { return (mIsSelected); }
				set
				{
					if (mIsSelected != value)
					{
						if (mDataContext is ILotusViewSelected view_selected)
						{
							if (view_selected.CanViewSelected(this))
							{
								mIsSelected = value;
								view_selected.SetViewSelected(this, value);
								NotifyPropertyChanged(PropertyArgsIsSelected);
								RaiseIsSelectedChanged();
							}
						}
						else
						{
							mIsSelected = value;
							NotifyPropertyChanged(PropertyArgsIsSelected);
							RaiseIsSelectedChanged();
						}
					}
				}
			}

			/// <summary>
			/// Доступность элемента отображения
			/// </summary>
			public Boolean IsEnabled
			{
				get { return (mIsEnabled); }
				set
				{
					if (mIsEnabled != value)
					{
						if (mDataContext is ILotusViewEnabled view_enabled)
						{
							mIsEnabled = value;
							view_enabled.SetViewEnabled(this, value);
							NotifyPropertyChanged(PropertyArgsIsEnabled);
							RaiseIsEnabledChanged();
						}
						else
						{
							mIsEnabled = value;
							NotifyPropertyChanged(PropertyArgsIsEnabled);
							RaiseIsEnabledChanged();
						}
					}
				}
			}

			/// <summary>
			/// Выбор элемента элемента отображения
			/// </summary>
			public Boolean? IsChecked
			{
				get { return (mIsChecked); }
				set
				{
					if (mIsChecked != value)
					{
						mIsChecked = value;
						NotifyPropertyChanged(PropertyArgsIsChecked);
						RaiseIsCheckedChanged();
					}
				}
			}

			/// <summary>
			/// Отображение элемента отображения в отдельном контексте
			/// </summary>
			public Boolean IsPresented
			{
				get { return (mIsPresented); }
				set
				{
					if (mIsPresented != value)
					{
						if (mDataContext is ILotusViewPresented view_presented)
						{
							mIsPresented = value;
							view_presented.SetViewPresented(this, value);
							NotifyPropertyChanged(PropertyArgsIsPresented);
							RaiseIsPresentedChanged();
						}
						else
						{
							mIsPresented = value;
							NotifyPropertyChanged(PropertyArgsIsPresented);
							RaiseIsPresentedChanged();
						}
					}
				}
			}

			/// <summary>
			/// Статус элемента находящегося в режиме редактирования
			/// </summary>
			public Boolean IsEditMode
			{
				get { return (mIsEditMode); }
				set
				{
					if (mIsEditMode != value)
					{
						mIsEditMode = value;
						NotifyPropertyChanged(PropertyArgsIsEditMode);
					}
				}
			}

			/// <summary>
			/// Элемент пользовательского интерфейса который непосредственно представляет данный элемент отображения
			/// </summary>
			public System.Object UIElement 
			{
				get { return (mUIElement); } 
				set 
				{ 
					mUIElement = value;
				}
			}

			/// <summary>
			/// Контекстное меню
			/// </summary>
			public CUIContextMenu UIContextMenu
			{
				get { return (mUIContextMenu); }
			}

			/// <summary>
			/// Возможность перемещать элемент отображения в элементе пользовательского интерфейса
			/// </summary>
			public virtual Boolean UIDraggableStatus
			{
				get { return (false); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ViewItem()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя элемента представления</param>
			//---------------------------------------------------------------------------------------------------------
			public ViewItem(String name)
				: base(name)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data_context">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public ViewItem(TData data_context)
			{
				mDataContext = data_context;
				SetDataContext();
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя элемента отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (mName);
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение данных элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseDataContextChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, DataContext, nameof(DataContext));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение выбора элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsSelectedChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, IsSelected, nameof(IsSelected));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение доступности элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsEnabledChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, IsEnabled, nameof(IsEnabled));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение выбора элемента отображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsCheckedChanged()
			{
				if (mOwner != null)
				{
					mOwner.OnNotifyUpdated(this, IsChecked, nameof(IsChecked));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса отображения элемента.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsPresentedChanged()
			{
				if (IOwner != null)
				{
					IOwner.OnNotifyUpdated(this, IsPresented, nameof(IsPresented));
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewItem =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ILotusViewItem CreateViewItem()
			{
				return (new ViewItem<TData>());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Открытие контекстного меню
			/// </summary>
			/// <param name="context_menu">Контекстное меню</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OpenContextMenu(System.Object context_menu)
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetDataContext()
			{
				if (mDataContext is ILotusNameable nameable)
				{
					mName = nameable.Name;
				}
				else
				{
					if (mDataContext != null)
					{
						mName = mDataContext.ToString();
					}
				}

				if (mDataContext is ILotusViewItemOwner view_item_owner)
				{
					view_item_owner.OwnerViewItem = this;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnsetDataContext()
			{
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс определяющий минимальный элемент отображения, с общими данными который реализует основные
		/// параметры просмотра и управления
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CViewItem : ViewItem<System.Object>, IComparable<CViewItem>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CViewItem()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя элемента представления</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItem(String name)
				: base(name)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data_context">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItem(System.Object data_context)
				: base(data_context)
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CViewItem other)
			{
				return (mName.CompareTo(other.Name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object Clone()
			{
				var clone = new CViewItem();
				clone.Name = mName;
				return (clone);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (mName);
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewItem =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание экземпляра элемента отображения
			/// </summary>
			/// <returns>Элемент отображения</returns>
			//---------------------------------------------------------------------------------------------------------
			public override ILotusViewItem CreateViewItem()
			{
				return (new CViewItem());
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================