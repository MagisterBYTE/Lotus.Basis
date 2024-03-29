using System;
using System.Collections.Generic;
using System.ComponentModel;

#if USE_HELIX
using HelixToolkit.SharpDX.Core;
#endif

using Lotus.Core;
using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshCommon
	*@{*/
    /// <summary>
    /// Трехмерная сетка.
    /// </summary>
    /// <remarks>
    /// Реализация структуры представляющий трехмерную сетку.
    /// </remarks>
    [Serializable]
    public class Mesh3Df : Entity3D, IComparable<Mesh3Df>, ICloneable, ILotusMeshOperaiton
    {
        #region Static fields
        protected static PropertyChangedEventArgs PropertyArgsLocation = new(nameof(Location));
        #endregion

        #region Fields
        // Основные параметры
        protected internal int _index;
        protected internal int _order;
        protected internal bool _hasUVMap = true;

        // Параметры геометрии
        protected internal ListVertex3D _vertices;
        protected internal ListTriangle3D _triangles;
        protected internal ListEdge3D _edges;

        // Размеры и позиция
        protected internal Vector3Df _location;
        protected internal Vector3Df _minPosition;
        protected internal Vector3Df _maxPosition;
        protected internal Vector3Df _up;
        protected internal Vector3Df _right;
        protected internal Vector3Df _front;

        // Платформенно-зависимая часть
#if USE_ASSIMP
		protected internal Assimp.Mesh? _assimpMesh;
#endif
#if USE_HELIX
		protected internal MeshGeometry3D? _helixMesh;
#endif
#if UNITY_2017_1_OR_NEWER
		protected internal UnityEngine.Mesh _unityMesh;
#endif
#if UNITY_EDITOR
		protected internal Autodesk.Fbx.FbxMesh _fbxMesh;
#endif
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Индекс меша в наборе мешей сцены.
        /// </summary>
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
            }
        }

        /// <summary>
        /// Порядок при сортировке мешей.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        //
        // ПАРАМЕТРЫ ГЕОМЕТРИИ
        //
        /// <summary>
        /// Список вершин меша.
        /// </summary>
        public ListVertex3D Vertices
        {
            get { return _vertices; }
        }

        /// <summary>
        /// Список треугольников меша.
        /// </summary>
        public ListTriangle3D Triangles
        {
            get { return _triangles; }
        }

        /// <summary>
        /// Список ребер меша.
        /// </summary>
        public ListEdge3D Edges
        {
            get { return _edges; }
        }

        /// <summary>
        /// Количество вершин меша.
        /// </summary>
        public int CountVertices
        {
            get { return Vertices.Count; }
        }

        /// <summary>
        /// Количество граней меша.
        /// </summary>
        public int CountFaces
        {
            get { return Triangles.Count; }
        }

        //
        // РАЗМЕРЫ И ПОЗИЦИЯ
        //
        /// <summary>
        /// Опорная точка меша.
        /// </summary>
        public virtual Vertex3Df Pivot
        {
            get { return _vertices[0]; }
            set
            {
                _vertices.Vertices[0] = value;
                UpdateData();
            }
        }

        /// <summary>
        /// Позиция геометрического центра меша.
        /// </summary>
        public Vector3Df Location
        {
            get { return _location; }
        }

        /// <summary>
        /// Размер меша по оси X.
        /// </summary>
        public float SizeX
        {
            get { return _maxPosition.X - _minPosition.X; }
        }

        /// <summary>
        /// Размер меша по оси Y.
        /// </summary>
        public float SizeY
        {
            get { return _maxPosition.Y - _minPosition.Y; }
        }

        /// <summary>
        /// Размер меша по оси Z.
        /// </summary>
        public float SizeZ
        {
            get { return _maxPosition.Z - _minPosition.Z; }
        }

        /// <summary>
        /// Тип структурного элемента меша.
        /// </summary>
        public TMeshElement MeshElement { get { return TMeshElement.Mesh; } }

        /// <summary>
        /// Индекс используемого материала.
        /// </summary>
        public int MaterialIndex
        {
#if USE_ASSIMP
			get { return _assimpMesh == null ? 0 : _assimpMesh.MaterialIndex; }
#else
            get { return 0; }
#endif
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public Mesh3Df()
        {
            _vertices = new ListVertex3D();
            _triangles = new ListTriangle3D(_vertices);
            _edges = new ListEdge3D(_vertices);
        }

#if USE_ASSIMP
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="mesh_name">Имя меша.</param>
		/// <param name="assimp_mesh">Меш Assimp.</param>
		public Mesh3Df(string mesh_name, Assimp.Mesh assimp_mesh)
		{
			_name = mesh_name;
			_vertices = new ListVertex3D();
			_triangles = new ListTriangle3D(_vertices);
			_edges = new ListEdge3D(_vertices);
			_assimpMesh = assimp_mesh;
			//CreateFromAs(unity_mesh);
		}
#endif

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="unity_mesh">Меш Unity.</param>
		public CMesh3Df(UnityEngine.Mesh unity_mesh)
		{
			_vertices = new CListVertex3D();
			_triangles = new CListTriangle3D(_vertices);
			_edges = new CListEdge3D(_vertices);
			_unityMesh = unity_mesh;
			CreateFromUnityMesh(unity_mesh);
		}
#endif
#if UNITY_EDITOR
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="fbx_mesh">Меш Fbx.</param>
		public CMesh3Df(Autodesk.Fbx.FbxMesh fbx_mesh)
		{
			_vertices = new CListVertex3D();
			_triangles = new CListTriangle3D(_vertices);
			_edges = new CListEdge3D(_vertices);
			_fbxMesh = fbx_mesh;
			_name = fbx_mesh.GetName();
			if(_name.IsExists() == false)
			{
				_name = fbx_mesh.GetInitialName();
			}
			//CreateFromUnityMesh(unity_mesh);
			//asdasd
		}
#endif
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение мешей для упорядочивания.
        /// </summary>
        /// <param name="other">Меш.</param>
        /// <returns>Статус сравнения мешей.</returns>
        public int CompareTo(Mesh3Df? other)
        {
            if (other == null) return 0;

            if (_order > other._order)
            {
                return 1;
            }
            else
            {
                if (_order < other._order)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Полное копирование меша.
        /// </summary>
        /// <returns>Копия меша.</returns>
        public virtual object Clone()
        {
            return Duplicate();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Имя меша.</returns>
        public override string ToString()
        {
            return _name;
        }
        #endregion

        #region ILotusMeshOperaiton methods
        /// <summary>
        /// Смещение вершин.
        /// </summary>
        /// <param name="offset">Вектор смещения.</param>
        public void Move(Vector3Df offset)
        {
            _vertices.Move(offset);
        }

        /// <summary>
        /// Врашение вершин меша.
        /// </summary>
        /// <param name="rotation">Кватернион вращения.</param>
        public void Rotate(Quaternion3Df rotation)
        {
            _vertices.Rotate(rotation);
        }

        /// <summary>
        /// Врашение вершин меша вокруг оси X.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <param name="isCenter">Относительно геометрического центра.</param>
        public void RotateFromX(float angle, bool isCenter)
        {
            _vertices.RotateFromX(angle, isCenter);
        }

        /// <summary>
        /// Врашение вершин меша вокруг оси Y.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <param name="isCenter">Относительно геометрического центра.</param>
        public void RotateFromY(float angle, bool isCenter)
        {
            _vertices.RotateFromY(angle, isCenter);
        }

        /// <summary>
        /// Врашение вершин меша вокруг оси Z.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <param name="isCenter">Относительно геометрического центра.</param>
        public void RotateFromZ(float angle, bool isCenter)
        {
            _vertices.RotateFromZ(angle, isCenter);
        }

        /// <summary>
        /// Масштабирование вершин меша.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(float scale)
        {
            _vertices.Scale(scale);
        }

        /// <summary>
        /// Масштабирование вершин меша.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(Vector3Df scale)
        {
            _vertices.Scale(scale);
        }

        /// <summary>
        /// Обратить нормали.
        /// </summary>
        public void FlipNormals()
        {
            _vertices.FlipNormals();
        }

        /// <summary>
        /// Обратить развёртку текстурных координат по горизонтали.
        /// </summary>
        /// <param name="channel">Канал.</param>
        public void FlipUVHorizontally(int channel = 0)
        {
            _vertices.FlipUVHorizontally(channel);
        }

        /// <summary>
        /// Обратить развёртку текстурных координат по вертикали.
        /// </summary>
        /// <param name="channel">Канал.</param>
        public void FlipUVVertically(int channel = 0)
        {
            _vertices.FlipUVVertically(channel);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление меша.
        /// </summary>
        /// <param name="mesh">Меш.</param>
        public virtual void Add(Mesh3Df mesh)
        {
            for (var i = 0; i < mesh.Triangles.Count; i++)
            {
                _triangles.Add(mesh._triangles[i].GetTriangleOffset(_vertices.Count));
            }

            Vertices.AddItems(mesh._vertices);
        }

        /// <summary>
        /// Дублирование меша.
        /// </summary>
        /// <returns>Меш.</returns>
        public virtual Mesh3Df Duplicate()
        {
            var mesh = new Mesh3Df
            {
                Name = _name + "(Copy)",
                Order = _order
            };
            mesh._vertices.Add(_vertices);
            mesh._triangles.Add(_triangles);

            return mesh;
        }

        /// <summary>
        /// Очистка всех данных меша.
        /// </summary>
        public virtual void Clear()
        {
            _vertices.Clear();
            _triangles.Clear();
            _edges.Clear();
        }

        /// <summary>
        /// Разворачивание грани меша в обратную сторону (относительно текущей нормали).
        /// </summary>
        public virtual void FlipFaces()
        {
            FlipTriangles();
            FlipNormals();
        }

        /// <summary>
        /// Обратить порядок вершин.
        /// </summary>
        public virtual void FlipTriangles()
        {
            for (var i = 0; i < _triangles.Count; i += 3)
            {
                //_triangles.Vertices[i].F();
            }
        }

        /// <summary>
        /// Обновление данных меша.
        /// </summary>
        public virtual void UpdateData()
        {

        }

        /// <summary>
        /// Вычисление нормалей для меша.
        /// </summary>
        public virtual void ComputeNormals()
        {

        }

        /// <summary>
        /// Вычисление текстурных координат (развертки) для меша.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public virtual void ComputeUVMap(int channel = 0)
        {

        }

        /// <summary>
        /// Центрирование меша.
        /// </summary>
        public virtual void Centering()
        {
            _vertices.Move(-_vertices.GetCentredPosition());
        }

        /// <summary>
        /// Вычисление локального ограничивающего объема.
        /// </summary>
        public virtual void ComputeLocalBoundingBox()
        {
            _minPosition = new Vector3Df(1e10f, 1e10f, 1e10f);
            _maxPosition = new Vector3Df(-1e10f, -1e10f, -1e10f);

            for (var i = 0; i < _vertices.Count; i++)
            {
                var tmp = _vertices[i].Position;

                _minPosition.X = Math.Min(_minPosition.X, tmp.X);
                _minPosition.Y = Math.Min(_minPosition.Y, tmp.Y);
                _minPosition.Z = Math.Min(_minPosition.Z, tmp.Z);

                _maxPosition.X = Math.Max(_maxPosition.X, tmp.X);
                _maxPosition.Y = Math.Max(_maxPosition.Y, tmp.Y);
                _maxPosition.Z = Math.Max(_maxPosition.Z, tmp.Z);
            }

            _location.X = (_minPosition.X + _maxPosition.X) / 2.0f;
            _location.Y = (_minPosition.Y + _maxPosition.Y) / 2.0f;
            _location.Z = (_minPosition.Z + _maxPosition.Z) / 2.0f;
        }
        #endregion

        #region Vertex methods
        /// <summary>
        /// Установка позиции вершины.
        /// </summary>
        /// <param name="index">Индекс вершины.</param>
        /// <param name="position">Позиция вершины.</param>
        public virtual void SetVertexPosition(int index, Vector3Df position)
        {
            _vertices.Vertices[index].Position = position;
        }
        #endregion

        #region Normals methods
        /// <summary>
        /// Получение средней(сглаженной) нормали к указанной вершине.
        /// </summary>
        /// <param name="indexVertex">Индекс вершины.</param>
        /// <returns>Сглаженная нормаль.</returns>
        public Vector3Df GetSmoothNormal(int indexVertex)
        {
            // Получаем список треугольников к указанной вершине
            var triangles = _triangles.GetTrianglesOfIndexVertex(indexVertex);

            // Суммируем нормали
            var normal = Vector3Df.Zero;
            for (var i = 0; i < triangles.Count; i++)
            {
                normal += triangles[i].GetNormal(indexVertex, _vertices);
            }

            // Усредняем
            normal /= triangles.Count;
            normal.Normalize();
            return normal;
        }

        /// <summary>
        /// Сглаживание нормалей.
        /// </summary>
        public void SmoothNormals()
        {
            for (var i = 0; i < _vertices.Count; i++)
            {
                _vertices.Vertices[i].Normal = GetSmoothNormal(i);
            }
        }
        #endregion

        #region Helix methods
#if USE_HELIX
		/// <summary>
		/// Создание меша.
		/// </summary>
		public void CreateFromHelixMesh()
		{
            _helixMesh = new MeshGeometry3D();

            // Вершины
            _helixMesh.Positions = new Vector3Collection(_assimpMesh!.VertexCount);
            for (var i = 0; i < _assimpMesh.VertexCount; i++)
            {
                _helixMesh.Positions.Add(_assimpMesh.Vertices[i].ToShVector3D());
            }

            // Нормали
            _helixMesh.Normals = new Vector3Collection(_assimpMesh!.Normals.Count);
            for (var i = 0; i < _assimpMesh.Normals.Count; i++)
            {
                _helixMesh.Normals.Add(_assimpMesh.Normals[i].ToShVector3D());
            }

            // Текстурные координаты
            if (_assimpMesh.TextureCoordinateChannels != null && _assimpMesh.TextureCoordinateChannels.Length > 0)
            {
                List<Assimp.Vector3D> tex_coord_1 = _assimpMesh.TextureCoordinateChannels[0];
                _helixMesh.TextureCoordinates = new Vector2Collection(tex_coord_1.Count);
                for (var i = 0; i < tex_coord_1.Count; i++)
                {
                    _helixMesh.TextureCoordinates.Add(tex_coord_1[i].ToShVector2D());
                }
            }

            // Индексы
            // Общее количество индексов
            var total_index = 0;
            for (var i = 0; i < _assimpMesh.FaceCount; i++)
            {
                total_index += _assimpMesh.Faces[i].Indices.Count;
            }

            _helixMesh.TriangleIndices = new IntCollection(total_index);
            for (var i = 0; i < _assimpMesh.FaceCount; i++)
            {
                List<int> indices = _assimpMesh.Faces[i].Indices;
                for (var j = 0; j < indices.Count; j++)
                {
                    _helixMesh.TriangleIndices.Add(indices[j]);
                }
            }
        }
#endif
        #endregion

        #region Unity methods
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Согласование меша из меша Unity.
		/// </summary>
		/// <param name="unity_mesh">Меш Unity.</param>
		public void CreateFromUnityMesh(UnityEngine.Mesh unity_mesh)
		{
			_name = unity_mesh.name;
			//
			// Vertices
			//
			UnityEngine.Vector3[] vertices = unity_mesh.vertices;
			UnityEngine.Vector3[] normals = unity_mesh.normals;
			UnityEngine.Vector2[] uv = unity_mesh.uv;
			UnityEngine.Vector2[] uv2 = unity_mesh.uv2;

			if((uv2 != null && uv2.Length > 0) && (uv != null && uv.Length > 0))
			{
				_hasUVMap = true;
				for (Int32 i = 0; i < vertices.Length; i++)
				{
					_vertices.AddVertex(vertices[i], normals[i], uv[i], uv2[i]);
				}
			}
			else
			{
				if ((uv != null && uv.Length > 0))
				{
					_hasUVMap = true;
					for (Int32 i = 0; i < vertices.Length; i++)
					{
						_vertices.AddVertex(vertices[i], normals[i], uv[i]);
					}
				}
				else
				{
					_hasUVMap = false;
					for (Int32 i = 0; i < vertices.Length; i++)
					{
						_vertices.AddVertex(vertices[i], normals[i]);
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
					_triangles.AddTriangle(triangles[t], triangles[t + 1], triangles[t + 2]);
				}
			}


			ComputeLocalBoundingBox();
		}

		/// <summary>
		/// Преобразование в тип <see cref="UnityEngine.Mesh"/>.
		/// </summary>
		/// <returns>Объект <see cref="UnityEngine.Mesh"/>.</returns>
		public UnityEngine.Mesh ToUnityMesh()
		{
			var mesh = new UnityEngine.Mesh();
			FillUnityMesh(ref mesh);
			return mesh;
		}

		/// <summary>
		/// Преобразование в тип <see cref="UnityEngine.Mesh"/>.
		/// </summary>
		/// <param name="mesh">Объект <see cref="UnityEngine.Mesh"/>.</param>
		public void ToUnityMesh(ref UnityEngine.Mesh mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException(nameof(mesh));
			}
			mesh.Clear(false);
			FillUnityMesh(ref mesh);
		}

		/// <summary>
		/// Заполнение данных объекта <see cref="UnityEngine.Mesh"/>.
		/// </summary>
		/// <param name="mesh">Объект <see cref="UnityEngine.Mesh"/>.</param>
		private void FillUnityMesh(ref UnityEngine.Mesh mesh)
		{
			if (_vertices.Count > 65000)
			{
				UnityEngine.Debug.LogError("A mesh may not have more than 65000 vertices. Vertex count: " + _vertices.Count);
			}
			mesh.name = _name;

			//
			// Vertices
			//
			List<UnityEngine.Vector3> vertices = new List<UnityEngine.Vector3>(_vertices.Count);
			for (Int32 i = 0; i < _vertices.Count; i++)
			{
				vertices.Add(_vertices.Vertices[i].Position);
			}
			mesh.SetVertices(vertices);

			//
			// Normals
			//
			List<UnityEngine.Vector3> normals = new List<UnityEngine.Vector3>(_vertices.Count);
			for (Int32 i = 0; i < _vertices.Count; i++)
			{
				normals.Add(_vertices.Vertices[i].Normal);
			}
			mesh.SetNormals(normals);

			//
			// Triangles
			// 
			List<Int32> triangles = new List<Int32>(_triangles.Count * 3);
			for (Int32 i = 0; i < _triangles.Count; i++)
			{
				triangles.Add(_triangles[i].IndexVertex0);
				triangles.Add(_triangles[i].IndexVertex1);
				triangles.Add(_triangles[i].IndexVertex2);
			}
			mesh.SetTriangles(triangles, 0);


			//
			// UV
			//
			if (_hasUVMap)
			{
				List<UnityEngine.Vector2> uvs = new List<UnityEngine.Vector2>(_vertices.Count);
				for (Int32 i = 0; i < _vertices.Count; i++)
				{
					uvs.Add(_vertices.Vertices[i].UV);
				}
				mesh.SetUVs(0, uvs);
			}

			//if (mChannelUV.Count > 0) mesh.SetUVs(0, _channelUV.ConvertToVector2List());
			//if (mChannelUV2.Count > 0) mesh.SetUVs(1, _channelUV2.ConvertToVector2List());
			//if (mChannelUV3.Count > 0) mesh.SetUVs(2, _channelUV3.ConvertToVector2List());

			//if (mTangents.Count > 0)
			//{
			//	mesh.RecalculateTangents();
			//}

			mesh.RecalculateBounds();
		}
#endif
        #endregion

        #region g3 methods
#if GEOMETRY3SHARP || UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Получение данных с меша типа <see cref="g3.DMesh3"/>.
		/// </summary>
		/// <param name="mesh">Меш.</param>
		public void FromDMesh3(g3.DMesh3 mesh)
		{
			_vertices.Clear();

			for (Int32 iv = 0; iv < mesh.VertexCount; iv++)
			{
				g3.NewVertexInfo vertex_info = new g3.NewVertexInfo();
				mesh.GetVertex(iv, ref vertex_info, true, false, true);

				Vector3Df pos = new Vector3Df((Single)vertex_info.v.x, (Single)vertex_info.v.y, (Single)vertex_info.v.z);
				Vector3Df norm = new Vector3Df(vertex_info.n.x, vertex_info.n.y, vertex_info.n.z);
				Vector2Df uv = new Vector2Df(vertex_info.uv.x, vertex_info.uv.y);
				_vertices.AddVertex(pos, norm, uv);
			}

			_triangles.Clear();

			for (Int32 it = 0; it < mesh.TriangleCount; it++)
			{
				g3.Index3i triangle = mesh.GetTriangle(it);
				_triangles.AddTriangle(triangle.a, triangle.b, triangle.c);
			}
		}

		/// <summary>
		/// Преобразование в тип <see cref="g3.DMesh3"/>.
		/// </summary>
		/// <returns>Объект <see cref="g3.DMesh3"/>.</returns>
		public g3.DMesh3 ToDMesh3()
		{
			g3.DMesh3 mesh = new g3.DMesh3(g3.MeshComponents.VertexNormals | g3.MeshComponents.VertexUVs);
			for (Int32 iv = 0; iv < _vertices.Count; iv++)
			{
				g3.Vector3d pos = new g3.Vector3d(_vertices[iv].Position.X, _vertices[iv].Position.Y, _vertices[iv].Position.Z);
				g3.Vector3f norm = new g3.Vector3f(_vertices[iv].Normal.X, _vertices[iv].Normal.Y, _vertices[iv].Normal.Z);
				g3.Vector2f uv = new g3.Vector2f(_vertices[iv].UV.X, _vertices[iv].UV.Y);
				g3.NewVertexInfo vertex = new g3.NewVertexInfo();
				vertex.v = pos;
				vertex.n = norm;
				vertex.uv = uv;
				vertex.bHaveN = true;
				vertex.bHaveUV = true;
				mesh.AppendVertex(vertex);
			}

			for (Int32 it = 0; it < _triangles.Count; it++)
			{
				g3.Index3i triangle = new g3.Index3i(_triangles[it].IndexVertex0,
					_triangles[it].IndexVertex1, _triangles[it].IndexVertex2);
				mesh.AppendTriangle(triangle);
			}

			return (mesh);
		}

		/// <summary>
		///.
		/// </summary>
		/// <param name="numcells"></param>
		/// <param name="max_offset"></param>
		/// <returns></returns>
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
		///.
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

		/// <summary>
		///.
		/// </summary>
		/// <param name="mesh"></param>
		/// <returns></returns>
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

    /// <summary>
    /// Набор всех трехмерных сеток(мешей) в сцене.
    /// </summary>
    /// <remarks>
    /// Предназначен для логического группирования всех мешей в обозревателе сцены.
    /// </remarks>
    public class MeshSet : Entity3D
    {
        #region Fields
        // Основные параметры
        protected internal ListArray<Mesh3Df> _meshes;
        protected internal Scene3D _ownerScene;
        #endregion

        #region Properties
        /// <summary>
        /// Наблюдаемая коллекция трехмерных сеток.
        /// </summary>
        public ListArray<Mesh3Df> Meshes
        {
            get { return _meshes; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="ownerScene">Сцена.</param>
        public MeshSet(Scene3D ownerScene)
        {
            _ownerScene = ownerScene;
            _name = "Сетки";
            _meshes = new ListArray<Mesh3Df>
            {
                IsNotify = true
            };
        }

#if USE_ASSIMP
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="ownerScene">Сцена Assimp.</param>
		/// <param name="assimp_scene">Сцена Assimp.</param>
		public MeshSet(Scene3D ownerScene, Assimp.Scene assimp_scene)
		{
			_ownerScene = ownerScene;
			_name = "Сетки";
			_meshes = new ListArray<Mesh3Df>();

			// Устанавливаем меши
			for (var i = 0; i < assimp_scene.MeshCount; i++)
			{
				Assimp.Mesh mesh = assimp_scene.Meshes[i];
				_meshes.Add(new Mesh3Df("Mesh_" + i.ToString(), mesh));
			}
		}
#endif
        #endregion

        #region ILotusTreeNodeViewBuilder methods
        /// <summary>
        /// Получение количества дочерних узлов.
        /// </summary>
        /// <returns>Количество дочерних узлов.</returns>
        public override int GetCountChildrenNode()
        {
            return _meshes.Count;
        }

        /// <summary>
        /// Получение дочернего узла по индексу.
        /// </summary>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Дочерней узел.</returns>
        public override object GetChildrenNode(int index)
        {
            return _meshes[index];
        }
        #endregion

        #region Main methods
#if USE_HELIX
		/// <summary>
		/// Создание всех мешей.
		/// </summary>
		public void CreateHelixMeshes()
		{
			for (var i = 0; i < _meshes.Count; i++)
			{
				//_meshes[i].CreateHelixMesh();
			}
		}
#endif
        #endregion
    }
    /**@}*/
}