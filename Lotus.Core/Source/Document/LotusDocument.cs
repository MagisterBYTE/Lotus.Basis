﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема документов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDocument.cs
*		Интерфейс концепции документа.
*		Под документом понимается объект который связан с отдельным физическим файлом для сохранения/загрузки своих
*	данных, позволяет себя отобразить, экспортировать в доступны форматы, а также отправить на печать.
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
		//! \defgroup CoreDocument Подсистема документов
		//! Подсистема документов реализует концепцию документы - объекта, который связан с отдельным физическим файлом
		//! для сохранения/загрузки своих данных, позволяет себя отобразить, экспортировать в доступны форматы,
		//! а также отправить на печать.
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс концепции документа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusDocument
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя физического файла
			/// </summary>
			String FileName { get; set; }

			/// <summary>
			/// Путь до файла
			/// </summary>
			String PathFile { get; set; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение расширения файла без точки
			/// </summary>
			/// <returns>Расширение файла без точки</returns>
			//---------------------------------------------------------------------------------------------------------
			String GetFileExtension();
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================