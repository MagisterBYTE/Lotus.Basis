//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема мешей
// Подраздел: Плоскостные трехмерные примитивы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMesh3DPlanarEllipse.cs
*		Плоскостной трехмерный примитив - эллипс.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Object3D
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup Object3DMeshPlanar
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Плоскостной трехмерный примитив - эллипс
		/// </summary>
		/// <remarks>
		/// <para>
		/// Топология вершин:
		/// 3)---2)
		/// |   / \
		/// |  /   \
		/// | /     \
		/// |/       \
		/// 0)--------1)
		/// </para>
		/// <para>
		/// Первый треугольник: 0, 2, 1
		/// Второй треугольник: 0, 3, 2
		/// Третий треугольник: 0, 4, 3 и т.д.
		/// </para>
		/// <para>
		/// Опорная точка элиппса – его геометрический центр
		/// </para>
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMeshPlanarEllipse3Df : CMeshPlanar3Df
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal Single mRadiusX;
			internal Single mRadiusY;
			internal Single mStartAngle;
			internal Int32 mNumberSegment = 18;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Радиус эллипса по X
			/// </summary>
			public Single RadiusX
			{
				get { return mRadiusX; }
				set
				{
					mRadiusX = value;
					CreateEllipse();
				}
			}

			/// <summary>
			/// Радиус эллипса по Y
			/// </summary>
			public Single RadiusY
			{
				get { return mRadiusY; }
				set
				{
					mRadiusY = value;
					CreateEllipse();
				}
			}

			/// <summary>
			/// Начальный угол(в градусах) генерации эллипса
			/// </summary>
			public Single StartAngle
			{
				get { return mStartAngle; }
				set
				{
					mStartAngle = value;
					CreateEllipse();
				}
			}

			/// <summary>
			/// Количество сегментов эллипса
			/// </summary>
			public Int32 NumberSegment
			{
				get { return mNumberSegment; }
				set
				{
					mNumberSegment = value;
					CreateEllipse();
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMeshPlanarEllipse3Df()
				:base()
			{
				mName = "Ellipse3D";
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание эллипса на основе внутренних данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void CreateEllipse()
			{
				// Сохраняем опорную точку
				Vector3Df pivot = mVertices[0].Position;
				mVertices.Clear();

				Single segment_angle = 360f / mNumberSegment;
				Single current_angle = mStartAngle;

				Vector3Df normal = GetPerpendicularVector();
				mVertices.AddVertex(pivot, normal, XGeometry2D.MapUV_MiddleCenter);

				for (var i = 0; i < mNumberSegment; i++)
				{
					Single angle_in_radians = current_angle * XMath.DegreeToRadian_f;
					Vector3Df pos = Vector3Df.Zero;
					switch (mPlaneType)
					{
						case Maths.TDimensionPlane.XZ:
							{
								pos.X = mRadiusX * XMath.Cos(angle_in_radians);
								pos.Z = mRadiusY * XMath.Sin(angle_in_radians);
							}
							break;
						case Maths.TDimensionPlane.ZY:
							{
								pos.Z = mRadiusX * XMath.Cos(angle_in_radians);
								pos.Y = mRadiusY * XMath.Sin(angle_in_radians);
							}
							break;
						case Maths.TDimensionPlane.XY:
							{
								pos.X = mRadiusX * XMath.Cos(angle_in_radians);
								pos.Y = mRadiusY * XMath.Sin(angle_in_radians);
							}
							break;
						default:
							break;
					}

					Vector2Df uv = new Vector2Df(0.5f * XMath.Cos(angle_in_radians) + 0.5f, 0.5f * XMath.Sin(angle_in_radians) + 0.5f);

					mVertices.AddVertex(pivot + pos, normal, uv);

					current_angle += segment_angle;
				}

				mTriangles.Clear();
				mTriangles.AddTriangleFan(0, mNumberSegment - 1, true);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание эллипса в плоскости XZ (Top - вид сверху)
			/// </summary>
			/// <param name="pivot">Опорная точка эллипса (его центр)</param>
			/// <param name="radius_x">Радиус эллипса по X</param>
			/// <param name="radius_z">Радиус эллипса по Z</param>
			/// <param name="start_angle">Начальный угол(в градусах) генерации эллипса</param>
			/// <param name="number_segment">Количество сегментов эллипса</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateEllipseXZ(Vector3Df pivot, Single radius_x, Single radius_z, Single start_angle, Int32 number_segment)
			{
				mPlaneType = Maths.TDimensionPlane.XZ;
				mRadiusX = radius_x;
				mRadiusY = radius_z;
				mStartAngle = start_angle;
				mNumberSegment = number_segment;

				mVertices.Clear();
				mVertices.AddVertex(pivot);

				CreateEllipse();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание эллипса в плоскости ZY (Right - вид справа)
			/// </summary>
			/// <param name="pivot">Опорная точка эллипса (его центр)</param>
			/// <param name="radius_z">Радиус эллипса по Z</param>
			/// <param name="radius_y">Радиус эллипса по Y</param>
			/// <param name="start_angle">Начальный угол(в градусах) генерации эллипса</param>
			/// <param name="number_segment">Количество сегментов эллипса</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateEllipseZY(Vector3Df pivot, Single radius_z, Single radius_y, Single start_angle, Int32 number_segment)
			{
				mPlaneType = Maths.TDimensionPlane.ZY;
				mRadiusX = radius_z;
				mRadiusY = radius_y;
				mStartAngle = start_angle;
				mNumberSegment = number_segment;

				mVertices.Clear();
				mVertices.AddVertex(pivot);

				CreateEllipse();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание эллипса в плоскости XY (Back - вид с сзади)
			/// </summary>
			/// <param name="pivot">Опорная точка эллипса (его центр)</param>
			/// <param name="radius_x">Радиус эллипса по X</param>
			/// <param name="radius_y">Радиус эллипса по Y</param>
			/// <param name="start_angle">Начальный угол(в градусах) генерации эллипса</param>
			/// <param name="number_segment">Количество сегментов эллипса</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateEllipseXY(Vector3Df pivot, Single radius_x, Single radius_y, Single start_angle, Int32 number_segment)
			{
				mPlaneType = Maths.TDimensionPlane.XY;
				mRadiusX = radius_x;
				mRadiusY = radius_y;
				mStartAngle = start_angle;
				mNumberSegment = number_segment;

				mVertices.Clear();
				mVertices.AddVertex(pivot);

				CreateEllipse();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление нормалей для эллипса
			/// </summary>
			/// <remarks>
			/// Нормаль вычисления путем векторного произведения по часовой стрелки
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeNormals()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление текстурных координат (развертки) для эллипса
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeUVMap(Int32 channel = 0)
			{

			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================