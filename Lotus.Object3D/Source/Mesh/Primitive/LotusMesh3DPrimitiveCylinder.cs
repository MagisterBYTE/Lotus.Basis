//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема мешей
// Подраздел: Объемные трехмерные примитивы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMesh3DPrimitiveCylinder.cs
*		Объемный трёхмерный примитив - цилиндр.
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
		/**
         * \defgroup Object3DMeshVolume Объемные трехмерные примитивы
         * \ingroup Object3DMesh
         * \brief Объемные трехмерные примитивы - это примитивы которые образуют объемную замкнутую поверхность.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Объемный трёхмерный примитив - цилиндр
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMeshPrimitiveCylinder3Df : CMesh3Df
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal Single mRadius;
			internal Single mHeight;
			internal Int32 mNumberVerticalSegment = 18;
			internal Int32 mNumberHorizontalSegment = 18;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Радиус цилиндра
			/// </summary>
			public Single Radius
			{
				get
				{
					return (mRadius);
				}

				set
				{
					mRadius = value;
					CreateCylinder();
				}
			}

			/// <summary>
			/// Высота цилиндра
			/// </summary>
			public Single Height
			{
				get
				{
					return (mHeight);
				}

				set
				{
					mHeight = value;
					CreateCylinder();
				}
			}

			/// <summary>
			/// Количество вертикальных сегментов
			/// </summary>
			public Int32 NumberVerticalSegment
			{
				get
				{
					return (mNumberVerticalSegment);
				}

				set
				{
					mNumberVerticalSegment = value;
					CreateCylinder();
				}
			}

			/// <summary>
			/// Количество горизонтальных сегментов
			/// </summary>
			public Int32 NumberHorizontalSegment
			{
				get
				{
					return (mNumberHorizontalSegment);
				}

				set
				{
					mNumberHorizontalSegment = value;
					CreateCylinder();
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMeshPrimitiveCylinder3Df()
				:base()
			{
				mName = "Cylinder3D";
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание цилиндра основе внутренних данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void CreateCylinder()
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание цилиндра с основанием в плоскости XZ (Top - вид сверху)
			/// </summary>
			/// <param name="pivot">Опорная точка (центр ниженого основания)</param>
			/// <param name="radius">Радиус цилиндра</param>
			/// <param name="height">Высота цилиндра</param>
			/// <param name="start_angle">Начальный угол(в градусах) генерации сферы</param>
			/// <param name="number_vertical_segment">Количество вертикальных сегментов</param>
			/// <param name="number_horizontal_segment">Количество горизонтальных сегментов</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateCylinderXZ(Vector3Df pivot, Single radius, Single height,
				Single start_angle, Int32 number_vertical_segment, Int32 number_horizontal_segment)
			{
				CMeshPlanarEllipse3Df up_cap = new CMeshPlanarEllipse3Df();
				up_cap.CreateEllipseXZ(pivot + Vector3Df.Up * height,radius, radius, start_angle, number_vertical_segment);

				CMeshPlanarEllipse3Df down_cap = new CMeshPlanarEllipse3Df();
				down_cap.CreateEllipseXZ(pivot, radius, radius, start_angle, number_vertical_segment);
				down_cap.RotateFromX(180, true);

				CMeshPlanarGrid3Df surface = new CMeshPlanarGrid3Df();
				List<Vector3Df> points = XGeometry3D.GeneratePointsOnCircleXZ(radius, number_vertical_segment, start_angle);
				//points.Reverse();

				for (Int32 i = 0; i < points.Count; i++)
				{
					points[i] += pivot;
				}

				surface.CreateFromPointListXZ(points, number_horizontal_segment, height / number_horizontal_segment, true);

				//surface.MinimizeToCylinder(radius);

				Add(up_cap);
				Add(down_cap);
				Add(surface);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление нормалей для цилиндра
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
			/// Вычисление текстурных координат (развертки) для цилиндра
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