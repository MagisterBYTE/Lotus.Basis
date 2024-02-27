namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmTile
	*@{*/
    /// <summary>
    /// Интерфейс для определения ячейки поля.
    /// </summary>
    public interface ILotusFieldCell
    {
        /// <summary>
        /// Владелец - поле которому принадлежат ячейка.
        /// </summary>
        ILotusField? OwnerField { get; set; }

        /// <summary>
        /// Слой для расположения ячейки.
        /// </summary>
        int CellLayer { get; set; }

        /// <summary>
        /// Координата ячейки по X.
        /// </summary>
        int CellCoordinateX { get; set; }

        /// <summary>
        /// Координата ячейки по Y.
        /// </summary>
        int CellCoordinateY { get; set; }

        /// <summary>
        /// Наличие границы у ячейки поля слева.
        /// </summary>
        bool IsCellBorderLeft { get; set; }

        /// <summary>
        /// Наличие границы у ячейки поля справа.
        /// </summary>
        bool IsCellBorderRight { get; set; }

        /// <summary>
        /// Наличие границы у ячейки поля сверху.
        /// </summary>
        bool IsCellBorderUp { get; set; }

        /// <summary>
        /// Наличие границы у ячейки поля снизу.
        /// </summary>
        bool IsCellBorderDown { get; set; }

        /// <summary>
        /// Статус ячейки.
        /// </summary>
        int CellStatus { get; set; }

        /// <summary>
        /// Смежная ячейка расположенная слева.
        /// </summary>
        ILotusFieldCell? CellLeft { get; }

        /// <summary>
        /// Смежная ячейка расположенная справа.
        /// </summary>
        ILotusFieldCell? CellRight { get; }

        /// <summary>
        /// Смежная ячейка расположенная сверху.
        /// </summary>
        ILotusFieldCell? CellTop { get; }

        /// <summary>
        /// Смежная ячейка расположенная снизу.
        /// </summary>
        ILotusFieldCell? CellBottom { get; }

        /// <summary>
        /// Элемент визуализации для отображения ячейки поля.
        /// </summary>
        object? VisualElement { get; set; }
    }

    /// <summary>
    /// Базовый класс реализующий функциональность ячейки поля.
    /// </summary>
    public class FieldCellBase : ILotusFieldCell
    {
        #region Fields
        protected internal ILotusField? _ownerField;
        protected internal int _cellLayer;
        protected internal int _cellCoordinateX;
        protected internal int _cellCoordinateY;
        protected internal bool _isCellBorderLeft;
        protected internal bool _isCellBorderRight;
        protected internal bool _isCellBorderUp;
        protected internal bool _isCellBorderDown;
        protected internal int _cellStatus;
        protected internal ILotusFieldCell? _cellLeft;
        protected internal ILotusFieldCell? _cellRight;
        protected internal ILotusFieldCell? _cellTop;
        protected internal ILotusFieldCell? _cellBottom;
        protected internal object? _visualElement;
        #endregion

        #region Properties
        /// <summary>
        /// Владелец - поле которому принадлежат ячейка.
        /// </summary>
        public ILotusField? OwnerField
        {
            get { return _ownerField; }
            set { _ownerField = value; }
        }

        /// <summary>
        /// Слой для расположения ячейки.
        /// </summary>
        public int CellLayer
        {
            get { return _cellLayer; }
            set { _cellLayer = value; }
        }

        /// <summary>
        /// Координата ячейки по X.
        /// </summary>
        public int CellCoordinateX
        {
            get { return _cellCoordinateX; }
            set { _cellCoordinateX = value; }
        }

        /// <summary>
        /// Координата ячейки по Y.
        /// </summary>
        public int CellCoordinateY
        {
            get { return _cellCoordinateY; }
            set { _cellCoordinateY = value; }
        }

        /// <summary>
        /// Наличие границы у ячейки поля слева.
        /// </summary>
        public bool IsCellBorderLeft
        {
            get { return _isCellBorderLeft; }
            set { _isCellBorderLeft = value; }
        }

        /// <summary>
        /// Наличие границы у ячейки поля справа.
        /// </summary>
        public bool IsCellBorderRight
        {
            get { return _isCellBorderRight; }
            set { _isCellBorderRight = value; }
        }

        /// <summary>
        /// Наличие границы у ячейки поля сверху.
        /// </summary>
        public bool IsCellBorderUp
        {
            get { return _isCellBorderUp; }
            set { _isCellBorderUp = value; }
        }

        /// <summary>
        /// Наличие границы у ячейки поля снизу.
        /// </summary>
        public bool IsCellBorderDown
        {
            get { return _isCellBorderDown; }
            set { _isCellBorderDown = value; }
        }

        /// <summary>
        /// Статус ячейки.
        /// </summary>
        public int CellStatus
        {
            get { return _cellStatus; }
            set { _cellStatus = value; }
        }

        /// <summary>
        /// Смежная ячейка расположенная слева.
        /// </summary>
        public ILotusFieldCell? CellLeft
        {
            get { return _cellLeft; }
        }

        /// <summary>
        /// Смежная ячейка расположенная справа.
        /// </summary>
        public ILotusFieldCell? CellRight
        {
            get { return _cellRight; }
        }

        /// <summary>
        /// Смежная ячейка расположенная сверху.
        /// </summary>
        public ILotusFieldCell? CellTop
        {
            get { return _cellTop; }
        }

        /// <summary>
        /// Смежная ячейка расположенная снизу.
        /// </summary>
        public ILotusFieldCell? CellBottom
        {
            get { return _cellBottom; }
        }

        /// <summary>
        /// Элемент визуализации для отображения ячейки поля.
        /// </summary>
        public object? VisualElement
        {
            get { return _visualElement; }
            set { _visualElement = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public FieldCellBase()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="cellCoordinateX">Координата ячейки по X.</param>
        /// <param name="cellCoordinateY">Координата ячейки по Y.</param>
        /// <param name="ownerField">Владелец - поле которому принадлежат ячейка.</param>
        public FieldCellBase(int cellCoordinateX, int cellCoordinateY, ILotusField ownerField)
        {
            _cellCoordinateX = cellCoordinateX;
            _cellCoordinateY = cellCoordinateY;
            _ownerField = ownerField;
            _cellLeft = _ownerField.GetCell(_cellCoordinateX - 1, _cellCoordinateY);
            _cellRight = _ownerField.GetCell(_cellCoordinateX + 1, _cellCoordinateY);
            _cellTop = _ownerField.GetCell(_cellCoordinateX, _cellCoordinateY + 1);
            _cellBottom = _ownerField.GetCell(_cellCoordinateX, _cellCoordinateY - 1);
        }
        #endregion

        #region System methods

        #endregion
    }
    /**@}*/
}