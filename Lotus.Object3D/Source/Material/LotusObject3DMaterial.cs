//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема материала
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObject3DMaterial.cs
*		Материал для определение параметров визуализации геометрии объекта.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
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
		//! \defgroup Object3DMaterial Подсистема материала
		//! Подсистема материала определяет параметры визуализации геометрии объекта
		//! \ingroup Object3D
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Материал для определение параметров визуализации геометрии объекта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CMaterial : CEntity3D
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static readonly PropertyChangedEventArgs PropertyArgsAmbientColor = new PropertyChangedEventArgs(nameof(AmbientColor));
			protected static readonly PropertyChangedEventArgs PropertyArgsDiffuseColor = new PropertyChangedEventArgs(nameof(DiffuseColor));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal TColor mAmbientColor;
			internal TColor mAmbientColorOriginal;
			internal TColor mDiffuseColor;
			internal TColor mDiffuseColorOriginal;
			internal ListArray<CTextureSlot> mTextureSlots;
			internal CTextureSlot mAmbientSlot;
			internal CTextureSlot mDiffuseSlot;
			internal CTextureSlot mNormalSlot;
			internal CTextureSlot mHeightSlot;
			internal CScene3D mOwnerScene;

			// Платформенно-зависимая часть
#if USE_HELIX
			internal Helix3D.PhongMaterial mHelixMaterial;
#endif
#if USE_ASSIMP
			internal Assimp.Material mAssimpMaterial;
#endif
#if (UNITY_2017_1_OR_NEWER)
			internal UnityEngine.Material mUnityMaterial;
