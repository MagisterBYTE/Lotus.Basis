﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема сообщений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMessageCommon.cs
*		Общие типы и интерфейсы подсистемы сообщений.
*		Общие типы и структуры данных подсистемы сообщений.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreMessage Подсистема сообщений
		//! Централизованная подсистема сообщений. Подсистема сообщений предназначена для уведомления всех подписчиков
		//! о произошедшем событии посредством соответствующих издателей
		//!
		//! ## Возможности/особенности
		//! 1. Возможность отправить сообщение любым подписчика
		//! 2. Легкость реализации обработчика сообщений
		//! 3. Минимальные накладные расходы, высокая скорость работы
		//! 4. Низкая связность и зависимость подсистемы от других подсистем
		//! 5. Полностью интегрирована в систему Lotus в качестве основной
		//!
		//! ## Описание
		//! Подсистема сообщений очень простая и основана на паттерне подписчик/издатель. Отправка сообщений происходит
		//! через издателя, и также возможно через статический метод центрального диспетчера сообщений
		//! (который создает при необходимости издателя по умолчанию) \ref Lotus.Core.CPublisher.
		//! Для обработки сообщения нужно реализовать соответствующий интерфейс \ref Lotus.Core.ILotusMessageHandler. 
		//! Компонент с реализаций интерфейса нужно зарегистрировать в либо диспетчере, либо в соответствующем издателе.
		//! Сообщения поступают в очередь и последовательно обрабатываются. Сообщения можно послать подписчикам издателя
		//! или через диспетчер всем подписчикам. Сообщения также можно послать из консоли.
		//! Многие подсистемы реализует интерфейс сообщений для централизованного и унифицированного управления.
		//!
		//! ## Использование
		//! 1. Реализовать интерфейс \ref Lotus.Core.ILotusMessageHandler
		//! 2. Зарегистрировать компонент в конкретном издателе \ref Lotus.Core.CPublisher
		//! 3. Теперь можно посылать команды через издателя \ref Lotus.Core.CPublisher
		//! 4. Методы диспетчер или издателя нужно использовать в ручную(непосредственно вызывать методы в нужных местах)
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс для определения аргумента сообщения
		/// </summary>
		/// <remarks>
		/// Фактические данный класс реализует полноценное хранение данных о произошедшем событии, источники событии.
		/// Чтобы исключить накладные расходы связанные с фрагментацией памяти малыми объектами, объекты классы управляются
		/// через пул.
		/// Таким образом фактически происходит циркуляция сообщений в системе без существенных накладных затрат
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CMessageArgs : ILotusPoolObject, ILotusIdentifierId<Int32>
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Int32 mId;
			protected internal String mName;
			protected internal System.Object mData;
			protected internal System.Object mSender;
			protected internal Boolean mIsPoolObject;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Уникальный идентификатор сообщения
			/// </summary>
			public Int32 Id
			{
				get { return mId; }
				set { mId = value; }
			}

			/// <summary>
			/// Наименование сообщения
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Данные сообщения
			/// </summary>
			public System.Object Data
			{
				get { return mData; }
				set { mData = value; }
			}

			/// <summary>
			/// Источник сообщения
			/// </summary>
			public System.Object Sender
			{
				get { return mSender; }
				set { mSender = value; }
			}

#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Источник сообщения - как пользовательский скрипт
			/// </summary>
			public UnityEngine.MonoBehaviour SenderBehaviour
			{
				get { return mSender as UnityEngine.MonoBehaviour; }
				set { mSender = value; }
			}
#endif

			/// <summary>
			/// Статус объекта из пула
			/// </summary>
			public Boolean IsPoolObject
			{
				get { return mIsPoolObject; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сообщения</param>
			/// <param name="is_pool">Статус размещения объекта в пуле</param>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs(String name, Boolean is_pool = false)
			{
				mName = name;
				mIsPoolObject = is_pool;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Уникальный идентификатор сообщения</param>
			/// <param name="is_pool">Статус размещения объекта в пуле</param>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs(Int32 id, Boolean is_pool = false)
			{
				mId = id;
				mIsPoolObject = is_pool;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сообщения</param>
			/// <param name="id">Уникальный идентификатор сообщения</param>
			/// <param name="is_pool">Статус размещения объекта в пуле</param>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs(String name, Int32 id, Boolean is_pool = false)
			{
				mName = name;
				mId = id;
				mIsPoolObject = is_pool;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сообщения</param>
			/// <param name="sender">Компонент - источник события</param>
			/// <param name="is_pool">Статус размещения объекта в пуле</param>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs(String name, System.Object sender, Boolean is_pool = false)
			{
				mName = name;
				mSender = sender;
				mIsPoolObject = is_pool;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Уникальный идентификатор сообщения</param>
			/// <param name="sender">Источник события</param>
			/// <param name="is_pool">Статус размещения объекта в пуле</param>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs(Int32 id, System.Object sender, Boolean is_pool = false)
			{
				mId = id;
				mSender = sender;
				mIsPoolObject = is_pool;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сообщения</param>
			/// <param name="id">Уникальный идентификатор сообщения</param>
			/// <param name="sender">Компонент - источник события</param>
			/// <param name="is_pool">Статус размещения объекта в пуле</param>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs(String name, Int32 id, System.Object sender, Boolean is_pool = false)
			{
				mName = name;
				mSender = sender;
				mIsPoolObject = is_pool;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="is_pool">Статус размещения объекта в пуле</param>
			//---------------------------------------------------------------------------------------------------------
			public CMessageArgs(Boolean is_pool)
			{
				mIsPoolObject = is_pool;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление сообщения</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return("Name <" + mName + "> Value[" + mData?.ToString() + "]");
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusPoolObject ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Псевдо-конструктор
			/// </summary>
			/// <remarks>
			/// Вызывается диспетчером пула в момент взятия объекта из пула
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public void OnPoolTake()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Псевдо-деструктор
			/// </summary>
			/// <remarks>
			/// Вызывается диспетчером пула в момент попадания объекта в пул
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public void OnPoolRelease()
			{
				mData = null;
				mSender = null;
				mName = "";
				mId = 0;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================