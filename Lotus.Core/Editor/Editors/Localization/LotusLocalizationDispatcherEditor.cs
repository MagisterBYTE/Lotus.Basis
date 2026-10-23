
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusLocalizationDispatcherEditor.cs
*		Редактор центрального диспетчера для локализации ресурсов.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор центрального диспетчера для локализации ресурсов
/// </summary>
------------
[CustomEditor(typeof(LotusLocalizationDispatcher))]
public class LotusLocalizationDispatcherEditor : Editor
{
	#region =============================================== СТАТИЧЕСКИЕ ДАННЫЕ ========================================
	private static readonly GUIContent mContentRemove = new GUIContent("X");
	private static readonly GUIContent mContentAdd = new GUIContent("+");
	#endregion

	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Создания основного сервиса для локализации текстовых данных
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathLocalization + "Create Localization Dispatcher", false, XCoreSettingsEditor.MenuOrderLocalization)]
	public static void Create()
	{
#pragma warning disable 0219
		LotusLocalizationDispatcher localization_service = LotusLocalizationDispatcher.Instance;
#pragma warning restore 0219
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	private LotusLocalizationDispatcher mDispatcher;
	private Int32 mSelectedLangName;
	private String mPathFileName = "Assets";
	private Vector2 mScrollAll;
	private CTranslatorYandex mTranslator;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Включение скрипта в инспекторе объектов
	/// </summary>
	--------
	public void OnEnable()
	{
		mDispatcher = this.target as LotusLocalizationDispatcher;
		mTranslator = new CTranslatorYandex();
	}

	--------
	/// <summary>
	/// Рисование в инспекторе объектов
	/// </summary>
	--------
	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		{
			GUILayout.Space(4.0f);
			XEditorInspector.DrawGroup("Singleton settings");
			{
				GUILayout.Space(2.0f);
				mDispatcher.IsMainInstance = XEditorInspector.PropertyBoolean(nameof(mDispatcher.IsMainInstance), mDispatcher.IsMainInstance);

				GUILayout.Space(2.0f);
				EditorGUI.BeginDisabledGroup(mDispatcher.IsMainInstance);
				{
					mDispatcher.DestroyMode = XEditorInspector.PropertyEnum(nameof(mDispatcher.DestroyMode), mDispatcher.DestroyMode);
				}
				EditorGUI.EndDisabledGroup();

				GUILayout.Space(2.0f);
				mDispatcher.IsDontDestroy = XEditorInspector.PropertyBoolean(nameof(mDispatcher.IsDontDestroy), mDispatcher.IsDontDestroy);
			}

			// Рисование языков
			DrawLanguages();

			// Рисование всех текстовых данных и автоматический перевод
			DrawTranslation();

			// Рисование тестирование переводчика
			DrawTranslator();
		}
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.Save();
		}

		GUILayout.Space(2.0f);
	}
	#endregion

	#region =============================================== ОБЩИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Перевод всех данных
	/// </summary>
	--------
	private void TranslateAllData()
	{
		mTranslator.AbbrsFrom = mDispatcher.DefaultLanguage.Abbreviation;
		mTranslator.AbbrsTo = CLanguageInfo.LanguageAbbrs[mSelectedLangName];

		for (var j = 0; j < mDispatcher.mListTranslate.Count; j++)
		{
			mTranslator.OriginalText = mDispatcher.mListTranslate[j].Default;

			mTranslator.Translate();

			mDispatcher.mListTranslate[j].Translate = mTranslator.TranslateText;

			EditorUtility.DisplayCancelableProgressBar("Translate", mTranslator.OriginalText, (j + 1) / (Single)mDispatcher.mListTranslate.Count);
		}

		EditorUtility.ClearProgressBar();
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	--------
	/// <summary>
	/// Рисование языков
	/// </summary>
	--------
	public void DrawLanguages()
	{
		GUILayout.Space(4.0f);
		mDispatcher.mExpandedLanguages = XEditorInspector.DrawSectionFoldout("Languages", mDispatcher.mExpandedLanguages);
		if (mDispatcher.mExpandedLanguages)
		{
			// Перебираем список
			XEditorInspector.DrawGroup("Items:");

			for (var i = 0; i < mDispatcher.Languages.Count; i++)
			{
				GUILayout.Space(4.0f);
				CLanguageInfo lang = mDispatcher.Languages[i];
				{
					// Заголовок
					if (i == 0)
					{
						EditorGUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("Name");
							EditorGUILayout.LabelField("File");
						}
						EditorGUILayout.EndHorizontal();
						GUILayout.Space(4.0f);
					}

					EditorGUI.BeginChangeCheck();
					EditorGUILayout.BeginHorizontal();
					{
						lang.Name = XEditorInspector.SelectorValue(lang.Name, CLanguageInfo.LanguageNames);
						lang.FileData = (TextAsset)EditorGUILayout.ObjectField(lang.FileData, typeof(TextAsset), false);

						if (GUILayout.Button(mContentRemove, XEditorStyles.ButtonMiniDefaultRedRightStyle))
						{
							if (i > 0)
							{
								mDispatcher.Languages.RemoveAt(i);
								EditorGUILayout.EndHorizontal();
								EditorGUI.indentLevel--;

								mDispatcher.SaveInEditor();
								break;
							}
							else
							{
								mDispatcher.DefaultLanguage.Name = "";
								mDispatcher.DefaultLanguage.FileData = null;
							}
						}
					}
					EditorGUILayout.EndHorizontal();
					if(EditorGUI.EndChangeCheck())
					{
						mDispatcher.SaveInEditor();
					}

				}
			}

			GUILayout.Space(2.0f);
			if (GUILayout.Button(mContentAdd, XEditorStyles.ButtonMiniHeaderMiddleStyle, GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
			{
				mDispatcher.AddLang("Default");
			}

			GUILayout.Space(4.0f);
			XEditorInspector.DrawGroup("Operations:");

			// Генерация основного файла
			GUILayout.Space(4.0f);
			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Generate base"))
				{
					// Путь к сохранению
					mPathFileName = EditorUtility.OpenFolderPanel("Set path localization data", mPathFileName, "Lang");
					var lang_name = CLanguageInfo.LanguageNames[mSelectedLangName];
					var path = Path.Combine(mPathFileName, lang_name) + ".bytes";

					// Корректируем путь
					path = path.Remove(0, path.IndexOf("Assets"));
					path = path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

					// Создаем файл
					mDispatcher.CreateLocalizeFileDefault(path);

					// Делаем его по умолчанию
					mDispatcher.DefaultLanguage.Name = lang_name;
					mDispatcher.DefaultLanguage.Abbreviation = CLanguageInfo.LanguageAbbrs[mSelectedLangName];
					mDispatcher.DefaultLanguage.FileData = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
				}

				mSelectedLangName = XEditorInspector.SelectorIndex(mSelectedLangName, CLanguageInfo.LanguageNames);
			}
			EditorGUILayout.EndHorizontal();

			GUILayout.Space(2.0f);
			if (GUILayout.Button("Clear"))
			{
				mDispatcher.ClearLangs();
			}
		}
	}

	--------
	/// <summary>
	/// Рисование всех текстовых данных и автоматический перевод
	/// </summary>
	--------
	public void DrawTranslation()
	{
		//GUILayout.Space(4.0f);
		//mLocalizationDispatcher.mExpandedTranslation = EditorGUILayout.Foldout("Translation", mLocalizationDispatcher.mExpandedTranslation);
		//if (mLocalizationDispatcher.mExpandedTranslation)
		//{
		//	if (mLocalizationDispatcher.mListTranslate.Count > 10)
		//	{
		//		mScrollAll = GUILayout.BeginScrollView(mScrollAll, 
		//			GUILayout.Height((EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 10 + 4));
		//	}

		//	for (Int32 i = 0; i < mLocalizationDispatcher.mListTranslate.Count; i++)
		//	{
		//		CTextTranslate tt = mLocalizationDispatcher.mListTranslate[i];
		//		EditorGUILayout.BeginHorizontal();
		//		{
		//			tt.Default = XEditorInspector.PropertyString(tt.Default, XEditorInspector.TextLeftStyle);
		//			tt.Translate = XEditorInspector.PropertyString(tt.Translate, XEditorInspector.TextRightStyle);
		//		}
		//		EditorGUILayout.EndHorizontal();
		//	}

		//	if (mLocalizationDispatcher.mListTranslate.Count > 10)
		//	{
		//		GUILayout.EndScrollView();
		//	}

		//	GUILayout.Space(4.0f);
		//	XEditorInspector.DrawHeader("Operations:", XEditorInspector.ColorInfo, TextAnchor.MiddleLeft);

		//	GUILayout.Space(2.0f);
		//	if (GUILayout.Button("Get All Data"))
		//	{
		//		mLocalizationDispatcher.GetAllLocalizeData();
		//	}

		//	GUILayout.Space(2.0f);
		//	EditorGUILayout.BeginHorizontal();
		//	{
		//		if (GUILayout.Button("Translate To"))
		//		{
		//			TranslateAllData();
		//		}

		//		mSelectedLangName = EditorGUILayout.Popup(mSelectedLangName, CLanguageInfo.LanguageNames);
		//	}
		//	EditorGUILayout.EndHorizontal();

		//	GUILayout.Space(2.0f);
		//	if (GUILayout.Button("Save translate"))
		//	{
		//		mLocalizationDispatcher.GetAllLocalizeData();
		//	}
		//}
	}

	--------
	/// <summary>
	/// Рисование тестирование переводчика
	/// </summary>
	--------
	public void DrawTranslator()
	{
		GUILayout.Space(4.0f);
		mDispatcher.mExpandedTestTranslator = XEditorInspector.DrawSectionFoldout("Test Translator", mDispatcher.mExpandedTestTranslator);
		if (mDispatcher.mExpandedTestTranslator)
		{
			GUILayout.Space(2.0f);
			mTranslator.AbbrsFrom = XEditorInspector.PropertyString("Abbrs From", mTranslator.AbbrsFrom);

			GUILayout.Space(2.0f);
			mTranslator.AbbrsTo = XEditorInspector.PropertyString("Abbrs To", mTranslator.AbbrsTo);

			GUILayout.Space(2.0f);
			mTranslator.OriginalText = XEditorInspector.PropertyString("Original Text", mTranslator.OriginalText);

			GUILayout.Space(2.0f);
			mTranslator.TranslateText = XEditorInspector.PropertyString("Translate Text", mTranslator.TranslateText);

			GUILayout.Space(2.0f);
			if (GUILayout.Button("Translate"))
			{
				mTranslator.Translate();
			}

			GUILayout.Space(2.0f);
			if (GUILayout.Button("Get All Translate"))
			{
				//mLocalizationDispatcher.CreateLocalizeFileDefault();
			}
		}
	}
	#endregion
}
