using System;
#if !UNITY_2017_1_OR_NEWER
using System.Numerics;
#endif

namespace Lotus.Core
{
    /** \addtogroup CoreCommonTypes
	*@{*/
    /// <summary>
    /// Класс реализующий поверхность как двухмерный массив данных.
    /// </summary>
    /// <remarks>
    /// Одна из основных функций поверхности - интерполяция данных при изменении ее размеров.
    /// </remarks>
    /// <typeparam name="TNumber">Тип данных поверхности.</typeparam>
    public class SurfaceData<TNumber> where TNumber : struct, IEquatable<TNumber>
#if !UNITY_2017_1_OR_NEWER
        , IAdditionOperators<TNumber, TNumber, TNumber>,
        ISubtractionOperators<TNumber, TNumber, TNumber>,
        IMultiplyOperators<TNumber, TNumber, TNumber>
#endif
    {
        #region Fields
        protected internal TNumber[] _data;
        protected internal int _width;
        protected internal int _height;
        protected internal int _rank;
        #endregion

        #region Properties
        /// <summary>
        /// Данные поверхности.
        /// </summary>
        public TNumber[] Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Количество элементов в поверхности.
        /// </summary>
        public int Count
        {
            get { return _data.Length; }
        }

        /// <summary>
        /// Ширина поверхности при двухмерной организации данных.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Высота поверхности при двухмерной организации данных.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Размерность поверхности.
        /// </summary>
        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public SurfaceData()
        {
            _data = [default];
            _width = 1;
            _height = 1;
            _rank = 1;
        }

        /// <summary>
        /// Конструктор инициализирует данные поверхности указанными значениями.
        /// </summary>
        /// <param name="data">Данные.</param>
        public SurfaceData(TNumber[] data)
        {
            _data = new TNumber[data.Length];
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = data[i];
            }
            _rank = 1;
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация элементов поверхности.
        /// </summary>
        /// <param name="index">Индекс элемента.</param>
        /// <returns>Элемент поверхности.</returns>
        public TNumber this[int index]
        {
            get
            {
                return _data[index];
            }
            set
            {
                _data[index] = value;
            }
        }

        /// <summary>
        /// Индексация элементов поверхности при двухмерной организации.
        /// </summary>
        /// <param name="x">Индекс элемента по x.</param>
        /// <param name="y">Индекс элемента по y.</param>
        /// <returns>Элемент поверхности.</returns>
        public TNumber this[int x, int y]
        {
            get
            {
                return _data[x + (y * _width)];
            }
            set
            {
                _data[x + (y * _width)] = value;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установка данных.
        /// </summary>
        /// <param name="count">Количество элементов.</param>
        /// <param name="saveOldData">Сохранять ли старые данные.</param>
        public void Set(int count, bool saveOldData)
        {
            if (saveOldData)
            {
                var data = new TNumber[count];
                var countCopy = count;
                if (count > Count)
                {
                    countCopy = Count;
                }
                for (var i = 0; i < countCopy; i++)
                {
                    data[i] = _data[i];
                }

                _data = data;
            }
            else
            {
                _data = new TNumber[count];
            }
        }

        /// <summary>
        /// Установка данных из источника.
        /// </summary>
        /// <param name="data">Данные.</param>
        public void SetFromData(TNumber[] data)
        {
            _data = new TNumber[data.Length];
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = data[i];
            }
        }

        /// <summary>
        /// Линейная интерполяция значений поверхности.
        /// </summary>
        /// <param name="newWidth">Новая ширина.</param>
        /// <param name="newHeight">Новая высота.</param>
        public void Resize(int newWidth, int newHeight)
        {
            var tempData = new TNumber[newWidth * newHeight];

            var factorX = _width / (double)newWidth;
            var factorY = _height / (double)newHeight;

            for (var x = 0; x < newWidth; ++x)
            {
                for (var y = 0; y < newHeight; ++y)
                {
                    var gx = (int)Math.Floor(x * factorX);
                    var gy = (int)Math.Floor(y * factorY);
                    var val = this[gx, gy];
                    tempData[x + (y * newWidth)] = val;
                }
            }

            _data = tempData;
            _width = newWidth;
            _height = newHeight;
            _rank = 2;
        }

        /// <summary>
        /// Линейная интерполяция значений поверхности.
        /// </summary>
        /// <param name="newWidth">Новая ширина.</param>
        /// <param name="newHeight">Новая высота.</param>
        /// <param name="useBilinear">Использовать билинейную интерполяцию.</param>
        public void Resize(int newWidth, int newHeight, bool useBilinear)
        {
            var tempData = new TNumber[newWidth * newHeight];

            var factorX = _width / (double)newWidth;
            var factorY = _height / (double)newHeight;

#if UNITY_2017_1_OR_NEWER
            _ = useBilinear; // Bilinear unavailable without generic math operators.
#endif

#if !UNITY_2017_1_OR_NEWER
            if (useBilinear)
            {
                double fractionX, fractionY, oneMinusX, oneMinusY;
                int ceilX, ceilY, floorX, floorY;
                TNumber c1, c2, c3, c4;
                TNumber b1, b2;

                for (var x = 0; x < newWidth; ++x)
                {
                    for (var y = 0; y < newHeight; ++y)
                    {
                        // Setup
                        floorX = (int)Math.Floor(x * factorX);
                        floorY = (int)Math.Floor(y * factorY);

                        ceilX = floorX + 1;
                        if (ceilX >= _width)
                        {
                            ceilX = floorX;
                        }

                        ceilY = floorY + 1;
                        if (ceilY >= _height)
                        {
                            ceilY = floorY;
                        }

                        fractionX = (x * factorX) - floorX;
                        fractionY = (y * factorY) - floorY;
                        oneMinusX = 1.0 - fractionX;
                        oneMinusY = 1.0 - fractionY;

                        c1 = this[floorX, floorY];
                        c2 = this[ceilX, floorY];
                        c3 = this[floorX, ceilY];
                        c4 = this[ceilX, ceilY];

                        b1 = (GetScale(c1, oneMinusX) + GetScale(c2, fractionX));
                        b2 = (GetScale(c3, oneMinusX) + GetScale(c4, fractionX));
                        var val = (GetScale(b1, oneMinusY) + GetScale(b2, fractionY));

                        tempData[x + (y * newWidth)] = val;
                    }
                }
            }
            else
#endif
            {
                // Under Unity bilinear path is unavailable (no generic math operators).
                for (var x = 0; x < newWidth; ++x)
                {
                    for (var y = 0; y < newHeight; ++y)
                    {
                        var gx = (int)Math.Floor(x * factorX);
                        var gy = (int)Math.Floor(y * factorY);
                        var val = this[gx, gy];
                        tempData[x + (y * newWidth)] = val;
                    }
                }
            }

            _data = tempData;
            _width = newWidth;
            _height = newHeight;
            _rank = 2;
        }

        /// <summary>
        /// Получить масштабированное значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="scale">Масштаб.</param>
        /// <returns>Масштабированное значение.</returns>
        protected virtual TNumber GetScale(TNumber value, double scale)
        {
            return value;
        }
        #endregion
    }

    /// <summary>
    /// Поверхность данных для целых значения.
    /// </summary>
    public class CSurfaceInt : SurfaceData<int>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public CSurfaceInt()
        {

        }

        /// <summary>
        /// Конструктор инициализирует данные поверхности указанными значениями.
        /// </summary>
        /// <param name="data">Данные.</param>
        public CSurfaceInt(int[] data)
        {
            SetFromData(data);
            _rank = 1;
        }

        #endregion

        #region Main methods
        /// <summary>
        /// Получить масштабированное значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="scale">Масштаб.</param>
        /// <returns>Масштабированное значение.</returns>
        protected override int GetScale(int value, double scale)
        {
            return (int)(value * scale);
        }
        #endregion
    }

