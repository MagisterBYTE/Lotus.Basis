//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObject3DNode.cs
*		Узел сцены - совокупность трехмерных моделей с применённой трансформацией.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Object3D
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup Object3DBase
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Узел сцены - совокупность трехмерных моделей с применённой трансформацией
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CNode3D : CEntity3D
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static readonly PropertyChangedEventArgs PropertyArgsOffset = new PropertyChangedEventArgs(nameof(Offset));
			protected static readonly PropertyChangedEventArgs PropertyArgsRotation = new PropertyChangedEventArgs(nameof(Rotation));
			protected static readonly PropertyChangedEventArgs PropertyArgsScale = new PropertyChangedEventArgs(nameof(Scale));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal CNode3D mParentNode;
			internal List<CNode3D> mChildren;
			internal Vector3Df mOffset;
			internal Vector3Df mOffsetOriginal;
			internal Quaternion3Df mRotation;
			internal Quaternion3Df mRotationOriginal;
			internal Vector3Df mScale;
			internal Vector3Df mScaleOriginal;
			internal ListArray<CEntity3D> mAllEntities;
			internal CScene3D mOwnerScene;

			// Платформенно-зависимая часть
#if USE_WINDOWS
			internal Media3D.TranslateTransform3D mTranslateTransform;
			internal Media3D.RotateTransform3D mRotateTransform;
			internal Media3D.ScaleTransform3D mScaleTransform;
			internal Media3D.Transform3DGroup mNodeTransform;
#endif
#if USE_HELIX
			internal List<Helix3D.MeshGeometryModel3D> mHelix3DModels;
#endif
#if USE_ASSIMP
			internal Assimp.Node mAssimpNode;
#endif
#if UNITY_2017_1_OR_NEWER
			internal UnityEngine.Transform mUnityNode;
