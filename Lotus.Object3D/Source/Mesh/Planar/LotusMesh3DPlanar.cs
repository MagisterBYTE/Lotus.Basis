using System;

using Lotus.Maths;

namespace Lotus.Object3D
{
    /**
     * \defgroup Object3DMeshPlanar Плоскостные трехмерные примитивы
     * \ingroup Object3DMesh
     * \brief Плоскостные трехмерные примитивы - это плоские примитивы которые создаются в какой либо определенной плоскости.
     * @{
     */
    /// <summary>
    /// Базовый плоскостной трехмерный примитив.
    /// </summary>
    [Serializable]
    public class MeshPlanar3Df : Mesh3Df
    {
        #region Static methods
        #endregion

        #region Fields
        protected internal Maths.TDimensionPlane _planeType;
        #endregion

        #region Properties
        /// <summary>
        /// Тип плоскости в которой расположен примитив.
        /// </summary>
        public Maths.TDimensionPlane PlaneType
        {
            get { return _planeType; }
            set
            {
                _planeType = value;
                ChangePlaneType();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public MeshPlanar3Df()
            : base()
        {
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение плоскости в которой расположен примитив.
        /// </summary>
        protected virtual void ChangePlaneType()
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение вектора перпендикулярного плоскости.
        /// </summary>
        /// <returns>Вектор.</returns>
        public Vector3Df GetPerpendicularVector()
        {
            if (_planeType == TDimensionPlane.XZ)
            {
                return Vector3Df.Up;
            }
            else
            {
                if (_planeType == TDimensionPlane.ZY)
                {
                    return Vector3Df.Right;
                }
                else
                {
                    return -Vector3Df.Forward;
                }
            }
        }

        /// <summary>
        /// Получение вектора на соответствующей плоскости на основу указанных двухмерных координат.
        /// </summary>
        /// <param name="x">Координта по X.</param>
        /// <param name="y">Координта по Y.</param>
        /// <returns>Вектор.</returns>
        public Vector3Df GetPlaneVector(float x, float y)
        {
            if (_planeType == TDimensionPlane.XZ)
            {
                return new Vector3Df(x, 0, y);
            }
            else
            {
                if (_planeType == TDimensionPlane.ZY)
                {
                    return new Vector3Df(0, y, x);
                }
                else
                {
                    return new Vector3Df(x, y, 0);
                }
            }
        }

        /// <summary>
        /// Получение вектора на соответствующей плоскости на основу указанных двухмерных координат.
        /// </summary>
        /// <param name="x">Координта по X.</param>
        /// <param name="y">Координта по Y.</param>
        /// <param name="vectorSave">Вектор для сохранения значимой координаты.</param>
        /// <returns>Вектор.</returns>
        public Vector3Df GetPlaneVector(float x, float y, Vector3Df vectorSave)
        {
            if (_planeType == TDimensionPlane.XZ)
            {
                return new Vector3Df(x, vectorSave.Y, y);
            }
            else
            {
                if (_planeType == TDimensionPlane.ZY)
                {
                    return new Vector3Df(vectorSave.X, y, x);
                }
                else
                {
                    return new Vector3Df(x, y, vectorSave.Z);
                }
            }
        }
        #endregion
    }
    /**@}*/
}