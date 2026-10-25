#if UNITY_EDITOR
using System.Collections.Generic;

using Lotus.Core;
using Lotus.Maths;

using UnityEditor;
using UnityEngine;

using TDimensionPlaneSelect = Lotus.Maths.TDimensionPlaneSelect;

namespace Lotus.Editor
{
    /** \addtogroup MathUnityEditor
    *@{*/
    /// <summary>
    /// Редактор базового компонента представляющего сплайн
    /// </summary>
    /// <typeparam name="TSpline">Тип компонента сплайна</typeparam>
    public class LotusSplineBaseEditor<TSpline> : UnityEditor.Editor where TSpline : LotusSplineBase
    {
        #region Static fields
        /// <summary>
        /// Заголовок группы основных параметров
        /// </summary>
        protected static readonly GUIContent ContentGroupMain = new GUIContent("Main setting");

        /// <summary>
        /// Заголовок группы параметров отображения сплайна
        /// </summary>
        protected static readonly GUIContent ContentGroupDraw = new GUIContent("Draw spline");

        /// <summary>
        /// Заголовок группы параметров редактирования сплайна
        /// </summary>
        protected static readonly GUIContent ContentGroupEditor = new GUIContent("Editor spline");

        /// <summary>
        /// Заголовок группы выбранной контрольной точки
        /// </summary>
        protected static readonly GUIContent ContentGroupSelected = new GUIContent("Selected point");
        #endregion

        #region Fields
        // Основные данные
        protected TSpline _spline;
        protected int _selectedIndexPoint;
        protected Vector3 _selectedPoint;
        protected int _addPointMode;
        protected Vector2 _scrollPosition;
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        protected virtual void OnEnable()
        {
            _spline = target as TSpline;
            if (_spline == null)
            {
                return;
            }

            if (_spline.SegmentsPath == null)
            {
                _spline.SegmentsPath = new List<TMoveSegment>();
            }
        }

        /// <summary>
        /// Рисование на сцене
        /// </summary>
        public void OnSceneGUI()
        {
            if (_spline == null)
            {
                return;
            }

            if (_selectedIndexPoint != -1 && _selectedIndexPoint < _spline.CountPoints)
            {
                // Получаем точку кривой в мировых координатах
                var positionOld = (Vector3)_spline.GetControlPointWorld(_selectedIndexPoint);

                // Точка перемещения ручки в мировых координатах
                var positionNew = Handles.PositionHandle(positionOld, Quaternion.identity);

                // Если они неравны
                if (positionOld != positionNew)
                {
                    switch (_spline.SnapPlane)
                    {
                        case TDimensionPlaneSelect.None:
                            break;
                        case TDimensionPlaneSelect.XZ:
                            positionNew = new Vector3(positionNew.x, positionOld.y, positionNew.z);
                            break;
                        case TDimensionPlaneSelect.XY:
                            positionNew = new Vector3(positionNew.x, positionNew.y, positionOld.z);
                            break;
                        case TDimensionPlaneSelect.ZY:
                            positionNew = new Vector3(positionOld.x, positionNew.y, positionNew.z);
                            break;
                        default:
                            break;
                    }

                    // Перемещаем точку в новую позицию
                    MovePoint(positionNew);
                }

                var handleControlId = GUIUtility.GetControlID(FocusType.Passive);
                Handles.DotHandleCap(handleControlId, positionOld, Quaternion.identity,
                    HandleUtility.GetHandleSize(positionOld) * 0.1f, EventType.Repaint);
            }

            // После ручек: иначе PositionHandle в Unity 6 не получает клики
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            var current = Event.current;
            if (current.type == EventType.MouseDown)
            {
                switch (current.button)
                {
                    // Левая кнопка мыши
                    case 0:
                        {
                            OnMouseDownLeft();
                            Repaint();
                        }
                        break;

                    // Правая кнопка мыши
                    case 1:
                        {
                            OnMouseDownRight();
                            Repaint();
                        }
                        break;
                    default:
                        break;
                }
            }

            if (_selectedIndexPoint == -1 || _addPointMode != 0)
            {
                Selection.activeGameObject = _spline.gameObject;
            }
        }
        #endregion

        #region Control point methods
        /// <summary>
        /// Камера активного Scene View
        /// </summary>
        /// <returns>Камера Scene View или текущая камера отрисовки</returns>
        protected static Camera GetSceneCamera()
        {
            var sceneView = SceneView.currentDrawingSceneView;
            if (sceneView == null)
            {
                sceneView = SceneView.lastActiveSceneView;
            }

            if (sceneView != null && sceneView.camera != null)
            {
                return sceneView.camera;
            }

            return Camera.current;
        }

