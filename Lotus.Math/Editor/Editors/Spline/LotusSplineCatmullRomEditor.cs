#if UNITY_EDITOR
using Lotus.Maths;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup MathUnityEditor
    *@{*/
    /// <summary>
    /// Редактор компонента представляющий сплайн CatmullRom
    /// </summary>
    [CustomEditor(typeof(LotusSplineCatmullRom))]
    public class LotusSplineCatmullRomEditor : LotusSplineBaseEditor<LotusSplineCatmullRom>
    {
        #region Static fields
        /// <summary>
        /// Контент кнопки добавления точки в начало сплайна
        /// </summary>
        protected static readonly GUIContent ContentButtonFirst = new GUIContent("Добавить точку сначала");

        /// <summary>
        /// Контент кнопки добавления точки в конец сплайна
        /// </summary>
        protected static readonly GUIContent ContentButtonLast = new GUIContent("Добавить точку в конце");

        /// <summary>
        /// Контент кнопки остановки режима добавления точек
        /// </summary>
        protected static readonly GUIContent ContentButtonStop = new GUIContent("Стоп");

        /// <summary>
        /// Контент кнопки вставки точки после выбранной
        /// </summary>
        protected static readonly GUIContent ContentButtonInsertAfter = new GUIContent("Вставить точку после");

        /// <summary>
        /// Контент кнопки вставки точки до выбранной
        /// </summary>
        protected static readonly GUIContent ContentButtonInsertBefore = new GUIContent("Вставить точку до");
        #endregion

        #region Unity events
        /// <summary>
        /// Рисование в инспекторе объектов
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Основные параметры
            GUILayout.Space(4.0f);
            XEditorInspector.DrawGroup(ContentGroupMain);

            GUILayout.Space(2.0f);
            _spline.IsClosed = XEditorInspector.PropertyBoolean(nameof(_spline.IsClosed), _spline.IsClosed);

            // Рисование параметров контрольных точек
            DrawControlPoint(false);

            // Рисование параметров отображения сплайна
            DrawVisualSpline();

            // Рисование параметров редактирования сплайна
            DrawEditorSpline();

            // Рисование параметров редактирования контрольной точки
            DrawEditorPoint();

            GUILayout.Space(2.0f);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Нажатие левой кнопки мыши на узел сплайна для редактирования
        /// </summary>
        protected override void OnMouseDownLeft()
        {
            var current = Event.current;
            var camera = GetSceneCamera();

            // Если есть режим добавления
            if (_addPointMode != 0)
            {
                if (GetClickPoint(current.mousePosition, camera, out var result))
                {
                    AddPoint(result);
                }
            }
            else
            {
                var selected = SelectPoint(current.mousePosition, camera);
                if (selected != -1)
                {
                    _selectedIndexPoint = selected;
                    current.Use();
                }
                else
                {
                    _selectedIndexPoint = -1;
                }
            }
        }

        /// <summary>
        /// Нажатие правой кнопки мыши на узел сплайна для редактирования
        /// </summary>
        protected override void OnMouseDownRight()
        {
            var current = Event.current;
            var index = SelectPoint(current.mousePosition, GetSceneCamera());
            if (index != -1)
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("Delete Point"), false, () => DeletePoint(index));
                menu.AddItem(new GUIContent("Insert Before"), false, () => InsertBeforePoint(index));
                menu.AddItem(new GUIContent("Insert After"), false, () => InsertAfterPoint(index));
                menu.ShowAsContext();
                current.Use();
            }
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование параметров редактирования сплайна
        /// </summary>
        protected override void DrawEditorSpline()
        {
            EditorGUI.BeginChangeCheck();
            {
                GUILayout.Space(4.0f);
                XEditorInspector.DrawGroup(ContentGroupEditor);

                GUILayout.Space(2.0f);
                var isLast = GUILayout.Toggle(_addPointMode == 1, _addPointMode == 1 ? ContentButtonStop : ContentButtonFirst);
                if (isLast)
                {
                    _addPointMode = 1;
                }

                GUILayout.Space(2.0f);
                var isFirst = GUILayout.Toggle(_addPointMode == 2, _addPointMode == 2 ? ContentButtonStop : ContentButtonLast);
                if (isFirst)
                {
                    _addPointMode = 2;
                }

                if (!isLast && !isFirst)
                {
                    _addPointMode = 0;
                }

                GUILayout.Space(2.0f);
                if (GUILayout.Button(ContentButtonInsertAfter))
                {
                    InsertAfterPoint(_selectedIndexPoint);
                }

                GUILayout.Space(2.0f);
                if (GUILayout.Button(ContentButtonInsertBefore))
                {
                    InsertBeforePoint(_selectedIndexPoint);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
