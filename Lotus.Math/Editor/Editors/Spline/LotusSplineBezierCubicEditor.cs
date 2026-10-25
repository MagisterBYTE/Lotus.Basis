#if UNITY_EDITOR
using Lotus.Maths;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup MathUnityEditor
    *@{*/
    /// <summary>
    /// Редактор компонента представляющего кубическую кривую Безье
    /// </summary>
    [CustomEditor(typeof(LotusSplineBezierCubic))]
    public class LotusSplineBezierCubicEditor : LotusSplineBaseEditor<LotusSplineBezierCubic>
    {
        #region Unity events
        /// <summary>
        /// Рисование в инспекторе объектов
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                // Основные параметры
                GUILayout.Space(4.0f);
                XEditorInspector.DrawGroup(ContentGroupMain);

                GUILayout.Space(2.0f);
                _spline.StartPoint = XEditorInspector.PropertyVector3D(nameof(_spline.StartPoint), _spline.StartPoint).ToVector3Df();

                GUILayout.Space(2.0f);
                _spline.HandlePoint1 = XEditorInspector.PropertyVector3D(nameof(_spline.HandlePoint1), _spline.HandlePoint1).ToVector3Df();

                GUILayout.Space(2.0f);
                _spline.HandlePoint2 = XEditorInspector.PropertyVector3D(nameof(_spline.HandlePoint2), _spline.HandlePoint2).ToVector3Df();

                GUILayout.Space(2.0f);
                _spline.EndPoint = XEditorInspector.PropertyVector3D(nameof(_spline.EndPoint), _spline.EndPoint).ToVector3Df();

                GUILayout.Space(2.0f);
                _spline.SegmentsSpline = XEditorInspector.PropertyInt(nameof(_spline.SegmentsSpline), _spline.SegmentsSpline);

                GUILayout.Space(2.0f);
                _spline.SnapPlane = XEditorInspector.PropertyEnum(nameof(_spline.SnapPlane), _spline.SnapPlane);

                GUILayout.Space(2.0f);
                EditorGUILayout.LabelField(nameof(_spline.Length), _spline.Length.ToString("F2"));

                GUILayout.Space(2.0f);
                EditorGUILayout.LabelField("CountDraw", _spline.DrawingPoints.Count.ToString());

                // Рисование параметров отображения сплайна
                DrawVisualSpline();

                // Рисование параметров редактирования контрольной точки
                DrawEditorPoint();
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }

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
    }
    /**@}*/
}
#endif
