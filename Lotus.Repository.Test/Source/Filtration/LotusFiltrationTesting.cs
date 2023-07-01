//=====================================================================================================================
// Проект: LotusPlatform
// Проект: Модуль репозитория
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusFiltrationTesting.cs
*		Тестирование методов фильтрации.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Repository;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для тестирования методов фильтрации
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class FiltrationTesting : IClassFixture<DomainContextFixture>
		{
			public DomainContextFixture Fixture { get; }

			public FiltrationTesting(DomainContextFixture fixture)
			{
				Fixture = fixture;
			}

			[Fact]
			public void TestFiltrationString()
			{
				var context = Fixture.CreateContext();

				var propertyName = new CFilterProperty();
				propertyName.PropertyName = nameof(Permission.Name);
				propertyName.PropertyType = TEntityPropertyType.String;

				//
				// TFilterFunction.Equals
				//

				propertyName.Value = "canEditUser";
				propertyName.Function = TFilterFunction.Equals;

				var result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Single(result);
				Assert.Equal("canEditUser", result[0].Name);


				//
				// TFilterFunction.NotEqual
				//
				propertyName.Value = "canEditUser";
				propertyName.Function = TFilterFunction.NotEqual;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(8, result.Length);

				//
				// TFilterFunction.Contains
				//
				propertyName.Value = "can";
				propertyName.Function = TFilterFunction.Contains;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(4, result.Length);

				//
				// TFilterFunction.StartsWith
				//
				propertyName.Value = "can";
				propertyName.Function = TFilterFunction.StartsWith;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(4, result.Length);

				//
				// TFilterFunction.EndsWith
				//
				propertyName.Value = "User";
				propertyName.Function = TFilterFunction.EndsWith;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(2, result.Length);

				//
				// TFilterFunction.NotEmpty
				//
				propertyName.Value = "";
				propertyName.Function = TFilterFunction.NotEmpty;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(7, result.Length);
			}

			[Fact]
  			public void TestFiltrationArrary()
			{
				var context = Fixture.CreateContext();

				var propertyName = new CFilterProperty();
				propertyName.PropertyName = "PermissionIds";
				propertyName.PropertyType = TEntityPropertyType.Integer;

				//
				// TFilterFunction.IncludeAny
				//

				propertyName.Values = new String[] {"5"};
				propertyName.Function = TFilterFunction.IncludeAny;

				var result = context.Roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

				Assert.Equal(4, result.Length);
				Assert.Equal("admin", result[0].Name);
				Assert.Equal("editor", result[1].Name);
				Assert.Equal("user", result[2].Name);
				Assert.Equal("guest", result[3].Name);

				propertyName.Values = new String[] { "4", "2" };
				propertyName.Function = TFilterFunction.IncludeAny;

				result = context.Roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

				Assert.Equal(3, result.Length);
				Assert.Equal("admin", result[0].Name);
				Assert.Equal("editor", result[1].Name);
				Assert.Equal("guest", result[2].Name);

				//
				// TFilterFunction.IncludeEquals
				//

				propertyName.Values = new String[] { "4", "5" };
				propertyName.Function = TFilterFunction.IncludeEquals;

				result = context.Roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

				Assert.Single(result);

				propertyName.Values = new String[] { "4" };
				propertyName.Function = TFilterFunction.IncludeEquals;

				result = context.Roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

				Assert.Empty(result);

				//
				// TFilterFunction.IncludeNone
				//

				propertyName.Values = new String[] { "4"};
				propertyName.Function = TFilterFunction.IncludeNone;

				result = context.Roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

				Assert.Equal(2, result.Length);
				Assert.Equal("editor", result[0].Name);
				Assert.Equal("user", result[1].Name);

				propertyName.Values = new String[] { "4", "2" };
				propertyName.Function = TFilterFunction.IncludeNone;

				result = context.Roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

				Assert.Single(result);
				Assert.Equal("user", result[0].Name);
			}

			[Fact]
			public void TestFiltrationNumber()
			{
				var context = Fixture.CreateContext();

				var propertyName = new CFilterProperty();
				propertyName.PropertyName = nameof(Permission.Id);
				propertyName.PropertyType = TEntityPropertyType.Integer;

				//
				// TFilterFunction.Equals
				//

				propertyName.Value = "1";
				propertyName.Function = TFilterFunction.Equals;

				var result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Single(result);
				Assert.Equal(1, result[0].Id);


				//
				// TFilterFunction.NotEqual
				//
				propertyName.Value = "2";
				propertyName.Function = TFilterFunction.NotEqual;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(8, result.Length);

				//
				// TFilterFunction.LessThan
				//
				propertyName.Value = "3";
				propertyName.Function = TFilterFunction.LessThan;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(2, result.Length);

				//
				// TFilterFunction.Between
				//
				propertyName.Values = new String[] { "4", "8"};
				propertyName.Function = TFilterFunction.Between;

				result = context.Permissions.Filter(propertyName).ToArray();

				Assert.Equal(3, result.Length);
			}
		}
	}
}
//=====================================================================================================================