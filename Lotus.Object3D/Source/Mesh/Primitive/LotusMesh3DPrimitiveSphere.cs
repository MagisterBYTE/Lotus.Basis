﻿//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема мешей
// Подраздел: Объемные трехмерные примитивы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMesh3DPrimitiveSphere.cs
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
		/** \addtogroup Object3DMeshVolume
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Объемный трёхмерный примитив - цилиндр
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMeshPrimitiveSphere3Df : CMesh3Df
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal Vector3Df mPivot;
			internal Single mRadius;
			internal Int32 mNumberVerticalSegment = 18;
			internal Int32 mNumberHorizontalSegment = 18;
			internal Boolean mIsHalf;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Радиус сферы
			/// </summary>
			public Single Radius
			{
				get
				{
					return mRadius;
				}

				set
				{
					mRadius = value;
					CreateSphere();
				}
			}

			/// <summary>
			/// Количество вертикальных сегментов
			/// </summary>
			public Int32 NumberVerticalSegment
			{
				get
				{
					return mNumberVerticalSegment;
				}

				set
				{
					mNumberVerticalSegment = value;
					CreateSphere();
				}
			}

			/// <summary>
			/// Количество горизонтальных сегментов
			/// </summary>
			public Int32 NumberHorizontalSegment
			{
				get
				{
					return mNumberHorizontalSegment;
				}

				set
				{
					mNumberHorizontalSegment = value;
					CreateSphere();
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMeshPrimitiveSphere3Df()
				:base()
			{
				mName = "Sphere3D";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="pivot">Опорная точка сферы(центр сферы)</param>
			/// <param name="radius">Радиус сферы</param>
			/// <param name="startAngle">Начальный угол(в градусах) генерации сферы</param>
			/// <param name="numberVerticalSegment">Количество вертикальных сегментов</param>
			/// <param name="numberHorizontalSegment">Количество горизонтальных сегментов</param>
			//---------------------------------------------------------------------------------------------------------
			public CMeshPrimitiveSphere3Df(Vector3Df pivot, Single radius, Single startAngle, Int32 numberVerticalSegment,
				 Int32 numberHorizontalSegment): base()
			{
				mName = "Sphere3D";
				Create(pivot, radius, startAngle, numberVerticalSegment, numberHorizontalSegment);
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание сферы основе внутренних данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected void CreateSphere()
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание сферы
			/// </summary>
			/// <param name="pivot">Опорная точка сферы(центр сферы)</param>
			/// <param name="radius">Радиус сферы</param>
			/// <param name="startAngle">Начальный угол(в градусах) генерации сферы</param>
			/// <param name="numberVerticalSegment">Количество вертикальных сегментов</param>
			/// <param name="numberHorizontalSegment">Количество горизонтальных сегментов</param>
			//---------------------------------------------------------------------------------------------------------
			public void Create(Vector3Df pivot, Single radius, Single startAngle, Int32 numberVerticalSegment,
				 Int32 numberHorizontalSegment)
			{
				// Сохраняем данные
				mNumberVerticalSegment = numberVerticalSegment;
				mNumberHorizontalSegment = numberHorizontalSegment;

				// Количество строк - есть количество горизонтальных сегментов
				var row_count = numberHorizontalSegment;

				// Количество столбов - есть количество вертикальных сегментов
				var column_count = numberVerticalSegment;

				mVertices.Clear();

				// Дельта углов для плоскостей
				var horizont_delta = 360.0f / column_count;
				var vertical_delta = 180.0f / row_count;

				for (var ir = 0; ir < row_count + 1; ir++)
				{
					for (var ic = 0; ic < column_count; ic++)
					{
						// Широта в градусах - угол в вертикальной плоскости в пределах [0, 180]
						var latitude_degree = ir * vertical_delta;

						// Долгота в градусах - угол в горизонтальной плоскости между в пределах [0, 359]
						var longitude_degree = ic * horizont_delta;

						var positon = Vector3Df.FromSpherical(radius, latitude_degree, longitude_degree);
						Vector2Df uv = XGeometry3D.GetMapUVFromSpherical(latitude_degree, longitude_degree);

						mVertices.AddVertex(positon + pivot, positon.Normalized, uv);
					}
				}

				// Заполняем треугольники
				mTriangles.Clear();
				mTriangles.AddTriangleRegularGrid(0, column_count - 1, row_count, true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление нормалей для сферы
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
			/// Вычисление текстурных координат (развертки) для сферы
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeUVMap(Int32 channel = 0)
			{
				var index = 0;
				for (var ir = 0; ir <= mNumberHorizontalSegment; ir++)
				{
					for (var ic = 0; ic <= mNumberVerticalSegment; ic++)
					{
						var u = ic / (Single)mNumberVerticalSegment;
						var v = ir / (Single)mNumberHorizontalSegment;

						mVertices.Vertices[index].UV = new Vector2Df(v, u);
						index++;
					}
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