using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreECS Подсистема ECS
     * \ingroup Core
     * \brief Подсистема ECS - это архитектурный шаблон представления объектов как правило игрового мира. ECS состоит из
        сущностей, к которым прикреплены компоненты содержащие данные, и системами, которые работают с компонентами 
        сущностей. Компонент является носителем только данных и недолжен, как правило,содержать никакой логики.
     * @{
     */
    /// <summary>
    /// Интерфейс для определения сущности.
    /// </summary>
    public interface ILotusEcsEntity
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        /// <remarks>
        /// Является уникальным в пределах одного мира.
        /// </remarks>
        int Id { get; }

        /// <summary>
        /// Статус активности сущности.
        /// </summary>
        /// <remarks>
        /// Только активные сущности обрабатываются системами.
        /// </remarks>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Слой расположения сущности.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        byte Layer { get; set; }

        /// <summary>
        /// Тег сущности.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        byte Tag { get; set; }

        /// <summary>
        /// Группа, которой принадлежит сущность.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        byte Group { get; set; }

        /// <summary>
        /// Статус маркировки сущности.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        byte Marked { get; set; }

        /// <summary>
        /// Количество компонентов.
        /// </summary>
        int ComponentCount { get; }

        /// <summary>
        /// Статус уничтожения сущности.
        /// </summary>
        bool IsDestroyed { get; set; }
    }

    /// <summary>
    /// Сущность.
    /// </summary>
    public struct TEcsEntity : ILotusEcsEntity, IEquatable<TEcsEntity>, IComparable<TEcsEntity>
    {
        #region Fields
        // Основные параметры
        internal int _id;
        internal bool _isEnabled;
        internal byte _layer;
        internal byte _tag;
        internal byte _group;
        internal byte _marked;
        internal int _componentCount;
        internal bool _isDestroyed;
        #endregion

        #region Properties
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        /// <remarks>
        /// Является уникальным в пределах одного мира.
        /// </remarks>
        public readonly int Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Статус активности сущности.
        /// </summary>
        /// <remarks>
        /// Только активные сущности обрабатываются системами.
        /// </remarks>
        public bool IsEnabled
        {
            readonly get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
            }
        }

        /// <summary>
        /// Слой расположения сущности.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        public byte Layer
        {
            readonly get
            {
                return _layer;
            }
            set
            {
                _layer = value;
            }
        }

        /// <summary>
        /// Тег сущности.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        public byte Tag
        {
            readonly get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        /// <summary>
        /// Группа, которой принадлежит сущность.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        public byte Group
        {
            readonly get
            {
                return _group;
            }
            set
            {
                _group = value;
            }
        }

        /// <summary>
        /// Статус маркировки сущности.
        /// </summary>
        /// <remarks>
        /// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя.
        /// </remarks>
        public byte Marked
        {
            readonly get
            {
                return _marked;
            }
            set
            {
                _marked = value;
            }
        }

        /// <summary>
        /// Количество компонентов.
        /// </summary>
        public readonly int ComponentCount
        {
            get
            {
                return _componentCount;
            }
        }

        /// <summary>
        /// Статус уничтожения сущности.
        /// </summary>
        public bool IsDestroyed
        {
            readonly get
            {
                return _isDestroyed;
            }
            set
            {
                _isDestroyed = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует сущность указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        public TEcsEntity(int id)
        {
            _id = id;
            _isEnabled = true;
            _layer = 0;
            _tag = 0;
            _group = 0;
            _marked = 0;
            _marked = 0;
            _componentCount = 0;
            _isDestroyed = false;
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
            if (obj != null)
            {
                if (obj is TEcsEntity entity)
                {
                    return Equals(entity);
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства сущностей по значению.
        /// </summary>
        /// <param name="other">Сущность.</param>
        /// <returns>Статус равенства сущностей.</returns>
        public readonly bool Equals(TEcsEntity other)
        {
            return _id == other.Id;
        }

        /// <summary>
        /// Сравнение сущностей для упорядочивания.
        /// </summary>
        /// <param name="other">Сущность.</param>
        /// <returns>Статус сравнения сущностей.</returns>
        public readonly int CompareTo(TEcsEntity other)
        {
            return _id.CompareTo(other.Id);
        }

        /// <summary>
        /// Получение хеш-кода сущности.
        /// </summary>
        /// <returns>Хеш-код сущности.</returns>
        public override readonly int GetHashCode()
        {
            return _id;
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление сущности с указанием значений.</returns>
        public override readonly string ToString()
        {
            return _id.ToString();
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнение сущностей на равенство.
        /// </summary>
        /// <param name="left">Первый сущность.</param>
        /// <param name="right">Второй сущность.</param>
        /// <returns>Статус равенства сущностей.</returns>
        public static bool operator ==(TEcsEntity left, TEcsEntity right)
        {
            return left.Id == right.Id;
        }

        /// <summary>
        /// Сравнение сущностей на неравенство.
        /// </summary>
        /// <param name="left">Первый сущность.</param>
        /// <param name="right">Второй сущность.</param>
        /// <returns>Статус неравенства сущностей.</returns>
        public static bool operator !=(TEcsEntity left, TEcsEntity right)
        {
            return left.Id != right.Id;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений сущностей.
        /// </summary>
        /// <param name="left">Левый сущность.</param>
        /// <param name="right">Правый сущность.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(TEcsEntity left, TEcsEntity right)
        {
            return left.Id < right.Id;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений сущностей.
        /// </summary>
        /// <param name="left">Левый сущность.</param>
        /// <param name="right">Правый сущность.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(TEcsEntity left, TEcsEntity right)
        {
            return left.Id > right.Id;
        }
        #endregion
    }
    /**@}*/
}