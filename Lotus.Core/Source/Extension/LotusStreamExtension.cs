//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusStreamExtension.cs
*		Методы расширения для бинарного потока и текстового потока.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.IO;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreExtension
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения для бинарного потока
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XBinaryStreamExtension
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Нулевые данные по значению в контексте записи/чтения ссылочных объектов бинарного потока
			/// </summary>
			public const Int32 ZERO_DATA = -1;

			/// <summary>
			/// Существующие данные по значению в контексте записи/чтения ссылочных объектов бинарного потока
			/// </summary>
			public const Int32 EXIST_DATA = 1;

			/// <summary>
			/// Метка успешности
			/// </summary>
			public const Int32 SUCCESS_LABEL = 198418;
			#endregion

			#region ======================================= ЗАПИСЬ ДАННЫХ =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись структуры DateTime
			/// </summary>
			/// <param name="writer">Средство записи данных в бинарном формате</param>
			/// <param name="dateTime">Список целых значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Write(this BinaryWriter writer, DateTime dateTime)
			{
				writer.Write(dateTime.Ticks);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка целых значений
			/// </summary>
			/// <param name="writer">Средство записи данных в бинарном формате</param>
			/// <param name="integers">Список целых значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Write(this BinaryWriter writer, IList<Int32> integers)
			{
				// Записываем данные по порядку
				if (integers != null && integers.Count > 0)
				{
					for (var i = 0; i < integers.Count; i++)
					{
						writer.Write(integers[i]);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка вещественных значений одинарной точности
			/// </summary>
			/// <param name="writer">Средство записи данных в бинарном формате</param>
			/// <param name="floats">Список вещественных значений одинарной точности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Write(this BinaryWriter writer, IList<Single> floats)
			{
				// Записываем данные по порядку
				if (floats != null && floats.Count > 0)
				{
					for (var i = 0; i < floats.Count; i++)
					{
						writer.Write(floats[i]);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка вещественных значений двойной точности
			/// </summary>
			/// <param name="writer">Средство записи данных в бинарном формате</param>
			/// <param name="doubles">Список вещественных значений двойной точности</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Write(this BinaryWriter writer, IList<Double> doubles)
			{
				// Записываем данные по порядку
				if (doubles != null && doubles.Count > 0)
				{
					for (var i = 0; i < doubles.Count; i++)
					{
						writer.Write(doubles[i]);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка примитивных типов данных
			/// </summary>
			/// <param name="writer">Средство записи данных в бинарном формате</param>
			/// <param name="primitives">Список примитивных данных</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Write<TPrimitive>(this BinaryWriter writer, IList<TPrimitive> primitives)
			{
				if (primitives != null && primitives.Count > 0)
				{
					Type type_item = typeof(TPrimitive);

					// Перечисление
					if (type_item.IsEnum)
					{
						// Записываем данные по порядку
						for (var i = 0; i < primitives.Count; i++)
						{
							writer.Write((Int32)(System.Object)primitives[i]);
						}
					}
					else
					{
						TypeCode type_code = Type.GetTypeCode(type_item);
						switch (type_code)
						{
							case TypeCode.Empty:
								break;
							case TypeCode.Object:
								break;
							case TypeCode.DBNull:
								break;
							case TypeCode.Boolean:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Boolean)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Char:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Char)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.SByte:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((SByte)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Byte:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Byte)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Int16:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Int16)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.UInt16:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((UInt16)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Int32:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Int32)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.UInt32:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((UInt32)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Int64:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Int64)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.UInt64:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((UInt64)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Single:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Single)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Double:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Double)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.Decimal:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((Decimal)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.DateTime:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((DateTime)(System.Object)primitives[i]);
									}
								}
								break;
							case TypeCode.String:
								{
									// Записываем данные по порядку
									for (var i = 0; i < primitives.Count; i++)
									{
										writer.Write((String)(System.Object)primitives[i]);
									}
								}
								break;
							default:
								break;
						}
					}
				}
			}
			#endregion

			#region ======================================= ЧТЕНИЕ ДАННЫХ =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение структуры DateTime
			/// </summary>
			/// <param name="reader">Средство чтения данных в бинарном формате</param>
			/// <returns>Объект DateTime</returns>
			//---------------------------------------------------------------------------------------------------------
			public static DateTime ReadDateTime(this BinaryReader reader)
			{
				return DateTime.FromBinary(reader.ReadInt64());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива целых значений
			/// </summary>
			/// <param name="reader">Средство чтения данных в бинарном формате</param>
			/// <param name="count">Количество элементов</param>
			/// <returns>Массив целых значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32[] ReadIntegers(this BinaryReader reader, Int32 count)
			{
				// Создаем массив
				var integers = new Int32[count];

				// Читаем данные по порядку
				for (var i = 0; i < count; i++)
				{
					integers[i] = reader.ReadInt32();
				}

				return integers;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива вещественных значений одинарной точности
			/// </summary>
			/// <param name="reader">Средство чтения данных в бинарном формате</param>
			/// <param name="count">Количество элементов</param>
			/// <returns>Массив вещественных значений одинарной точности</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single[] ReadFloats(this BinaryReader reader, Int32 count)
			{
				// Создаем массив
				var floats = new Single[count];

				// Читаем данные по порядку
				for (var i = 0; i < count; i++)
				{
					floats[i] = reader.ReadSingle();
				}

				return floats;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива вещественных значений двойной точности
			/// </summary>
			/// <param name="reader">Средство чтения данных в бинарном формате</param>
			/// <param name="count">Количество элементов</param>
			/// <returns>Массив вещественных значений двойной точности</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double[] ReadDoubles(this BinaryReader reader, Int32 count)
			{
				// Создаем массив
				var doubles = new Double[count];

				// Читаем данные по порядку
				for (var i = 0; i < count; i++)
				{
					doubles[i] = reader.ReadDouble();
				}

				return doubles;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива примитивных типов данных
			/// </summary>
			/// <remarks>
			/// К примитивными данным относятся все числовые типы, строковой тип, логический тип и перечисление
			/// </remarks>
			/// <param name="reader">Средство чтения данных в бинарном формате</param>
			/// <param name="count">Количество элементов</param>
			/// <returns>Массив примитивных данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TPrimitive[] ReadPimitives<TPrimitive>(this BinaryReader reader, Int32 count)
			{
				Type type_item = typeof(TPrimitive);

				// Создаем массив
				var primitives = new TPrimitive[count];

				// Перечисление
				if (type_item.IsEnum)
				{
					// Читаем данные по порядку
					for (var i = 0; i < count; i++)
					{
						primitives[i] = (TPrimitive)(System.Object)XConverter.ToEnumOfType(type_item, reader.ReadInt32());
					}
				}
				else
				{
					TypeCode type_code = Type.GetTypeCode(type_item);
					switch (type_code)
					{
						case TypeCode.Empty:
							break;
						case TypeCode.Object:
							break;
						case TypeCode.DBNull:
							break;
						case TypeCode.Boolean:
							{
								var bytes = reader.ReadBytes(count * sizeof(Boolean));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Char:
							{
								var bytes = reader.ReadBytes(count * sizeof(Char));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.SByte:
							{
								var bytes = reader.ReadBytes(count * sizeof(SByte));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Byte:
							{
								var bytes = reader.ReadBytes(count * sizeof(Byte));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Int16:
							{
								var bytes = reader.ReadBytes(count * sizeof(Int16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.UInt16:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Int32:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.UInt32:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Int64:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.UInt64:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Single:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Double:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.Decimal:
							{
								var bytes = reader.ReadBytes(count * sizeof(UInt16));
								Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
							}
							break;
						case TypeCode.DateTime:
							{
								for (var i = 0; i < count; i++)
								{
									primitives[i] = (TPrimitive)(System.Object)reader.ReadDateTime();
								}
							}
							break;
						case TypeCode.String:
							{
								for (var i = 0; i < count; i++)
								{
									primitives[i] = (TPrimitive)(System.Object)reader.ReadString();
								}
							}
							break;
						default:
							break;
					}
				}


				return primitives;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения для текстового потока
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XTextStreamExtension
		{
			#region ======================================= ЗАПИСЬ ДАННЫХ =============================================
			#endregion

			#region ======================================= ЧТЕНИЕ ДАННЫХ =============================================
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================