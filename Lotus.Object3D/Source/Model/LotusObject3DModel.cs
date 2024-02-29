using System.ComponentModel;

#if USE_WINDOWS
using System.Windows;
using System.Windows.Media;
using Media3D = System.Windows.Media.Media3D;
#endif

#if USE_HELIX
using HelixToolkit.Wpf;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Model.Scene;
#endif

using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DBase
	*@{*/
    /// <summary>
    /// Модель - меш с примененным материалом который подлежит отображению через узел сцены.
    /// </summary>
    public class Model3D : Node3D
    {
        #region Static fields
        protected static readonly PropertyChangedEventArgs PropertyArgsIsVisible = new(nameof(IsVisible));
        protected static readonly PropertyChangedEventArgs PropertyArgsMesh = new(nameof(Mesh));
        protected static readonly PropertyChangedEventArgs PropertyArgsMaterial = new(nameof(Material));
        protected static readonly PropertyChangedEventArgs PropertyArgsLocation = new(nameof(Location));
        protected static readonly PropertyChangedEventArgs PropertyArgsSizeX = new(nameof(SizeX));
        protected static readonly PropertyChangedEventArgs PropertyArgsSizeY = new(nameof(SizeY));
        protected static readonly PropertyChangedEventArgs PropertyArgsSizeZ = new(nameof(SizeZ));
        #endregion

        #region Fields
        // Основные параметры
        protected internal bool _isVisible = true;
        protected internal Mesh3Df? _mesh;
        protected internal Material? _material;

        // Размеры и позиция
        protected internal Vector3Df _location;
        protected internal Vector3Df _minPosition;
        protected internal Vector3Df _maxPosition;

        // Платформенно-зависимая часть
#if USE_HELIX
		protected internal MeshNode? _helixModel;
#endif
#if UNITY_2017_1_OR_NEWER
		protected internal UnityEngine.MeshFilter _unityModel;
#endif
#if UNITY_EDITOR
		protected internal Autodesk.Fbx.FbxNode _fbxModel;
#endif
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Видимость модели.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                RaiseIsVisibleChanged();
                OnPropertyChanged(PropertyArgsIsVisible);
            }
        }

        /// <summary>
        /// Меш модели.
        /// </summary>
        public Mesh3Df? Mesh
        {
            get
            {
                return _mesh;
            }
            set
            {
                _mesh = value;
                RaiseMeshChanged();
                OnPropertyChanged(PropertyArgsMesh);
            }
        }

        /// <summary>
        /// Материал модели.
        /// </summary>
        public Material? Material
        {
            get
            {
                return _material;
            }
            set
            {
                _material = value;
                RaiseMaterialChanged();
                OnPropertyChanged(PropertyArgsMaterial);
            }
        }

        //
        // РАЗМЕРЫ И ПОЗИЦИЯ
        //
        /// <summary>
        /// Позиция геометрического центра модели.
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

        //
        // ПЛАТФОРМЕННО-ЗАВИСИМАЯ ЧАСТЬ
        //
#if USE_HELIX
		/// <summary>
		/// Модель.
		/// </summary>
		public MeshNode? HelixModel
		{
			get { return _helixModel; }
		}
