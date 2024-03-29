using System;
using System.Collections.Generic;

namespace Lotus.Core
{
    /**
     * \defgroup CoreTreeNode Подсистема иерархических данных
     * \ingroup Core
     * \brief Подсистема иерархических данных реализует универсальный тип узла дерева совокупность которых 
        формирует иерархическое дерево.
     * @{
     */
    /// <summary>
    /// Определение интерфейса узла дерева.
    /// </summary>
    public interface ILotusTreeNode : ILotusIdentifierId<Guid>, ILotusCheckOne<ILotusTreeNode>
    {
        /// <summary>
        /// Статус выбора узла.
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Статус выбора узла флажком.
        /// </summary>
        bool? IsChecked { get; set; }

        /// <summary>
        /// Статус раскрытия узла.
        /// </summary>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Порядок следования узла(индекс его расположения).
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// Родительский узел.
        /// </summary>
        ILotusTreeNode? IParentTreeNode { get; set; }

        /// <summary>
        /// Список дочерних узлов.
        /// </summary>
        IEnumerable<ILotusTreeNode>? IChildNodes { get; }
    }
    /**@}*/
}