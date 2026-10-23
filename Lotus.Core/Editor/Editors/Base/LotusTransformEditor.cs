
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusTransformEditor.cs
*		Редактор компонента трансформации.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEditor;
using UnityEngine;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор компонента трансформации
/// </summary>
------------
[CustomEditor(typeof(Transform))]
[CanEditMultipleObjects]
public class LotusInspectorTransformEditor : Editor
{
	#region =============================================== КОНСТАНТНЫЕ ДАННЫЕ ========================================
	/// <summary>
	/// Ширина поля для редактирования значения
	/// </summary>
	private const Single FIELD_WIDTH = 212.0f;

	/// <summary>
	/// Статус расширенного режима отображения
	/// </summary>
	private const Boolean WIDE_MODE = true;

	/// <summary>
	/// Максимальное значение для позиции
	/// </summary>
	private const Single MAX_VALUE_POS = 100000.0f;

	/// <summary>
	/// Контент для отображения позиции
	/// </summary>
	private static GUIContent ContentPosition = new GUIContent(LocalString("Position"),
		LocalString("The local position of this Game Object relative to the parent"));

	/// <summary>
	/// Контент для отображения вращения
	/// </summary>
	private static GUIContent ContentRotation = new GUIContent(LocalString("Rotation"), 
		LocalString("The local rotation of this Game Object relative to the parent"));

	/// <summary>
	/// Контент для отображения масштаба
	/// </summary>
	private static GUIContent ContentScale = new GUIContent(LocalString("Scale"), 
		LocalString("The local scaling of this Game Object relative to the parent"));

	/// <summary>
	/// Контент для отображения масштаба
	/// </summary>
	private static GUIContent ContentButtonZero = new GUIContent("X", "Set zero value");

	/// <summary>
	/// Предупреждение
	/// </summary>
	private static String WarningTextPos = LocalString("Due to floating-point precision limitations, it is recommended to bring the world coordinates of the GameObject within a smaller range.");
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	// Основные параметры
	protected Transform mTransform;
	protected SerializedProperty mPositionProperty;
	protected SerializedProperty mRotationProperty;
	protected SerializedProperty mScaleProperty;

	// Привязка
	protected Boolean mIsSnapPositionX;
	protected Boolean mIsSnapPositionY;
	protected Boolean mIsSnapPositionZ;
	protected Vector3 mSnapPositionSize;

	// Расширенное управление
	protected Boolean mShowGlobalPosition;
	protected Boolean mShowStepMove;
	protected Vector3 mStepMove = new Vector3(2, 2, 2);
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Включение скрипта в инспекторе объектов
	/// </summary>
	--------
	public void OnEnable()
	{
		mTransform = this.target as Transform;

		this.mPositionProperty = this.serializedObject.FindProperty("m_LocalPosition");
		this.mRotationProperty = this.serializedObject.FindProperty("m_LocalRotation");
		this.mScaleProperty = this.serializedObject.FindProperty("m_LocalScale");
	}

	--------
	/// <summary>
	/// Рисование в инспекторе объектов
	/// </summary>
	--------
	public override void OnInspectorGUI()
	{
		EditorGUILayout.LabelField("Custom Inspector Lotus", EditorStyles.centeredGreyMiniLabel);

		EditorGUIUtility.wideMode = WIDE_MODE;

		// align field to right of inspector
		if (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth < FIELD_WIDTH)
		{
			EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - FIELD_WIDTH;
		}

		this.serializedObject.Update();

		this.PositionPropertyField();

		this.RotationPropertyField();

		this.ScalePropertyField();

		mShowStepMove = XEditorInspector.PropertyBoolean("Step move", mShowStepMove);

		var text = String.Format("I: {0}, WP: {1}, LS: {2}", mTransform.GetSiblingIndex(), mTransform.position.ToString(),
			mTransform.lossyScale.ToString());
		EditorGUILayout.HelpBox(text, MessageType.None);

		if (!ValidatePosition(mTransform.position))
		{
			EditorGUILayout.HelpBox(WarningTextPos, MessageType.Warning);
		}

		this.serializedObject.ApplyModifiedProperties();
	}
	#endregion

	#region =============================================== ОБЩИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Получение локализованной версии текстовых данных
	/// </summary>
	/// <param name="text">Исходная строка</param>
	/// <returns>Локализованная строка</returns>
	--------
	private static String LocalString(String text)
	{
		return (text);
	}

	--------
	/// <summary>
	/// Проверка на корректность позиции
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	/// <returns>Статус корректности позиции</returns>
	--------
	private Boolean ValidatePosition(Vector3 position)
	{
		if (Mathf.Abs(position.x) > MAX_VALUE_POS) return (false);
		if (Mathf.Abs(position.y) > MAX_VALUE_POS) return (false);
		if (Mathf.Abs(position.z) > MAX_VALUE_POS) return (false);
		return (true);
	}

	--------
	/// <summary>
	/// Установка нулевой позиции объекта
	/// </summary>
	--------
	private void SetPositionZero()
	{
		for (var i = 0; i < Selection.transforms.Length; i++)
		{
			Selection.transforms[i].localPosition = Vector3.zero;
		}

		this.mPositionProperty.serializedObject.SetIsDifferentCacheDirty();
	}

