#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе со спрайтами
    /// </summary>
    public static class XSprite
    {
        #region Constants
        /// <summary>
        /// Имя спрайта по умолчанию
        /// </summary>
        public const string DEFAULT_NAME = "UISprite";

        /// <summary>
        /// Имя фонового спрайта
        /// </summary>
        public const string BACKGROUND_NAME = "Background";

        /// <summary>
        /// Имя спрайта маски
        /// </summary>
        public const string MASK_NAME = "UIMask";

        /// <summary>
        /// Имя спрайта Knob
        /// </summary>
        public const string KNOB_NAME = "Knob";

        /// <summary>
        /// Имя спрайта Checkmark
        /// </summary>
        public const string CHECKMARK_NAME = "Checkmark";
        #endregion

        #region Fields
        private static Sprite _default;
        private static Sprite _background;
        private static Sprite _mask;
        private static Sprite _knob;
        private static Sprite _checkmark;
        #endregion

        #region Properties
        /// <summary>
        /// Спрайт по умолчанию
        /// </summary>
        public static Sprite Default
        {
            get
            {
                if (_default == null)
                {
                    LoadDefault();
                }

#if UNITY_EDITOR
                if (_default == null)
                {
                    _default = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
                }
#endif

                return _default;
            }
        }

        /// <summary>
        /// Спрайт фонового изображения
        /// </summary>
        public static Sprite Background
        {
            get
            {
                if (_background == null)
                {
                    LoadDefault();
                }

                return _background;
            }
        }

        /// <summary>
        /// Спрайт маски
        /// </summary>
        public static Sprite Mask
        {
            get
            {
                if (_mask == null)
                {
                    LoadDefault();
                }

                return _mask;
            }
        }

        /// <summary>
        /// Спрайт Knob
        /// </summary>
        public static Sprite Knob
        {
            get
            {
                if (_knob == null)
                {
                    LoadDefault();
                }

                return _knob;
            }
        }

        /// <summary>
        /// Спрайт Checkmark
        /// </summary>
        public static Sprite Checkmark
        {
            get
            {
                if (_checkmark == null)
                {
                    LoadDefault();
                }

                return _checkmark;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Загрузка спрайтов по умолчанию
        /// </summary>
        internal static void LoadDefault()
        {
            var sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            for (var i = 0; i < sprites.Length; i++)
            {
                if (DEFAULT_NAME.Equal(sprites[i].name))
                {
                    _default = sprites[i];
                    continue;
                }

                if (BACKGROUND_NAME.Equal(sprites[i].name))
                {
                    _background = sprites[i];
                    continue;
                }

                if (MASK_NAME.Equal(sprites[i].name))
                {
                    _mask = sprites[i];
                    continue;
                }

                if (KNOB_NAME.Equal(sprites[i].name))
                {
                    _knob = sprites[i];
                    continue;
                }

                if (CHECKMARK_NAME.Equal(sprites[i].name))
                {
                    _checkmark = sprites[i];
                }
            }
        }
        #endregion

        #region Search methods
        /// <summary>
        /// Поиск ресурса спрайта по имени
        /// </summary>
        /// <param name="name">Имя спрайта</param>
        /// <returns>Найденный ресурс спрайта или null</returns>
        public static Sprite Find(string name)
        {
            var sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            for (var i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].name == name)
                {
                    return sprites[i];
                }
            }

#if UNITY_EDITOR
            var textureSprite = XResourcesDispatcher.Find<Texture2D>(name);
            if (textureSprite != null)
            {
                return textureSprite.GetSpriteInDesign();
            }

            var textures = Resources.FindObjectsOfTypeAll<Texture2D>();
            for (var it = 0; it < textures.Length; it++)
            {
                var allSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(textures[it]));
                for (var i = 0; i < allSprites.Length; i++)
                {
                    if (allSprites[i] is Sprite sprite && sprite.name == name)
                    {
                        return sprite;
                    }
                }
            }
#endif
            return null;
        }

        /// <summary>
        /// Поиск ресурса спрайта по вхождению имени
        /// </summary>
        /// <param name="name">Имя спрайта</param>
        /// <returns>Найденный ресурс спрайта или null</returns>
        public static Sprite FindMatch(string name)
        {
            var sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            for (var i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].name.Contains(name, StringComparison.Ordinal))
                {
                    return sprites[i];
                }
            }

#if UNITY_EDITOR
            var textureSprite = XResourcesDispatcher.Find<Texture2D>(name);
            if (textureSprite != null)
            {
                return textureSprite.GetSpriteInDesign();
            }

            var textures = Resources.FindObjectsOfTypeAll<Texture2D>();
            for (var it = 0; it < textures.Length; it++)
            {
                var allSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(textures[it]));
                for (var i = 0; i < allSprites.Length; i++)
                {
                    if (allSprites[i] is Sprite sprite && sprite.name.Contains(name, StringComparison.Ordinal))
                    {
                        return sprite;
                    }
                }
            }
#endif
            return null;
        }
        #endregion
    }

    /**@}*/
}
#endif