#endif
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="ownerScene">Сцена.</param>
        /// <param name="parentNode">Родительский узел.</param>
        public Model3D(Scene3D ownerScene, Node3D parentNode)
            : base(ownerScene)
        {
            _ownerScene = ownerScene;
            _parentNode = parentNode;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="ownerScene">Сцена.</param>
        /// <param name="parentNode">Родительский узел.</param>
        /// <param name="mesh">Меш.</param>
        /// <param name="material">Материал.</param>
        public Model3D(Scene3D ownerScene, Node3D parentNode, Mesh3Df mesh, Material material)
            : this(ownerScene, parentNode)
        {
            _mesh = mesh;
            _material = material;
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="owner_scene">Сцена.</param>
		/// <param name="parent_node">Родительский узел.</param>
		/// <param name="unity_model">Модель Unity.</param>
		public CModel3D(CScene3D owner_scene, CNode3D parent_node, UnityEngine.MeshFilter unity_model)
			: this(owner_scene, parent_node)
		{
			_name = unity_model.name;
			_unityModel = unity_model;
		}
#endif
#if UNITY_EDITOR
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="owner_scene">Сцена.</param>
		/// <param name="parent_node">Родительский узел.</param>
		/// <param name="fbx_model">Модель Fbx.</param>
		public CModel3D(CScene3D owner_scene, CNode3D parent_node, Autodesk.Fbx.FbxNode fbx_model)
			: this(owner_scene, parent_node)
		{
			_name = fbx_model.GetName();
			_fbxModel = fbx_model;
		}
#endif
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение позиции узла.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected override void RaiseOffsetChanged()
        {
            base.RaiseOffsetChanged();
        }

        /// <summary>
        /// Изменение вращения узла.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected override void RaiseRotationChanged()
        {
            base.RaiseRotationChanged();
        }

        /// <summary>
        /// Изменение масштаба узла.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected override void RaiseScaleChanged()
        {
            base.RaiseScaleChanged();
        }

        /// <summary>
        /// Изменение статуса видимости модели.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseIsVisibleChanged()
        {
        }

        /// <summary>
        /// Изменение меша модели.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseMeshChanged()
        {

        }

        /// <summary>
        /// Изменение материала модели.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseMaterialChanged()
        {

        }
        #endregion

        #region Main methods

        #endregion

        #region Windows methods
#if USE_WINDOWS
		/// <summary>
		/// Получение ограничивающего объема с учетом трансформации.
		/// </summary>
		/// <returns>Ограничивающий объем.</returns>
		public Media3D.Rect3D GetBoundsRect()
		{
			return Media3D.Rect3D.Empty;
			//return (mHelix3DModel.BoundsWithTransform.ToWinRect3D());
		}
#endif
        #endregion

        #region Helix methods
#if USE_HELIX
		/// <summary>
		/// Создание модели.
		/// </summary>
		public void CreateHelixModel()
		{
			//mHelix3DModel = new Helix3D.MeshGeometryModel3D();

			// Геометрия
			//mHelix3DModel.Geometry = mSubmittedMesh._helixMesh;

			// Материал
			//mHelix3DModel.Material = mOwnerNode.OwnerScene.GetMaterialHelixFromIndex(mSubmittedMesh.MaterialIndex);

			// Трансформация
			//mHelix3DModel.Transform = new Media3D.MatrixTransform3D(mOwnerNode._assimpNode.Transform.ToWinMatrix4D());
			//mHelix3DModel.Transform = mOwnerNode._nodeTransform;
		}

		/// <summary>
		/// Вычисление ограничивающего объема.
		/// </summary>
		internal void ComputeBoundingBox()
		{
			if (_helixModel != null)
			{
				SharpDX.BoundingBox bounding_box = _helixModel.BoundsWithTransform;

				_minPosition = new Vector3D(bounding_box.Minimum.X, bounding_box.Minimum.Y, bounding_box.Minimum.Z);
				_maxPosition = new Vector3D(bounding_box.Maximum.X, bounding_box.Maximum.Y, bounding_box.Maximum.Z);

				_location.X = (_minPosition.X + _maxPosition.X) / 2.0f;
				_location.Y = (_minPosition.Y + _maxPosition.Y) / 2.0f;
				_location.Z = (_minPosition.Z + _maxPosition.Z) / 2.0f;

				OnPropertyChanged(PropertyArgsLocation);
				OnPropertyChanged(PropertyArgsSizeX);
				OnPropertyChanged(PropertyArgsSizeY);
				OnPropertyChanged(PropertyArgsSizeZ);
			}
		}
#endif
        #endregion
    }
    /**@}*/
}