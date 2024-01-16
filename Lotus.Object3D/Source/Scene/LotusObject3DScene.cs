//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема сцены
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObject3DScene.cs
*		Сцена для представления всех сущностей 3D контента.
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
using System.Linq;
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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEditor;
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
		/// Сцена для представления всех сущностей 3D контента
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CScene3D : CEntity3D, IEnumerator, IEnumerable
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
#if USE_ASSIMP
			/// <summary>
			/// Представляет контекст импорта/экспорта Assimp, который загружает или сохраняет модели с помощью неуправляемой библиотеки. 
			/// Кроме того, предлагается функция преобразования для обхода загрузки данных модели в управляемую память
			/// </summary>
			protected readonly static Assimp.AssimpContext AssimpContextDefault = new Assimp.AssimpContext();
#endif
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка поддерживаемых экспортируемых файлов
			/// </summary>
			/// <returns>Список обозначений экспортируемых файлов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String[] GetSupportedExportFormats()
			{
				var items = AssimpContextDefault.GetSupportedExportFormats();
				var formats = new String[items.Length];
				for (var i = 0; i < items.Length; i++)
				{
					formats[i] = items[i].FormatId;
				}

				return formats;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка поддерживаемых импортируемых файлов
			/// </summary>
			/// <returns>Список расширений импортируемых файлов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String[] GetSupportedImportFormats()
			{
				return AssimpContextDefault.GetSupportedImportFormats();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка 3D контента по полному пути
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Объект <see cref="CScene3D"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public static CScene3D LoadFromFile(String file_name)
			{
				try
				{
					Assimp.PostProcessSteps step =
						Assimp.PostProcessSteps.FindInstances |
						Assimp.PostProcessSteps.OptimizeGraph |
						Assimp.PostProcessSteps.ValidateDataStructure |
						Assimp.PostProcessSteps.SortByPrimitiveType |
						Assimp.PostProcessSteps.Triangulate |
						Assimp.PostProcessSteps.FlipWindingOrder |
						Assimp.PostProcessSteps.FlipUVs;

					//Assimp.PostProcessPreset

					Assimp.Scene assimp_scene = AssimpContextDefault.ImportFile(file_name, step);
					if(assimp_scene != null)
					{
						var scene = new CScene3D(Path.GetFileNameWithoutExtension(file_name), assimp_scene);
						scene.FileName = file_name;
						return scene;
					}
				}
				catch (Exception exc)
				{
					XLogger.LogExceptionModule(nameof(CScene3D), exc);
				}

				return null;
			}
#endif
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Идентификация
			protected internal String? _fileName;

			// Основные параметры
			protected internal CNode3D? _rootNode;
			protected internal CMeshSet _meshSet;
			protected internal CMaterialSet _materialSet;
			protected internal CTextureSet _textureSet;
			internal ListArray<CEntity3D> _allEntities;

			// Размеры и позиция
			protected internal Vector3Df _minPosition;
			protected internal Vector3Df _maxPosition;
			protected internal Vector3Df _centerPosition;

			// Поддержка перечисления
			protected internal Int32 _enumeratorIndex;

			// Платформенно-зависимая часть
#if USE_ASSIMP
			protected internal Assimp.Scene _assimpScene;
#endif
#if USE_HELIX
			protected internal SceneNode _helixScene;
#endif
#if UNITY_2017_1_OR_NEWER
			protected internal GameObject _unityScene;
#endif
#if UNITY_EDITOR
			protected internal Autodesk.Fbx.FbxScene _fbxScene;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ИДЕНТИФИКАЦИЯ
			//
			/// <summary>
			/// Имя файла
			/// </summary>
			public String? FileName
			{
				get { return _fileName; }
				set
				{
					_fileName = value;
				}
			}

			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Корневой узел сцены
			/// </summary>
			public CNode3D? RootNode
			{
				get { return _rootNode; }
			}

			/// <summary>
			/// Набор всех трехмерных сеток(мешей) в сцене
			/// </summary>
			public CMeshSet MeshSet
			{
				get { return _meshSet; }
			}

			/// <summary>
			/// Набор всех материалов в сцене
			/// </summary>
			public CMaterialSet MaterialSet
			{
				get { return _materialSet; }
			}

			/// <summary>
			/// Все элементы сцены
			/// </summary>
			public ListArray<CEntity3D> AllEntities
			{
				get
				{
					if (_allEntities == null)
					{
						_allEntities = new ListArray<CEntity3D>();
						_allEntities.IsNotify = false;
						_allEntities.Add(_meshSet);
						_allEntities.Add(_materialSet);
						_allEntities.Add(_textureSet);
						_allEntities.Add(_rootNode);
					}

					return _allEntities;
				}
			}

			//
			// РАЗМЕРЫ И ПОЗИЦИЯ
			//
			/// <summary>
			/// Геометрический центр сцены
			/// </summary>
			public Vector3Df CenterPosition
			{
				get { return _centerPosition; }
			}

			/// <summary>
			/// Размер сцены по X с учетом всех элементов
			/// </summary>
			public Single SizeX
			{
				get { return _maxPosition.X - _minPosition.X; }
			}

			/// <summary>
			/// Размер сцены по Y с учетом всех элементов
			/// </summary>
			public Single SizeY
			{
				get { return _maxPosition.Y - _minPosition.Y; }
			}

			/// <summary>
			/// Размер сцены по Z с учетом всех элементов
			/// </summary>
			public Single SizeZ
			{
				get { return _maxPosition.Z - _minPosition.Z; }
			}

			//
			// ПЛАТФОРМЕННО-ЗАВИСИМАЯ ЧАСТЬ
			//
#if USE_HELIX
			/// <summary>
			/// Сцена Helix
			/// </summary>
			public SceneNode HelixScene
			{
				get { return _helixScene; }
			}
#endif
#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Сцена Unity
			/// </summary>
			public UnityEngine.GameObject UnityScene
			{
				get { return (_unityScene);}
				set
				{
					if (_unityScene != value)
					{
						_unityScene = value;
						CreateSceneFromUnityGameObject();
					}
				}
			}
#endif
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CScene3D()
			{
				_meshSet = new CMeshSet(this);
				_materialSet = new CMaterialSet(this);
				_textureSet = new CTextureSet(this);
				_allEntities = new ListArray<CEntity3D>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сцены</param>
			//---------------------------------------------------------------------------------------------------------
			public CScene3D(String name)
				:this()
			{
				_name = name;
			}

#if USE_HELIX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сцены</param>
			/// <param name="helix_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CScene3D(String name, SceneNode helix_scene)
			{
				_name = name;
				_helixScene = helix_scene;
				//CreateSceneFromHelixScene();
				//_meshSet = new CMeshSet();
				//_materialSet = new CMaterialSet();
				//_rootNode = new CNode3D(this, _assimpScene.RootNode);
				//ComputeBoundingBox();
			}
#endif

#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сцены</param>
			/// <param name="assimp_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CScene3D(String name, Assimp.Scene assimp_scene)
			{
				_name = name;
				_assimpScene = assimp_scene;
				_meshSet = new CMeshSet(_assimpScene);
				_materialSet = new CMaterialSet(this, _assimpScene);
				_rootNode = new CNode3D(this, _assimpScene.RootNode);
				ComputeBoundingBox();
			}
#endif
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="unity_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CScene3D(GameObject unity_scene)
				:this()
			{
				_unityScene = unity_scene;
				CreateSceneFromUnityGameObject();
			}
#endif

#if UNITY_EDITOR
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="fbx_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CScene3D(Autodesk.Fbx.FbxScene fbx_scene)
				: this()
			{
				_fbxScene = fbx_scene;
				CreateSceneFromFbxScene();
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ IEnumerator ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Текущий объект
			/// </summary>
			/// <returns>Текущий объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Current
			{
				get
				{
					return this;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Возможность продвинуться вперед
			/// </summary>
			/// <returns>Статус возможности продвинуться вперед</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean MoveNext()
			{
				_enumeratorIndex++;
				return _enumeratorIndex == 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка объекта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Reset()
			{
				_enumeratorIndex = 0;
			}
			#endregion

			#region ======================================= МЕТОДЫ IEnumerable ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение перечислителя
			/// </summary>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
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
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение меша по индексу
			/// </summary>
			/// <param name="index">Индекс меша</param>
			/// <returns>Меш</returns>
			//---------------------------------------------------------------------------------------------------------
			public CMesh3Df GetMeshFromIndex(Int32 index)
			{
				return _meshSet.Meshes[index];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение материала по индексу
			/// </summary>
			/// <param name="index">Индекс материала</param>
			/// <returns>Материал</returns>
			//---------------------------------------------------------------------------------------------------------
			public CMaterial GetMaterialFromIndex(Int32 index)
			{
				return _materialSet.Materials[index];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление ограничивающего объема сцены
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void ComputeBoundingBox()
			{
				//Assimp.Vector3D min_position = new Assimp.Vector3D(1e10f, 1e10f, 1e10f);
				//Assimp.Vector3D max_position = new Assimp.Vector3D(-1e10f, -1e10f, -1e10f);
				//Assimp.Matrix4x4 identity = Assimp.Matrix4x4.Identity;

				//ComputeBoundingBox(_assimpScene.RootNode, ref min_position, ref max_position, ref identity);

				//_maxPosition = max_position.ToVector3D();
				//_minPosition = min_position.ToVector3D();

				//mCenterPosition.X = (_minPosition.X + _maxPosition.X) / 2.0f;
				//mCenterPosition.Y = (_minPosition.Y + _maxPosition.Y) / 2.0f;
				//mCenterPosition.Z = (_minPosition.Z + _maxPosition.Z) / 2.0f;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ UNITY ====================================
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание сцены из игрового объекта Unity
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void CreateSceneFromUnityGameObject()
			{
				if (_unityScene == null) return;

				_rootNode = null;
				_meshSet.Meshes.Clear();
				_materialSet.Materials.Clear();

				_name = _unityScene.name;
				_rootNode = CreateSceneRecursiveFromUnity(_unityScene.transform, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивно создаем объекты
			/// </summary>
			/// <param name="transform">Компонент трансформации Unity</param>
			/// <param name="parent_node">Родительский узел</param>
			/// <returns>Созданный узел</returns>
			//---------------------------------------------------------------------------------------------------------
			private CNode3D CreateSceneRecursiveFromUnity(Transform transform, CNode3D parent_node)
			{
				// Если на присутствует компонент меш фильтр то сразу  создаем корневой узел как модель
				MeshFilter mesh_filter = transform.GetComponent<MeshFilter>();
				CNode3D node = null;
				if (mesh_filter != null)
				{
					// Добавляем меш
					if(mesh_filter.sharedMesh != null)
					{
						CMesh3Df mesh = new CMesh3Df(mesh_filter.sharedMesh);
						_meshSet.Meshes.Add(mesh);
					}

					// Добавляем материал
					MeshRenderer mesh_renderer = transform.GetComponent<MeshRenderer>();
					if(mesh_renderer != null)
					{
						Material[] unity_materials = mesh_renderer.sharedMaterials;
						for (Int32 i = 0; i < unity_materials.Length; i++)
						{
							Material unity_mat = unity_materials[i];
							if(unity_mat != null)
							{
								CMaterial material = new CMaterial(this, unity_mat);
								_materialSet.Materials.Add(material);

								// Получаем все текстуры
								String[] textures_name = unity_mat.GetTexturePropertyNames();
								for (Int32 t = 0; t < textures_name.Length; t++)
								{
									String texture_prop_name = textures_name[t];
									Texture unity_texture = unity_mat.GetTexture(texture_prop_name);
									if(unity_texture != null)
									{
										CTexture texture = new CTexture(material, unity_texture as Texture2D);
										_textureSet.Textures.Add(texture);
									}
								}
							}
						}
					}

					node = new CModel3D(this, parent_node, mesh_filter);
				}
				else
				{
					node = new CNode3D(this, parent_node);
				}

				if (parent_node != null)
				{
					parent_node.Children.Add(node);
				}

				for (Int32 i = 0; i < transform.childCount; i++)
				{
					Transform child_transform = transform.GetChild(i);
					CreateSceneRecursiveFromUnity(child_transform, node);
				}

				return (node);
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ UNITY_EDITOR =============================
#if UNITY_EDITOR
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание сцены из сцены FbxScene
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void CreateSceneFromFbxScene()
			{
				if (_fbxScene == null) return;

				_rootNode = null;
				_meshSet.Meshes.Clear();
				_materialSet.Materials.Clear();

				_name = _fbxScene.GetName();
				_rootNode = CreateSceneRecursiveFromFbx(_fbxScene.GetRootNode(), null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное создание объектов Fbx
			/// </summary>
			/// <param name="fbx_node">Узел сцены Fbx</param>
			/// <param name="parent_node">Родительский узел</param>
			/// <returns>Созданный узел</returns>
			//---------------------------------------------------------------------------------------------------------
			private CNode3D CreateSceneRecursiveFromFbx(Autodesk.Fbx.FbxNode fbx_node, CNode3D parent_node)
			{
				// Если на узле присутсвет меш то сразу создаем корневой узел как модель
				Autodesk.Fbx.FbxNodeAttribute node_attribute = fbx_node.GetNodeAttribute();
				CNode3D node = null;
				if (node_attribute != null && node_attribute.GetAttributeType() == Autodesk.Fbx.FbxNodeAttribute.EType.eMesh)
				{
					// Добавляем меш
					Autodesk.Fbx.FbxMesh fbx_mesh = fbx_node.GetMesh();
					if (fbx_mesh != null)
					{
						CMesh3Df mesh = new CMesh3Df(fbx_mesh);
						_meshSet.Meshes.Add(mesh);
					}

					// Добавляем материал
					Int32 count_material = 1;
					for (Int32 i = 0; i < count_material; i++)
					{
						Autodesk.Fbx.FbxSurfaceMaterial fbx_material = fbx_node.GetMaterial(i);
						if (fbx_material != null)
						{
							CMaterial material = new CMaterial(this, fbx_material);
							_materialSet.Materials.Add(material);
						}
					}

					node = new CModel3D(this, parent_node, fbx_node);
				}
				else
				{
					node = new CNode3D(this, parent_node, fbx_node);
				}

				if (parent_node != null)
				{
					parent_node.Children.Add(node);
				}

				for (Int32 i = 0; i < fbx_node.GetChildCount(); i++)
				{
					Autodesk.Fbx.FbxNode child_node = fbx_node.GetChild(i);
					CreateSceneRecursiveFromFbx(child_node, node);
				}

				return (node);
			}
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================