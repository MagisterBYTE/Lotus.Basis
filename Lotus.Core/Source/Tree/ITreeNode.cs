using System;
using System.Collections.Generic;

namespace Lotus.Core
{
	/// <summary>
	/// Определение интерфейса для объектов которые могут находиться в дереве.
	/// </summary>
	public interface ITreeNode : ILotusIdentifierId<Guid>, ILotusCheckOne<ITreeNode>
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
		ITreeNode? IParentTreeNode { get; set; }

		/// <summary>
		/// Список дочерних узлов.
		/// </summary>
		IEnumerable<ITreeNode>? IChildNodes { get; }
	}
}
