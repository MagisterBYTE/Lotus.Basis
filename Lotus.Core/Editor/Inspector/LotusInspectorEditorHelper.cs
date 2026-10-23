
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusInspectorEditorHelper.cs
*		Определение дополнительных параметров и методов для рисования свойств/полей.
*		Определение дополнительных параметров оформления и методов обеспечивающих стилистическое единство отображения 
*	свойств/полей компонентов.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Reflection;
using UnityEngine;
------------
using Lotus.Core;

namespace Lotus
{
	namespace Editor
	{

		/** \addtogroup CoreUnityEditor
		*@{*/

		/// <summary>
		/// Статический класс для определения дополнительных параметров и методов для рисования свойств/полей
		/// </summary>

		public static class XEditorInspector
		{
			#region Fields
			/// <summary>
			/// Рабочий контент GUI
			/// </summary>
			public static readonly GUIContent Content = new GUIContent();

			/// <summary>
			/// Рабочий контент GUI для надписей
			/// </summary>
			public static readonly GUIContent ContentLabel = new GUIContent();

			/// <summary>
			/// Рабочий контент GUI для заголовков
			/// </summary>
			public static readonly GUIContent ContentHeader = new GUIContent();

			/// <summary>
			/// Актуальная ширина панели инспектора свойств
			/// </summary>
			public readonly static PropertyInfo PropertyContextWidth = typeof(UnityEditor.EditorGUIUtility).GetProperty("contextWidth", BindingFlags.NonPublic | BindingFlags.Static);
			#endregion

			#region Main methods
			
			/// <summary>
			/// Получение текстуры иконки связанной с расширением файла
			/// </summary>
			/// <remarks>
			/// https://answers.unity.com/questions/792118/what-are-the-editor-resources-by-name-for-editorgu.html
			/// </remarks>
			/// <param name="ext">Расширение файла</param>
			/// <returns>Текстура иконки файла или текстура общей иконки</returns>
			
