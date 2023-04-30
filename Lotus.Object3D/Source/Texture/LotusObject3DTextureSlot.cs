//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема текстур
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObject3DTextureSlot.cs
*		Определение типа текстурного слота определяющего параметры наложения текстуры.
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
		/** \addtogroup Object3DTexture
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определяет, как генерируются координаты текстуры
		/// <para>
		/// Приложениям реального времени обычно требуются полные UV-координаты. Поэтому настоятельно рекомендуется
		/// использовать шаг <see cref = "Assimp.PostProcessSteps.GenerateUVCoords" />. Он генерирует правильные UV-каналы для
		/// объектов, не относящихся к UV, при условии, что дано точное описание того, как должно выглядеть отображение.
		/// </para>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TTextureMapping
		{
			/// <summary>
			/// Coordinates are taken from the an existing UV channel.
			/// <para>
			/// The AI_MATKEY_UVWSRC key specifies from the UV channel the texture coordinates
			/// are to be taken from since meshes can have more than one UV channel.
			/// </para>
			/// </summary>
			FromUV = 0x0,

			/// <summary>
			/// Spherical mapping
			/// </summary>
			Sphere = 0x1,

			/// <summary>
			/// Cylinder mapping
			/// </summary>
			Cylinder = 0x2,

			/// <summary>
			/// Cubic mapping
			/// </summary>
			Box = 0x3,

			/// <summary>
			/// Planar mapping
			/// </summary>
			Plane = 0x4,

			/// <summary>
			/// Unknown mapping that is not recognied.
			/// </summary>
			Unknown = 0x5
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Defines how the Nth texture of a specific type is combined
		/// with the result of all previous layers.
		/// <para>
		/// Example (left: key, right: value):
		/// <code>
		/// DiffColor0     - gray
		/// DiffTextureOp0 - TextureOperation.Multiply
		/// DiffTexture0   - tex1.png
		/// DiffTextureOp0 - TextureOperation.Add
		/// DiffTexture1   - tex2.png
		/// </code>
		/// <para>
		/// Written as an equation, the final diffuse term for a specific
		/// pixel would be:
		/// </para>
		/// <code>
		/// diffFinal = DiffColor0 * sampleTex(DiffTexture0, UV0) + sampleTex(DiffTexture1, UV0) * diffContrib;
		/// </code>
		/// </para>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TTextureOperation
		{
			/// <summary>
			/// T = T1 * T2
			/// </summary>
			Multiply = 0x0,

			/// <summary>
			/// T = T1 + T2
			/// </summary>
			Add = 0x1,

			/// <summary>
			/// T = T1 - T2
			/// </summary>
			Subtract = 0x2,

			/// <summary>
			/// T = T1 / T2
			/// </summary>
			Divide = 0x3,

			/// <summary>
			/// T = (T1 + T2) - (T1 * T2)
			/// </summary>
			SmoothAdd = 0x4,

			/// <summary>
			/// T = T1 + (T2 - 0.5)
			/// </summary>
			SignedAdd = 0x5
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Defines how UV coordinates outside the [0..1] range are handled. Commonly
		/// referred to as the 'wrapping mode'
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TTextureWrapMode
		{
			/// <summary>
			/// A texture coordinate u|v is translated to u % 1| v % 1.
			/// </summary>
			Wrap = 0x0,

			/// <summary>
			/// Texture coordinates outside [0...1] are clamped to the nearest valid value.
			/// </summary>
			Clamp = 0x1,

			/// <summary>
			/// A texture coordinate u|v becomes u1|v1 if (u - (u % 1)) % 2 is zero
			/// and 1 - (u % 1) | 1 - (v % 1) otherwise.
			/// </summary>
			Mirror = 0x2,

			/// <summary>
			/// If the texture coordinates for a pixel are outside [0...1] the texture is not
			/// applied to that pixel.
			/// </summary>
			Decal = 0x3,
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Текстурный слот для определения параметров текстурного наложения 
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CTextureSlot : CEntity3D
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal CTexture mTexture;
			protected internal TTextureMapping mMapping;
			protected internal Int32 mUVIndex;
			protected internal Single mBlendFactor;
			protected internal TTextureOperation mOperation;
			protected internal TTextureWrapMode mWrapModeU;
			protected internal TTextureWrapMode mWrapModeV;
			protected internal CMaterial mOwnerMaterial;

			// Платформенно-зависимая часть
#if USE_ASSIMP
			internal Assimp.TextureSlot mAssimpTextureSlot;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Текстура связанная с данным текстурным слотом
			/// </summary>
			[Browsable(false)]
			public CTexture Texture
			{
				get { return (mTexture); }
				set
				{
					mTexture = value;
				}
			}

			/// <summary>
			/// Режим наложения и формирования текстурных координат
			/// </summary>
			[DisplayName("Mapping")]
			[Description("Режим наложения и формирования текстурных координат")]
			[Category(XInspectorGroupDesc.Params)]
			public TTextureMapping Mapping
			{
				get { return (mMapping); }
				set
				{
					mMapping = value;
				}
			}

			/// <summary>
			/// Индекс текстурных координат
			/// </summary>
			[DisplayName("UVIndex")]
			[Description("Индекс текстурных координат")]
			[Category(XInspectorGroupDesc.Params)]
			public Int32 UVIndex
			{
				get { return (mUVIndex); }
				set
				{
					mUVIndex = value;
				}
			}

			/// <summary>
			/// Фактор смешивания
			/// </summary>
			[DisplayName("BlendFactor")]
			[Description("Фактор смешивания")]
			[Category(XInspectorGroupDesc.Params)]
			public Single BlendFactor
			{
				get { return (mBlendFactor); }
				set
				{
					mBlendFactor = value;
				}
			}

			/// <summary>
			/// Режим смешивания текстур
			/// </summary>
			[DisplayName("Operation")]
			[Description("Режим смешивания текстур")]
			[Category(XInspectorGroupDesc.Params)]
			public TTextureOperation Operation
			{
				get { return (mOperation); }
				set
				{
					mOperation = value;
				}
			}

			/// <summary>
			/// Режим обвёртки текстурных координат по U - координате
			/// </summary>
			[DisplayName("WrapModeU")]
			[Description("Режим обвёртки текстурных координат по U - координате")]
			[Category(XInspectorGroupDesc.Params)]
			public TTextureWrapMode WrapModeU
			{
				get { return (mWrapModeU); }
				set
				{
					mWrapModeU = value;
				}
			}

			/// <summary>
			/// Режим обвёртки текстурных координат по V - координате
			/// </summary>
			[DisplayName("WrapModeU")]
			[Description("Режим обвёртки текстурных координат по V - координате")]
			[Category(XInspectorGroupDesc.Params)]
			public TTextureWrapMode WrapModeV
			{
				get { return (mWrapModeV); }
				set
				{
					mWrapModeV = value;
				}
			}

			/// <summary>
			/// Владелец материал
			/// </summary>
			[Browsable(false)]
			public CMaterial OwnerMaterial
			{
				get { return (mOwnerMaterial); }
				set
				{
					mOwnerMaterial = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTextureSlot()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_material">Материал</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextureSlot(CMaterial owner_material)
			{
				mOwnerMaterial = owner_material;
			}

#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="assimp_texture_slot">Текстурный слот</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextureSlot(Assimp.TextureSlot assimp_texture_slot)
			{
				mAssimpTextureSlot = assimp_texture_slot;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="material">Материал</param>
			/// <param name="assimp_texture_slot">Текстурный слот</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextureSlot(CMaterial material, Assimp.TextureSlot assimp_texture_slot)
			{
				mOwnerMaterial = material;
				mAssimpTextureSlot = assimp_texture_slot;
			}
#endif
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================