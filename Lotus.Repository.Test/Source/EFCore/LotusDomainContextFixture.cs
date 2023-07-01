//=====================================================================================================================
// Проект: LotusPlatform
// Проект: Модуль репозитория
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDomainContextFixture.cs
*		Контекст базы данных для тестирования.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		public class DomainContextFixture
		{
			private static readonly Object _lock = new();
			private static Boolean _databaseInitialized;

			public DomainContextFixture()
			{
				lock (_lock)
				{
					if (!_databaseInitialized)
					{
						using (var context = CreateContext())
						{
							context.Database.EnsureDeleted();
							context.Database.EnsureCreated();

							context.Initialize();
						}

						_databaseInitialized = true;
					}
				}
			}

			public DomainContext CreateContext()
			{
				return DomainContext.Create(XConnection.ConnectionString);
			}
		}
	}
}
//=====================================================================================================================