    /// <summary>
    /// Поверхность данных для цвета.
    /// </summary>
    public class CSurfaceColor : SurfaceData<TColor>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public CSurfaceColor()
        {

        }

        /// <summary>
        /// Конструктор инициализирует данные поверхности указанными значениями.
        /// </summary>
        /// <param name="data">Данные.</param>
        public CSurfaceColor(TColor[] data)
        {
            SetFromData(data);
            _rank = 1;
        }

#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями.
			/// </summary>
			/// <param name="data">Данные.</param>
			public CSurfaceColor(UnityEngine.Color32[] data)
			{
				SetFromData(data);
				_rank = 1;
			}

			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями.
			/// </summary>
			/// <param name="data">Данные.</param>
			public CSurfaceColor(UnityEngine.Texture2D texture)
			{
				if (texture != null)
				{
					this.SetFromData(texture.GetPixels32());
					_width = texture.width;
					_height = texture.height;
					_rank = 2;
				}
				else
				{
					_data = new TColor[] { TColor.FromBGRA(0xFFFFFFFF) };
					_width = 1;
					_height = 1;
					_rank = 1;
				}
			}
#endif
        #endregion

        #region Main methods
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Установка данных из источника.
		/// </summary>
		/// <param name="data">Данные.</param>
		public void SetFromData(UnityEngine.Color32[] data)
		{
			_data = new TColor[data.Length];
			for (var i = 0; i < _data.Length; i++)
			{
				_data[i].A = data[i].a;
				_data[i].R = data[i].r;
				_data[i].G = data[i].g;
				_data[i].B = data[i].b;
			}
		}

		/// <summary>
		/// Установка данных из источника (текстуры).
		/// </summary>
		/// <param name="texture">Текстура.</param>
		public void SetFromData(UnityEngine.Texture2D texture)
		{
			if (texture != null)
			{
				this.SetFromData(texture.GetPixels32());
				_width = texture.width;
				_height = texture.height;
				_rank = 2;
			}
		}
#endif
        #endregion

        #region Main methods
        /// <summary>
        /// Получить масштабированное значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="scale">Масштаб.</param>
        /// <returns>Масштабированное значение.</returns>
        protected override TColor GetScale(TColor value, double scale)
        {
            return (value * scale);
        }
        #endregion
    }
    /**@}*/
}