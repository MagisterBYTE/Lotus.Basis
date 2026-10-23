#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе с материалами
    /// </summary>
    public static class XMaterial
    {
        #region Constants
        /// <summary>
        /// Имя обычного материала красного цвета
        /// </summary>
        public const string DIFFUSE_RED_NAME = "DiffuseRed";

        /// <summary>
        /// Имя обычного материала зеленого цвета
        /// </summary>
        public const string DIFFUSE_GREEN_NAME = "DiffuseGreen";

        /// <summary>
        /// Имя обычного материала синего цвета
        /// </summary>
        public const string DIFFUSE_BLUE_NAME = "DiffuseBlue";
        #endregion

        #region Fields
        private static Material _default;
        private static Material _defaultLine;
        private static Material _diffuseRed;
        private static Material _diffuseGreen;
        private static Material _diffuseBlue;
        #endregion

        #region Properties
        /// <summary>
        /// Материал по умолчанию
        /// </summary>
        public static Material Default
        {
            get
            {
                if (_default == null)
                {
                    LoadDefault();
                }

                return _default;
            }
        }

        /// <summary>
        /// Материал для рисования линии
        /// </summary>
        public static Material DefaultLine
        {
            get
            {
                if (_defaultLine == null)
                {
                    LoadDefault();
                }

                return _defaultLine;
            }
        }

        /// <summary>
        /// Материал красного цвета
        /// </summary>
        public static Material DiffuseRed
        {
            get
            {
                if (_diffuseRed == null)
                {
                    LoadDefault();
                }

                return _diffuseRed;
            }
        }

        /// <summary>
        /// Материал зеленого цвета
        /// </summary>
        public static Material DiffuseGreen
        {
            get
            {
                if (_diffuseGreen == null)
                {
                    LoadDefault();
                }

                return _diffuseGreen;
            }
        }

        /// <summary>
        /// Материал синего цвета
        /// </summary>
        public static Material DiffuseBlue
        {
            get
            {
                if (_diffuseBlue == null)
                {
                    LoadDefault();
                }

                return _diffuseBlue;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Загрузка материалов по умолчанию
        /// </summary>
        internal static void LoadDefault()
        {
            var materials = Resources.FindObjectsOfTypeAll<Material>();
            for (var i = 0; i < materials.Length; i++)
            {
                if (materials[i].name.Equal("Default-Material"))
                {
                    _default = new Material(materials[i]);
                    continue;
                }

                if (materials[i].name.Equal("Default-Line"))
                {
                    _defaultLine = new Material(materials[i]);
                }
            }

            // Unity 6 / URP: если встроенные материалы ещё не подгружены — создаём fallback
            if (_default == null)
            {
                var shader = Shader.Find("Universal Render Pipeline/Lit")
                    ?? Shader.Find("Standard")
                    ?? Shader.Find("Diffuse");
                _default = new Material(shader)
                {
                    name = "Default-Material"
                };
            }

            if (_defaultLine == null)
            {
                var lineShader = Shader.Find("Hidden/Internal-Colored")
                    ?? Shader.Find("Sprites/Default")
                    ?? _default.shader;
                _defaultLine = new Material(lineShader)
                {
                    name = "Default-Line"
                };
            }

            _diffuseRed = CreateColoredMaterial(_default, DIFFUSE_RED_NAME, Color.red);
            _diffuseGreen = CreateColoredMaterial(_default, DIFFUSE_GREEN_NAME, Color.green);
            _diffuseBlue = CreateColoredMaterial(_default, DIFFUSE_BLUE_NAME, Color.blue);
        }

        private static Material CreateColoredMaterial(Material source, string name, Color color)
        {
            var material = new Material(source)
            {
                name = name
            };

            // Unity 6 / URP использует _BaseColor; Built-in — _Color / Material.color
            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", color);
            }
            else
            {
                material.color = color;
            }

            return material;
        }
        #endregion
    }

    /**@}*/
}
#endif