#endif
#if UNITY_EDITOR
			internal Autodesk.Fbx.FbxSurfaceMaterial mFbxMaterial;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Цвет подсветки материала
			/// </summary>
			[DisplayName("AmbientColor")]
			[Description("Цвет подсветки материала")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(0)]
			public TColor AmbientColor
			{
				get { return (mAmbientColor); }
				set
				{
					if (mAmbientColor != value)
					{
						mAmbientColor = value;
						RaiseAmbientColorChanged();
						NotifyPropertyChanged(PropertyArgsAmbientColor);
					}
				}
			}

			/// <summary>
			/// Основной цвет материала
			/// </summary>
			[DisplayName("DiffuseColor")]
			[Description("Основной цвет материала")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(1)]
			public TColor DiffuseColor
			{
				get { return (mDiffuseColor); }
				set
				{
					if (mDiffuseColor != value)
					{
						mDiffuseColor = value;
						RaiseDiffuseColorChanged();
						NotifyPropertyChanged(PropertyArgsDiffuseColor);
					}
				}
			}

			/// <summary>
			/// Все текстурные слоты
			/// </summary>
			[Browsable(false)]
			public ListArray<CTextureSlot> TextureSlots
			{
				get { return (mTextureSlots); }
			}

			/// <summary>
			/// Владелец сцена
			/// </summary>
			[Browsable(false)]
			public CScene3D OwnerScene
			{
				get { return (mOwnerScene); }
				set
				{
					mOwnerScene = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CMaterial(CScene3D owner_scene)
			{
				mOwnerScene = owner_scene;
				mTextureSlots = new ListArray<CTextureSlot>
				{
					IsNotify = true
				};
			}

#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="assimp_material">Материал</param>
			//---------------------------------------------------------------------------------------------------------
			public CMaterial(CScene3D owner_scene, Assimp.Material assimp_material)
			{
				mOwnerScene = owner_scene;
				mTextureSlots = new ListArray<CTextureSlot>();

				mAssimpMaterial = assimp_material;
				mName = mAssimpMaterial.Name;

				if (mAssimpMaterial.HasTextureAmbient)
				{
					mAmbientSlot = new CTextureSlot(this, mAssimpMaterial.TextureAmbient);
					mAmbientSlot.Name = "Ambient";
					mTextureSlots.Add(mAmbientSlot);
				}

				if (mAssimpMaterial.HasTextureDiffuse)
				{
					mDiffuseSlot = new CTextureSlot(this, mAssimpMaterial.TextureDiffuse);
					mDiffuseSlot.Name = "Diffuse";
					mTextureSlots.Add(mDiffuseSlot);
				}

				if (mAssimpMaterial.HasTextureNormal)
				{
					mNormalSlot = new CTextureSlot(this, mAssimpMaterial.TextureNormal);
					mNormalSlot.Name = "Normal";
					mTextureSlots.Add(mNormalSlot);
				}

				if (mAssimpMaterial.HasTextureHeight)
				{
					mHeightSlot = new CTextureSlot(this, mAssimpMaterial.TextureHeight);
					mHeightSlot.Name = "Height";
					mTextureSlots.Add(mHeightSlot);
				}
			}
#endif

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="unity_material">Материал Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public CMaterial(CScene3D owner_scene, UnityEngine.Material unity_material) 
				: this(owner_scene)
			{
				mName = unity_material.name;
				mUnityMaterial = unity_material;
				//mAmbientColorOriginal = mAmbientColor = mUnityMaterial.GetColor("Ambient").ToTColor();
				//mDiffuseColorOriginal = mDiffuseColor = mUnityMaterial.GetColor("Diffuse").ToTColor();
			}
#endif
#if UNITY_EDITOR
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="fbx_material">Материал Fbx</param>
			//---------------------------------------------------------------------------------------------------------
			public CMaterial(CScene3D owner_scene, Autodesk.Fbx.FbxSurfaceMaterial fbx_material)
				: this(owner_scene)
			{
				mName = fbx_material.GetName();
				mFbxMaterial = fbx_material;
				//mAmbientColorOriginal = mAmbientColor = mUnityMaterial.GetColor("Ambient").ToTColor();
				//mDiffuseColorOriginal = mDiffuseColor = mUnityMaterial.GetColor("Diffuse").ToTColor();
			}
#endif
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение цвета подсветки материала.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseAmbientColorChanged()
			{
#if USE_WINDOWS

#endif
#if USE_HELIX
				if (mHelixMaterial != null)
				{
					//mHelixMaterial.AmbientColor = mAmbientColor.ToShColor4();
				}
#endif
#if (UNITY_2017_1_OR_NEWER)
				if (mUnityMaterial != null)
				{
					mUnityMaterial.SetColor("Ambient", mAmbientColor);
				}
#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение основного цвета материала.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseDiffuseColorChanged()
			{
#if USE_WINDOWS

#endif
#if USE_HELIX
				if (mHelixMaterial != null)
				{
					//mHelixMaterial.DiffuseColor = mDiffuseColor.ToShColor4();
				}
#endif
#if (UNITY_2017_1_OR_NEWER)
				if (mUnityMaterial != null)
				{
					mUnityMaterial.SetColor("Diffuse", mAmbientColor);
				}
#endif
			}
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ HELIX ====================================
#if USE_HELIX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание материала
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void CreateHelixMaterial()
			{
				try
				{
					mHelixMaterial = new Helix3D.PhongMaterial();
					mHelixMaterial.Name = mName;

                    //if (mAssimpMaterial.HasTextureDiffuse)
                    //{
                    //    mHelixMaterial.DiffuseMap = mDiffuseSlot.GetTextureSteam();
                    //}

                    //if (mAssimpMaterial.HasTextureNormal)
                    //{
                    //    mHelixMaterial.NormalMap = mNormalSlot.GetTextureSteam();
                    //}

                    //if (mAssimpMaterial.HasTextureHeight)
                    //{
                    //    mHelixMaterial.DisplacementMap = mHeightSlot.GetTextureSteam();
                    //}
                }

				catch (Exception exc)
				{
					XLogger.LogExceptionModule(nameof(CScene3D), exc);
				}
			}
#endif

			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Набор всех материалов в сцене
		/// </summary>
		/// <remarks>
		/// Предназначен для логического группирования всех материалов в обозревателе сцены
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CMaterialSet : CEntity3D
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal ListArray<CMaterial> mMaterials;
			internal CScene3D mOwnerScene;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наблюдаемая коллекция материал
			/// </summary>
			[Browsable(false)]
			public ListArray<CMaterial> Materials
			{
				get { return (mMaterials); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CMaterialSet(CScene3D owner_scene)
			{
				mOwnerScene = owner_scene;
				mName = "Материалы";
				mMaterials = new ListArray<CMaterial>
				{
					IsNotify = true
				};
			}

#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="assimp_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CMaterialSet(CScene3D owner_scene, Assimp.Scene assimp_scene)
			{
				mName = "Материалы";
				mMaterials = new ListArray<CMaterial>();

				// Устанавливаем материалы
				for (Int32 i = 0; i < assimp_scene.MaterialCount; i++)
				{
					Assimp.Material assimp_material = assimp_scene.Materials[i];
					CMaterial material = new CMaterial(owner_scene, assimp_material);
					mMaterials.Add(material);
				}
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ ILotusTreeNodeViewBuilder ==========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количества дочерних узлов
			/// </summary>
			/// <returns>Количество дочерних узлов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetCountChildrenNode()
			{
				return (mMaterials.Count);
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
				return (mMaterials[index]);
			}
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ HELIX ====================================
#if USE_HELIX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание всех материалов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void CreateHelixMaterials()
			{
				for (Int32 i = 0; i < mMaterials.Count; i++)
				{
					mMaterials[i].CreateHelixMaterial();
				}
			}
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================