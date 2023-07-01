//=====================================================================================================================
// Проект: LotusPlatform
// Проект: Модуль репозитория
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusEntityRole.cs
*		Роль.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Repository;
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Пользователь
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class Role : EntityDb<Int32>
		{
			public String? Name { get; set; }

			public List<Permission> Permissions { get; set; } = new List<Permission>();

			public Role(Int32 id, String name)
				: base(id)
			{
				Name = name;
			}

			public Role(Int32 id, String name, params Permission[] permissions)
				: base(id)
			{
				Name = name;
				Permissions.AddRange(permissions);
			}
		}
	}
}
//=====================================================================================================================
