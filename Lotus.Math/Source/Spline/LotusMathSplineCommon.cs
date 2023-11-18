//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема для работы со сплайнами
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathSplineCommon.cs
*		Алгоритмы представления и визуализации различных сплайнов.
*		Реализация алгоритмов которые представляют различные виды сплайнов, и позволяют производить базовые действия со
*	сплайнами, их аппроксимацию для отображения, перемещение по сплайну.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup MathSpline Подсистема для работы со сплайнами
         * \ingroup Math
         * \brief Подсистема представления и визуализации различных сплайнов.
		 * \details Реализация алгоритмов которые представляют различные виды сплайнов и позволяют производить базовые 
			действия со сплайнами, их аппроксимацию для отображения, перемещение по сплайну.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура для хранения сегмента пути
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public struct TMoveSegment
		{
			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Длина сегмента
			/// </summary>
			public Single Length;

			/// <summary>
			/// Совокупная длина с учетом данного сегмента
			/// </summary>
			public Single Summa;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="length">Длина сегмента</param>
			/// <param name="summa">Совокупная длина с учетом данного сегмента</param>
			//---------------------------------------------------------------------------------------------------------
			public TMoveSegment(Single length, Single summa)
			{
				Length = length;
				Summa = summa;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для представления сплайна в двухмерном пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusSpline2D
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Количество контрольных точек сплайна
			/// </summary>
			Int32 CountPoints { get; }

			/// <summary>
			/// Длина сплайна
			/// </summary>
			Single Length { get; }

			/// <summary>
			/// Контрольные точки сплайна
			/// </summary>
			IList<Vector2Df> ControlPoints { get; }

			/// <summary>
			/// Список точек сплайна для рисования
			/// </summary>
			IList<Vector2Df> DrawingPoints { get; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			Vector2Df CalculatePoint(Single time);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Растеризация сплайна - вычисление точек отрезков для рисования сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void ComputeDrawingPoints();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление длины сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void ComputeLengthSpline();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление выпуклого четырехугольника, заданного опорными точками внутри которого находится сплайн
			/// </summary>
			/// <returns>Прямоугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			Rect2Df GetArea();
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для представления сплайна в трехмерном пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusSpline3D
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Количество контрольных точек сплайна
			/// </summary>
			Int32 CountPoints { get; }

			/// <summary>
			/// Длина сплайна
			/// </summary>
			Single Length { get; }

			/// <summary>
			/// Контрольные точки сплайна
			/// </summary>
			IList<Vector3Df> ControlPoints { get; }

			/// <summary>
			/// Список точек сплайна для рисования
			/// </summary>
			IList<Vector3Df> DrawingPoints { get; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			Vector3Df CalculatePoint(Single time);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Растеризация сплайна - вычисление точек отрезков для рисования сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void ComputeDrawingPoints();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление длины сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void ComputeLengthSpline();
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс для представления сплайна в двухмерном пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public abstract class CSplineBase2D : ILotusSpline2D
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Квадрат минимальной длины отрезка для отображения сплайна
			/// </summary>
			/// <remarks>
			/// Если в результате растеризации получится отрезок меньшей длины(квадрата длины) он
			/// не будет принимается в расчет и отображаться
			/// </remarks>
			public static Int32 MinimalSqrLine = 2;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal Vector2Df[] _controlPoints;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 _segmentsSpline = 10;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal List<Vector2Df> _drawingPoints;
			[NonSerialized]
			protected internal Single _length = 0;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal List<TMoveSegment> _segmentsPath;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Начальная точка
			/// </summary>
			public Vector2Df StartPoint
			{
				get { return _controlPoints[0]; }
				set { _controlPoints[0] = value; }
			}

			/// <summary>
			/// Конечная точка
			/// </summary>
			public Vector2Df EndPoint
			{
				get { return _controlPoints[_controlPoints.Length - 1]; }
				set { _controlPoints[_controlPoints.Length - 1] = value; }
			}

			/// <summary>
			/// Количество сегментов сплайна
			/// </summary>
			/// <remarks>
			/// Данный параметр используется при растеризации сплайна. Чем больше значение тем выше качество отображения сплайна
			/// </remarks>
			public Int32 SegmentsSpline
			{
				get { return _segmentsSpline; }
				set
				{
					if (_segmentsSpline != value)
					{
						_segmentsSpline = value;
						OnUpdateSpline();
					}
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSpline2D ===================================
			/// <summary>
			/// Количество контрольных точек сплайна
			/// </summary>
			public Int32 CountPoints
			{
				get { return _controlPoints.Length; }
			}

			/// <summary>
			/// Длина сплайна
			/// </summary>
			public Single Length
			{
				get { return _length; }
			}

			/// <summary>
			/// Контрольные точки сплайна
			/// </summary>
			public IList<Vector2Df> ControlPoints
			{
				get { return _controlPoints; }
			}

			/// <summary>
			/// Список точек сплайна для рисования
			/// </summary>
			public IList<Vector2Df> DrawingPoints
			{
				get { return _drawingPoints; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			/// <param name="count">Количество контрольных точек</param>
			//---------------------------------------------------------------------------------------------------------
			public CSplineBase2D(Int32 count)
			{
				_controlPoints = new Vector2Df[count];
				_drawingPoints = new List<Vector2Df>();
				_segmentsPath = new List<TMoveSegment>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="startPoint">Начальная точка</param>
			/// <param name="endPoint">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public CSplineBase2D(Vector2Df startPoint, Vector2Df endPoint)
			{
				_controlPoints = new Vector2Df[3];
				_controlPoints[0] = startPoint;
				_controlPoints[1] = (startPoint + endPoint) / 2;
				_controlPoints[2] = endPoint;
				_drawingPoints = new List<Vector2Df>();
				_segmentsPath = new List<TMoveSegment>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="controlPoints">Набор контрольных точек сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public CSplineBase2D(params Vector2Df[] controlPoints)
			{
				_controlPoints = new Vector2Df[controlPoints.Length];
				Array.Copy(controlPoints, _controlPoints, controlPoints.Length);
				_drawingPoints = new List<Vector2Df>();
				_segmentsPath = new List<TMoveSegment>();
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSpline2D =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <remarks>
			/// Это основной метод для реализации
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public abstract Vector2Df CalculatePoint(Single time);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Растеризация сплайна - вычисление точек отрезков для рисования сплайна
			/// </summary>
			/// <remarks>
			/// Качество(степень) растеризации зависит от свойства <see cref="SegmentsSpline"/>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ComputeDrawingPoints()
			{
				_drawingPoints.Clear();
				Vector2Df prev = _controlPoints[0];
				_drawingPoints.Add(prev);
				for (var i = 1; i < _segmentsSpline; i++)
				{
					var time = (Single)i / _segmentsSpline;
					Vector2Df point = CalculatePoint(time);
					
					// Добавляем если длина больше
					if ((point - prev).SqrLength > MinimalSqrLine)
					{
						_drawingPoints.Add(point);
						prev = point;
					}
				}

				_drawingPoints.Add(EndPoint);

				// Проверяем еще раз
				if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
				{
					_drawingPoints.RemoveAt(_drawingPoints.Count - 1);
					_drawingPoints[_drawingPoints.Count - 1] = EndPoint;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление длины сплайна
			/// </summary>
			/// <remarks>
			/// Реализация по умолчанию вычисляет длину сплайна по сумме отрезков растеризации
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ComputeLengthSpline()
			{
				_length = 0;
				_segmentsPath.Clear();
				for (var i = 1; i < _drawingPoints.Count; i++)
				{
					var length = (_drawingPoints[i] - _drawingPoints[i - 1]).Length;
					_length += length;
					var segment = new TMoveSegment(length, _length);
					_segmentsPath.Add(segment);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление выпуклого четырехугольника, заданного опорными точками внутри которого находится сплайн
			/// </summary>
			/// <remarks>
			/// Реализация по умолчанию вычисляет прямоугольник на основе контрольных точек
			/// </remarks>
			/// <returns>Прямоугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Rect2Df GetArea()
			{
				Single x_min = StartPoint.X, x_max = EndPoint.X, y_min = StartPoint.Y, y_max = EndPoint.Y;

				for (var i = 0; i < _drawingPoints.Count; i++)
				{
					Vector2Df point = _drawingPoints[i];
					if (point.X < x_min) x_min = point.X;
					if (point.X > x_max) x_max = point.X;
					if (point.Y < y_min) y_min = point.Y;
					if (point.Y > y_max) y_max = point.Y;
				}

				var rect_area = new Rect2Df();
				rect_area.X = x_min;
				rect_area.Width = x_max - x_min;
				rect_area.Y = y_min;
				rect_area.Height = y_max - y_min;

				return rect_area;
			}
			#endregion

			#region ======================================= МЕТОДЫ IMove ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение позиции на сплайне
			/// </summary>
			/// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
			/// <returns>Позиция на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector2Df GetMovePosition(Single position)
			{
				// Ограничения
				if (position >= Length)
				{
					return EndPoint;
				}
				if (position <= 0.0f)
				{
					return StartPoint;
				}

				// Находим в какой отрезок попадает эта позиция
				var index = 0;
				for (var i = 0; i < _segmentsPath.Count; i++)
				{
					// Если позиция меньше значит она попала в отрезок
					if (position < _segmentsPath[i].Summa)
					{
						index = i;
						break;
					}
				}

				// Перемещение относительно данного сегмента
				Single delta = 0;

				if (index == 0)
				{
					delta = position / _segmentsPath[index].Length;
				}
				else
				{
					delta = (position - _segmentsPath[index - 1].Summa) / _segmentsPath[index].Length;
				}

				// Интерполируем позицию
				return Vector2Df.Lerp(_drawingPoints[index], _drawingPoints[index + 1], delta);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение скорости на сплайне
			/// </summary>
			/// <remarks>
			/// Это коэффициент изменения скорости на сплайне применяется для регулировки скорости
			/// </remarks>
			/// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
			/// <returns>Скорость на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single GetMoveVelocity(Single position)
			{
				return 1.0f;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение направления на сплайне
			/// </summary>
			/// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
			/// <returns>Направление на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector2Df GetMoveDirection(Single position)
			{
				return GetMovePosition(position + 0.01f);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение положения на сплайне
			/// </summary>
			/// <param name="position">Позиция на сплайне</param>
			/// <param name="epsilon">Погрешность при сравнении</param>
			/// <returns>Положение точки от 0 до 1</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single GetMoveTime(Vector2Df position, Single epsilon = 0.01f)
			{
				// Просматриваем все отрезки и находим нужное положение
				for (var i = 1; i < _drawingPoints.Count; i++)
				{
					Vector2Df p1 = _drawingPoints[i] - _drawingPoints[i - 1];
					Vector2Df p2 = position - _drawingPoints[i - 1];

					var angle = Vector2Df.Dot(p2, p1) / Vector2Df.Dot(p2, p1);
					if (angle + epsilon > 0 && angle - epsilon < 1)
					{
						// TODO: Найти точное положение на всей кривой
						return angle;
					}
				}

				return -1;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка параметров сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnResetSpline()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация параметров сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnInitSpline()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление параметров сплайна
			/// </summary>
			/// <remarks>
			/// Реализация по умолчанию обновляет список точек для рисования и длину сплайна
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnUpdateSpline()
			{
				ComputeDrawingPoints();
				ComputeLengthSpline();
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рисование сплайна с помощью GL
			/// </summary>
			/// <param name="material">Материал</param>
			/// <param name="color">Цвет сплайна</param>
			/// <param name="alternative">Альтернативный цвет сплайна на нечетных сегментах</param>
			/// <param name="is_alternative">Статус использования альтернативного цвета</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OnDrawSplineGL(Material material, Color color, Color alternative, Boolean is_alternative = true)
			{
				GL.PushMatrix();
				GL.LoadPixelMatrix();
				material.SetPass(0);
				if (is_alternative == false)
				{
					GL.Color(color);
				}
				GL.Begin(GL.LINES);
				{
					for (var i = 1; i < _drawingPoints.Count; i++)
					{
						var p1 = new Vector2Df(_drawingPoints[i - 1].X, Screen.height - _drawingPoints[i - 1].Y);
						var p2 = new Vector2Df(_drawingPoints[i].X, Screen.height - _drawingPoints[i].Y);

						if (is_alternative)
						{
							if (i % 2 == 0)
							{
								GL.Color(color);
								GL.Vertex(p1.ToVector3XY());
								GL.Vertex(p2.ToVector3XY());
							}
							else
							{
								GL.Color(alternative);
								GL.Vertex(p1.ToVector3XY());
								GL.Vertex(p2.ToVector3XY());
							}
						}
					}
				}
				GL.End();
				GL.PopMatrix();
			}
#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и корректировка расположения первой точки на сплайне.
			/// Применяется когда надо скорректировать отображения сплайна при его замкнутости
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void CheckCorrectStartPoint()
			{
				_drawingPoints[_drawingPoints.Count - 1] = StartPoint;
				if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
				{
					_drawingPoints.RemoveAt(_drawingPoints.Count - 1);
					_drawingPoints[_drawingPoints.Count - 1] = StartPoint;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и корректировка расположения последней точки на сплайне
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void CheckCorrectEndPoint()
			{
				_drawingPoints[_drawingPoints.Count - 1] = EndPoint;
				if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
				{
					_drawingPoints.RemoveAt(_drawingPoints.Count - 1);
					_drawingPoints[_drawingPoints.Count - 1] = EndPoint;
				}
			}
			#endregion

			#region ======================================= РАБОТА С КОНТРОЛЬНЫМИ ТОЧКАМИ =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение контрольной точки сплайна по индексу в локальных координатах
			/// </summary>
			/// <param name="index">Позиция(индекс) точки</param>
			/// <returns>Контрольная точка сплайна в локальных координатах</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Vector2Df GetControlPoint(Int32 index)
			{
				return _controlPoints[index];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка контрольной точки сплайна по индексу в локальных координатах
			/// </summary>
			/// <param name="index">Позиция(индекс) точки</param>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetControlPoint(Int32 index, Vector2Df point, Boolean updateSpline = false)
			{
				_controlPoints[index] = point;
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление контрольной точки к сплайну в локальных координатах
			/// </summary>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddControlPoint(Vector2Df point, Boolean updateSpline = false)
			{
				Array.Resize(ref _controlPoints, _controlPoints.Length + 1);
				_controlPoints[_controlPoints.Length - 1] = point;
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка контрольной точки в сплайн в локальных координатах в указанной позиции
			/// </summary>
			/// <param name="index">Позиция(индекс) вставки точки</param>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void InsertControlPoint(Int32 index, Vector2Df point, Boolean updateSpline = false)
			{
				_controlPoints = XArray.InsertAt(_controlPoints, point, index);
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление контрольной точки в локальных координатах
			/// </summary>
			/// <param name="index">Позиция(индекс) удаляемой точки</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveControlPoint(Int32 index, Boolean updateSpline = false)
			{
				_controlPoints = XArray.RemoveAt(_controlPoints, index);
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление контрольной точки в локальных координатах
			/// </summary>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveControlPoint(Vector2Df point, Boolean updateSpline = false)
			{
				for (var i = 0; i < _controlPoints.Length; i++)
				{
					if (_controlPoints[i].Approximately(point))
					{
						_controlPoints = XArray.RemoveAt(_controlPoints, i);
						if (updateSpline)
						{
							this.OnUpdateSpline();
						}
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление всех контрольных точек
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ClearControlPoints()
			{
				Array.Clear(_controlPoints, 0, _controlPoints.Length);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс для представления сплайна в трехмерном пространстве
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public abstract class CSplineBase3D : ILotusSpline3D
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Квадрат минимальной длины отрезка для отображения сплайна
			/// </summary>
			/// <remarks>
			/// Если в результате растеризации получится отрезок меньшей длины(квадрата длины) он
			/// не будет принимается в расчет и отображаться
			/// </remarks>
			public static Int32 MinimalSqrLine = 2;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal Vector3Df[] _controlPoints;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 _segmentsSpline;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal List<Vector3Df> _drawingPoints;
			[NonSerialized]
			protected internal Single _length = 0;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal List<TMoveSegment> _segmentsPath;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Начальная точка
			/// </summary>
			public Vector3Df StartPoint
			{
				get { return _controlPoints[0]; }
				set { _controlPoints[0] = value; }
			}

			/// <summary>
			/// Конечная точка
			/// </summary>
			public Vector3Df EndPoint
			{
				get { return _controlPoints[_controlPoints.Length - 1]; }
				set { _controlPoints[_controlPoints.Length - 1] = value; }
			}

			/// <summary>
			/// Количество сегментов сплайна
			/// </summary>
			/// <remarks>
			/// Данный параметр используется при растеризации сплайна. Чем больше значение тем выше качество отображения сплайна
			/// </remarks>
			public Int32 SegmentsSpline
			{
				get { return _segmentsSpline; }
				set
				{
					if (_segmentsSpline != value)
					{
						_segmentsSpline = value;
						OnUpdateSpline();
					}
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSpline3D ===================================
			/// <summary>
			/// Количество контрольных точек сплайна
			/// </summary>
			public Int32 CountPoints
			{
				get { return _controlPoints.Length; }
			}

			/// <summary>
			/// Длина сплайна
			/// </summary>
			public Single Length
			{
				get { return _length; }
			}

			/// <summary>
			/// Контрольные точки сплайна
			/// </summary>
			public IList<Vector3Df> ControlPoints
			{
				get { return _controlPoints; }
			}

			/// <summary>
			/// Список точек сплайна для рисования
			/// </summary>
			public IList<Vector3Df> DrawingPoints
			{
				get { return _drawingPoints; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			/// <param name="count">Количество контрольных точек</param>
			//---------------------------------------------------------------------------------------------------------
			public CSplineBase3D(Int32 count)
			{
				_controlPoints = new Vector3Df[count];
				_drawingPoints = new List<Vector3Df>();
				_segmentsPath = new List<TMoveSegment>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="startPoint">Начальная точка</param>
			/// <param name="endPoint">Конечная точка</param>
			//---------------------------------------------------------------------------------------------------------
			public CSplineBase3D(Vector3Df startPoint, Vector3Df endPoint)
			{
				_controlPoints = new Vector3Df[3];
				_controlPoints[0] = startPoint;
				_controlPoints[1] = (startPoint + endPoint) / 2;
				_controlPoints[2] = endPoint;
				_drawingPoints = new List<Vector3Df>();
				_segmentsPath = new List<TMoveSegment>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="controlPoints">Набор контрольных точек сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public CSplineBase3D(params Vector3Df[] controlPoints)
			{
				_controlPoints = new Vector3Df[controlPoints.Length];
				Array.Copy(controlPoints, _controlPoints, controlPoints.Length);
				_drawingPoints = new List<Vector3Df>();
				_segmentsPath = new List<TMoveSegment>();
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSpline3D =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление точки на сплайне
			/// </summary>
			/// <remarks>
			/// Это основной метод для реализации
			/// </remarks>
			/// <param name="time">Положение точки от 0 до 1</param>
			/// <returns>Позиция точки на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public abstract Vector3Df CalculatePoint(Single time);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Растеризация сплайна - вычисление точек отрезков для рисования сплайна
			/// </summary>
			/// <remarks>
			/// Качество(степень) растеризации зависит от свойства <see cref="SegmentsSpline"/>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ComputeDrawingPoints()
			{
				_drawingPoints.Clear();
				Vector3Df prev = _controlPoints[0];
				_drawingPoints.Add(prev);
				for (var i = 1; i < _segmentsSpline; i++)
				{
					var time = (Single)i / _segmentsSpline;
					Vector3Df point = CalculatePoint(time);

					// Добавляем если длина больше
					if ((point - prev).SqrLength > MinimalSqrLine)
					{
						_drawingPoints.Add(point);
						prev = point;
					}
				}

				_drawingPoints.Add(EndPoint);

				// Проверяем еще раз
				if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
				{
					_drawingPoints.RemoveAt(_drawingPoints.Count - 1);
					_drawingPoints[_drawingPoints.Count - 1] = EndPoint;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление длины сплайна
			/// </summary>
			/// <remarks>
			/// Реализация по умолчанию вычисляет длину сплайна по сумме отрезков растеризации
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ComputeLengthSpline()
			{
				_length = 0;
				_segmentsPath.Clear();
				for (var i = 1; i < _drawingPoints.Count; i++)
				{
					var length = (_drawingPoints[i] - _drawingPoints[i - 1]).Length;
					_length += length;
					var segment = new TMoveSegment(length, _length);
					_segmentsPath.Add(segment);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление выпуклого четырехугольника, заданного опорными точками внутри которого находится сплайн
			/// </summary>
			/// <remarks>
			/// Реализация по умолчанию вычисляет прямоугольник на основе контрольных точек
			/// </remarks>
			/// <returns>Прямоугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Rect2Df GetArea()
			{
				Single x_min = StartPoint.X, x_max = EndPoint.X, y_min = StartPoint.Y, y_max = EndPoint.Y;

				for (var i = 0; i < _drawingPoints.Count; i++)
				{
					Vector3Df point = _drawingPoints[i];
					if (point.X < x_min) x_min = point.X;
					if (point.X > x_max) x_max = point.X;
					if (point.Y < y_min) y_min = point.Y;
					if (point.Y > y_max) y_max = point.Y;
				}

				var rect_area = new Rect2Df();
				rect_area.X = x_min;
				rect_area.Width = x_max - x_min;
				rect_area.Y = y_min;
				rect_area.Height = y_max - y_min;

				return rect_area;
			}
			#endregion

			#region ======================================= МЕТОДЫ IMove ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение позиции на сплайне
			/// </summary>
			/// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
			/// <returns>Позиция на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df GetMovePosition(Single position)
			{
				// Ограничения
				if (position >= Length)
				{
					return EndPoint;
				}
				if (position <= 0.0f)
				{
					return StartPoint;
				}

				// Находим в какой отрезок попадает эта позиция
				var index = 0;
				for (var i = 0; i < _segmentsPath.Count; i++)
				{
					// Если позиция меньше значит она попала в отрезок
					if (position < _segmentsPath[i].Summa)
					{
						index = i;
						break;
					}
				}

				// Перемещение относительно данного сегмента
				Single delta = 0;

				if (index == 0)
				{
					delta = position / _segmentsPath[index].Length;
				}
				else
				{
					delta = (position - _segmentsPath[index - 1].Summa) / _segmentsPath[index].Length;
				}

				// Интерполируем позицию
				return Vector3Df.Lerp(_drawingPoints[index], _drawingPoints[index + 1], delta);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение скорости на сплайне
			/// </summary>
			/// <remarks>
			/// Это коэффициент изменения скорости на сплайне применяется для регулировки скорости
			/// </remarks>
			/// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
			/// <returns>Скорость на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single GetMoveVelocity(Single position)
			{
				return 1.0f;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение направления на сплайне
			/// </summary>
			/// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
			/// <returns>Направление на сплайне</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df GetMoveDirection(Single position)
			{
				return GetMovePosition(position + 0.01f);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение положения на сплайне
			/// </summary>
			/// <param name="position">Позиция на сплайне</param>
			/// <param name="epsilon">Погрешность при сравнении</param>
			/// <returns>Положение точки от 0 до 1</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single GetMoveTime(Vector3Df position, Single epsilon = 0.01f)
			{
				// Просматриваем все отрезки и находим нужное положение
				for (var i = 1; i < _drawingPoints.Count; i++)
				{
					Vector3Df p1 = _drawingPoints[i] - _drawingPoints[i - 1];
					Vector3Df p2 = position - _drawingPoints[i - 1];

					var angle = Vector3Df.Dot(p2, p1) / Vector3Df.Dot(p2, p1);
					if (angle + epsilon > 0 && angle - epsilon < 1)
					{
						// TODO: Найти точное положение на всей кривой
						return angle;
					}
				}

				return -1;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка параметров сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnResetSpline()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация параметров сплайна
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnInitSpline()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление параметров сплайна
			/// </summary>
			/// <remarks>
			/// Реализация по умолчанию обновляет список точек для рисования и длину сплайна
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnUpdateSpline()
			{
				ComputeDrawingPoints();
				ComputeLengthSpline();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и корректировка расположения первой точки на сплайне.
			/// Применяется когда надо скорректировать отображения сплайна при его замкнутости
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void CheckCorrectStartPoint()
			{
				_drawingPoints[_drawingPoints.Count - 1] = StartPoint;
				if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
				{
					_drawingPoints.RemoveAt(_drawingPoints.Count - 1);
					_drawingPoints[_drawingPoints.Count - 1] = StartPoint;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и корректировка расположения последней точки на сплайне
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void CheckCorrectEndPoint()
			{
				_drawingPoints[_drawingPoints.Count - 1] = EndPoint;
				if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
				{
					_drawingPoints.RemoveAt(_drawingPoints.Count - 1);
					_drawingPoints[_drawingPoints.Count - 1] = EndPoint;
				}
			}
			#endregion

			#region ======================================= РАБОТА С КОНТРОЛЬНЫМИ ТОЧКАМИ =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение контрольной точки сплайна по индексу в локальных координатах
			/// </summary>
			/// <param name="index">Позиция(индекс) точки</param>
			/// <returns>Контрольная точка сплайна в локальных координатах</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Vector3Df GetControlPoint(Int32 index)
			{
				return _controlPoints[index];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка контрольной точки сплайна по индексу в локальных координатах
			/// </summary>
			/// <param name="index">Позиция(индекс) точки</param>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetControlPoint(Int32 index, Vector3Df point, Boolean updateSpline = false)
			{
				_controlPoints[index] = point;
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление контрольной точки к сплайну в локальных координатах
			/// </summary>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddControlPoint(Vector3Df point, Boolean updateSpline = false)
			{
				Array.Resize(ref _controlPoints, _controlPoints.Length + 1);
				_controlPoints[_controlPoints.Length - 1] = point;
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка контрольной точки в сплайн в локальных координатах в указанной позиции
			/// </summary>
			/// <param name="index">Позиция(индекс) вставки точки</param>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void InsertControlPoint(Int32 index, Vector3Df point, Boolean updateSpline = false)
			{
				_controlPoints = XArray.InsertAt(_controlPoints, point, index);
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление контрольной точки в локальных координатах
			/// </summary>
			/// <param name="index">Позиция(индекс) удаляемой точки</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveControlPoint(Int32 index, Boolean updateSpline = false)
			{
				_controlPoints = XArray.RemoveAt(_controlPoints, index);
				if (updateSpline)
				{
					this.OnUpdateSpline();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление контрольной точки в локальных координатах
			/// </summary>
			/// <param name="point">Контрольная точка сплайна в локальных координатах</param>
			/// <param name="updateSpline">Статус обновления сплайна</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RemoveControlPoint(Vector3Df point, Boolean updateSpline = false)
			{
				for (var i = 0; i < _controlPoints.Length; i++)
				{
					if (_controlPoints[i].Approximately(point))
					{
						_controlPoints = XArray.RemoveAt(_controlPoints, i);
						if (updateSpline)
						{
							this.OnUpdateSpline();
						}
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление всех контрольных точек
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ClearControlPoints()
			{
				Array.Clear(_controlPoints, 0, _controlPoints.Length);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================