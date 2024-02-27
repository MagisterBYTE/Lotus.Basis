using System;
using System.Collections.Generic;

using Lotus.Core;
using Lotus.Maths;

namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmPathFinder
	*@{*/
    /// <summary>
    /// Точка пути для алгоритма A-Star.
    /// </summary>
    public struct TPathPointStar : IComparable<TPathPointStar>
    {
        #region Const
        /// <summary>
        /// Компаратор для точки пути алгоритма A-Star.
        /// </summary>
        public static readonly TPathPointStar Comparer = new TPathPointStar();
        #endregion

        #region Fields
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Позиция точки пути по X.
        /// </summary>
        public int X;

        /// <summary>
        /// Позиция точки пути по Y.
        /// </summary>
        public int Y;

        /// <summary>
        /// Длина пути от старта (Параметр G).
        /// </summary>
        public int PathLengthFromStart;

        /// <summary>
        /// Примерное расстояние до цели (Параметр H).
        /// </summary>
        public int HeuristicEstimatePathLength;

        /// <summary>
        /// Ожидаемое полное расстояние до цели (Параметр F).
        /// </summary>
        public int EstimateFullPathLength;

        /// <summary>
        /// Позиция родительской точки пути по X.
        /// </summary>
        /// <remarks>
        /// Под родительской точкой понимается точка из котором мы пришли в данную.
        /// </remarks>
        public int ParentX;

        /// <summary>
        /// Позиция родительской точки пути по Y.
        /// </summary>
        /// <remarks>
        /// Под родительской точкой понимается точка из котором мы пришли в данную.
        /// </remarks>
        public int ParentY;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Позиция точки пути.
        /// </summary>
        public Vector2Di Location
        {
            readonly get { return new Vector2Di(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Позиция родительской точки пути.
        /// </summary>
        /// <remarks>
        /// Под родительской точкой понимается точка из котором мы пришли в данную.
        /// </remarks>
        public Vector2Di ParentLocation
        {
            readonly get { return new Vector2Di(ParentX, ParentY); }
            set
            {
                ParentX = value.X;
                ParentY = value.Y;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="location">Позиция точки пути.</param>
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

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="x">Позиция точки пути по X.</param>
        /// <param name="y">Позиция точки пути по Y.</param>
        public TPathPointStar(int x, int y)
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

        #region System methods
        /// <summary>
        /// Сравнение точек пути по полному расстоянию до цели.
        /// </summary>
        /// <param name="other">Точка пути.</param>
        /// <returns>Статус сравнения.</returns>
        public readonly int CompareTo(TPathPointStar other)
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

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление с указанием значений координат.</returns>
        public override readonly string ToString()
        {
            return "X = " + X.ToString() + "; Y = " + Y.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Тип эвристической функции для оценки расстояния и стоимости маршрута.
    /// </summary>
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

    /// <summary>
    /// Поиск пути по алгоритму A-Star.
    /// </summary>
    /// <see href="https://www.codeproject.com/Articles/15307/A-algorithm-implementation-in-C"/>
    public class PathFinderAStar : PathFinder
    {
        #region Fields
        // Основные параметры
        protected internal PriorityQueue<TPathPointStar> _openList;
        protected internal List<TPathPointStar> _closeList;
        protected internal int[,]? _directions;
        protected internal int _horiz = 0;
        protected internal THeuristicFormula _formula = THeuristicFormula.Manhattan;
        protected internal int _heuristicEstimate = 2;
        protected internal bool _punishChangeDirection = false;
        protected internal bool _reopenCloseNodes = false;
        protected internal bool _tieBreaker = false;
        #endregion

        #region Properties
        /// <summary>
        /// Тип эвристической функции для оценки расстояния и стоимости маршрута.
        /// </summary>
        public THeuristicFormula Formula
        {
            get { return _formula; }
            set { _formula = value; }
        }

        /// <summary>
        /// Константа для оценки расстояния и стоимости маршрута.
        /// </summary>
        /// <remarks>
        /// Это константа, которая повлияет на расчетное расстояние от текущей позиции до места назначения цели. 
        /// Эвристическая функция используется для создания оценки, как долго он будет принимать для достижения
        /// цели. Чем лучше оценки, тем короче нашли путь
        /// </remarks>
        public int HeuristicEstimate
        {
            get { return _heuristicEstimate; }
            set { _heuristicEstimate = value; }
        }

        /// <summary>
        /// Повышенная стоимость смены направлений.
        /// </summary>
        /// <remarks>
        /// Смысл заключается в том что когда алгоритм меняет направление он будет иметь небольшую стоимость.
        /// Конечный результат заключается в том, что если путь будет найден, он будет сравнительно ровной, не
        /// слишком много меняет направление, так выглядит более естественно. Недостатком является то, что это займет
        /// больше времени, потому что необходимо исследование дополнительных узлов
        /// </remarks>
        public bool PunishChangeDirection
        {
            get { return _punishChangeDirection; }
            set { _punishChangeDirection = value; }
        }

        /// <summary>
        /// Повторное открытие закрытых узлов.
        /// </summary>
        /// <remarks>
        /// Истинного значение, разрешает алгоритму повторно анализировать узлы, которые уже были закрыты, 
        /// когда стоимость меньше, чем предыдущее значение. Если повторно просматривать узлы разрешено то путь
        /// будет лучше и ровнее путь, но это займет больше времени
        /// </remarks>
        public bool ReopenCloseNodes
        {
            get { return _reopenCloseNodes; }
            set { _reopenCloseNodes = value; }
        }

        /// <summary>
        /// Использовать дополнительно время.
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
        public bool TieBreaker
        {
            get { return _tieBreaker; }
            set { _tieBreaker = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public PathFinderAStar()
            : base()
        {
            _openList = new PriorityQueue<TPathPointStar>();
            _closeList = new List<TPathPointStar>();
            _heuristicEstimate = 2;
            _searchLimit = 10000;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="map">Карта.</param>
        public PathFinderAStar(ILotusMap2D map)
            : base(map)
        {
            _openList = new PriorityQueue<TPathPointStar>();
            _closeList = new List<TPathPointStar>();
            _heuristicEstimate = 2;
            _searchLimit = 10000;
        }
        #endregion

        #region МЕТОДЫ methods
        /// <summary>
        /// Сброс данных о прохождении пути.
        /// </summary>
        public override void ResetWave()
        {
            _openList.Clear();
            _closeList.Clear();

            _isFoundPath = false;
        }

        /// <summary>
        /// Распространение волны по карте.
        /// </summary>
        /// <returns>Статус нахождение пути.</returns>
        public override bool ExpansionWave()
        {
            if (_map == null)
            {
                return false;
            }

            _isFoundPath = false;

            _openList.Clear();
            _closeList.Clear();

            if (_isAllowDiagonal)
            {
                _directions = new int[8, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 }, { 1, -1 }, { 1, 1 }, { -1, 1 }, { -1, -1 } };
            }
            else
            {
                _directions = new int[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
            }

            TPathPointStar parent_node;
            parent_node.PathLengthFromStart = 0;
            parent_node.HeuristicEstimatePathLength = _heuristicEstimate;
            parent_node.EstimateFullPathLength = parent_node.PathLengthFromStart + parent_node.HeuristicEstimatePathLength;
            parent_node.X = _start.X;
            parent_node.Y = _start.Y;
            parent_node.ParentX = parent_node.X;
            parent_node.ParentY = parent_node.Y;

            _openList.Push(parent_node);

            while (_openList.Count > 0)
            {
                parent_node = _openList.Pop();

                // Если нашли
                if (parent_node.X == _target.X && parent_node.Y == _target.Y)
                {
                    _closeList.Add(parent_node);
                    _isFoundPath = true;
                    break;
                }

                if (_closeList.Count > _searchLimit)
                {
                    return _isFoundPath;
                }

                if (_punishChangeDirection)
                {
                    _horiz = parent_node.X - parent_node.ParentX;
                }

                // Проводим вычисления для каждого смежного элемента
                for (var i = 0; i < (_isAllowDiagonal ? 8 : 4); i++)
                {
                    TPathPointStar new_node;
                    new_node.X = parent_node.X + _directions[i, 0];
                    new_node.Y = parent_node.Y + _directions[i, 1];

                    if (new_node.X < 0 || new_node.Y < 0 || new_node.X >= _map.MapWidth || new_node.Y >= _map.MapHeight)
                    {
                        continue;
                    }

                    int new_length;
                    if (_heavyDiagonals && i > 3)
                    {
                        new_length = parent_node.PathLengthFromStart + (int)(_map.Map[new_node.X, new_node.Y] * 2.41f);
                    }
                    else
                    {
                        new_length = parent_node.PathLengthFromStart + _map.Map[new_node.X, new_node.Y];
                    }


                    if (new_length == parent_node.PathLengthFromStart)
                    {
                        //Unbrekeable
                        continue;
                    }

                    if (_punishChangeDirection)
                    {
                        if (new_node.X - parent_node.X != 0)
                        {
                            if (_horiz == 0)
                            {
                                new_length += 20;
                            }
                        }
                        if (new_node.Y - parent_node.Y != 0)
                        {
                            if (_horiz != 0)
                            {
                                new_length += 20;
                            }
                        }
                    }

                    var found_in_open_index = -1;
                    for (var j = 0; j < _openList.Count; j++)
                    {
                        if (_openList[j].X == new_node.X && _openList[j].Y == new_node.Y)
                        {
                            found_in_open_index = j;
                            break;
                        }
                    }
                    if (found_in_open_index != -1 && _openList[found_in_open_index].PathLengthFromStart <= new_length)
                    {
                        continue;
                    }

                    var found_in_close_index = -1;
                    for (var j = 0; j < _closeList.Count; j++)
                    {
                        if (_closeList[j].X == new_node.X && _closeList[j].Y == new_node.Y)
                        {
                            found_in_close_index = j;
                            break;
                        }
                    }
                    if (found_in_close_index != -1 && (_reopenCloseNodes || _closeList[found_in_close_index].PathLengthFromStart <= new_length))
                    {
                        continue;
                    }

                    new_node.ParentX = parent_node.X;
                    new_node.ParentY = parent_node.Y;
                    new_node.PathLengthFromStart = new_length;

                    switch (_formula)
                    {
                        case THeuristicFormula.MaxDXDY:
                            {
                                new_node.HeuristicEstimatePathLength = _heuristicEstimate * Math.Max(Math.Abs(new_node.X - _target.X), Math.Abs(new_node.Y - _target.Y));
                            }
                            break;
                        case THeuristicFormula.DiagonalShortCut:
                            {
                                var h_diagonal = Math.Min(Math.Abs(new_node.X - _target.X), Math.Abs(new_node.Y - _target.Y));
                                var h_straight = Math.Abs(new_node.X - _target.X) + Math.Abs(new_node.Y - _target.Y);
                                new_node.HeuristicEstimatePathLength = (_heuristicEstimate * 2 * h_diagonal) + (_heuristicEstimate * (h_straight - (2 * h_diagonal)));
                            }
                            break;
                        case THeuristicFormula.Euclidean:
                            {
                                new_node.HeuristicEstimatePathLength = (int)(_heuristicEstimate * Math.Sqrt(Math.Pow(new_node.X - _target.X, 2) + Math.Pow(new_node.Y - _target.Y, 2)));
                            }
                            break;
                        case THeuristicFormula.EuclideanNoSQR:
                            {
                                new_node.HeuristicEstimatePathLength = (int)(_heuristicEstimate * (Math.Pow(new_node.X - _target.X, 2) + Math.Pow(new_node.Y - _target.Y, 2)));
                            }
                            break;
                        case THeuristicFormula.Custom1:
                            var dxy = new TMapPoint(Math.Abs(_target.X - new_node.X), Math.Abs(_target.Y - new_node.Y));
                            var Orthogonal = Math.Abs(dxy.X - dxy.Y);
                            var Diagonal = Math.Abs((dxy.X + dxy.Y - Orthogonal) / 2);
                            new_node.HeuristicEstimatePathLength = _heuristicEstimate * (Diagonal + Orthogonal + dxy.X + dxy.Y);
                            break;
                        default:
                            {
                                new_node.HeuristicEstimatePathLength = _heuristicEstimate * (Math.Abs(new_node.X - _target.X) + Math.Abs(new_node.Y - _target.Y));
                            }
                            break;
                    }

                    if (_tieBreaker)
                    {
                        var dx1 = parent_node.X - _target.X;
                        var dy1 = parent_node.Y - _target.Y;
                        var dx2 = _start.X - _target.X;
                        var dy2 = _start.Y - _target.Y;
                        var cross = Math.Abs((dx1 * dy2) - (dx2 * dy1));
                        new_node.HeuristicEstimatePathLength = (int)(new_node.HeuristicEstimatePathLength + (cross * 0.001));
                    }

                    new_node.EstimateFullPathLength = new_node.PathLengthFromStart + new_node.HeuristicEstimatePathLength;

                    _openList.Push(new_node);
                }

                _closeList.Add(parent_node);
            }

            return _isFoundPath;
        }

        /// <summary>
        /// Построение пути.
        /// </summary>
        public override void BuildPath()
        {
            _path.Clear();
            var node = _closeList[_closeList.Count - 1];
            for (var i = _closeList.Count - 1; i >= 0; i--)
            {
                if ((node.ParentX == _closeList[i].X && node.ParentY == _closeList[i].Y) || i == _closeList.Count - 1)
                {
                    node = _closeList[i];
                    _path.AddPathPoint(node.X, node.Y, node.PathLengthFromStart);
                }
                else
                {
                    _closeList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Подготовка данных к запуску алгоритма. Применяется когда надо отобразить распространение алгоритма по шагам.
        /// </summary>
        public override void PreparationsWaveOnStep()
        {
            _isFoundPath = false;

            _openList.Clear();
            _closeList.Clear();

            if (_isAllowDiagonal)
            {
                _directions = new int[8, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 }, { 1, -1 }, { 1, 1 }, { -1, 1 }, { -1, -1 } };
            }
            else
            {
                _directions = new int[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
            }

            TPathPointStar parent_node;
            parent_node.PathLengthFromStart = 0;
            parent_node.HeuristicEstimatePathLength = _heuristicEstimate;
            parent_node.EstimateFullPathLength = parent_node.PathLengthFromStart + parent_node.HeuristicEstimatePathLength;
            parent_node.X = _start.X;
            parent_node.Y = _start.Y;
            parent_node.ParentX = parent_node.X;
            parent_node.ParentY = parent_node.Y;

            _openList.Push(parent_node);
        }

        /// <summary>
        /// Распространение волны по карте по шагово.
        /// </summary>
        /// <remarks>
        /// Метод должен быть вызван в цикле до достижения окончания поиска.
        /// </remarks>
        /// <returns>True, если решение еще не найдено и False если решение найдено или превышен лимит.</returns>
        public override bool ExpansionWaveOnStep()
        {
            if (_map == null)
            {
                return false;
            }

            var parent_node = _openList.Pop();

            // Если нашли
            if (parent_node.X == _target.X && parent_node.Y == _target.Y)
            {
                _closeList.Add(parent_node);
                return false;
            }

            if (_closeList.Count > _searchLimit)
            {
                return false;
            }

            if (_punishChangeDirection)
            {
                _horiz = parent_node.X - parent_node.ParentX;
            }

            // Проводим вычисления для каждого смежного элемента
            for (var i = 0; i < (_isAllowDiagonal ? 8 : 4); i++)
            {
                TPathPointStar new_node;
                new_node.X = parent_node.X + _directions![i, 0];
                new_node.Y = parent_node.Y + _directions![i, 1];

                if (new_node.X < 0 || new_node.Y < 0 || new_node.X >= _map.MapWidth || new_node.Y >= _map.MapHeight)
                {
                    continue;
                }

                int new_length;
                if (_heavyDiagonals && i > 3)
                {
                    new_length = parent_node.PathLengthFromStart + (int)(_map.Map[new_node.X, new_node.Y] * 2.41f);
                }
                else
                {
                    new_length = parent_node.PathLengthFromStart + _map.Map[new_node.X, new_node.Y];
                }


                if (new_length == parent_node.PathLengthFromStart)
                {
                    //Unbrekeable
                    continue;
                }

                if (_punishChangeDirection)
                {
                    if (new_node.X - parent_node.X != 0)
                    {
                        if (_horiz == 0)
                        {
                            new_length += 20;
                        }
                    }
                    if (new_node.Y - parent_node.Y != 0)
                    {
                        if (_horiz != 0)
                        {
                            new_length += 20;
                        }
                    }
                }

                var found_in_open_index = -1;
                for (var j = 0; j < _openList.Count; j++)
                {
                    if (_openList[j].X == new_node.X && _openList[j].Y == new_node.Y)
                    {
                        found_in_open_index = j;
                        break;
                    }
                }
                if (found_in_open_index != -1 && _openList[found_in_open_index].PathLengthFromStart <= new_length)
                {
                    continue;
                }

                var found_in_close_index = -1;
                for (var j = 0; j < _closeList.Count; j++)
                {
                    if (_closeList[j].X == new_node.X && _closeList[j].Y == new_node.Y)
                    {
                        found_in_close_index = j;
                        break;
                    }
                }
                if (found_in_close_index != -1 && (_reopenCloseNodes || _closeList[found_in_close_index].PathLengthFromStart <= new_length))
                {
                    continue;
                }

                new_node.ParentX = parent_node.X;
                new_node.ParentY = parent_node.Y;
                new_node.PathLengthFromStart = new_length;

                switch (_formula)
                {
                    case THeuristicFormula.MaxDXDY:
                        {
                            new_node.HeuristicEstimatePathLength = _heuristicEstimate * Math.Max(Math.Abs(new_node.X - _target.X), Math.Abs(new_node.Y - _target.Y));
                        }
                        break;
                    case THeuristicFormula.DiagonalShortCut:
                        {
                            var h_diagonal = Math.Min(Math.Abs(new_node.X - _target.X), Math.Abs(new_node.Y - _target.Y));
                            var h_straight = Math.Abs(new_node.X - _target.X) + Math.Abs(new_node.Y - _target.Y);
                            new_node.HeuristicEstimatePathLength = (_heuristicEstimate * 2 * h_diagonal) + (_heuristicEstimate * (h_straight - (2 * h_diagonal)));
                        }
                        break;
                    case THeuristicFormula.Euclidean:
                        {
                            new_node.HeuristicEstimatePathLength = (int)(_heuristicEstimate * Math.Sqrt(Math.Pow(new_node.X - _target.X, 2) + Math.Pow(new_node.Y - _target.Y, 2)));
                        }
                        break;
                    case THeuristicFormula.EuclideanNoSQR:
                        {
                            new_node.HeuristicEstimatePathLength = (int)(_heuristicEstimate * (Math.Pow(new_node.X - _target.X, 2) + Math.Pow(new_node.Y - _target.Y, 2)));
                        }
                        break;
                    case THeuristicFormula.Custom1:
                        var dxy = new TMapPoint(Math.Abs(_target.X - new_node.X), Math.Abs(_target.Y - new_node.Y));
                        var Orthogonal = Math.Abs(dxy.X - dxy.Y);
                        var Diagonal = Math.Abs((dxy.X + dxy.Y - Orthogonal) / 2);
                        new_node.HeuristicEstimatePathLength = _heuristicEstimate * (Diagonal + Orthogonal + dxy.X + dxy.Y);
                        break;
                    default:
                        {
                            new_node.HeuristicEstimatePathLength = _heuristicEstimate * (Math.Abs(new_node.X - _target.X) + Math.Abs(new_node.Y - _target.Y));
                        }
                        break;
                }

                if (_tieBreaker)
                {
                    var dx1 = parent_node.X - _target.X;
                    var dy1 = parent_node.Y - _target.Y;
                    var dx2 = _start.X - _target.X;
                    var dy2 = _start.Y - _target.Y;
                    var cross = Math.Abs((dx1 * dy2) - (dx2 * dy1));
                    new_node.HeuristicEstimatePathLength = (int)(new_node.HeuristicEstimatePathLength + (cross * 0.001));
                }

                new_node.EstimateFullPathLength = new_node.PathLengthFromStart + new_node.HeuristicEstimatePathLength;

                _openList.Push(new_node);
            }

            _closeList.Add(parent_node);

            return true;
        }

        /// <summary>
        /// Заполнение данных распространения волны действия алгоритма.
        /// </summary>
        /// <remarks>
        /// Метод в основном служебный, предназначен для демонстрации действия алгоритма.
        /// </remarks>
        /// <param name="wave">Карта для отображения волны действия алгоритма.</param>
        public override void SetWave(int[,] wave)
        {
            for (var i = 0; i < _closeList.Count; i++)
            {
                wave[_closeList[i].X, _closeList[i].Y] = _closeList[i].PathLengthFromStart;
            }
        }
        #endregion
    }
    /**@}*/
}