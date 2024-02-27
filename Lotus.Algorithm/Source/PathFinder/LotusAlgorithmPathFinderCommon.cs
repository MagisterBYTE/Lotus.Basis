using System;
using System.Collections.Generic;

using Lotus.Maths;

namespace Lotus.Algorithm
{
    /**
     * \defgroup AlgorithmPathFinder Алгоритмы поиска пути
     * \ingroup Algorithm
     * \brief Подсистема алгоритмов поиска пути. В подсистеме представлены базовые алгоритмы поиска пути на двухмерной
		карте с прямоугольными ячейками.
	 * \details Более специализированные алгоритмы будут реализованы в других модулях.
     * @{
     */
    /// <summary>
    /// Направления движения.
    /// </summary>
    public enum TPathMoveDirection
    {
        /// <summary>
        /// Север (вверх).
        /// </summary>
        North,

        /// <summary>
        /// Юг (вниз).
        /// </summary>
        South,

        /// <summary>
        /// Запад (влево).
        /// </summary>
        West,

        /// <summary>
        /// Восток (вправо).
        /// </summary>
        East,

        /// <summary>
        /// Северо-восток (вверх-вправо по диагонали).
        /// </summary>
        Northeast,

        /// <summary>
        /// Северо-запад (вверх-влево по диагонали).
        /// </summary>
        Northwest,

        /// <summary>
        /// Юго-восток (вниз-вправо по диагонали).
        /// </summary>
        Southeast,

        /// <summary>
        /// Юго-запад (вниз-влево по диагонали).
        /// </summary>
        Southwest
    }

