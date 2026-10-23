
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusInspectorMethodHolder.cs
*		Определение структур данных для отображения атрибута вызова метода через инспектор свойств.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
------------
using Lotus.Core;

namespace Lotus
{
	namespace Editor
	{

		/// <summary>
		/// Данные для вызова метода без аргумента
		/// </summary>

		public class CMethodHolder : IComparable<CMethodHolder>
		{
			#region Fields
			/// <summary>
			/// Атрибут вызова метода
			/// </summary>
			public LotusMethodCallAttribute MethodAttribute;

			/// <summary>
			/// Вызываемый метод
			/// </summary>
			public MethodInfo Method;

			/// <summary>
			/// Экземпляр объекта
			/// </summary>
			public System.Object Instance;
			#endregion

			#region Constructors
			
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			
			public CMethodHolder()
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения</returns>
			
			public Int32 CompareTo(CMethodHolder other)
			{
				return (MethodAttribute.DrawOrder.CompareTo(other.MethodAttribute.DrawOrder));
			}
			#endregion

			#region Main methods
			
			/// <summary>
			/// Получение сигнатуры метода
			/// </summary>
			/// <returns>Сигнатура метода</returns>
			
			protected virtual String GetSignatureMethod()
			{
				return (" ()");
			}

			
			/// <summary>
			/// Вызов метода
			/// </summary>
			
			protected virtual void InvokeMethod()
			{
				if (Method.IsStatic)
				{
					Method.Invoke(null, null);
				}
				else
				{
					Method.Invoke(Instance, null);
				}
			}

			
			/// <summary>
			/// Рисование элементов для установки аргументов
			/// </summary>
			
			protected virtual void DrawArguments()
			{

			}

			
			/// <summary>
			/// Получение имени метода для отображения
			/// </summary>
			/// <returns>Имя метода</returns>
			
			protected String GetMethodDisplay()
			{
				if (MethodAttribute.DisplayName.IsExists())
				{
					if (MethodAttribute.IsSignature)
					{
						return (MethodAttribute.DisplayName + GetSignatureMethod());
					}
					else
					{
						return (MethodAttribute.DisplayName);
					}
				}
				else
				{
					if (MethodAttribute.IsSignature)
					{
						return (Method.Name + GetSignatureMethod());
					}
					else
					{
						return (Method.Name);
					}
				}
			}

			
			/// <summary>
			/// Рисование элементов управления для вызова метода
			/// </summary>
			
			public virtual void Draw()
			{
				XEditorInspector.DrawDecorateAttributes(Method);

				if (EditorApplication.isPlaying)
				{
					GUI.enabled = !(MethodAttribute.Mode == TMethodCallMode.OnlyEditor);
				}
				else
				{
					GUI.enabled = !(MethodAttribute.Mode == TMethodCallMode.OnlyPlay);
				}

				Rect rect_button = EditorGUILayout.BeginHorizontal(GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
				{
					EditorGUILayout.PrefixLabel("");

					if (Event.current.type != EventType.Layout)
					{
						if (GUI.Button(rect_button, GetMethodDisplay()))
						{
							if (Instance != null)
							{
								InvokeMethod();
							}
							
						}
					}
				}
				EditorGUILayout.EndHorizontal();

				GUI.enabled = true;
			}
			#endregion
		}


		/// <summary>
		/// Данные для вызова метода с одним аргументов
		/// </summary>

		public class CMethodHolderArg1 : CMethodHolder
		{
			#region Fields
			/// <summary>
			/// Имя первого аргумента
			/// </summary>
			public String FirstArgumentName;

			/// <summary>
			/// Имя типа первого аргумента
			/// </summary>
			public String FirstArgumentTypeName;

			/// <summary>
			/// Первый аргумент
			/// </summary>
			public CVariant FirstArgument;
			#endregion

			#region Constructors
			
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			
			public CMethodHolderArg1()
			{
				FirstArgument = new CVariant();
			}
			#endregion

			#region Main methods
			
			/// <summary>
			/// Получение сигнатуры метода
			/// </summary>
			/// <returns>Сигнатура метода</returns>
			
			protected override String GetSignatureMethod()
			{
				return (" (" + FirstArgumentTypeName + " " + FirstArgumentName + ")");
			}

			
			/// <summary>
			/// Вызов метода
			/// </summary>
			
			protected override void InvokeMethod()
			{
				CReflectedType.ArgList1[0] = FirstArgument.Get();

				if (Method.IsStatic)
				{
					Method.Invoke(null, CReflectedType.ArgList1);
				}
				else
				{
					Method.Invoke(Instance, CReflectedType.ArgList1);
				}
			}

			
			/// <summary>
			/// Рисование элементов для установки аргументов
			/// </summary>
			
