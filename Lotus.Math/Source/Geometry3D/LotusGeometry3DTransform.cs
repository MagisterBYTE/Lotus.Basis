//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 3D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry3DTransform.cs
*		Трансформация трехмерного объекта.
*		Реализация типа реализующего комплексную трансформацию, удобное и эффективное управления параметрами
*	трансформации трехмерного объекта.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup MathGeometry3D
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Трансформация трехмерного объекта
		/// </summary>
		/// <remarks>
		/// Реализация типа реализующего комплексную трансформацию, удобное и эффективное управления параметрами
		/// трансформации трехмерного объекта
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public struct Transform3Df
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal Vector3Df mPivot;
			internal Vector3Df mOffset;
			internal Quaternion3Df mRotation;
			internal Vector3Df mForward;
			internal Vector3Df mRight;
			internal Vector3Df mUp;
			internal Boolean mUpdateOrt;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Опорная точка трехмерного объекта
			/// </summary>
			public Vector3Df Pivot
			{
				get { return (mPivot); }
				set 
				{
					mPivot = value;
				}
			}

			/// <summary>
			/// Смещение трехмерного объекта
			/// </summary>
			public Vector3Df Offset
			{
				get { return (mOffset); }
				set
				{
					mOffset = value;
				}
			}

			/// <summary>
			/// Вращение трехмерного объекта
			/// </summary>
			public Quaternion3Df Rotation
			{
				get { return (mRotation); }
				set { mRotation = value; }
			}

			/// <summary>
			/// Вектор "вперед"
			/// </summary>
			public Vector3Df Forward
			{
				get { return (mForward); }
				set
				{

				}
			}

			/// <summary>
			/// Вектор "вправо"
			/// </summary>
			public Vector3Df Right
			{
				get { return (mRight); }
				set
				{

				}
			}

			/// <summary>
			/// Вектор вверх
			/// </summary>
			public Vector3Df Up
			{
				get { return (mUp); }
				set
				{

				}
			}

			/// <summary>
			/// Матрица трансформации объекта
			/// </summary>
			public Matrix4Dx4 MatrixTransform
			{
				get
				{
					var transform = new Matrix4Dx4();

					//Vector3Df pos = Pivot;
					//Single x = -Vector3Df.Dot(ref mRight, ref pos);
					//Single y = -Vector3Df.Dot(ref mUp, ref pos);
					//Single z = -Vector3Df.Dot(ref mForward, ref pos);

					//transform.M11 = mRight.X;
					//transform.M12 = mUp.X;
					//transform.M13 = mForward.X;
					//transform.M14 = 0.0;

					//transform.M21 = mRight.Y;
					//transform.M22 = mUp.Y;
					//transform.M23 = mForward.Y;
					//transform.M24 = 0.0;

					//transform.M31 = mRight.Z;
					//transform.M32 = mUp.Z;
					//transform.M33 = mForward.Z;
					//transform.M34 = 0.0;

					//transform.M41 = x;
					//transform.M42 = y;
					//transform.M43 = z;
					//transform.M44 = 1.0;

					return (transform);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует трансформацию указанными параметрами
			/// </summary>
			/// <param name="pivot">Опорная точка объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public Transform3Df(Vector3Df pivot)
			{
				mPivot = pivot;
				mOffset = Vector3Df.Zero;
				mRotation = Quaternion3Df.Identity;
				mForward = Vector3Df.Forward;
				mRight = Vector3Df.Right;
				mUp = Vector3Df.Up;
				mUpdateOrt = false;
			}
			#endregion

			#region ======================================= МЕТОДЫ ТРАНСФОРМАЦИИ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка локального вращения объекта вокруг собственной оси
			/// </summary>
			/// <param name="axis">Ось вращения</param>
			/// <param name="angle">Угол вращения, задается в градусах</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetRotate(in Vector3Df axis, Single angle)
			{
				// Локальное вращение
				mRotation = new Quaternion3Df(axis, angle);

				// Трансформируем орты
				mUp = mRotation.TransformVector(Vector3Df.Up);
				mForward = mRotation.TransformVector(Vector3Df.Forward);
				mRight = mRotation.TransformVector(Vector3Df.Right);
				mUpdateOrt = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Локальное вращение объекта вокруг собственной оси
			/// </summary>
			/// <param name="axis">Ось вращения</param>
			/// <param name="angle">Угол приращения, задается в градусах</param>
			//---------------------------------------------------------------------------------------------------------
			public void Rotate(in Vector3Df axis, Single angle)
			{
				//// Локальное вращение
				//mAngleAxisPivot += angle;
				//mRotation = new Quaternion3D(axis, mAngleAxisPivot);

				//// Трансформируем орты
				//mUp = mRotation.TransformVector(Vector3Df.Up);
				//mForward = mRotation.TransformVector(Vector3Df.Forward);
				//mRight = mRotation.TransformVector(Vector3Df.Right);
				//mUpdateOrt = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка вращения объекта вокруг оси и определенного центра
			/// </summary>
			/// <param name="center">Центр вращения</param>
			/// <param name="axis">Ось вращения</param>
			/// <param name="angle">Угол вращения, задается в градусах</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetRotateAround(in Vector3Df center, in Vector3Df axis, Single angle)
			{
				// Глобальное вращение
				//Quaternion3D rotation = new Quaternion3D(axis, angle + 180);
				//mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
				//mPositionGlobal = center;
				//mAngleAxisGlobal = angle;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка вращения объекта вокруг оси и определенного центра
			/// </summary>
			/// <param name="center">Центр вращения</param>
			/// <param name="axis">Ось вращения</param>
			/// <param name="angle">Угол вращения, задается в градусах</param>
			/// <param name="parent">Родительская трансформация</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetRotateAround(in Vector3Df center, in Vector3Df axis, Single angle, Transform3Df parent)
			{
				// Глобальное вращение
				//Vector3Df axis_d = new Vector3Df(axis);
				//Quaternion3D rotation = new Quaternion3D(axis_d, angle + 180);
				//mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
				//mPositionGlobal = center - parent.Pivot;
				//mAngleAxisGlobal = angle;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вращение объекта вокруг оси и определенного центра
			/// </summary>
			/// <param name="center">Центр вращения</param>
			/// <param name="axis">Ось вращения</param>
			/// <param name="angle">Угол приращения, задается в градусах</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateAround(in Vector3Df center, in Vector3Df axis, Single angle)
			{
				// Глобальное вращение
				//mAngleAxisGlobal += angle;
				//Quaternion3D rotation = new Quaternion3D(axis, mAngleAxisGlobal + 180);
				//mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
				//mPositionGlobal = center;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вращение объекта вокруг оси и определенного центра
			/// </summary>
			/// <param name="center">Центр вращения</param>
			/// <param name="axis">Ось вращения</param>
			/// <param name="angle">Угол приращения, задается в градусах</param>
			/// <param name="parent">Родительская трансформация</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateAround(in Vector3Df center, in Vector3Df axis, Single angle, Transform3Df parent)
			{
				//// Глобальное вращение
				//Vector3Df axis_d = new Vector3Df(axis);
				//mAngleAxisGlobal += angle;
				//Quaternion3D rotation = new Quaternion3D(axis_d, mAngleAxisGlobal + 180);

				//// 1) Переносим позицию в локальный центр
				////Vector3Df position = mOriginalPosition + center;
				//mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
				//mPositionGlobal = center;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка направления взгляда объекта на определенную точку
			/// </summary>
			/// <param name="point">Точка</param>
			/// <param name="up">Вектор вверх</param>
			//---------------------------------------------------------------------------------------------------------
			public void LookAt(Vector3Df point, Vector3Df up)
			{
				Vector3Df direction = point - Pivot;
				direction.Normalize();
				mRotation.SetLookRotation(in direction, in up);
				
				// Трансформируем орты
				mForward = direction;
				mUp = up;
				mRight = Vector3Df.Cross(in mUp, in mForward);
				mUpdateOrt = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление орт
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Update()
			{
				if (mUpdateOrt)
				{
					mUp = Vector3Df.Cross(in mForward, in mRight);
					mRight = Vector3Df.Cross(in mUp, in mForward);
					

					mForward.Normalize();
					mRight.Normalize();
					mUp.Normalize();
					mUpdateOrt = false;
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================