//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема мешей
// Подраздел: Плоскостные трехмерные примитивы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMesh3DPlanarQuad.cs
*		Плоскостной трехмерный примитив - четырехугольник.
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
		/// Плоскостной трехмерный примитив - четырехугольник
		/// </summary>
		/// <remarks>
		/// <para>
		/// Топология вершин:
		/// 2)------- 3)
		/// |       / |
		/// |     /   |
		/// |   /     |
		/// | /       |
		/// 0)--------1)
		/// </para>
		/// <para>
		/// Первый треугольник: 0, 2, 3
		/// Второй треугольник: 0, 3, 1
		/// </para>
		/// <para>
		/// Опорная точка четырёхугольника – его геометрический центр
		/// </para>
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMeshPlanarQuad3Df : CMeshPlanar3Df
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника по четырем вершинам
			/// </summary>
			/// <param name="p1">Первая вершина четырехугольника</param>
			/// <param name="p2">Вторая вершина четырехугольника</param>
			/// <param name="p3">Третья вершина четырехугольника</param>
			/// <param name="p4">Четверта вершина четырехугольника</param>
			/// <returns>Четырехугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMeshPlanarQuad3Df CreateOfPoint(Vector3Df p1, Vector3Df p2, Vector3Df p3, Vector3Df p4)
			{
				var mesh = new CMeshPlanarQuad3Df(p1, p2, p3, p4);
				return mesh;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника в плоскости XZ (Top - вид сверху)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Z
			/// </remarks>
			/// <param name="pivot">Опорная точка четырехугольника</param>
			/// <param name="width">Ширина четырехугольника</param>
			/// <param name="height">Высота четырехугольника</param>
			/// <returns>Четырехугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMeshPlanarQuad3Df CreateXZ(Vector3Df pivot, Single width, Single height)
			{
				var mesh = new CMeshPlanarQuad3Df();
				mesh.CreateQuadXZ(pivot, width, height);
				return mesh;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника в плоскости ZY (Right - вид справа)
			/// </summary>
			/// <remarks>
			/// Ширина по оси Z
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка четырехугольника</param>
			/// <param name="width">Ширина четырехугольника</param>
			/// <param name="height">Высота четырехугольника</param>
			/// <returns>Четырехугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMeshPlanarQuad3Df CreateZY(Vector3Df pivot, Single width, Single height)
			{
				var mesh = new CMeshPlanarQuad3Df();
				mesh.CreateQuadZY(pivot, width, height);
				return mesh;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника в плоскости XY (Back - вид с сзади)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка четырехугольника</param>
			/// <param name="width">Ширина четырехугольника</param>
			/// <param name="height">Высота четырехугольника</param>
			/// <returns>Четырехугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMeshPlanarQuad3Df CreateXY(Vector3Df pivot, Single width, Single height)
			{
				var mesh = new CMeshPlanarQuad3Df();
				mesh.CreateQuadXY(pivot, width, height);
				return mesh;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Ширина четырехугольника
			/// </summary>
			public Single Width
			{
				get
				{
					return Vector3Df.Distance(in mVertices.Vertices[1].Position, in mVertices.Vertices[0].Position);
				}

				set
				{
					// Получаем направление
					Vector3Df dir = (mVertices.Vertices[1].Position - mVertices.Vertices[0].Position).Normalized;

					// Смещаем 1 вершину от 0
					mVertices.Vertices[1].Position = mVertices.Vertices[0].Position + dir * value;

					// Смещаем 3 вершину от 2
					mVertices.Vertices[3].Position = mVertices.Vertices[2].Position + dir * value;

					UpdateData();

					//Centering();
				}
			}

			/// <summary>
			/// Высота четырехугольника
			/// </summary>
			public Single Height
			{
				get
				{
					return Vector3Df.Distance(in mVertices.Vertices[2].Position, in mVertices.Vertices[0].Position);
				}

				set
				{

					// Получаем направление
					Vector3Df dir = (mVertices.Vertices[2].Position - mVertices.Vertices[0].Position).Normalized;

					// Смещаем 2 вершину от 0
					mVertices.Vertices[2].Position = mVertices.Vertices[0].Position + dir * value;

					// Смещаем 3 вершину от 1
					mVertices.Vertices[3].Position = mVertices.Vertices[1].Position + dir * value;

					UpdateData();
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMeshPlanarQuad3Df()
				:base()
			{
				mName = "Quad3D";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="p1">Первая вершина четырехугольника</param>
			/// <param name="p2">Вторая вершина четырехугольника</param>
			/// <param name="p3">Третья вершина четырехугольника</param>
			/// <param name="p4">Четверта вершина четырехугольника</param>
			//---------------------------------------------------------------------------------------------------------
			public CMeshPlanarQuad3Df(Vector3Df p1, Vector3Df p2, Vector3Df p3, Vector3Df p4)
				: base()
			{
				mName = "Quad3D";
				CreateQuadOfPoint(p1, p2, p3, p4);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника по четырем вершинам
			/// </summary>
			/// <param name="p1">Первая вершина четырехугольника</param>
			/// <param name="p2">Вторая вершина четырехугольника</param>
			/// <param name="p3">Третья вершина четырехугольника</param>
			/// <param name="p4">Четверта вершина четырехугольника</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateQuadOfPoint(Vector3Df p1, Vector3Df p2, Vector3Df p3, Vector3Df p4)
			{
				mVertices.Clear();
				mVertices.AddVertex(p1);
				mVertices.AddVertex(p2);
				mVertices.AddVertex(p3);
				mVertices.AddVertex(p4);

				mTriangles.Clear();
				mTriangles.AddTriangleQuad();

				this.ComputeNormals();
				this.ComputeUVMap();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника в плоскости XZ (Top - вид сверху)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Z
			/// </remarks>
			/// <param name="pivot">Опорная точка четырехугольника</param>
			/// <param name="width">Ширина четырехугольника</param>
			/// <param name="height">Высота четырехугольника</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateQuadXZ(Vector3Df pivot, Single width, Single height)
			{
				mPlaneType = Maths.TDimensionPlane.XZ;

				mVertices.Clear();
				mVertices.AddVertex(pivot + new Vector3Df(-width / 2, 0, -height / 2));
				mVertices.AddVertex(pivot + new Vector3Df(width / 2, 0, -height / 2));
				mVertices.AddVertex(pivot + new Vector3Df(-width / 2, 0, height / 2));
				mVertices.AddVertex(pivot + new Vector3Df(width / 2, 0, height / 2));

				mTriangles.Clear();
				mTriangles.AddTriangleQuad();

				this.ComputeNormals();
				this.ComputeUVMap();

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника в плоскости ZY (Right - вид справа)
			/// </summary>
			/// <remarks>
			/// Ширина по оси Z
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка четырехугольника</param>
			/// <param name="width">Ширина четырехугольника</param>
			/// <param name="height">Высота четырехугольника</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateQuadZY(Vector3Df pivot, Single width, Single height)
			{
				mPlaneType = Maths.TDimensionPlane.ZY;

				mVertices.Clear();
				mVertices.AddVertex(pivot + new Vector3Df(0, -height / 2, -width / 2));
				mVertices.AddVertex(pivot + new Vector3Df(0, -height / 2, width / 2));
				mVertices.AddVertex(pivot + new Vector3Df(0, height / 2,-width / 2));
				mVertices.AddVertex(pivot + new Vector3Df(0, height / 2, width / 2));

				mTriangles.Clear();
				mTriangles.AddTriangleQuad();

				this.ComputeNormals();
				this.ComputeUVMap();

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание четырехугольника в плоскости XY (Back - вид с сзади)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка четырехугольника</param>
			/// <param name="width">Ширина четырехугольника</param>
			/// <param name="height">Высота четырехугольника</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateQuadXY(Vector3Df pivot, Single width, Single height)
			{
				mPlaneType = Maths.TDimensionPlane.XY;

				mVertices.Clear();
				mVertices.AddVertex(pivot + new Vector3Df(-width / 2, -height / 2, 0));
				mVertices.AddVertex(pivot + new Vector3Df(width / 2, -height / 2, 0));
				mVertices.AddVertex(pivot + new Vector3Df(-width / 2, height / 2, 0));
				mVertices.AddVertex(pivot + new Vector3Df(width / 2, height / 2, 0));

				mTriangles.Clear();
				mTriangles.AddTriangleQuad();

				this.ComputeNormals();
				this.ComputeUVMap();

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление нормалей для четырехугольника
			/// </summary>
			/// <remarks>
			/// Нормаль вычисления путем векторного произведения по часовой стрелки
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeNormals()
			{
				Vector3Df down = mVertices.Vertices[2].Position - mVertices.Vertices[0].Position;
				Vector3Df right = mVertices.Vertices[1].Position - mVertices.Vertices[0].Position;

				Vector3Df normal = Vector3Df.Cross(in down, in right).Normalized;

				mVertices.Vertices[0].Normal = normal;
				mVertices.Vertices[1].Normal = normal;
				mVertices.Vertices[2].Normal = normal;
				mVertices.Vertices[3].Normal = normal;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление текстурных координат (развертки) для четырехугольника
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeUVMap(Int32 channel = 0)
			{
				mVertices.Vertices[0].UV = XGeometry2D.MapUVBottomLeft;
				mVertices.Vertices[1].UV = XGeometry2D.MapUVBottomRight;
				mVertices.Vertices[2].UV = XGeometry2D.MapUVTopLeft;
				mVertices.Vertices[3].UV = XGeometry2D.MapUVTopRight;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================