			public static Texture2D GetIconForFile(String ext)
			{
				var file_icon = ext;
				var dot = ext.IndexOf('.');
				if (dot > -1)
				{
					file_icon = ext.Substring(dot + 1);
				}

				file_icon = file_icon.ToLower();


				switch (file_icon)
				{
					case "guiskin":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_GUISKIN).image as Texture2D);
						}
					case "mat":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_MATERIAL).image as Texture2D);
						}
					case "prefab":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_PREFAB).image as Texture2D);
						}
					case "shader":
					case "cginc":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_SHADER).image as Texture2D);
						}
					case "txt":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_TEXT).image as Texture2D);
						}
					case "xml":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_XML).image as Texture2D);
						}
					case "cs":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_SCRIPT).image as Texture2D);
						}
					case "boo":
						{
							return UnityEditor.EditorGUIUtility.FindTexture("boo Script Icon");
						}
					case "js":
						{
							return UnityEditor.EditorGUIUtility.FindTexture("Js Script Icon");
						}
					case "meta":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_META).image as Texture2D);
						}
					case "unity":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_SCENE).image as Texture2D);
						}
					case "asset":
					case "prefs":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_GAME_MANAGER).image as Texture2D);
						}
					case "anim":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_ANIMATION_CLIP).image as Texture2D);
						}
					case "signal":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_SIGNAL).image as Texture2D);
						}
					case "physicmaterial":
					case "physicsmaterial":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_PHYSICS_MATERIAL).image as Texture2D);
						}
					case "ttf":
					case "otf":
					case "fon":
					case "fnt":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_FONT).image as Texture2D);
						}
					case "aac":
					case "aif":
					case "aiff":
					case "au":
					case "mid":
					case "midi":
					case "mp3":
					case "mpa":
					case "ra":
					case "ram":
					case "wma":
					case "wav":
					case "wave":
					case "ogg":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_AUDIO_SOURCE).image as Texture2D);
						}
					case "ai":
					case "apng":
					case "png":
					case "bmp":
					case "cdr":
					case "dib":
					case "eps":
					case "exif":
					case "gif":
					case "ico":
					case "icon":
					case "j":
					case "j2c":
					case "j2k":
					case "jas":
					case "jiff":
					case "jng":
					case "jp2":
					case "jpc":
					case "jpe":
					case "jpeg":
					case "jpf":
					case "jpg":
					case "jpw":
					case "jpx":
					case "jtf":
					case "mac":
					case "omf":
					case "qif":
					case "qti":
					case "qtif":
					case "tex":
					case "tfw":
					case "tga":
					case "tif":
					case "tiff":
					case "wmf":
					case "psd":
					case "exr":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_TEXTURE_2D).image as Texture2D);
						}
					case "3df":
					case "3dm":
					case "3dmf":
					case "3ds":
					case "3dv":
					case "3dx":
					case "blend":
					case "c4d":
					case "lwo":
					case "lws":
					case "ma":
					case "max":
					case "mb":
					case "mesh":
					case "obj":
					case "vrl":
					case "wrl":
					case "wrz":
					case "fbx":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_PREFAB_MODEL).image as Texture2D);
						}
					case "asf":
					case "asx":
					case "avi":
					case "dat":
					case "divx":
					case "dvx":
					case "mlv":
					case "m2l":
					case "m2t":
					case "m2ts":
					case "m2v":
					case "m4e":
					case "m4v":
					case "mjp":
					case "mov":
					case "movie":
					case "mp21":
					case "mp4":
					case "mpe":
					case "mpeg":
					case "mpg":
					case "mpv2":
					case "ogm":
					case "qt":
					case "rm":
					case "rmvb":
					case "wmw":
					case "xvid":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_VIDEO_CLIP).image as Texture2D);
						}
					case "colors":
					case "gradients":
					case "curves":
					case "curvesnormalized":
					case "particlecurves":
					case "particlecurvessigned":
					case "particledoublecurves":
					case "particledoublecurvessigned":
						{
							return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_SCRIPTABLE_OBJECT).image as Texture2D);
						}
				}
				return (UnityEditor.EditorGUIUtility.IconContent(XEditorStyles.ICON_DEFAULT_ASSET).image as Texture2D);
			}

			
			/// <summary>
			/// Получение текстуры иконки связанной с указанным типом
			/// </summary>
			/// <param name="type">Тип объекта</param>
			/// <returns>Текстура иконки файла или текстура общей иконки</returns>
			
			public static Texture2D GetIconForFile(Type type)
			{
				Type type_editor = typeof(UnityEditor.EditorGUIUtility);
				MethodInfo findtexturebytype_method = type_editor.GetMethod("FindTextureByType", BindingFlags.NonPublic | BindingFlags.Static);

				if (findtexturebytype_method != null)
				{
					var texture = findtexturebytype_method.Invoke(null, new System.Object[] { type });
					if (texture is Texture2D)
					{
						return (texture as Texture2D);
					}
				}


				return UnityEditor.EditorGUIUtility.FindTexture("DefaultAsset Icon");
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОДГОТОВКИ КОНТЕНТА ================================
			
			/// <summary>
			/// Подготовка контента GUI для рисования свойства на основе доступных атрибутов
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			
			public static void PrepareContent(UnityEditor.SerializedProperty property)
			{
				if (property != null)
				{
					// Есть атрибут имени
					LotusDisplayNameAttribute name = property.GetAttribute<LotusDisplayNameAttribute>();
					if (name != null)
					{
						Content.text = name.Name;
						UnityEditor.EditorGUI.indentLevel = name.Indent;
					}
					else
					{
						Content.text = property.displayName;
					}

					// Есть атрибут подсказки
					TooltipAttribute tooltip = property.GetAttribute<TooltipAttribute>();
					if (tooltip != null)
					{
						Content.tooltip = tooltip.tooltip;
					}
					else
					{
						Content.tooltip = "";
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ РИСОВАНИЯ СЕКЦИИ ===================================
			
			/// <summary>
			/// Рисование секции
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSection(String text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				ContentHeader.text = text;
				DrawSection(ContentHeader, XEditorStyles.ColorHeaderSection, text_anchor);
			}

			
			/// <summary>
			/// Рисование секции
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSection(String text, Color color_text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				ContentHeader.text = text;
				DrawSection(ContentHeader, color_text, text_anchor);
			}

			
			/// <summary>
			/// Рисование секции
			/// </summary>
			/// <param name="content">Контент заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSection(GUIContent content, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				DrawSection(content, XEditorStyles.ColorHeaderSection, text_anchor);
			}

			
			/// <summary>
			/// Рисование секции
			/// </summary>
			/// <param name="content">Контент заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSection(GUIContent content, Color color_text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				GUIStyle style = UnityEditor.EditorStyles.boldLabel;

				// Старые параметры
				Color old_color = style.normal.textColor;
				TextAnchor old_anchor = style.alignment;
				style.fontSize++;

				// Новые параметры
				style.normal.textColor = color_text;
				style.alignment = text_anchor;

				// Рисуем
				GUILayout.Space(UnityEditor.EditorGUIUtility.standardVerticalSpacing);
				GUILayout.Label(content, style);

				// Восстанавливаем старые параметры
				style.normal.textColor = old_color;
				style.alignment = old_anchor;
				style.fontSize--;
			}

			
			/// <summary>
			/// Рисование секции в области
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSectionBox(String text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				ContentHeader.text = text;
				DrawSectionBox(ContentHeader, XEditorStyles.ColorHeaderSection, text_anchor);
			}

			
			/// <summary>
			/// Рисование секции в области
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSectionBox(String text, Color color_text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				ContentHeader.text = text;
				DrawSectionBox(ContentHeader, color_text, text_anchor);
			}

			
			/// <summary>
			/// Рисование секции в области
			/// </summary>
			/// <param name="content">Контент заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSectionBox(GUIContent content, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				DrawSectionBox(content, XEditorStyles.ColorHeaderSection, text_anchor);
			}

			
			/// <summary>
			/// Рисование секции в области
			/// </summary>
			/// <param name="content">Контент заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawSectionBox(GUIContent content, Color color_text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				GUIStyle style = UnityEditor.EditorStyles.boldLabel;

				// Старые параметры
				Color old_color = style.normal.textColor;
				TextAnchor old_anchor = style.alignment;
				style.fontSize++;

				// Новые параметры
				style.normal.textColor = color_text;
				style.alignment = text_anchor;

				// Рисуем
				Rect position = UnityEditor.EditorGUILayout.BeginHorizontal();
				{
					//GUILayout.Space(-OffsetFoldoutOne);
					GUILayout.Box("", UnityEditor.EditorStyles.helpBox, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT + UnityEditor.EditorGUIUtility.standardVerticalSpacing));

					position.height -= UnityEditor.EditorGUIUtility.standardVerticalSpacing;
					if (text_anchor == TextAnchor.UpperLeft ||
						text_anchor == TextAnchor.MiddleLeft ||
						text_anchor == TextAnchor.LowerLeft)
					{
						position.x += UnityEditor.EditorGUIUtility.standardVerticalSpacing * 2;
					}

					GUI.Label(position, content, style);
				}
				UnityEditor.EditorGUILayout.EndHorizontal();

				// Восстанавливаем старые параметры
				style.normal.textColor = old_color;
				style.alignment = old_anchor;
				style.fontSize--;
			}

			
			/// <summary>
			/// Рисование секции в области по центру инспектора свойств с раскрытием
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="opened">Статус открывания</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			/// <returns>Статус открытости</returns>
			
			public static Boolean DrawSectionFoldout(String text, Boolean opened, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				var result = "";
				if (opened == false)
				{
					result = XString.TriangleRight + " " + text;
				}
				else
				{
					result = XString.TriangleDown + " " + text;
				}

				ContentHeader.text = result;
				DrawSectionBox(ContentHeader, text_anchor);

				if (Event.current.type == EventType.MouseDown)
				{
					Rect mouse_rect = GUILayoutUtility.GetLastRect();
					if (mouse_rect.Contains(Event.current.mousePosition))
					{
						opened = !opened;
						Event.current.Use();
					}
				}

				return (opened);
			}

			
			/// <summary>
			/// Рисование секции в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="opened">Статус открывания</param>
			/// <param name="on_draw">Делегат для рисования данных при раскрытии панели</param>
			/// <param name="on_move_up">Делегат для действий при перемещении вверх</param>
			/// <param name="on_move_down">Делегат для действий при перемещении вниз</param>
			/// <param name="on_delete">Делегат для действий при удалении</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			/// <returns>Статус открытости</returns>
			
			public static Boolean DrawSectionFoldout(String text, ref Boolean opened, Action on_draw,
				Action on_move_up, Action on_move_down, Action on_delete,
				TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				var result = "";
				if (opened == false)
				{
					result = XString.TriangleRight + " " + text;
				}
				else
				{
					result = XString.TriangleDown + " " + text;
				}

				ContentHeader.text = result;
				DrawSectionBox(ContentHeader, text_anchor);

				Rect mouse_rect = GUILayoutUtility.GetLastRect();

				if (opened)
				{
					on_draw();
				}

				// Управляющие кнопки
				var rect_button_close = new Rect(mouse_rect.xMax - XInspectorViewParams.BUTTON_MINI_WIDTH - 1, mouse_rect.y + 2,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.HEADER_HEIGHT - 1);
				if (GUI.Button(rect_button_close, "X", XEditorStyles.ButtonMiniDefaultRedRightStyle))
				{
					on_delete();
				}

				var rect_button_move_down = new Rect(mouse_rect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH *2) - 2, mouse_rect.y + 2,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.HEADER_HEIGHT - 1);
				if (GUI.Button(rect_button_move_down, XString.TriangleDown, UnityEditor.EditorStyles.miniButtonMid))
				{
					on_move_down();
				}

				var rect_button_move_up = new Rect(mouse_rect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 3) - 3, mouse_rect.y + 2,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.HEADER_HEIGHT - 1);
				if (GUI.Button(rect_button_move_up, XString.TriangleUp, UnityEditor.EditorStyles.miniButtonLeft))
				{
					on_move_up();
				}

				if (Event.current.type == EventType.MouseDown)
				{
					if (mouse_rect.Contains(Event.current.mousePosition) &&
						rect_button_close.Contains(Event.current.mousePosition) == false &&
						rect_button_move_down.Contains(Event.current.mousePosition) == false &&
						rect_button_move_up.Contains(Event.current.mousePosition) == false)
					{
						opened = !opened;
						Event.current.Use();
					}
				}

				return (opened);
			}
			#endregion

			#region ======================================= МЕТОДЫ РИСОВАНИЯ ГРУППЫ ===================================
			
			/// <summary>
			/// Рисование группы
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroup(String text, TextAnchor text_anchor = TextAnchor.MiddleLeft)
			{
				ContentHeader.text = text;
				DrawGroup(ContentHeader, XEditorStyles.ColorHeaderGroup, 0, text_anchor);
			}

			
			/// <summary>
			/// Рисование группы
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroup(String text, Color color_text, TextAnchor text_anchor = TextAnchor.MiddleLeft)
			{
				ContentHeader.text = text;
				DrawGroup(ContentHeader, color_text, 0, text_anchor);
			}

			
			/// <summary>
			/// Рисование группы
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="indent">Уровень смещения</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroup(String text, Color color_text, Int32 indent, TextAnchor text_anchor = TextAnchor.MiddleLeft)
			{
				ContentHeader.text = text;
				DrawGroup(ContentHeader, color_text, indent, text_anchor);
			}

			
			/// <summary>
			/// Рисование группы
			/// </summary>
			/// <param name="content">Контент заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroup(GUIContent content, TextAnchor text_anchor = TextAnchor.MiddleLeft)
			{
				DrawGroup(content, XEditorStyles.ColorHeaderGroup, 0, text_anchor);
			}

			
			/// <summary>
			/// Рисование группы
			/// </summary>
			/// <param name="content">Контент заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroup(GUIContent content, Color color_text, TextAnchor text_anchor = TextAnchor.MiddleLeft)
			{
				DrawGroup(content, color_text, 0, text_anchor);
			}

			
			/// <summary>
			/// Рисование группы
			/// </summary>
			/// <param name="content">Контент заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="indent">Уровень смещения</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroup(GUIContent content, Color color_text, Int32 indent, TextAnchor text_anchor = TextAnchor.MiddleLeft)
			{
				GUIStyle style = UnityEditor.EditorStyles.boldLabel;

				// Старые параметры
				Color old_color = style.normal.textColor;
				TextAnchor old_anchor = style.alignment;

				// Новые параметры
				style.normal.textColor = color_text;
				style.alignment = text_anchor;

				// Рисуем
				GUILayout.Space(UnityEditor.EditorGUIUtility.standardVerticalSpacing);
				if (indent > 0)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Space(XInspectorViewParams.OFFSET_INDENT * indent);
					GUILayout.Label(content, style);
					GUILayout.EndHorizontal();
				}
				else
				{
					GUILayout.Label(content, style);
				}

				// Восстанавливаем старые параметры
				style.normal.textColor = old_color;
				style.alignment = old_anchor;
			}

			
			/// <summary>
			/// Рисование группы в области
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroupBox(String text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				DrawGroupBox(text, XEditorStyles.ColorHeaderGroup, 0, text_anchor);
			}

			
			/// <summary>
			/// Рисование группы в области
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroupBox(String text, Color color_text, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				DrawGroupBox(text, color_text, 0, text_anchor);
			}

			
			/// <summary>
			/// Рисование группы в области
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="color_text">Цвет текста заголовка</param>
			/// <param name="indent">Уровень смещения</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			
			public static void DrawGroupBox(String text, Color color_text, Int32 indent, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				GUIStyle style = UnityEditor.EditorStyles.boldLabel;

				// Старые параметры
				Color old_color = style.normal.textColor;
				TextAnchor old_anchor = style.alignment;

				// Новые параметры
				style.normal.textColor = color_text;
				style.alignment = text_anchor;

				// Рисуем
				Rect position = UnityEditor.EditorGUILayout.BeginHorizontal();
				{
					if (indent > 0)
					{
						GUILayout.Space(XInspectorViewParams.OFFSET_INDENT * indent);
					}
					GUILayout.Box("", UnityEditor.EditorStyles.helpBox, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

					position.height -= UnityEditor.EditorGUIUtility.standardVerticalSpacing;
					if (text_anchor == TextAnchor.UpperLeft ||
						text_anchor == TextAnchor.MiddleLeft ||
						text_anchor == TextAnchor.LowerLeft)
					{
						position.x += UnityEditor.EditorGUIUtility.standardVerticalSpacing * 2;
					}

					GUI.Label(position, text, style);
				}
				UnityEditor.EditorGUILayout.EndHorizontal();

				// Восстанавливаем старые параметры
				style.normal.textColor = old_color;
				style.alignment = old_anchor;
			}

			
			/// <summary>
			/// Рисование группы по центру инспектора свойств с раскрытием и смещением с левой стороны
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="opened">Статус открывания</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			/// <returns>Статус открытости</returns>
			
			public static Boolean DrawGroupFoldout(String text, Boolean opened, TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				var result = "";
				if (opened == false)
				{
					result = XString.TriangleRight + " " + text;
				}
				else
				{
					result = XString.TriangleDown + " " + text;
				}

				DrawGroupBox(result, text_anchor);

				if (Event.current.type == EventType.MouseDown)
				{
					Rect mouse_rect = GUILayoutUtility.GetLastRect();
					if (mouse_rect.Contains(Event.current.mousePosition))
					{
						opened = !opened;
						Event.current.Use();
					}
				}

				return (opened);
			}

			
			/// <summary>
			/// Рисование группы в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="opened">Статус открывания</param>
			/// <param name="on_draw">Делегат для рисования данных при раскрытии панели</param>
			/// <param name="on_move_up">Делегат для действий при перемещении вверх</param>
			/// <param name="on_move_down">Делегат для действий при перемещении вниз</param>
			/// <param name="on_delete">Делегат для действий при удалении</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			/// <returns>Статус открытости</returns>
			
			public static Boolean DrawGroupFoldout(String text, ref Boolean opened, Action on_draw,
				Action on_move_up, Action on_move_down, Action on_delete,
				TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				var result = "";
				if (opened == false)
				{
					result = XString.TriangleRight + " " + text;
				}
				else
				{
					result = XString.TriangleDown + " " + text;
				}

				DrawGroupBox(result, text_anchor);

				Rect mouse_rect = GUILayoutUtility.GetLastRect();

				if (opened)
				{
					on_draw();
				}

				// Управляющие кнопки
				var rect_button_delete = new Rect(mouse_rect.xMax - XInspectorViewParams.BUTTON_MINI_WIDTH - 1, mouse_rect.y + 1,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
				if (GUI.Button(rect_button_delete, "X", XEditorStyles.ButtonMiniDefaultRedRightStyle))
				{
					on_delete();
				}

				var rect_button_move_down = new Rect(mouse_rect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 2) - 2, mouse_rect.y + 1,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
				if (GUI.Button(rect_button_move_down, XString.TriangleDown, UnityEditor.EditorStyles.miniButtonMid))
				{
					on_move_down();
				}

				var rect_button_move_up = new Rect(mouse_rect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 3) - 3, mouse_rect.y + 1,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
				if (GUI.Button(rect_button_move_up, XString.TriangleUp, UnityEditor.EditorStyles.miniButtonLeft))
				{
					on_move_up();
				}

				if (Event.current.type == EventType.MouseDown)
				{
					if (mouse_rect.Contains(Event.current.mousePosition) &&
						rect_button_delete.Contains(Event.current.mousePosition) == false &&
						rect_button_move_down.Contains(Event.current.mousePosition) == false &&
						rect_button_move_up.Contains(Event.current.mousePosition) == false)
					{
						opened = !opened;
						Event.current.Use();
					}
				}

				return (opened);
			}

			
			/// <summary>
			/// Рисование группы в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			/// <param name="opened">Статус открывания</param>
			/// <param name="on_draw">Делегат для рисования данных при раскрытии панели</param>
			/// <param name="on_move_up">Делегат для действий при перемещении вверх</param>
			/// <param name="on_move_down">Делегат для действий при перемещении вниз</param>
			/// <param name="on_duplicate">Делегат для действий при дублировании элемента</param>
			/// <param name="on_delete">Делегат для действий при удалении</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			/// <returns>Статус открытости</returns>
			
			public static Boolean DrawGroupFoldout(String text, ref Boolean opened, Action on_draw,
				Action on_move_up, Action on_move_down, Action on_duplicate, Action on_delete,
				TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				return (DrawGroupFoldout(0, text, ref opened, on_draw, on_move_up, on_move_down, on_duplicate,
					on_delete, text_anchor));
			}

			
			/// <summary>
			/// Рисование группы в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
			/// </summary>
			/// <param name="indent">Уровень вложения заголовка</param>
			/// <param name="text">Текст заголовка</param>
			/// <param name="opened">Статус открывания</param>
			/// <param name="on_draw">Делегат для рисования данных при раскрытии панели</param>
			/// <param name="on_move_up">Делегат для действий при перемещении вверх</param>
			/// <param name="on_move_down">Делегат для действий при перемещении вниз</param>
			/// <param name="on_duplicate">Делегат для действий при дублировании элемента</param>
			/// <param name="on_delete">Делегат для действий при удалении</param>
			/// <param name="text_anchor">Выравнивание текста</param>
			/// <returns>Статус открытости</returns>
			
			public static Boolean DrawGroupFoldout(Int32 indent, String text, ref Boolean opened, Action on_draw,
				Action on_move_up, Action on_move_down, Action on_duplicate, Action on_delete,
				TextAnchor text_anchor = TextAnchor.MiddleCenter)
			{
				var result = "";
				if (opened == false)
				{
					result = XString.TriangleRight + " " + text;
				}
				else
				{
					result = XString.TriangleDown + " " + text;
				}

				DrawGroupBox(result, XEditorStyles.ColorHeaderGroup, indent, text_anchor);

				Rect mouse_rect = GUILayoutUtility.GetLastRect();

				if (opened)
				{
					on_draw();
				}

				// Управляющие кнопки
				var rect_button_delete = new Rect(mouse_rect.xMax - XInspectorViewParams.BUTTON_MINI_WIDTH - 1, mouse_rect.y + 1,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
				if (GUI.Button(rect_button_delete, "X", XEditorStyles.ButtonMiniDefaultRedRightStyle))
				{
					on_delete();
				}

				var rect_button_dublicate = new Rect(mouse_rect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 2) - 2, mouse_rect.y + 1,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
				if (GUI.Button(rect_button_dublicate, "D", UnityEditor.EditorStyles.miniButtonMid))
				{
					on_duplicate();
				}

				var rect_button_move_down = new Rect(mouse_rect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 3) - 3, mouse_rect.y + 1,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
				if (GUI.Button(rect_button_move_down, XString.TriangleDown, UnityEditor.EditorStyles.miniButtonMid))
				{
					on_move_down();
				}

				var rect_button_move_up = new Rect(mouse_rect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 4) - 4, mouse_rect.y + 1,
					XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
				if (GUI.Button(rect_button_move_up, XString.TriangleUp, UnityEditor.EditorStyles.miniButtonLeft))
				{
					on_move_up();
				}

				if (Event.current.type == EventType.MouseDown)
				{
					if (mouse_rect.Contains(Event.current.mousePosition) &&
						rect_button_delete.Contains(Event.current.mousePosition) == false &&
						rect_button_dublicate.Contains(Event.current.mousePosition) == false &&
						rect_button_move_down.Contains(Event.current.mousePosition) == false &&
						rect_button_move_up.Contains(Event.current.mousePosition) == false)
					{
						opened = !opened;
						Event.current.Use();
					}
				}

				return (opened);
			}
			#endregion

			#region ======================================= МЕТОДЫ РИСОВАНИЯ ЗАГОЛОВКОВ ===============================
			
			/// <summary>
			/// Рисование заголовков
			/// </summary>
			/// <param name="text">Текст заголовка</param>
			
			public static void DrawCaption(String text)
            {
				GUI.color = Color.yellow;
                UnityEditor.EditorGUILayout.LabelField(text, UnityEditor.EditorStyles.boldLabel);
				GUI.color = Color.white;
			}
            #endregion

            #region ======================================= МЕТОДЫ РАСЧЕТА ПОЗИЦИЙ ====================================
            
            /// <summary>
            /// Вычисление прямоугольников для отображения элементов с учетом отступов
            /// </summary>
            /// <param name="rect">Базовый прямоугольник</param>
            /// <param name="rect_label">Первый вычисляемый прямоугольник</param>
            /// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
            /// <param name="rect_field">Второй вычисляемый прямоугольник</param>
            /// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
            /// <param name="offset_right">Дополнительное смещение от правой стороны</param>
            
            public static void ComputeRects(Rect rect, out Rect rect_label, Single coeff1, out Rect rect_field, Single coeff2,
				Single offset_right = 0)
			{
				// Получаем актуальную ширину инспектора свойств
				var context_width = (Single)PropertyContextWidth.GetValue(null, null);

				// Рабочая ширина
				var work_width = context_width - (offset_right + XInspectorViewParams.SCROLL_WIDTH);
				rect_label = rect;

				// Если ноль то ширина для надписи равна текущей
				if (coeff1 == 0)
				{
					rect_label.width = UnityEditor.EditorGUIUtility.labelWidth;

					rect_field = rect;
					rect_field.x = rect_label.xMax;
					rect_field.width = Mathf.Min(work_width, rect.width) - UnityEditor.EditorGUIUtility.labelWidth;
				}
				else
				{
					rect_label.width = work_width * coeff1;

					rect_field = rect;
					rect_field.x = rect_label.xMax;
					rect_field.width = Mathf.Min(work_width, rect.width) * coeff2;
				}

				if (rect_field.xMax > work_width)
				{
					var delta = rect_field.xMax - work_width - XInspectorViewParams.SCROLL_WIDTH;
					rect_field.width -= delta + 6;
				}
			}

			
			/// <summary>
			/// Вычисление прямоугольников для отображения элементов с учетом отступов
			/// </summary>
			/// <param name="rect">Базовый прямоугольник</param>
			/// <param name="rect_label">Первый вычисляемый прямоугольник</param>
			/// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
			/// <param name="rect_field1">Второй вычисляемый прямоугольник</param>
			/// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
			/// <param name="rect_field2">Третий вычисляемый прямоугольник</param>
			/// <param name="coeff3">Коэффициент ширины третьего прямоугольника</param>
			/// <param name="offset_right">Дополнительное смещение от правой стороны</param>
			
			public static void ComputeRects(Rect rect, out Rect rect_label, Single coeff1, out Rect rect_field1, Single coeff2,
				out Rect rect_field2, Single coeff3, Single offset_right = 0)
			{
				// Получаем актуальную ширину инспектора свойств
				var context_width = (Single)PropertyContextWidth.GetValue(null, null);

				// Рабочая ширина
				var work_width = context_width - (offset_right + XInspectorViewParams.SCROLL_WIDTH);
				rect_label = rect;

				// Если ноль то ширина для надписи равна текущей
				if (coeff1 == 0)
				{
					rect_label.width = UnityEditor.EditorGUIUtility.labelWidth;
				}
				else
				{
					rect_label.width = work_width * coeff1;
				}

				rect_field1 = rect;
				rect_field1.x = rect_label.xMax;
				rect_field1.width = Mathf.Min(work_width, rect.width) * coeff2;

				rect_field2 = rect;
				rect_field2.x = rect_field1.xMax;
				rect_field2.width = Mathf.Min(work_width, rect.width) * coeff3;

				if (rect_field2.xMax > work_width)
				{
					var delta = rect_field2.xMax - work_width - XInspectorViewParams.SCROLL_WIDTH;
					rect_field2.width -= delta + 6;
				}
			}

			
			/// <summary>
			/// Вычисление прямоугольников для отображения элементов с учетом отступов
			/// </summary>
			/// <param name="rect">Базовый прямоугольник</param>
			/// <param name="rect_label">Первый вычисляемый прямоугольник</param>
			/// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
			/// <param name="rect_field">Второй вычисляемый прямоугольник</param>
			/// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
			/// <param name="rect_button_right">Прямоугольник для кнопки справа</param>
			
			public static void ComputeRectsWithButton(Rect rect, out Rect rect_label, Single coeff1, out Rect rect_field,
				Single coeff2, out Rect rect_button_right)
			{
				ComputeRects(rect, out rect_label, coeff1, out rect_field, coeff2, 0);
				rect_field.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_right = rect;
				rect_button_right.x = rect_field.xMax;
				rect_button_right.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_right.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
			}

			
			/// <summary>
			/// Вычисление прямоугольников для отображения элементов с учетом отступов
			/// </summary>
			/// <param name="rect">Базовый прямоугольник</param>
			/// <param name="rect_label">Первый вычисляемый прямоугольник</param>
			/// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
			/// <param name="rect_field1">Второй вычисляемый прямоугольник</param>
			/// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
			/// <param name="rect_field2">Третий вычисляемый прямоугольник</param>
			/// <param name="coeff3">Коэффициент ширины третьего прямоугольника</param>
			/// <param name="rect_button_right">Прямоугольник для кнопки справа</param>
			
			public static void ComputeRectsWithButtons(Rect rect, out Rect rect_label, Single coeff1, out Rect rect_field1,
				Single coeff2, out Rect rect_field2, Single coeff3, out Rect rect_button_right)
			{
				ComputeRects(rect, out rect_label, coeff1, out rect_field1, coeff2, out rect_field2, coeff3, 0);
				rect_field2.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_right = rect;
				rect_button_right.x = rect_field2.xMax;
				rect_button_right.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_right.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
			}

			
			/// <summary>
			/// Вычисление прямоугольников для отображения элементов с учетом отступов
			/// </summary>
			/// <param name="rect">Базовый прямоугольник</param>
			/// <param name="rect_label">Первый вычисляемый прямоугольник</param>
			/// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
			/// <param name="rect_field">Второй вычисляемый прямоугольник</param>
			/// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
			/// <param name="rect_button_left">Прямоугольник для кнопки слева</param>
			/// <param name="rect_button_right">Прямоугольник для кнопки справа</param>
			
			public static void ComputeRectsWithButtons(Rect rect, out Rect rect_label, Single coeff1, out Rect rect_field, Single coeff2,
				out Rect rect_button_left, out Rect rect_button_right)
			{
				ComputeRects(rect, out rect_label, coeff1, out rect_field, coeff2, 0);
				rect_field.width -= XInspectorViewParams.BUTTON_MINI_WIDTH * 2;

				rect_button_left = rect;
				rect_button_left.x = rect_field.xMax;
				rect_button_left.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_left.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

				rect_button_right = rect_button_left;
				rect_button_right.x = rect_button_left.xMax;
				rect_button_right.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_right.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
			}

			
			/// <summary>
			/// Вычисление прямоугольников для отображения элементов с учетом отступов
			/// </summary>
			/// <param name="rect">Базовый прямоугольник</param>
			/// <param name="rect_label">Первый вычисляемый прямоугольник</param>
			/// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
			/// <param name="rect_field">Второй вычисляемый прямоугольник</param>
			/// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
			/// <param name="rect_button_left">Прямоугольник для кнопки слева</param>
			/// <param name="rect_button_middle">Прямоугольник для кнопки по центру</param>
			/// <param name="rect_button_right">Прямоугольник для кнопки справа</param>
			
			public static void ComputeRectsWithButtons(Rect rect, out Rect rect_label, Single coeff1, out Rect rect_field, Single coeff2,
				out Rect rect_button_left, out Rect rect_button_middle, out Rect rect_button_right)
			{
				ComputeRects(rect, out rect_label, coeff1, out rect_field, coeff2, 0);
				rect_field.width -= XInspectorViewParams.BUTTON_MINI_WIDTH * 3;

				rect_button_left = rect;
				rect_button_left.x = rect_field.xMax;
				rect_button_left.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_left.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

				rect_button_middle = rect_button_left;
				rect_button_middle.x = rect_button_left.xMax;
				rect_button_middle.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_middle.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

				rect_button_right = rect_button_middle;
				rect_button_right.x = rect_button_middle.xMax;
				rect_button_right.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
				rect_button_right.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
			}
			#endregion

			#region ======================================= РЕДАКТОРЫ СВОЙСТВ =========================================
			
			/// <summary>
			/// Редактор выбора индекса из массива значений строкового типа
			/// </summary>
			/// <param name="selected">Выбранный индекс</param>
			/// <param name="contents">Массив значений</param>
			/// <returns>Выбранный индекс</returns>
			
			public static Int32 SelectorIndex(Int32 selected, String[] contents)
			{
				selected = UnityEditor.EditorGUILayout.Popup(selected, contents, UnityEditor.EditorStyles.toolbarPopup, GUILayout.ExpandWidth(true));

				return (selected);
			}

			
			/// <summary>
			/// Редактор выбора индекса из массива значений строкового типа
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="selected">Выбранный индекс</param>
			/// <param name="contents">Массив значений</param>
			/// <returns>Выбранный индекс</returns>
			
			public static Int32 SelectorIndex(String name, Int32 selected, String[] contents)
			{
				selected = UnityEditor.EditorGUILayout.Popup(name, selected, contents, UnityEditor.EditorStyles.toolbarPopup, GUILayout.ExpandWidth(true));

				return (selected);
			}

			
			/// <summary>
			/// Редактор выбора значения из массива значений строкового типа
			/// </summary>
			/// <param name="selected">Выбранное значение</param>
			/// <param name="contents">Массив значений</param>
			/// <returns>Выбранное значение</returns>
			
			public static String SelectorValue(String selected, String[] contents)
			{
				var index = -1;

				for (var i = 0; i < contents.Length; i++)
				{
					if (contents[i].EqualIgnoreCase(selected))
					{
						index = i;
						break;
					}
				}

				index = UnityEditor.EditorGUILayout.Popup(index, contents, UnityEditor.EditorStyles.toolbarPopup, GUILayout.ExpandWidth(true));

				if (index == -1) return (contents[0]);
				return (contents[index]);
			}

			
			/// <summary>
			/// Редактор свойства типа Boolean
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Boolean PropertyBoolean(String name, Boolean value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.Toggle(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Boolean с отображением как группы
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Boolean PropertyBooleanAsGroup(String name, Boolean value)
			{
				GUIStyle style = UnityEditor.EditorStyles.label;

				// Старые параметры
				Color old_color = style.normal.textColor;
				Font old_font = style.font;

				// Новые параметры
				style.normal.textColor = XEditorStyles.ColorHeaderGroup;
				style.font = UnityEditor.EditorStyles.boldFont;

				// Рисуем
				Content.text = name;
				value = UnityEditor.EditorGUILayout.Toggle(Content, value);

				// Восстанавливаем старые параметры
				style.normal.textColor = old_color;
				style.font = old_font;

				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Int32
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Int32 PropertyInt(String name, Int32 value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.IntField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Int32 в указанном диапазоне
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <param name="min">Минимальное значение</param>
			/// <param name="max">Максимальное значение</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Int32 PropertyIntSlider(String name, Int32 value, Int32 min, Int32 max)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.IntSlider(Content, value, min, max);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства для слоя
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Int32 PropertyLayer(String name, Int32 value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.LayerField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Single
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Single PropertyFloat(String name, Single value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.FloatField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Single в указанном диапазоне
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <param name="min">Минимальное значение</param>
			/// <param name="max">Максимальное значение</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Single PropertyFloatSlider(String name, Single value, Single min, Single max)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.Slider(Content, value, min, max);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Single в указанном диапазоне
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <param name="min">Минимальное значение</param>
			/// <param name="max">Максимальное значение</param>
			/// <param name="step">Шаг приращения</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Single PropertyFloatManager(String name, Single value, Single min, Single max, Single step)
			{
				Content.text = name;
				UnityEditor.EditorGUILayout.BeginHorizontal();
				{
					value = UnityEditor.EditorGUILayout.Slider(Content, value, min, max);

					if (GUILayout.Button(XString.TriangleDown, UnityEditor.EditorStyles.miniButtonLeft,
						GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
					{
						value -= step;
						if (value < min) value = min;
					}

					if (GUILayout.Button(XString.TriangleUp, UnityEditor.EditorStyles.miniButtonMid,
						GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
					{
						value += step;
						if (value > max) value = max;
					}

					if (GUILayout.Button("Rand", UnityEditor.EditorStyles.miniButtonRight))
					{
						value = UnityEngine.Random.Range(min, max);
						if (value < min) value = min;
						if (value > max) value = max;
					}
				}
				UnityEditor.EditorGUILayout.EndHorizontal();
				return (value);
			}

			
			/// <summary>
			/// Рисовании строкового свойства
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			
			public static void PropertyLabel(String name, String value)
			{
				Content.text = name;
				ContentLabel.text = value;
				UnityEditor.EditorGUILayout.LabelField(Content, ContentLabel, GUILayout.ExpandWidth(true));
			}

			
			/// <summary>
			/// Редактор свойства типа String
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static String PropertyString(String name, String value)
			{
				if (value == null)
				{
					value = "";
				}

				Content.text = name;
				value = UnityEditor.EditorGUILayout.TextField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Enum
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static TEnum PropertyEnum<TEnum>(String name, TEnum value) where TEnum : Enum
			{
				Content.text = name;
				value = (TEnum)UnityEditor.EditorGUILayout.EnumPopup(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Enum (для установки флагов)
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Enum PropertyFlags(String name, Enum value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.EnumFlagsField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Vector2
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Vector2 PropertyVector2D(String name, Vector2 value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.Vector2Field(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Vector3
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Vector3 PropertyVector3D(String name, Vector3 value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.Vector3Field(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Vector4
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Vector4 PropertyVector4D(String name, Vector4 value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.Vector4Field(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Vector4 представляемого как бордюры в формате данных спрайта
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="data">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Vector4 PropertyBorderSprite(String name, Vector4 data)
			{
				Content.text = name;
				UnityEditor.EditorGUILayout.BeginHorizontal();
				{
					UnityEditor.EditorGUILayout.PrefixLabel(Content, UnityEditor.EditorStyles.label);
					data.x = UnityEditor.EditorGUILayout.FloatField(data.x, GUILayout.MaxWidth(60));
					data.w = UnityEditor.EditorGUILayout.FloatField(data.w, GUILayout.MaxWidth(60));
					data.z = UnityEditor.EditorGUILayout.FloatField(data.z, GUILayout.MaxWidth(60));
					data.y = UnityEditor.EditorGUILayout.FloatField(data.y, GUILayout.MaxWidth(60));
				}
				UnityEditor.EditorGUILayout.EndHorizontal();

				return (data);
			}

			
			/// <summary>
			/// Редактор свойства типа Vector4 представляемого как бордюры в стандартном формате
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="data">Значение свойства</param>
			/// <returns>Отредактированное значение</returns>
			
			public static Vector4 PropertyBorderNormal(String name, Vector4 data)
			{
				Content.text = name;
				UnityEditor.EditorGUILayout.BeginHorizontal();
				{
					UnityEditor.EditorGUILayout.PrefixLabel(Content, UnityEditor.EditorStyles.label);
					data.x = UnityEditor.EditorGUILayout.FloatField(data.x, GUILayout.MaxWidth(60));
					data.y = UnityEditor.EditorGUILayout.FloatField(data.y, GUILayout.MaxWidth(60));
					data.z = UnityEditor.EditorGUILayout.FloatField(data.z, GUILayout.MaxWidth(60));
					data.w = UnityEditor.EditorGUILayout.FloatField(data.w, GUILayout.MaxWidth(60));
				}
				UnityEditor.EditorGUILayout.EndHorizontal();

				return (data);
			}

			
			/// <summary>
			/// Редактор свойства типа Color
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Color PropertyColor(String name, Color value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.ColorField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Rect
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static Rect PropertyRect(String name, Rect value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.RectField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа RectOffset
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static RectOffset PropertyRectOffset(String name, RectOffset value)
			{
				Content.text = name;
				UnityEditor.EditorGUILayout.BeginHorizontal();
				{
					UnityEditor.EditorGUILayout.PrefixLabel(Content, UnityEditor.EditorStyles.label);
					value.left = UnityEditor.EditorGUILayout.IntField(value.left, GUILayout.MaxWidth(60));
					value.top = UnityEditor.EditorGUILayout.IntField(value.top, GUILayout.MaxWidth(60));
					value.right = UnityEditor.EditorGUILayout.IntField(value.right, GUILayout.MaxWidth(60));
					value.bottom = UnityEditor.EditorGUILayout.IntField(value.bottom, GUILayout.MaxWidth(60));
				}
				UnityEditor.EditorGUILayout.EndHorizontal();

				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа AnimationCurve
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static AnimationCurve PropertyCurve(String name, AnimationCurve value)
			{
				Content.text = name;
				value = UnityEditor.EditorGUILayout.CurveField(Content, value);
				return (value);
			}

			
			/// <summary>
			/// Редактор свойства типа Component
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static TComponent PropertyComponent<TComponent>(String name, TComponent value) where TComponent : UnityEngine.Component
			{
				Content.text = name;
				var result = UnityEditor.EditorGUILayout.ObjectField(Content, value, typeof(TComponent), true) as TComponent;
				return (result);
			}

			
			/// <summary>
			/// Редактор свойства типа GameObject
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static GameObject PropertyGameObject(String name, GameObject value)
			{
				Content.text = name;
				var result = UnityEditor.EditorGUILayout.ObjectField(Content, value, typeof(GameObject), true) as GameObject;
				return (result);
			}

			
			/// <summary>
			/// Редактор свойства типа UnityEngine.Object (для ресурсов)
			/// </summary>
			/// <param name="name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns>Измененное значение свойства</returns>
			
			public static TResource PropertyResource<TResource>(String name, TResource value) where TResource : UnityEngine.Object
			{
				Content.text = name;
				var result = UnityEditor.EditorGUILayout.ObjectField(Content, value, typeof(TResource), false,
					GUILayout.Height(UnityEditor.EditorGUIUtility.singleLineHeight)) as TResource;
				return (result);
			}
			#endregion

			#region ======================================= ЭЛЕМЕНТЫ ДЕКОРАЦИИ ========================================
			
			/// <summary>
			/// Получение и отображение атрибутов декорации
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			
			public static void DrawDecorateAttributes(UnityEditor.SerializedProperty property)
			{
				CCachedMember cached_data = XEditorCachedData.GetMember(property);
				DrawDecorateAttributes(cached_data);
			}

			
			/// <summary>
			/// Получение и отображение атрибутов декорации
			/// </summary>
			/// <param name="member_info">Метаданные члена данных</param>
			
			public static void DrawDecorateAttributes(MemberInfo member_info)
			{
				CCachedMember cached_data = XEditorCachedData.GetMember(member_info);
				DrawDecorateAttributes(cached_data);
			}

	----
			/// <summary>
			/// Получение и отображение атрибутов декорации
			/// </summary>
			/// <param name="cached_data">Кэшированные данные</param>
	----
			public static void DrawDecorateAttributes(CCachedMember cached_data)
			{
				// Space
				SpaceAttribute space_attribute = cached_data.GetAttribute<SpaceAttribute>();
				if (space_attribute != null)
				{
					GUILayout.Space(space_attribute.height);
				}

				// Header
				HeaderAttribute header_attribute = cached_data.GetAttribute<HeaderAttribute>();
				if (header_attribute != null)
				{
					GUILayout.Label(header_attribute.header, UnityEditor.EditorStyles.boldLabel);
				}

				// HeaderSection
				LotusHeaderSectionAttribute header_section_attribute = cached_data.GetAttribute<LotusHeaderSectionAttribute>();
				if (header_section_attribute != null)
				{
					XEditorInspector.DrawSection(header_section_attribute.Name, header_section_attribute.TextColor,
						XEnum.ToEnum<TextAnchor>(header_section_attribute.TextAlignment));
				}

				// HeaderSectionBox
				LotusHeaderSectionBoxAttribute header_section_box_attribute = cached_data.GetAttribute<LotusHeaderSectionBoxAttribute>();
				if (header_section_box_attribute != null)
				{
					XEditorInspector.DrawSectionBox(header_section_box_attribute.Name, header_section_box_attribute.TextColor,
						XEnum.ToEnum<TextAnchor>(header_section_box_attribute.TextAlignment));
				}

				// HeaderGroup
				LotusHeaderGroupAttribute header_group_attribute = cached_data.GetAttribute<LotusHeaderGroupAttribute>();
				if (header_group_attribute != null)
				{
					if (header_group_attribute.TextColor == TColor.Zero)
					{
						header_group_attribute.TextColor = new TColor(XEditorStyles.ColorHeaderGroup);
					}

					XEditorInspector.DrawGroup(header_group_attribute.Name,
						header_group_attribute.TextColor,
						header_group_attribute.Indent,
						XEnum.ToEnum<TextAnchor>(header_group_attribute.TextAlignment));
				}

				// HeaderGroup
				LotusHeaderGroupBoxAttribute header_group_box_attribute = cached_data.GetAttribute<LotusHeaderGroupBoxAttribute>();
				if (header_group_box_attribute != null)
				{
					if (header_group_box_attribute.TextColor == TColor.Zero)
					{
						header_group_box_attribute.TextColor = new TColor(XEditorStyles.ColorHeaderGroup);
					}

					XEditorInspector.DrawGroupBox(header_group_box_attribute.Name,
						header_group_box_attribute.TextColor,
						header_group_box_attribute.Indent,
						XEnum.ToEnum<TextAnchor>(header_group_box_attribute.TextAlignment));
				}
			}
			#endregion
		}

		/**@}*/

	}
}