	--------
	/// <summary>
	/// Смещение объекта на указанный шаг
	/// </summary>
	/// <param name="x">Смещение по X</param>
	/// <param name="y">Смещение по Y</param>
	/// <param name="z">Смещение по Z</param>
	--------
	private void OffsetPosition(Single x, Single y, Single z)
	{
		for (var i = 0; i < Selection.transforms.Length; i++)
		{
			Selection.transforms[i].localPosition += new Vector3(x, y, z);
		}

		this.mPositionProperty.serializedObject.SetIsDifferentCacheDirty();
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	--------
	/// <summary>
	/// Редактор свойства позиции игрового объекта
	/// </summary>
	--------
	protected void PositionPropertyField()
	{
		EditorGUILayout.BeginVertical();
		{
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.PropertyField(this.mPositionProperty, ContentPosition);
				if (GUILayout.Button(ContentButtonZero, EditorStyles.miniButtonRight, GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
				{
					SetPositionZero();
				}
			}
			EditorGUILayout.EndHorizontal();

			if (mShowStepMove)
			{
				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Space(EditorGUIUtility.labelWidth + 14);

					if (GUILayout.Button(XString.TriangleLeft, EditorStyles.miniButtonLeft))
					{
						OffsetPosition(-mStepMove.x, 0, 0);
					}

					if (GUILayout.Button(XString.TriangleRight, EditorStyles.miniButtonRight))
					{
						OffsetPosition(mStepMove.x, 0, 0);
					}

					GUILayout.Space(12);

					if (GUILayout.Button(XString.TriangleLeft, EditorStyles.miniButtonLeft))
					{
						OffsetPosition(0, -mStepMove.y, 0);
					}

					if (GUILayout.Button(XString.TriangleRight, EditorStyles.miniButtonRight))
					{
						OffsetPosition(0, mStepMove.y, 0);
					}

					GUILayout.Space(12);

					if (GUILayout.Button(XString.TriangleLeft, EditorStyles.miniButtonLeft))
					{
						OffsetPosition(0, 0, -mStepMove.z);
					}

					if (GUILayout.Button(XString.TriangleRight, EditorStyles.miniButtonRight))
					{
						OffsetPosition(0, 0, mStepMove.z);
					}

					GUILayout.Space(22);
				}
				EditorGUILayout.EndHorizontal();
			}

		}
		EditorGUILayout.EndVertical();
	}

	--------
	/// <summary>
	/// Редактор свойства вращения игрового объекта
	/// </summary>
	--------
	protected void RotationPropertyField()
	{
		Quaternion local_rotation = mTransform.localRotation;
		Transform[] transforms = Selection.transforms;
		for (var i = 0; i < transforms.Length; i++)
		{
			if (local_rotation != transforms[i].localRotation)
			{
				EditorGUI.showMixedValue = true;
				break;
			}
		}

		EditorGUI.BeginChangeCheck();

		Vector3 euler_angles = Vector3.zero;
		//euler_angles.x = XMath.RoundToNearest(local_rotation.eulerAngles.x, 1);
		//euler_angles.y = XMath.RoundToNearest(local_rotation.eulerAngles.y, 1);
		//euler_angles.z = XMath.RoundToNearest(local_rotation.eulerAngles.z, 1);
		//local_rotation.eulerAngles = euler_angles;

		EditorGUILayout.BeginHorizontal();
		{
			euler_angles = EditorGUILayout.Vector3Field(ContentRotation, local_rotation.eulerAngles);
			if (GUILayout.Button(ContentButtonZero, EditorStyles.miniButtonRight, GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
			{
				euler_angles = Vector3.zero;
				for (var i = 0; i < Selection.transforms.Length; i++)
				{
					Selection.transforms[i].localEulerAngles = Vector3.zero;
				}

				this.mRotationProperty.serializedObject.SetIsDifferentCacheDirty();
			}
		}
		EditorGUILayout.EndHorizontal();


		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObjects(this.targets, "Rotation Changed");

			transforms = Selection.transforms;
			for (var i = 0; i < transforms.Length; i++)
			{
				transforms[i].localEulerAngles = euler_angles;
			}


			this.mRotationProperty.serializedObject.SetIsDifferentCacheDirty();
		}

		EditorGUI.showMixedValue = false;
	}

	--------
	/// <summary>
	/// Редактор свойства масштаба игрового объекта
	/// </summary>
	--------
	protected void ScalePropertyField()
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.PropertyField(this.mScaleProperty, ContentScale);
			if (GUILayout.Button(ContentButtonZero, EditorStyles.miniButtonRight, GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
			{
				for (var i = 0; i < Selection.transforms.Length; i++)
				{
					Selection.transforms[i].localScale = Vector3.one;
				}

				this.mScaleProperty.serializedObject.SetIsDifferentCacheDirty();
			}
		}
		EditorGUILayout.EndHorizontal();
	}
	#endregion
}
