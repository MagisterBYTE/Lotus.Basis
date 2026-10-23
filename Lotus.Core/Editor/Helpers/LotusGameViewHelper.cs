
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusGameViewHelper.cs
*		Получение и управление параметрами окна игры редактора.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;

namespace Lotus
{
	namespace Editor
	{

		/** \addtogroup CoreUnityEditor
		*@{*/

		/// <summary>
		/// Тип изменения размеров экрана
		/// </summary>

		public enum TGameViewSizeType
		{
			/// <summary>
			/// Соотношение сторон
			/// </summary>
			AspectRatio,

			/// <summary>
			/// Фиксированное разрешение
			/// </summary>
			FixedResolution
		}


		/// <summary>
		/// Статический класс реализующий методы для получение и управления параметрами окна игры редактора
		/// </summary>
		/// <remarks>
		/// Только для режима редактора Unity
		/// </remarks>

		public static class XEditorGameView
		{
			#region Fields
			/// <summary>
			/// Тип окна игры редактора
			/// </summary>
			private static Type mTypeGameView;

			/// <summary>
			/// Экземпляр окна игры редактора
			/// </summary>
			private static System.Object mGameViewWindow;

			/// <summary>
			/// Метод получения размеров окна игры редактора (Статический метод)
			/// </summary>
			private static MethodInfo mMethodGetSizeOfMainGameView;

			/// <summary>
			/// Свойство для получения текущих параметров размера окна игры редактора
			/// </summary>
			private static PropertyInfo mPropertyСurrentGameViewSize;

			/// <summary>
			/// Текущие параметры размера окна игры редактора
			/// </summary>
			private static System.Object mСurrentGameViewSize;

			/// <summary>
			/// Свойство для получения текущей группы размеров
			/// </summary>
			private static PropertyInfo mPropertyCurrentSizeGroupType;

			/// <summary>
			/// Текущие группа размеров
			/// </summary>
			private static System.Object mCurrentSizeGroupType;

			/// <summary>
			/// Тип объект содержащий списки всех групп размеров
			/// </summary>
			private static Type mTypeGameViewSizes;

			/// <summary>
			/// Объект содержащий списки всех групп размеров
			/// </summary>
			private static System.Object mGameViewSizes;

			/// <summary>
			/// Метод для получения соответствующий группы размеров
			/// </summary>
			private static MethodInfo mMethodGetGroup;
			#endregion

			#region Properties
			/// <summary>
			/// Ширина окна игры редактора
			/// </summary>
			public static Int32 Width
			{
				get
				{
					Init();
					var res = (Vector2)mMethodGetSizeOfMainGameView.Invoke(null, null);
					return ((Int32)res.x);
				}
			}

			/// <summary>
			/// Высота окна игры редактора
			/// </summary>
			public static Int32 Height
			{
				get
				{
					Init();
					var res = (Vector2)mMethodGetSizeOfMainGameView.Invoke(null, null);
					return ((Int32)res.y);
				}
			}

