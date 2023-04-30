//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 3D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry3DQuaternion.cs
*		Кватернион.
*		Реализация кватерниона для эффективного представления вращения объектов в трехмерном пространстве.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
		/// Кватернион
		/// </summary>
		/// <remarks>
		/// Реализация кватерниона для эффективного представления вращения объектов в трехмерном пространстве
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Quaternion3D : IEquatable<Quaternion3D>, IComparable<Quaternion3D>, ICloneable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Единичный кватернион
			/// </summary>
			public static readonly Quaternion3D Identity = new Quaternion3D(0, 0, 0);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ  =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона посредством вектора оси и угла поворота
			/// </summary>
			/// <param name="axis">Ось поворота</param>
			/// <param name="angle">Угол поворота (в градусах)</param>
			/// <param name="result">Результирующий кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public static void AxisAngle(in Vector3D axis, Double angle, out Quaternion3D result)
			{
				Vector3D v = axis.Normalized;

				var half_angle = angle * 0.5;
				var sin_a = Math.Sin(half_angle * XMath.DegreeToRadian_d);

				result.X = v.X * sin_a;
				result.Y = v.Y * sin_a;
				result.Z = v.Z * sin_a;
				result.W = Math.Cos(half_angle * XMath.DegreeToRadian_d);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота от одного направления до другого по кратчайшей дуге
			/// </summary>
			/// <param name="from_direction">Начальное направление</param>
			/// <param name="to_direction">Требуемое направление</param>
			/// <param name="result">Результирующий кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public static void FromToRotation(in Vector3D from_direction, in Vector3D to_direction, out Quaternion3D result)
			{
				// Получаем ось вращения
				Vector3D axis = from_direction ^ to_direction;

				result.X = axis.X;
				result.Y = axis.Y;
				result.Z = axis.Z;
				result.W = from_direction * to_direction;
				result.Normalize();

				// reducing angle to halfangle
				result.W += 1.0;

				// angle close to PI
				if (result.W <= XMath.Eplsilon_d)
				{
					if (from_direction.Z * from_direction.Z > from_direction.X * from_direction.X)
					{
						// from * vector3(1,0,0) 
						result.Set(0, from_direction.Z, -from_direction.Y, result.W);
					}
					else
					{
						//from * vector3(0,0,1) 
						result.Set(from_direction.Y, -from_direction.X, 0, result.W);
					}
				}

				// Нормализация
				result.Normalize();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота по направлению взгляда
			/// </summary>
			/// <param name="direction">Вектор направления</param>
			/// <param name="up">Вектор "вверх"</param>
			/// <param name="result">Результирующий кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetLookRotation(in Vector3D direction, in Vector3D up, out Quaternion3D result)
			{
				// Step 1. Setup basis vectors describing the rotation given the
				// input vector and assuming an initial up direction of (0, 1, 0)
				// The perpendicular vector to Up and Direction
				var right = Vector3D.Cross(in up, in direction);

				// The actual up vector given the direction and the right vector
				var upCalc = Vector3D.Cross(in direction, in right);


				// Step 2. Put the three vectors into the matrix to bulid a basis rotation matrix
				// This step isnt necessary, but im adding it because often you would want to convert from matricies to quaternions instead of vectors to quaternions
				// If you want to skip this step, you can use the vector values directly in the quaternion setup below
				var basis = new Matrix4Dx4(right.X, right.Y, right.Z, 0.0,
												  upCalc.X, upCalc.Y, upCalc.Z, 0.0,
												  direction.X, direction.Y, direction.Z, 0.0,
												  0.0, 0.0, 0.0, 1.0);

				// Преобразуем в кватернион.
				result = basis.ToQuaternion3D();

				result.Normalize();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция кватернионов
			/// </summary>
			/// <param name="from">Начальный кватернион</param>
			/// <param name="to">Конечный кватернион</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D Lerp(in Quaternion3D from, in Quaternion3D to, Double time)
			{
				Quaternion3D quaternion;
				quaternion.X = from.X + ((to.X - from.X) * time);
				quaternion.Y = from.Y + ((to.Y - from.Y) * time);
				quaternion.Z = from.Z + ((to.Z - from.Z) * time);
				quaternion.W = from.W + ((to.W - from.W) * time);
				return quaternion;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Десереализация кватерниона из строки
			/// </summary>
			/// <param name="data">Строка данных</param>
			/// <returns>Кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D DeserializeFromString(String data)
			{
				var quaternion = new Quaternion3D();
				var quaternion_data = data.Split(';');
				quaternion.X = XMath.ParseDouble(quaternion_data[0]);
				quaternion.Y = XMath.ParseDouble(quaternion_data[1]);
				quaternion.Z = XMath.ParseDouble(quaternion_data[2]);
				quaternion.W = XMath.ParseDouble(quaternion_data[3]);
				return quaternion;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Координата X
			/// </summary>
			public Double X;

			/// <summary>
			/// Координата Y
			/// </summary>
			public Double Y;

			/// <summary>
			/// Координата Z
			/// </summary>
			public Double Z;

			/// <summary>
			/// Координата W
			/// </summary>
			public Double W;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Квадрат длины кватерниона
			/// </summary>
			public Double SqrLength
			{
				get { return ((X * X) + (Y * Y) + (Z * Z) + (W * W)); }
			}

			/// <summary>
			/// Длина кватерниона
			/// </summary>
			public Double Length
			{
				get { return Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W)); }
			}

			/// <summary>
			/// Нормализированный кватернион
			/// </summary>
			public Quaternion3D Normalized
			{
				get
				{
					var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
					return new Quaternion3D(X * inv_lentgh, Y * inv_lentgh, Z * inv_lentgh, W * inv_lentgh);
				}
			}

			/// <summary>
			/// Сопряженный кватернион
			/// </summary>
			public Quaternion3D Conjugated
			{
				get
				{
					return new Quaternion3D(-X, -Y, -Z, W);
				}
			}

			/// <summary>
			/// Инверсный кватернион
			/// </summary>
			public Quaternion3D Inversed
			{
				get
				{
					var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
					return new Quaternion3D(X * inv_lentgh, Y * inv_lentgh, Z * inv_lentgh, W * inv_lentgh * -1.0);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует кватернион указанными параметрами
			/// </summary>
			/// <param name="x">X - координата</param>
			/// <param name="y">Y - координата</param>
			/// <param name="z">Z - координата</param>
			/// <param name="w">W - координата</param>
			//---------------------------------------------------------------------------------------------------------
			public Quaternion3D(Double x, Double y, Double z, Double w = 1.0)
			{
				X = x;
				Y = y;
				Z = z;
				W = w;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует кватернион указанным кватернионом
			/// </summary>
			/// <param name="source">Кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public Quaternion3D(Quaternion3D source)
			{
				X = source.X;
				Y = source.Y;
				Z = source.Z;
				W = source.W;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует кватернион посредством вектора поворота и угла поворота
			/// </summary>
			/// <param name="axis">Ось поворота</param>
			/// <param name="angle">Угол поворота (в градусах)</param>
			//---------------------------------------------------------------------------------------------------------
			public Quaternion3D(Vector3D axis, Double angle)
			{
				Vector3D v = axis.Normalized;

				var half_angle = angle * 0.5;
				var sin_a = Math.Sin(half_angle * XMath.DegreeToRadian_d);

				X = v.X * sin_a;
				Y = v.Y * sin_a;
				Z = v.Z * sin_a;
				W = Math.Cos(half_angle * XMath.DegreeToRadian_d);
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(Object obj)
			{
				if (obj != null)
				{
					if (typeof(Quaternion3D) == obj.GetType())
					{
						var quaternion = (Quaternion3D)obj;
						return Equals(quaternion);
					}
				}

				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства кватернионов по значению
			/// </summary>
			/// <param name="other">Сравниваемый кватернион</param>
			/// <returns>Статус равенства кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(Quaternion3D other)
			{
				return this == other;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение кватернионов для упорядочивания
			/// </summary>
			/// <param name="other">Кватернион</param>
			/// <returns>Статус сравнения кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(Quaternion3D other)
			{
				if (X > other.X)
				{
					return 1;
				}
				else
				{
					if (X == other.X && Y > other.Y)
					{
						return 1;
					}
					else
					{
						if (X == other.X && Y == other.Y && Z > other.Z)
						{
							return -1;
						}
						else
						{
							return 0;
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода кватерниона
			/// </summary>
			/// <returns>Хеш-код кватерниона</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование кватерниона
			/// </summary>
			/// <returns>Копия кватерниона</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление кватерниона с указанием значений координат</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return "X = " + X.ToString("F3") + "; Y = " + Y.ToString("F3") + "; Z = "
				       + Z.ToString("F3") + "; W = " + W.ToString("F3");
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение кватернионов
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Сумма кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D operator +(Quaternion3D left, Quaternion3D right)
			{
				return new Quaternion3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание кватернионов
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Разность кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D operator -(Quaternion3D left, Quaternion3D right)
			{
				return new Quaternion3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение кватерниона на скаляр
			/// </summary>
			/// <param name="quaternion">Кватернион</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D operator *(Quaternion3D quaternion, Double scalar)
			{
				return new Quaternion3D(quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar, quaternion.W * scalar);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Деление кватерниона на скаляр
			/// </summary>
			/// <param name="quaternion">Кватернион</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D operator /(Quaternion3D quaternion, Double scalar)
			{
				scalar = 1 / scalar;
				return new Quaternion3D(quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar, quaternion.W * scalar);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение кватерниона на кватернион
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D operator *(Quaternion3D left, Quaternion3D right)
			{
				return new Quaternion3D((left.W * right.X) + (left.X * right.W) + (left.Y * right.Z) - (left.Z * right.Y),
					(left.W * right.Y) + (left.Y * right.W) + (left.Z * right.X) - (left.X * right.Z),
					(left.W * right.Z) + (left.Z * right.W) + (left.X * right.Y) - (left.Y * right.X),
					(left.W * right.W) - (left.X * right.X) - (left.Y * right.Y) - (left.Z * right.Z));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение кватернионов на равенство
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Статус равенства кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(Quaternion3D left, Quaternion3D right)
			{
				return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение кватернионов на неравенство
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Статус неравенства кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(Quaternion3D left, Quaternion3D right)
			{
				return left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратный кватернион
			/// </summary>
			/// <param name="quaternion">Исходный кватернион</param>
			/// <returns>Обратный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3D operator -(Quaternion3D quaternion)
			{
				return new Quaternion3D(-quaternion.X, -quaternion.Y, -quaternion.Z, -quaternion.W);
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация компонентов кватерниона на основе индекса
			/// </summary>
			/// <param name="index">Индекс компонента</param>
			/// <returns>Компонента кватерниона</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double this[Int32 index]
			{
				get
				{
					switch (index)
					{
						case 0:
							return X;
						case 1:
							return Y;
						case 2:
							return Z;
						default:
							return W;
					}
				}
				set
				{
					switch (index)
					{
						case 0:
							X = value;
							break;
						case 1:
							Y = value;
							break;
						case 2:
							Z = value;
							break;
						default:
							W = value;
							break;
					}
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нормализация кватерниона
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Normalize()
			{
				var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
				X *= inv_lentgh;
				Y *= inv_lentgh;
				Z *= inv_lentgh;
				W *= inv_lentgh;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сопряжение кватерниона
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Conjugate()
			{
				X *= -1.0;
				Y *= -1.0;
				Z *= -1.0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нормализация кватерниона
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Inverse()
			{
				var inv_length = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
				X *= inv_length;
				Y *= inv_length;
				Z *= inv_length;
				W *= -1.0f * inv_length;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка параметров кватерниона
			/// </summary>
			/// <param name="x">X - координата</param>
			/// <param name="y">Y - координата</param>
			/// <param name="z">Z - координата</param>
			/// <param name="w">W - координата</param>
			//---------------------------------------------------------------------------------------------------------
			public void Set(Double x, Double y, Double z, Double w = 1.0)
			{
				X = x;
				Y = y;
				Z = z;
				W = w;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона посредством вектора оси и угла поворота
			/// </summary>
			/// <param name="axis">Ось поворота</param>
			/// <param name="angle">Угол поворота (в градусах)</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromAxisAngle(in Vector3D axis, Double angle)
			{
				Vector3D v = axis.Normalized;

				var half_angle = angle * 0.5;
				var sin_a = Math.Sin(half_angle * XMath.DegreeToRadian_d);

				X = v.X * sin_a;
				Y = v.Y * sin_a;
				Z = v.Z * sin_a;
				W = Math.Cos(half_angle * XMath.DegreeToRadian_d);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота от одного направления до другого по кратчайшей дуге
			/// </summary>
			/// <param name="from_direction">Начальное направление</param>
			/// <param name="to_direction">Требуемое направление</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromToRotation(in Vector3D from_direction, in Vector3D to_direction)
			{
				// Получаем ось вращения
				Vector3D axis = from_direction ^ to_direction;

				Set(axis.X, axis.Y, axis.Z, from_direction * to_direction);
				Normalize();

				// reducing angle to halfangle
				W += 1.0;

				// angle close to PI
				if (W <= XMath.Eplsilon_d)
				{
					if (from_direction.Z * from_direction.Z > from_direction.X * from_direction.X)
					{
						// from * vector3(1,0,0) 
						Set(0, from_direction.Z, -from_direction.Y, W);
					}
					else
					{
						//from * vector3(0,0,1) 
						Set(from_direction.Y, -from_direction.X, 0, W);
					}
				}

				// Нормализация
				Normalize();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота по направлению взгляда
			/// </summary>
			/// <param name="direction">Вектор взгляда</param>
			/// <param name="up">Вектор "вверх"</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLookRotation(in Vector3D direction, in Vector3D up)
			{
				// Step 1. Setup basis vectors describing the rotation given the
				// input vector and assuming an initial up direction of (0, 1, 0)
				// The perpendicular vector to Up and Direction
				var right = Vector3D.Cross(in up, in direction);

				// The actual up vector given the direction and the right vector
				var compUp = Vector3D.Cross(in direction, in right);


				// Step 2. Put the three vectors into the matrix to bulid a basis rotation matrix
				// This step isnt necessary, but im adding it because often you would want to convert from matricies to quaternions instead of vectors to quaternions
				// If you want to skip this step, you can use the vector values directly in the quaternion setup below
				var basis = new Matrix4Dx4(right.X, right.Y, right.Z, 0.0,
												  compUp.X, compUp.Y, compUp.Z, 0.0,
											      direction.X, direction.Y, direction.Z, 0.0,
											      0.0, 0.0, 0.0, 1.0);

				// Преобразуем в кватернион
				this = basis.ToQuaternion3D();

				Normalize();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Трансформация вектора
			/// </summary>
			/// <param name="vector">Вектор</param>
			/// <returns>Трансформированный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3D TransformVector(in Vector3D vector)
			{
				// Быстрая трансформация
				Quaternion3D r = this * new Quaternion3D(vector.X, vector.Y, vector.Z, 0) * Conjugated;
				return new Vector3D(r.X, r.Y, r.Z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сериализация кватерниона в строку
			/// </summary>
			/// <returns>Строка данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public String SerializeToString()
			{
				return String.Format("{0};{1};{2};{3}", X, Y, Z, W);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Кватернион
		/// </summary>
		/// <remarks>
		/// Реализация кватерниона для эффективного представления вращения объектов в трехмерном пространстве
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct Quaternion3Df : IEquatable<Quaternion3Df>, IComparable<Quaternion3Df>, ICloneable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Единичный кватернион 
			/// </summary>
			public static readonly Quaternion3Df Identity = new Quaternion3Df(0, 0, 0);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ  =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона посредством вектора оси и угла поворота
			/// </summary>
			/// <param name="axis">Ось поворота</param>
			/// <param name="angle">Угол поворота (в градусах)</param>
			/// <param name="result">Результирующий кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public static void AxisAngle(in Vector3Df axis, Single angle, out Quaternion3Df result)
			{
				Vector3Df v = axis.Normalized;

				var half_angle = angle * 0.5f;
				var sin_a = (Single)Math.Sin(half_angle * XMath.DegreeToRadian_d);

				result.X = v.X * sin_a;
				result.Y = v.Y * sin_a;
				result.Z = v.Z * sin_a;
				result.W = (Single)Math.Cos(half_angle * XMath.DegreeToRadian_d);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота от одного направления до другого по кратчайшей дуге
			/// </summary>
			/// <param name="from_direction">Начальное направление</param>
			/// <param name="to_direction">Требуемое направление</param>
			/// <param name="result">Результирующий кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetFromToRotation(in Vector3Df from_direction, in Vector3Df to_direction, out Quaternion3Df result)
			{
				// Получаем ось вращения
				Vector3Df axis = from_direction ^ to_direction;

				result.X = axis.X;
				result.Y = axis.Y;
				result.Z = axis.Z;
				result.W = from_direction * to_direction;
				result.Normalize();

				// reducing angle to halfangle
				result.W += 1.0f;

				// angle close to PI
				if (result.W <= XMath.Eplsilon_d)
				{
					if (from_direction.Z * from_direction.Z > from_direction.X * from_direction.X)
					{
						// from * vector3(1,0,0) 
						result.Set(0, from_direction.Z, -from_direction.Y, result.W);
					}
					else
					{
						//from * vector3(0,0,1) 
						result.Set(from_direction.Y, -from_direction.X, 0, result.W);
					}
				}

				// Нормализация
				result.Normalize();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота по направлению взгляда
			/// </summary>
			/// <param name="direction">Вектор направления</param>
			/// <param name="up">Вектор "вверх"</param>
			/// <param name="result">Результирующий кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetLookRotation(in Vector3Df direction, in Vector3Df up, out Quaternion3Df result)
			{
				// Step 1. Setup basis vectors describing the rotation given the
				// input vector and assuming an initial up direction of (0, 1, 0)
				// The perpendicular vector to Up and Direction
				var right = Vector3Df.Cross(in up, in direction);

				// The actual up vector given the direction and the right vector
				var compUp = Vector3Df.Cross(in direction, in right);


				// Step 2. Put the three vectors into the matrix to build a basis rotation matrix
				// This step isnt necessary, but im adding it because often you would want to convert from matricies to quaternions instead of vectors to quaternions
				// If you want to skip this step, you can use the vector values directly in the quaternion setup below
				var basis = new Matrix4Dx4(right.X, right.Y, right.Z, 0.0,
												  compUp.X, compUp.Y, compUp.Z, 0.0,
												  direction.X, direction.Y, direction.Z, 0.0,
												  0.0, 0.0, 0.0, 1.0);

				// Преобразуем в кватернион.
				result = basis.ToQuaternion3Df();

				result.Normalize();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a quaternion given a rotation matrix
			/// </summary>
			/// <param name="matrix">The rotation matrix</param>
			/// <param name="result">When the method completes, contains the newly created quaternion</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationMatrix(in Matrix3Dx3f matrix, out Quaternion3Df result)
			{
				Single sqrt;
				Single half;
				var scale = matrix.M11 + matrix.M22 + matrix.M33;

				if (scale > 0.0f)
				{
					sqrt = (Single)Math.Sqrt(scale + 1.0f);
					result.W = sqrt * 0.5f;
					sqrt = 0.5f / sqrt;

					result.X = (matrix.M23 - matrix.M32) * sqrt;
					result.Y = (matrix.M31 - matrix.M13) * sqrt;
					result.Z = (matrix.M12 - matrix.M21) * sqrt;
				}
				else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
				{
					sqrt = (Single)Math.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
					half = 0.5f / sqrt;

					result.X = 0.5f * sqrt;
					result.Y = (matrix.M12 + matrix.M21) * half;
					result.Z = (matrix.M13 + matrix.M31) * half;
					result.W = (matrix.M23 - matrix.M32) * half;
				}
				else if (matrix.M22 > matrix.M33)
				{
					sqrt = (Single)Math.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
					half = 0.5f / sqrt;

					result.X = (matrix.M21 + matrix.M12) * half;
					result.Y = 0.5f * sqrt;
					result.Z = (matrix.M32 + matrix.M23) * half;
					result.W = (matrix.M31 - matrix.M13) * half;
				}
				else
				{
					sqrt = (Single)Math.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
					half = 0.5f / sqrt;

					result.X = (matrix.M31 + matrix.M13) * half;
					result.Y = (matrix.M32 + matrix.M23) * half;
					result.Z = 0.5f * sqrt;
					result.W = (matrix.M12 - matrix.M21) * half;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a quaternion given a yaw, pitch, and roll value
			/// </summary>
			/// <param name="yaw">The yaw of rotation</param>
			/// <param name="pitch">The pitch of rotation</param>
			/// <param name="roll">The roll of rotation</param>
			/// <param name="result">When the method completes, contains the newly created quaternion</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationYawPitchRoll(Single yaw, Single pitch, Single roll, out Quaternion3Df result)
			{
				var half_roll = roll * 0.5f;
				var half_pitch = pitch * 0.5f;
				var half_yaw = yaw * 0.5f;

				var sin_roll = (Single)Math.Sin(half_roll);
				var cos_roll = (Single)Math.Cos(half_roll);
				var sin_pitch = (Single)Math.Sin(half_pitch);
				var cos_pitch = (Single)Math.Cos(half_pitch);
				var sin_yaw = (Single)Math.Sin(half_yaw);
				var cos_yaw = (Single)Math.Cos(half_yaw);

				result.X = (cos_yaw * sin_pitch * cos_roll) + (sin_yaw * cos_pitch * sin_roll);
				result.Y = (sin_yaw * cos_pitch * cos_roll) - (cos_yaw * sin_pitch * sin_roll);
				result.Z = (cos_yaw * cos_pitch * sin_roll) - (sin_yaw * sin_pitch * cos_roll);
				result.W = (cos_yaw * cos_pitch * cos_roll) + (sin_yaw * sin_pitch * sin_roll);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a quaternion given a yaw, pitch, and roll value
			/// </summary>
			/// <param name="yaw">The yaw of rotation</param>
			/// <param name="pitch">The pitch of rotation</param>
			/// <param name="roll">The roll of rotation</param>
			/// <returns>The newly created quaternion</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df RotationYawPitchRoll(Single yaw, Single pitch, Single roll)
			{
				Quaternion3Df result;
				RotationYawPitchRoll(yaw, pitch, roll, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция кватернионов
			/// </summary>
			/// <param name="from">Начальный кватернион</param>
			/// <param name="to">Конечный кватернион</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df Lerp(in Quaternion3Df from, in Quaternion3Df to, Single time)
			{
				Quaternion3Df quaternion;
				quaternion.X = from.X + ((to.X - from.X) * time);
				quaternion.Y = from.Y + ((to.Y - from.Y) * time);
				quaternion.Z = from.Z + ((to.Z - from.Z) * time);
				quaternion.W = from.W + ((to.W - from.W) * time);
				return quaternion;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Десереализация кватерниона из строки
			/// </summary>
			/// <param name="data">Строка данных</param>
			/// <returns>Кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df DeserializeFromString(String data)
			{
				var quaternion = new Quaternion3Df();
				var quaternion_data = data.Split(';');
				quaternion.X = XMath.ParseSingle(quaternion_data[0]);
				quaternion.Y = XMath.ParseSingle(quaternion_data[1]);
				quaternion.Z = XMath.ParseSingle(quaternion_data[2]);
				quaternion.W = XMath.ParseSingle(quaternion_data[3]);
				return quaternion;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Координата X
			/// </summary>
			public Single X;

			/// <summary>
			/// Координата Y
			/// </summary>
			public Single Y;

			/// <summary>
			/// Координата Z
			/// </summary>
			public Single Z;

			/// <summary>
			/// Координата W
			/// </summary>
			public Single W;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Квадрат длины кватерниона
			/// </summary>
			public Single SqrLength
			{
				get { return (X * X) + (Y * Y) + (Z * Z) + (W * W); }
			}

			/// <summary>
			/// Длина кватерниона
			/// </summary>
			public Single Length
			{
				get { return (Single)Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W)); }
			}

			/// <summary>
			/// Нормализованный кватернион
			/// </summary>
			public Quaternion3Df Normalized
			{
				get
				{
					var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
					return new Quaternion3Df(X * inv_lentgh, Y * inv_lentgh, Z * inv_lentgh, W * inv_lentgh);
				}
			}

			/// <summary>
			/// Сопряженный кватернион
			/// </summary>
			public Quaternion3Df Conjugated
			{
				get
				{
					return new Quaternion3Df(-X, -Y, -Z, W);
				}
			}

			/// <summary>
			/// Инверсный кватернион
			/// </summary>
			public Quaternion3Df Inversed
			{
				get
				{
					var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
					return new Quaternion3Df(X * inv_lentgh, Y * inv_lentgh, Z * inv_lentgh, W * inv_lentgh * -1.0f);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует кватернион указанными параметрами
			/// </summary>
			/// <param name="x">X - координата</param>
			/// <param name="y">Y - координата</param>
			/// <param name="z">Z - координата</param>
			/// <param name="w">W - координата</param>
			//---------------------------------------------------------------------------------------------------------
			public Quaternion3Df(Single x, Single y, Single z, Single w = 1.0f)
			{
				X = x;
				Y = y;
				Z = z;
				W = w;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует кватернион указанным кватернионом
			/// </summary>
			/// <param name="source">Кватернион</param>
			//---------------------------------------------------------------------------------------------------------
			public Quaternion3Df(Quaternion3Df source)
			{
				X = source.X;
				Y = source.Y;
				Z = source.Z;
				W = source.W;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует кватернион посредством вектора поворота и угла поворота
			/// </summary>
			/// <param name="axis">Ось поворота</param>
			/// <param name="angle">Угол поворота (в градусах)</param>
			//---------------------------------------------------------------------------------------------------------
			public Quaternion3Df(Vector3Df axis, Single angle)
			{
				Vector3Df v = axis.Normalized;

				var half_angle = angle * 0.5f;
				var sin_a = (Single)Math.Sin(half_angle * XMath.DegreeToRadian_d);

				X = v.X * sin_a;
				Y = v.Y * sin_a;
				Z = v.Z * sin_a;
				W = (Single)Math.Cos(half_angle * XMath.DegreeToRadian_d);
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(Object obj)
			{
				if (obj != null)
				{
					if (typeof(Quaternion3Df) == obj.GetType())
					{
						var quaternion = (Quaternion3Df)obj;
						return Equals(quaternion);
					}
				}

				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства кватернионов по значению
			/// </summary>
			/// <param name="other">Сравниваемый кватернион</param>
			/// <returns>Статус равенства кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(Quaternion3Df other)
			{
				return this == other;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение кватернионов для упорядочивания
			/// </summary>
			/// <param name="other">Кватернион</param>
			/// <returns>Статус сравнения кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(Quaternion3Df other)
			{
				if (X > other.X)
				{
					return 1;
				}
				else
				{
					if (X == other.X && Y > other.Y)
					{
						return 1;
					}
					else
					{
						if (X == other.X && Y == other.Y && Z > other.Z)
						{
							return -1;
						}
						else
						{
							return 0;
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода кватерниона
			/// </summary>
			/// <returns>Хеш-код кватерниона</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование кватерниона
			/// </summary>
			/// <returns>Копия кватерниона</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление кватерниона с указанием значений координат</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return "X = " + X.ToString("F3") + "; Y = " + Y.ToString("F3") + "; Z = "
				       + Z.ToString("F3") + "; W = " + W.ToString("F3");
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение кватернионов
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Сумма кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df operator +(Quaternion3Df left, Quaternion3Df right)
			{
				return new Quaternion3Df(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание кватернионов
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Разность кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df operator -(Quaternion3Df left, Quaternion3Df right)
			{
				return new Quaternion3Df(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение кватерниона на скаляр
			/// </summary>
			/// <param name="quaternion">Кватернион</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df operator *(Quaternion3Df quaternion, Single scalar)
			{
				return new Quaternion3Df(quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar, quaternion.W * scalar);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Деление кватерниона на скаляр
			/// </summary>
			/// <param name="quaternion">Кватернион</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df operator /(Quaternion3Df quaternion, Single scalar)
			{
				scalar = 1 / scalar;
				return new Quaternion3Df(quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar, quaternion.W * scalar);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение кватерниона на кватернион
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df operator *(Quaternion3Df left, Quaternion3Df right)
			{
				return new Quaternion3Df((left.W * right.X) + (left.X * right.W) + (left.Y * right.Z) - (left.Z * right.Y),
					(left.W * right.Y) + (left.Y * right.W) + (left.Z * right.X) - (left.X * right.Z),
					(left.W * right.Z) + (left.Z * right.W) + (left.X * right.Y) - (left.Y * right.X),
					(left.W * right.W) - (left.X * right.X) - (left.Y * right.Y) - (left.Z * right.Z));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение кватернионов на равенство
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Статус равенства кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(Quaternion3Df left, Quaternion3Df right)
			{
				return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение кватернионов на неравенство
			/// </summary>
			/// <param name="left">Первый кватернион</param>
			/// <param name="right">Второй кватернион</param>
			/// <returns>Статус неравенства кватернионов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(Quaternion3Df left, Quaternion3Df right)
			{
				return left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратный кватернион
			/// </summary>
			/// <param name="quaternion">Исходный кватернион</param>
			/// <returns>Обратный кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Quaternion3Df operator -(Quaternion3Df quaternion)
			{
				return new Quaternion3Df(-quaternion.X, -quaternion.Y, -quaternion.Z, -quaternion.W);
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация компонентов кватерниона на основе индекса
			/// </summary>
			/// <param name="index">Индекс компонента</param>
			/// <returns>Компонента кватерниона</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single this[Int32 index]
			{
				get
				{
					switch (index)
					{
						case 0:
							return X;
						case 1:
							return Y;
						case 2:
							return Z;
						default:
							return W;
					}
				}
				set
				{
					switch (index)
					{
						case 0:
							X = value;
							break;
						case 1:
							Y = value;
							break;
						case 2:
							Z = value;
							break;
						default:
							W = value;
							break;
					}
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нормализация кватерниона
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Normalize()
			{
				var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
				X *= inv_lentgh;
				Y *= inv_lentgh;
				Z *= inv_lentgh;
				W *= inv_lentgh;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сопряжение кватерниона
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Conjugate()
			{
				X *= -1.0f;
				Y *= -1.0f;
				Z *= -1.0f;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Нормализация кватерниона
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Inverse()
			{
				var inv_length = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
				X *= inv_length;
				Y *= inv_length;
				Z *= inv_length;
				W *= -1.0f * inv_length;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка параметров кватерниона
			/// </summary>
			/// <param name="x">X - координата</param>
			/// <param name="y">Y - координата</param>
			/// <param name="z">Z - координата</param>
			/// <param name="w">W - координата</param>
			//---------------------------------------------------------------------------------------------------------
			public void Set(Single x, Single y, Single z, Single w = 1.0f)
			{
				X = x;
				Y = y;
				Z = z;
				W = w;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона посредством вектора оси и угла поворота
			/// </summary>
			/// <param name="axis">Ось поворота</param>
			/// <param name="angle">Угол поворота (в градусах)</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromAxisAngle(in Vector3Df axis, Single angle)
			{
				Vector3Df v = axis.Normalized;

				var half_angle = angle * 0.5f;
				var sin_a = (Single)Math.Sin(half_angle * XMath.DegreeToRadian_d);

				X = v.X * sin_a;
				Y = v.Y * sin_a;
				Z = v.Z * sin_a;
				W = (Single)Math.Cos(half_angle * XMath.DegreeToRadian_d);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота от одного направления до другого по кратчайшей дуге
			/// </summary>
			/// <param name="from_direction">Начальное направление</param>
			/// <param name="to_direction">Требуемое направление</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromToRotation(in Vector3Df from_direction, in Vector3Df to_direction)
			{
				SetFromToRotation(in from_direction, in to_direction, out this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка кватерниона поворота по направлению взгляда
			/// </summary>
			/// <param name="direction">Вектор взгляда</param>
			/// <param name="up">Вектор "вверх"</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLookRotation(in Vector3Df direction, in Vector3Df up)
			{
				SetLookRotation(in direction, in up, out this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Трансформация вектора
			/// </summary>
			/// <param name="vector">Вектор</param>
			/// <returns>Трансформированный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df TransformVector(in Vector3Df vector)
			{
				// Быстрая трансформация
				Quaternion3Df r = this * new Quaternion3Df(vector.X, vector.Y, vector.Z, 0) * Conjugated;
				return new Vector3Df(r.X, r.Y, r.Z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сериализация кватерниона в строку
			/// </summary>
			/// <returns>Строка данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public String SerializeToString()
			{
				return String.Format("{0};{1};{2};{3}", X, Y, Z, W);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================