    /// <summary>
    /// Точка пути.
    /// </summary>
    /// <remarks>
    /// Структура для описания точки пути которая содержит также длину от старта.
    /// </remarks>
    public struct TPathPoint : IEquatable<TPathPoint>, IComparable<TPathPoint>, ICloneable
    {
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
        public int LengthFromStart;
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
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="location">Позиция точки карты.</param>
        public TPathPoint(Vector2Di location)
        {
            X = location.X;
            Y = location.Y;
            LengthFromStart = 0;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="x">Позиция точки пути по X.</param>
        /// <param name="y">Позиция точки пути по Y.</param>
        public TPathPoint(int x, int y)
        {
            X = x;
            Y = y;
            LengthFromStart = 0;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="x">Позиция точки пути по X.</param>
        /// <param name="y">Позиция точки пути по Y.</param>
        /// <param name="lengthFromStart">Длина пути от старта (Параметр G).</param>
        public TPathPoint(int x, int y, int lengthFromStart)
        {
            X = x;
            Y = y;
            LengthFromStart = lengthFromStart;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is TPathPoint path_point)
            {
                return Equals(path_point);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства точек пути по значению.
        /// </summary>
        /// <param name="other">Сравниваемая точка пути.</param>
        /// <returns>Статус равенства.</returns>
        public readonly bool Equals(TPathPoint other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Сравнение точек пути для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемая точка пути.</param>
        /// <returns>Статус сравнения.</returns>
        public readonly int CompareTo(TPathPoint other)
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
                    if (X == other.X && Y == other.Y)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода точки пути.
        /// </summary>
        /// <returns>Хеш-код точки пути.</returns>
        public override readonly int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        /// <summary>
        /// Полное копирование точки пути.
        /// </summary>
        /// <returns>Копия точки пути.</returns>
        public readonly object Clone()
        {
            return MemberwiseClone();
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

        #region Operators 
        /// <summary>
        /// Сравнение объектов на равенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус равенства.</returns>
        public static bool operator ==(TPathPoint left, TPathPoint right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус неравенство.</returns>
        public static bool operator !=(TPathPoint left, TPathPoint right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Сравнение объектов по операции меньше.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator <(TPathPoint left, TPathPoint right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Сравнение объектов по операции меньше или равно.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator <=(TPathPoint left, TPathPoint right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Сравнение объектов по операции больше.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator >(TPathPoint left, TPathPoint right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Сравнение объектов по операции больше или равно.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator >=(TPathPoint left, TPathPoint right)
        {
            return left.CompareTo(right) >= 0;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение направления движения на следующую точку пути.
        /// </summary>
        /// <param name="next">Следующая точка пути.</param>
        /// <returns>Направления движения.</returns>
        public readonly TPathMoveDirection GetMoveDirection(TPathPoint next)
        {
            // Вверх-вниз
            if (X == next.X)
            {
                if (Y > next.Y)
                {
                    return TPathMoveDirection.North;
                }
                else
                {
                    return TPathMoveDirection.South;
                }
            }
            else
            {
                // По бокам
                if (Y == next.Y)
                {
                    if (X > next.X)
                    {
                        return TPathMoveDirection.West;
                    }
                    else
                    {
                        return TPathMoveDirection.East;
                    }
                }

                // Диагонали
                if (X > next.X && Y > next.Y)
                {
                    return TPathMoveDirection.Northwest;
                }
                if (X < next.X && Y > next.Y)
                {
                    return TPathMoveDirection.Northeast;
                }
                if (X > next.X && Y < next.Y)
                {
                    return TPathMoveDirection.Southwest;
                }
                if (X < next.X && Y < next.Y)
                {
                    return TPathMoveDirection.Southeast;
                }
            }

            return TPathMoveDirection.North;
        }
        #endregion
    }

    /// <summary>
    /// Путь.
    /// </summary>
    /// <remarks>
    /// Путь представляет собой список точек от начальной до конечной располагаемых последовательно.
    /// </remarks>
    public class CPath : List<TPathPoint>
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Начальная точка пути.
        /// </summary>
        public TPathPoint Start
        {
            get { return this[0]; }
        }

        /// <summary>
        /// Целевая точка пути.
        /// </summary>
        public TPathPoint Target
        {
            get { return this[Count - 1]; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CPath()
            : base()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="capacity">Емкость.</param>
        public CPath(int capacity)
            : base(capacity)
        {

        }
        #endregion

        #region Maim methods
        /// <summary>
        /// Добавление точки пути.
        /// </summary>
        /// <param name="x">Позиция точки пути по X.</param>
        /// <param name="y">Позиция точки пути по Y.</param>
        public void AddPathPoint(int x, int y)
        {
            Add(new TPathPoint(x, y));
        }

        /// <summary>
        /// Добавление точки пути.
        /// </summary>
        /// <param name="x">Позиция точки пути по X.</param>
        /// <param name="y">Позиция точки пути по Y.</param>
        /// <param name="lengthFromStart">Длина пути от старта (Параметр G).</param>
        public void AddPathPoint(int x, int y, int lengthFromStart)
        {
            Add(new TPathPoint(x, y, lengthFromStart));
        }

        /// <summary>
        ///Проверка на пересечение путей.
        /// </summary>
        /// <param name="path">Проверяемый путь.</param>
        /// <param name="pointIntersect">Точка пересечения.</param>
        /// <returns>Статус пересечения путей.</returns>
        public bool Intersect(CPath path, out TPathPoint pointIntersect)
        {
            for (var i = 0; i < Count; i++)
            {
                var p = this[i];
                for (var j = 0; j < path.Count; j++)
                {
                    if (p.X == path[j].X && p.Y == path[j].Y)
                    {
                        pointIntersect = p;
                        return true;
                    }
                }
            }

            pointIntersect = new TPathPoint(0, 0);
            return false;
        }
        #endregion
    }

    /// <summary>
    /// Определение интерфейса для алгоритма поиска пути.
    /// </summary>
    public interface ILotusPathFinder
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Интерфейс карты для поиска пути.
        /// </summary>
        ILotusMap2D? Map { get; set; }

        /// <summary>
        /// Возможность перемещения по диагонали.
        /// </summary>
        bool IsAllowDiagonal { get; set; }

        /// <summary>
        /// Стоимость перемещения по диагонали выше чем обычное перемещение.
        /// </summary>
        bool HeavyDiagonals { get; set; }

        /// <summary>
        /// Лимит поиска.
        /// </summary>
        int SearchLimit { get; set; }

        /// <summary>
        /// Начальная точка для поиска пути.
        /// </summary>
        TMapPoint Start { get; set; }

        /// <summary>
        /// Целевая точка для поиска пути.
        /// </summary>
        TMapPoint Target { get; set; }

        /// <summary>
        /// Статус нахождения пути.
        /// </summary>
        bool IsFoundPath { get; }

        /// <summary>
        /// Найденный путь.
        /// </summary>
        CPath Path { get; }

        //
        // СОБЫТИЯ
        //
        /// <summary>
        /// Событие для нотификации об окончании нахождения пути.
        /// </summary>
        event Action OnPathFound;
        #endregion

        #region МЕТОДЫ 
        /// <summary>
        /// Поиск пути по предварительно установленным параметрам.
        /// </summary>
        /// <returns>Статус нахождения пути.</returns>
        bool Find();

        /// <summary>
        /// Поиск пути по указанным данным.
        /// </summary>
        /// <param name="start">Начальная точка для поиска пути.</param>
        /// <param name="target">Целевая точка для поиска пути.</param>
        /// <returns>Статус нахождения пути.</returns>
        bool Find(TMapPoint start, TMapPoint target);
        #endregion
    }

    /// <summary>
    /// Базовый класс для поиска пути.
    /// </summary>
    public abstract class PathFinder : ILotusPathFinder
    {
        #region Fields
        // Основные параметры
        protected internal ILotusMap2D? _map;
        protected internal bool _isAllowDiagonal;
        protected internal bool _heavyDiagonals;
        protected internal int _searchLimit;
        protected internal TMapPoint _start;
        protected internal TMapPoint _target;
        protected internal bool _isFoundPath;
        protected internal CPath _path;

        // События
        protected internal Action? _onPathFound;
        #endregion

        #region Properties ILotusPathFinder 
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Интерфейс карты для поиска пути.
        /// </summary>
        public ILotusMap2D? Map
        {
            get { return _map; }
            set { _map = value; }
        }

        /// <summary>
        /// Возможность перемещения по диагонали.
        /// </summary>
        public bool IsAllowDiagonal
        {
            get { return _isAllowDiagonal; }
            set { _isAllowDiagonal = value; }
        }

        /// <summary>
        /// Стоимость перемещения по диагонали выше чем обычное перемещение.
        /// </summary>
        public bool HeavyDiagonals
        {
            get { return _heavyDiagonals; }
            set { _heavyDiagonals = value; }
        }

        /// <summary>
        /// Лимит поиска.
        /// </summary>
        public int SearchLimit
        {
            get { return _searchLimit; }
            set { _searchLimit = value; }
        }

        /// <summary>
        /// Начальная точка для поиска пути.
        /// </summary>
        public TMapPoint Start
        {
            get { return _start; }
            set { _start = value; }
        }

        /// <summary>
        /// Целевая точка для поиска пути.
        /// </summary>
        public TMapPoint Target
        {
            get { return _target; }
            set { _target = value; }
        }

        /// <summary>
        /// Статус нахождения пути.
        /// </summary>
        public bool IsFoundPath
        {
            get { return _isFoundPath; }
        }

        /// <summary>
        /// Найденный путь.
        /// </summary>
        public CPath Path
        {
            get { return _path; }
        }

        //
        // СОБЫТИЯ
        //
        /// <summary>
        /// Событие для нотификации об окончании нахождения пути.
        /// </summary>
        public event Action OnPathFound
        {
            add { _onPathFound += value; }
            remove { _onPathFound -= value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        protected PathFinder()
        {
            _path = new CPath();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="map">Карта.</param>
        protected PathFinder(ILotusMap2D map)
        {
            _map = map;
            _path = new CPath();
        }
        #endregion

        #region ILotusPathFinder methods
        /// <summary>
        /// Поиск пути по предварительно установленным параметрам.
        /// </summary>
        /// <returns>Статус нахождения пути.</returns>
        public bool Find()
        {
            if (ExpansionWave())
            {
                BuildPath();
            }

            return _isFoundPath;
        }

        /// <summary>
        /// Поиск пути по указанным данным.
        /// </summary>
        /// <param name="start">Начальная точка для поиска пути.</param>
        /// <param name="target">Целевая точка для поиска пути.</param>
        /// <returns>Статус нахождения пути.</returns>
        public bool Find(TMapPoint start, TMapPoint target)
        {
            _start = start;
            _target = target;
            return Find();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Сброс данных о прохождении пути.
        /// </summary>
        public abstract void ResetWave();

        /// <summary>
        /// Распространение волны по карте.
        /// </summary>
        /// <returns>Статус нахождение пути.</returns>
        public abstract bool ExpansionWave();

        /// <summary>
        /// Построение пути.
        /// </summary>
        public abstract void BuildPath();

        /// <summary>
        /// Подготовка данных к запуску волны. Применяется когда надо отобразить распространение волны по шагам.
        /// </summary>
        public abstract void PreparationsWaveOnStep();

        /// <summary>
        /// Распространение волны по карте пошагово.
        /// </summary>
        /// <remarks>
        /// Метод должен быть вызван в цикле до достижения окончания поиска.
        /// </remarks>
        /// <returns>True, если решение еще не найдено и False если решение найдено или превышен лимит.</returns>
        public abstract bool ExpansionWaveOnStep();

        /// <summary>
        /// Заполнение данных распространения волны действия алгоритма.
        /// </summary>
        /// <remarks>
        /// Метод в основном служебный, предназначен для демонстрации действия алгоритма.
        /// </remarks>
        /// <param name="wave">Карта для отображения волны действия алгоритма.</param>
        public abstract void SetWave(int[,] wave);
        #endregion
    }
    /**@}*/
}