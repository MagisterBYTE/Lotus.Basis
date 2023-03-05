//=====================================================================================================================
// Проект: Модуль алгоритмов
// Раздел: Алгоритмы поиска пути
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAlgorithmPathFinderWave.cs
*		Простой волновой поиск пути.
*		Реализация простого волнового алгоритма поиска минимального пути на двухмерной карте.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
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
		/// Волновой поиск пути
		/// </summary>
		/// <remarks>
		/// Реализация простого волнового алгоритма поиска минимального пути на двухмерной карте
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CPathFinderWave : CPathFinder
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Int32[,] mWaveMap;
			protected internal Int32 mStepWave;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Карта отображающая действие алгоритма - распространение волны
			/// </summary>
			/// <remarks>
			/// Значение в ячейки определяет шаг распространения волны
			/// </remarks>
			public Int32[,] WaveMap
			{
				get { return mWaveMap; }
				set { mWaveMap = value; }
			}

			/// <summary>
			/// Количество шагов волны
			/// </summary>
			public Int32 StepWave
			{
				get { return mStepWave; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CPathFinderWave()
				: base()
			{
				mWaveMap = new Int32[1, 1];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="map">Карта</param>
			//---------------------------------------------------------------------------------------------------------
			public CPathFinderWave(ILotusMap2D map)
				: base(map)
			{
				mWaveMap = new Int32[map.MapWidth, map.MapHeight];
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
				for (Int32 y = 0; y < mMap.MapHeight; y++)
				{
					for (Int32 x = 0; x < mMap.MapWidth; x++)
					{
						mWaveMap[x, y] = -1;
					}
				}

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
				Boolean add = true;
				Int32 indicate_wall = -2, indicate_empty = -1;
				mIsFoundPath = false;
				mStepWave = 0;

				// Заполняем карту поиска пути на основе карты препятствий
				for (Int32 y = 0; y < mMap.MapHeight; y++)
				{
					for (Int32 x = 0; x < mMap.MapWidth; x++)
					{
						if (mMap.Map[x, y] == XMapCode.BLOCK)
						{
							mWaveMap[x, y] = indicate_wall; // индикатор стены
						}
						else
						{
							mWaveMap[x, y] = indicate_empty; // индикатор еще не ступали сюда
						}
					}
				}

				// Если стартовая позиция находится на стене
				if (mWaveMap[mStart.X, mStart.Y] == indicate_wall)
				{
					return false;
				}

				// Если финишная позиция находится на стене
				if (mWaveMap[mTarget.X, mTarget.Y] == indicate_wall)
				{
					return false;
				}

				// Начинаем с финиша
				mWaveMap[mTarget.X, mTarget.Y] = 0;

				while (add == true)
				{
					add = false;
					for (Int32 x = 0; x < mMap.MapWidth; x++)
					{
						for (Int32 y = 0; y < mMap.MapHeight; y++)
						{
							// Если ячейка свободная
							if (mWaveMap[x, y] == mStepWave)
							{
								// Ставим значение шага + 1 в соседние ячейки (если они проходимы)
								if (x - 1 >= 0 && mWaveMap[x - 1, y] == indicate_empty)
								{
									mWaveMap[x - 1, y] = mStepWave + 1;
								}

								if (x + 1 < mMap.MapWidth && mWaveMap[x + 1, y] == indicate_empty)
								{
									mWaveMap[x + 1, y] = mStepWave + 1;
								}

								if (y - 1 >= 0 && mWaveMap[x, y - 1] == indicate_empty)
								{
									mWaveMap[x, y - 1] = mStepWave + 1;
								}
								if (y + 1 < mMap.MapHeight && mWaveMap[x, y + 1] == indicate_empty)
								{
									mWaveMap[x, y + 1] = mStepWave + 1;
								}
							}
						}
					}

					mStepWave++;

					add = true;

					// Решение найдено
					if (mWaveMap[mStart.X, mStart.Y] != indicate_empty)
					{
						mIsFoundPath = true;
						add = false;
					}

					// Решение не найдено
					if (mStepWave > mMap.MapWidth * mMap.MapHeight)
					{
						add = false;
					}

					// Если есть лимит и он превышен
					if(mSearchLimit > 0 && mStepWave > mSearchLimit)
					{
						add = false;
					}
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
				if (mIsFoundPath == false) return;
				mPath.Clear();

				Int32 minimum = 100000;
				Boolean found = true;
				Int32 current_value;
				Int32 count = 0;
				Int32 cx = mStart.X;
				Int32 cy = mStart.Y;

				while (found)
				{
					if (mWaveMap[cx, cy] == 0)
					{
						found = false;
						break;
					}

					if (cx - 1 >= 0)
					{
						current_value = mWaveMap[cx - 1, cy];
						if (current_value < minimum && current_value > 0)
						{
							cx = cx - 1;
							minimum = current_value;
							if (!mIsAllowDiagonal) goto ortho;
						}
					}

					if (cx + 1 < mMap.MapWidth)
					{
						current_value = mWaveMap[cx + 1, cy];
						if (current_value < minimum && current_value > 0)
						{
							cx = cx + 1;
							minimum = current_value;
							if (!mIsAllowDiagonal) goto ortho;
						}
					}

					if (cy - 1 >= 0)
					{
						current_value = mWaveMap[cx, cy - 1];
						if (current_value < minimum && current_value > 0)
						{
							cy = cy - 1;
							minimum = current_value;
							if (!mIsAllowDiagonal) goto ortho;
						}
					}

					if (cy + 1 < mMap.MapHeight)
					{
						current_value = mWaveMap[cx, cy + 1];
						if (current_value < minimum && current_value > 0)
						{
							cy = cy + 1;
							minimum = current_value;
							if (!mIsAllowDiagonal) goto ortho;
						}
					}

					ortho:;
					mPath.AddPathPoint(cx, cy, minimum);

					count++;

					if (count > mMap.MapHeight * mMap.MapWidth)
					{
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подготовка данных к запуску волны. Применяется когда надо отобразить распространение волны по шагам
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void PreparationsWaveOnStep()
			{
				Int32 indicate_wall = -2, indicate_empty = -1;

				// Заполняем карту поиска пути на основе карты препятствий
				for (Int32 y = 0; y < mMap.MapHeight; y++)
				{
					for (Int32 x = 0; x < mMap.MapWidth; x++)
					{
						if (mMap.Map[x, y] == XMapCode.BLOCK)
						{
							mWaveMap[x, y] = indicate_wall; // индикатор стены
						}
						else
						{
							mWaveMap[x, y] = indicate_empty; // индикатор еще не ступали сюда
						}
					}
				}

				// Если стартовая позиция находится на стене
				if (mWaveMap[mStart.X, mStart.Y] == indicate_wall)
				{
					return;
				}

				// Если финишная позиция находится на стене
				if (mWaveMap[mTarget.X, mTarget.Y] == indicate_wall)
				{
					return;
				}

				// Начинаем с финиша
				mWaveMap[mTarget.X, mTarget.Y] = 0;
				mIsFoundPath = false;
				mStepWave = 0;
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
				Int32 indicate_empty = -1;

				for (Int32 x = 0; x < mMap.MapWidth; x++)
				{
					for (Int32 y = 0; y < mMap.MapHeight; y++)
					{
						// Если ячейка свободная
						if (mWaveMap[x, y] == mStepWave)
						{
							// Ставим значение шага + 1 в соседние ячейки (если они проходимы)
							if (x - 1 >= 0 && mWaveMap[x - 1, y] == indicate_empty)
							{
								mWaveMap[x - 1, y] = mStepWave + 1;
							}

							if (x + 1 < mMap.MapWidth && mWaveMap[x + 1, y] == indicate_empty)
							{
								mWaveMap[x + 1, y] = mStepWave + 1;
							}

							if (y - 1 >= 0 && mWaveMap[x, y - 1] == indicate_empty)
							{
								mWaveMap[x, y - 1] = mStepWave + 1;
							}
							if (y + 1 < mMap.MapHeight && mWaveMap[x, y + 1] == indicate_empty)
							{
								mWaveMap[x, y + 1] = mStepWave + 1;
							}
						}
					}
				}

				mStepWave++;

				// Решение найдено
				if (mWaveMap[mStart.X, mStart.Y] != indicate_empty)
				{
					mIsFoundPath = true;
					return false;
				}

				// Решение не найдено
				if (mStepWave > mMap.MapWidth * mMap.MapHeight)
				{
					return false;
				}

				// Если есть лимит и он превышен
				if (mSearchLimit > 0 && mStepWave > mSearchLimit)
				{
					return false;
				}

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
				for (Int32 ix = 0; ix < mMap.MapWidth; ix++)
				{
					for (Int32 iy = 0; iy < mMap.MapHeight; iy++)
					{
						wave[ix, iy] = mWaveMap[ix, iy];
					}
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