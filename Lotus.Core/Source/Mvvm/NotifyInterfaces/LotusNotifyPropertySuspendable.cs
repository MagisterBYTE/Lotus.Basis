using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreNotifyInterfaces
	*@{*/
    /// <summary>
    /// Класс для реализации оповещения об изменении своих свойств с возможностью приостановки оповещения.
    /// </summary>
    public class PropertyChangedSuspendable : INotifyPropertyChanged
    {
        /// <summary>
        /// Статус приостановки оповещения объекта об изменении своих свойств.
        /// </summary>
        public bool SuspendableNotify { get; set; }

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
            if (PropertyChanged != null && !SuspendableNotify)
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
            if (PropertyChanged != null && !SuspendableNotify)
            {
                PropertyChanged(this, args);
            }
        }
    }
    /**@}*/
}