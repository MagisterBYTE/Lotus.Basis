using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using Lotus.Core;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryEFCore
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения для работы с EntityTypeBuilder.
    /// </summary>
    public static class XEntityTypeBuilderExtensions
    {
        public const string IgnoreOverrideDeleteBehavior = "IgnoreOverrideDeleteBehavior";

        /// <summary>
        /// Добавление столбца - первичного ключа.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="builder">Построитель сущности.</param>
        /// <returns>Построитель сущности.</returns>
        public static EntityTypeBuilder<TEntity> AddKeyColumn<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, ILotusIdentifierId<Guid>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            return builder;
        }

        /// <summary>
        /// Устанавливает частичный индекс на не удалённые записи.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="builder">Построитель сущности.</param>
        /// <param name="indexExpression">Выражение для индекса.</param>
        /// <returns>Построитель сущности.</returns>
        public static IndexBuilder<TEntity> HasNotDeletedPartialUniqueIndex<TEntity>(this EntityTypeBuilder<TEntity> builder,
            [NotNull] Expression<Func<TEntity, object>> indexExpression)
            where TEntity : class, ILotusEntityDbSoftDeletable
        {
            return builder
                .HasIndex(indexExpression!)
                .HasFilter("deleted IS NULL")
                .IsUnique();
        }

        /// <summary>
        /// Добавление столбцов - даты создания и модификации.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="builder">Построитель сущности.</param>
        /// <returns>Построитель сущности.</returns>
        public static EntityTypeBuilder<TEntity> SetDatesPolicy<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, ILotusEntityDb<Guid>
        {
            builder
                .Property(x => x.Created)
                .HasColumnName("created")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder
                .Property(x => x.Modified)
                .HasColumnName("modified")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

            return builder;
        }

        /// <summary>
        /// Устанавливает поведение при удалении, которое не будет переопределено на стандартное.
        /// Сделано так потому что EF Core не позволяет устанавливать поведение по удалению по умолчанию (всегда Cascade).
        /// </summary>
        /// <typeparam name="TPrincipalEntity">Тип сущности.</typeparam>
        /// <typeparam name="TDependentEntity">Тип сущности.</typeparam>
        /// <param name="builder">Построитель сущности.</param>
        /// <param name="behavior">Поведение при удалении.</param>
        /// <returns>Построитель сущности.</returns>
        public static ReferenceCollectionBuilder<TPrincipalEntity, TDependentEntity> SetDeleteBehavior<TPrincipalEntity, TDependentEntity>(
            [NotNull] this ReferenceCollectionBuilder<TPrincipalEntity, TDependentEntity> builder,
            DeleteBehavior behavior)
            where TPrincipalEntity : class
            where TDependentEntity : class
        {
            return builder
                .OnDelete(behavior)
                .HasAnnotation(IgnoreOverrideDeleteBehavior, true);
        }

        /// <summary>
        /// Установка мягкой политики удаления.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="builder">Построитель сущности.</param>
        /// <returns>Построитель сущности.</returns>
        public static EntityTypeBuilder<TEntity> SetSoftDeletePolicy<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, ILotusEntityDbSoftDeletable
        {
            builder.Property(x => x.Deleted).HasColumnName("deleted");

            builder.HasIndex(x => x.Deleted);
            builder.HasQueryFilter(x => x.Deleted == null);

            return builder;
        }
    }
    /**@}*/
}