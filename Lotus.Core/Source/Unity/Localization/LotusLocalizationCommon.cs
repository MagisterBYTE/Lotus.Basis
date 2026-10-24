#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityLocalization Подсистема локализации Unity
     * \ingroup Core
     * \brief Подсистема локализация обеспечивает предоставление ресурсов и управление ими на различных языках.
        Основной тип ресурса - это текстовая информация, хотя локализованными могут быть и изображение, и звуковые ресурсы.
        Подсистема локализации достаточно гибкая и использует различные концепции и методики для локализации текстовых
        ресурсов(использование интерфейса для локализации, атрибутов и общего использования подсистемы).
        Возможности подсистемы включают автоматическое формирование текста для локализации из необходимых
        объектов на уровне сцены, автоматический машинный перевод и удобное редактирования текстовых данных.
     * \details
        //## Возможности/особенности
        1. Гибкое использование подсистемы через атрибуты, интерфейс или специальный тип
        2. Использование подсистемы с ключом локализации или без него
        3. Автоматическое получение всех локализуемых данных на уровне сцены
        4. Возможность автоматического перевода с помощью web сервиса Yandex.Translate
        5. Интегрирована в модуль Graphics2D

        //## Описание
        Все данные локализации хранятся в текстовых файлах определенной структуры. В качестве разделителя данных используется
        символы ## - это позволяет хранить несколько строк как единый объект. Если имеется ключ локализации то он записывается
        в квадратных скобках после разделителя. В в центральном диспетчере локализации \ref Lotus.Core.LotusLocalizationDispatcher
        происходит загрузка в словарь (если поддерживается ключ локализации) или в список.

        //## Использование
        1. Работа ведется в режиме редактора с помощью диспетчера локализации \ref Lotus.Core.LotusLocalizationDispatcher.
        2. Создайте/получите все необходимые данные с поддержкой локализации любым удобным способом.
        3. Установите язык по умолчанию(исходный), получите все данные локализации и сохраните файл.
        4. Создайте копию файла и переведите содержимое файла на любой язык не нарушая его структуры или воспользуетесь автоматическим переводом.
        5. Добавьте данные в диспетчер и укажите язык переведенного файла.
     * @{
     */
    /// <summary>
    /// Тип ресурса который поддерживает локализацию
    /// </summary>
    public enum TLocalizeResource
    {
        /// <summary>
        /// Текстовые данные
        /// </summary>
        Text,

        /// <summary>
        /// Графические данные
        /// </summary>
        Image,

        /// <summary>
        /// Звуковые данные
        /// </summary>
        Audio
    }


    /// <summary>
    /// Базовый интерфейс для локализации
    /// </summary>
    /// <remarks>
    /// Указанный интерфейс предназначен для базового определения статуса локализации объекта и
    /// информирование о процессе локализации (смене языка).
    /// Интерфейс не определяет формат и методы хранения данных. Это все на усмотрение и реализацию соответствующего типа
    /// </remarks>
    public interface ILotusLocalizableBase
    {
        #region Main methods
        /// <summary>
        /// Установка локализованных данных
        /// </summary>
        /// <remarks>
        /// Это основной метод который будет вызываться у элемента для установки локализованных данных. Метод должен/будет
        /// вызываться при смене активного языка
        /// </remarks>
        void SetLocalizableData();
        #endregion
    }


    /// <summary>
    /// Определения интерфейса для локализации текста
    /// </summary>
    /// <remarks>
    /// Реализация данного интерфейса предназначено в основном для объектов которые представляют одну логическую
    /// единицу текста для локализации
    /// </remarks>
    public interface ILotusLocalizable : ILotusLocalizableBase
    {
        #region Properties
        /// <summary>
        /// Текст
        /// </summary>
        string TextLocalize { get; set; }

        /// <summary>
        /// Ключ локализации
        /// </summary>
        /// <remarks>
        /// Определяет ключ для текста в подсистеме локализации.
        /// Если значение -1 то данный объект не входит в подсистему локализации.
        /// Если ноль то используется поиск на основе порядкового номера(индекса) текста (это линейный поиск
        /// и соответственно это медленно зато не требует никаких дополнительных данных).
        /// Значение отличное нуля и -1 означает что это уникальный ключ в словаре и, следовательно, используется быстрый поиск.
        /// По умолчанию значение ключа локализации формируется на основании хэш-кода исходного текста
        /// </remarks>
        int IDKeyLocalize { set; get; }
        #endregion
    }


    /// <summary>
    /// Класс-оболочка для текста поддерживающего локализацию
    /// </summary>
    /// <remarks>
    /// Вспомогательный тип поддерживающей сериализацию на уровне Unity
    /// </remarks>
    [Serializable]
    public struct TLocalizableText : IEquatable<TLocalizableText>, IComparable<TLocalizableText>, ICloneable
    {
        #region Fields
        /// <summary>
        /// Текст для локализации
        /// </summary>
        public string Text;

        /// <summary>
        /// Ключ локализации
        /// </summary>
        /// <remarks>
        /// Определяет ключ для текста в подсистеме локализации.
        /// Если значение -1 то данный объект не входит в подсистему локализации.
        /// Если ноль то используется поиск на основе порядкового номера(индекса) текста (это линейный поиск
        /// и соответственно это медленно зато не требует никаких дополнительных данных).
        /// Значение отличное нуля и -1 означает что это уникальный ключ в словаре и, следовательно, используется быстрый поиск.
        /// По умолчанию значение ключа локализации формируется на основании хэш-кода исходного текста
        /// </remarks>
        public int IDKeyLocalize;
        #endregion

        #region Properties
        /// <summary>
        /// Статус локализации
        /// </summary>
        /// <remarks>
        /// Соответственно если значение на ключа локализации не равно -1 то значит текст поддерживает локализацию
        /// </remarks>
        public bool IsLocalize
        {
            get { return IDKeyLocalize != -1; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="text">Текст для локализации</param>
        public TLocalizableText(string text)
        {
            IDKeyLocalize = 0;
            Text = text;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="text">Текст для локализации</param>
        /// <param name="id">Ключ локализации</param>
        public TLocalizableText(string text, int id)
        {
            IDKeyLocalize = id;
            Text = text;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>Статус равенства объектов</returns>
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (typeof(TLocalizableText) == obj.GetType())
                {
                    var text = (TLocalizableText)obj;
                    return Equals(text);
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства локализованных текстов по значению
        /// </summary>
        /// <param name="other">Сравниваемый локализованный текст</param>
        /// <returns>Статус равенства</returns>
        public bool Equals(TLocalizableText other)
        {
            return IDKeyLocalize == other.IDKeyLocalize;
        }

        /// <summary>
        /// Сравнение локализованных текстов для упорядочивания
        /// </summary>
        /// <param name="other">Сравниваемый локализованный текст</param>
        /// <returns>Статус сравнения</returns>
        public int CompareTo(TLocalizableText other)
        {
            if (IDKeyLocalize > other.IDKeyLocalize)
            {
                return 1;
            }
            else
            {
                if (IDKeyLocalize < other.IDKeyLocalize)
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
        /// Получение хеш-кода локализованного текста
        /// </summary>
        /// <returns>Хеш-код локализованного текста</returns>
        public override int GetHashCode()
        {
            return IDKeyLocalize == 0 ? Text.GetHashCode() : IDKeyLocalize;
        }

        /// <summary>
        /// Полное копирование
        /// </summary>
        /// <returns>Копия</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Преобразование к текстовому представлению
        /// </summary>
        /// <returns>Локализованный текст</returns>
        public override string ToString()
        {
            return Text;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнение объектов на равенство
        /// </summary>
        /// <param name="left">Первый объект</param>
        /// <param name="right">Второй объект</param>
        /// <returns>Статус равенства</returns>
        public static bool operator ==(TLocalizableText left, TLocalizableText right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство
        /// </summary>
        /// <param name="left">Первый объект</param>
        /// <param name="right">Второй объект</param>
        /// <returns>Статус неравенство</returns>
        public static bool operator !=(TLocalizableText left, TLocalizableText right)
        {
            return !(left == right);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение ключа локализации текста посредством хэш-кода строки
        /// </summary>
        public void GetIDFromText()
        {
            // Самый простой метод
            IDKeyLocalize = Text.GetHashCode();
        }

        /// <summary>
        /// Установка локализованных данных
        /// </summary>
        /// <remarks>
        /// Методы должен/будет вызываться при смене активного языка
        /// </remarks>
        public void SetLocalizableData()
        {
            if (IDKeyLocalize != 0 && IDKeyLocalize != -1)
            {
                LotusLocalizationDispatcher.GetTextCurrentFromID(IDKeyLocalize, out Text);
            }
            else
            {
                if (IDKeyLocalize == 0)
                {
                    Text = LotusLocalizationDispatcher.GetTextCurrentFromTextDefault(Text);
                }
            }
        }
        #endregion
    }

    /**@}*/
}
#endif
