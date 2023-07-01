//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема сериализации
// Подраздел: Подсистема сериализаторов данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializatorBinaryPrimitive.cs
*		Сериализация примитивных данных в бинарный поток.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.IO;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreSerialization
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий сериализацию примитивных данных в бинарный поток
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XSerializatorBinaryPrimitive
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись объекта примитивного типа в бинарный поток
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="type">Тип объекта</param>
			/// <param name="instance">Экземпляр объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteToBinary(BinaryWriter writer, Type type, System.Object instance)
			{
				switch (type.Name)
				{
					//
					//
					//
					case nameof(Boolean):
						{
							writer.Write((Boolean)instance);
						}
						break;
					case nameof(Byte):
						{
							writer.Write((Byte)instance);
						}
						break;
					case nameof(Char):
						{
							writer.Write((Char)instance);
						}
						break;
					case nameof(Int16):
						{
							writer.Write((Int16)instance);
						}
						break;
					case nameof(UInt16):
						{
							writer.Write((UInt16)instance);
						}
						break;
					case nameof(Int32):
						{
							writer.Write((Int32)instance);
						}
						break;
					case nameof(UInt32):
						{
							writer.Write((UInt32)instance);
						}
						break;
					case nameof(Int64):
						{
							writer.Write((Int64)instance);
						}
						break;
					case nameof(UInt64):
						{
							writer.Write((Int64)(UInt64)instance);
						}
						break;
					case nameof(Single):
						{
							writer.Write((Single)instance);
						}
						break;
					case nameof(Double):
						{
							writer.Write((Double)instance);
						}
						break;
					case nameof(Decimal):
						{
							writer.Write((Decimal)instance);
						}
						break;
					case nameof(String):
						{
							writer.Write((String)instance);
						}
						break;
					case nameof(DateTime):
						{
							writer.Write((DateTime)instance);
						}
						break;
					case nameof(TimeSpan):
						{
							writer.Write(((TimeSpan)instance).Ticks);
						}
						break;
					case nameof(Version):
						{
							writer.Write(((Version)instance).ToString());
						}
						break;
					case nameof(Uri):
						{
							writer.Write(((Uri)instance).ToString());
						}
						break;

					case nameof(TColor):
						{
							var color = (TColor)instance;
							writer.Write(color.ToRGBA());
						}
						break;
					case nameof(CVariant):
						{
							var variant = (CVariant)instance;
							writer.Write(variant.SerializeToString());
						}
						break;
					//
					// UnityEngine
					//
#if UNITY_2017_1_OR_NEWER
					case nameof(UnityEngine.Vector2):
						{
							UnityEngine.Vector2 vector = (UnityEngine.Vector2)instance;
							writer.Write(vector.x);
							writer.Write(vector.y);
						}
						break;
					case nameof(UnityEngine.Vector3):
						{
							UnityEngine.Vector3 vector = (UnityEngine.Vector3)instance;
							writer.Write(vector.x);
							writer.Write(vector.y);
							writer.Write(vector.z);
						}
						break;
					case nameof(UnityEngine.Vector4):
						{
							UnityEngine.Vector4 vector = (UnityEngine.Vector4)instance;
							writer.Write(vector.x);
							writer.Write(vector.y);
							writer.Write(vector.z);
							writer.Write(vector.w);
						}
						break;
					case nameof(UnityEngine.Vector2Int):
						{
							UnityEngine.Vector2Int vector = (UnityEngine.Vector2Int)instance;
							writer.Write(vector.x);
							writer.Write(vector.y);
						}
						break;
					case nameof(UnityEngine.Vector3Int):
						{
							UnityEngine.Vector3Int vector = (UnityEngine.Vector3Int)instance;
							writer.Write(vector.x);
							writer.Write(vector.y);
							writer.Write(vector.z);
						}
						break;
					case nameof(UnityEngine.Quaternion):
						{
							UnityEngine.Quaternion quaternion = (UnityEngine.Quaternion)instance;
							writer.Write(quaternion.x);
							writer.Write(quaternion.y);
							writer.Write(quaternion.z);
							writer.Write(quaternion.w);
						}
						break;
					case nameof(UnityEngine.Color):
						{
							UnityEngine.Color color = (UnityEngine.Color)instance;
							writer.Write(color.ToRGBA());
						}
						break;
					case nameof(UnityEngine.Color32):
						{
							UnityEngine.Color32 color = (UnityEngine.Color32)instance;
							writer.Write(color.ToRGBA());
						}
						break;
					case nameof(UnityEngine.Rect):
						{
							UnityEngine.Rect rect = (UnityEngine.Rect)instance;
							writer.Write(rect.x);
							writer.Write(rect.y);
							writer.Write(rect.width);
							writer.Write(rect.height);
						}
						break;
					case nameof(UnityEngine.RectInt):
						{
							UnityEngine.RectInt rect = (UnityEngine.RectInt)instance;
							writer.Write(rect.x);
							writer.Write(rect.y);
							writer.Write(rect.width);
							writer.Write(rect.height);
						}
						break;
					case nameof(UnityEngine.Bounds):
						{
							UnityEngine.Bounds bounds = (UnityEngine.Bounds)instance;
							writer.Write(bounds.min.x);
							writer.Write(bounds.min.y);
							writer.Write(bounds.min.z);
							writer.Write(bounds.max.x);
							writer.Write(bounds.max.y);
							writer.Write(bounds.max.z);
						}
						break;
					case nameof(UnityEngine.BoundsInt):
						{
							UnityEngine.BoundsInt bounds = (UnityEngine.BoundsInt)instance;
							writer.Write(bounds.min.x);
							writer.Write(bounds.min.y);
							writer.Write(bounds.min.z);
							writer.Write(bounds.max.x);
							writer.Write(bounds.max.y);
							writer.Write(bounds.max.z);
						}
						break;
#endif
					default:
						{
							// Проверка на перечисление
							if (type.IsEnum)
							{
								writer.Write((Int32)instance);
								break;
							}

							// Проверка на примитивный тип
							if (type.HasAttribute<LotusSerializeAsPrimitiveAttribute>())
							{
								MethodInfo method_info = type.GetMethod(
									LotusSerializeAsPrimitiveAttribute.SERIALIZETOSTRING, 
									BindingFlags.Public | BindingFlags.Instance);
								if (method_info != null)
								{
									var data = method_info.Invoke(instance, null).ToString();
									writer.Write(data);
								}
							}
						}
						break;
				}
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================