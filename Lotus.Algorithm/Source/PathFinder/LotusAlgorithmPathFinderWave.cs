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
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Algorithm
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup AlgorithmPathFinder
		*@{*/
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
			protected internal Int32[,] _waveMap;
			protected internal Int32 _stepWave;
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
				get { return _waveMap; }
				set { _waveMap = value; }
			}

			/// <summary>
			/// Количество шагов волны
			/// </summary>
			public Int32 StepWave
			{
				get { return _stepWave; }
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
				_waveMap = new Int32[1, 1];
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
				_waveMap = new Int32[map.MapWidth, map.MapHeight];
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
				if(_map == null) return;

				for (var y = 0; y < _map.MapHeight; y++)
				{
					for (var x = 0; x < _map.MapWidth; x++)
					{
						_waveMap[x, y] = -1;
					}
				}

				_isFoundPath = false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распространение волны по карте
			/// </summary>
			/// <returns>Статус нахождение пути</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean ExpansionWave()
			{
				if (_map == null) return false;

				var add = true;
				Int32 indicate_wall = -2, indicate_empty = -1;
				_isFoundPath = false;
				_stepWave = 0;

				// Заполняем карту поиска пути на основе карты препятствий
				for (var y = 0; y < _map.MapHeight; y++)
				{
					for (var x = 0; x < _map.MapWidth; x++)
					{
						if (_map.Map[x, y] == XMapCode.BLOCK)
						{
							_waveMap[x, y] = indicate_wall; // индикатор стены
						}
						else
						{
							_waveMap[x, y] = indicate_empty; // индикатор еще не ступали сюда
						}
					}
				}

				// Если стартовая позиция находится на стене
				if (_waveMap[_start.X, _start.Y] == indicate_wall)
				{
					return false;
				}

				// Если финишная позиция находится на стене
				if (_waveMap[_target.X, _target.Y] == indicate_wall)
				{
					return false;
				}

				// Начинаем с финиша
				_waveMap[_target.X, _target.Y] = 0;

				while (add == true)
				{
					add = false;
					for (var x = 0; x < _map.MapWidth; x++)
					{
						for (var y = 0; y < _map.MapHeight; y++)
						{
							// Если ячейка свободная
							if (_waveMap[x, y] == _stepWave)
							{
								// Ставим значение шага + 1 в соседние ячейки (если они проходимы)
								if (x - 1 >= 0 && _waveMap[x - 1, y] == indicate_empty)
								{
									_waveMap[x - 1, y] = _stepWave + 1;
								}

								if (x + 1 < _map.MapWidth && _waveMap[x + 1, y] == indicate_empty)
								{
									_waveMap[x + 1, y] = _stepWave + 1;
								}

								if (y - 1 >= 0 && _waveMap[x, y - 1] == indicate_empty)
								{
									_waveMap[x, y - 1] = _stepWave + 1;
								}
								if (y + 1 < _map.MapHeight && _waveMap[x, y + 1] == indicate_empty)
								{
									_waveMap[x, y + 1] = _stepWave + 1;
								}
							}
						}
					}

					_stepWave++;

					add = true;

					// Решение найдено
					if (_waveMap[_start.X, _start.Y] != indicate_empty)
					{
						_isFoundPath = true;
						add = false;
					}

					// Решение не найдено
					if (_stepWave > _map.MapWidth * _map.MapHeight)
					{
						add = false;
					}

					// Если есть лимит и он превышен
					if (_searchLimit > 0 && _stepWave > _searchLimit)
					{
						add = false;
					}
				}

				return _isFoundPath;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение пути
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void BuildPath()
			{
				if (_map == null) return;
				if (_isFoundPath == false) return;
				_path.Clear();

				var minimum = 100000;
				var found = true;
				Int32 current_value;
				var count = 0;
				var cx = _start.X;
				var cy = _start.Y;

				while (found)
				{
					if (_waveMap[cx, cy] == 0)
					{
						found = false;
						break;
					}

					if (cx - 1 >= 0)
					{
						current_value = _waveMap[cx - 1, cy];
						if (current_value < minimum && current_value > 0)
						{
							cx = cx - 1;
							minimum = current_value;
							if (!_isAllowDiagonal) goto ortho;
						}
					}

					if (cx + 1 < _map.MapWidth)
					{
						current_value = _waveMap[cx + 1, cy];
						if (current_value < minimum && current_value > 0)
						{
							cx = cx + 1;
							minimum = current_value;
							if (!_isAllowDiagonal) goto ortho;
						}
					}

					if (cy - 1 >= 0)
					{
						current_value = _waveMap[cx, cy - 1];
						if (current_value < minimum && current_value > 0)
						{
							cy = cy - 1;
							minimum = current_value;
							if (!_isAllowDiagonal) goto ortho;
						}
					}

					if (cy + 1 < _map.MapHeight)
					{
						current_value = _waveMap[cx, cy + 1];
						if (current_value < minimum && current_value > 0)
						{
							cy = cy + 1;
							minimum = current_value;
							if (!_isAllowDiagonal) goto ortho;
						}
					}

				ortho:;
					_path.AddPathPoint(cx, cy, minimum);

					count++;

					if (count > _map.MapHeight * _map.MapWidth)
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
				if (_map == null) return;

				Int32 indicate_wall = -2, indicate_empty = -1;

				// Заполняем карту поиска пути на основе карты препятствий
				for (var y = 0; y < _map.MapHeight; y++)
				{
					for (var x = 0; x < _map.MapWidth; x++)
					{
						if (_map.Map[x, y] == XMapCode.BLOCK)
						{
							_waveMap[x, y] = indicate_wall; // индикатор стены
						}
						else
						{
							_waveMap[x, y] = indicate_empty; // индикатор еще не ступали сюда
						}
					}
				}

				// Если стартовая позиция находится на стене
				if (_waveMap[_start.X, _start.Y] == indicate_wall)
				{
					return;
				}

				// Если финишная позиция находится на стене
				if (_waveMap[_target.X, _target.Y] == indicate_wall)
				{
					return;
				}

				// Начинаем с финиша
				_waveMap[_target.X, _target.Y] = 0;
				_isFoundPath = false;
				_stepWave = 0;
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
				if (_map == null) return false;

				var indicate_empty = -1;

				for (var x = 0; x < _map.MapWidth; x++)
				{
					for (var y = 0; y < _map.MapHeight; y++)
					{
						// Если ячейка свободная
						if (_waveMap[x, y] == _stepWave)
						{
							// Ставим значение шага + 1 в соседние ячейки (если они проходимы)
							if (x - 1 >= 0 && _waveMap[x - 1, y] == indicate_empty)
							{
								_waveMap[x - 1, y] = _stepWave + 1;
							}

							if (x + 1 < _map.MapWidth && _waveMap[x + 1, y] == indicate_empty)
							{
								_waveMap[x + 1, y] = _stepWave + 1;
							}

							if (y - 1 >= 0 && _waveMap[x, y - 1] == indicate_empty)
							{
								_waveMap[x, y - 1] = _stepWave + 1;
							}
							if (y + 1 < _map.MapHeight && _waveMap[x, y + 1] == indicate_empty)
							{
								_waveMap[x, y + 1] = _stepWave + 1;
							}
						}
					}
				}

				_stepWave++;

				// Решение найдено
				if (_waveMap[_start.X, _start.Y] != indicate_empty)
				{
					_isFoundPath = true;
					return false;
				}

				// Решение не найдено
				if (_stepWave > _map.MapWidth * _map.MapHeight)
				{
					return false;
				}

				// Если есть лимит и он превышен
				if (_searchLimit > 0 && _stepWave > _searchLimit)
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
				if (_map == null) return;

				for (var ix = 0; ix < _map.MapWidth; ix++)
				{
					for (var iy = 0; iy < _map.MapHeight; iy++)
					{
						wave[ix, iy] = _waveMap[ix, iy];
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