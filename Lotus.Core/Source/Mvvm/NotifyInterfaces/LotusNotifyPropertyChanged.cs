using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lotus.Core
{
    /**
     * \defgroup CoreNotifyInterfaces Интерфейсы уведомления
     * \ingroup Core
     * \brief Подсистема для определение дополнительных интерфейсов по уведомлению об изменении свойств объектов 
		и данных коллекций. 
     * @{
     */
    /// <summary>
    /// Определение дополнительного интерфейса для нотификации об изменении данных.
    /// </summary>
    /// <remarks>
    /// Указанный интерфейс является базой для реализации связывания данных. Он обеспечивает концепцию «издатель-подписчик»,
    /// в рамках того что если объект является «издателем», т.е. другим объектам нужно знать об изменение его свойства,
    /// он обязательно должен правильно и безопасно реализовать указанный интерфейс
    /// </remarks>
    public interface ILotusNotifyPropertyChanged
    {
        /// <summary>
        /// Событие для нотификации об изменении значения свойства. Аргумент - источник события, имя свойства и его новое значение.
        /// </summary>
        Action<object, string, object> OnPropertyChanged { get; set; }
    }

    /// <summary>
    /// Базовый класс для реализации оповещения об изменении своих свойств.
    /// </summary>
    /// <remarks>
    /// В качестве нотификации о изменение свойств используются стандартный интерфейс уведомлений <see cref="INotifyPropertyChanged"/>.
    /// </remarks>
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие срабатывает ПОСЛЕ изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="args">Аргументы события.</param>
        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }
    }
    /**@}*/
}