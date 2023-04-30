//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема генерации псевдослучайных значений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathRandomGuarantee.cs
*		Генератор получения гарантированных вероятностных значений в указанном интервале.
*		Реализация генератора который обеспечивает точное в процентной отношении получения гарантированных вероятностных
*	значений в указанном интервале.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup MathRandom
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Генератор получения гарантированных вероятностных значений в указанном интервале
		/// </summary>
		/// <remarks>
		/// Реализация генератора который обеспечивает точное в процентной отношении получения гарантированных
		/// вероятностных значений в указанном интервале.
		/// <para>
		/// Разберем более подробно использование объектов данного типа
		/// </para>
		/// <para>
		/// Например, нам нужно получить объект с вероятностью 25% на 100 вызовов. В этом случае надо использовать
		/// AddProbability(1, 25). Теперь если вызвать NextProbability() - 100 раз, гарантировано 
		/// будут возвращено 25 раз значение 1, т.е. значение индекса
		/// </para>
		/// <para>
		/// Например, нам нужно получить 1-ю вещь с вероятностью 25%, 2-ю вещь с вероятностью 50% и 3-ю вещь с вероятностью 25%
		/// В этом случае надо использовать AddProbabilityList(25, 50, 25), здесь индексы присваиваются автоматически начиная с нулю
		/// Обратите внимания в сумме проценты дают 100% - это значит каждый раз будет выпадать какая-либо вещь, 
		/// т.е. будет возвращаться индекс 0 (что соответствует 1 вещи) или 1 или 2
		/// </para>
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CRandomGuarantee
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal Int32 mCapacity;
			internal Int32[] mData;
			internal List<Int32> mProbability;
			internal Int32 mCurrentIndex;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Емкость генератора
			/// </summary>
			public Int32 Capacity
			{
				get { return mCapacity; }
			}

			/// <summary>
			/// Список вероятностей в процентах
			/// </summary>
			public List<Int32> Probability
			{
				get { return mProbability; }
			}

			/// <summary>
			/// Текущий индекс данных
			/// </summary>
			public Int32 CurrentIndex
			{
				get { return mCurrentIndex; }
			}

			/// <summary>
			/// Текущее значение данных
			/// </summary>
			public Int32 CurrentValue
			{
				get
				{
					if (mCurrentIndex == -1)
					{
						return mCurrentIndex;
					}
					else
					{
						return mData[mCurrentIndex];
					}
				}
			}

			/// <summary>
			/// Данные
			/// </summary>
			public Int32[] Data
			{
				get { return mData; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует базовую емкость генератора
			/// </summary>
			/// <param name="capacity">Емкость генератора</param>
			//---------------------------------------------------------------------------------------------------------
			public CRandomGuarantee(Int32 capacity = 100)
			{
				mCapacity = capacity;
				mData = new Int32[mCapacity];
				mProbability = new List<Int32>();
				mCurrentIndex = -1;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перезапуск данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Reset()
			{
				for (var i = 0; i < mProbability.Count; i++)
				{
					mData[i] = mProbability[i];
				}

				for (var ir = mProbability.Count; ir < mCapacity; ir++)
				{
					mData[ir] = -1;
				}

				var rnd = new Random();
				var n = mData.Length;
				while (n > 1)
				{
					n--;
					var k = rnd.Next(n + 1);

					var old = mData[n];
					mData[n] = mData[k];
					mData[k] = old;
				}

				mCurrentIndex = -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переопределение емкости генератора
			/// </summary>
			/// <remarks>
			/// Автоматически очищается список вероятностей, его надо создавать по-новому
			/// </remarks>
			/// <param name="capacity">Емкость генератора</param>
			//---------------------------------------------------------------------------------------------------------
			public void ResetCapacity(Int32 capacity)
			{
				mCapacity = capacity;
				mData = new Int32[mCapacity];
				mProbability.Clear();
				mCurrentIndex = -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вероятности значения
			/// </summary>
			/// <remarks>
			/// Индекс значения и есть статус выпадения этого значения. Должен быть больше нуля
			/// </remarks>
			/// <param name="index">Индекс вероятности</param>
			/// <param name="probability">Вероятность значения в процентах</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddProbability(Int32 index, Int32 probability)
			{
				var count = probability * mCapacity / 100;
				for (var i = 0; i < count; i++)
				{
					mProbability.Add(index);
				}

				Reset();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление вероятности значения списком
			/// </summary>
			/// <remarks>
			/// Здесь индексы присваиваются автоматически начиная с нулю
			/// </remarks>
			/// <param name="probability">Вероятность значения в процентах</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddProbabilityList(params Int32[] probability)
			{
				for (var index = 0; index < probability.Length; index++)
				{
					var count = probability[index] * mCapacity / 100;
					for (var i = 0; i < count; i++)
					{
						mProbability.Add(index);
					}
				}

				Reset();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка списка вероятностей
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ClearProbability()
			{
				mProbability.Clear();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение следующий вероятности
			/// </summary>
			/// <returns>Следующая вероятность</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 NextProbability()
			{
				if (mCurrentIndex == mCapacity - 1)
				{
					mCurrentIndex = 0;
				}
				else
				{
					mCurrentIndex++;
				}

				return mData[mCurrentIndex];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение следующий вероятности в перезапуск по новому в конце цикла
			/// </summary>
			/// <returns>Следующая вероятность</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 NextProbabilityAndReset()
			{
				if (mCurrentIndex == mCapacity - 1)
				{
					Reset();
					mCurrentIndex = 0;
				}
				else
				{
					mCurrentIndex++;
				}

				return mData[mCurrentIndex];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на совпадение данной вероятности
			/// </summary>
			/// <param name="index">Индекс вероятности</param>
			/// <returns>Статус вероятность</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean CheckProbability(Int32 index)
			{
				if (mCurrentIndex == mCapacity - 1)
				{
					mCurrentIndex = 0;
				}
				else
				{
					mCurrentIndex++;
				}

				return mData[mCurrentIndex] == index;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных вероятностей в виде списка строк
			/// </summary>
			/// <returns>Список строк</returns>
			//---------------------------------------------------------------------------------------------------------
			public IList<String> GetDataStrings()
			{
				var lines = new String[mData.Length];

				for (var i = 0; i < mData.Length; i++)
				{
					lines[i] = "i = " + i.ToString() + ", value = " + Data[i].ToString();
				}

				return lines;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================