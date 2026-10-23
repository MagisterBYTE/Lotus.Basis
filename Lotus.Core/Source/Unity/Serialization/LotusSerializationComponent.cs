#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
using UnityEngine.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreUnitySerialization
    *@{*/
    /// <summary>
    /// Компонент сериализации данных
    /// </summary>
    /// <remarks>
    /// Реализация компонента предназначенного для сохранения данных стандартных компонентов Unity и пользовательских компонентов.
    /// Определяет игровой объект как сохраняемый и позволяет задать объем сохраняемых данных
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Serialization/Serialization Component")]
    public class LotusSerializationComponent : MonoBehaviour, IComparable<LotusSerializationComponent>
    {
        #region Fields
        // Данные подсистемы сериализации
        [SerializeField]
        [FormerlySerializedAs("mIDKeySerial")]
        internal long _idKeySerial = -1;
        [SerializeField]
        [FormerlySerializedAs("mSerializationVolume")]
        internal TSerializationVolume _serializationVolume;
        [SerializeField]
        [FormerlySerializedAs("mIsSerializablePrefab")]
        internal bool _isSerializablePrefab;
        [SerializeField]
        [FormerlySerializedAs("mIsSerializableChild")]
        internal bool _isSerializableChild;

        // Служебные данные
        internal int _statusLoad;
        #endregion

        #region Properties
        /// <summary>
        /// Сериализуемый ключ объекта
        /// </summary>
        /// <remarks>
        /// Должен оставаться постоянным и уникальным на всей протяженности жизни объекта.
        /// В случае если объект создается повторно (вследствие загрузки данных) то ключ должен инициализируется существующим значением
        /// </remarks>
        public long IDKeySerial
        {
            get { return _idKeySerial; }
            set { _idKeySerial = value; }
        }

        /// <summary>
        /// Объем и тип сериализации данных игрового объекта
        /// </summary>
        public TSerializationVolume SerializationVolume
        {
            get { return _serializationVolume; }
            set { _serializationVolume = value; }
        }

        /// <summary>
        /// Статус созданного из префаба игрового объекта
        /// </summary>
        /// <remarks>
        /// Если объект создан из префаба, то не имеет смысл хранить всего компоненты.
        /// В большинстве случаев достаточно сохранять только компонент трансформации непосредственно в атрибутах игрового объекта
        /// </remarks>
        public bool IsSerializablePrefab
        {
            get { return _isSerializablePrefab; }
            set { _isSerializablePrefab = value; }
        }

        /// <summary>
        /// Статус сохранения дочерних игровых объектов
        /// </summary>
        /// <remarks>
        /// По умолчанию сохраняется только игровой объект и определённых набор компонентов.
        /// Включение свойства означает что сохранятся вся дочерняя иерархия игровых объектов
        /// </remarks>
        public bool IsSerializableChild
        {
            get { return _isSerializableChild; }
            set { _isSerializableChild = value; }
        }

        /// <summary>
        /// Статус загрузки/обновления объекта
        /// </summary>
        public int StatusLoad
        {
            get { return _statusLoad; }
            set { _statusLoad = value; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        public void Reset()
        {
            Init();
        }

        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
            Init();
            // XSerializationDispatcherUnity.RegisterSerialization(this);
        }

        /// <summary>
        /// Игровой объект уничтожился
        /// </summary>
        public void OnDestroy()
        {
            // XSerializationDispatcherUnity.UnregisterSerialization(this);
        }
        #endregion

        #region System methods
        /// <summary>
        /// Сравнения двух компонентов сериализации для формирования правильного последовательности сериализации
        /// </summary>
        /// <param name="other">Компонент сериализации</param>
        /// <returns>Статус сравнения</returns>
        public int CompareTo(LotusSerializationComponent other)
        {
            var depthThis = transform.AbsoluteDepth();
            var depthOther = other.transform.AbsoluteDepth();

            if (depthThis > depthOther)
            {
                return 1;
            }
            else
            {
                if (depthThis < depthOther)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Первичная инициализация данных и параметров компонента
        /// </summary>
        public void Init()
        {
            if (_idKeySerial == -1)
            {
                _idKeySerial = XGenerateId.Generate(this);
            }
        }
        #endregion
    }

    /**@}*/
}
#endif
