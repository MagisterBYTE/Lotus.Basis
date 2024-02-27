using System;

using Lotus.Maths;

namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmPathFinder
	*@{*/
    /// <summary>
    /// Статический класс для кодов проходимости ячеек.
    /// </summary>
    /// <remarks>
    /// Предназначен для унификации представления статуса проходимости/непроходимости ячеек.
    /// </remarks>
    public static class XMapCode
    {
        /// <summary>
        /// Блокированная(непроходимая) ячейка.
        /// </summary>
        public const int BLOCK = 0;

        /// <summary>
        /// Стандартная проходимая ячейка.
        /// </summary>
        public const int EMPTY = 1;
    }

    /// <summary>
    /// Точка карты.
    /// </summary>
    /// <remarks>
    /// Структура для описания базовой точки карты.
    /// </remarks>
    public struct TMapPoint : IEquatable<TMapPoint>, IComparable<TMapPoint>
    {
        #region Const
        /// <summary>
        /// Неопределенная точка карты.
        /// </summary>
        public static readonly TMapPoint Undef = new TMapPoint(-1, -1);
        #endregion

        #region Fields
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Позиция точки карты по X.
        /// </summary>
        public int X;

        /// <summary>
        /// Позиция точки карты по Y.
        /// </summary>
        public int Y;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Позиция точки карты.
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
        public TMapPoint(Vector2Di location)
        {
            X = location.X;
            Y = location.Y;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="x">Позиция точки карты по X.</param>
        /// <param name="y">Позиция точки карты по Y.</param>
        public TMapPoint(int x, int y)
        {
            X = x;
            Y = y;
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
            if (obj is TMapPoint map_point)
            {
                return Equals(map_point);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства точек карты по значению.
        /// </summary>
        /// <param name="other">Сравниваемая точка карты.</param>
        /// <returns>Статус равенства.</returns>
        public readonly bool Equals(TMapPoint other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Сравнение точек карты для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемая точка карты.</param>
        /// <returns>Статус сравнения.</returns>
        public readonly int CompareTo(TMapPoint other)
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
        /// Получение хеш-кода точки карты.
        /// </summary>
        /// <returns>Хеш-код точки карты.</returns>
        public override readonly int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
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
    /// Определение интерфейса для прямоугольный карты по которой осуществляется поиск пути.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Карта должна представлять собой лишь данные по ее размеру и проходимости/недоступности соответствующий ячеек/точек.
    /// </para>
    /// <para>
    /// Проходимость ячейки определяется в диапазоне от 1 (полностью проходима) до 100 (самая сложная проходимость).
    /// </para>
    /// </remarks>
    public interface ILotusMap2D
    {
        #region Properties
        /// <summary>
        /// Основная карта.
        /// </summary>
        /// <remarks>
        /// Основная карта представляет собой двухмерный массив целых числе где обозначены
        /// уровни проходимости каждой ячейки
        /// </remarks>
        int[,] Map { get; }

        /// <summary>
        /// Ширина карты (количество ячеек по горизонтали).
        /// </summary>
        int MapWidth { get; }

        /// <summary>
        /// Высота карты (количество ячеек по вертикали).
        /// </summary>
        int MapHeight { get; }
        #endregion
    }

    /// <summary>
    /// Класс представляющий собой карту.
    /// </summary>
    public class CMap2D : ILotusMap2D
    {
        #region Fields
        // Основные параметры
        protected internal int[,] _map;
        protected internal int _mapWidth;
        protected internal int _mapHeight;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Основная карта с препятствиями.
        /// </summary>
        public int[,] Map
        {
            get { return _map; }
            set { _map = value; }
        }

        /// <summary>
        /// Ширина карты (количество ячеек по горизонтали).
        /// </summary>
        public int MapWidth
        {
            get { return _mapWidth; }
            set { _mapWidth = value; }
        }

        /// <summary>
        /// Высота карты (количество ячеек по вертикали).
        /// </summary>
        public int MapHeight
        {
            get { return _mapHeight; }
            set { _mapHeight = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CMap2D()
            : this(1, 1)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="mapWidth">Ширина карты.</param>
        /// <param name="mapHeight">Высота карты.</param>
        public CMap2D(int mapWidth, int mapHeight)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _map = new int[_mapWidth, _mapHeight];
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установка всей карты проходимой.
        /// </summary>
        public void SetEmpty()
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    _map[x, y] = XMapCode.EMPTY;
                }
            }
        }

        /// <summary>
        /// Установка проходимой ячейки по указанным координатам.
        /// </summary>
        /// <param name="x">Координата ячейки по X.</param>
        /// <param name="y">Координата ячейки по Y.</param>
        public void SetEmpty(int x, int y)
        {
            _map[x, y] = XMapCode.EMPTY;
        }

        /// <summary>
        /// Установка всей карты полностью непроходимой.
        /// </summary>
        public void SetBlock()
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    _map[x, y] = XMapCode.BLOCK;
                }
            }
        }

        /// <summary>
        /// Установка непроходимой ячейки по указанным координатам.
        /// </summary>
        /// <param name="x">Координата ячейки по X.</param>
        /// <param name="y">Координата ячейки по Y.</param>
        public void SetBlock(int x, int y)
        {
            _map[x, y] = XMapCode.BLOCK;
        }

        /// <summary>
        /// Установка всей карты указанного уровня проходимости.
        /// </summary>
        /// <remarks>
        /// Проходимость ячейки определяется в диапазоне от 1 (полностью проходима) до 100 (самая сложная проходимость).
        /// </remarks>
        /// <param name="passability">Уровень проходимости.</param>
        public void SetPassability(int passability)
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    _map[x, y] = passability;
                }
            }
        }

        /// <summary>
        /// Установка уровня проходимости по указанным координатам.
        /// </summary>
        /// <remarks>
        /// Проходимость ячейки определяется в диапазоне от 1 (полностью проходима) до 100 (самая сложная проходимость).
        /// </remarks>
        /// <param name="x">Координата ячейки по X.</param>
        /// <param name="y">Координата ячейки по Y.</param>
        /// <param name="passability">Уровень проходимости.</param>
        public void SetPassability(int x, int y, int passability)
        {
            _map[x, y] = passability;
        }
        #endregion
    }

    /// <summary>
    /// Класс представляющий собой карту с возможность отображения ячеек.
    /// </summary>
    /// <remarks>
    /// Служебный тип для демонстрации работы алгоритма.
    /// </remarks>
    public class CMap2DView : CMap2D
    {
        #region Fields
        // Основные параметры
        protected internal TMapPoint _start;
        protected internal TMapPoint _target;
        protected internal int[,] _wave;
        protected internal CPath? _path;

        // Параметры отображения
        protected internal float _offsetX;
        protected internal float _offsetY;
        protected internal float _sizeCell;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
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
        /// Позиция по X начальной точки пути.
        /// </summary>
        public int StartX
        {
            get { return _start.X; }
            set
            {
                _start.X = value;
            }
        }

        /// <summary>
        /// Позиция по Y начальной точки пути.
        /// </summary>
        public int StartY
        {
            get { return _start.Y; }
            set
            {
                _start.Y = value;
            }
        }

        /// <summary>
        /// Позиция по X целевой точки пути.
        /// </summary>
        public int TargetX
        {
            get { return _target.X; }
            set
            {
                _target.X = value;
            }
        }

        /// <summary>
        /// Позиция по Y целевой точки пути.
        /// </summary>
        public int TargetY
        {
            get { return _target.Y; }
            set
            {
                _target.Y = value;
            }
        }

        /// <summary>
        /// Карта для отображения волны действия алгоритма.
        /// </summary>
        public int[,] Wave
        {
            get { return _wave; }
        }

        /// <summary>
        /// Путь.
        /// </summary>
        public CPath? Path
        {
            get { return _path; }
            set
            {
                _path = value;
            }
        }

        //
        // ПАРАМЕТРЫ ОТОБРАЖЕНИЯ
        //
        /// <summary>
        /// Смещение в экранных координатах по X.
        /// </summary>
        public float OffsetX
        {
            get { return _offsetX; }
            set
            {
                _offsetX = value;
            }
        }

        /// <summary>
        /// Смещение в экранных координатах по Y.
        /// </summary>
        public float OffsetY
        {
            get { return _offsetY; }
            set
            {
                _offsetY = value;
            }
        }

        /// <summary>
        /// Размер ячейки карты.
        /// </summary>
        public float SizeCell
        {
            get { return _sizeCell; }
            set
            {
                _sizeCell = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CMap2DView()
            : base(1, 1)
        {
            _start = TMapPoint.Undef;
            _target = TMapPoint.Undef;
            _wave = new int[1, 1];
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="mapWidth">Ширина карты.</param>
        /// <param name="mapHeight">Высота карты.</param>
        public CMap2DView(int mapWidth, int mapHeight)
            : base(mapWidth, mapHeight)
        {
            _start = TMapPoint.Undef;
            _target = TMapPoint.Undef;
            _wave = new int[mapWidth, mapHeight];
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Сброс данных карты для отображения волны действия алгоритма.
        /// </summary>
        public void ResetWave()
        {
            for (var ix = 0; ix < _mapWidth; ix++)
            {
                for (var iy = 0; iy < _mapHeight; iy++)
                {
                    _wave[ix, iy] = 0;
                }
            }
        }
        #endregion

        #region Draw methods
#if UNITY_EDITOR
		/// <summary>
		/// Рисование базовой сетки.
		/// </summary>
		public void DrawGrid()
		{
			for (Int32 ix = 0; ix < _mapWidth; ix++)
			{
				for (Int32 iy = 0; iy < _mapHeight; iy++)
				{
					UnityEngine.Rect cell = new UnityEngine.Rect();
					cell.x = mOffsetX + ix * mSizeCell;
					cell.y = mOffsetY + iy * mSizeCell;
					cell.width = mSizeCell;
					cell.height = mSizeCell;

					UnityEngine.GUI.Box(cell, "");
				}
			}
		}

		/// <summary>
		/// Рисование начальной позиции.
		/// </summary>
		/// <param name="color">Цвет ячейки.</param>
		/// <param name="text">Надпись в ячейки.</param>
		public void DrawStartPosition(UnityEngine.Color color, String text = "Str")
		{
			if (_start.X > -1 && _start.X < _mapWidth && _start.Y > -1 && _start.Y < _mapHeight)
			{
				UnityEngine.Texture2D texture_box = UnityEngine.GUI.skin.box.normal.background;
				UnityEngine.GUI.skin.box.normal.background = UnityEngine.Texture2D.whiteTexture;

				UnityEngine.Rect cell = new UnityEngine.Rect();
				cell.x = mOffsetX + _start.X * mSizeCell + 2;
				cell.y = mOffsetY + _start.Y * mSizeCell + 2;
				cell.width = mSizeCell - 4;
				cell.height = mSizeCell - 4;
				UnityEngine.GUI.backgroundColor = color;
				UnityEngine.GUI.Box(cell, text);

				UnityEngine.GUI.skin.box.normal.background = texture_box;
			}
		}

		/// <summary>
		/// Рисование конечной позиции.
		/// </summary>
		/// <param name="color">Цвет ячейки.</param>
		/// <param name="text">Надпись в ячейки.</param>
		public void DrawEndPosition(UnityEngine.Color color, String text = "End")
		{
			if (_target.X > -1 && _target.X < _mapWidth && _target.Y > -1 && _target.Y < _mapHeight)
			{
				UnityEngine.Texture2D texture_box = UnityEngine.GUI.skin.box.normal.background;
				UnityEngine.GUI.skin.box.normal.background = UnityEngine.Texture2D.whiteTexture;

				UnityEngine.Rect cell = new UnityEngine.Rect();
				cell.x = mOffsetX + _target.X * mSizeCell + 2;
				cell.y = mOffsetY + _target.Y * mSizeCell + 2;
				cell.width = mSizeCell - 4;
				cell.height = mSizeCell - 4;
				UnityEngine.GUI.backgroundColor = color;
				UnityEngine.GUI.Box(cell, text);

				UnityEngine.GUI.skin.box.normal.background = texture_box;
			}
		}

		/// <summary>
		/// Рисование карты проходимости.
		/// </summary>
		/// <param name="color">Цвет ячейки.</param>
		public void DrawPassability(UnityEngine.Color color)
		{
			UnityEngine.Texture2D texture_box = UnityEngine.GUI.skin.box.normal.background;
			UnityEngine.GUI.skin.box.normal.background = UnityEngine.Texture2D.whiteTexture;
			for (Int32 ix = 0; ix < _mapWidth; ix++)
			{
				for (Int32 iy = 0; iy < _mapHeight; iy++)
				{
					// Только если это препятствие
					if (_map[ix, iy] >= XMapCode.EMPTY)
					{
						UnityEngine.Rect cell = new UnityEngine.Rect();
						cell.x = mOffsetX + ix * mSizeCell + 2;
						cell.y = mOffsetY + iy * mSizeCell + 2;
						cell.width = mSizeCell - 4;
						cell.height = mSizeCell - 4;
						UnityEngine.GUI.backgroundColor = color;
						UnityEngine.GUI.Box(cell, _map[ix, iy].ToString());
					}
				}
			}
			UnityEngine.GUI.skin.box.normal.background = texture_box;
		}

		/// <summary>
		/// Рисование карты блокированных ячеек.
		/// </summary>
		/// <param name="color">Цвет ячейки.</param>
		public void DrawBlock(UnityEngine.Color color)
		{
			UnityEngine.Texture2D texture_box = UnityEngine.GUI.skin.box.normal.background;
			UnityEngine.GUI.skin.box.normal.background = UnityEngine.Texture2D.whiteTexture;
			for (Int32 ix = 0; ix < _mapWidth; ix++)
			{
				for (Int32 iy = 0; iy < _mapHeight; iy++)
				{
					// Только если это препятствие
					if (_map[ix, iy] == XMapCode.BLOCK)
					{
						UnityEngine.Rect cell = new UnityEngine.Rect();
						cell.x = mOffsetX + ix * mSizeCell + 2;
						cell.y = mOffsetY + iy * mSizeCell + 2;
						cell.width = mSizeCell - 4;
						cell.height = mSizeCell - 4;
						UnityEngine.GUI.backgroundColor = color;
						UnityEngine.GUI.Box(cell, _map[ix, iy].ToString());
					}
				}
			}
			UnityEngine.GUI.skin.box.normal.background = texture_box;
		}

		/// <summary>
		/// Рисование волны действия алгоритма.
		/// </summary>
		/// <param name="color">Цвет ячейки.</param>
		public void DrawWave(UnityEngine.Color color)
		{
			UnityEngine.Texture2D texture_box = UnityEngine.GUI.skin.box.normal.background;
			UnityEngine.GUI.skin.box.normal.background = UnityEngine.Texture2D.whiteTexture;
			for (Int32 ix = 0; ix < _mapWidth; ix++)
			{
				for (Int32 iy = 0; iy < _mapHeight; iy++)
				{
					// Только если это препятствие
					if (mWave[ix, iy] > 0)
					{
						UnityEngine.Rect cell = new UnityEngine.Rect();
						cell.x = mOffsetX + ix * mSizeCell + 2;
						cell.y = mOffsetY + iy * mSizeCell + 2;
						cell.width = mSizeCell - 4;
						cell.height = mSizeCell - 4;
						UnityEngine.GUI.backgroundColor = color;
						UnityEngine.GUI.Box(cell, mWave[ix, iy].ToString());
					}
				}
			}
			UnityEngine.GUI.skin.box.normal.background = texture_box;
		}

		/// <summary>
		/// Рисование пути.
		/// </summary>
		/// <param name="color">Цвет ячейки.</param>
		public void DrawPath(UnityEngine.Color color)
		{
			if (_path != null)
			{
				UnityEngine.Texture2D texture_box = UnityEngine.GUI.skin.box.normal.background;
				UnityEngine.GUI.skin.box.normal.background = UnityEngine.Texture2D.whiteTexture;
				for (Int32 i = 0; i < _path.Count; i++)
				{
					UnityEngine.Rect cell = new UnityEngine.Rect();
					cell.x = mOffsetX + _path[i].X * mSizeCell + 2;
					cell.y = mOffsetY + _path[i].Y * mSizeCell + 2;
					cell.width = mSizeCell - 4;
					cell.height = mSizeCell - 4;
					UnityEngine.GUI.backgroundColor = color;
					UnityEngine.GUI.Box(cell, i.ToString());
				}
				UnityEngine.GUI.skin.box.normal.background = texture_box;
			}
		}
#endif
        #endregion
    }
    /**@}*/
}