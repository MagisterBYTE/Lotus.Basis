
#if USE_WINDOWS
using System.Windows;
using System.Windows.Media;
using Media3D = System.Windows.Media.Media3D;
#endif

#if USE_HELIX
using HelixToolkit.Wpf;
using Helix3D = HelixToolkit.Wpf.SharpDX;
#endif

namespace Lotus.Object3D
{
    /** \addtogroup Object3DTexture
	*@{*/
    /// <summary>
    /// Определяет, как генерируются координаты текстуры.
    /// <para>
    /// Приложениям реального времени обычно требуются полные UV-координаты. Поэтому настоятельно рекомендуется
    /// использовать шаг "Assimp.PostProcessSteps.GenerateUVCoords". Он генерирует правильные UV-каналы для
    /// объектов, не относящихся к UV, при условии, что дано точное описание того, как должно выглядеть отображение.
    /// </para>.
    /// </summary>
    public enum TTextureMapping
    {
        /// <summary>
        /// Coordinates are taken from the an existing UV channel.
        /// <para>
        /// The AI_MATKEY_UVWSRC key specifies from the UV channel the texture coordinates
        /// are to be taken from since meshes can have more than one UV channel.
        /// </para>.
        /// </summary>
        FromUV = 0x0,

        /// <summary>
        /// Spherical mapping.
        /// </summary>
        Sphere = 0x1,

        /// <summary>
        /// Cylinder mapping.
        /// </summary>
        Cylinder = 0x2,

        /// <summary>
        /// Cubic mapping.
        /// </summary>
        Box = 0x3,

        /// <summary>
        /// Planar mapping.
        /// </summary>
        Plane = 0x4,

        /// <summary>
        /// Unknown mapping that is not recognied.
        /// </summary>
        Unknown = 0x5
    }

    /// <summary>
    /// Defines how the Nth texture of a specific type is combined.
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
    /// </para>.
    /// </summary>
    public enum TTextureOperation
    {
        /// <summary>
        /// T = T1 * T2.
        /// </summary>
        Multiply = 0x0,

        /// <summary>
        /// T = T1 + T2.
        /// </summary>
        Add = 0x1,

        /// <summary>
        /// T = T1 - T2.
        /// </summary>
        Subtract = 0x2,

        /// <summary>
        /// T = T1 / T2.
        /// </summary>
        Divide = 0x3,

        /// <summary>
        /// T = (T1 + T2) - (T1 * T2).
        /// </summary>
        SmoothAdd = 0x4,

        /// <summary>
        /// T = T1 + (T2 - 0.5).
        /// </summary>
        SignedAdd = 0x5
    }

    /// <summary>
    /// Defines how UV coordinates outside the [0..1] range are handled. Commonly.
    /// referred to as the 'wrapping mode'.
    /// </summary>
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
        /// A texture coordinate u|v becomes u1|v1 if (u - (u % 1)) % 2 is zero.
        /// and 1 - (u % 1) | 1 - (v % 1) otherwise.
        /// </summary>
        Mirror = 0x2,

        /// <summary>
        /// If the texture coordinates for a pixel are outside [0...1] the texture is not.
        /// applied to that pixel.
        /// </summary>
        Decal = 0x3,
    }

    /// <summary>
    /// Текстурный слот для определения параметров текстурного наложения.
    /// </summary>
    public class TextureSlot : Entity3D
    {
        #region Static methods
        #endregion

        #region Fields
        // Основные параметры
        protected internal Texture? _texture;
        protected internal TTextureMapping _mapping;
        protected internal int _indexUV;
        protected internal float _blendFactor;
        protected internal TTextureOperation _operation;
        protected internal TTextureWrapMode _wrapModeU;
        protected internal TTextureWrapMode _wrapModeV;
        protected internal Material? _ownerMaterial;

        // Платформенно-зависимая часть
#if USE_ASSIMP
		internal Assimp.TextureSlot _assimpTextureSlot;
#endif
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Текстура связанная с данным текстурным слотом.
        /// </summary>
        public Texture? Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
            }
        }

        /// <summary>
        /// Режим наложения и формирования текстурных координат.
        /// </summary>
        public TTextureMapping Mapping
        {
            get { return _mapping; }
            set
            {
                _mapping = value;
            }
        }

        /// <summary>
        /// Индекс текстурных координат.
        /// </summary>
        public int UVIndex
        {
            get { return _indexUV; }
            set
            {
                _indexUV = value;
            }
        }

        /// <summary>
        /// Фактор смешивания.
        /// </summary>
        public float BlendFactor
        {
            get { return _blendFactor; }
            set
            {
                _blendFactor = value;
            }
        }

        /// <summary>
        /// Режим смешивания текстур.
        /// </summary>
        public TTextureOperation Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
            }
        }

        /// <summary>
        /// Режим обвёртки текстурных координат по U - координате.
        /// </summary>
        public TTextureWrapMode WrapModeU
        {
            get { return _wrapModeU; }
            set
            {
                _wrapModeU = value;
            }
        }

        /// <summary>
        /// Режим обвёртки текстурных координат по V - координате.
        /// </summary>
        public TTextureWrapMode WrapModeV
        {
            get { return _wrapModeV; }
            set
            {
                _wrapModeV = value;
            }
        }

        /// <summary>
        /// Владелец материал.
        /// </summary>
        public Material? OwnerMaterial
        {
            get { return _ownerMaterial; }
            set
            {
                _ownerMaterial = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public TextureSlot()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="ownerMaterial">Материал.</param>
        public TextureSlot(Material ownerMaterial)
        {
            _ownerMaterial = ownerMaterial;
        }

#if USE_ASSIMP
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="assimp_texture_slot">Текстурный слот.</param>
		public TextureSlot(Assimp.TextureSlot assimp_texture_slot)
		{
			_assimpTextureSlot = assimp_texture_slot;
		}

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="material">Материал.</param>
		/// <param name="assimp_texture_slot">Текстурный слот.</param>
		public TextureSlot(Material material, Assimp.TextureSlot assimp_texture_slot)
		{
			_ownerMaterial = material;
			_assimpTextureSlot = assimp_texture_slot;
		}
#endif
        #endregion

        #region Main methods
        #endregion
    }
    /**@}*/
}