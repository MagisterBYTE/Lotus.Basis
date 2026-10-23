#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
using UnityEngine.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityInspector
    *@{*/
    /// <summary>
    /// Атрибут для определения поддерживаемого типа интерфейса
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class LotusInterfaceComponentAttribute : PropertyAttribute
    {
        #region Fields
        internal Type _typeInterface;
        #endregion

        #region Properties
        /// <summary>
        /// Тип интерфейса
        /// </summary>
        public Type TypeInterface
        {
            get { return _typeInterface; }
            set { _typeInterface = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="typeInterface">Тип интерфейса</param>
        public LotusInterfaceComponentAttribute(Type typeInterface)
        {
            _typeInterface = typeInterface;
        }
        #endregion
    }


    /// <summary>
    /// Класс оболочка для хранения скрипта и типа интерфейса который он поддерживает
    /// </summary>
    public class CInterfaceComponent
    {
        #region Fields
        [SerializeField]
        [FormerlySerializedAs("mScript")]
        internal MonoBehaviour _script;
        [SerializeField]
        [FormerlySerializedAs("mTypeInterface")]
        internal Type _typeInterface;
        #endregion

        #region Properties
        /// <summary>
        /// Тип интерфейса
        /// </summary>
        public Type TypeInterface
        {
            get { return _typeInterface; }
            set { _typeInterface = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="typeInterface">Тип интерфейса</param>
        public CInterfaceComponent(Type typeInterface)
        {
            _typeInterface = typeInterface;
        }
        #endregion
    }


    /// <summary>
    /// Класс оболочка для хранения скрипта и типа интерфейса который он поддерживает
    /// </summary>
    /// <typeparam name="TInterface">Тип интерфейса</typeparam>
    public class InterfaceComponent<TInterface> : CInterfaceComponent
    {
        #region Fields
        [NonSerialized]
        internal TInterface _interface;
        #endregion

        #region Properties
        /// <summary>
        /// Интерфейс
        /// </summary>
        public TInterface Interface
        {
            get { return _interface; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public InterfaceComponent()
            : base(typeof(TInterface))
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="script">Пользовательский скрипт реализующий интерфейс</param>
        public InterfaceComponent(MonoBehaviour script)
            : base(typeof(TInterface))
        {
            _script = script;
        }
        #endregion
    }

    /**@}*/
}
#endif
