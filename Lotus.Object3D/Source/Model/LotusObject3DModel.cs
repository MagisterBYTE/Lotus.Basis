//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема трехмерной модели
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObject3DModel.cs
*		Модель - меш с примененным материалом который подлежит отображению через узел сцены.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
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
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Model.Scene;
#endif
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Object3D
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup Object3DBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Модель - меш с примененным материалом который подлежит отображению через узел сцены
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CModel3D : CNode3D
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVisible = new PropertyChangedEventArgs(nameof(IsVisible));
			protected static readonly PropertyChangedEventArgs PropertyArgsMesh = new PropertyChangedEventArgs(nameof(Mesh));
			protected static readonly PropertyChangedEventArgs PropertyArgsMaterial = new PropertyChangedEventArgs(nameof(Material));
			protected static readonly PropertyChangedEventArgs PropertyArgsLocation = new PropertyChangedEventArgs(nameof(Location));
			protected static readonly PropertyChangedEventArgs PropertyArgsSizeX = new PropertyChangedEventArgs(nameof(SizeX));
			protected static readonly PropertyChangedEventArgs PropertyArgsSizeY = new PropertyChangedEventArgs(nameof(SizeY));
			protected static readonly PropertyChangedEventArgs PropertyArgsSizeZ = new PropertyChangedEventArgs(nameof(SizeZ));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Boolean mIsVisible = true;
			protected internal CMesh3Df mMesh;
			protected internal CMaterial mMaterial;

			// Размеры и позиция
			protected internal Vector3Df mLocation;
			protected internal Vector3Df mMinPosition;
			protected internal Vector3Df mMaxPosition;

			// Платформенно-зависимая часть
#if USE_HELIX
			internal MeshNode mHelixModel;
#endif
#if (UNITY_2017_1_OR_NEWER)
			internal UnityEngine.MeshFilter mUnityModel;
#endif
#if UNITY_EDITOR
			internal Autodesk.Fbx.FbxNode mFbxModel;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Видимость модели
			/// </summary>
			[DisplayName("Видимость")]
			[Description("Видимость модели")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(0)]
			public Boolean IsVisible
			{
				get
				{
					return (mIsVisible);
				}
				set
				{
					mIsVisible = value;
					RaiseIsVisibleChanged();
					NotifyPropertyChanged(PropertyArgsIsVisible );
				}
			}

			/// <summary>
			/// Меш модели
			/// </summary>
			[DisplayName("Меш модели")]
			[Description("Меш модели")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(1)]
			public CMesh3Df Mesh
			{
				get
				{
					return (mMesh);
				}
				set
				{
					mMesh = value;
					RaiseMeshChanged();
					NotifyPropertyChanged(PropertyArgsMesh);
				}
			}

			/// <summary>
			/// Материал модели
			/// </summary>
			[DisplayName("Материал модели")]
			[Description("Материал модели")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(1)]
			public CMaterial Material
			{
				get
				{
					return (mMaterial);
				}
				set
				{
					mMaterial = value;
					RaiseMaterialChanged();
					NotifyPropertyChanged(PropertyArgsMaterial);
				}
			}

			//
			// РАЗМЕРЫ И ПОЗИЦИЯ
			//
			/// <summary>
			/// Позиция геометрического центра модели
			/// </summary>
			[DisplayName("Центр")]
			[Description("Позиция геометрического центра модели")]
			[Category(XInspectorGroupDesc.Size)]
			public Vector3Df Location
			{
				get { return (mLocation); }
			}

			/// <summary>
			/// Размер меша по оси X
			/// </summary>
			[DisplayName("Размер по X")]
			[Description("Размер меша по оси X")]
			[Category(XInspectorGroupDesc.Size)]
			public Single SizeX
			{
				get { return (mMaxPosition.X - mMinPosition.X); }
			}

			/// <summary>
			/// Размер меша по оси Y
			/// </summary>
			[DisplayName("Размер по Y")]
			[Description("Размер меша по оси Y")]
			[Category(XInspectorGroupDesc.Size)]
			public Single SizeY
			{
				get { return (mMaxPosition.Y - mMinPosition.Y); }
			}

			/// <summary>
			/// Размер меша по оси Z
			/// </summary>
			[DisplayName("Размер по Z")]
			[Description("Размер меша по оси Z")]
			[Category(XInspectorGroupDesc.Size)]
			public Single SizeZ
			{
				get { return (mMaxPosition.Z - mMinPosition.Z); }
			}

			//
			// ПЛАТФОРМЕННО-ЗАВИСИМАЯ ЧАСТЬ
			//