#endif
#if UNITY_EDITOR
			internal Autodesk.Fbx.FbxNode mFbxNode;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Родительский узел
			/// </summary>
			[Browsable(false)]
			public CNode3D ParentNode
			{
				get { return mParentNode; }
				set
				{
					mParentNode = value;
				}
			}

			/// <summary>
			/// Дочерние узлы
			/// </summary>
			[Browsable(false)]
			public List<CNode3D> Children
			{
				get { return mChildren; }
				set
				{
					mChildren = value;
				}
			}

			//
			// РАЗМЕРЫ И ПОЗИЦИЯ
			//
			/// <summary>
			/// Смещение узла относительно родительского узла
			/// </summary>
			[DisplayName("Смещение")]
			[Description("Смещение узла относительно родительского узла")]
			[Category(XInspectorGroupDesc.Size)]
			[LotusDefaultValue("mOffsetOriginal")]
			[LotusPropertyOrder(0)]
			public Vector3Df Offset
			{
				get { return mOffset; }
				set
				{
					mOffset = value;
					RaiseOffsetChanged();
					NotifyPropertyChanged(PropertyArgsOffset);
				}
			}

			/// <summary>
			/// Кватернион вращения узла относительно родительского узла
			/// </summary>
			[DisplayName("Кватернион вращения")]
			[Description("Кватернион вращения узла относительно родительского узла")]
			[Category(XInspectorGroupDesc.Size)]
			[LotusPropertyOrder(2)]
			public Quaternion3Df Rotation
			{
				get { return mRotation; }
				set
				{
					mRotation = value;
					RaiseRotationChanged();
					NotifyPropertyChanged(PropertyArgsRotation);
				}
			}

			/// <summary>
			/// Масштаб узла относительно родительского узла
			/// </summary>
			[DisplayName("Масштаб")]
			[Description("Масштаб узла относительно родительского узла")]
			[Category(XInspectorGroupDesc.Size)]
			[LotusDefaultValue(nameof(mScaleOriginal), TInspectorMemberType.Field)]
			[LotusPropertyOrder(1)]
			public Vector3Df Scale
			{
				get { return mScale; }
				set
				{
					mScale = value;
					RaiseScaleChanged();
					NotifyPropertyChanged(PropertyArgsScale);
				}
			}

			/// <summary>
			/// Все элементы узла
			/// </summary>
			[Browsable(false)]
			public ListArray<CEntity3D> AllEntities
			{
				get
				{
					if (mAllEntities == null)
					{
						mAllEntities = new ListArray<CEntity3D>();
						for (var i = 0; i < Children.Count; i++)
						{
							mAllEntities.Add(Children[i]);
						}
					}

					return mAllEntities;
				}
			}

			/// <summary>
			/// Владелец сцена
			/// </summary>
			[Browsable(false)]
			public CScene3D OwnerScene
			{
				get { return mOwnerScene; }
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
			/// <param name="ownerScene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D ownerScene)
			{
				mOwnerScene = ownerScene;
				mChildren = new List<CNode3D>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="ownerScene">Сцена</param>
			/// <param name="parentNode">Родительский узел</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D ownerScene, CNode3D parentNode)
				: this(ownerScene)
			{
				mParentNode = parentNode;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="ownerScene">Сцена</param>
			/// <param name="name">Имя узла</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D ownerScene, String name) 
				: this(ownerScene)
			{
				mName = name;
			}

#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="assimp_node">Узел сцены</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D owner_scene, Assimp.Node assimp_node)
			{
				InitData(owner_scene, null, assimp_node);

				InitModels();

				if (mAssimpNode.HasChildren)
				{
					for (Int32 i = 0; i < mAssimpNode.ChildCount; i++)
					{
						mChildren.Add(new CNode3D(owner_scene, this, mAssimpNode.Children[i]));
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="parent_node">Родительский узел</param>
			/// <param name="assimp_node">Узел сцены</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D owner_scene, CNode3D parent_node, Assimp.Node assimp_node)
			{
				InitData(owner_scene, parent_node, assimp_node);

				InitModels();

				if (mAssimpNode.HasChildren)
				{
					for (Int32 i = 0; i < mAssimpNode.ChildCount; i++)
					{
						mChildren.Add(new CNode3D(owner_scene, this, mAssimpNode.Children[i]));
					}
				}
			}
#endif

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="unity_node">Компонент трансформации Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D owner_scene, UnityEngine.Transform unity_node)
				: this(owner_scene)
			{
				mName = unity_node.name;
				mUnityNode = unity_node;
				mOffsetOriginal = mOffset = mUnityNode.localPosition.ToVector3Df();
				mRotationOriginal = mRotation = mUnityNode.localRotation.ToQuaternion3Df();
				mScaleOriginal = mScale = mUnityNode.localScale.ToVector3Df();
			}
#endif
#if UNITY_EDITOR
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="fbx_node">Компонент трансформации Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D owner_scene, Autodesk.Fbx.FbxNode fbx_node)
				: this(owner_scene, null, fbx_node)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="fbx_node">Компонент трансформации Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public CNode3D(CScene3D owner_scene, CNode3D parent_node, Autodesk.Fbx.FbxNode fbx_node)
				: this(owner_scene, parent_node)
			{
				mName = fbx_node.GetName();
				mFbxNode = fbx_node;

				Autodesk.Fbx.FbxDouble3 offset = mFbxNode.LclTranslation.Get();
				mOffsetOriginal = mOffset = new Vector3Df((Single)offset.X, (Single)offset.Y, (Single)offset.Z);

				//mRotationOriginal = mRotation = mUnityNode.localRotation.ToQuaternion3Df();
				//mScaleOriginal = mScale = mUnityNode.localScale.ToVector3Df();
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
			protected virtual void RaiseOffsetChanged()
			{
#if USE_WINDOWS
				if (mTranslateTransform != null)
				{
					mTranslateTransform.OffsetX = mOffset.X;
					mTranslateTransform.OffsetY = mOffset.Y;
					mTranslateTransform.OffsetZ = mOffset.Z;
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if(mUnityNode != null)
				{
					mUnityNode.localPosition = mOffset;
				}
#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение вращения узла.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseRotationChanged()
			{
#if USE_WINDOWS
				if (mRotateTransform != null)
				{
					//mRotateTransform.M = mRotation;
					//mRotateTransform.OffsetY = mOffset.Y;
					//mRotateTransform.OffsetZ = mOffset.Z;
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if (mUnityNode != null)
				{
					mUnityNode.localRotation = new UnityEngine.Quaternion(mRotation.X, mRotation.Y,
						mRotation.Z, mRotation.W);
				}
#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение масштаба узла.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseScaleChanged()
			{
#if USE_WINDOWS
				if (mScaleTransform != null)
				{
					mScaleTransform.ScaleX = mScale.X;
					mScaleTransform.ScaleY = mScale.Y;
					mScaleTransform.ScaleZ = mScale.Z;
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if (mUnityNode != null)
				{
					mUnityNode.localScale = mScale;
				}
#endif
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewItemBuilder ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количества дочерних узлов
			/// </summary>
			/// <returns>Количество дочерних узлов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetCountChildrenNode()
			{
				return AllEntities.Count;
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
				return AllEntities[index];
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ HELIX ====================================
#if USE_HELIX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание модели
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void CreateHelixModels()
			{
                //if (mAssimpNode.HasMeshes)
                //{
                //    for (Int32 i = 0; i < mModels.Count; i++)
                //    {
                //        mModels[i].CreateHelixModel();

                //        // Добавляем в список мешей
                //        mHelix3DModels.Add(mModels[i].Helix3DModel);
                //    }

                //    // Добавляем на сцену
                //    for (Int32 i = 0; i < mAssimpNode.MeshCount; i++)
                //    {
                //        mOwnerScene.mHelixScene.Add(mHelix3DModels[i]);
                //    }
                //}

                //for (Int32 i = 0; i < mChildren.Count; i++)
                //{
                //    mChildren[i].CreateHelixModels();
                //}
            }
#endif
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ ASSIMP ===================================
#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация объекта класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			/// <param name="parent_node">Родительский узел</param>
			/// <param name="assimp_node">Узел сцены</param>
			//---------------------------------------------------------------------------------------------------------
			protected void InitData(CScene3D owner_scene, CNode3D parent_node, Assimp.Node assimp_node)
			{
				mOwnerScene = owner_scene;
				mParentNode = parent_node;

				if (assimp_node != null)
				{
					mAssimpNode = assimp_node;
					mName = mAssimpNode.Name;

					Assimp.Vector3D scale;
					Assimp.Quaternion rotation;
					Assimp.Vector3D offset;
					mAssimpNode.Transform.Decompose(out scale, out rotation, out offset);
					mScale = scale.ToVector3D();
					mScaleOriginal = mScale;
					mRotation = rotation.ToQuaternion3Df();
					mOffset = offset.ToVector3D();
					mOffsetOriginal = mOffset;

					mTranslateTransform = new Media3D.TranslateTransform3D(mOffset.X, mOffset.Y, mOffset.Z);
					mRotateTransform = new Media3D.RotateTransform3D();
					mScaleTransform = new Media3D.ScaleTransform3D(mScale.X, mScale.Y, mScale.Z);
					mNodeTransform = new Media3D.Transform3DGroup();

					mNodeTransform.Children.Add(mTranslateTransform);
					mNodeTransform.Children.Add(mRotateTransform);
					mNodeTransform.Children.Add(mScaleTransform);
				}

				mChildren = new List<CNode3D>();
				mHelix3DModels = new List<Helix3D.MeshGeometryModel3D>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инициализация моделей
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void InitModels()
			{
				if (mAssimpNode.HasMeshes)
				{
					//mModels = new List<CModel3D>(mAssimpNode.MeshCount);
					//for (Int32 i = 0; i < mAssimpNode.MeshCount; i++)
					//{
					//	CModel3D model = new CModel3D(this, i);
					//	mModels.Add(model);
					//}
				}
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ UNITY ====================================
#if UNITY_2017_1_OR_NEWER
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================