#if UNITY_EDITOR
using Lotus.Maths;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup MathUnityEditor
    *@{*/
    /// <summary>
    /// Редактор компонента представляющий сплайн BezierPath
    /// </summary>
    [CustomEditor(typeof(LotusSplineBezierPath))]
    public class LotusSplineBezierPathEditor : LotusSplineBaseEditor<LotusSplineBezierPath>
    {
        #region Static fields
        /// <summary>
        /// Контент кнопки добавления кривой
        /// </summary>
        protected static readonly GUIContent ContentAddCurve = new GUIContent("Добавить кривую");

        /// <summary>
        /// Контент кнопки удаления последней кривой
        /// </summary>
        protected static readonly GUIContent ContentRemoveCurve = new GUIContent("Удалить последнюю кривую");
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
            var selected = SelectPoint(current.mousePosition, GetSceneCamera());
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

        /// <summary>
        /// Нажатие правой кнопки мыши на узел сплайна для редактирования
        /// </summary>
        protected override void OnMouseDownRight()
        {
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
                if (GUILayout.Button(ContentAddCurve))
                {
                    _spline.AddCurve();
                }

                GUILayout.Space(2.0f);
                if (GUILayout.Button(ContentRemoveCurve))
                {
                    _spline.RemoveCurve();
                    if (_selectedIndexPoint > _spline.CountPoints)
                    {
                        _selectedIndexPoint = _spline.CountPoints - 1;
                    }
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }
        }

        /// <summary>
        /// Рисование параметров редактирования контрольной точки
        /// </summary>
        protected override void DrawEditorPoint()
        {
            EditorGUI.BeginChangeCheck();
            {
                GUILayout.Space(4.0f);
                XEditorInspector.DrawGroup(ContentGroupSelected);

                if (_selectedIndexPoint != -1)
                {
                    GUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Selected point", _selectedIndexPoint.ToString());

                    GUILayout.Space(2.0f);
                    GUI.changed = false;
                    _selectedPoint = XEditorInspector.PropertyVector3D("World position",
                        _spline.GetControlPointWorld(_selectedIndexPoint));
                    if (GUI.changed)
                    {
                        _spline.SetControlPointWorld(_selectedIndexPoint, _selectedPoint.ToVector3Df(), true);
                    }

                    if (_spline.IsHandlePoint(_selectedIndexPoint))
                    {
                        GUILayout.Space(2.0f);
                        EditorGUI.BeginChangeCheck();
                        var mode = XEditorInspector.PropertyEnum("Mode", _spline.GetHandleMode(_selectedIndexPoint));
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(_spline, "Change Point Mode");
                            _spline.SetHandleMode(_selectedIndexPoint, mode);
                            serializedObject.Save();
                        }
                    }
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
