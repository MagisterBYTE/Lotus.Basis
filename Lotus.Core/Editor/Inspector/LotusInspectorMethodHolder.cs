#if UNITY_EDITOR
using System;
using System.Reflection;

using Lotus.Core;
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /// <summary>
    /// Данные для вызова метода без аргумента
    /// </summary>
    public class CMethodHolder : IComparable<CMethodHolder>
    {
        #region Fields
        /// <summary>
        /// Атрибут вызова метода
        /// </summary>
        public LotusMethodCallAttribute MethodAttribute;

        /// <summary>
        /// Вызываемый метод
        /// </summary>
        public MethodInfo Method;

        /// <summary>
        /// Экземпляр объекта
        /// </summary>
        public object Instance;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CMethodHolder()
        {
        }
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение объектов для упорядочивания
        /// </summary>
        /// <param name="other">Сравниваемый объект</param>
        /// <returns>Статус сравнения</returns>
        public int CompareTo(CMethodHolder other)
        {
            return MethodAttribute.DrawOrder.CompareTo(other.MethodAttribute.DrawOrder);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение сигнатуры метода
        /// </summary>
        /// <returns>Сигнатура метода</returns>
        protected virtual string GetSignatureMethod()
        {
            return " ()";
        }

        /// <summary>
        /// Вызов метода
        /// </summary>
        protected virtual void InvokeMethod()
        {
            if (Method.IsStatic)
            {
                Method.Invoke(null, null);
            }
            else
            {
                Method.Invoke(Instance, null);
            }
        }

        /// <summary>
        /// Рисование элементов для установки аргументов
        /// </summary>
        protected virtual void DrawArguments()
        {
        }

        /// <summary>
        /// Получение имени метода для отображения
        /// </summary>
        /// <returns>Имя метода</returns>
        protected string GetMethodDisplay()
        {
            if (MethodAttribute.DisplayName.IsExists())
            {
                if (MethodAttribute.IsSignature)
                {
                    return MethodAttribute.DisplayName + GetSignatureMethod();
                }

                return MethodAttribute.DisplayName;
            }

            if (MethodAttribute.IsSignature)
            {
                return Method.Name + GetSignatureMethod();
            }

            return Method.Name;
        }

        /// <summary>
        /// Рисование элементов управления для вызова метода
        /// </summary>
        public virtual void Draw()
        {
            XEditorInspector.DrawDecorateAttributes(Method);

            if (EditorApplication.isPlaying)
            {
                GUI.enabled = MethodAttribute.Mode != TMethodCallMode.OnlyEditor;
            }
            else
            {
                GUI.enabled = MethodAttribute.Mode != TMethodCallMode.OnlyPlay;
            }

            var rectButton = EditorGUILayout.BeginHorizontal(GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
            {
                EditorGUILayout.PrefixLabel(string.Empty);

                if (Event.current.type != EventType.Layout)
                {
                    if (GUI.Button(rectButton, GetMethodDisplay()))
                    {
                        if (Instance != null)
                        {
                            InvokeMethod();
                        }
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }
        #endregion
    }

    /// <summary>
    /// Данные для вызова метода с одним аргументом
    /// </summary>
    public class CMethodHolderArg1 : CMethodHolder
    {
        #region Fields
        /// <summary>
        /// Имя первого аргумента
        /// </summary>
        public string FirstArgumentName;

        /// <summary>
        /// Имя типа первого аргумента
        /// </summary>
        public string FirstArgumentTypeName;

        /// <summary>
        /// Первый аргумент
        /// </summary>
        public CVariant FirstArgument;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CMethodHolderArg1()
        {
            FirstArgument = new CVariant();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение сигнатуры метода
        /// </summary>
        /// <returns>Сигнатура метода</returns>
        protected override string GetSignatureMethod()
        {
            return " (" + FirstArgumentTypeName + " " + FirstArgumentName + ")";
        }

        /// <summary>
        /// Вызов метода
        /// </summary>
        protected override void InvokeMethod()
        {
            CReflectedType.ArgList1[0] = FirstArgument.Get();

            if (Method.IsStatic)
            {
                Method.Invoke(null, CReflectedType.ArgList1);
            }
            else
            {
                Method.Invoke(Instance, CReflectedType.ArgList1);
            }
        }

        /// <summary>
        /// Рисование элементов для установки аргументов
        /// </summary>
        protected override void DrawArguments()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.indentLevel++;
                LotusVariantDrawer.DrawLayoutVariant(FirstArgumentName, FirstArgument);
                EditorGUI.indentLevel--;

                //if (MethodAttribute is LotusMethodArgFileAttribute)
                //{
                //    LotusMethodArgFileAttribute argFile = MethodAttribute as LotusMethodArgFileAttribute;
                //
                //    if (GUILayout.Button("...", EditorStyles.miniButtonRight,
                //        GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
                //    {
                //        string fileName = string.Empty;
                //        if (argFile.IsOpenFile)
                //        {
                //            fileName = XFileDialog.Open("Открыть", argFile.DefaultPath, argFile.Extension);
                //        }
                //        else
                //        {
                //            fileName = XFileDialog.Save("Сохранить", argFile.DefaultPath, argFile.DefaultName, argFile.Extension);
                //        }
                //
                //        if (fileName.IsExists())
                //        {
                //            FirstArgument.StringValue = fileName.RemoveToWith(XEditorSettings.ASSETS_PATH);
                //        }
                //    }
                //}
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Рисование элементов управления для вызова метода
        /// </summary>
        public override void Draw()
        {
            XEditorInspector.DrawDecorateAttributes(Method);

            if (EditorApplication.isPlaying)
            {
                GUI.enabled = MethodAttribute.Mode != TMethodCallMode.OnlyEditor;
            }
            else
            {
                GUI.enabled = MethodAttribute.Mode != TMethodCallMode.OnlyPlay;
            }

            var rectButton = EditorGUILayout.BeginHorizontal(GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
            {
                EditorGUILayout.PrefixLabel(string.Empty);

                if (Event.current.type != EventType.Layout)
                {
                    if (GUI.Button(rectButton, GetMethodDisplay()))
                    {
                        if (Instance != null)
                        {
                            InvokeMethod();
                        }
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            var rectBox = EditorGUILayout.BeginVertical();
            {
                if (Event.current.type != EventType.Layout)
                {
                    rectBox.x += XInspectorViewParams.OFFSET_INDENT;
                    rectBox.width -= XInspectorViewParams.OFFSET_INDENT;
                    rectBox.height += 2;
                    if (rectBox.height > XInspectorViewParams.CONTROL_HEIGHT)
                    {
                        GUI.Box(rectBox, GUIContent.none, XEditorStyles.BOX_SHURIKEN);
                    }
                }
                DrawArguments();
            }
            EditorGUILayout.EndVertical();

            GUI.enabled = true;
        }
        #endregion
    }

    /// <summary>
    /// Данные для вызова метода с двумя аргументами
    /// </summary>
    public class CMethodHolderArg2 : CMethodHolderArg1
    {
        #region Fields
        /// <summary>
        /// Имя второго аргумента
        /// </summary>
        public string SecondArgumentName;

        /// <summary>
        /// Имя типа второго аргумента
        /// </summary>
        public string SecondArgumentTypeName;

        /// <summary>
        /// Второй аргумент
        /// </summary>
        public CVariant SecondArgument;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CMethodHolderArg2()
        {
            FirstArgument = new CVariant();
            SecondArgument = new CVariant();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение сигнатуры метода
        /// </summary>
        /// <returns>Сигнатура метода</returns>
        protected override string GetSignatureMethod()
        {
            return " (" + FirstArgumentTypeName + " " + FirstArgumentName + ", " +
                SecondArgumentTypeName + " " + SecondArgumentName + ")";
        }

        /// <summary>
        /// Вызов метода
        /// </summary>
        protected override void InvokeMethod()
        {
            CReflectedType.ArgList2[0] = FirstArgument.Get();
            CReflectedType.ArgList2[1] = SecondArgument.Get();

            if (Method.IsStatic)
            {
                Method.Invoke(null, CReflectedType.ArgList2);
            }
            else
            {
                Method.Invoke(Instance, CReflectedType.ArgList2);
            }
        }

        /// <summary>
        /// Рисование элементов для установки аргументов
        /// </summary>
        protected override void DrawArguments()
        {
            base.DrawArguments();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.indentLevel++;
                LotusVariantDrawer.DrawLayoutVariant(SecondArgumentName, SecondArgument);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndHorizontal();
        }
        #endregion
    }
}
#endif
