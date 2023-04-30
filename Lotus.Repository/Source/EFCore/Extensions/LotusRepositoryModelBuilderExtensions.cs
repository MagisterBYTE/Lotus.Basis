//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryEntityTypeBuilderExtensions.cs
*		Статический класс реализующий методы расширения для работы с ModelBuilder.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup RepositoryExtensions
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения для работы с ModelBuilder
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XModelBuilderExtensions
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменить поведение при удалении на <see cref="DeleteBehavior.Restrict"/>
			/// </summary>
			/// <param name="modelBuilder">Построитель модели</param>
			/// <returns>Построитель модели</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ModelBuilder ReplaceCascadeDeleteBehaviorToRestrict(this ModelBuilder modelBuilder)
			{
				var cascadeFKs = modelBuilder.Model.GetEntityTypes()
					.SelectMany(x => x.GetForeignKeys())
					.Where(x => !x.IsOwnership
								&& !HasIgnoreAnnotation(x)
								&& x.DeleteBehavior == DeleteBehavior.Cascade);

				foreach (var fk in cascadeFKs)
				{
					fk.DeleteBehavior = DeleteBehavior.Restrict;
				}

				return modelBuilder;

				Boolean HasIgnoreAnnotation(IMutableForeignKey key)
				{
					return (Boolean?)key.FindAnnotation(XEntityTypeBuilderExtensions.IgnoreOverrideDeleteBehavior)?.Value == true;
				}
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================