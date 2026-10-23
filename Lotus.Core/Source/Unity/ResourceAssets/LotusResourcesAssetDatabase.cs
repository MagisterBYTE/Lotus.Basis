#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы работы с базой данных редактора Unity
    /// </summary>
    /// <remarks>
    /// Только для режима редактора Unity. API ориентирован на Unity 6 (AssetEditingScope, RenameAsset, RemoveObjectFromAsset).
    /// </remarks>
    public static class XEditorAssetDatabase
    {
#if UNITY_EDITOR
        #region Main methods
        /// <summary>
        /// Переименование ресурса
        /// </summary>
        /// <param name="assetPath">Путь до ресурса (относительный, Assets/...)</param>
        /// <param name="newName">Новое имя ресурса (без расширения)</param>
        /// <returns>Новый путь до ресурса или исходный путь при ошибке</returns>
        public static string RenameAsset(string assetPath, string newName)
        {
            if (string.IsNullOrEmpty(assetPath) || string.IsNullOrEmpty(newName))
            {
                return assetPath;
            }

            var newNameWithoutExt = Path.GetFileNameWithoutExtension(newName);
            var error = AssetDatabase.RenameAsset(assetPath, newNameWithoutExt);
            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogWarning($"XEditorAssetDatabase.RenameAsset failed: {error}");
                return assetPath;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var directory = Path.GetDirectoryName(assetPath)?.Replace('\\', '/') ?? "Assets";
            var extension = Path.GetExtension(assetPath);
            return $"{directory}/{newNameWithoutExt}{extension}";
        }

        /// <summary>
        /// Переименование ресурса по полному пути файловой системы
        /// </summary>
        /// <param name="fullPath">Полный путь до ресурса</param>
        /// <param name="newName">Новое имя ресурса</param>
        /// <returns>Новый asset-путь до ресурса или пустая строка при ошибке</returns>
        public static string RenameAssetFromFullPath(string fullPath, string newName)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                return string.Empty;
            }

            var normalized = fullPath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var assetPath = normalized.RemoveTo(XCoreSettings.ASSETS_PATH);
            return RenameAsset(assetPath, newName);
        }

        /// <summary>
        /// Получение всех ресурсов определённого типа из указанной директории
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="folder">Путь к директории</param>
        /// <returns>Список загруженных ресурсов</returns>
        public static List<TResource> GetAssetsFromFolder<TResource>(string folder) where TResource : UnityEngine.Object
        {
            var result = new List<TResource>();
            if (string.IsNullOrEmpty(folder))
            {
                return result;
            }

            folder = folder.TrimEnd('/');
            var guids = AssetDatabase.FindAssets($"t:{typeof(TResource).Name}", new[] { folder });

            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var resource = AssetDatabase.LoadAssetAtPath<TResource>(path);
                if (resource != null)
                {
                    result.Add(resource);
                }
            }

            return result;
        }
        #endregion

        #region Sub-asset methods
        /// <summary>
        /// Добавление вложенного ресурса текстового типа
        /// </summary>
        /// <param name="assetPath">Путь к основному ресурсу</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <param name="value">Текстовое значение</param>
        public static void AddSubAssetOfText(string assetPath, string subAssetName, string value)
        {
            var textAsset = new TextAsset(value)
            {
                name = subAssetName
            };

            using (new AssetDatabase.AssetEditingScope())
            {
                AssetDatabase.AddObjectToAsset(textAsset, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Добавление вложенного ресурса
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="assetPath">Путь к основному ресурсу</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <param name="subAsset">Вложенный ресурс</param>
        /// <returns>Вложенный ресурс</returns>
        public static TResource AddSubAssetOfType<TResource>(string assetPath, string subAssetName, TResource subAsset)
            where TResource : UnityEngine.Object
        {
            subAsset = PrepareSubAsset(subAsset, subAssetName);

            using (new AssetDatabase.AssetEditingScope())
            {
                AssetDatabase.AddObjectToAsset(subAsset, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return subAsset;
        }

        /// <summary>
        /// Добавление вложенного ресурса
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="assetObject">Основной ресурс</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <param name="subAsset">Вложенный ресурс</param>
        /// <returns>Вложенный ресурс</returns>
        public static TResource AddSubAssetOfType<TResource>(UnityEngine.Object assetObject, string subAssetName, TResource subAsset)
            where TResource : UnityEngine.Object
        {
            subAsset = PrepareSubAsset(subAsset, subAssetName);

            using (new AssetDatabase.AssetEditingScope())
            {
                AssetDatabase.AddObjectToAsset(subAsset, assetObject);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return subAsset;
        }

        /// <summary>
        /// Обновление вложенного ресурса текстового типа
        /// </summary>
        /// <param name="assetPath">Путь к основному ресурсу</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <param name="value">Текстовое значение</param>
        public static void UpdateSubAssetOfText(string assetPath, string subAssetName, string value)
        {
            var textAsset = new TextAsset(value)
            {
                name = subAssetName
            };

            using (new AssetDatabase.AssetEditingScope())
            {
                RemoveSubAssetsInternal(assetPath, subAssetName, typeof(TextAsset));
                AssetDatabase.AddObjectToAsset(textAsset, assetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Обновление вложенного ресурса текстового типа
        /// </summary>
        /// <param name="assetObject">Основной ресурс</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <param name="value">Текстовое значение</param>
        public static void UpdateSubAssetOfText(UnityEngine.Object assetObject, string subAssetName, string value)
        {
            UpdateSubAssetOfText(AssetDatabase.GetAssetPath(assetObject), subAssetName, value);
        }

        /// <summary>
        /// Обновление вложенного ресурса
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="assetObject">Основной ресурс</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <param name="subAsset">Вложенный ресурс</param>
        /// <returns>Вложенный ресурс</returns>
        public static TResource UpdateSubAssetOfType<TResource>(UnityEngine.Object assetObject, string subAssetName, TResource subAsset)
            where TResource : UnityEngine.Object
        {
            var assetPath = AssetDatabase.GetAssetPath(assetObject);
            subAsset = PrepareSubAsset(subAsset, subAssetName);

            using (new AssetDatabase.AssetEditingScope())
            {
                RemoveSubAssetsInternal(assetPath, subAssetName, null);
                AssetDatabase.AddObjectToAsset(subAsset, assetObject);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return subAsset;
        }

        /// <summary>
        /// Получение вложенного ресурса
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="assetPath">Путь к основному ресурсу</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <returns>Вложенный ресурс или null</returns>
        public static TResource GetSubAssetOfType<TResource>(string assetPath, string subAssetName)
            where TResource : UnityEngine.Object
        {
            var allSubAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
            for (var i = 0; i < allSubAssets.Length; i++)
            {
                if (allSubAssets[i].name == subAssetName && allSubAssets[i] is TResource typed)
                {
                    return typed;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение вложенного ресурса
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="assetObject">Основной ресурс</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <returns>Вложенный ресурс или null</returns>
        public static TResource GetSubAssetOfType<TResource>(UnityEngine.Object assetObject, string subAssetName)
            where TResource : UnityEngine.Object
        {
            return GetSubAssetOfType<TResource>(AssetDatabase.GetAssetPath(assetObject), subAssetName);
        }

        /// <summary>
        /// Получение вложенного ресурса как текста
        /// </summary>
        /// <param name="assetPath">Путь к основному ресурсу</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <returns>Текстовое значение или null</returns>
        public static string GetSubAssetOfText(string assetPath, string subAssetName)
        {
            var textAsset = GetSubAssetOfType<TextAsset>(assetPath, subAssetName);
            return textAsset != null ? textAsset.text : null;
        }

        /// <summary>
        /// Получение вложенного ресурса как текста
        /// </summary>
        /// <param name="assetObject">Основной ресурс</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <returns>Текстовое значение или null</returns>
        public static string GetSubAssetOfText(UnityEngine.Object assetObject, string subAssetName)
        {
            return GetSubAssetOfText(AssetDatabase.GetAssetPath(assetObject), subAssetName);
        }

        /// <summary>
        /// Удаление вложенного ресурса
        /// </summary>
        /// <param name="assetPath">Путь к основному ресурсу</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        public static void RemoveSubAsset(string assetPath, string subAssetName)
        {
            using (new AssetDatabase.AssetEditingScope())
            {
                RemoveSubAssetsInternal(assetPath, subAssetName, null);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Удаление вложенного ресурса
        /// </summary>
        /// <param name="assetObject">Основной ресурс</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        public static void RemoveSubAsset(UnityEngine.Object assetObject, string subAssetName)
        {
            RemoveSubAsset(AssetDatabase.GetAssetPath(assetObject), subAssetName);
        }

        /// <summary>
        /// Удаление вложенного ресурса указанного типа
        /// </summary>
        /// <param name="assetPath">Путь к основному ресурсу</param>
        /// <param name="subAssetName">Имя вложенного ресурса</param>
        /// <param name="subAssetType">Тип вложенного ресурса</param>
        public static void RemoveSubAsset(string assetPath, string subAssetName, Type subAssetType)
        {
            using (new AssetDatabase.AssetEditingScope())
            {
                RemoveSubAssetsInternal(assetPath, subAssetName, subAssetType);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        #endregion

        #region Private methods
        private static TResource PrepareSubAsset<TResource>(TResource subAsset, string subAssetName)
            where TResource : UnityEngine.Object
        {
            var path = AssetDatabase.GetAssetPath(subAsset);
            if (string.IsNullOrEmpty(path))
            {
                subAsset.name = subAssetName;
                return subAsset;
            }

            // Уже существующий ассет — копируем, чтобы не перемещать исходник
            subAsset = UnityEngine.Object.Instantiate(subAsset);
            subAsset.name = subAssetName;
            return subAsset;
        }

        private static void RemoveSubAssetsInternal(string assetPath, string subAssetName, Type subAssetType)
        {
            var allSubAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
            for (var i = 0; i < allSubAssets.Length; i++)
            {
                var candidate = allSubAssets[i];
                if (candidate == null || candidate.name != subAssetName)
                {
                    continue;
                }

                if (subAssetType != null && candidate.GetType() != subAssetType)
                {
                    continue;
                }

                AssetDatabase.RemoveObjectFromAsset(candidate);
                UnityEngine.Object.DestroyImmediate(candidate, true);
            }
        }
        #endregion
#endif
    }

    /**@}*/
}
#endif
