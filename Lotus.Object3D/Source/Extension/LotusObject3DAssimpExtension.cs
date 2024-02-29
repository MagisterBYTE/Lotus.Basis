
#if USE_WINDOWS
using System.Windows;
using System.Windows.Media;
using Media3D = System.Windows.Media.Media3D;
#endif

namespace Lotus.Object3D
{
    /**
     * \defgroup Object3DExtension Подсистема расширений методов
     * \ingroup Object3D
     * \brief Подсистема расширений методов обеспечивает взаимодействия между различными платформами и технологиями
		представления и отображения 3D объектов.
     * @{
     */
    /// <summary>
    /// Статический класс для реализации методов расширений библиотеки Assimp.
    /// </summary>
    public static class XAssimpExtension
    {
#if USE_ASSIMP
        #region Main methods
		/// <summary>
		/// Аппроксимация равенства векторов.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <param name="other">Вектор.</param>
		/// <param name="epsilon">Погрешность.</param>
		/// <returns>Статус равенства значений.</returns>
		public static Boolean Approximately(this Assimp.Vector3D @this, Assimp.Vector3D other, Double epsilon)
		{
			if ((Math.Abs(@this.X - other.X) < epsilon)
				&& (Math.Abs(@this.Y - other.Y) < epsilon)
				&& (Math.Abs(@this.Z - other.Z) < epsilon))
			{
				return true;
			}

			return false;
		}
        #endregion

        #region Convert methods
		/// <summary>
		/// Преобразование в тип Vector3D.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="Maths.Vector3D"/>.</returns>
		public static Maths.Vector3D ToVector3D(this Assimp.Vector3D @this)
		{
			return new Maths.Vector3D(@this.X, @this.Y, @this.Z);
		}

		/// <summary>
		/// Преобразование в тип Quaternion3D.
		/// </summary>
		/// <param name="this">Кватернион.</param>
		/// <returns>Объект <see cref="Maths.Quaternion3D"/>.</returns>
		public static Maths.Quaternion3D ToQuaternion3D(this Assimp.Quaternion @this)
		{
			return new Maths.Quaternion3D(@this.X, @this.Y, @this.Z);
		}

		/// <summary>
		/// Преобразование в тип Quaternion3Df.
		/// </summary>
		/// <param name="this">Кватернион.</param>
		/// <returns>Объект <see cref="Maths.Quaternion3Df"/>.</returns>
		public static Maths.Quaternion3Df ToQuaternion3Df(this Assimp.Quaternion @this)
		{
			return new Maths.Quaternion3Df(@this.X, @this.Y, @this.Z);
		}

		/// <summary>
		/// Преобразование в тип Vector3D.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="Media3D.Vector3D"/>.</returns>
		public static Media3D.Vector3D ToWinVector3D(this Assimp.Vector3D @this)
		{
			return new Media3D.Vector3D(@this.X, @this.Y, @this.Z);
		}

		/// <summary>
		/// Преобразование в тип Point3D.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="Media3D.Point3D"/>.</returns>
		public static Media3D.Point3D ToWinPoint3D(this Assimp.Vector3D @this)
		{
			return new Media3D.Point3D(@this.X, @this.Y, @this.Z);
		}

		/// <summary>
		/// Преобразование в тип Point.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="Point"/>.</returns>
		public static Point ToWinPoint(this Assimp.Vector3D @this)
		{
			return new Point(@this.X, @this.Y);
		}

		/// <summary>
		/// Преобразование в тип Vector.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="Vector"/>.</returns>
		public static Vector ToWinVector(this Assimp.Vector2D @this)
		{
			return new Vector(@this.X, @this.Y);
		}

		/// <summary>
		/// Преобразование в тип Point.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="Point"/>.</returns>
		public static Point ToWinPoint(this Assimp.Vector2D @this)
		{
			return new Point(@this.X, @this.Y);
		}

		/// <summary>
		/// Преобразование в тип Rect3D.
		/// </summary>
		/// <param name="this">Ограничивающий бокс.</param>
		/// <returns>Объект <see cref="Media3D.Rect3D"/>.</returns>
		public static Media3D.Rect3D ToWinRect3D(this SharpDX.BoundingBox @this)
		{
			return new Media3D.Rect3D(@this.Center.X, @this.Center.Y, @this.Center.Z,
				@this.Size.X, @this.Size.Y, @this.Size.Z);
		}

		/// <summary>
		/// Преобразование в тип Matrix3D.
		/// </summary>
		/// <param name="this">Матрица.</param>
		/// <returns>Объект <see cref="Media3D.Matrix3D"/>.</returns>
		public static Media3D.Matrix3D ToWinMatrix4D(this Assimp.Matrix4x4 @this)
		{
			return new Media3D.Matrix3D(@this.A1, @this.A2, @this.A3, @this.A4,
															@this.B1, @this.B2, @this.B3, @this.B4,
															@this.C1, @this.C2, @this.C3, @this.C4,
															@this.D1, @this.D2, @this.D3, @this.D4);
		}

		/// <summary>
		/// Преобразование в тип SharpDX.Vector3.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="SharpDX.Vector3"/>.</returns>
		public static SharpDX.Vector3 ToShVector3D(this Assimp.Vector3D @this)
		{
			return new SharpDX.Vector3(@this.X, @this.Y, @this.Z);
		}

		/// <summary>
		/// Преобразование в тип SharpDX.Vector2.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="SharpDX.Vector2"/>.</returns>
		public static SharpDX.Vector2 ToShVector2D(this Assimp.Vector3D @this)
		{
			return new SharpDX.Vector2(@this.X, @this.Y);
		}

		/// <summary>
		/// Преобразование в тип SharpDX.Vector2.
		/// </summary>
		/// <param name="this">Вектор.</param>
		/// <returns>Объект <see cref="SharpDX.Vector2"/>.</returns>
		public static SharpDX.Vector2 ToShVector2D(this Assimp.Vector2D @this)
		{
			return new SharpDX.Vector2(@this.X, @this.Y);
		}

		/// <summary>
		/// Преобразование в тип SharpDX.Color4.
		/// </summary>
		/// <param name="this">Цвет.</param>
		/// <returns>Объект <see cref="SharpDX.Color4"/>.</returns>
		public static SharpDX.Color4 ToShColor4(this Color @this)
		{
			return new SharpDX.Color4(@this.ScR, @this.ScG, @this.ScB, @this.ScA);
		}
        #endregion
#endif
    }
    /**@}*/
}