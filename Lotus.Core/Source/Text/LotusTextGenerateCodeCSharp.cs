﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема текстовых данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTextGenerateCodeCSharp.cs
*		Простой генератор программного кода на языке C# на основе текстовых данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreText
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Простой генератор программного кода на языке C# на основе текстовых данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CTextGenerateCodeCSharp : CTextGenerateCodeBase
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Разделитель для части
			/// </summary>
			public static readonly CTextLine DelimetrPart = new CTextLine("//", '=', 120);

			/// <summary>
			/// Разделитель для секции
			/// </summary>
			public static readonly CTextLine DelimetrSection = new CTextLine("//", '-', 120);
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextGenerateCodeCSharp(Int32 capacity = 24)
				: base(capacity)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="str">Строка</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextGenerateCodeCSharp(String str)
				: base(str)
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление разделителя для части
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void AddDelimetrPart()
			{
				CTextLine delimetr_part = DelimetrPart.Duplicate();
				delimetr_part.Index = _lines.Count;
				delimetr_part.Owned = this;
				delimetr_part.Indent = _currentIndent;
				_lines.Add(delimetr_part);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление разделителя для секции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void AddDelimetrSection()
			{
				CTextLine delimetr_section = DelimetrSection.Duplicate();
				delimetr_section.Index = _lines.Count;
				delimetr_section.Owned = this;
				delimetr_section.Indent = _currentIndent;
				_lines.Add(delimetr_section);
			}
			#endregion

			#region ======================================= РАБОТА С ПРОСТРАНСТВАМИ ИМЕН  =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление используемых пространств имён
			/// </summary>
			/// <param name="namespaces">Список пространства имён</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddNamespaceUsing(params String[] namespaces)
			{
				if (namespaces != null)
				{
					for (var i = 0; i < namespaces.Length; i++)
					{
						Add("using " + namespaces[i] + ";");
					}
				}
				AddDelimetrPart();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление открытие пространства имени
			/// </summary>
			/// <param name="spaceName">Имя пространства имён</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddNamespaceOpen(String spaceName)
			{
				Add("namespace " + spaceName);
				Add("{");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление закрытия пространства имени
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void AddNamespaceClose()
			{
				Add("}");
			}
			#endregion

			#region ======================================= ОПИСАНИЯ ФАЙЛА ============================================
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ ТИПОВ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации статического публичного класса
			/// </summary>
			/// <param name="className">Имя класса</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddClassStaticPublic(String className)
			{
				Add("public static class " + className);
				Add("{");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление окончание декларации класса
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void AddClassEndDeclaration()
			{
				Add("}");
			}
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ ПОЛЕЙ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации константного публичного поля
			/// </summary>
			/// <param name="typeName">Имя типа</param>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="value">Значения поля</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddFieldConstPublic(String typeName, String fieldName, String value)
			{
				Add("public const " + typeName + " " + fieldName + " = " + value + ";");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации константного публичного поля типа String
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="value">Значения поля</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddFieldConstPublicString(String fieldName, String value)
			{
				Add("public const String " + fieldName + " = \"" + value + "\";");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации статического поля только для чтения
			/// </summary>
			/// <param name="typeName">Имя типа</param>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="value">Значения поля</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddFieldStaticReadonlyPublic(String typeName, String fieldName, String value)
			{
				Add("public static readonly " + typeName + " " + fieldName + " = " + value + ";");
			}
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ СВОЙСТВ ========================================
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ МЕТОДОВ ========================================
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ КОММЕНТАРИЕВ ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление комментария
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddComment(String text)
			{
				if (text.IsExists())
				{
					Add("// " + text);
				}
				else
				{
					Add("//");
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление комментария для секции или раздела
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddCommentSection(String text)
			{
				Add("//");
				if (text.IsExists())
				{
					Add("// " + text);
				}
				else
				{
					Add("//");
				}
				Add("//");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление стандартного краткого комментария XML
			/// </summary>
			/// <param name="delimetrSectionBefore">Статус добавления разделителя секции перед комментарием</param>
			/// <param name="text">Текст комментария</param>
			/// <param name="delimetrSectionAfter">Статус добавления разделителя секции после комментария </param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddCommentSummary(Boolean delimetrSectionBefore, String text, Boolean delimetrSectionAfter)
			{
				if (delimetrSectionBefore)
				{
					AddDelimetrSection();
				}
				Add("/// <summary>");
				Add("/// " + text);
				Add("/// </summary>");
				if (delimetrSectionAfter)
				{
					AddDelimetrSection();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление стандартного краткого комментария XML для полей и свойств
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddCommentSummaryForData(String text)
			{
				Add("/// <summary>");
				Add("/// " + text);
				Add("/// </summary>");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление стандартного расширенного комментария XML для полей и свойств
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AddCommentRemarksForData(String text)
			{
				Add("/// <remarks>");
				Add("/// " + text);
				Add("/// </remarks>");
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================