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
			protected internal CNode3D _parentNode;
			protected internal List<CNode3D> _children;
			protected internal Vector3Df _offset;
			protected internal Vector3Df _offsetOriginal;
			protected internal Quaternion3Df _rotation;
			protected internal Quaternion3Df _rotationOriginal;
			protected internal Vector3Df _scale;
			protected internal Vector3Df _scaleOriginal;
			protected internal ListArray<CEntity3D> _allEntities;
			protected internal CScene3D _ownerScene;

			// Платформенно-зависимая часть
#if USE_WINDOWS
			protected internal Media3D.TranslateTransform3D _translateTransform;
			protected internal Media3D.RotateTransform3D _rotateTransform;
			protected internal Media3D.ScaleTransform3D _scaleTransform;
			protected internal Media3D.Transform3DGroup _nodeTransform;
#endif
#if USE_HELIX
			protected internal List<Helix3D.MeshGeometryModel3D> _helix3DModels;
#endif
#if USE_ASSIMP
			protected internal Assimp.Node _assimpNode;
#endif
#if UNITY_2017_1_OR_NEWER
			protected internal UnityEngine.Transform _unityNode;
#endif
#if UNITY_EDITOR
			protected internal Autodesk.Fbx.FbxNode _fbxNode;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Родительский узел
			/// </summary>
			public CNode3D ParentNode
			{
				get { return _parentNode; }
				set
				{
					_parentNode = value;
				}
			}

			/// <summary>
			/// Дочерние узлы
			/// </summary>
			public List<CNode3D> Children
			{
				get { return _children; }
				set
				{
					_children = value;
				}
			}

			//
			// РАЗМЕРЫ И ПОЗИЦИЯ
			//
			/// <summary>
			/// Смещение узла относительно родительского узла
			/// </summary>
			public Vector3Df Offset
			{
				get { return _offset; }
				set
				{
					_offset = value;
					RaiseOffsetChanged();
					NotifyPropertyChanged(PropertyArgsOffset);
				}
			}

			/// <summary>
			/// Кватернион вращения узла относительно родительского узла
			/// </summary>
			public Quaternion3Df Rotation
			{
				get { return _rotation; }
				set
				{
					_rotation = value;
					RaiseRotationChanged();
					NotifyPropertyChanged(PropertyArgsRotation);
				}
			}

			/// <summary>
			/// Масштаб узла относительно родительского узла
			/// </summary>
			public Vector3Df Scale
			{
				get { return _scale; }
				set
				{
					_scale = value;
					RaiseScaleChanged();
					NotifyPropertyChanged(PropertyArgsScale);
				}
			}

			/// <summary>
			/// Все элементы узла
			/// </summary>
			public ListArray<CEntity3D> AllEntities
			{
				get
				{
					if (_allEntities == null)
					{
						_allEntities = new ListArray<CEntity3D>();
						for (var i = 0; i < Children.Count; i++)
						{
							_allEntities.Add(Children[i]);
						}
					}

					return _allEntities;
				}
			}

			/// <summary>
			/// Владелец сцена
			/// </summary>
			public CScene3D OwnerScene
			{
				get { return _ownerScene; }
				set
				{
					_ownerScene = value;
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
				_ownerScene = ownerScene;
				_children = new List<CNode3D>();
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
				_parentNode = parentNode;
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
				_name = name;
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

				if (_assimpNode.HasChildren)
				{
					for (var i = 0; i < _assimpNode.ChildCount; i++)
					{
						_children.Add(new CNode3D(owner_scene, this, _assimpNode.Children[i]));
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

				if (_assimpNode.HasChildren)
				{
					for (var i = 0; i < _assimpNode.ChildCount; i++)
					{
						_children.Add(new CNode3D(owner_scene, this, _assimpNode.Children[i]));
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
				_name = unity_node.name;
				_unityNode = unity_node;
				_offsetOriginal = _offset = _unityNode.localPosition.ToVector3Df();
				_rotationOriginal = _rotation = _unityNode.localRotation.ToQuaternion3Df();
				_scaleOriginal = _scale = _unityNode.localScale.ToVector3Df();
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
				_name = fbx_node.GetName();
				_fbxNode = fbx_node;

				Autodesk.Fbx.FbxDouble3 offset = _fbxNode.LclTranslation.Get();
				_offsetOriginal = _offset = new Vector3Df((Single)offset.X, (Single)offset.Y, (Single)offset.Z);

				//_rotationOriginal = _rotation = _unityNode.localRotation.ToQuaternion3Df();
				//_scaleOriginal = _scale = _unityNode.localScale.ToVector3Df();
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
				if (_translateTransform != null)
				{
					_translateTransform.OffsetX = _offset.X;
					_translateTransform.OffsetY = _offset.Y;
					_translateTransform.OffsetZ = _offset.Z;
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if(_unityNode != null)
				{
					_unityNode.localPosition = _offset;
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
				if (_rotateTransform != null)
				{
					//_rotateTransform.M = _rotation;
					//_rotateTransform.OffsetY = _offset.Y;
					//_rotateTransform.OffsetZ = _offset.Z;
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if (_unityNode != null)
				{
					_unityNode.localRotation = new UnityEngine.Quaternion(_rotation.X, _rotation.Y,
						_rotation.Z, _rotation.W);
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
				if (_scaleTransform != null)
				{
					_scaleTransform.ScaleX = _scale.X;
					_scaleTransform.ScaleY = _scale.Y;
					_scaleTransform.ScaleZ = _scale.Z;
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if (_unityNode != null)
				{
					_unityNode.localScale = _scale;
				}
#endif
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
                //if (_assimpNode.HasMeshes)
                //{
                //    for (Int32 i = 0; i < _models.Count; i++)
                //    {
                //        _models[i].CreateHelixModel();

                //        // Добавляем в список мешей
                //        _helix3DModels.Add(mModels[i].Helix3DModel);
                //    }

                //    // Добавляем на сцену
                //    for (Int32 i = 0; i < _assimpNode.MeshCount; i++)
                //    {
                //        _ownerScene._helixScene.Add(_helix3DModels[i]);
                //    }
                //}

                //for (Int32 i = 0; i < _children.Count; i++)
                //{
                //    _children[i].CreateHelixModels();
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
				_ownerScene = owner_scene;
				_parentNode = parent_node;

				if (assimp_node != null)
				{
					_assimpNode = assimp_node;
					_name = _assimpNode.Name;

					Assimp.Vector3D scale;
					Assimp.Quaternion rotation;
					Assimp.Vector3D offset;
					_assimpNode.Transform.Decompose(out scale, out rotation, out offset);
					_scale = scale.ToVector3D();
					_scaleOriginal = _scale;
					_rotation = rotation.ToQuaternion3Df();
					_offset = offset.ToVector3D();
					_offsetOriginal = _offset;

					_translateTransform = new Media3D.TranslateTransform3D(_offset.X, _offset.Y, _offset.Z);
					_rotateTransform = new Media3D.RotateTransform3D();
					_scaleTransform = new Media3D.ScaleTransform3D(_scale.X, _scale.Y, _scale.Z);
					_nodeTransform = new Media3D.Transform3DGroup();

					_nodeTransform.Children.Add(_translateTransform);
					_nodeTransform.Children.Add(_rotateTransform);
					_nodeTransform.Children.Add(_scaleTransform);
				}

				_children = new List<CNode3D>();
				_helix3DModels = new List<Helix3D.MeshGeometryModel3D>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инициализация моделей
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void InitModels()
			{
				if (_assimpNode.HasMeshes)
				{
					//mModels = new List<CModel3D>(_assimpNode.MeshCount);
					//for (Int32 i = 0; i < _assimpNode.MeshCount; i++)
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