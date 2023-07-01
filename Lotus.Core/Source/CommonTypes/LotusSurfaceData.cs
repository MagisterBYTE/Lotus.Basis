//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема общих типов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSurfaceData.cs
*		Определения поверхности как двухмерного универсального массива данных.
*		Реализация класса поверхности как двухмерного массива данных. Может заполнятся различным образом, используется
*	как некая абстракция изображения. Основной свойство то что при изменении его размеров данные могут быть интерполированы.
*	В Unity в основном планируется использовать в режиме редактора.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreCommonTypes
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс реализующий поверхность как двухмерный массив данных
		/// </summary>
		/// <remarks>
		/// Одна из основных функций поверхности - интерполяция данных при изменении ее размеров
		/// </remarks>
		/// <typeparam name="TType">Тип данных поверхности</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class SurfaceData<TType> where TType : struct
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal TType[] mData;
			protected internal Int32 mWidth;
			protected internal Int32 mHeight;
			protected internal Int32 mRank;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Данные поверхности
			/// </summary>
			public TType[] Data
			{
				get { return mData; }
			}

			/// <summary>
			/// Количество элементов в поверхности
			/// </summary>
			public Int32 Count
			{
				get { return mData.Length; }
			}

			/// <summary>
			/// Ширина поверхности при двухмерной организации данных
			/// </summary>
			public Int32 Width
			{
				get { return mWidth; }
				set { mWidth = value; }
			}

			/// <summary>
			/// Высота поверхности при двухмерной организации данных
			/// </summary>
			public Int32 Height
			{
				get { return mHeight; }
				set { mHeight = value; }
			}

			/// <summary>
			/// Размерность поверхности
			/// </summary>
			public Int32 Rank
			{
				get { return mRank; }
				set { mRank = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public SurfaceData()
			{
				mData = new TType[] { default };
				mWidth = 1;
				mHeight = 1;
				mRank = 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public SurfaceData(TType[] data)
			{
				mData = new TType[data.Length];
				for (var i = 0; i < mData.Length; i++)
				{
					mData[i] = data[i];
				}
				mRank = 1;
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация элементов поверхности
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент поверхности</returns>
			//---------------------------------------------------------------------------------------------------------
			public TType this[Int32 index]
			{
				get
				{
					return mData[index];
				}
				set
				{
					mData[index] = value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация элементов поверхности при двухмерной организации
			/// </summary>
			/// <param name="x">Индекс элемента по x</param>
			/// <param name="y">Индекс элемента по y</param>
			/// <returns>Элемент поверхности</returns>
			//---------------------------------------------------------------------------------------------------------
			public TType this[Int32 x, Int32 y]
			{
				get
				{
					return mData[x + (y * mWidth)];
				}
				set
				{
					mData[x + (y * mWidth)] = value;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных
			/// </summary>
			/// <param name="count">Количество элементов</param>
			/// <param name="saveOldData">Сохранять ли старые данные</param>
			//---------------------------------------------------------------------------------------------------------
			public void Set(Int32 count, Boolean saveOldData)
			{
				if (saveOldData)
				{
					var data = new TType[count];
					var count_copy = count;
					if (count > Count)
					{
						count_copy = Count;
					}
					for (var i = 0; i < count_copy; i++)
					{
						data[i] = mData[i];
					}

					mData = data;
				}
				else
				{
					mData = new TType[count];
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных из источника
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromData(TType[] data)
			{
				mData = new TType[data.Length];
				for (var i = 0; i < mData.Length; i++)
				{
					mData[i] = data[i];
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значений поверхности
			/// </summary>
			/// <param name="newWidth">Новая ширина</param>
			/// <param name="newHeight">Новая высота</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 newWidth, Int32 newHeight)
			{
				var temp_data = new TType[newWidth * newHeight];

				var factor_x = mWidth / (Double)newWidth;
				var factor_y = mHeight / (Double)newHeight;

				for (var x = 0; x < newWidth; ++x)
				{
					for (var y = 0; y < newHeight; ++y)
					{
						var gx = (Int32)Math.Floor(x * factor_x);
						var gy = (Int32)Math.Floor(y * factor_y);
						TType val = this[gx, gy];
						temp_data[x + (y * newWidth)] = val;
					}
				}

				mData = temp_data;
				mWidth = newWidth;
				mHeight = newHeight;
				mRank = 2;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Поверхность данных для целых значения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSurfaceInt : SurfaceData<Int32>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceInt()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceInt(Int32[] data)
			{
				SetFromData(data);
				mRank = 1;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значений поверхности
			/// </summary>
			/// <param name="newWidth">Новая ширина</param>
			/// <param name="newHeight">Новая высота</param>
			/// <param name="useBilinear">Использовать билинейную интерполяцию</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 newWidth, Int32 newHeight, Boolean useBilinear)
			{
				var temp_data = new Int32[newWidth * newHeight];

				var factor_x = mWidth / (Double)newWidth;
				var factor_y = mHeight / (Double)newHeight;

				if (useBilinear)
				{
					Double fraction_x, fraction_y, one_minus_x, one_minus_y;
					Int32 ceil_x, ceil_y, floor_x, floor_y;
					Int32 c1, c2, c3, c4;
					Int32 b1, b2;

					for (var x = 0; x < newWidth; ++x)
					{
						for (var y = 0; y < newHeight; ++y)
						{
							// Setup
							floor_x = (Int32)Math.Floor(x * factor_x);
							floor_y = (Int32)Math.Floor(y * factor_y);

							ceil_x = floor_x + 1;
							if (ceil_x >= mWidth)
							{
								ceil_x = floor_x;
							}

							ceil_y = floor_y + 1;
							if (ceil_y >= mHeight)
							{
								ceil_y = floor_y;
							}

							fraction_x = (x * factor_x) - floor_x;
							fraction_y = (y * factor_y) - floor_y;
							one_minus_x = 1.0 - fraction_x;
							one_minus_y = 1.0 - fraction_y;

							c1 = this[floor_x, floor_y];
							c2 = this[ceil_x, floor_y];
							c3 = this[floor_x, ceil_y];
							c4 = this[ceil_x, ceil_y];

							b1 = (Int32)((one_minus_x * c1) + (fraction_x * c2));
							b2 = (Int32)((one_minus_x * c3) + (fraction_x * c4));
							var val = (Int32)((one_minus_y * b1) + (fraction_y * b2));


							temp_data[x + (y * newWidth)] = val;
						}
					}
				}
				else
				{
					for (var x = 0; x < newWidth; ++x)
					{
						for (var y = 0; y < newHeight; ++y)
						{
							var gx = (Int32)Math.Floor(x * factor_x);
							var gy = (Int32)Math.Floor(y * factor_y);
							var val = this[gx, gy];
							temp_data[x + (y * newWidth)] = val;
						}
					}
				}

				mData = temp_data;
				mWidth = newWidth;
				mHeight = newHeight;
				mRank = 2;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Поверхность данных для цвета
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSurfaceColor : SurfaceData<TColor>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor(TColor[] data)
			{
				SetFromData(data);
				mRank = 1;
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor(UnityEngine.Color32[] data)
			{
				SetFromData(data);
				mRank = 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor(UnityEngine.Texture2D texture)
			{
				if (texture != null)
				{
					this.SetFromData(texture.GetPixels32());
					mWidth = texture.width;
					mHeight = texture.height;
					mRank = 2;
				}
				else
				{
					mData = new TColor[] { TColor.White };
					mWidth = 1;
					mHeight = 1;
					mRank = 1;
				}
			}
#endif
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных из источника
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromData(UnityEngine.Color32[] data)
			{
				mData = new TColor[data.Length];
				for (Int32 i = 0; i < mData.Length; i++)
				{
					mData[i].A = data[i].a;
					mData[i].R = data[i].r;
					mData[i].G = data[i].g;
					mData[i].B = data[i].b;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных из источника (текстуры)
			/// </summary>
			/// <param name="texture">Текстура</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromData(UnityEngine.Texture2D texture)
			{
				if (texture != null)
				{
					this.SetFromData(texture.GetPixels32());
					mWidth = texture.width;
					mHeight = texture.height;
					mRank = 2;
				}
			}
#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значений поверхности
			/// </summary>
			/// <param name="newWidth">Новая ширина</param>
			/// <param name="newHeight">Новая высота</param>
			/// <param name="useBilinear">Использовать билинейную интерполяцию</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 newWidth, Int32 newHeight, Boolean useBilinear)
			{
				var temp_data = new TColor[newWidth * newHeight];

				var factor_x = mWidth / (Single)newWidth;
				var factor_y = mHeight / (Single)newHeight;

				if (useBilinear)
				{
					Single fraction_x, fraction_y, one_minus_x, one_minus_y;
					Int32 ceil_x, ceil_y, floor_x, floor_y;
					TColor c1, c2, c3, c4;
					TColor b1, b2;

					for (var x = 0; x < newWidth; ++x)
					{
						for (var y = 0; y < newHeight; ++y)
						{
							// Setup
							floor_x = (Int32)Math.Floor(x * factor_x);
							floor_y = (Int32)Math.Floor(y * factor_y);

							ceil_x = floor_x + 1;
							if (ceil_x >= mWidth)
							{
								ceil_x = floor_x;
							}

							ceil_y = floor_y + 1;
							if (ceil_y >= mHeight)
							{
								ceil_y = floor_y;
							}

							fraction_x = (x * factor_x) - floor_x;
							fraction_y = (y * factor_y) - floor_y;
							one_minus_x = 1.0f - fraction_x;
							one_minus_y = 1.0f - fraction_y;

							c1 = this[floor_x, floor_y];
							c2 = this[ceil_x, floor_y];
							c3 = this[floor_x, ceil_y];
							c4 = this[ceil_x, ceil_y];

							b1 = TColor.Add(TColor.Scale(c1, one_minus_x), TColor.Scale(c2, fraction_x));
							b2 = TColor.Add(TColor.Scale(c3, one_minus_x), TColor.Scale(c4, fraction_x));
							var val = TColor.Add(TColor.Scale(b1, one_minus_y), TColor.Scale(b2, fraction_y));

							temp_data[x + (y * newWidth)] = val;
						}
					}
				}
				else
				{
					for (var x = 0; x < newWidth; ++x)
					{
						for (var y = 0; y < newHeight; ++y)
						{
							var gx = (Int32)Math.Floor(x * factor_x);
							var gy = (Int32)Math.Floor(y * factor_y);
							TColor val = this[gx, gy];
							temp_data[x + (y * newWidth)] = val;
						}
					}
				}

				mData = temp_data;
				mWidth = newWidth;
				mHeight = newHeight;
				mRank = 2;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================