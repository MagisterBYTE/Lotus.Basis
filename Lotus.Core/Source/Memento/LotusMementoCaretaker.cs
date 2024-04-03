namespace Lotus.Core
{
    /** \addtogroup CoreMemento
	*@{*/
    /// <summary>
    /// Интерфейс смотрителя за объектом.
    /// </summary>
    /// <remarks>
    /// Смотритель может сохранить состояние объекта и восстановить его.
    /// </remarks>
    public interface ILotusMementoCaretaker
    {
        #region Properties
        /// <summary>
        /// Объект состояние которого сохраняется и восстанавливается.
        /// </summary>
        ILotusMementoOriginator MementoOriginator { get; set; }
        #endregion

        #region Methods 
        /// <summary>
        /// Сохранить состояние объекта.
        /// </summary>
        /// <param name="stateName">Наименование состояния объекта.</param>
        void SaveState(string stateName);

        /// <summary>
        /// Восстановить состояние объекта.
        /// </summary>
        /// <param name="stateName">Наименование состояния объекта.</param>
        void RestoreState(string stateName);
        #endregion
    }

    /// <summary>
    /// Класс реализующий смотрителя за объектом.
    /// </summary>
    public class CMementoCaretaker : ILotusMementoCaretaker
    {
        #region Fields
        // Общие данные
        protected internal ILotusMementoOriginator _originator;
        protected internal object _state;
        #endregion

        #region Properties
        /// <summary>
        /// Объект состояние которого сохраняется и восстанавливается.
        /// </summary>
        public ILotusMementoOriginator MementoOriginator
        {
            get { return _originator; }
            set { _originator = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CMementoCaretaker()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="originator">Объект.</param>
        /// <param name="stateName">Наименование состояния объекта.</param>
        public CMementoCaretaker(ILotusMementoOriginator originator, string stateName)
        {
            _originator = originator;
            _state = originator.GetMemento(stateName);
        }
        #endregion

        #region Main methods 
        /// <summary>
        /// Сохранить состояние объекта.
        /// </summary>
        /// <param name="stateName">Наименование состояния объекта.</param>
        public virtual void SaveState(string stateName)
        {
            _state = _originator.GetMemento(stateName);
        }

        /// <summary>
        /// Восстановить состояние объекта.
        /// </summary>
        /// <param name="stateName">Наименование состояния объекта.</param>
        public virtual void RestoreState(string stateName)
        {
            _originator.SetMemento(_state, stateName);
        }
        #endregion
    }

    /// <summary>
    /// Класс реализующий смотрителя за объектом с поддержкой отмены/повторения действий.
    /// </summary>
    public class CMementoCaretakerChanged : ILotusMementoCaretaker, ILotusMementoState
    {
        #region Fields
        // Общие данные
        protected internal ILotusMementoOriginator _originator;
        protected internal object _beforeState;
        protected internal object _afterState;
        protected internal string _stateName;
        #endregion

        #region Properties
        /// <summary>
        /// Объект состояние которого сохраняется и восстанавливается.
        /// </summary>
        public ILotusMementoOriginator MementoOriginator
        {
            get { return _originator; }
            set { _originator = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        /// <remarks>
        /// Конструктор без параметров запрещен.
        /// </remarks>
        private CMementoCaretakerChanged()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="originator">Объект.</param>
        /// <param name="stateName">Наименование состояния объекта.</param>
        public CMementoCaretakerChanged(ILotusMementoOriginator originator, string stateName)
        {
            _originator = originator;
            _beforeState = originator.GetMemento(stateName);
            _stateName = stateName;
        }
        #endregion

        #region Main methods 
        /// <summary>
        /// Сохранить состояние объекта.
        /// </summary>
        /// <param name="stateName">Наименование состояния объекта.</param>
        public virtual void SaveState(string stateName)
        {
            _beforeState = _originator.GetMemento(stateName);
        }

        /// <summary>
        /// Восстановить состояние объекта.
        /// </summary>
        /// <param name="stateName">Наименование состояния объекта.</param>
        public virtual void RestoreState(string stateName)
        {
            _originator.SetMemento(_beforeState, stateName);
        }
        #endregion

        #region ILotusMementoState methods
        /// <summary>
        /// Отмена последнего действия.
        /// </summary>
        public virtual void Undo()
        {
            if (_originator != null)
            {
                // Сначала сохраняем актуальное значение
                _afterState = _originator.GetMemento(_stateName);

                // Теперь ставим предыдущие
                _originator.SetMemento(_beforeState, _stateName);
            }
        }

        /// <summary>
        /// Повторение последнего действия.
        /// </summary>
        public virtual void Redo()
        {
            if (_originator != null)
            {
                // Сначала сохраняем актуальное значение
                _beforeState = _originator.GetMemento(_stateName);

                // Теперь ставим предыдущие
                _originator.SetMemento(_afterState, _stateName);
            }
        }
        #endregion
    }
    /**@}*/
}