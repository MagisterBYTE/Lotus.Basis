//=====================================================================================================================
// Проект: Модуль алгоритмов
// Раздел: Алгоритмы поиска пути
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAlgorithmPathFinderAStar.cs
*		Простой волновой поиск пути.
*		Реализация простого волнового алгоритма поиска минимального пути на двухмерной карте.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Algorithm
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup AlgorithmPathFinder
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Точка пути для алгоритма A-Star
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public struct TPathPointStar : IComparable<TPathPointStar>
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Компаратор для точки пути алгоритма A-Star
			/// </summary>
			public static readonly TPathPointStar Comparer = new TPathPointStar();
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Позиция точки пути по X
			/// </summary>
			public Int32 X;

			/// <summary>
			/// Позиция точки пути по Y
			/// </summary>
			public Int32 Y;

			/// <summary>
			/// Длина пути от старта (Параметр G)
			/// </summary>
			public Int32 PathLengthFromStart;

			/// <summary>
			/// Примерное расстояние до цели (Параметр H)
			/// </summary>
			public Int32 HeuristicEstimatePathLength;

			/// <summary>
			/// Ожидаемое полное расстояние до цели (Параметр F)
			/// </summary>
			public Int32 EstimateFullPathLength;

			/// <summary>
			/// Позиция родительской точки пути по X
			/// </summary>
			/// <remarks>
			/// Под родительской точкой понимается точка из котором мы пришли в данную
			/// </remarks>
			public Int32 ParentX;

			/// <summary>
			/// Позиция родительской точки пути по Y
			/// </summary>
			/// <remarks>
			/// Под родительской точкой понимается точка из котором мы пришли в данную
			/// </remarks>
			public Int32 ParentY;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Позиция точки пути
			/// </summary>
			public Vector2Di Location
			{
				get { return new Vector2Di(X, Y); }
				set
				{
					X = value.X;
					Y = value.Y;
				}
			}

			/// <summary>
			/// Позиция родительской точки пути
			/// </summary>
			/// <remarks>
			/// Под родительской точкой понимается точка из котором мы пришли в данную
			/// </remarks>
			public Vector2Di ParentLocation
			{
				get { return new Vector2Di(ParentX, ParentY); }
				set
				{
					ParentX = value.X;
					ParentY = value.Y;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="location">Позиция точки пути</param>
			//---------------------------------------------------------------------------------------------------------
			public TPathPointStar(Vector2Di location)
			{
				X = location.X;
				Y = location.Y;
				PathLengthFromStart = 0;
				HeuristicEstimatePathLength = 0;
				EstimateFullPathLength = 0;
				ParentX = 0;
				ParentY = 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="x">Позиция точки пути по X</param>
			/// <param name="y">Позиция точки пути по Y</param>
			//---------------------------------------------------------------------------------------------------------
			public TPathPointStar(Int32 x, Int32 y)
			{
				X = x;
				Y = y;
				PathLengthFromStart = 0;
				HeuristicEstimatePathLength = 0;
				EstimateFullPathLength = 0;
				ParentX = 0;
				ParentY = 0;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение точек пути по полному расстоянию до цели
			/// </summary>
			/// <param name="other">Точка пути</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(TPathPointStar other)
			{
				if (EstimateFullPathLength > other.EstimateFullPathLength)
				{
					return 1;
				}
				else
				{
					if (EstimateFullPathLength < other.EstimateFullPathLength)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление с указанием значений координат</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return "X = " + X.ToString() + "; Y = " + Y.ToString();
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип эвристической функции для оценки расстояния и стоимости маршрута
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum THeuristicFormula
		{
			/// <summary>
			/// 
			/// </summary>
			Manhattan = 1,

			/// <summary>
			/// 
			/// </summary>
			MaxDXDY = 2,

			/// <summary>
			/// 
			/// </summary>
			DiagonalShortCut = 3,

			/// <summary>
			/// 
			/// </summary>
			Euclidean = 4,

			/// <summary>
			/// 
			/// </summary>
			EuclideanNoSQR = 5,

			/// <summary>
			/// 
			/// </summary>
			Custom1 = 6
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Поиск пути по алгоритму A-Star
		/// </summary>
		/// <see href="https://www.codeproject.com/Articles/15307/A-algorithm-implementation-in-C"/>
		//-------------------------------------------------------------------------------------------------------------
		public class CPathFinderAStar : CPathFinder
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal PriorityQueue<TPathPointStar> mOpenList;
			protected internal List<TPathPointStar> mCloseList;
			protected internal Int32[,] mDirections;
			protected internal Int32 mHoriz = 0;
			protected internal THeuristicFormula mFormula = THeuristicFormula.Manhattan;
			protected internal Int32 mHeuristicEstimate = 2;
			protected internal Boolean mPunishChangeDirection = false;
			protected internal Boolean mReopenCloseNodes = false;
			protected internal Boolean mTieBreaker = false;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Тип эвристической функции для оценки расстояния и стоимости маршрута
			/// </summary>
			public THeuristicFormula Formula
			{
				get { return mFormula; }
				set { mFormula = value; }
			}

			/// <summary>
			/// Константа для оценки расстояния и стоимости маршрута
			/// </summary>
			/// <remarks>
			/// Это константа, которая повлияет на расчетное расстояние от текущей позиции до места назначения цели. 
			/// Эвристическая функция используется для создания оценки, как долго он будет принимать для достижения
			/// цели. Чем лучше оценки, тем короче нашли путь
			/// </remarks>
			public Int32 HeuristicEstimate
			{
				get { return mHeuristicEstimate; }
				set { mHeuristicEstimate = value; }
			}

			/// <summary>
			/// Повышенная стоимость смены направлений
			/// </summary>
			/// <remarks>
			/// Смысл заключается в том что когда алгоритм меняет направление он будет иметь небольшую стоимость.
			/// Конечный результат заключается в том, что если путь будет найден, он будет сравнительно ровной, не
			/// слишком много меняет направление, так выглядит более естественно. Недостатком является то, что это займет
			/// больше времени, потому что необходимо исследование дополнительных узлов
			/// </remarks>
			public Boolean PunishChangeDirection
			{
				get { return mPunishChangeDirection; }
				set { mPunishChangeDirection = value; }
			}

			/// <summary>
			/// Повторное открытие закрытых узлов
			/// </summary>
			/// <remarks>
			/// Истинного значение, разрешает алгоритму повторно анализировать узлы, которые уже были закрыты, 
			/// когда стоимость меньше, чем предыдущее значение. Если повторно просматривать узлы разрешено то путь
			/// будет лучше и ровнее путь, но это займет больше времени
			/// </remarks>
			public Boolean ReopenCloseNodes
			{
				get { return mReopenCloseNodes; }
				set { mReopenCloseNodes = value; }
			}

			/// <summary>
			/// Использовать дополнительно время
			/// </summary>
			/// <remarks>
			/// Иногда, когда алгоритм находит путь, он найдет много возможного выбора для той же стоимости и места назначения.
			/// Урегулирование дополнительного времени говорит алгоритму, что, когда у этого есть разнообразный выбор исследовать,
			/// вместо этого это должно продолжить идти. Когда это идет, изменяющиеся затраты могут использоваться во второй
			/// формуле, чтобы определить "лучшее предположение", чтобы следовать. Обычно, эта формула постепенно увеличивает
			/// эвристику от текущей позиции до цели, умноженной на постоянный множитель.
			/// </remarks>
			/// <example>
			/// Heuristic = Heuristic + Abs(CurrentX * GoalY - GoalX * CurrentY) * 0.001 
			/// </example>
			public Boolean TieBreaker
			{
				get { return mTieBreaker; }
				set { mTieBreaker = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CPathFinderAStar()
				: base()
			{
				mOpenList = new PriorityQueue<TPathPointStar>();
				mCloseList = new List<TPathPointStar>();
				mHeuristicEstimate = 2;
				mSearchLimit = 10000;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="map">Карта</param>
			//---------------------------------------------------------------------------------------------------------
			public CPathFinderAStar(ILotusMap2D map)
				: base(map)
			{
				mOpenList = new PriorityQueue<TPathPointStar>();
				mCloseList = new List<TPathPointStar>();
				mHeuristicEstimate = 2;
				mSearchLimit = 10000;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сброс данных о прохождении пути
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void ResetWave()
			{
				mOpenList.Clear();
				mCloseList.Clear();

				mIsFoundPath = false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распространение волны по карте
			/// </summary>
			/// <returns>Статус нахождение пути</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean ExpansionWave()
			{
				mIsFoundPath = false;

				mOpenList.Clear();
				mCloseList.Clear();

				if (mIsAllowDiagonal)
				{
					mDirections = new Int32[8, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 }, { 1, -1 }, { 1, 1 }, { -1, 1 }, { -1, -1 } };
				}
				else
				{
					mDirections = new Int32[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
				}

				TPathPointStar parent_node;
				parent_node.PathLengthFromStart = 0;
				parent_node.HeuristicEstimatePathLength = mHeuristicEstimate;
				parent_node.EstimateFullPathLength = parent_node.PathLengthFromStart + parent_node.HeuristicEstimatePathLength;
				parent_node.X = mStart.X;
				parent_node.Y = mStart.Y;
				parent_node.ParentX = parent_node.X;
				parent_node.ParentY = parent_node.Y;

				mOpenList.Push(parent_node);

				while (mOpenList.Count > 0)
				{
					parent_node = mOpenList.Pop();

					// Если нашли
					if (parent_node.X == mTarget.X && parent_node.Y == mTarget.Y)
					{
						mCloseList.Add(parent_node);
						mIsFoundPath = true;
						break;
					}

					if (mCloseList.Count > mSearchLimit)
					{
						return mIsFoundPath;
					}

					if (mPunishChangeDirection)
					{
						mHoriz = parent_node.X - parent_node.ParentX;
					}

					// Проводим вычисления для каждого смежного элемента
					for (Int32 i = 0; i < (mIsAllowDiagonal ? 8 : 4); i++)
					{
						TPathPointStar new_node;
						new_node.X = parent_node.X + mDirections[i, 0];
						new_node.Y = parent_node.Y + mDirections[i, 1];

						if (new_node.X < 0 || new_node.Y < 0 || new_node.X >= mMap.MapWidth || new_node.Y >= mMap.MapHeight)
						{
							continue;
						}

						Int32 new_length;
						if (mHeavyDiagonals && i > 3)
						{
							new_length = parent_node.PathLengthFromStart + (Int32)(mMap.Map[new_node.X, new_node.Y] * 2.41f);
						}
						else
						{
							new_length = parent_node.PathLengthFromStart + mMap.Map[new_node.X, new_node.Y];
						}


						if (new_length == parent_node.PathLengthFromStart)
						{
							//Unbrekeable
							continue;
						}

						if (mPunishChangeDirection)
						{
							if (new_node.X - parent_node.X != 0)
							{
								if (mHoriz == 0)
								{
									new_length += 20;
								}
							}
							if (new_node.Y - parent_node.Y != 0)
							{
								if (mHoriz != 0)
								{
									new_length += 20;
								}
							}
						}

						Int32 found_in_open_index = -1;
						for (Int32 j = 0; j < mOpenList.Count; j++)
						{
							if (mOpenList[j].X == new_node.X && mOpenList[j].Y == new_node.Y)
							{
								found_in_open_index = j;
								break;
							}
						}
						if (found_in_open_index != -1 && mOpenList[found_in_open_index].PathLengthFromStart <= new_length)
						{
							continue;
						}

						Int32 found_in_close_index = -1;
						for (Int32 j = 0; j < mCloseList.Count; j++)
						{
							if (mCloseList[j].X == new_node.X && mCloseList[j].Y == new_node.Y)
							{
								found_in_close_index = j;
								break;
							}
						}
						if (found_in_close_index != -1 && (mReopenCloseNodes || mCloseList[found_in_close_index].PathLengthFromStart <= new_length))
						{
							continue;
						}

						new_node.ParentX = parent_node.X;
						new_node.ParentY = parent_node.Y;
						new_node.PathLengthFromStart = new_length;

						switch (mFormula)
						{
							default:
							case THeuristicFormula.Manhattan:
								{
									new_node.HeuristicEstimatePathLength = mHeuristicEstimate * (Math.Abs(new_node.X - mTarget.X) + Math.Abs(new_node.Y - mTarget.Y));
								}
								break;
							case THeuristicFormula.MaxDXDY:
								{
									new_node.HeuristicEstimatePathLength = mHeuristicEstimate * Math.Max(Math.Abs(new_node.X - mTarget.X), Math.Abs(new_node.Y - mTarget.Y));
								}
								break;
							case THeuristicFormula.DiagonalShortCut:
								{
									Int32 h_diagonal = Math.Min(Math.Abs(new_node.X - mTarget.X), Math.Abs(new_node.Y - mTarget.Y));
									Int32 h_straight = Math.Abs(new_node.X - mTarget.X) + Math.Abs(new_node.Y - mTarget.Y);
									new_node.HeuristicEstimatePathLength = mHeuristicEstimate * 2 * h_diagonal + mHeuristicEstimate * (h_straight - 2 * h_diagonal);
								}
								break;
							case THeuristicFormula.Euclidean:
								{
									new_node.HeuristicEstimatePathLength = (Int32)(mHeuristicEstimate * Math.Sqrt(Math.Pow(new_node.X - mTarget.X, 2) + Math.Pow(new_node.Y - mTarget.Y, 2)));
								}
								break;
							case THeuristicFormula.EuclideanNoSQR:
								{
									new_node.HeuristicEstimatePathLength = (Int32)(mHeuristicEstimate * (Math.Pow(new_node.X - mTarget.X, 2) + Math.Pow(new_node.Y - mTarget.Y, 2)));
								}
								break;
							case THeuristicFormula.Custom1:
								TMapPoint dxy = new TMapPoint(Math.Abs(mTarget.X - new_node.X), Math.Abs(mTarget.Y - new_node.Y));
								Int32 Orthogonal = Math.Abs(dxy.X - dxy.Y);
								Int32 Diagonal = Math.Abs((dxy.X + dxy.Y - Orthogonal) / 2);
								new_node.HeuristicEstimatePathLength = mHeuristicEstimate * (Diagonal + Orthogonal + dxy.X + dxy.Y);
								break;
						}

						if (mTieBreaker)
						{
							Int32 dx1 = parent_node.X - mTarget.X;
							Int32 dy1 = parent_node.Y - mTarget.Y;
							Int32 dx2 = mStart.X - mTarget.X;
							Int32 dy2 = mStart.Y - mTarget.Y;
							Int32 cross = Math.Abs(dx1 * dy2 - dx2 * dy1);
							new_node.HeuristicEstimatePathLength = (Int32)(new_node.HeuristicEstimatePathLength + cross * 0.001);
						}

						new_node.EstimateFullPathLength = new_node.PathLengthFromStart + new_node.HeuristicEstimatePathLength;

						mOpenList.Push(new_node);
					}

					mCloseList.Add(parent_node);
				}

				return mIsFoundPath;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение пути
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void BuildPath()
			{
				mPath.Clear();
				TPathPointStar node = mCloseList[mCloseList.Count - 1];
				for (Int32 i = mCloseList.Count - 1; i >= 0; i--)
				{
					if (node.ParentX == mCloseList[i].X && node.ParentY == mCloseList[i].Y || i == mCloseList.Count - 1)
					{
						node = mCloseList[i];
						mPath.AddPathPoint(node.X, node.Y, node.PathLengthFromStart);
					}
					else
					{
						mCloseList.RemoveAt(i);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подготовка данных к запуску алгоритма. Применяется когда надо отобразить распространение алгоритма по шагам
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void PreparationsWaveOnStep()
			{
				mIsFoundPath = false;

				mOpenList.Clear();
				mCloseList.Clear();

				if (mIsAllowDiagonal)
				{
					mDirections = new Int32[8, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 }, { 1, -1 }, { 1, 1 }, { -1, 1 }, { -1, -1 } };
				}
				else
				{
					mDirections = new Int32[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
				}

				TPathPointStar parent_node;
				parent_node.PathLengthFromStart = 0;
				parent_node.HeuristicEstimatePathLength = mHeuristicEstimate;
				parent_node.EstimateFullPathLength = parent_node.PathLengthFromStart + parent_node.HeuristicEstimatePathLength;
				parent_node.X = mStart.X;
				parent_node.Y = mStart.Y;
				parent_node.ParentX = parent_node.X;
				parent_node.ParentY = parent_node.Y;

				mOpenList.Push(parent_node);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распространение волны по карте по шагово
			/// </summary>
			/// <remarks>
			/// Метод должен быть вызван в цикле до достижения окончания поиска
			/// </remarks>
			/// <returns>True, если решение еще не найдено и False если решение найдено или превышен лимит</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean ExpansionWaveOnStep()
			{
				TPathPointStar parent_node = mOpenList.Pop();

				// Если нашли
				if (parent_node.X == mTarget.X && parent_node.Y == mTarget.Y)
				{
					mCloseList.Add(parent_node);
					return false;
				}

				if (mCloseList.Count > mSearchLimit)
				{
					return false;
				}

				if (mPunishChangeDirection)
				{
					mHoriz = parent_node.X - parent_node.ParentX;
				}

				// Проводим вычисления для каждого смежного элемента
				for (Int32 i = 0; i < (mIsAllowDiagonal ? 8 : 4); i++)
				{
					TPathPointStar new_node;
					new_node.X = parent_node.X + mDirections[i, 0];
					new_node.Y = parent_node.Y + mDirections[i, 1];

					if (new_node.X < 0 || new_node.Y < 0 || new_node.X >= mMap.MapWidth || new_node.Y >= mMap.MapHeight)
					{
						continue;
					}

					Int32 new_length;
					if (mHeavyDiagonals && i > 3)
					{
						new_length = parent_node.PathLengthFromStart + (Int32)(mMap.Map[new_node.X, new_node.Y] * 2.41f);
					}
					else
					{
						new_length = parent_node.PathLengthFromStart + mMap.Map[new_node.X, new_node.Y];
					}


					if (new_length == parent_node.PathLengthFromStart)
					{
						//Unbrekeable
						continue;
					}

					if (mPunishChangeDirection)
					{
						if (new_node.X - parent_node.X != 0)
						{
							if (mHoriz == 0)
							{
								new_length += 20;
							}
						}
						if (new_node.Y - parent_node.Y != 0)
						{
							if (mHoriz != 0)
							{
								new_length += 20;
							}
						}
					}

					Int32 found_in_open_index = -1;
					for (Int32 j = 0; j < mOpenList.Count; j++)
					{
						if (mOpenList[j].X == new_node.X && mOpenList[j].Y == new_node.Y)
						{
							found_in_open_index = j;
							break;
						}
					}
					if (found_in_open_index != -1 && mOpenList[found_in_open_index].PathLengthFromStart <= new_length)
					{
						continue;
					}

					Int32 found_in_close_index = -1;
					for (Int32 j = 0; j < mCloseList.Count; j++)
					{
						if (mCloseList[j].X == new_node.X && mCloseList[j].Y == new_node.Y)
						{
							found_in_close_index = j;
							break;
						}
					}
					if (found_in_close_index != -1 && (mReopenCloseNodes || mCloseList[found_in_close_index].PathLengthFromStart <= new_length))
					{
						continue;
					}

					new_node.ParentX = parent_node.X;
					new_node.ParentY = parent_node.Y;
					new_node.PathLengthFromStart = new_length;

					switch (mFormula)
					{
						default:
						case THeuristicFormula.Manhattan:
							{
								new_node.HeuristicEstimatePathLength = mHeuristicEstimate * (Math.Abs(new_node.X - mTarget.X) + Math.Abs(new_node.Y - mTarget.Y));
							}
							break;
						case THeuristicFormula.MaxDXDY:
							{
								new_node.HeuristicEstimatePathLength = mHeuristicEstimate * Math.Max(Math.Abs(new_node.X - mTarget.X), Math.Abs(new_node.Y - mTarget.Y));
							}
							break;
						case THeuristicFormula.DiagonalShortCut:
							{
								Int32 h_diagonal = Math.Min(Math.Abs(new_node.X - mTarget.X), Math.Abs(new_node.Y - mTarget.Y));
								Int32 h_straight = Math.Abs(new_node.X - mTarget.X) + Math.Abs(new_node.Y - mTarget.Y);
								new_node.HeuristicEstimatePathLength = mHeuristicEstimate * 2 * h_diagonal + mHeuristicEstimate * (h_straight - 2 * h_diagonal);
							}
							break;
						case THeuristicFormula.Euclidean:
							{
								new_node.HeuristicEstimatePathLength = (Int32)(mHeuristicEstimate * Math.Sqrt(Math.Pow(new_node.X - mTarget.X, 2) + Math.Pow(new_node.Y - mTarget.Y, 2)));
							}
							break;
						case THeuristicFormula.EuclideanNoSQR:
							{
								new_node.HeuristicEstimatePathLength = (Int32)(mHeuristicEstimate * (Math.Pow(new_node.X - mTarget.X, 2) + Math.Pow(new_node.Y - mTarget.Y, 2)));
							}
							break;
						case THeuristicFormula.Custom1:
							TMapPoint dxy = new TMapPoint(Math.Abs(mTarget.X - new_node.X), Math.Abs(mTarget.Y - new_node.Y));
							Int32 Orthogonal = Math.Abs(dxy.X - dxy.Y);
							Int32 Diagonal = Math.Abs((dxy.X + dxy.Y - Orthogonal) / 2);
							new_node.HeuristicEstimatePathLength = mHeuristicEstimate * (Diagonal + Orthogonal + dxy.X + dxy.Y);
							break;
					}

					if (mTieBreaker)
					{
						Int32 dx1 = parent_node.X - mTarget.X;
						Int32 dy1 = parent_node.Y - mTarget.Y;
						Int32 dx2 = mStart.X - mTarget.X;
						Int32 dy2 = mStart.Y - mTarget.Y;
						Int32 cross = Math.Abs(dx1 * dy2 - dx2 * dy1);
						new_node.HeuristicEstimatePathLength = (Int32)(new_node.HeuristicEstimatePathLength + cross * 0.001);
					}

					new_node.EstimateFullPathLength = new_node.PathLengthFromStart + new_node.HeuristicEstimatePathLength;

					mOpenList.Push(new_node);
				}

				mCloseList.Add(parent_node);

				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Заполнение данных распространения волны действия алгоритма
			/// </summary>
			/// <remarks>
			/// Метод в основном служебный, предназначен для демонстрации действия алгоритма
			/// </remarks>
			/// <param name="wave">Карта для отображения волны действия алгоритма</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetWave(Int32[,] wave)
			{
				for (Int32 i = 0; i < mCloseList.Count; i++)
				{
					wave[mCloseList[i].X, mCloseList[i].Y] = mCloseList[i].PathLengthFromStart;
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================