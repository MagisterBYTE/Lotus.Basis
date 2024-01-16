//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема текстур
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObject3DTexture.cs
*		Определение типов и параметров изображения представляющего собой текстуру.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
//---------------------------------------------------------------------------------------------------------------------
#if USE_WINDOWS
using System.Windows;
using System.Windows.Media;
using Media3D = System.Windows.Media.Media3D;
#endif
//---------------------------------------------------------------------------------------------------------------------
#if USE_HELIX
using HelixToolkit.Wpf;
using Helix3D = HelixToolkit.Wpf.SharpDX;
#endif
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Object3D
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup Object3DTexture Подсистема текстур
         * \ingroup Object3D
         * \brief Подсистема текстур определяет данные текстуры и праметров ее наложения.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Назначение текстуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TTextureDestination
		{
			/// <summary>
			/// Нет особого(конкретного) предназначения текстуры
			/// </summary>
			None,

			/// <summary>
			/// Основная текстура
			/// </summary>
			Diffuse,

			/// <summary>
			/// Тексутра для формирования бликов
			/// </summary>
			Specular,

			/// <summary>
			/// Текстура для представления подсветки
			/// </summary>
			Ambient,

			/// <summary>
			/// Текстура для определения свечения объекта
			/// </summary>
			/// <remarks>
			/// Излучающая текстура, добавляемая к результату расчета освещения. 
			/// На нее не влияет падающий свет, вместо этого она представляет свет, который объект излучает естественным образом
			/// </remarks>
			Emissive,

			/// <summary>
			/// Текстура представляющая собой карту нормалей касательного пространства
			/// </summary>
			Normals,

			/// <summary>
			/// Текстура представляющее собой некое смещение.
			/// </summary>
			/// <remarks>
			/// Точное назначение и формат зависят от приложения. Более высокие значения цвета означают более высокие смещения вершин.
			/// </remarks>
			Displacement,

			/// <summary>
			/// Текстура карты высот
			/// </summary>
			/// <remarks>
			/// По соглашению, более высокие значения оттенков серого означают более высокие отметки от некоторой базовой высоты
			/// </remarks>
			Height,

			/// <summary>
			/// Текстура, определяющая глянцевитость материала
			/// </summary>
			/// <remarks>
			/// Это показатель уравнения зеркального (фонгового) освещения. 
			/// Обычно существует функция преобразования, определенная для сопоставления линейных значений цвета 
			/// в текстуре с подходящей экспонентой.
			/// </remarks>
			Shininess
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Формат цвета изображения предстающего собой текстуру
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TTextureFormatColor
		{
			/// <summary>
			/// Alpha-only texture format, 8 bit integer
			/// </summary>
			Alpha8,

			/// <summary>
			/// Color texture format, 8-bits per channel
			/// </summary>
			RGB24,

			/// <summary>
			/// Color with alpha texture format, 8-bits per channel
			/// </summary>
			RGBA32,

			/// <summary>
			/// Color with alpha texture format, 8-bits per channel
			/// </summary>
			ARGB32,

			/// <summary>
			/// Color with alpha texture format, 8-bits per channel
			/// </summary>
			RGB565,
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий  параметры изображения представляющего текстуру и отдельные параметры наложения текстуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CTexture : CEntity3D
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static readonly PropertyChangedEventArgs PropertyArgsFileName = new PropertyChangedEventArgs(nameof(FileName));
			protected static readonly PropertyChangedEventArgs PropertyArgsDestination = new PropertyChangedEventArgs(nameof(Destination));
			protected static readonly PropertyChangedEventArgs PropertyArgsWidth = new PropertyChangedEventArgs(nameof(Width));
			protected static readonly PropertyChangedEventArgs PropertyArgsHeight = new PropertyChangedEventArgs(nameof(Height));
			protected static readonly PropertyChangedEventArgs PropertyArgsAlphaIsTransparency = new PropertyChangedEventArgs(nameof(AlphaIsTransparency));
			protected static readonly PropertyChangedEventArgs PropertyArgsFormatColor = new PropertyChangedEventArgs(nameof(FormatColor));
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка тексутры в память по полному пути
			/// </summary>
			/// <param name="fileName">Имя файла</param>
			/// <returns>Объект <see cref="MemoryStream"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public static MemoryStream LoadTextureToMemory(String fileName)
			{
				using (var file = new FileStream(fileName, FileMode.Open))
				{
					var memory = new MemoryStream();
					file.CopyTo(memory);
					return memory;
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String? _fileName;
			protected internal TTextureDestination _destination;
			protected internal Int32 _width;
			protected internal Int32 _height;
			protected internal Boolean _alphaIsTransparency;
			protected internal TTextureFormatColor _formatColor;
			protected internal CMaterial? _ownerMaterial;

#if UNITY_2017_1_OR_NEWER
			internal UnityEngine.Texture2D _unityTexture;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Имя файла текстуры
			/// </summary>
			public String? FileName
			{
				get
				{
					return _fileName;
				}
				set
				{
					_fileName = value;
					RaiseFileNameChanged();
					NotifyPropertyChanged(PropertyArgsFileName);
				}
			}

			/// <summary>
			/// Назначение текстуры
			/// </summary>
			public TTextureDestination Destination
			{
				get
				{
					return _destination;
				}
				set
				{
					_destination = value;
					RaiseDestinationChanged();
					NotifyPropertyChanged(PropertyArgsDestination);
				}
			}

			/// <summary>
			/// Ширина текстуры
			/// </summary>
			public Int32 Width
			{
				get
				{
					return _width;
				}
				set
				{
					_width = value;
					RaiseWidthChanged();
					NotifyPropertyChanged(PropertyArgsWidth);
				}
			}

			/// <summary>
			/// Высота текстуры
			/// </summary>
			public Int32 Height
			{
				get
				{
					return _height;
				}
				set
				{
					_height = value;
					RaiseHeightChanged();
					NotifyPropertyChanged(PropertyArgsHeight);
				}
			}

			/// <summary>
			/// Альфа канал текстуру определяет ее прозрачность
			/// </summary>
			public Boolean AlphaIsTransparency
			{
				get
				{
					return _alphaIsTransparency;
				}
				set
				{
					_alphaIsTransparency = value;
					RaiseAlphaIsTransparencyChanged();
					NotifyPropertyChanged(PropertyArgsAlphaIsTransparency);
				}
			}

			/// <summary>
			/// Формат цвета изображения предстающего собой текстуру
			/// </summary>
			public TTextureFormatColor FormatColor
			{
				get
				{
					return _formatColor;
				}
				set
				{
					_formatColor = value;
					RaiseFormatColorChanged();
					NotifyPropertyChanged(PropertyArgsFormatColor);
				}
			}

			/// <summary>
			/// Владелец материал
			/// </summary>
			public CMaterial? OwnerMaterial
			{
				get { return _ownerMaterial; }
				set
				{
					_ownerMaterial = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTexture()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="ownerMaterial">Материал</param>
			//---------------------------------------------------------------------------------------------------------
			public CTexture(CMaterial ownerMaterial)
			{
				_ownerMaterial = ownerMaterial;
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_material">Материал</param>
			/// <param name="unity_texture">Двухмерная текстура Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public CTexture(CMaterial owner_material, UnityEngine.Texture2D unity_texture)
				:this(owner_material)
			{
				if(unity_texture != null)
				{
					_name = unity_texture.name;
					_unityTexture = unity_texture;
				}
			}
#endif
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение имени текстуры.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseFileNameChanged()
			{
#if USE_WINDOWS

#endif
#if UNITY_2017_1_OR_NEWER

#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение назначение текстуры.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseDestinationChanged()
			{
#if USE_WINDOWS

#endif
#if UNITY_2017_1_OR_NEWER

#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение ширины текстуры.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseWidthChanged()
			{
#if USE_WINDOWS

#endif
#if UNITY_2017_1_OR_NEWER

#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение высоты текстуры.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseHeightChanged()
			{
#if USE_WINDOWS

#endif
#if UNITY_2017_1_OR_NEWER

#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса использования aльфа каналы текстуры как её прозрачности.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseAlphaIsTransparencyChanged()
			{
#if USE_WINDOWS

#endif
#if UNITY_2017_1_OR_NEWER

#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение формата цвета изображения.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseFormatColorChanged()
			{
#if USE_WINDOWS

#endif
#if UNITY_2017_1_OR_NEWER

#endif
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Набор всех текстур в сцене
		/// </summary>
		/// <remarks>
		/// Предназначен для логического группирования всех текстур в обозревателе сцены
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CTextureSet : CEntity3D
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal ListArray<CTexture> _textures;
			internal CScene3D _ownerScene;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наблюдаемая коллекция материал
			/// </summary>
			public ListArray<CTexture> Textures
			{
				get { return _textures; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="ownerScene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextureSet(CScene3D ownerScene)
			{
				_ownerScene = ownerScene;
				_name = "Тексутры";
				_textures = new ListArray<CTexture>
				{
					IsNotify = true
				};
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewModelBuilder =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количества дочерних узлов
			/// </summary>
			/// <returns>Количество дочерних узлов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetCountChildrenNode()
			{
				return _textures.Count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дочернего узла по индексу
			/// </summary>
			/// <param name="index">Индекс дочернего узла</param>
			/// <returns>Дочерней узел</returns>
			//---------------------------------------------------------------------------------------------------------
			public override System.Object GetChildrenNode(Int32 index)
			{
				return _textures[index];
			}
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ HELIX ====================================
#if USE_HELIX
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================