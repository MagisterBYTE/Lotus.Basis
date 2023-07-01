//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема сообщений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMessagePublisher.cs
*		Определение издателя сообщения.
*		Реализация издателя сообщения, через которого происходит вся работа по отправки и обработки сообщений.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreMessage
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		///  Определение интерфейса издателя сообщений
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusPublisher : ILotusNameable
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регистрация подписки на обработку сообщений
			/// </summary>
			/// <param name="messageHandler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			void RegisterMessageHandler(ILotusMessageHandler messageHandler);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отмена регистрации подписки на обработку сообщений
			/// </summary>
			/// <param name="messageHandler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			void UnRegisterMessageHandler(ILotusMessageHandler messageHandler);
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Издателя сообщения
		/// </summary>
		/// <remarks>
		/// Реализация издателя сообщений который хранит все обработчики сообщений и обеспечивает
		/// посылку сообщений.
		/// Методы издателя нужно вызывать вручную в соответствующих местах
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CPublisher : ILotusPublisher
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструирование объекта базового класса для определения аргумента сообщения
			/// </summary>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMessageArgs ConstructorMessageArgs()
			{
				return new CMessageArgs(true);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal String mName;
			protected internal PoolManager<CMessageArgs> mMessageArgsPools;
			protected internal ListArray<ILotusMessageHandler> mMessageHandlers;
			protected internal QueueArray<CMessageArgs> mQueueMessages;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя издателя
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Пул объектов типа оболочки для хранения аргументов сообщения
			/// </summary>
			public PoolManager<CMessageArgs> MessageArgsPools
			{
				get { return mMessageArgsPools; }
			}

			/// <summary>
			/// Список обработчиков сообщений
			/// </summary>
			public ListArray<ILotusMessageHandler> MessageHandlers
			{
				get { return mMessageHandlers; }
			}

			/// <summary>
			/// Очередь сообщений
			/// </summary>
			public QueueArray<CMessageArgs> QueueMessages
			{
				get { return mQueueMessages; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CPublisher()
				: this("")
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя издателя</param>
			//---------------------------------------------------------------------------------------------------------
			public CPublisher(String name)
			{
				mName = name;
				mMessageArgsPools = new PoolManager<CMessageArgs>(100, ConstructorMessageArgs);
				mMessageHandlers = new ListArray<ILotusMessageHandler>(10);
				mQueueMessages = new QueueArray<CMessageArgs>(100);
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusPublisher ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регистрация подписки на обработку сообщений
			/// </summary>
			/// <param name="messageHandler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RegisterMessageHandler(ILotusMessageHandler messageHandler)
			{
				if(mMessageHandlers.Contains(messageHandler) == false)
				{
					mMessageHandlers.Add(in messageHandler);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отмена регистрации подписки на обработку сообщений
			/// </summary>
			/// <param name="messageHandler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnRegisterMessageHandler(ILotusMessageHandler messageHandler)
			{
				mMessageHandlers.Remove(in messageHandler);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка сообщений
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnUpdate()
			{
				// Если у нас есть обработчики и сообщения
				if (mMessageHandlers.Count > 0 && mQueueMessages.Count > 0)
				{
					// Перебираем все сообщения
					while (mQueueMessages.Count != 0)
					{
						// Выталкиваем сообщения
						CMessageArgs message = mQueueMessages.Dequeue();

						for (var i = 0; i < mMessageHandlers.Count; i++)
						{
							var code = mMessageHandlers[i].OnMessageHandler(message);

							// Сообщение почему-то обработано с отрицательным результатом 
							if (code == XMessageHandlerResultCode.NEGATIVERESULT)
							{
#if UNITY_2017_1_OR_NEWER
								UnityEngine.Debug.LogWarning(message.ToString());
#else
								XLogger.LogWarning(message.ToString());
#endif
							}
						}

						// Если объект был из пула
						if (message.IsPoolObject)
						{
							mMessageArgsPools.Release(message);
						}
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ОТПРАВКИ СООБЩЕНИЙ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения
			/// </summary>
			/// <param name="message">Аргументы сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(CMessageArgs message)
			{
				mQueueMessages.Enqueue(message);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения
			/// </summary>
			/// <param name="name">Имя сообщения</param>
			/// <param name="data">Данные сообщения</param>
			/// <param name="sender">Источник сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(String name, System.Object data, System.Object sender)
			{
				CMessageArgs message = mMessageArgsPools.Take();
				message.Name = name;
				message.Data = data;
				message.Sender = sender;
				mQueueMessages.Enqueue(message);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения
			/// </summary>
			/// <param name="id">Уникальный идентификатор сообщения</param>
			/// <param name="data">Данные сообщения</param>
			/// <param name="sender">Источник сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(Int32 id, System.Object data, System.Object sender)
			{
				CMessageArgs message = mMessageArgsPools.Take();
				message.Id = id;
				message.Data = data;
				message.Sender = sender;
				mQueueMessages.Enqueue(message);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================