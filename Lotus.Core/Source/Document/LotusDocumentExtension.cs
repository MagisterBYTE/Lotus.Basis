//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема документов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDocumentExtension.cs
*		 Статический класс для реализации функциональности интерфейса документа.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreDocument
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для реализации функциональности интерфейса <see cref="ILotusDocument"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDocumentExtension
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка документа из файла
			/// </summary>
			/// <typeparam name="TDocument">Тип документа</typeparam>
			/// <param name="title">Заголовок диалога</param>
			/// <param name="serializer">Сериализатор</param>
			/// <returns>Документ</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TDocument LoadDocument<TDocument>(String title = null, ILotusSerializer serializer = null) where TDocument : ILotusDocument
			{
				String file_name = XFileDialog.Open(title != null ? title : "Открыть документ", "", null);
				if (file_name.IsExists())
				{
					TDocument document = default;

					// Если есть сериализатор то используем его
					if (serializer != null)
					{
						document = serializer.LoadFrom<TDocument>(file_name);
					}
					else
					{
						// Загружаем файл
						document = XSerializationDispatcher.LoadFrom<TDocument>(file_name);
					}
					if (document != null)
					{
						// Получаем путь и имя файла
						document.PathFile = Path.GetDirectoryName(file_name);
						document.FileName = Path.GetFileName(file_name);

						// Если документ поддерживает коллекцию то обновляем связи
						if (document is ILotusOwnerObject owner_object)
						{
							// Обновляем связи
							owner_object.UpdateOwnedObjects();
						}

						// Корректируем имя
						if (document is ILotusNameable nameable)
						{
							if (nameable.Name.IsExists() == false)
							{
								nameable.Name = Path.GetFileNameWithoutExtension(file_name);
							}
						}
					}

					return (document);
				}

				return (default);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка документа из файла
			/// </summary>
			/// <typeparam name="TDocument">Тип документа</typeparam>
			/// <param name="document">Документ</param>
			/// <param name="title">Заголовок диалога</param>
			/// <param name="serializer">Сериализатор</param>
			/// <returns>Статус</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean LoadDocument<TDocument>(this TDocument document, String title = null, ILotusSerializer serializer = null) where TDocument : ILotusDocument
			{
				if(document != null)
				{
					String file_name = XFileDialog.Open(title != null ? title : "Открыть документ", "", document.GetFileExtension());
					if (file_name.IsExists())
					{
						// Получаем путь и имя файла
						document.PathFile = Path.GetDirectoryName(file_name);
						document.FileName = Path.GetFileName(file_name);

						// Если документ поддерживает коллекцию то очищаем её
						if (document is ILotusOwnerObject owner_object)
						{
							if (owner_object is System.Collections.IList list)
							{
								list.Clear();
							}

							// Загружаем данные
							if (serializer != null)
							{
								serializer.UpdateFrom(document, file_name);
							}
							else
							{
								XSerializationDispatcher.UpdateFrom(document, file_name);
							}

							// Обновляем связи
							owner_object.UpdateOwnedObjects();
						}
						else
						{
							if (serializer != null)
							{
								serializer.UpdateFrom(document, file_name);
							}
							else
							{
								XSerializationDispatcher.UpdateFrom(document, file_name);
							}
						}

						// Корректируем имя
						if (document is ILotusNameable nameable)
						{
							if(nameable.Name.IsExists() == false)
							{
								nameable.Name = Path.GetFileNameWithoutExtension(file_name);
							}
						}

						return (true);
					}
				}
				else
				{
					return (false);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Восстановление данных документа к состоянию последнего сохранения
			/// </summary>
			/// <param name="document">Документ</param>
			/// <param name="serializer">Сериализатор</param>
			/// <returns>Статус восстановления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean RestoreDocument<TDocument>(this TDocument document, ILotusSerializer serializer = null) where TDocument : ILotusDocument
			{
				if (document != null)
				{
					String file_name = Path.Combine(document.PathFile, document.FileName);
					if (File.Exists(file_name))
					{
						// Если документ поддерживает коллекцию то очищаем её
						if (document is ILotusOwnerObject owner_object)
						{
							if (owner_object is System.Collections.IList list)
							{
								list.Clear();
							}

							// Загружаем данные
							if (serializer != null)
							{
								serializer.UpdateFrom(document, file_name);
							}
							else
							{
								XSerializationDispatcher.UpdateFrom(document, file_name);
							}

							// Обновляем связи
							owner_object.UpdateOwnedObjects();
						}
						else
						{
							// Загружаем данные
							if (serializer != null)
							{
								serializer.UpdateFrom(document, file_name);
							}
							else
							{
								XSerializationDispatcher.UpdateFrom(document, file_name);
							}
						}

						return (true);
					}
				}
				else
				{
					return (false);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранение изменения документа в связанный физический файл
			/// </summary>
			/// <param name="document">Документ</param>
			/// <param name="title">Заголовок диалога</param>
			/// <param name="serializer">Сериализатор</param>
			/// <returns>Статус сохранения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SaveDocument<TDocument>(this TDocument document, String title = null, ILotusSerializer serializer = null) where TDocument : ILotusDocument
			{
				if (document != null)
				{
					// Если не существует путь или имя файла
					if (document.PathFile.IsExists() == false || document.FileName.IsExists() == false)
					{
						String doc_name = "Документ";

						// Получаем имя
						if (document is ILotusNameable nameable)
						{
							if (nameable.Name.IsExists())
							{
								doc_name = nameable.Name;
							}
						}

						String file_name = XFileDialog.Save(title != null ? title : "Сохранить документ", document.PathFile, doc_name, document.GetFileExtension());
						if (file_name.IsExists())
						{
							// Сохраняем документ
							if (serializer != null)
							{
								serializer.SaveTo(file_name, document);
							}
							else
							{
								XSerializationDispatcher.SaveTo(file_name, document);
							}
							document.PathFile = Path.GetDirectoryName(file_name);
							document.FileName = Path.GetFileName(file_name);

							if (document is ILotusNameable doc_nameable)
							{
								if (doc_nameable.Name.IsExists() == false)
								{
									doc_nameable.Name = Path.GetFileNameWithoutExtension(file_name);
								}
							}

							return (true);
						}
					}
					else
					{
						// Проверяем путь
						String file_name = Path.Combine(document.PathFile, document.FileName);
						if (File.Exists(file_name))
						{
							// Сохраняем документ
							if (serializer != null)
							{
								serializer.SaveTo(file_name, document);
							}
							else
							{
								XSerializationDispatcher.SaveTo(file_name, document);
							}

							return (true);
						}
					}
				}
				else
				{
					return (false);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранение изменения документа в физический файл
			/// </summary>
			/// <param name="document">Документ</param>
			/// <param name="title">Заголовок диалога</param>
			/// <param name="serializer">Сериализатор</param>
			/// <returns>Статус сохранения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SaveAsDocument<TDocument>(this TDocument document, String title = null, ILotusSerializer serializer = null) where TDocument : ILotusDocument
			{
				if (document != null)
				{
					String doc_name = "Документ";

					// Получаем имя
					if (document is ILotusNameable nameable)
					{
						if (nameable.Name.IsExists())
						{
							doc_name = nameable.Name;
						}
					}

					String file_name = XFileDialog.Save(title != null ? title : "Сохранить документ как", document.PathFile, doc_name, document.GetFileExtension());
					if (file_name.IsExists())
					{
						// Сохраняем документ
						if(serializer != null)
						{
							serializer.SaveTo(file_name, document);
						}
						else
						{
							XSerializationDispatcher.SaveTo(file_name, document);
						}
						document.PathFile = Path.GetDirectoryName(file_name);
						document.FileName = Path.GetFileName(file_name);

						if (document is ILotusNameable doc_nameable)
						{
							if (doc_nameable.Name.IsExists() == false)
							{
								doc_nameable.Name = Path.GetFileNameWithoutExtension(file_name);
							}
						}

						return (true);
					}
				}
				else
				{
					return (false);
				}

				return (false);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================