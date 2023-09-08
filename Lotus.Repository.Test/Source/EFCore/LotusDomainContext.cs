//=====================================================================================================================
// Проект: LotusPlatform
// Проект: Модуль репозитория
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDomainContext.cs
*		Контекст базы данных для тестирования.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Repository;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Контекст базы данных для тестирования
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class DomainContext : DbContext
		{
			public static DomainContext Create(String connectingString)
			{
				var optionBuilder = new DbContextOptionsBuilder<DomainContext>();
				optionBuilder.UseNpgsql(connectingString, db => db.MigrationsAssembly("Lotus.Repository.Test"));
				return new DomainContext(optionBuilder.Options);
			}

			public DbSet<Person> Peoples { get; set; } = default!;

			public DbSet<Permission> Permissions { get; set; } = default!;

			public DbSet<Role> Roles { get; set; } = default!;

			public DbSet<ResourceFile> ResourceFiles { get; set; } = default!;

			public DomainContext(DbContextOptions<DomainContext> options)
				: base(options)
			{
			}

			public void Initialize()
			{
				var permissionAdmin = new Permission(1, "admin");
				var permissionEditor = new Permission(2, "editor");
				var permissionUser = new Permission(3, "user");
				var permissionCanCreateUser = new Permission(4, "canCreateUser");
				var permissionCanEditUser = new Permission(5, "canEditUser");
				var permissionCanWorldCreate = new Permission(6, "canWorldCreate");
				var permissionCanPersonCreate = new Permission(7, "canWorldCreate");
				var permissionFake1 = new Permission(8, "");
				var permissionFake2 = new Permission(9, null);

				Permissions.Add(permissionAdmin);
				Permissions.Add(permissionEditor);
				Permissions.Add(permissionUser);
				Permissions.Add(permissionCanCreateUser);
				Permissions.Add(permissionCanEditUser);
				Permissions.Add(permissionCanWorldCreate);
				Permissions.Add(permissionCanPersonCreate);
				Permissions.Add(permissionFake1);
				Permissions.Add(permissionFake2);

				SaveChanges();

				var roleAdmin = new Role(1, "admin", permissionAdmin, permissionCanCreateUser, permissionCanEditUser);
				var roleEditor = new Role(2, "editor", permissionEditor, permissionCanEditUser);
				var roleUser = new Role(3, "user", permissionUser, permissionCanEditUser, permissionCanWorldCreate, permissionCanPersonCreate);
				var roleGuest = new Role(4, "guest", permissionCanCreateUser, permissionCanEditUser);

				Roles.Add(roleAdmin);
				Roles.Add(roleEditor);
				Roles.Add(roleUser);
				Roles.Add(roleGuest);

				SaveChanges();
			}
		}
	}
}
//=====================================================================================================================
