#if UNITY_EDITOR
using System.Collections.Specialized;
using System.Linq;

using Lotus.Core;

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Lotus.Editor
{
    /// <summary>
    /// Редактор для функционирования и управления простым кодогенератором
    /// </summary>
    public class LotusGenerateCodeWindow : LotusUtilityBaseWindow
    {
        #region Static methods
        /// <summary>
        /// Показать окно для функционирования и управления простым кодогенератором
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathUtility + "Generate Code", false, XCoreSettingsEditor.MenuOrderUtility + 20)]
        public static void ShowGenerateCode()
        {
            var window = GetWindow<LotusGenerateCodeWindow>();
            window.titleContent.text = "Generate Code";

            window.OnConstruct();
            window.Show();
        }
        #endregion

        #region Fields
        // Основные данные
        #endregion

        #region Main methods
        /// <summary>
        /// Конструирование окна
        /// </summary>
        public void OnConstruct()
        {
            _isManager = false;
            _tabs = new string[2];
            _tabs[0] = "Autogen: Tags, Layers, Axis";
            _tabs[1] = "Autogen: Serialization";
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование панели обозревателя
        /// </summary>
        public override void DrawExplore()
        {
            switch (_selectedTab)
            {
                case 0:
                    {
                        DrawAutogenTags();
                        DrawAutogenLayers();
                        DrawAutogenAxis();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Рисование основной панели
        /// </summary>
        public override void DrawContent()
        {
            GUILayout.Box("RectContent", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        }
        #endregion

        #region Tags methods
        /// <summary>
        /// Автогенерация кода для константных данных тэгов
        /// </summary>
        public void DrawAutogenTags()
        {
            GUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Генерация констант Tag", EditorStyles.largeLabel,
                GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

            GUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Файл: LotusBaseUnityTags", EditorStyles.boldLabel);

            GUILayout.Space(2.0f);
            if (GUILayout.Button("Generate"))
            {
                var generateCode = new CTextGenerateCodeCSharp();
                generateCode.AddFileHeader("Модуль общей функциональности", "Базовая подсистема Unity");
                generateCode.AddFileBriefDesc("LotusBaseUnityTags.cs", "Общие данные для организации работы с тэгами объектов Unity");
                generateCode.AddFileVersion();
                generateCode.AddNamespaceUsing("System", "UnityEngine");
                generateCode.AddNamespaceOpen("Lotus");
                {
                    generateCode.CurrentIndent++;
                    generateCode.AddNamespaceOpen("Common");
                    {
                        generateCode.CurrentIndent++;
                        generateCode.AddDoxygenAddToGroup("UnityCommonBase");
                        generateCode.AddCommentSummary(true, "Статический класс содержащий константы используемых тэгов объектов Unity", true);
                        generateCode.AddClassStaticPublic("XTag");
                        {
                            generateCode.CurrentIndent++;

                            var tags = InternalEditorUtility.tags;
                            for (var i = 0; i < tags.Length; i++)
                            {
                                if (tags[i] == "Untagged")
                                {
                                    generateCode.AddCommentSummaryForData("Тэг по умолчанию");
                                    generateCode.AddFieldConstPublicString(tags[i].ToConstCase(), tags[i]);
                                    generateCode.AddEmptyLine();
                                    continue;
                                }

                                if (tags[i] == "EditorOnly")
                                {
                                    generateCode.AddCommentSummaryForData("Тэг только для режима редактора");
                                    generateCode.AddFieldConstPublicString(tags[i].ToConstCase(), tags[i]);
                                    generateCode.AddEmptyLine();
                                    continue;
                                }

                                generateCode.AddCommentSummaryForData("Тэг - " + tags[i]);
                                generateCode.AddFieldConstPublicString(tags[i].ToConstCase(), tags[i]);

                                if (i != tags.Length - 1)
                                {
                                    generateCode.AddEmptyLine();
                                }
                            }

                            generateCode.CurrentIndent--;
                        }
                        generateCode.AddClassEndDeclaration();
                        generateCode.AddDoxygenEndGroup();

                        generateCode.CurrentIndent--;
                    }
                    generateCode.AddNamespaceClose();

                    generateCode.CurrentIndent--;
                }
                generateCode.AddNamespaceClose();
                generateCode.AddDelimiterPart();

                generateCode.SetLength(120);
                generateCode.Save(@"Lotus\Source\Common\Base\LotusBaseUnityTags.cs");
            }
        }
        #endregion

        #region Layers methods
        /// <summary>
        /// Автогенерация кода для константных данных слоев
        /// </summary>
        public void DrawAutogenLayers()
        {
            GUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Генерация констант Layers", EditorStyles.largeLabel,
                GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

            GUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Файл: LotusBaseUnityLayers", EditorStyles.boldLabel);

            GUILayout.Space(2.0f);
            if (GUILayout.Button("Generate"))
            {
                var generateCode = new CTextGenerateCodeCSharp();
                generateCode.AddFileHeader("Модуль общей функциональности", "Базовая подсистема Unity");
                generateCode.AddFileBriefDesc("LotusBaseUnityLayers.cs", "Общие данные для организации работы с различными слоями в Unity");
                generateCode.AddFileVersion();
                generateCode.AddNamespaceUsing("System", "UnityEngine");
                generateCode.AddNamespaceOpen("Lotus");
                {
                    generateCode.CurrentIndent++;
                    generateCode.AddNamespaceOpen("Common");
                    {
                        generateCode.CurrentIndent++;
                        generateCode.AddDoxygenAddToGroup("UnityCommonBase");
                        generateCode.AddCommentSummary(true, "Статический класс содержащий константы используемых слоев расположения объектов Unity", true);
                        generateCode.AddClassStaticPublic("XLayer");
                        {
                            generateCode.CurrentIndent++;

                            generateCode.AddCommentSection("ИМЕНА СЛОЕВ");

                            var layers = InternalEditorUtility.layers;
                            for (var i = 0; i < layers.Length; i++)
                            {
                                generateCode.AddCommentSummaryForData(GetCommentLayer(layers[i]));
                                generateCode.AddFieldConstPublicString(layers[i].ToConstCase(), layers[i]);
                                generateCode.AddEmptyLine();
                            }

                            generateCode.AddCommentSection("ЧИСЛОВЫЕ КОДЫ СЛОЕВ");

                            for (var i = 0; i < layers.Length; i++)
                            {
                                generateCode.AddCommentSummaryForData(GetCommentLayer(layers[i]));
                                generateCode.AddFieldStaticReadonlyPublic("Int32", layers[i].ToVarName() + "_ID",
                                    "LayerMask.NameToLayer(" + layers[i].ToConstCase() + ")");
                                generateCode.AddEmptyLine();
                            }

                            generateCode.AddCommentSection("МАСКИ СЛОЕВ");

                            for (var i = 0; i < layers.Length; i++)
                            {
                                generateCode.AddCommentSummaryForData(GetCommentLayer(layers[i], false));
                                generateCode.AddFieldStaticReadonlyPublic("Int32", "Mask" + layers[i].ToVarName(),
                                    "1 << " + layers[i].ToVarName() + "_ID");

                                if (i != layers.Length - 1)
                                {
                                    generateCode.AddEmptyLine();
                                }
                            }

                            generateCode.CurrentIndent--;
                        }
                        generateCode.AddClassEndDeclaration();
                        generateCode.AddDoxygenEndGroup();

                        generateCode.CurrentIndent--;
                    }
                    generateCode.AddNamespaceClose();

                    generateCode.CurrentIndent--;
                }
                generateCode.AddNamespaceClose();
                generateCode.AddDelimiterPart();

                generateCode.SetLengthWithTabs(120);
                generateCode.Save(@"Lotus\Source\Common\Base\LotusBaseUnityLayers.cs");
            }
        }

        /// <summary>
        /// Получение комментария для слоя
        /// </summary>
        /// <param name="layer">Комментарий</param>
        /// <param name="isObjectDesc">Описание для самого слоя</param>
        /// <returns>Комментарий для слоя</returns>
        private string GetCommentLayer(string layer, bool isObjectDesc = true)
        {
            switch (layer)
            {
                case "Default":
                    return isObjectDesc ? "Слой по умолчанию" : "Маска для слоя по умолчанию";
                case "TransparentFX":
                    return isObjectDesc ? "Слой для прозрачных объектов" : "Маска для слоя прозрачных объектов";
                case "IgnoreRaycast":
                case "Ignore Raycast":
                    return isObjectDesc
                        ? "Слой для объектов которые игнорируют коллизию"
                        : "Маска для слоя объектов которые игнорируют коллизию";
                case "Water":
                    return isObjectDesc
                        ? "Слой для объектов которые представляют собой воду"
                        : "Маска для слоя объектов которые представляют собой воду";
                case "UI":
                    return isObjectDesc
                        ? "Слой для объектов пользовательского интерфейса"
                        : "Маска для слоя объектов пользовательского интерфейса";
                default:
                    return isObjectDesc ? "Слой - " + layer : "Маска слоя для - " + layer;
            }
        }
        #endregion

        #region Axes methods
        /// <summary>
        /// Автогенерация кода для константных данных осей
        /// </summary>
        public void DrawAutogenAxis()
        {
            GUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Генерация констант Axis", EditorStyles.largeLabel,
                GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

            GUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Файл: LotusBaseUnityAxes", EditorStyles.boldLabel);

            GUILayout.Space(2.0f);
            if (GUILayout.Button("Generate"))
            {
                var generateCode = new CTextGenerateCodeCSharp();
                generateCode.AddFileHeader("Модуль общей функциональности", "Базовая подсистема Unity");
                generateCode.AddFileBriefDesc("LotusBaseUnityAxes.cs", "Общие данные для организации работы с осями ввода Unity");
                generateCode.AddFileVersion();
                generateCode.AddNamespaceUsing("System", "UnityEngine");
                generateCode.AddNamespaceOpen("Lotus");
                {
                    generateCode.CurrentIndent++;
                    generateCode.AddNamespaceOpen("Common");
                    {
                        generateCode.CurrentIndent++;
                        generateCode.AddDoxygenAddToGroup("UnityCommonBase");
                        generateCode.AddCommentSummary(true, "Статический класс содержащий константы используемых осей ввода Unity", true);
                        generateCode.AddClassStaticPublic("XAxis");
                        {
                            generateCode.CurrentIndent++;

                            var axes = GetAllAxisNames();
                            for (var i = 0; i < axes.Length; i++)
                            {
                                generateCode.AddCommentSummaryForData("Ось - " + axes[i]);
                                generateCode.AddFieldConstPublicString(axes[i].ToConstCase(), axes[i]);

                                if (i != axes.Length - 1)
                                {
                                    generateCode.AddEmptyLine();
                                }
                            }

                            generateCode.CurrentIndent--;
                        }
                        generateCode.AddClassEndDeclaration();
                        generateCode.AddDoxygenEndGroup();

                        generateCode.CurrentIndent--;
                    }
                    generateCode.AddNamespaceClose();

                    generateCode.CurrentIndent--;
                }
                generateCode.AddNamespaceClose();
                generateCode.AddDelimiterPart();

                generateCode.SetLengthWithTabs(120);
                generateCode.Save(@"Lotus\Source\Common\Base\LotusBaseUnityAxes.cs");
            }
        }

        /// <summary>
        /// Получение списка доступных осей
        /// </summary>
        /// <returns>Список осей</returns>
        private string[] GetAllAxisNames()
        {
            var result = new StringCollection();

            var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            var axesProperty = serializedObject.FindProperty("m_Axes");

            axesProperty.Next(true);
            axesProperty.Next(true);

            while (axesProperty.Next(false))
            {
                SerializedProperty axis = axesProperty.Copy();

                axis.Next(true);
                result.Add(axis.stringValue);
            }

            return result.Cast<string>().Distinct().ToArray();
        }
        #endregion
    }
}
#endif