#if USE_HELIX
			/// <summary>
			/// Модель
			/// </summary>
			[Browsable(false)]
			public MeshNode HelixModel
			{
				get { return (mHelixModel); }
			}
#endif
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="parent_node">Родительский узел</param>
			//---------------------------------------------------------------------------------------------------------
			public CModel3D(CScene3D owner_scene, CNode3D parent_node)
				: base(owner_scene)
			{
				mOwnerScene = owner_scene;
				mParentNode = parent_node;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="parent_node">Родительский узел</param>
			/// <param name="mesh">Меш</param>
			/// <param name="material">Материал</param>
			//---------------------------------------------------------------------------------------------------------
			public CModel3D(CScene3D owner_scene, CNode3D parent_node, CMesh3Df mesh, CMaterial material)
				: this(owner_scene, parent_node)
			{
				mMesh = mesh;
				mMaterial = material;
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="parent_node">Родительский узел</param>
			/// <param name="unity_model">Модель Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public CModel3D(CScene3D owner_scene, CNode3D parent_node, UnityEngine.MeshFilter unity_model)
				: this(owner_scene, parent_node)
			{
				mName = unity_model.name;
				mUnityModel = unity_model;
			}
#endif
#if UNITY_EDITOR
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="parent_node">Родительский узел</param>
			/// <param name="fbx_model">Модель Fbx</param>
			//---------------------------------------------------------------------------------------------------------
			public CModel3D(CScene3D owner_scene, CNode3D parent_node, Autodesk.Fbx.FbxNode fbx_model)
				: this(owner_scene, parent_node)
			{
				mName = fbx_model.GetName();
				mFbxModel = fbx_model;
			}
#endif
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение позиции узла.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected override void RaiseOffsetChanged()
			{
				base.RaiseOffsetChanged();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение вращения узла.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected override void RaiseRotationChanged()
			{
				base.RaiseRotationChanged();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение масштаба узла.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected override void RaiseScaleChanged()
			{
				base.RaiseScaleChanged();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение статуса видимости модели.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsVisibleChanged()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение меша модели.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseMeshChanged()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение материала модели.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseMaterialChanged()
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================

			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ WINDOWS ==================================
#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение ограничивающего объема с учетом трансформации
			/// </summary>
			/// <returns>Ограничивающий объем</returns>
			//---------------------------------------------------------------------------------------------------------
			public Media3D.Rect3D GetBoundsRect()
			{
				return (Media3D.Rect3D.Empty);
				//return (mHelix3DModel.BoundsWithTransform.ToWinRect3D());
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ HELIX ====================================
#if USE_HELIX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание модели
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void CreateHelixModel()
			{
				//mHelix3DModel = new Helix3D.MeshGeometryModel3D();

				// Геометрия
				//mHelix3DModel.Geometry = mSubmittedMesh.mHelixMesh;

				// Материал
				//mHelix3DModel.Material = mOwnerNode.OwnerScene.GetMaterialHelixFromIndex(mSubmittedMesh.MaterialIndex);

				// Трансформация
				//mHelix3DModel.Transform = new Media3D.MatrixTransform3D(mOwnerNode.mAssimpNode.Transform.ToWinMatrix4D());
				//mHelix3DModel.Transform = mOwnerNode.mNodeTransform;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление ограничивающего объема
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			internal void ComputeBoundingBox()
			{
				if (mHelixModel != null)
				{
					SharpDX.BoundingBox bounding_box = mHelixModel.BoundsWithTransform;

					mMinPosition = new Vector3D(bounding_box.Minimum.X, bounding_box.Minimum.Y, bounding_box.Minimum.Z);
					mMaxPosition = new Vector3D(bounding_box.Maximum.X, bounding_box.Maximum.Y, bounding_box.Maximum.Z);

					mLocation.X = (mMinPosition.X + mMaxPosition.X) / 2.0f;
					mLocation.Y = (mMinPosition.Y + mMaxPosition.Y) / 2.0f;
					mLocation.Z = (mMinPosition.Z + mMaxPosition.Z) / 2.0f;

					NotifyPropertyChanged(PropertyArgsLocation);
					NotifyPropertyChanged(PropertyArgsSizeX);
					NotifyPropertyChanged(PropertyArgsSizeY);
					NotifyPropertyChanged(PropertyArgsSizeZ);
				}
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ ASSIMP ===================================
#if USE_ASSIMP

#endif
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ UNITY ====================================
#if (UNITY_2017_1_OR_NEWER)
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================