        /// <summary>
        /// Выбор точки для модификации
        /// </summary>
        /// <param name="screenPoint">Точка в экранных координатах</param>
        /// <param name="camera">Камера</param>
        /// <returns>Индекс выбранной точки или -1</returns>
        protected int SelectPoint(Vector2 screenPoint, Camera camera)
        {
            if (camera == null)
            {
                return -1;
            }

            var radiusSqr = 120f;
            for (var i = 0; i < _spline.CountPoints; i++)
            {
                // Получаем вершину и трансформируем в мировое пространство
                Vector3 vertex = _spline.GetControlPointWorld(i);

                // Проецируем на экран
                var point = HandleUtility.WorldToGUIPoint(vertex);

                var deltaX = screenPoint.x - point.x;
                var deltaXSqr = deltaX * deltaX;
                if (deltaXSqr > radiusSqr)
                {
                    continue;
                }

                var deltaY = screenPoint.y - point.y;
                var deltaYSqr = deltaY * deltaY;
                if (deltaYSqr > radiusSqr)
                {
                    continue;
                }

                if (deltaXSqr + deltaYSqr > radiusSqr)
                {
                    continue;
                }

                return i;
            }

            return -1;
        }

        /// <summary>
        /// Перемещения выбранной точки сплайна
        /// </summary>
        /// <param name="newPosition">Новая позиция точки сплайна в мировых координатах</param>
        protected void MovePoint(Vector3 newPosition)
        {
            // Записываем откат
            Undo.RecordObject(_spline, "Move point");

            // Полностью обновляем кривую
            _spline.SetControlPointWorld(_selectedIndexPoint, newPosition.ToVector3Df(), true);

            // Сохраняем
            serializedObject.Save();
        }

        /// <summary>
        /// Добавление точки к сплайну
        /// </summary>
        /// <param name="point">Точка сплайна в мировых координатах</param>
        protected void AddPoint(Vector3 point)
        {
            // Записываем откат
            Undo.RecordObject(_spline, "Add point");

            // Режим добавления в конце
            if (_addPointMode == 1)
            {
                _spline.AddControlPointWorld(point.ToVector3Df(), true);
            }
            else
            {
                _spline.InsertControlPointWorld(0, point.ToVector3Df(), true);
            }

            serializedObject.Save();
        }

        /// <summary>
        /// Удаление точки сплайна по индексу
        /// </summary>
        /// <param name="index">Позиция(индекс) удаляемой точки</param>
        protected void DeletePoint(int index)
        {
            // Записываем откат
            Undo.RecordObject(_spline, "Delete Point");

            // Удаляем точку
            _spline.RemoveControlPoint(index, true);

            // Сохраняем
            serializedObject.Save();
        }

        /// <summary>
        /// Добавление точки посередине к сплайну перед указанной точки
        /// </summary>
        /// <param name="index">Индекс точки перед которой добавляется новая точка</param>
        protected void InsertBeforePoint(int index)
        {
            if (index > 0)
            {
                // Записываем откат
                Undo.RecordObject(_spline, "Insert Point");

                // Считаем точку посередине
                Vector3 p1 = _spline.GetControlPoint(index);
                Vector3 p2 = _spline.GetControlPoint(index - 1);

                // Добавляем точку посередине
                _spline.InsertControlPointWorld(index - 1, ((p1 + p2) / 2).ToVector3Df(), true);

                // Сохраняем
                serializedObject.Save();
            }
        }

        /// <summary>
        /// Добавление точки посередине к сплайну после указанной точки
        /// </summary>
        /// <param name="index">Индекс точки после которой добавляется новая точка</param>
        protected void InsertAfterPoint(int index)
        {
            if (index < _spline.CountPoints - 1)
            {
                // Записываем откат
                Undo.RecordObject(_spline, "Insert Point");

                // Считаем точку посередине
                Vector3 p1 = _spline.GetControlPoint(index);
                Vector3 p2 = _spline.GetControlPoint(index + 1);

                // Добавляем точку посередине
                _spline.InsertControlPointWorld(index + 1, ((p1 + p2) / 2).ToVector3Df(), true);

                // Сохраняем
                serializedObject.Save();
            }
        }

