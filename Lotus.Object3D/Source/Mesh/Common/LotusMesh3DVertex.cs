using System;
using System.Collections.Generic;

using Lotus.Core;
using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshCommon
	*@{*/
    /// <summary>
    /// Вершина трёхмерной полигональной сетки(меша).
    /// </summary>
    /// <remarks>
    /// На ряду с основным компонентом вершины - позицией в трехмерном пространстве каждая вершина содержит также нормаль, 
    /// цвет, текстурные координаты, а также дополнительные данные и вспомогательную информацию
    /// </remarks>
    [Serializable]
    public struct Vertex3Df : ILotusMeshOperaiton, IComparable<Vertex3Df>, IEquatable<Vertex3Df>
    {
        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров вершины.
        /// </summary>
        public static string ToStringFormat = "{0} [{1:0.000}; {2:0.000}; {3:0.000}]";
        #endregion

        #region Fields
        /// <summary>
        /// Индекс вершины в списке вершин.
        /// </summary>
        public int Index;

        /// <summary>
        /// Позиция вершины.
        /// </summary>
        public Vector3Df Position;

        /// <summary>
        /// Нормаль вершины.
        /// </summary>
        public Vector3Df Normal;

        /// <summary>
        /// Тангентс вершины.
        /// </summary>
        public Vector3Df Tangent;

        /// <summary>
        /// Координаты текстурной развёртки основного канала.
        /// </summary>
        public Vector2Df UV;

        /// <summary>
        /// Координаты текстурной развёртки дополнительного канала.
        /// </summary>
        public Vector2Df UV2;

        /// <summary>
        /// Координаты текстурной развёртки дополнительного канала.
        /// </summary>
        public Vector2Df UV3;

        /// <summary>
        /// Статус уникальной вершины.
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
        /// Также применяем правило, что в списке вершин <see cref="ListVertex3D"/> все вершины должны быть уникальными
        /// </para>
        /// </remarks>
        public bool IsUnique;
        #endregion

        #region Properties
        /// <summary>
        /// Тип структурного элемента меша.
        /// </summary>
        public readonly TMeshElement MeshElement { get { return TMeshElement.Vertex; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        public Vertex3Df(Vector3Df position)
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

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="uv">Текстурные координаты вершины.</param>
        public Vertex3Df(Vector3Df position, Vector2Df uv)
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

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="normal">Единичная нормаль вершины.</param>
        public Vertex3Df(Vector3Df position, Vector3Df normal)
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

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="normal">Единичная нормаль вершины.</param>
        /// <param name="uv">Текстурные координаты вершины.</param>
        public Vertex3Df(Vector3Df position, Vector3Df normal, Vector2Df uv)
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

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="normal">Единичная нормаль вершины.</param>
        /// <param name="uv">Текстурные координаты вершины.</param>
        /// <param name="uv2">Вторые текстурные координаты вершины.</param>
        public Vertex3Df(Vector3Df position, Vector3Df normal, Vector2Df uv, Vector2Df uv2)
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
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="position">Позиция вершины.</param>
		/// <param name="normal">Единичная нормаль вершины.</param>
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

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="position">Позиция вершины.</param>
		/// <param name="normal">Единичная нормаль вершины.</param>
		/// <param name="uv">Текстурные координаты вершины.</param>
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

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="position">Позиция вершины.</param>
		/// <param name="normal">Единичная нормаль вершины.</param>
		/// <param name="uv">Текстурные координаты вершины.</param>
		/// <param name="uv2">Вторые текстурные координаты вершины.</param>
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

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is Vertex3Df vertex)
            {
                return Equals(vertex);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства вершин по значению позиции.
        /// </summary>
        /// <remarks>
        /// Вершины равные если равны их позиции в трехмерном пространстве.
        /// </remarks>
        /// <param name="other">Сравниваемая вершина.</param>
        /// <returns>Статус равенства вершин.</returns>
        public readonly bool Equals(Vertex3Df other)
        {
            return Vector3Df.Approximately(in Position, in other.Position, XMeshSetting.Eplsilon_f);
        }

        /// <summary>
        /// Сравнение вершин для упорядочивания.
        /// </summary>
        /// <remarks>
        /// Сравнение вершин происходит по индексу.
        /// </remarks>
        /// <param name="other">Вершина.</param>
        /// <returns>Статус сравнения вершин.</returns>
        public readonly int CompareTo(Vertex3Df other)
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

        /// <summary>
        /// Получение хеш-кода вершины.
        /// </summary>
        /// <returns>Хеш-код вершины.</returns>
        public override readonly int GetHashCode()
        {
            return Position.X.GetHashCode() ^ Position.Y.GetHashCode() ^ Position.Z.GetHashCode();
        }

        /// <summary>
        /// Получение хеш-кода указанной вершины.
        /// </summary>
        /// <param name="obj">Вершина.</param>
        /// <returns>Хеш-код вершины.</returns>
        public readonly int GetHashCode(Vertex3Df obj)
        {
            return obj.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Позиция вершины.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, Index, Position.X, Position.Y, Position.Z);
        }
        #endregion

        #region ILotusMeshOperaiton methods
        /// <summary>
        /// Смещение вершины.
        /// </summary>
        /// <param name="offset">Вектор смещения.</param>
        public void Move(Vector3Df offset)
        {
            Position += offset;
        }

        /// <summary>
        /// Врашение вершины.
        /// </summary>
        /// <param name="rotation">Кватернион вращения.</param>
        public void Rotate(Quaternion3Df rotation)
        {
            Position = rotation.TransformVector(in Position);
            Normal = rotation.TransformVector(in Normal);
        }

        /// <summary>
        /// Однородное масштабирование вершины.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(float scale)
        {
            Position *= scale;
        }

        /// <summary>
        /// Масштабирование вершины.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(Vector3Df scale)
        {
            Position = Vector3Df.Scale(Position, scale);
            Normal = Vector3Df.Scale(Normal, scale).Normalized;
        }

        /// <summary>
        /// Обратить нормали.
        /// </summary>
        public void FlipNormals()
        {
            Normal = -Normal;
        }

        /// <summary>
        /// Обратить развёртку текстурных координат по горизонтали.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public void FlipUVHorizontally(int channel = 0)
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

        /// <summary>
        /// Обратить развёртку текстурных координат по вертикали.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public void FlipUVVertically(int channel = 0)
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

        #region Main methods
        /// <summary>
        /// Полное дублирование вершины.
        /// </summary>
        /// <remarks>
        /// У дубликата индекс вершины всегда равен -1.
        /// </remarks>
        /// <returns>Дубликат вершины.</returns>
        public readonly Vertex3Df Duplicate()
        {
            var copy = (Vertex3Df)MemberwiseClone();
            copy.Index = -1;
            return copy;
        }

        /// <summary>
        /// Смещение вершины вдоль нормали.
        /// </summary>
        /// <param name="offset">Размер смещения.</param>
        public void OffsetFromNormal(float offset)
        {
            Position = Position + (Normal * offset);
        }

        /// <summary>
        /// Получение вершины смещенной вдоль нормали.
        /// </summary>
        /// <param name="offset">Размер смещения.</param>
        /// <returns>Смещенная вершина.</returns>
        public readonly Vertex3Df GetVertexOffsetFromNormal(float offset)
        {
            var copy = Duplicate();
            copy.Position = Position + (Normal * offset);
            return copy;
        }
        #endregion
    }

    /// <summary>
    /// Вспомогательный класс реализующий список вершин.
    /// </summary>
    [Serializable]
    public class ListVertex3D : ListArray<Vertex3Df>, ILotusMeshOperaiton
    {
        #region Properties
        /// <summary>
        /// Тип структурного элемента меша.
        /// </summary>
        public TMeshElement MeshElement { get { return TMeshElement.Vertex; } }

        /// <summary>
        /// Список вершин для прямого доступа.
        /// </summary>
        public Vertex3Df[] Vertices { get { return _arrayOfItems; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public ListVertex3D()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="list">Список вершин.</param>
        public ListVertex3D(params Vector3Df[] list)
        {
            for (var i = 0; i < list.Length; i++)
            {
                AddVertex(list[i]);
            }
        }
        #endregion

        #region ILotusMeshOperaiton methods
        /// <summary>
        /// Смещение вершин.
        /// </summary>
        /// <param name="offset">Вектор смещения.</param>
        public void Move(Vector3Df offset)
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Position += offset;
            }
        }

        /// <summary>
        /// Врашение вершин.
        /// </summary>
        /// <param name="rotation">Кватернион вращения.</param>
        public void Rotate(Quaternion3Df rotation)
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Position = rotation.TransformVector(in _arrayOfItems[i].Position);
                _arrayOfItems[i].Normal = rotation.TransformVector(in _arrayOfItems[i].Normal);
            }
        }

        /// <summary>
        /// Врашение вершин вокруг оси X.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <param name="isCenter">Относительно геометрического центра.</param>
        public void RotateFromX(float angle, bool isCenter)
        {
            if (isCenter)
            {
                var offset = GetCentredPosition();
                var rotation = Matrix4Dx4.Identity;
                Matrix4Dx4.RotationX(angle, ref rotation);
                for (var i = 0; i < _count; i++)
                {
                    _arrayOfItems[i].Position -= offset;
                    _arrayOfItems[i].Position.TransformAsPoint(in rotation);
                    _arrayOfItems[i].Position += offset;
                    _arrayOfItems[i].Normal.TransformAsVector(in rotation);
                }
            }
            else
            {
                var rotation = Matrix4Dx4.Identity;
                Matrix4Dx4.RotationX(angle, ref rotation);
                for (var i = 0; i < _count; i++)
                {
                    _arrayOfItems[i].Position.TransformAsPoint(in rotation);
                    _arrayOfItems[i].Normal.TransformAsVector(in rotation);
                }
            }
        }

        /// <summary>
        /// Врашение вершин вокруг оси Y.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <param name="isCenter">Относительно геометрического центра.</param>
        public void RotateFromY(float angle, bool isCenter)
        {
            if (isCenter)
            {
                var offset = GetCentredPosition();
                var rotation = Matrix4Dx4.Identity;
                Matrix4Dx4.RotationY(angle, ref rotation);
                for (var i = 0; i < _count; i++)
                {
                    _arrayOfItems[i].Position -= offset;
                    _arrayOfItems[i].Position.TransformAsPoint(in rotation);
                    _arrayOfItems[i].Position += offset;
                    _arrayOfItems[i].Normal.TransformAsVector(in rotation);
                }
            }
            else
            {
                var rotation = Matrix4Dx4.Identity;
                Matrix4Dx4.RotationY(angle, ref rotation);
                for (var i = 0; i < _count; i++)
                {
                    _arrayOfItems[i].Position.TransformAsPoint(in rotation);
                    _arrayOfItems[i].Normal.TransformAsVector(in rotation);
                }
            }
        }

        /// <summary>
        /// Врашение вершин вокруг оси Z.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <param name="isCenter">Относительно геометрического центра.</param>
        public void RotateFromZ(float angle, bool isCenter)
        {
            if (isCenter)
            {
                var offset = GetCentredPosition();
                var rotation = Matrix4Dx4.Identity;
                Matrix4Dx4.RotationZ(angle, ref rotation);
                for (var i = 0; i < _count; i++)
                {
                    _arrayOfItems[i].Position -= offset;
                    _arrayOfItems[i].Position.TransformAsPoint(in rotation);
                    _arrayOfItems[i].Position += offset;
                    _arrayOfItems[i].Normal.TransformAsVector(in rotation);
                }
            }
            else
            {
                var rotation = Matrix4Dx4.Identity;
                Matrix4Dx4.RotationZ(angle, ref rotation);
                for (var i = 0; i < _count; i++)
                {
                    _arrayOfItems[i].Position.TransformAsPoint(in rotation);
                    _arrayOfItems[i].Normal.TransformAsVector(in rotation);
                }
            }
        }

        /// <summary>
        /// Однородное масштабирование вершин.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(float scale)
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Position *= scale;
            }
        }

        /// <summary>
        /// Масштабирование вершины.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(Vector3Df scale)
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Position = Vector3Df.Scale(_arrayOfItems[i].Position, scale);
                _arrayOfItems[i].Normal = Vector3Df.Scale(_arrayOfItems[i].Normal, scale).Normalized;
            }
        }

        /// <summary>
        /// Обратить нормали.
        /// </summary>
        public void FlipNormals()
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Normal = -_arrayOfItems[i].Normal;
            }
        }

        /// <summary>
        /// Обратить развёртку текстурных координат по горизонтали.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public void FlipUVHorizontally(int channel = 0)
        {
            switch (channel)
            {
                case 0:
                    for (var i = 0; i < _count; i++)
                    {
                        _arrayOfItems[i].UV = new Vector2Df(1.0f - _arrayOfItems[i].UV.X, _arrayOfItems[i].UV.Y);
                    }
                    break;
                case 1:
                    for (var i = 0; i < _count; i++)
                    {
                        _arrayOfItems[i].UV2 = new Vector2Df(1.0f - _arrayOfItems[i].UV2.X, _arrayOfItems[i].UV2.Y);
                    }
                    break;
                case 2:
                    for (var i = 0; i < _count; i++)
                    {
                        _arrayOfItems[i].UV3 = new Vector2Df(1.0f - _arrayOfItems[i].UV3.X, _arrayOfItems[i].UV3.Y);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Обратить развёртку текстурных координат по вертикали.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public void FlipUVVertically(int channel = 0)
        {
            switch (channel)
            {
                case 0:
                    for (var i = 0; i < _count; i++)
                    {
                        _arrayOfItems[i].UV = new Vector2Df(_arrayOfItems[i].UV.X, 1.0f - _arrayOfItems[i].UV.Y);
                    }
                    break;
                case 1:
                    for (var i = 0; i < _count; i++)
                    {
                        _arrayOfItems[i].UV2 = new Vector2Df(_arrayOfItems[i].UV2.X, 1.0f - _arrayOfItems[i].UV2.Y);
                    }
                    break;
                case 2:
                    for (var i = 0; i < _count; i++)
                    {
                        _arrayOfItems[i].UV3 = new Vector2Df(_arrayOfItems[i].UV3.X, 1.0f - _arrayOfItems[i].UV3.Y);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Полное дублирование списка вершин.
        /// </summary>
        /// <returns>Дубликат списка вершин.</returns>
        public ListVertex3D Duplicate()
        {
            var list_vertex = new ListVertex3D();
            for (var i = 0; i < _count; i++)
            {
                list_vertex.Add(_arrayOfItems[i]);
            }
            return list_vertex;
        }

        /// <summary>
        /// Смещение вершины вдоль нормали.
        /// </summary>
        /// <param name="offset">Размер смещения.</param>
        public void OffsetFromNormal(float offset)
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Position = _arrayOfItems[i].Position + (_arrayOfItems[i].Normal * offset);
            }
        }

        /// <summary>
        /// Получение списка вершин смещенных вдоль нормали.
        /// </summary>
        /// <param name="offset">Размер смещения.</param>
        /// <returns>Список вершин смещенных вдоль нормали.</returns>
        public ListVertex3D GetListVertexOffsetFromNormal(float offset)
        {
            var list_vertex = new ListVertex3D();
            for (var i = 0; i < _count; i++)
            {
                list_vertex.Add(_arrayOfItems[i].Position + (_arrayOfItems[i].Normal * offset));
            }
            return list_vertex;
        }

        /// <summary>
        /// Получение позиции вершины с наибольшими координатами.
        /// </summary>
        /// <returns>Позиция вершины с наибольшими координатами.</returns>
        public Vector3Df GetMaximizedPosition()
        {
            float size = 20000;

            var max_x = -size;
            var max_y = -size;
            var max_z = -size;

            for (var i = 0; i < _count; i++)
            {
                if (_arrayOfItems[i].Position.X > max_x) max_x = _arrayOfItems[i].Position.X;
                if (_arrayOfItems[i].Position.Y > max_y) max_y = _arrayOfItems[i].Position.Y;
                if (_arrayOfItems[i].Position.Z > max_z) max_z = _arrayOfItems[i].Position.Z;
            }

            return new Vector3Df(max_x, max_y, max_z);
        }

        /// <summary>
        /// Получение позиции вершины с наименьшими координатами.
        /// </summary>
        /// <returns>Позиция вершины с наименьшими координатами.</returns>
        public Vector3Df GetMinimizedPosition()
        {
            float size = 20000;

            var min_x = size;
            var min_y = size;
            var min_z = size;

            for (var i = 0; i < _count; i++)
            {
                if (_arrayOfItems[i].Position.X < min_x) min_x = _arrayOfItems[i].Position.X;
                if (_arrayOfItems[i].Position.Y < min_y) min_y = _arrayOfItems[i].Position.Y;
                if (_arrayOfItems[i].Position.Z < min_z) min_z = _arrayOfItems[i].Position.Z;
            }

            return new Vector3Df(min_x, min_y, min_z);
        }

        /// <summary>
        /// Получение средней(геометрического центра) позиции вершин.
        /// </summary>
        /// <returns>Средняя позиция вершин.</returns>
        public Vector3Df GetCentredPosition()
        {
            var max = GetMaximizedPosition();
            var min = GetMinimizedPosition();

            return (max + min) / 2;
        }
        #endregion

        #region Vertex methods
        /// <summary>
        /// Добавление вершины.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        public void AddVertex(Vector3Df position)
        {
            var vertex = new Vertex3Df(position)
            {
                Index = _count
            };
            Add(vertex);
        }

        /// <summary>
        /// Добавление вершины.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="uv">Текстурные координаты вершины.</param>
        public void AddVertex(Vector3Df position, Vector2Df uv)
        {
            var vertex = new Vertex3Df(position, uv)
            {
                Index = _count
            };
            Add(vertex);
        }

        /// <summary>
        /// Добавление вершины.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="normal">Единичная нормаль вершины.</param>
        public void AddVertex(Vector3Df position, Vector3Df normal)
        {
            var vertex = new Vertex3Df(position, normal)
            {
                Index = _count
            };
            Add(vertex);
        }

        /// <summary>
        /// Добавление вершины.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="normal">Единичная нормаль вершины.</param>
        /// <param name="uv">Текстурные координаты вершины.</param>
        public void AddVertex(Vector3Df position, Vector3Df normal, Vector2Df uv)
        {
            var vertex = new Vertex3Df(position, normal, uv)
            {
                Index = _count
            };
            Add(vertex);
        }

        /// <summary>
        /// Добавление вершины.
        /// </summary>
        /// <param name="position">Позиция вершины.</param>
        /// <param name="normal">Единичная нормаль вершины.</param>
        /// <param name="uv">Текстурные координаты вершины.</param>
        /// <param name="uv2">Вторые текстурные координаты вершины.</param>
        public void AddVertex(Vector3Df position, Vector3Df normal, Vector2Df uv, Vector2Df uv2)
        {
            var vertex = new Vertex3Df(position, normal, uv, uv2)
            {
                Index = _count
            };
            Add(vertex);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Добавление вершины.
		/// </summary>
		/// <param name="position">Позиция вершины.</param>
		/// <param name="normal">Единичная нормаль вершины.</param>
		public void AddVertex(UnityEngine.Vector3 position, UnityEngine.Vector3 normal)
		{
			CVertex3Df vertex = new CVertex3Df(position, normal);
			vertex.Index = _count;
			Add(vertex);
		}

		/// <summary>
		/// Добавление вершины.
		/// </summary>
		/// <param name="position">Позиция вершины.</param>
		/// <param name="normal">Единичная нормаль вершины.</param>
		/// <param name="uv">Текстурные координаты вершины.</param>
		public void AddVertex(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Vector2 uv)
		{
			CVertex3Df vertex = new CVertex3Df(position, normal, uv);
			vertex.Index = _count;
			Add(vertex);
		}

		/// <summary>
		/// Добавление вершины.
		/// </summary>
		/// <param name="position">Позиция вершины.</param>
		/// <param name="normal">Единичная нормаль вершины.</param>
		/// <param name="uv">Текстурные координаты вершины.</param>
		/// <param name="uv2">Вторые текстурные координаты вершины.</param>
		public void AddVertex(UnityEngine.Vector3 position, UnityEngine.Vector3 normal, UnityEngine.Vector2 uv, UnityEngine.Vector2 uv2)
		{
			CVertex3Df vertex = new CVertex3Df(position, normal, uv, uv2);
			vertex.Index = _count;
			Add(vertex);
		}
#endif

        /// <summary>
        /// Добавление вершин.
        /// </summary>
        /// <param name="positions">Позиции вершины.</param>
        public void AddVertexes(IList<Vector3Df> positions)
        {
            for (var i = 0; i < positions.Count; i++)
            {
                var vertex = new Vertex3Df(positions[i])
                {
                    Index = _count
                };
                Add(vertex);
            }
        }

        /// <summary>
        /// Добавление вершин.
        /// </summary>
        /// <param name="positions">Позиции вершины.</param>
        /// <param name="uvs">Текстурные координаты вершин.</param>
        public void AddVertexes(IList<Vector3Df> positions, IList<Vector2Df> uvs)
        {
            if (positions.Count != uvs.Count)
            {
#if UNITY_2017_1_OR_NEWER
				UnityEngine.Debug.LogError(String.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#else
                XLogger.LogError(string.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#endif
            }

            for (var i = 0; i < positions.Count; i++)
            {
                var vertex = new Vertex3Df(positions[i], uvs[i])
                {
                    Index = _count
                };
                Add(vertex);
            }
        }

        /// <summary>
        /// Добавление вершин.
        /// </summary>
        /// <param name="positions">Позиции вершины.</param>
        /// <param name="normal">Единичная нормаль вершины.</param>
        /// <param name="uvs">Текстурные координаты вершин.</param>
        public void AddVertexes(IList<Vector3Df> positions, Vector3Df normal, IList<Vector2Df> uvs)
        {
            if (positions.Count != uvs.Count)
            {
#if UNITY_2017_1_OR_NEWER
				UnityEngine.Debug.LogError(String.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#else
                XLogger.LogError(string.Format("Positions: {0} != UV {1}", positions.Count, uvs.Count));
#endif
            }

            for (var i = 0; i < positions.Count; i++)
            {
                var vertex = new Vertex3Df(positions[i], normal, uvs[i])
                {
                    Index = _count
                };
                Add(vertex);
            }
        }
        #endregion
    }
    /**@}*/
}