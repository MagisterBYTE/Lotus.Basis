
// Проект: Модуль базового ядра Unity
// Раздел: Window утилиты Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusGenerateCodeWindow.cs
*		Редактор для функционирования и управления простым кодогенератором.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор для функционирования и управления простым кодогенератором
/// </summary>
------------
public class LotusGenerateCodeWindow : LotusUtilityBaseWindow
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Показать окно для функционирования и управления простым кодогенератором
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathUtility + "Generate Code", false, XCoreSettingsEditor.MenuOrderUtility + 20)]
	public static void ShowGenerateCode()
	{
		var window = GetWindow<LotusGenerateCodeWindow>();
		window.titleContent.text = "Generate Code";

		window.OnConstruct();
		window.Show();
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	// Основные данные
	#endregion

	#region =============================================== ОБШИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Конструирование окна
	/// </summary>
	--------
	public void OnConstruct()
	{
		mIsManager = false;
		mTabs = new String[2];
		mTabs[0] = "Autogen: Tags, Layers, Axis";
		mTabs[1] = "Autogen: Serialization";
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	--------
	/// <summary>
	/// Рисование панели обозревателя
	/// </summary>
	--------
	public override void DrawExplore()
	{
		switch (mSelectedTab)
		{
			case 0:
				{
					DrawAutogenTags();
					DrawAutogenLayers();
					DrawAutogenAxis();
				}
				break;
			default:
				break;
		}
	}

	--------
	/// <summary>
	/// Рисование основной панели
	/// </summary>
	--------
	public override void DrawContent()
	{
		GUILayout.Box("RectContent", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
	}
	#endregion

	#region =============================================== РАБОТА С ТЭГАМИ ===========================================
	--------
	/// <summary>
	/// Автогенерация кода для константных данных тэгов
	/// </summary>
	--------
	public void DrawAutogenTags()
	{
		GUILayout.Space(4.0f);
		EditorGUILayout.LabelField("Генерация констант Tag", EditorStyles.largeLabel,
			GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

		GUILayout.Space(4.0f);
		EditorGUILayout.LabelField("Файл: LotusBaseUnityTags", EditorStyles.boldLabel);

		GUILayout.Space(2.0f);
		if (GUILayout.Button("Generate"))
		{
			var generateCode = new CTextGenerateCodeCSharp();
			generateCode.AddFileHeader("Модуль общей функциональности", "Базовая подсистема Unity");
			generateCode.AddFileBriefDesc("LotusBaseUnityTags.cs", "Общие данные для организации работы с тэгами объектов Unity");
			generateCode.AddFileVersion();
			generateCode.AddNamespaceUsing("System", "UnityEngine");
			generateCode.AddNamespaceOpen("Lotus");
			{
				generateCode.CurrentIndent++;
				generateCode.AddNamespaceOpen("Common");
				{
					generateCode.CurrentIndent++;
					generateCode.AddDoxygenAddToGroup("UnityCommonBase");
					generateCode.AddCommentSummary(true, "Статический класс содержащий константы используемых тэгов объектов Unity", true);
					generateCode.AddClassStaticPublic("XTag");
					{
						generateCode.CurrentIndent++;

						var tags = UnityEditorInternal.InternalEditorUtility.tags;
						for (var i = 0; i < tags.Length; i++)
						{
							if (tags[i] == "Untagged")
							{
								generateCode.AddCommentSummaryForData("Тэг по умолчанию");
								generateCode.AddFieldConstPublicString(tags[i].ToConstCase(), tags[i]);
								generateCode.AddEmptyLine();
								continue;
							}

							if (tags[i] == "EditorOnly")
							{
								generateCode.AddCommentSummaryForData("Тэг только для режима редактора");
								generateCode.AddFieldConstPublicString(tags[i].ToConstCase(), tags[i]);
								generateCode.AddEmptyLine();
								continue;
							}

							generateCode.AddCommentSummaryForData("Тэг - " + tags[i]);
							generateCode.AddFieldConstPublicString(tags[i].ToConstCase(), tags[i]);

							if (i != tags.Length - 1)
							{
								generateCode.AddEmptyLine();
							}
						}

						generateCode.CurrentIndent--;
					}
					generateCode.AddClassEndDeclaration();
					generateCode.AddDoxygenEndGroup();

					generateCode.CurrentIndent--;
				}
				generateCode.AddNamespaceClose();

				generateCode.CurrentIndent--;
			}
			generateCode.AddNamespaceClose();
			generateCode.AddDelimetrPart();

			generateCode.SetLengthWithTabsOnlyDelimetrs(120);
			generateCode.Save(@"Lotus\Source\Common\Base\LotusBaseUnityTags.cs");
		}
	}
	#endregion

	#region =============================================== РАБОТА СО СЛОЯМИ ==========================================
	--------
	/// <summary>
	/// Автогенерация кода для константных данных слоев
	/// </summary>
	--------
	public void DrawAutogenLayers()
	{
		GUILayout.Space(4.0f);
		EditorGUILayout.LabelField("Генерация констант Layers", EditorStyles.largeLabel,
			GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

		GUILayout.Space(4.0f);
		EditorGUILayout.LabelField("Файл: LotusBaseUnityLayers", EditorStyles.boldLabel);

		GUILayout.Space(2.0f);
		if (GUILayout.Button("Generate"))
		{
			var generateCode = new CTextGenerateCodeCSharp();
			generateCode.AddFileHeader("Модуль общей функциональности", "Базовая подсистема Unity");
			generateCode.AddFileBriefDesc("LotusBaseUnityLayers.cs", "Общие данные для организации работы с различными слоями в Unity");
			generateCode.AddFileVersion();
			generateCode.AddNamespaceUsing("System", "UnityEngine");
			generateCode.AddNamespaceOpen("Lotus");
			{
				generateCode.CurrentIndent++;
				generateCode.AddNamespaceOpen("Common");
				{
					generateCode.CurrentIndent++;
					generateCode.AddDoxygenAddToGroup("UnityCommonBase");
					generateCode.AddCommentSummary(true, "Статический класс содержащий константы используемых слоев расположения объектов Unity", true);
					generateCode.AddClassStaticPublic("XLayer");
					{
						generateCode.CurrentIndent++;

						generateCode.AddCommentSection("ИМЕНА СЛОЕВ");

						var layers = UnityEditorInternal.InternalEditorUtility.layers;
						for (var i = 0; i < layers.Length; i++)
						{
							generateCode.AddCommentSummaryForData(GetCommentLayer(layers[i]));
							generateCode.AddFieldConstPublicString(layers[i].ToConstCase(), layers[i]);
							generateCode.AddEmptyLine();
						}

						generateCode.AddCommentSection("ЧИСЛОВЫЕ КОДЫ СЛОЕВ");

						for (var i = 0; i < layers.Length; i++)
						{
							generateCode.AddCommentSummaryForData(GetCommentLayer(layers[i]));
							generateCode.AddFieldStaticReadonlyPublic("Int32", layers[i].ToVarName() + "_ID",
								"LayerMask.NameToLayer(" + layers[i].ToConstCase() + ")");
							generateCode.AddEmptyLine();
						}

						generateCode.AddCommentSection("МАСКИ СЛОЕВ");

						for (var i = 0; i < layers.Length; i++)
						{
							generateCode.AddCommentSummaryForData(GetCommentLayer(layers[i], false));
							generateCode.AddFieldStaticReadonlyPublic("Int32", "Mask" + layers[i].ToVarName(),
								"1 << " + layers[i].ToVarName() + "_ID");

							if (i != layers.Length - 1)
							{
								generateCode.AddEmptyLine();
							}
						}

						generateCode.CurrentIndent--;
					}
					generateCode.AddClassEndDeclaration();
					generateCode.AddDoxygenEndGroup();

					generateCode.CurrentIndent--;
				}
				generateCode.AddNamespaceClose();

				generateCode.CurrentIndent--;
			}
			generateCode.AddNamespaceClose();
			generateCode.AddDelimetrPart();

			generateCode.SetLengthWithTabsOnlyDelimetrs(120);
			generateCode.Save(@"Lotus\Source\Common\Base\LotusBaseUnityLayers.cs");
		}
	}

	--------
	/// <summary>
	/// Получение комментария для слоя
	/// </summary>
	/// <param name="layer">Комментарий</param>
	/// <param name="is_object_desc">Описание для самого слоя</param>
	/// <returns>Комментарий для слоя</returns>
	--------
	private String GetCommentLayer(String layer, Boolean is_object_desc = true)
	{
		var result = "";
		switch (layer)
		{
			case "Default":
				{
					if (is_object_desc)
					{
						result = "Слой по умолчанию";
					}
					else
					{
						result = "Маска для слоя по умолчанию";
					}
				}
				break;
			case "TransparentFX":
				{
					if (is_object_desc)
					{
						result = "Слой для прозрачных объектов";
					}
					else
					{
						result = "Маска для слоя прозрачных объектов";
					}
				}
				break;
			case "IgnoreRaycast":
			case "Ignore Raycast":
				{
					if (is_object_desc)
					{
						result = "Слой для объектов которые игнорируют коллизию";
					}
					else
					{
						result = "Маска для слоя объектов которые игнорируют коллизию";
					}
				}
				break;
			case "Water":
				{
					if (is_object_desc)
					{
						result = "Слой для объектов которые представляют собой воду";
					}
					else
					{
						result = "Маска для слоя объектов которые представляют собой воду";
					}
				}
				break;
			case "UI":
				{
					if (is_object_desc)
					{
						result = "Слой для объектов пользовательского интерфейса";
					}
					else
					{
						result = "Маска для слоя объектов пользовательского интерфейса";
					}
				}
				break;
			default:
				{
					if (is_object_desc)
					{
						result = "Слой - " + layer;
					}
					else
					{
						result = "Маска слоя для - " + layer;
					}
				}
				break;
		}

		return (result);
	}
	#endregion

	#region =============================================== РАБОТА С ОСЯМИ ============================================
	--------
	/// <summary>
	/// Автогенерация кода для константных данных осей
	/// </summary>
	--------
	public void DrawAutogenAxis()
	{
		GUILayout.Space(4.0f);
		EditorGUILayout.LabelField("Генерация констант Axis", EditorStyles.largeLabel,
			GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

		GUILayout.Space(4.0f);
		EditorGUILayout.LabelField("Файл: LotusBaseUnityAxes", EditorStyles.boldLabel);

		GUILayout.Space(2.0f);
		if (GUILayout.Button("Generate"))
		{
			var generateCode = new CTextGenerateCodeCSharp();
			generateCode.AddFileHeader("Модуль общей функциональности", "Базовая подсистема Unity");
			generateCode.AddFileBriefDesc("LotusBaseUnityAxes.cs", "Общие данные для организации работы с осями ввода Unity");
			generateCode.AddFileVersion();
			generateCode.AddNamespaceUsing("System", "UnityEngine");
			generateCode.AddNamespaceOpen("Lotus");
			{
				generateCode.CurrentIndent++;
				generateCode.AddNamespaceOpen("Common");
				{
					generateCode.CurrentIndent++;
					generateCode.AddDoxygenAddToGroup("UnityCommonBase");
					generateCode.AddCommentSummary(true, "Статический класс содержащий константы используемых осей ввода Unity", true);
					generateCode.AddClassStaticPublic("XAxis");
					{
						generateCode.CurrentIndent++;

						var axes = GetAllAxisNames();
						for (var i = 0; i < axes.Length; i++)
						{
							generateCode.AddCommentSummaryForData("Ось - " + axes[i]);
							generateCode.AddFieldConstPublicString(axes[i].ToConstCase(), axes[i]);

							if (i != axes.Length - 1)
							{
								generateCode.AddEmptyLine();
							}
						}

						generateCode.CurrentIndent--;
					}
					generateCode.AddClassEndDeclaration();
					generateCode.AddDoxygenEndGroup();

					generateCode.CurrentIndent--;
				}
				generateCode.AddNamespaceClose();

				generateCode.CurrentIndent--;
			}
			generateCode.AddNamespaceClose();
			generateCode.AddDelimetrPart();

			generateCode.SetLengthWithTabsOnlyDelimetrs(120);
			generateCode.Save(@"Lotus\Source\Common\Base\LotusBaseUnityAxes.cs");
		}
	}

	--------
	/// <summary>
	/// Получение списка доступных осей
	/// </summary>
	/// <returns>Список осей</returns>
	--------
	private String[] GetAllAxisNames()
	{
		var result = new StringCollection();

		var serialized_object = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
		var axes_property = serialized_object.FindProperty("m_Axes");

		axes_property.Next(true);
		axes_property.Next(true);

		while (axes_property.Next(false))
		{
			SerializedProperty axis = axes_property.Copy();

			axis.Next(true);
			result.Add(axis.stringValue);
		}

		return result.Cast<string>().Distinct().ToArray();
	}
	#endregion
}
