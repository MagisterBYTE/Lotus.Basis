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
// Последнее изменение от 30.04.2023
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
		/**
         * \defgroup Object3DMaterial Подсистема материала
         * \ingroup Object3D
         * \brief Подсистема материала определяет параметры визуализации геометрии объекта.
         * @{
         */
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
			internal TColor _ambientColor;
			internal TColor _ambientColorOriginal;
			internal TColor _diffuseColor;
			internal TColor _diffuseColorOriginal;
			internal ListArray<CTextureSlot> _textureSlots;
			internal CTextureSlot? _ambientSlot;
			internal CTextureSlot? _diffuseSlot;
			internal CTextureSlot? _normalSlot;
			internal CTextureSlot? _heightSlot;
			internal CScene3D _ownerScene;

			// Платформенно-зависимая часть
#if USE_HELIX
			internal Helix3D.PhongMaterial _helixMaterial;
#endif
#if USE_ASSIMP
			internal Assimp.Material _assimpMaterial;
#endif
#if UNITY_2017_1_OR_NEWER
			internal UnityEngine.Material _unityMaterial;
#endif
#if UNITY_EDITOR
			internal Autodesk.Fbx.FbxSurfaceMaterial _fbxMaterial;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Цвет подсветки материала
			/// </summary>
			public TColor AmbientColor
			{
				get { return _ambientColor; }
				set
				{
					if (_ambientColor != value)
					{
						_ambientColor = value;
						RaiseAmbientColorChanged();
						NotifyPropertyChanged(PropertyArgsAmbientColor);
					}
				}
			}

			/// <summary>
			/// Основной цвет материала
			/// </summary>
			public TColor DiffuseColor
			{
				get { return _diffuseColor; }
				set
				{
					if (_diffuseColor != value)
					{
						_diffuseColor = value;
						RaiseDiffuseColorChanged();
						NotifyPropertyChanged(PropertyArgsDiffuseColor);
					}
				}
			}

			/// <summary>
			/// Все текстурные слоты
			/// </summary>
			public ListArray<CTextureSlot> TextureSlots
			{
				get { return _textureSlots; }
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
			public CMaterial(CScene3D ownerScene)
			{
				_ownerScene = ownerScene;
				_textureSlots = new ListArray<CTextureSlot>
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
				_ownerScene = owner_scene;
				_textureSlots = new ListArray<CTextureSlot>();

				_assimpMaterial = assimp_material;
				_name = _assimpMaterial.Name;

				if (_assimpMaterial.HasTextureAmbient)
				{
					_ambientSlot = new CTextureSlot(this, _assimpMaterial.TextureAmbient);
					_ambientSlot.Name = "Ambient";
					_textureSlots.Add(_ambientSlot);
				}

				if (_assimpMaterial.HasTextureDiffuse)
				{
					_diffuseSlot = new CTextureSlot(this, _assimpMaterial.TextureDiffuse);
					_diffuseSlot.Name = "Diffuse";
					_textureSlots.Add(_diffuseSlot);
				}

				if (_assimpMaterial.HasTextureNormal)
				{
					_normalSlot = new CTextureSlot(this, _assimpMaterial.TextureNormal);
					_normalSlot.Name = "Normal";
					_textureSlots.Add(_normalSlot);
				}

				if (_assimpMaterial.HasTextureHeight)
				{
					_heightSlot = new CTextureSlot(this, _assimpMaterial.TextureHeight);
					_heightSlot.Name = "Height";
					_textureSlots.Add(_heightSlot);
				}
			}
#endif

#if UNITY_2017_1_OR_NEWER
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
				_name = unity_material.name;
				_unityMaterial = unity_material;
				//_ambientColorOriginal = _ambientColor = _unityMaterial.GetColor("Ambient").ToTColor();
				//_diffuseColorOriginal = _diffuseColor = _unityMaterial.GetColor("Diffuse").ToTColor();
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
				_name = fbx_material.GetName();
				_fbxMaterial = fbx_material;
				//_ambientColorOriginal = _ambientColor = _unityMaterial.GetColor("Ambient").ToTColor();
				//_diffuseColorOriginal = _diffuseColor = _unityMaterial.GetColor("Diffuse").ToTColor();
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
				if (_helixMaterial != null)
				{
					//_helixMaterial.AmbientColor = _ambientColor.ToShColor4();
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if (_unityMaterial != null)
				{
					_unityMaterial.SetColor("Ambient", _ambientColor);
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
				if (_helixMaterial != null)
				{
					//_helixMaterial.DiffuseColor = _diffuseColor.ToShColor4();
				}
#endif
#if UNITY_2017_1_OR_NEWER
				if (_unityMaterial != null)
				{
					_unityMaterial.SetColor("Diffuse", _ambientColor);
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
					_helixMaterial = new Helix3D.PhongMaterial();
					_helixMaterial.Name = _name;

                    //if (_assimpMaterial.HasTextureDiffuse)
                    //{
                    //    _helixMaterial.DiffuseMap = _diffuseSlot.GetTextureSteam();
                    //}

                    //if (_assimpMaterial.HasTextureNormal)
                    //{
                    //    _helixMaterial.NormalMap = _normalSlot.GetTextureSteam();
                    //}

                    //if (_assimpMaterial.HasTextureHeight)
                    //{
                    //    _helixMaterial.DisplacementMap = _heightSlot.GetTextureSteam();
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
			protected internal ListArray<CMaterial> _materials;
			protected internal CScene3D _ownerScene;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наблюдаемая коллекция материал
			/// </summary>
			public ListArray<CMaterial> Materials
			{
				get { return _materials; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="ownerScene">Сцена</param>
			//---------------------------------------------------------------------------------------------------------
			public CMaterialSet(CScene3D ownerScene)
			{
				_ownerScene = ownerScene;
				_name = "Материалы";
				_materials = new ListArray<CMaterial>
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
				_name = "Материалы";
				_materials = new ListArray<CMaterial>();

				// Устанавливаем материалы
				for (var i = 0; i < assimp_scene.MaterialCount; i++)
				{
					Assimp.Material assimp_material = assimp_scene.Materials[i];
					var material = new CMaterial(owner_scene, assimp_material);
					_materials.Add(material);
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
				return _materials.Count;
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
				return _materials[index];
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
				for (var i = 0; i < _materials.Count; i++)
				{
					_materials[i].CreateHelixMaterial();
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