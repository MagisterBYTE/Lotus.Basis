#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

using Lotus.Core;

namespace Lotus.Editor
{
    /// <summary>
    /// Класс реализующий возможности изменять размер панелей в окне редакторов
    /// </summary>
    public class LotusEditorSplitView
    {
        #region Fields
        protected bool _isHorizontal;
        protected float _splitNormalizedPosition;
        protected bool _isResize;
        protected Vector2 _scrollPosition;
        protected Rect _availableRect;
        protected bool _isClosedLayout;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="isHorizontal">Статус горизонтального расположения</param>
        /// <param name="splitNormalizedPosition">Нормализованная позиция разделителя</param>
        public LotusEditorSplitView(bool isHorizontal, float splitNormalizedPosition = 0.5f)
        {
            _splitNormalizedPosition = splitNormalizedPosition;
            _isHorizontal = isHorizontal;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Начало отображения изменяемой панели
        /// </summary>
        public void BeginSplitView()
        {
            Rect tempRect;

            if (_isHorizontal)
            {
                tempRect = EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            }
            else
            {
                tempRect = EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));
            }

            if (tempRect.width > 0.0f)
            {
                _availableRect = tempRect;
            }

            if (_isHorizontal)
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition,
                    GUILayout.Width(_availableRect.width * _splitNormalizedPosition));
            }
            else
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition,
                    GUILayout.Height(_availableRect.height * _splitNormalizedPosition));
            }

            _isClosedLayout = false;
        }

        /// <summary>
        /// Отображение разделителя
        /// </summary>
        /// <param name="offsetX">Дополнительное смещение разделителя по X</param>
        /// <param name="offsetY">Дополнительное смещение разделителя по Y</param>
        public void Split(float offsetX, float offsetY)
        {
            GUILayout.EndScrollView();
            GUILayout.Space(2.0f);
            ResizeSplitFirstView(offsetX, offsetY);
            _isClosedLayout = true;
        }

        /// <summary>
        /// Окончание отображения изменяемой панели
        /// </summary>
        public void EndSplitView()
        {
            if (!_isClosedLayout)
            {
                _isClosedLayout = true;
                GUILayout.EndScrollView();
            }

            if (_isHorizontal)
            {
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.EndVertical();
            }
        }

        /// <summary>
        /// Изменение размера панели
        /// </summary>
        /// <param name="offsetX">Дополнительное смещение разделителя по X</param>
        /// <param name="offsetY">Дополнительное смещение разделителя по Y</param>
        private void ResizeSplitFirstView(float offsetX, float offsetY)
        {
            Rect resizeHandleRect;

            if (_isHorizontal)
            {
                resizeHandleRect = new Rect((_availableRect.width * _splitNormalizedPosition) + offsetX, _availableRect.y,
                    4f, _availableRect.height);
            }
            else
            {
                resizeHandleRect = new Rect(_availableRect.x, (_availableRect.height * _splitNormalizedPosition) + offsetY,
                    _availableRect.width, 4f);
            }

            GUI.DrawTexture(resizeHandleRect, EditorGUIUtility.isProSkin ? XTexture2D.WhiteAlpha25
                : XTexture2D.BlackAlpha25);

            if (_isHorizontal)
            {
                EditorGUIUtility.AddCursorRect(resizeHandleRect, MouseCursor.ResizeHorizontal);
            }
            else
            {
                EditorGUIUtility.AddCursorRect(resizeHandleRect, MouseCursor.ResizeVertical);
            }

            if (Event.current.type == EventType.MouseDown && resizeHandleRect.Contains(Event.current.mousePosition))
            {
                _isResize = true;
            }

            if (_isResize)
            {
                if (_isHorizontal)
                {
                    _splitNormalizedPosition = (Event.current.mousePosition.x - offsetX) / _availableRect.width;
                }
                else
                {
                    _splitNormalizedPosition = (Event.current.mousePosition.y - offsetY) / _availableRect.height;
                }
            }

            if (Event.current.type == EventType.MouseUp)
            {
                _isResize = false;
            }
        }
        #endregion
    }
}
#endif
