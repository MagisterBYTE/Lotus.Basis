//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема мешей
// Подраздел: Общее данные
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMesh3D.cs
*		Трехмерная сетка.
*		Реализация структуры представляющий трехмерную сетку.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//---------------------------------------------------------------------------------------------------------------------
#if USE_HELIX
using HelixToolkit.SharpDX.Core;
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
		/** \addtogroup Object3DMeshCommon
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Трехмерная сетка
		/// </summary>
		/// <remarks>
		/// Реализация структуры представляющий трехмерную сетку
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMesh3Df : CEntity3D, IComparable<CMesh3Df>, ICloneable, ILotusMeshOperaiton
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static PropertyChangedEventArgs PropertyArgsLocation = new PropertyChangedEventArgs(nameof(Location));
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal protected Int32 mIndex;
			internal protected Int32 mOrder;
			internal protected Boolean mHasUVMap = true;

			// Параметры геометрии
			internal protected CListVertex3D mVertices;
			internal protected CListTriangle3D mTriangles;
			internal protected CListEdge3D mEdges;

			// Размеры и позиция
			internal protected Vector3Df mLocation;
			internal protected Vector3Df mMinPosition;
			internal protected Vector3Df mMaxPosition;
			internal protected Vector3Df mUp;
			internal protected Vector3Df mRight;
			internal protected Vector3Df mFront;

			// Платформенно-зависимая часть
#if USE_ASSIMP
			internal Assimp.Mesh mAssimpMesh;
#endif
#if USE_HELIX
			internal MeshGeometry3D mHelixMesh;
#endif
#if (UNITY_2017_1_OR_NEWER)
			internal UnityEngine.Mesh mUnityMesh;
#endif
#if UNITY_EDITOR
			internal Autodesk.Fbx.FbxMesh mFbxMesh;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Индекс меша в наборе мешей сцены
			/// </summary>
			[DisplayName("Индекс меша")]
			[Description("Индекс меша в наборе мешей сцены")]
			[Category(XInspectorGroupDesc.Params)]
			[ReadOnly(true)]
			[LotusPropertyOrder(0)]
			public Int32 Index
			{
				get { return (mIndex); }
				set
				{
					mIndex = value;
				}
			}

			/// <summary>
			/// Порядок при сортировке мешей
			/// </summary>
			public Int32 Order
			{
				get { return (mOrder); }
				set { mOrder = value; }
			}

			//
			// ПАРАМЕТРЫ ГЕОМЕТРИИ
			//
			/// <summary>
			/// Список вершин меша
			/// </summary>
			public CListVertex3D Vertices
			{
				get { return (mVertices); }
			}

			/// <summary>
			/// Список треугольников меша
			/// </summary>
			public CListTriangle3D Triangles
			{
				get { return (mTriangles); }
			}

			/// <summary>
			/// Список ребер меша
			/// </summary>
			public CListEdge3D Edges
			{
				get { return (mEdges); }
			}

			/// <summary>
			/// Количество вершин меша
			/// </summary>
			[DisplayName("Кол-во вершин")]
			[Description("Количество вершин меша")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(2)]
			public Int32 CountVertices
			{
				get { return (Vertices.Count); }
			}

			/// <summary>
			/// Количество граней меша
			/// </summary>
			[DisplayName("Кол-во граней")]
			[Description("Количество граней меша")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(3)]
			public Int32 CountFaces
			{
				get { return (Triangles.Count); }
			}

			//
			// РАЗМЕРЫ И ПОЗИЦИЯ
			//
			/// <summary>
			/// Опорная точка меша
			/// </summary>
			public virtual CVertex3Df Pivot
			{
				get { return (mVertices[0]); }
				set
				{
					mVertices.Vertices[0] = value;
					UpdateData();
				}
			}

			/// <summary>
			/// Позиция геометрического центра меша
			/// </summary>
			[DisplayName("Центр")]
			[Description("Позиция геометрического центра меша")]
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

			/// <summary>
			/// Тип структурного элемента меша
			/// </summary>
			public TMeshElement MeshElement { get { return (TMeshElement.Mesh); } }


			/// <summary>
			/// Индекс используемого материала
			/// </summary>
			[DisplayName("Индекс материала")]
			[Description("Индекс используемого материала")]
			[Category(XInspectorGroupDesc.Params)]
			[LotusPropertyOrder(1)]
			public Int32 MaterialIndex
			{
#if USE_ASSIMP
				get { return (mAssimpMesh.MaterialIndex); }
#else
				get { return (0); }
#endif
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMesh3Df()
			{
				mVertices = new CListVertex3D();
				mTriangles = new CListTriangle3D(mVertices);
				mEdges = new CListEdge3D(mVertices);
			}

#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="mesh_name">Имя меша</param>
			/// <param name="assimp_mesh">Меш Assimp</param>
			//---------------------------------------------------------------------------------------------------------
			public CMesh3Df(String mesh_name, Assimp.Mesh assimp_mesh)
			{
				mName = mesh_name;
				mVertices = new CListVertex3D();
				mTriangles = new CListTriangle3D(mVertices);
				mEdges = new CListEdge3D(mVertices);
				mAssimpMesh = assimp_mesh;
				//CreateFromAs(unity_mesh);
			}
#endif

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="unity_mesh">Меш Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public CMesh3Df(UnityEngine.Mesh unity_mesh)
			{
				mVertices = new CListVertex3D();
				mTriangles = new CListTriangle3D(mVertices);
				mEdges = new CListEdge3D(mVertices);
				mUnityMesh = unity_mesh;
				CreateFromUnityMesh(unity_mesh);
			}
#endif
#if (UNITY_EDITOR)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="fbx_mesh">Меш Fbx</param>
			//---------------------------------------------------------------------------------------------------------
			public CMesh3Df(Autodesk.Fbx.FbxMesh fbx_mesh)
			{
				mVertices = new CListVertex3D();
				mTriangles = new CListTriangle3D(mVertices);
				mEdges = new CListEdge3D(mVertices);
				mFbxMesh = fbx_mesh;
				mName = fbx_mesh.GetName();
				if(mName.IsExists() == false)
				{
					mName = fbx_mesh.GetInitialName();
				}
				//CreateFromUnityMesh(unity_mesh);
				//asdasd
			}
#endif
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение мешей для упорядочивания
			/// </summary>
			/// <param name="other">Меш</param>
			/// <returns>Статус сравнения мешей</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CMesh3Df other)
			{
				if (mOrder > other.mOrder)
				{
					return 1;
				}
				else
				{
					if (mOrder < other.mOrder)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование меша
			/// </summary>
			/// <returns>Копия меша</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Object Clone()
			{
				return Duplicate();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя меша</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return mName;
			}
			#endregion

			#region ======================================= ILotusMeshOperaiton - МЕТОДЫ ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Смещение вершин
			/// </summary>
			/// <param name="offset">Вектор смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public void Move(Vector3Df offset)
			{
				mVertices.Move(offset);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин меша
			/// </summary>
			/// <param name="rotation">Кватернион вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public void Rotate(Quaternion3Df rotation)
			{
				mVertices.Rotate(rotation);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин меша вокруг оси X
			/// </summary>
			/// <param name="angle">Угол в градусах</param>
			/// <param name="is_center">Относительно геометрического центра</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateFromX(Single angle, Boolean is_center)
			{
				mVertices.RotateFromX(angle, is_center);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин меша вокруг оси Y
			/// </summary>
			/// <param name="angle">Угол в градусах</param>
			/// <param name="is_center">Относительно геометрического центра</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateFromY(Single angle, Boolean is_center)
			{
				mVertices.RotateFromY(angle, is_center);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин меша вокруг оси Z
			/// </summary>
			/// <param name="angle">Угол в градусах</param>
			/// <param name="is_center">Относительно геометрического центра</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateFromZ(Single angle, Boolean is_center)
			{
				mVertices.RotateFromZ(angle, is_center);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Масштабирование вершин меша
			/// </summary>
			/// <param name="scale">Масштаб</param>
			//---------------------------------------------------------------------------------------------------------
			public void Scale(Single scale)
			{
				mVertices.Scale(scale);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Масштабирование вершин меша
			/// </summary>
			/// <param name="scale">Масштаб</param>
			//---------------------------------------------------------------------------------------------------------
			public void Scale(Vector3Df scale)
			{
				mVertices.Scale(scale);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить нормали
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void FlipNormals()
			{
				mVertices.FlipNormals();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить развёртку текстурных координат по горизонтали 
			/// </summary>
			/// <param name="channel">Канал</param>
			//---------------------------------------------------------------------------------------------------------
			public void FlipUVHorizontally(Int32 channel = 0)
			{
				mVertices.FlipUVHorizontally(channel);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить развёртку текстурных координат по вертикали 
			/// </summary>
			/// <param name="channel">Канал</param>
			//---------------------------------------------------------------------------------------------------------
			public void FlipUVVertically(Int32 channel = 0)
			{
				mVertices.FlipUVVertically(channel);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление меша
			/// </summary>
			/// <param name="mesh">Меш</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Add(CMesh3Df mesh)
			{
				for (Int32 i = 0; i < mesh.Triangles.Count; i++)
				{
					mTriangles.Add(mesh.mTriangles[i].GetTriangleOffset(mVertices.Count));
				}

				Vertices.AddItems(mesh.mVertices);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование меша
			/// </summary>
			/// <returns>Меш</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CMesh3Df Duplicate()
			{
				CMesh3Df mesh = new CMesh3Df();
				mesh.Name = mName + "(Copy)";
				mesh.Order = mOrder;
				mesh.mVertices.Add(mVertices);
				mesh.mTriangles.Add(mTriangles);

				return mesh;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка всех данных меша
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Clear()
			{
				mVertices.Clear();
				mTriangles.Clear();
				mEdges.Clear();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Разворачивание грани меша в обратную сторону (относительно текущей нормали)
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void FlipFaces()
			{
				FlipTriangles();
				FlipNormals();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить порядок вершин
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void FlipTriangles()
			{
				for (Int32 i = 0; i < mTriangles.Count; i += 3)
				{
					//mTriangles.Vertices[i].F();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных меша
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UpdateData()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление нормалей для меша
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ComputeNormals()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление текстурных координат (развертки) для меша
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ComputeUVMap(Int32 channel = 0)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Центрирование меша
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Centering()
			{
				mVertices.Move(-mVertices.GetCentredPosition());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление локального ограничивающего объема
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ComputeLocalBoundingBox()
			{
				mMinPosition = new Vector3Df(1e10f, 1e10f, 1e10f);
				mMaxPosition = new Vector3Df(-1e10f, -1e10f, -1e10f);

				for (Int32 i = 0; i < mVertices.Count; i++)
				{
					Vector3Df tmp = mVertices[i].Position;

					mMinPosition.X = Math.Min(mMinPosition.X, tmp.X);
					mMinPosition.Y = Math.Min(mMinPosition.Y, tmp.Y);
					mMinPosition.Z = Math.Min(mMinPosition.Z, tmp.Z);

					mMaxPosition.X = Math.Max(mMaxPosition.X, tmp.X);
					mMaxPosition.Y = Math.Max(mMaxPosition.Y, tmp.Y);
					mMaxPosition.Z = Math.Max(mMaxPosition.Z, tmp.Z);
				}

				mLocation.X = (mMinPosition.X + mMaxPosition.X) / 2.0f;
				mLocation.Y = (mMinPosition.Y + mMaxPosition.Y) / 2.0f;
				mLocation.Z = (mMinPosition.Z + mMaxPosition.Z) / 2.0f;
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ВЕРШИНАМИ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка позиции вершины
			/// </summary>
			/// <param name="index">Индекс вершины</param>
			/// <param name="position">Позиция вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetVertexPosition(Int32 index, Vector3Df position)
			{
				mVertices.Vertices[index].Position = position;
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С НОРМАЛЯМИ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение средней(сглаженной) нормали к указанной вершине
			/// </summary>
			/// <param name="index_vertex">Индекс вершины</param>
			/// <returns>Сглаженная нормаль</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df GetSmoothNormal(Int32 index_vertex)
			{
				// Получаем список треугольников к указанной вершине
				List<CTriangle3Df> triangles = mTriangles.GetTrianglesOfIndexVertex(index_vertex);

				// Суммируем нормали
				Vector3Df normal = Vector3Df.Zero;
				for (Int32 i = 0; i < triangles.Count; i++)
				{
					normal += (triangles[i].GetNormal(index_vertex, mVertices));
				}

				// Усредняем
				normal /= triangles.Count;
				normal.Normalize();
				return (normal);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сглаживание нормалей
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void SmoothNormals()
			{
				for (Int32 i = 0; i < mVertices.Count; i++)
				{
					mVertices.Vertices[i].Normal = GetSmoothNormal(i);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ HELIX ====================================
#if USE_HELIX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание меша
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void CreateFromHelixMesh()
			{
                mHelixMesh = new MeshGeometry3D();

                // Вершины
                mHelixMesh.Positions = new Vector3Collection(mAssimpMesh.VertexCount);
                for (Int32 i = 0; i < mAssimpMesh.VertexCount; i++)
                {
                    mHelixMesh.Positions.Add(mAssimpMesh.Vertices[i].ToShVector3D());
                }

                // Нормали
                mHelixMesh.Normals = new Vector3Collection(mAssimpMesh.Normals.Count);
                for (Int32 i = 0; i < mAssimpMesh.Normals.Count; i++)
                {
                    mHelixMesh.Normals.Add(mAssimpMesh.Normals[i].ToShVector3D());
                }

                // Текстурные координаты
                if (mAssimpMesh.TextureCoordinateChannels != null && mAssimpMesh.TextureCoordinateChannels.Length > 0)
                {
                    List<Assimp.Vector3D> tex_coord_1 = mAssimpMesh.TextureCoordinateChannels[0];
                    mHelixMesh.TextureCoordinates = new Vector2Collection(tex_coord_1.Count);
                    for (Int32 i = 0; i < tex_coord_1.Count; i++)
                    {
                        mHelixMesh.TextureCoordinates.Add(tex_coord_1[i].ToShVector2D());
                    }
                }

                // Индексы
                // Общее количество индексов
                Int32 total_index = 0;
                for (Int32 i = 0; i < mAssimpMesh.FaceCount; i++)
                {
                    total_index += mAssimpMesh.Faces[i].Indices.Count;
                }

                mHelixMesh.TriangleIndices = new IntCollection(total_index);
                for (Int32 i = 0; i < mAssimpMesh.FaceCount; i++)
                {
                    List<Int32> indices = mAssimpMesh.Faces[i].Indices;
                    for (Int32 j = 0; j < indices.Count; j++)
                    {
                        mHelixMesh.TriangleIndices.Add(indices[j]);
                    }
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
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Согласование меша из меша Unity
			/// </summary>
			/// <param name="unity_mesh">Меш Unity</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateFromUnityMesh(UnityEngine.Mesh unity_mesh)
			{
				mName = unity_mesh.name;
				//
				// Vertices
				//
				UnityEngine.Vector3[] vertices = unity_mesh.vertices;
				UnityEngine.Vector3[] normals = unity_mesh.normals;
				UnityEngine.Vector2[] uv = unity_mesh.uv;
				UnityEngine.Vector2[] uv2 = unity_mesh.uv2;

				if((uv2 != null && uv2.Length > 0) && (uv != null && uv.Length > 0))
				{
					mHasUVMap = true;
					for (Int32 i = 0; i < vertices.Length; i++)
					{
						mVertices.AddVertex(vertices[i], normals[i], uv[i], uv2[i]);
					}
				}
				else
				{
					if ((uv != null && uv.Length > 0))
					{
						mHasUVMap = true;
						for (Int32 i = 0; i < vertices.Length; i++)
						{
							mVertices.AddVertex(vertices[i], normals[i], uv[i]);
						}
					}
					else
					{
						mHasUVMap = false;
						for (Int32 i = 0; i < vertices.Length; i++)
						{
							mVertices.AddVertex(vertices[i], normals[i]);
						}
					}
				}

				//
				// Triangles
				//
				for (Int32 ic = 0; ic < unity_mesh.subMeshCount; ic++)
				{
					Int32[] triangles = unity_mesh.GetTriangles(ic);
					for (Int32 t = 0; t < triangles.Length / 3; t += 3)
					{
						mTriangles.AddTriangle(triangles[t], triangles[t + 1], triangles[t + 2]);
					}
				}


				ComputeLocalBoundingBox();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в тип <see cref="UnityEngine.Mesh"/>
			/// </summary>
			/// <returns>Объект <see cref="UnityEngine.Mesh"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public UnityEngine.Mesh ToUnityMesh()
			{
				var mesh = new UnityEngine.Mesh();
				FillUnityMesh(ref mesh);
				return mesh;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в тип <see cref="UnityEngine.Mesh"/>
			/// </summary>
			/// <param name="mesh">Объект <see cref="UnityEngine.Mesh"/></param>
			//---------------------------------------------------------------------------------------------------------
			public void ToUnityMesh(ref UnityEngine.Mesh mesh)
			{
				if (mesh == null)
				{
					throw new ArgumentNullException(nameof(mesh));
				}
				mesh.Clear(false);
				FillUnityMesh(ref mesh);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных объекта <see cref="UnityEngine.Mesh"/>
			/// </summary>
			/// <param name="mesh">Объект <see cref="UnityEngine.Mesh"/></param>
			//---------------------------------------------------------------------------------------------------------
			private void FillUnityMesh(ref UnityEngine.Mesh mesh)
			{
				if (mVertices.Count > 65000)
				{
					UnityEngine.Debug.LogError("A mesh may not have more than 65000 vertices. Vertex count: " + mVertices.Count);
				}
				mesh.name = mName;

				//
				// Vertices
				//
				List<UnityEngine.Vector3> vertices = new List<UnityEngine.Vector3>(mVertices.Count);
				for (Int32 i = 0; i < mVertices.Count; i++)
				{
					vertices.Add(mVertices.Vertices[i].Position);
				}
				mesh.SetVertices(vertices);

				//
				// Normals
				//
				List<UnityEngine.Vector3> normals = new List<UnityEngine.Vector3>(mVertices.Count);
				for (Int32 i = 0; i < mVertices.Count; i++)
				{
					normals.Add(mVertices.Vertices[i].Normal);
				}
				mesh.SetNormals(normals);

				//
				// Triangles
				// 
				List<Int32> triangles = new List<Int32>(mTriangles.Count * 3);
				for (Int32 i = 0; i < mTriangles.Count; i++)
				{
					triangles.Add(mTriangles[i].IndexVertex0);
					triangles.Add(mTriangles[i].IndexVertex1);
					triangles.Add(mTriangles[i].IndexVertex2);
				}
				mesh.SetTriangles(triangles, 0);


				//
				// UV
				//
				if (mHasUVMap)
				{
					List<UnityEngine.Vector2> uvs = new List<UnityEngine.Vector2>(mVertices.Count);
					for (Int32 i = 0; i < mVertices.Count; i++)
					{
						uvs.Add(mVertices.Vertices[i].UV);
					}
					mesh.SetUVs(0, uvs);
				}

				//if (mChannelUV.Count > 0) mesh.SetUVs(0, mChannelUV.ConvertToVector2List());
				//if (mChannelUV2.Count > 0) mesh.SetUVs(1, mChannelUV2.ConvertToVector2List());
				//if (mChannelUV3.Count > 0) mesh.SetUVs(2, mChannelUV3.ConvertToVector2List());

				//if (mTangents.Count > 0)
				//{
				//	mesh.RecalculateTangents();
				//}

				mesh.RecalculateBounds();
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ ПЛАТФОРМЫ G3 =======================================
#if GEOMETRY3SHARP || UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных с меша типа <see cref="g3.DMesh3"/>
			/// </summary>
			/// <param name="mesh">Меш</param>
			//---------------------------------------------------------------------------------------------------------
			public void FromDMesh3(g3.DMesh3 mesh)
			{
				mVertices.Clear();

				for (Int32 iv = 0; iv < mesh.VertexCount; iv++)
				{
					g3.NewVertexInfo vertex_info = new g3.NewVertexInfo();
					mesh.GetVertex(iv, ref vertex_info, true, false, true);

					Vector3Df pos = new Vector3Df((Single)vertex_info.v.x, (Single)vertex_info.v.y, (Single)vertex_info.v.z);
					Vector3Df norm = new Vector3Df(vertex_info.n.x, vertex_info.n.y, vertex_info.n.z);
					Vector2Df uv = new Vector2Df(vertex_info.uv.x, vertex_info.uv.y);
					mVertices.AddVertex(pos, norm, uv);
				}

				mTriangles.Clear();

				for (Int32 it = 0; it < mesh.TriangleCount; it++)
				{
					g3.Index3i triangle = mesh.GetTriangle(it);
					mTriangles.AddTriangle(triangle.a, triangle.b, triangle.c);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в тип <see cref="g3.DMesh3"/>
			/// </summary>
			/// <returns>Объект <see cref="g3.DMesh3"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public g3.DMesh3 ToDMesh3()
			{
				g3.DMesh3 mesh = new g3.DMesh3(g3.MeshComponents.VertexNormals | g3.MeshComponents.VertexUVs);
				for (Int32 iv = 0; iv < mVertices.Count; iv++)
				{
					g3.Vector3d pos = new g3.Vector3d(mVertices[iv].Position.X, mVertices[iv].Position.Y, mVertices[iv].Position.Z);
					g3.Vector3f norm = new g3.Vector3f(mVertices[iv].Normal.X, mVertices[iv].Normal.Y, mVertices[iv].Normal.Z);
					g3.Vector2f uv = new g3.Vector2f(mVertices[iv].UV.X, mVertices[iv].UV.Y);
					g3.NewVertexInfo vertex = new g3.NewVertexInfo();
					vertex.v = pos;
					vertex.n = norm;
					vertex.uv = uv;
					vertex.bHaveN = true;
					vertex.bHaveUV = true;
					mesh.AppendVertex(vertex);
				}

				for (Int32 it = 0; it < mTriangles.Count; it++)
				{
					g3.Index3i triangle = new g3.Index3i(mTriangles[it].IndexVertex0,
						mTriangles[it].IndexVertex1, mTriangles[it].IndexVertex2);
					mesh.AppendTriangle(triangle);
				}

				return (mesh);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// 
			/// </summary>
			/// <param name="numcells"></param>
			/// <param name="max_offset"></param>
			/// <returns></returns>
			//---------------------------------------------------------------------------------------------------------
			public g3.BoundedImplicitFunction3d ToBoundedImplicitMesh(Int32 numcells, Double max_offset)
			{
				g3.DMesh3 mesh = ToDMesh3();

				Double meshCellsize = mesh.CachedBounds.MaxDim / numcells;
				g3.MeshSignedDistanceGrid levelSet = new g3.MeshSignedDistanceGrid(mesh, meshCellsize);
				levelSet.ExactBandWidth = (int)(max_offset / meshCellsize) + 1;
				levelSet.Compute();
				return new g3.DenseGridTrilinearImplicit(levelSet.Grid, levelSet.GridOrigin, levelSet.CellSize);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="root"></param>
			/// <param name="numcells"></param>
			/// <returns></returns>
			public g3.DMesh3 GenerateMeshFromImplicit(g3.BoundedImplicitFunction3d root, Int32 numcells)
			{
				g3.MarchingCubes c = new g3.MarchingCubes();
				c.Implicit = root;
				c.RootMode = g3.MarchingCubes.RootfindingModes.LerpSteps;      // cube-edge convergence method
				c.RootModeSteps = 5;                                        // number of iterations
				c.Bounds = root.Bounds();
				c.CubeSize = c.Bounds.MaxDim / numcells;
				c.Bounds.Expand(3 * c.CubeSize);                            // leave a buffer of cells
				c.Generate();
				g3.MeshNormals.QuickCompute(c.Mesh);                           // generate normals
				return (c.Mesh);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// 
			/// </summary>
			/// <param name="mesh"></param>
			/// <returns></returns>
			//---------------------------------------------------------------------------------------------------------
			public CMesh3Df Intersection(CMesh3Df mesh)
			{
				g3.BoundedImplicitFunction3d one = this.ToBoundedImplicitMesh(64, 1);
				g3.BoundedImplicitFunction3d two = mesh.ToBoundedImplicitMesh(64, 1);

				g3.ImplicitUnion3d intersection = new g3.ImplicitUnion3d();
				intersection.A = one;
				intersection.B = two;

				g3.DMesh3 dmesh = GenerateMeshFromImplicit(intersection, 128);

				CMesh3Df intersect_mesh = new CMesh3Df();
				intersect_mesh.Name = "Intersect";
				intersect_mesh.FromDMesh3(dmesh);

				return (intersect_mesh);
			}
#endif
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Набор всех трехмерных сеток(мешей) в сцене
		/// </summary>
		/// <remarks>
		/// Предназначен для логического группирования всех мешей в обозревателе сцены
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CMeshSet : CEntity3D
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal ListArray<CMesh3Df> mMeshes;
			protected internal CScene3D mOwnerScene;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наблюдаемая коллекция трехмерных сеток
			/// </summary>
			[Browsable(false)]
			public ListArray<CMesh3Df> Meshes
			{
				get { return (mMeshes); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="owner_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CMeshSet(CScene3D owner_scene)
			{
				mOwnerScene = owner_scene;
				mName = "Сетки";
				mMeshes = new ListArray<CMesh3Df>();
				mMeshes.IsNotify = true;
			}

#if USE_ASSIMP
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="assimp_scene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CMeshSet(Assimp.Scene assimp_scene)
			{
				mName = "Сетки";
				mMeshes = new ListArray<CMesh3Df>();

				// Устанавливаем меши
				for (Int32 i = 0; i < assimp_scene.MeshCount; i++)
				{
					Assimp.Mesh mesh = assimp_scene.Meshes[i];
					mMeshes.Add(new CMesh3Df("Mesh_" + i.ToString(), mesh));
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
				return (mMeshes.Count);
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
				return (mMeshes[index]);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
#if USE_HELIX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание всех мешей
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void CreateHelixMeshes()
			{
				for (Int32 i = 0; i < mMeshes.Count; i++)
				{
					//mMeshes[i].CreateHelixMesh();
				}
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