#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе с двухмерными текстурами
    /// </summary>
    public static class XTexture2D
    {
        #region Fields
        private static Texture2D _whiteAlpha25;
        private static Texture2D _whiteAlpha50;
        private static Texture2D _whiteAlpha75;
        private static Texture2D _white3x3Alpha10;
        private static Texture2D _white3x3Alpha25;
        private static Texture2D _blackAlpha25;
        private static Texture2D _blackAlpha50;
        private static Texture2D _blackAlpha75;
        #endregion

        #region Properties
        /// <summary>
        /// Белая текстура
        /// </summary>
        public static Texture2D White
        {
            get { return Texture2D.whiteTexture; }
        }

        /// <summary>
        /// Белая текстура с прозрачностью 25
        /// </summary>
        public static Texture2D WhiteAlpha25
        {
            get
            {
                if (_whiteAlpha25 == null)
                {
                    _whiteAlpha25 = CreateSolid(1, 1, new Color(1, 1, 1, 0.25f));
                }

                return _whiteAlpha25;
            }
        }

        /// <summary>
        /// Белая текстура с прозрачностью 50
        /// </summary>
        public static Texture2D WhiteAlpha50
        {
            get
            {
                if (_whiteAlpha50 == null)
                {
                    _whiteAlpha50 = CreateSolid(1, 1, new Color(1, 1, 1, 0.5f));
                }

                return _whiteAlpha50;
            }
        }

        /// <summary>
        /// Белая текстура с прозрачностью 75
        /// </summary>
        public static Texture2D WhiteAlpha75
        {
            get
            {
                if (_whiteAlpha75 == null)
                {
                    _whiteAlpha75 = CreateSolid(1, 1, new Color(1, 1, 1, 0.75f));
                }

                return _whiteAlpha75;
            }
        }

        /// <summary>
        /// Белая текстура размером 3x3 с прозрачностью на краях 10
        /// </summary>
        /// <remarks>
        /// Заполнение текстуры (степень прозрачности):
        /// (0.1) (0.1) (0.1)
        /// (1.0) (1.0) (1.0)
        /// (0.1) (0.1) (0.1)
        /// </remarks>
        public static Texture2D White3x3Alpha10
        {
            get
            {
                if (_white3x3Alpha10 == null)
                {
                    _white3x3Alpha10 = CreateWhite3x3Edge(0.1f);
                }

                return _white3x3Alpha10;
            }
        }

        /// <summary>
        /// Белая текстура размером 3x3 с прозрачностью на краях 25
        /// </summary>
        /// <remarks>
        /// Заполнение текстуры (степень прозрачности):
        /// (0.25) (0.25) (0.25)
        /// (1.0) (1.0) (1.0)
        /// (0.25) (0.25) (0.25)
        /// </remarks>
        public static Texture2D White3x3Alpha25
        {
            get
            {
                if (_white3x3Alpha25 == null)
                {
                    _white3x3Alpha25 = CreateWhite3x3Edge(0.25f);
                }

                return _white3x3Alpha25;
            }
        }

        /// <summary>
        /// Черная текстура
        /// </summary>
        public static Texture2D Black
        {
            get { return Texture2D.blackTexture; }
        }

        /// <summary>
        /// Черная текстура с прозрачностью 25
        /// </summary>
        public static Texture2D BlackAlpha25
        {
            get
            {
                if (_blackAlpha25 == null)
                {
                    _blackAlpha25 = CreateSolid(1, 1, new Color(0, 0, 0, 0.25f));
                }

                return _blackAlpha25;
            }
        }

        /// <summary>
        /// Черная текстура с прозрачностью 50
        /// </summary>
        public static Texture2D BlackAlpha50
        {
            get
            {
                if (_blackAlpha50 == null)
                {
                    _blackAlpha50 = CreateSolid(1, 1, new Color(0, 0, 0, 0.5f));
                }

                return _blackAlpha50;
            }
        }

        /// <summary>
        /// Черная текстура с прозрачностью 75
        /// </summary>
        public static Texture2D BlackAlpha75
        {
            get
            {
                if (_blackAlpha75 == null)
                {
                    _blackAlpha75 = CreateSolid(1, 1, new Color(0, 0, 0, 0.75f));
                }

                return _blackAlpha75;
            }
        }
        #endregion

        #region Private methods
        private static Texture2D CreateSolid(int width, int height, Color color)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, color);
            texture.Apply(false, false);
            return texture;
        }

        private static Texture2D CreateWhite3x3Edge(float edgeOpacity)
        {
            var texture = new Texture2D(3, 3, TextureFormat.RGBA32, false);
            var edge = new Color(1, 1, 1, edgeOpacity);

            for (var x = 0; x < 3; x++)
            {
                texture.SetPixel(x, 0, edge);
                texture.SetPixel(x, 1, Color.white);
                texture.SetPixel(x, 2, edge);
            }

            texture.Apply(false, false);
            return texture;
        }
        #endregion
    }
    /**@}*/
}
#endif