			/// <summary>
			/// Описание размера
			/// </summary>
			public static String Name
			{
				get
				{
					Init();

					var base_text_prop = mСurrentGameViewSize.GetType().GetProperty("baseText",
						BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

					var base_text = base_text_prop.GetValue(mСurrentGameViewSize);

					return (base_text.ToString());
				}
			}

			/// <summary>
			/// Тип изменения размеров экрана
			/// </summary>
			public static TGameViewSizeType SizeType
			{
				get
				{
					Init();
					var size_type_prop = mСurrentGameViewSize.GetType().GetProperty("sizeType",
						BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

					var size_type = size_type_prop.GetValue(mСurrentGameViewSize);

					var value = size_type.ToString();
					if(value.IndexOf("Aspect") > -1)
					{
						return (TGameViewSizeType.AspectRatio);
					}
					else
					{
						return (TGameViewSizeType.FixedResolution);
					}
				}
			}

			/// <summary>
			/// Текущая группа размеров
			/// </summary>
			public static GameViewSizeGroupType SizeGroupType
			{
				get
				{
					Init();
					return ((GameViewSizeGroupType)mCurrentSizeGroupType);
				}
			}
			#endregion

			#region Main methods
			
			/// <summary>
			/// Конвертация в тип GameViewSizeGroupType
			/// </summary>
			/// <param name="build_target">Целевая платформа сборки</param>
			/// <returns>Объект GameViewSizeGroupType</returns>
			
			public static GameViewSizeGroupType FromBuildTarget(BuildTarget build_target)
			{
				GameViewSizeGroupType game_view = GameViewSizeGroupType.Standalone;

				switch (build_target)
				{
					case BuildTarget.StandaloneOSX:
						break;
					case BuildTarget.StandaloneWindows:
						break;
					case BuildTarget.iOS:
						{
							game_view = GameViewSizeGroupType.iOS;
						}
						break;
					case BuildTarget.Android:
						{
							game_view = GameViewSizeGroupType.Android;
						}
						break;
					case BuildTarget.StandaloneWindows64:
						break;
					case BuildTarget.WebGL:
						break;
					case BuildTarget.WSAPlayer:
						break;
					case BuildTarget.StandaloneLinux64:
						break;
					case BuildTarget.PS4:
						break;
					case BuildTarget.XboxOne:
						break;
					case BuildTarget.tvOS:
						break;
					case BuildTarget.Switch:
						break;
					case BuildTarget.Lumin:
						break;
					case BuildTarget.Stadia:
						break;
					case BuildTarget.LinuxHeadlessSimulation:
						break;
					case BuildTarget.GameCoreXboxOne:
						break;
					case BuildTarget.PS5:
						break;
					case BuildTarget.EmbeddedLinux:
						break;
					case BuildTarget.NoTarget:
						break;
					default:
						break;
				}

				return game_view;

			}

			
			/// <summary>
			/// Инициализация данных
			/// </summary>
			
			private static void Init()
			{
				if (mTypeGameView == null)
				{
					mTypeGameView = Type.GetType("UnityEditor.GameView,UnityEditor");
				}

				if (mGameViewWindow == null)
				{
					mGameViewWindow = UnityEditor.EditorWindow.GetWindow(mTypeGameView, false, null, false);
				}

				if (mMethodGetSizeOfMainGameView == null)
				{
					mMethodGetSizeOfMainGameView = mTypeGameView.GetMethod("GetSizeOfMainGameView", BindingFlags.NonPublic | BindingFlags.Static);
				}

				if(mPropertyСurrentGameViewSize == null)
				{
					mPropertyСurrentGameViewSize = mTypeGameView.GetProperty("currentGameViewSize",
						BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				}

				mСurrentGameViewSize = mPropertyСurrentGameViewSize.GetValue(mGameViewWindow);

				if (mPropertyCurrentSizeGroupType == null)
				{
					mPropertyCurrentSizeGroupType = mTypeGameView.GetProperty("currentSizeGroupType",
						BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}

				mCurrentSizeGroupType = mPropertyCurrentSizeGroupType.GetValue(null);

				if(mTypeGameViewSizes == null)
				{
					var sizes_type = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSizes");
					mTypeGameViewSizes = typeof(ScriptableSingleton<>).MakeGenericType(sizes_type);
				}

				var instance_prop = mTypeGameViewSizes.GetProperty("instance");
				mGameViewSizes = instance_prop.GetValue(null, null);

				if(mMethodGetGroup == null)
				{
					mMethodGetGroup = mGameViewSizes.GetType().GetMethod("GetGroup",
						BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				}
			}

			
			/// <summary>
			/// Получение группы размеров
			/// </summary>
			/// <remarks>
			/// Метод возвращает список размеров для соответствующей группы
			/// </remarks>
			/// <param name="build_target">Тип группы размеров</param>
			/// <returns>Группа размеров</returns>
			
			static System.Object GetGroup(BuildTarget build_target)
			{
				Init();
				return mMethodGetGroup.Invoke(mGameViewSizes, new System.Object[] { (Int32)FromBuildTarget(build_target) });
			}

			
			/// <summary>
			/// Проверка на существование указанного размера для указанной типа группы размеров
			/// </summary>
			/// <param name="build_target">Тип группы размеров</param>
			/// <param name="text">Описание размера</param>
			/// <returns>Статус проверки</returns>
			
			public static Boolean SizeExists(BuildTarget build_target, String text)
			{
				Init();
				return (FindSize(build_target, text) != -1);
			}

			
			/// <summary>
			/// Проверка на существование указанного размера для указанной типа группы размеров
			/// </summary>
			/// <param name="build_target">Тип группы размеров</param>
			/// <param name="width">Ширина</param>
			/// <param name="height">Высота</param>
			/// <returns>Статус проверки</returns>
			
			public static Boolean SizeExists(BuildTarget build_target, Int32 width, Int32 height)
			{
				Init();
				return (FindSize(build_target, width, height) != -1);
			}

			
			/// <summary>
			/// Поиск индекса размера для указанной типа группы размеров
			/// </summary>
			/// <param name="build_target">Тип группы размеров</param>
			/// <param name="text">Описание размера</param>
			/// <returns>Индекс размера</returns>
			
			public static Int32 FindSize(BuildTarget build_target, String text)
			{
				Init();
				// GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupType);
				// string[] texts = group.GetDisplayTexts();
				// for loop...

				var group = GetGroup(build_target);
				var get_display_texts = group.GetType().GetMethod("GetDisplayTexts");
				var display_texts = get_display_texts.Invoke(group, null) as String[];
				for (var i = 0; i < display_texts.Length; i++)
				{
					var display = display_texts[i];
					// the text we get is "Name (W:H)" if the size has a name, or just "W:H" e.g. 16:9
					// so if we're querying a custom size text we substring to only get the name
					// You could see the outputs by just logging
					// Debug.Log(display);
					var pren = display.IndexOf('(');
					if (pren != -1)
					{
						display = display.Substring(0, pren - 1); // -1 to remove the space that's before the prens. This is very implementation-depdenent
					}
					if (display == text)
					{
						return i;
					}
				}
				return (-1);
			}

			
			/// <summary>
			/// Поиск индекса размера  для указанной типа группы размеров
			/// </summary>
			/// <param name="build_target">Тип группы размеров</param>
			/// <param name="width">Ширина</param>
			/// <param name="height">Высота</param>
			/// <returns>Индекс размера</returns>
			
			public static Int32 FindSize(BuildTarget build_target, Int32 width, Int32 height)
			{
				Init();
				// goal:
				// GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupType);
				// Int32 sizesCount = group.GetBuiltinCount() + group.GetCustomCount();
				// iterate through the sizes via group.GetGameViewSize(Int32 index)

				var group = GetGroup(build_target);
				var group_type = group.GetType();
				var get_builtin_count = group_type.GetMethod("GetBuiltinCount");
				var get_custom_count = group_type.GetMethod("GetCustomCount");
				var sizes_count = (Int32)get_builtin_count.Invoke(group, null) + (Int32)get_custom_count.Invoke(group, null);
				var get_game_view_size = group_type.GetMethod("GetGameViewSize");
				var gvs_type = get_game_view_size.ReturnType;
				var width_prop = gvs_type.GetProperty("width");
				var height_prop = gvs_type.GetProperty("height");
				var index_value = new System.Object[1];
				for (var i = 0; i < sizes_count; i++)
				{
					index_value[0] = i;
					var size = get_game_view_size.Invoke(group, index_value);
					var sizeWidth = (Int32)width_prop.GetValue(size, null);
					var sizeHeight = (Int32)height_prop.GetValue(size, null);
					if (sizeWidth == width && sizeHeight == height)
						return i;
				}
				return -1;
			}

			
			/// <summary>
			/// Установка размера по индексу
			/// </summary>
			/// <param name="index">Индекс размера</param>
			
			public static void SetSize(Int32 index)
			{
				Init();
				var selected_size_index_prop = mTypeGameView.GetProperty("selectedSizeIndex",
						BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				selected_size_index_prop.SetValue(mGameViewWindow, index, null);
			}

			
			/// <summary>
			/// Добавление произвольного размера
			/// </summary>
			/// <param name="size_type">Тип изменения размеров экрана</param>
			/// <param name="build_target">Тип группы размеров</param>
			/// <param name="width">Ширина</param>
			/// <param name="height">Высота</param>
			/// <param name="text">Описание</param>
			
			public static void AddCustomSize(TGameViewSizeType size_type, BuildTarget build_target, 
				Int32 width, Int32 height, String text)
			{
				Init();
				// GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupTyge);
				// group.AddCustomSize(new GameViewSize(viewSizeType, width, height, text);

				var group = GetGroup(build_target);

				var add_сustom_size = mMethodGetGroup.ReturnType.GetMethod("AddCustomSize"); // or group.GetType().

				var gvs_type = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSize");

				Type view_size_type = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.GameViewSizeType");

				var ctor = gvs_type.GetConstructor(new Type[] { view_size_type, typeof(Int32), typeof(Int32), typeof(String) });

				var new_size = ctor.Invoke(new System.Object[] { (Int32)size_type, width, height, text });

				add_сustom_size.Invoke(group, new System.Object[] { new_size });
			}

			
			/// <summary>
			/// Применить указанные параметры к окну игры редактора
			/// </summary>
			/// <remarks>
			/// Если указанные параметры будут найдены то они буду активированы, 
			/// в противном случае добавиться размер по указанным параметрам
			/// </remarks>
			/// <param name="size_type">Тип изменения размеров экрана</param>
			/// <param name="build_target">Тип группы размеров</param>
			/// <param name="width">Ширина</param>
			/// <param name="height">Высота</param>
			/// <param name="text">Описание</param>
			
			public static void ApplyCustomSize(TGameViewSizeType size_type, BuildTarget build_target,
				Int32 width, Int32 height, String text)
			{
				// Смотрим есть такие размеры
				var index = FindSize(build_target, width, height);
				if (index == -1)
				{
					XEditorGameView.AddCustomSize(size_type, build_target, width,
						height, text);

					index = XEditorGameView.FindSize(build_target, width, height);

					XEditorGameView.SetSize(index);
				}
				else
				{
					XEditorGameView.SetSize(index);
				}
			}
			#endregion
		}

		/**@}*/


	}
}