        /// <summary>
        /// Получение точки в пространстве
        /// </summary>
        /// <param name="screenPoint">Точка в экранных координатах</param>
        /// <param name="camera">Камера</param>
        /// <param name="position">Результирующая точка в пространстве</param>
        /// <returns>Статус успешности получения точки</returns>
        protected bool GetClickPoint(Vector2 screenPoint, Camera camera, out Vector3 position)
        {
            if (camera == null)
            {
                position = Vector3.zero;
                return false;
            }

            // Получаем луч
            var ray = HandleUtility.GUIPointToWorldRay(screenPoint);

            // Строим плоскость
            var plane = new Plane();
            switch (_spline.SnapPlane)
            {
                case TDimensionPlaneSelect.XZ:
                    {
                        plane = new Plane(Vector3.up, Vector3.zero);
                    }
                    break;
                case TDimensionPlaneSelect.XY:
                    {
                        plane = new Plane(Vector3.forward, Vector3.zero);
                    }
                    break;
                case TDimensionPlaneSelect.ZY:
                    {
                        plane = new Plane(Vector3.right, Vector3.zero);
                    }
                    break;
                default:
                    break;
            }

            // Пускаем луч и смотрим есть ли пересечение
            if (plane.Raycast(ray, out var distance))
            {
                position = ray.GetPoint(distance);
                position = _spline.transform.InverseTransformPoint(position);
                return true;
            }

            position = Vector3.zero;
            return false;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Нажатие левой кнопки мыши на узел сплайна для редактирования
        /// </summary>
        protected virtual void OnMouseDownLeft()
        {
        }

        /// <summary>
        /// Нажатие правой кнопки мыши на узел сплайна для редактирования
        /// </summary>
        protected virtual void OnMouseDownRight()
        {
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование параметров контрольных точек
        /// </summary>
        /// <param name="isRemove">Возможность удаления контрольной точки</param>
        protected virtual void DrawControlPoint(bool isRemove)
        {
            EditorGUI.BeginChangeCheck();
            {
                GUILayout.Space(2.0f);
                _spline.StartPoint = XEditorInspector.PropertyVector3D(nameof(_spline.StartPoint), _spline.StartPoint).ToVector3Df();

                var height = Mathf.Clamp(XInspectorViewParams.CONTROL_HEIGHT_SPACE * (_spline.CountPoints - 1),
                    XInspectorViewParams.CONTROL_HEIGHT_SPACE * 2, XInspectorViewParams.CONTROL_HEIGHT_SPACE * 8);

                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(height + 4));
                {
                    for (var i = 1; i < _spline.CountPoints - 1; i++)
                    {
                        GUILayout.Space(2.0f);
                        if (isRemove)
                        {
                            var isRemoved = false;
                            EditorGUILayout.BeginHorizontal();
                            {
                                _spline.ControlPoints[i] = XEditorInspector.PropertyVector3D("Point " + i.ToString(),
                                    _spline.ControlPoints[i]).ToVector3Df();
                                if (GUILayout.Button("X", EditorStyles.miniButtonRight))
                                {
                                    _spline.RemoveControlPoint(i, true);
                                    isRemoved = true;
                                }
                            }
                            EditorGUILayout.EndHorizontal();

                            if (isRemoved)
                            {
                                break;
                            }
                        }
                        else
                        {
                            _spline.ControlPoints[i] = XEditorInspector.PropertyVector3D("Point " + i.ToString(),
                                _spline.ControlPoints[i]).ToVector3Df();
                        }
                    }
                }
                GUILayout.EndScrollView();

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
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }
        }

        /// <summary>
        /// Рисование параметров отображения сплайна
        /// </summary>
        protected virtual void DrawVisualSpline()
        {
            EditorGUI.BeginChangeCheck();
            {
                GUILayout.Space(4.0f);
                XEditorInspector.DrawGroup(ContentGroupDraw);

                GUILayout.Space(2.0f);
                _spline.SizeControlHandle = XEditorInspector.PropertyFloatSlider("HandleSize", _spline.SizeControlHandle, 10, 50);

                GUILayout.Space(2.0f);
                _spline.ColorControlStart = XEditorInspector.PropertyColor("ControlStart", _spline.ColorControlStart);

                GUILayout.Space(2.0f);
                _spline.ColorControl = XEditorInspector.PropertyColor("Control", _spline.ColorControl);

                GUILayout.Space(2.0f);
                _spline.ColorControlEnd = XEditorInspector.PropertyColor("ControlEnd", _spline.ColorControlEnd);

                GUILayout.Space(2.0f);
                _spline.ColorSpline = XEditorInspector.PropertyColor("Spline", _spline.ColorSpline);

                GUILayout.Space(2.0f);
                _spline.ColorSplineAlt = XEditorInspector.PropertyColor("SplineAlt", _spline.ColorSplineAlt);
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }
        }

        /// <summary>
        /// Рисование параметров редактирования сплайна
        /// </summary>
        protected virtual void DrawEditorSpline()
        {
        }

        /// <summary>
        /// Рисование параметров редактирования контрольной точки
        /// </summary>
        protected virtual void DrawEditorPoint()
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
