using System;
using System.Collections.Generic;

using Lotus.Core;

namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmTile
	*@{*/
    /// <summary>
    /// Интерфейс определения поля для размещения тайлов.
    /// </summary>
    public interface ILotusField
    {
        #region Properties
        /// <summary>
        /// Событие нажатие на ячейку поля.
        /// </summary>
        Action<ILotusFieldCell>? OnCellDown { get; set; }

        /// <summary>
        /// Событие отпускание ячейки поля.
        /// </summary>
        Action<ILotusFieldCell>? OnCellUp { get; set; }

        /// <summary>
        /// Событие щелчок по ячейки поля.
        /// </summary>
        Action<ILotusFieldCell>? OnCellClick { get; set; }
        #endregion

        #region МЕТОДЫ 
        /// <summary>
        /// Получить ячейку поля по координатам.
        /// </summary>
        /// <param name="x">Координата ячейки по X.</param>
        /// <param name="y">Координата ячейки по Y.</param>
        /// <returns>Ячейка поля.</returns>
        ILotusFieldCell? GetCell(int x, int y);

        /// <summary>
        /// Споско ячеек.
        /// </summary>
        IList<ILotusFieldCell> ICells { get; }
        #endregion
    }

    /// <summary>
    /// Базовый класс реализующий функциональность поля для размещения тайлов.
    /// </summary>
    /// <typeparam name="TCell">Тип ячейки поля.</typeparam>
    public class FieldBaseTemplate<TCell> : ILotusField where TCell : ILotusFieldCell
    {
        #region Fields
        protected internal ListArray<TCell> _cells;
        #endregion

        #region Properties
        /// <summary>
        /// Все ячейки поля.
        /// </summary>
        public IList<ILotusFieldCell> ICells
        {
            get { return (IList<ILotusFieldCell>)_cells; }
        }

        /// <summary>
        /// Все ячейки поля.
        /// </summary>
        public ListArray<TCell> Cells
        {
            get { return _cells; }
        }

        /// <summary>
        /// Событие нажатие на ячейку поля.
        /// </summary>
        public Action<ILotusFieldCell>? OnCellDown { get; set; }

        /// <summary>
        /// Событие отпускание ячейки поля.
        /// </summary>
        public Action<ILotusFieldCell>? OnCellUp { get; set; }

        /// <summary>
        /// Событие щелчок по ячейки поля.
        /// </summary>
        public Action<ILotusFieldCell>? OnCellClick { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public FieldBaseTemplate()
        {
            _cells = new ListArray<TCell>();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public FieldBaseTemplate(int capacity)
        {
            _cells = new ListArray<TCell>(capacity);
        }
        #endregion

        #region System methods

        #endregion

        #region ILotusFieldCell methods
        /// <summary>
        /// Получить ячейку поля по координатам.
        /// </summary>
        /// <param name="x">Координата ячейки по X.</param>
        /// <param name="y">Координата ячейки по Y.</param>
        /// <returns></returns>
        public ILotusFieldCell? GetCell(int x, int y)
        {
            for (var i = 0; i < _cells.Count; i++)
            {
                if (_cells[i].CellCoordinateX == x && _cells[i].CellCoordinateY == y)
                {
                    return _cells[i];
                }
            }

            return null;
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс реализующий функциональность поля для размещения тайлов.
    /// </summary>
    public class CFieldBase : FieldBaseTemplate<FieldCellBase>
    {
        #region Static methods
        /// <summary>
        /// Создание прямоугольного поля.
        /// </summary>
        /// <remarks>
        /// Порядок ячеек
        /// 0,1,2,3,4
        /// 5,6,7,8,9
        /// </remarks>
        /// <param name="countX">Размер поля по X.</param>
        /// <param name="countY">Размер поля по Y.</param>
        /// <returns>Прямоугольное поле.</returns>
        public static CFieldBase CreateSquare(int countX, int countY)
        {
            var fieldBase = new CFieldBase(countX * countY);
            for (var y = 0; y < countY; y++)
            {
                for (var x = 0; x < countX; x++)
                {
                    fieldBase.Cells.Add(new FieldCellBase(x, y, fieldBase));
                }
            }

            return fieldBase;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CFieldBase()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public CFieldBase(int capacity)
            : base(capacity)
        {
        }
        #endregion
    }
    /**@}*/
}