			protected override void DrawArguments()
			{
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUI.indentLevel++;
					LotusVariantDrawer.DrawLayoutVariant(FirstArgumentName, FirstArgument);
					EditorGUI.indentLevel--;

					//if(MethodAttribute is LotusMethodArgFileAttribute)
					//{
					//	LotusMethodArgFileAttribute arg_file = MethodAttribute as LotusMethodArgFileAttribute;

					//	if (GUILayout.Button("...", EditorStyles.miniButtonRight, 
					//		GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
					//	{
					//		String file_name = "";
					//		if(arg_file.IsOpenFile)
					//		{
					//			file_name = XFileDialog.Open("Открыть", arg_file.DefaultPath, arg_file.Extension);
					//		}
					//		else
					//		{
					//			file_name = XFileDialog.Save("Сохранить", arg_file.DefaultPath, arg_file.DefaultName, arg_file.Extension);
					//		}

					//		if (file_name.IsExists())
					//		{
					//			FirstArgument.StringValue = file_name.RemoveToWith(XEditorSettings.ASSETS_PATH);
					//		}
					//	}
					//}
				}
				EditorGUILayout.EndHorizontal();
			}

			
			/// <summary>
			/// Рисование элементов управления для вызова метода
			/// </summary>
			
			public override void Draw()
			{
				XEditorInspector.DrawDecorateAttributes(Method);

				if (EditorApplication.isPlaying)
				{
					GUI.enabled = !(MethodAttribute.Mode == TMethodCallMode.OnlyEditor);
				}
				else
				{
					GUI.enabled = !(MethodAttribute.Mode == TMethodCallMode.OnlyPlay);
				}

				Rect rect_button = EditorGUILayout.BeginHorizontal(GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
				{
					EditorGUILayout.PrefixLabel("");

					if (Event.current.type != EventType.Layout)
					{
						if (GUI.Button(rect_button, GetMethodDisplay()))
						{
							if (Instance != null)
							{
								InvokeMethod();
							}

						}
					}
				}
				EditorGUILayout.EndHorizontal();

				Rect rect_box = EditorGUILayout.BeginVertical();
				{
					if (Event.current.type != EventType.Layout)
					{
						rect_box.x += XInspectorViewParams.OFFSET_INDENT;
						rect_box.width -= XInspectorViewParams.OFFSET_INDENT;
						rect_box.height += 2;
						if (rect_box.height > XInspectorViewParams.CONTROL_HEIGHT)
						{
							GUI.Box(rect_box, "", XEditorStyles.BOX_SHURIKEN);
						}
					}
					DrawArguments();
				}
				EditorGUILayout.EndVertical();

				GUI.enabled = true;
			}
			#endregion
		}


		/// <summary>
		/// Данные для вызова метода с двумя аргументами
		/// </summary>

		public class CMethodHolderArg2 : CMethodHolderArg1
		{
			#region Fields
			/// <summary>
			/// Имя второго аргумента
			/// </summary>
			public String SecondArgumentName;

			/// <summary>
			/// Имя типа второго аргумента
			/// </summary>
			public String SecondArgumentTypeName;

			/// <summary>
			/// Второй аргумент
			/// </summary>
			public CVariant SecondArgument;
			#endregion

			#region Constructors
			
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			
			public CMethodHolderArg2()
			{
				FirstArgument = new CVariant();
				SecondArgument = new CVariant();
			}
			#endregion

			#region Main methods
			
			/// <summary>
			/// Получение сигнатуры метода
			/// </summary>
			/// <returns>Сигнатура метода</returns>
			
			protected override String GetSignatureMethod()
			{
				var signature = " (" + FirstArgumentTypeName + " " + FirstArgumentName + ", " +
					SecondArgumentTypeName + " " + SecondArgumentName + ")";

				return (signature);
			}

			
			/// <summary>
			/// Вызов метода
			/// </summary>
			
			protected override void InvokeMethod()
			{
				CReflectedType.ArgList2[0] = FirstArgument.Get();
				CReflectedType.ArgList2[1] = SecondArgument.Get();

				if (Method.IsStatic)
				{
					Method.Invoke(null, CReflectedType.ArgList2);
				}
				else
				{
					Method.Invoke(Instance, CReflectedType.ArgList2);
				}
			}

			
			/// <summary>
			/// Рисование элементов для установки аргументов
			/// </summary>
			
			protected override void DrawArguments()
			{
				base.DrawArguments();

				EditorGUILayout.BeginHorizontal();
				{
					EditorGUI.indentLevel++;
					LotusVariantDrawer.DrawLayoutVariant(SecondArgumentName, SecondArgument);
					EditorGUI.indentLevel--;
				}
				EditorGUILayout.EndHorizontal();
			}
			#endregion
		}
	}
}
