//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема мешей
// Подраздел: Общее данные
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMesh3DVertex.cs
*		Вершина трёхмерной полигональной сетки(меша).
*		Вершина меша предъявляет собой основный данные из которых состоит меш.
*		На ряду с основным компонентом вершины - позицией в трехмерном пространстве каждая вершина содержит также нормаль,
*	цвет, текстурные координаты, а также дополнительные данные и вспомогательную информацию.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Object3D
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup Object3DMeshCommon
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вершина трёхмерной полигональной сетки(меша)
		/// </summary>
		/// <remarks>
		/// На ряду с основным компонентом вершины - позицией в трехмерном пространстве каждая вершина содержит также нормаль, 
		/// цвет, текстурные координаты, а также дополнительные данные и вспомогательную информацию
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public struct CVertex3Df : ILotusMeshOperaiton, IComparable<CVertex3Df>, IEquatable<CVertex3Df>, ICloneable
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Текстовый формат отображения параметров вершины
			/// </summary>
			public static String ToStringFormat = "{0} [{1:0.000}; {2:0.000}; {3:0.000}]";
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Индекс вершины в списке вершин
			/// </summary>
			public Int32 Index;

			/// <summary>
			/// Позиция вершины
			/// </summary>
			public Vector3Df Position;

			/// <summary>
			/// Нормаль вершины
			/// </summary>
			public Vector3Df Normal;

			/// <summary>
			/// Тангентс вершины
			/// </summary>
			public Vector3Df Tangent;

			/// <summary>
			/// Координаты текстурной развёртки основного канала
			/// </summary>
			public Vector2Df UV;

			/// <summary>
			/// Координаты текстурной развёртки дополнительного канала
			/// </summary>
			public Vector2Df UV2;

			/// <summary>
			/// Координаты текстурной развёртки дополнительного канала
			/// </summary>
			public Vector2Df UV3;

			/// <summary>
			/// Статус уникальной вершины
			/// </summary>
			/// <remarks>
			/// <para>
			/// Статус уникальной вершины подразумевается что в меше есть только одна такая вершина с данной позиций, 
			/// если имеется еще одна такая вершина с данной позиций то эти вершины не уникальны. 
			/// </para>
			/// <para>
			/// Данные параметр применяется при трансформации чтобы можно было понять что если НЕ уникальная 
			/// вершина трансформируется то значит должна трансформироваться и другая НЕ уникальная вершина с 
			/// данной позицией чтобы сохранилась топология сетки
			/// </para>
			/// <para>
			/// Также применяем правило, что в списке вершин <see cref="CListVertex3D"/> все вершины должны быть уникальными
			/// </para>
			/// </remarks>
			public Boolean IsUnique;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Тип структурного элемента меша
			/// </summary>
			public TMeshElement MeshElement { get { return TMeshElement.Vertex; } }
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(Vector3Df position)
			{
				Index = -1;
				Position = position;
				Normal = Vector3Df.Zero;
				Tangent = Vector3Df.Zero;
				UV = Vector2Df.Zero;
				UV2 = Vector2Df.Zero;
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(Vector3Df position, Vector2Df uv)
			{
				Index = -1;
				Position = position;
				Normal = Vector3Df.Zero;
				Tangent = Vector3Df.Zero;
				UV = uv;
				UV2 = Vector2Df.Zero;
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(Vector3Df position, Vector3Df normal)
			{
				Index = -1;
				Position = position;
				Normal = normal;
				Tangent = Vector3Df.Zero;
				UV = Vector2Df.Zero;
				UV2 = Vector2Df.Zero;
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(Vector3Df position, Vector3Df normal, Vector2Df uv)
			{
				Index = -1;
				Position = position;
				Normal = normal;
				Tangent = Vector3Df.Zero;
				UV = uv;
				UV2 = Vector2Df.Zero;
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			/// <param name="uv2">Вторые текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(Vector3Df position, Vector3Df normal, Vector2Df uv, Vector2Df uv2)
			{
				Index = -1;
				Position = position;
				Normal = normal;
				Tangent = Vector3Df.Zero;
				UV = uv;
				UV2 = uv2;
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(UnityEngine.Vector3 position, UnityEngine.Vector3 normal)
			{
				Index = -1;
				Position = position.ToVector3Df();
				Normal = normal.ToVector3Df();
				Tangent = Vector3Df.Zero;
				UV = Vector2Df.Zero;
				UV2 = Vector2Df.Zero;
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Vector2 uv)
			{
				Index = -1;
				Position = position.ToVector3Df();
				Normal = normal.ToVector3Df();
				Tangent = Vector3Df.Zero;
				UV = uv.ToVector2Df();
				UV2 = Vector2Df.Zero;
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			/// <param name="uv2">Вторые текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Vector2 uv, UnityEngine.Vector2 uv2)
			{
				Index = -1;
				Position = position.ToVector3Df();
				Normal = normal.ToVector3Df();
				Tangent = Vector3Df.Zero;
				UV = uv.ToVector2Df();
				UV2 = uv2.ToVector2Df();
				UV3 = Vector2Df.Zero;
				IsUnique = true;
			}
#endif
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(Object obj)
			{
				if (obj != null)
				{
					if (GetType() == obj.GetType())
					{
						var vertex = (CVertex3Df)obj;
						return Equals(vertex);
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства вершин по значению позиции
			/// </summary>
			/// <remarks>
			/// Вершины равные если равны их позиции в трехмерном пространстве
			/// </remarks>
			/// <param name="other">Сравниваемая вершина</param>
			/// <returns>Статус равенства вершин</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(CVertex3Df other)
			{
				return Vector3Df.Approximately(in Position, in other.Position, XMeshSetting.Eplsilon_f);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение вершин для упорядочивания
			/// </summary>
			/// <remarks>
			/// Сравнение вершин происходит по индексу
			/// </remarks>
			/// <param name="other">Вершина</param>
			/// <returns>Статус сравнения вершин</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CVertex3Df other)
			{
				if (Index > other.Index)
				{
					return 1;
				}
				else
				{
					if (Index < other.Index)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода вершины
			/// </summary>
			/// <returns>Хеш-код вершины</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return Position.X.GetHashCode() ^ Position.Y.GetHashCode() ^ Position.Z.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода указанной вершины
			/// </summary>
			/// <param name="obj">Вершина</param>
			/// <returns>Хеш-код вершины</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetHashCode(CVertex3Df obj)
			{
				return obj.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование вершины
			/// </summary>
			/// <returns>Копия вершины</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Позиция вершины</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return String.Format(ToStringFormat, Index, Position.X, Position.Y, Position.Z);
			}
			#endregion

			#region ======================================= ILotusMeshOperaiton - МЕТОДЫ ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Смещение вершины
			/// </summary>
			/// <param name="offset">Вектор смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public void Move(Vector3Df offset)
			{
				Position += offset;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершины
			/// </summary>
			/// <param name="rotation">Кватернион вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public void Rotate(Quaternion3Df rotation)
			{
				Position = rotation.TransformVector(in Position);
				Normal = rotation.TransformVector(in Normal);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Однородное масштабирование вершины
			/// </summary>
			/// <param name="scale">Масштаб</param>
			//---------------------------------------------------------------------------------------------------------
			public void Scale(Single scale)
			{
				Position *= scale;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Масштабирование вершины
			/// </summary>
			/// <param name="scale">Масштаб</param>
			//---------------------------------------------------------------------------------------------------------
			public void Scale(Vector3Df scale)
			{
				Position = Vector3Df.Scale(Position, scale);
				Normal = Vector3Df.Scale(Normal, scale).Normalized;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить нормали
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void FlipNormals()
			{
				Normal = -Normal;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить развёртку текстурных координат по горизонтали 
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public void FlipUVHorizontally(Int32 channel = 0)
			{
				switch (channel)
				{
					case 0:
						UV = new Vector2Df(1 - UV.X, UV.Y);
						break;
					case 1:
						UV2 = new Vector2Df(1 - UV2.X, UV2.Y);
						break;
					case 2:
						UV3 = new Vector2Df(1 - UV3.X, UV3.Y);
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить развёртку текстурных координат по вертикали 
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public void FlipUVVertically(Int32 channel = 0)
			{
				switch (channel)
				{
					case 0:
						UV = new Vector2Df(UV.X, 1 - UV.Y);
						break;
					case 1:
						UV2 = new Vector2Df(UV2.X, 1 - UV2.Y);
						break;
					case 2:
						UV3 = new Vector2Df(UV3.X, 1 - UV3.Y);
						break;
					default:
						break;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное дублирование вершины
			/// </summary>
			/// <remarks>
			/// У дубликата индекс вершины всегда равен -1
			/// </remarks>
			/// <returns>Дубликат вершины</returns>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df Duplicate()
			{
				var copy = (CVertex3Df)MemberwiseClone();
				copy.Index = - 1;
				return copy;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Смещение вершины вдоль нормали
			/// </summary>
			/// <param name="offset">Размер смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public void OffsetFromNormal(Single offset)
			{
				Position = Position + Normal * offset;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение вершины смещенной вдоль нормали
			/// </summary>
			/// <param name="offset">Размер смещения</param>
			/// <returns>Смещенная вершина</returns>
			//---------------------------------------------------------------------------------------------------------
			public CVertex3Df GetVertexOffsetFromNormal(Single offset)
			{
				CVertex3Df copy = Duplicate();
				copy.Position = Position + Normal * offset;
				return copy;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Вспомогательный класс реализующий список вершин
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CListVertex3D : ListArray<CVertex3Df>, ILotusMeshOperaiton
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Тип структурного элемента меша
			/// </summary>
			public TMeshElement MeshElement { get { return TMeshElement.Vertex; } }

			/// <summary>
			/// Список вершин для прямого доступа
			/// </summary>
			public CVertex3Df[] Vertices { get { return mArrayOfItems; } }
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CListVertex3D()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="list">Список вершин</param>
			//---------------------------------------------------------------------------------------------------------
			public CListVertex3D(params Vector3Df[] list)
			{
				for (var i = 0; i < list.Length; i++)
				{
					AddVertex(list[i]);
				}
			}
			#endregion

			#region ======================================= ILotusMeshOperaiton - МЕТОДЫ ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Смещение вершин
			/// </summary>
			/// <param name="offset">Вектор смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public void Move(Vector3Df offset)
			{
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Position += offset;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин
			/// </summary>
			/// <param name="rotation">Кватернион вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public void Rotate(Quaternion3Df rotation)
			{
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Position = rotation.TransformVector(in mArrayOfItems[i].Position);
					mArrayOfItems[i].Normal = rotation.TransformVector(in mArrayOfItems[i].Normal);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин вокруг оси X
			/// </summary>
			/// <param name="angle">Угол в градусах</param>
			/// <param name="isCenter">Относительно геометрического центра</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateFromX(Single angle, Boolean isCenter)
			{
				if (isCenter)
				{
					Vector3Df offset = GetCentredPosition();
					Matrix4Dx4 rotation = Matrix4Dx4.Identity;
					Matrix4Dx4.RotationX(angle, ref rotation);
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].Position -= offset;
						mArrayOfItems[i].Position.TransformAsPoint(in rotation);
						mArrayOfItems[i].Position += offset;
						mArrayOfItems[i].Normal.TransformAsVector(in rotation);
					}
				}
				else
				{
					Matrix4Dx4 rotation = Matrix4Dx4.Identity;
					Matrix4Dx4.RotationX(angle, ref rotation);
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].Position.TransformAsPoint(in rotation);
						mArrayOfItems[i].Normal.TransformAsVector(in rotation);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин вокруг оси Y
			/// </summary>
			/// <param name="angle">Угол в градусах</param>
			/// <param name="isCenter">Относительно геометрического центра</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateFromY(Single angle, Boolean isCenter)
			{
				if (isCenter)
				{
					Vector3Df offset = GetCentredPosition();
					Matrix4Dx4 rotation = Matrix4Dx4.Identity;
					Matrix4Dx4.RotationY(angle, ref rotation);
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].Position -= offset;
						mArrayOfItems[i].Position.TransformAsPoint(in rotation);
						mArrayOfItems[i].Position += offset;
						mArrayOfItems[i].Normal.TransformAsVector(in rotation);
					}
				}
				else
				{
					Matrix4Dx4 rotation = Matrix4Dx4.Identity;
					Matrix4Dx4.RotationY(angle, ref rotation);
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].Position.TransformAsPoint(in rotation);
						mArrayOfItems[i].Normal.TransformAsVector(in rotation);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Врашение вершин вокруг оси Z
			/// </summary>
			/// <param name="angle">Угол в градусах</param>
			/// <param name="isCenter">Относительно геометрического центра</param>
			//---------------------------------------------------------------------------------------------------------
			public void RotateFromZ(Single angle, Boolean isCenter)
			{
				if (isCenter)
				{
					Vector3Df offset = GetCentredPosition();
					Matrix4Dx4 rotation = Matrix4Dx4.Identity;
					Matrix4Dx4.RotationZ(angle, ref rotation);
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].Position -= offset;
						mArrayOfItems[i].Position.TransformAsPoint(in rotation);
						mArrayOfItems[i].Position += offset;
						mArrayOfItems[i].Normal.TransformAsVector(in rotation);
					}
				}
				else
				{
					Matrix4Dx4 rotation = Matrix4Dx4.Identity;
					Matrix4Dx4.RotationZ(angle, ref rotation);
					for (var i = 0; i < mCount; i++)
					{
						mArrayOfItems[i].Position.TransformAsPoint(in rotation);
						mArrayOfItems[i].Normal.TransformAsVector(in rotation);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Однородное масштабирование вершин
			/// </summary>
			/// <param name="scale">Масштаб</param>
			//---------------------------------------------------------------------------------------------------------
			public void Scale(Single scale)
			{
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Position *= scale;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Масштабирование вершины
			/// </summary>
			/// <param name="scale">Масштаб</param>
			//---------------------------------------------------------------------------------------------------------
			public void Scale(Vector3Df scale)
			{
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Position = Vector3Df.Scale(mArrayOfItems[i].Position, scale);
					mArrayOfItems[i].Normal = Vector3Df.Scale(mArrayOfItems[i].Normal, scale).Normalized;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить нормали
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void FlipNormals()
			{
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Normal = -mArrayOfItems[i].Normal;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить развёртку текстурных координат по горизонтали 
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public void FlipUVHorizontally(Int32 channel = 0)
			{
				switch (channel)
				{
					case 0:
						for (var i = 0; i < mCount; i++)
						{
							mArrayOfItems[i].UV = new Vector2Df(1.0f - mArrayOfItems[i].UV.X, mArrayOfItems[i].UV.Y);
						}
						break;
					case 1:
						for (var i = 0; i < mCount; i++)
						{
							mArrayOfItems[i].UV2 = new Vector2Df(1.0f - mArrayOfItems[i].UV2.X, mArrayOfItems[i].UV2.Y);
						}
						break;
					case 2:
						for (var i = 0; i < mCount; i++)
						{
							mArrayOfItems[i].UV3 = new Vector2Df(1.0f - mArrayOfItems[i].UV3.X, mArrayOfItems[i].UV3.Y);
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обратить развёртку текстурных координат по вертикали 
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public void FlipUVVertically(Int32 channel = 0)
			{
				switch (channel)
				{
					case 0:
						for (var i = 0; i < mCount; i++)
						{
							mArrayOfItems[i].UV = new Vector2Df(mArrayOfItems[i].UV.X, 1.0f - mArrayOfItems[i].UV.Y);
						}
						break;
					case 1:
						for (var i = 0; i < mCount; i++)
						{
							mArrayOfItems[i].UV2 = new Vector2Df(mArrayOfItems[i].UV2.X, 1.0f - mArrayOfItems[i].UV2.Y);
						}
						break;
					case 2:
						for (var i = 0; i < mCount; i++)
						{
							mArrayOfItems[i].UV3 = new Vector2Df(mArrayOfItems[i].UV3.X, 1.0f - mArrayOfItems[i].UV3.Y);
						}
						break;
					default:
						break;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное дублирование списка вершин
			/// </summary>
			/// <returns>Дубликат списка вершин</returns>
			//---------------------------------------------------------------------------------------------------------
			public CListVertex3D Duplicate()
			{
				var list_vertex = new CListVertex3D();
				for (var i = 0; i < mCount; i++)
				{
					list_vertex.Add(mArrayOfItems[i]);
				}
				return list_vertex;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Смещение вершины вдоль нормали
			/// </summary>
			/// <param name="offset">Размер смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public void OffsetFromNormal(Single offset)
			{
				for (var i = 0; i < mCount; i++)
				{
					mArrayOfItems[i].Position = mArrayOfItems[i].Position + mArrayOfItems[i].Normal * offset;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка вершин смещенных вдоль нормали
			/// </summary>
			/// <param name="offset">Размер смещения</param>
			/// <returns>Список вершин смещенных вдоль нормали</returns>
			//---------------------------------------------------------------------------------------------------------
			public CListVertex3D GetListVertexOffsetFromNormal(Single offset)
			{
				var list_vertex = new CListVertex3D();
				for (var i = 0; i < mCount; i++)
				{
					list_vertex.Add(mArrayOfItems[i].Position + mArrayOfItems[i].Normal * offset);
				}
				return list_vertex;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение позиции вершины с наибольшими координатами
			/// </summary>
			/// <returns>Позиция вершины с наибольшими координатами</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df GetMaximizedPosition()
			{
				Single size = 20000;

				var max_x = -size;
				var max_y = -size;
				var max_z = -size;

				for (var i = 0; i < mCount; i++)
				{
					if (mArrayOfItems[i].Position.X > max_x) max_x = mArrayOfItems[i].Position.X;
					if (mArrayOfItems[i].Position.Y > max_y) max_y = mArrayOfItems[i].Position.Y;
					if (mArrayOfItems[i].Position.Z > max_z) max_z = mArrayOfItems[i].Position.Z;
				}

				return new Vector3Df(max_x, max_y, max_z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение позиции вершины с наименьшими координатами
			/// </summary>
			/// <returns>Позиция вершины с наименьшими координатами</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df GetMinimizedPosition()
			{
				Single size = 20000;

				var min_x = size;
				var min_y = size;
				var min_z = size;

				for (var i = 0; i < mCount; i++)
				{
					if (mArrayOfItems[i].Position.X < min_x) min_x = mArrayOfItems[i].Position.X;
					if (mArrayOfItems[i].Position.Y < min_y) min_y = mArrayOfItems[i].Position.Y;
					if (mArrayOfItems[i].Position.Z < min_z) min_z = mArrayOfItems[i].Position.Z;
				}

				return new Vector3Df(min_x, min_y, min_z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение средней(геометрического центра) позиции вершин
			/// </summary>
			/// <returns>Средняя позиция вершин</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector3Df GetCentredPosition()
			{
				Vector3Df max = GetMaximizedPosition();
				Vector3Df min = GetMinimizedPosition();

				return (max + min) / 2;
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ВЕРШИНАМИ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(Vector3Df position)
			{
				var vertex = new CVertex3Df(position);
				vertex.Index = mCount;
				Add(vertex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(Vector3Df position, Vector2Df uv)
			{
				var vertex = new CVertex3Df(position, uv);
				vertex.Index = mCount;
				Add(vertex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(Vector3Df position, Vector3Df normal)
			{
				var vertex = new CVertex3Df(position, normal);
				vertex.Index = mCount;
				Add(vertex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(Vector3Df position, Vector3Df normal, Vector2Df uv)
			{
				var vertex = new CVertex3Df(position, normal, uv);
				vertex.Index = mCount;
				Add(vertex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			/// <param name="uv2">Вторые текстурные координаты вершины</param> 
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(Vector3Df position, Vector3Df normal, Vector2Df uv, Vector2Df uv2)
			{
				var vertex = new CVertex3Df(position, normal, uv, uv2);
				vertex.Index = mCount;
				Add(vertex);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(UnityEngine.Vector3 position, UnityEngine.Vector3 normal)
			{
				CVertex3Df vertex = new CVertex3Df(position, normal);
				vertex.Index = mCount;
				Add(vertex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Vector2 uv)
			{
				CVertex3Df vertex = new CVertex3Df(position, normal, uv);
				vertex.Index = mCount;
				Add(vertex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершины
			/// </summary>
			/// <param name="position">Позиция вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uv">Текстурные координаты вершины</param>
			/// <param name="uv2">Вторые текстурные координаты вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertex(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Vector2 uv, UnityEngine.Vector2 uv2)
			{
				CVertex3Df vertex = new CVertex3Df(position, normal, uv, uv2);
				vertex.Index = mCount;
				Add(vertex);
			}
#endif

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершин
			/// </summary>
			/// <param name="positions">Позиции вершины</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertexes(IList<Vector3Df> positions)
			{
				for (var i = 0; i < positions.Count; i++)
				{
					var vertex = new CVertex3Df(positions[i]);
					vertex.Index = mCount;
					Add(vertex);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершин
			/// </summary>
			/// <param name="positions">Позиции вершины</param>
			/// <param name="uvs">Текстурные координаты вершин</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertexes(IList<Vector3Df> positions, IList<Vector2Df> uvs)
			{
				if (positions.Count != uvs.Count)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogError(String.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#else
					XLogger.LogError(String.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#endif
				}

				for (var i = 0; i < positions.Count; i++)
				{
					var vertex = new CVertex3Df(positions[i], uvs[i]);
					vertex.Index = mCount;
					Add(vertex);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вершин
			/// </summary>
			/// <param name="positions">Позиции вершины</param>
			/// <param name="normal">Единичная нормаль вершины</param>
			/// <param name="uvs">Текстурные координаты вершин</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddVertexes(IList<Vector3Df> positions, Vector3Df normal, IList<Vector2Df> uvs)
			{
				if (positions.Count != uvs.Count)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogError(String.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#else
					XLogger.LogError(String.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#endif
				}

				for (var i = 0; i < positions.Count; i++)
				{
					var vertex = new CVertex3Df(positions[i], normal, uvs[i]);
					vertex.Index = mCount;
					Add(vertex);
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================