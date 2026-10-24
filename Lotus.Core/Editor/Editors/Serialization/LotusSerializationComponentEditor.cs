#if UNITY_EDITOR
using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор компонента для сериализации данных
    /// </summary>
    [CustomEditor(typeof(LotusSerializationComponent))]
    public class LotusSerializationComponentEditor : UnityEditor.Editor
    {
        #region Fields
        private LotusSerializationComponent _serialization;
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        public void OnEnable()
        {
            _serialization = target as LotusSerializationComponent;
        }

        /// <summary>
        /// Рисование в инспекторе объектов
        /// </summary>
        public override void OnInspectorGUI()
        {
            GUILayout.Space(4.0f);
            EditorGUI.BeginChangeCheck();
            {
                GUILayout.Space(2.0f);
                EditorGUILayout.BeginHorizontal();
                {
                    XEditorInspector.DrawGroup("IDSerialKey = " + _serialization.IDKeySerial.ToString());
                    if (GUILayout.Button("Generate"))
                    {
                        _serialization.IDKeySerial = XGenerateId.Generate(_serialization);
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(4.0f);
                _serialization.SerializationVolume = (TSerializationVolume)XEditorInspector.PropertyFlags(
                    "SerialVolume", _serialization.SerializationVolume);
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }

            GUILayout.Space(2.0f);
        }
        #endregion
    }
    /**@}*/
}
#endif
