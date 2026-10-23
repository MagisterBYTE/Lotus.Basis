#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections;

using UnityEngine;

namespace Lotus.Core
{
    
    /** \addtogroup CoreUnityComponent
    *@{*/
    
    /// <summary>
    /// Статический класс реализующий методы расширений функциональности компонента Transform
    /// </summary>
    
    public static class XTransformExtension
    {
        #region Main methods
        
        /// <summary>
        /// Установка позиции игрового объекта по X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="x">Позиция по X</param>
        
        public static void SetX(this Transform @this, float x)
        {
#if UNITY_EDITOR
            if (@this == null)
            {
                return;
            }
#endif
            Vector3 pos = @this.position;
            pos.x = x;
            @this.position = pos;
        }

        
        /// <summary>
        /// Установка позиции игрового объекта по Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="y">Позиция по Y</param>
        
        public static void SetY(this Transform @this, float y)
        {
#if UNITY_EDITOR
            if (@this == null)
            {
                return;
            }
#endif
            Vector3 pos = @this.position;
            pos.y = y;
            @this.position = pos;
        }

        
        /// <summary>
        /// Установка позиции игрового объекта по Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="z">Координата по Z</param>
        
        public static void SetZ(this Transform @this, float z)
        {
#if UNITY_EDITOR
            if (@this == null)
            {
                return;
            }
#endif
            Vector3 pos = @this.position;
            pos.z = z;
            @this.position = pos;
        }

        
        /// <summary>
        /// Наклон или поворот игрового объекта относительно правого вектора (по оси X)
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="x">Угол поворота в градусах</param>
        
        public static void SetPitch(this Transform @this, float x)
        {
#if UNITY_EDITOR
            if (@this == null)
            {
                return;
            }
#endif
            Vector3 pos = @this.eulerAngles;
            pos.x = x;
            @this.eulerAngles = pos;
        }

        
        /// <summary>
        /// Рыскание или поворот игрового объекта относительно верхнего вектора (по оси Y)
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="y">Угол поворота в градусах</param>
        
        public static void SetYaw(this Transform @this, float y)
        {
#if UNITY_EDITOR
            if (@this == null)
            {
                return;
            }
#endif
            Vector3 pos = @this.eulerAngles;
            pos.y = y;
            @this.eulerAngles = pos;
        }

        
        /// <summary>
        /// Вращение или поворот игрового объекта относительно вектора направления (по оси Z)
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="z">Угол поворота в градусах</param>
        
        public static void SetRoll(this Transform @this, float z)
        {
#if UNITY_EDITOR
            if (@this == null)
            {
                return;
            }
#endif
            Vector3 pos = @this.eulerAngles;
            pos.z = z;
            @this.eulerAngles = pos;
        }

        
        /// <summary>
        /// Абсолютная глубина компонента в иерархии
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <returns>Абсолютная глубина компонента в иерархии</returns>
        
        public static int AbsoluteDepth(this Transform @this)
        {
            var depth = 0;
            Transform currentTransform;
            if(@this.parent != null)
            {
                depth = 1;
                currentTransform = @this.parent;
                if(currentTransform.parent != null)
                {
                    depth = 2;
                    currentTransform = @this.parent;
                    if (currentTransform.parent != null)
                    {
                        depth = 3;
                        currentTransform = @this.parent;
                        if (currentTransform.parent != null)
                        {
                            depth = 4;
                            currentTransform = @this.parent;
                            if (currentTransform.parent != null)
                            {
                                depth = 5;
                                currentTransform = @this.parent;
                                if (currentTransform.parent != null)
                                {
                                    depth = 6;
                                    currentTransform = @this.parent;
                                    if (currentTransform.parent != null)
                                    {
                                        depth = 7;
                                        currentTransform = @this.parent;
                                        if (currentTransform.parent != null)
                                        {
                                            depth = 8;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return depth;
        }

        
        /// <summary>
        /// Вращение по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время полного поворота</param>
        
        public static void RotateX(this Transform @this, float duration)
        {
            var rotation = @this.eulerAngles.x;
            rotation += 360 * Time.unscaledDeltaTime / duration;
            @this.eulerAngles = new Vector3(rotation, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Вращение по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время полного поворота</param>
        
        public static void RotateY(this Transform @this, float duration)
        {
            var rotation = @this.eulerAngles.y;
            rotation += 360 * Time.unscaledDeltaTime / duration;
            @this.eulerAngles = new Vector3(@this.eulerAngles.x, rotation, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Вращение по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время полного поворота</param>
        
        public static void RotateZ(this Transform @this, float duration)
        {
            var rotation = @this.eulerAngles.z;
            rotation += 360 * Time.unscaledDeltaTime / duration;
            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, rotation);
        }

        
        /// <summary>
        /// Получение компонента указанного типа от непосредственного родителя
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Компонент трансформации</param>
        /// <returns>Компонент</returns>
        
        public static TComponent GetComponentFromParent<TComponent>(this Transform @this) where TComponent : Component
        {
            if (@this.parent != null)
            {
                return (@this.transform.parent.GetComponent<TComponent>());
            }

            return (null);
        }

        
        /// <summary>
        /// Получение компонента указанного типа от непосредственного родителя или его родителя
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Компонент трансформации</param>
        /// <returns>Компонент</returns>
        
        public static TComponent GetComponentFromParentOrHisParent<TComponent>(this Transform @this) where TComponent : Component
        {
            if (@this.parent != null)
            {
                TComponent сomponent = @this.parent.GetComponent<TComponent>();
                if (сomponent != null)
                {
                    return (сomponent);
                }
                else
                {
                    if (@this.parent.parent != null)
                    {
                        return (@this.parent.parent.GetComponent<TComponent>());
                    }
                }
            }

            return (null);
        }
        #endregion

        #region Move methods
        
        /// <summary>
        /// Старт перемещению компонента трансформации к указанной позиции
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void MoveTo(this MonoBehaviour @this, float duration, Vector3 targetPosition, TEasingType easingType)
        {
            MoveTo(@this, @this.transform, duration, targetPosition, easingType);
        }

        
        /// <summary>
        /// Старт перемещению компонента трансформации к указанной позиции
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        
        public static void MoveTo(this MonoBehaviour @this, float duration, Vector3 targetPosition, TEasingType easingType, Action onCompleted)
        {
            MoveTo(@this, @this.transform, duration, targetPosition, easingType, onCompleted);
        }

        
        /// <summary>
        /// Старт перемещению компонента трансформации к указанной позиции
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void MoveTo(this MonoBehaviour @this, Transform transform, float duration, Vector3 targetPosition, TEasingType easingType)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(MoveToLinearIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(MoveToQuadInIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(MoveToQuadOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(MoveToQuadInOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(MoveToCubeInIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(MoveToCubeOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(MoveToCubeInOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(MoveToBackInIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(MoveToBackOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(MoveToBackInOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(MoveToExpoInIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(MoveToExpoOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(MoveToExpoInOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(MoveToSineInIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(MoveToSineOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(MoveToSineInOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(MoveToElasticInIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(MoveToElasticOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(MoveToElasticInOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(MoveToBounceInIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(MoveToBounceOutIteration(transform, duration, targetPosition));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(MoveToBounceInOutIteration(transform, duration, targetPosition));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Старт перемещению компонента трансформации к указанной позиции
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        
        public static void MoveTo(this MonoBehaviour @this, Transform transform, float duration, Vector3 targetPosition, TEasingType easingType, Action onCompleted)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(MoveToLinearIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(MoveToQuadInIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(MoveToQuadOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(MoveToQuadInOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(MoveToCubeInIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(MoveToCubeOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(MoveToCubeInOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(MoveToBackInIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(MoveToBackOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(MoveToBackInOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(MoveToExpoInIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(MoveToExpoOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(MoveToExpoInOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(MoveToSineInIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(MoveToSineOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(MoveToSineInOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(MoveToElasticInIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(MoveToElasticOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(MoveToElasticInOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(MoveToBounceInIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(MoveToBounceOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(MoveToBounceInOutIteration(transform, duration, targetPosition, onCompleted));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToLinearIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.Linear(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToLinearIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.Linear(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToQuadInIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.QuadIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToQuadInIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.QuadIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToQuadOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.QuadOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToQuadOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.QuadOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToQuadInOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.QuadInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToQuadInOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.QuadInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToCubeInIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.CubeIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToCubeInIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.CubeIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToCubeOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.CubeOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToCubeOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.CubeOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToCubeInOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.CubeInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToCubeInOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.CubeInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBackInIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BackIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBackInIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BackIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBackOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BackOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBackOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BackOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBackInOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BackInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBackInOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BackInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToExpoInIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ExpoIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToExpoInIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ExpoIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToExpoOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ExpoOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToExpoOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ExpoOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToExpoInOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ExpoInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToExpoInOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ExpoInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToSineInIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.SineIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToSineInIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.SineIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToSineOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.SineOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToSineOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.SineOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToSineInOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.SineInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToSineInOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.SineInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToElasticInIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ElasticIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToElasticInIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ElasticIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToElasticOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ElasticOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToElasticOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ElasticOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToElasticInOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ElasticInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToElasticInOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.ElasticInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBounceInIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BounceIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBounceInIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BounceIn(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBounceOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BounceOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBounceOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BounceOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBounceInOutIteration(this Transform @this, float duration, Vector3 targetPosition)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BounceInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;
        }

        
        /// <summary>
        /// Подпрограмма перемещения компонента трансформации
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время перемещения</param>
        /// <param name="targetPosition">Целевая позиция</param>
        /// <param name="onCompleted">Обработчик события окончания перемещения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator MoveToBounceInOutIteration(this Transform @this, float duration, Vector3 targetPosition, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Vector3 startPosition = @this.position;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.position = XEasing.BounceInOut(ref startPosition, ref targetPosition, time);
                yield return null;
            }

            @this.position = targetPosition;

            onCompleted();
        }
        #endregion

        #region Rotation methods
        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationTo(this MonoBehaviour @this, float duration, Quaternion target, TEasingType easingType)
        {
            RotationTo(@this, @this.transform, duration, target, easingType);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationTo(this MonoBehaviour @this, float duration, Quaternion target, TEasingType easingType, Action onCompleted)
        {
            RotationTo(@this, @this.transform, duration, target, easingType, onCompleted);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationTo(this MonoBehaviour @this, Transform transform, float duration, Quaternion target, TEasingType easingType)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToLinearIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToQuadInIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToQuadOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToQuadInOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToCubeInIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToCubeOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToCubeInOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToBackInIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToBackOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToBackInOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToExpoInIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToExpoOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToExpoInOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToSineInIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToSineOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToSineInOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToElasticInIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToElasticOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToElasticInOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToBounceInIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToBounceOutIteration(transform, duration, target));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToBounceInOutIteration(transform, duration, target));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationTo(this MonoBehaviour @this, Transform transform, float duration, Quaternion target, TEasingType easingType, Action onCompleted)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToLinearIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToQuadInIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToQuadOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToQuadInOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToCubeInIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToCubeOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToCubeInOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToBackInIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToBackOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToBackInOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToExpoInIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToExpoOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToExpoInOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToSineInIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToSineOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToSineInOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToElasticInIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToElasticOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToElasticInOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToBounceInIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToBounceOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToBounceInOutIteration(transform, duration, target, onCompleted));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToLinearIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, time);
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToLinearIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, time);
                yield return null;
            }

            @this.rotation = target;
            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToQuadInIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, time * time);
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToQuadInIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, time * time);
                yield return null;
            }

            @this.rotation = target;
            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToQuadOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.QuadOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToQuadOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.QuadOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToQuadInOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.QuadInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToQuadInOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.QuadInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToCubeInIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.CubeIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToCubeInIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.CubeIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToCubeOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.CubeOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToCubeOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.CubeOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToCubeInOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.CubeInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToCubeInOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.CubeInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBackInIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BackIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBackInIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BackIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBackOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BackOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBackOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BackOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBackInOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BackInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBackInOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BackInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToExpoInIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ExpoIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToExpoInIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ExpoIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToExpoOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ExpoOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToExpoOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ExpoOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToExpoInOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ExpoInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Кватернион вращения</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToExpoInOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ExpoInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToSineInIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.SineIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToSineInIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.SineIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToSineOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.SineOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToSineOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.SineOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToSineInOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.SineInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToSineInOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.SineInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToElasticInIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ElasticIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToElasticInIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ElasticIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToElasticOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ElasticOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToElasticOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ElasticOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToElasticInOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ElasticInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToElasticInOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.ElasticInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBounceInIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BounceIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBounceInIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BounceIn(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBounceOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BounceOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBounceOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BounceOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBounceInOutIteration(this Transform @this, float duration, Quaternion target)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BounceInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации к указанному кватерниону
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="target">Целевой кватернион</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToBounceInOutIteration(this Transform @this, float duration, Quaternion target, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            Quaternion startRotation = @this.rotation;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.rotation = Quaternion.Lerp(startRotation, target, XEasing.BounceInOut(0, 1, time));
                yield return null;
            }

            @this.rotation = target;

            onCompleted();
        }
        #endregion

        #region RotationX methods
        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси X
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationToX(this MonoBehaviour @this, float duration, float targetAngle, TEasingType easingType)
        {
            RotationToX(@this, @this.transform, duration, targetAngle, easingType);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси X
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationToX(this MonoBehaviour @this, float duration, float targetAngle, TEasingType easingType, Action onCompleted)
        {
            RotationToX(@this, @this.transform, duration, targetAngle, easingType, onCompleted);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси X
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationToX(this MonoBehaviour @this, Transform transform, float duration, float targetAngle, TEasingType easingType)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToXLinearIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToXQuadInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToXQuadOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToXQuadInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToXCubeInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToXCubeOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToXCubeInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToXBackInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToXBackOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToXBackInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToXExpoInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToXExpoOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToXExpoInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToXSineInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToXSineOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToXSineInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToXElasticInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToXElasticOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToXElasticInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToXBounceInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToXBounceOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToXBounceInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси X
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationToX(this MonoBehaviour @this, Transform transform, float duration, float targetAngle, TEasingType easingType, Action onCompleted)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToXLinearIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToXQuadInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToXQuadOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToXQuadInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToXCubeInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToXCubeOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToXCubeInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToXBackInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToXBackOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToXBackInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToXExpoInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToXExpoOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToXExpoInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToXSineInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToXSineOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToXSineInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToXElasticInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToXElasticOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToXElasticInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToXBounceInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToXBounceOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToXBounceInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXLinearIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.Linear(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXLinearIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.Linear(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXQuadInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.QuadIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXQuadInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.QuadIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXQuadOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.QuadOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXQuadOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.QuadOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXQuadInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.QuadInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXQuadInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.QuadInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXCubeInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.CubeIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXCubeInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.CubeIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXCubeOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.CubeOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXCubeOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.CubeOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXCubeInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.CubeInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXCubeInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.CubeInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBackInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BackIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBackInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BackIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBackOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BackOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBackOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BackOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBackInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BackInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBackInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BackInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXExpoInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ExpoIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXExpoInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ExpoIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXExpoOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ExpoOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXExpoOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ExpoOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXExpoInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ExpoInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXExpoInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ExpoInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXSineInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.SineIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXSineInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.SineIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXSineOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.SineOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXSineOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.SineOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXSineInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.SineInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXSineInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.SineInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXElasticInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ElasticIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXElasticInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ElasticIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXElasticOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ElasticOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXElasticOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ElasticOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXElasticInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ElasticInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXElasticInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.ElasticInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBounceInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BounceIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBounceInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BounceIn(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBounceOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BounceOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBounceOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BounceOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBounceInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BounceInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси X
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToXBounceInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.x;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(XEasing.BounceInOut(startAngle, targetAngle, time), @this.eulerAngles.y, @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(targetAngle, @this.eulerAngles.y, @this.eulerAngles.z);

            onCompleted();
        }
        #endregion

        #region RotationY methods
        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Y
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationToY(this MonoBehaviour @this, float duration, float targetAngle, TEasingType easingType)
        {
            RotationToY(@this, @this.transform, duration, targetAngle, easingType);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Y
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationToY(this MonoBehaviour @this, float duration, float targetAngle, TEasingType easingType, Action onCompleted)
        {
            RotationToY(@this, @this.transform, duration, targetAngle, easingType, onCompleted);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Y
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationToY(this MonoBehaviour @this, Transform transform, float duration, float targetAngle, TEasingType easingType)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToYLinearIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToYQuadInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToYQuadOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToYQuadInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToYCubeInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToYCubeOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToYCubeInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToYBackInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToYBackOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToYBackInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToYExpoInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToYExpoOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToYExpoInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToYSineInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToYSineOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToYSineInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToYElasticInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToYElasticOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToYElasticInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToYBounceInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToYBounceOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToYBounceInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Y
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationToY(this MonoBehaviour @this, Transform transform, float duration, float targetAngle, TEasingType easingType, Action onCompleted)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToYLinearIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToYQuadInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToYQuadOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToYQuadInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToYCubeInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToYCubeOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToYCubeInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToYBackInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToYBackOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToYBackInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToYExpoInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToYExpoOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToYExpoInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToYSineInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToYSineOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToYSineInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToYElasticInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToYElasticOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToYElasticInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToYBounceInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToYBounceOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToYBounceInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYLinearIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.Linear(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYLinearIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.Linear(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYQuadInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYQuadInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYQuadOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYQuadOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYQuadInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYQuadInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYCubeInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYCubeInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYCubeOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYCubeOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYCubeInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYCubeInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBackInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBackInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBackOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBackOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBackInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBackInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYExpoInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYExpoInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYExpoOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYExpoOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYExpoInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYExpoInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYSineInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYSineInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYSineOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYSineOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYSineInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYSineInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYElasticInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYElasticInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYElasticOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYElasticOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYElasticInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYElasticInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBounceInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBounceInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceIn(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBounceOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBounceOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBounceInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Y
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToYBounceInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.y;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceInOut(startAngle, targetAngle, time), @this.eulerAngles.z);
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, targetAngle, @this.eulerAngles.z);

            onCompleted();
        }
        #endregion

        #region RotationZ methods
        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Z
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationToZ(this MonoBehaviour @this, float duration, float targetAngle, TEasingType easingType)
        {
            RotationToZ(@this, @this.transform, duration, targetAngle, easingType);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Z
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationToZ(this MonoBehaviour @this, float duration, float targetAngle, TEasingType easingType, Action onCompleted)
        {
            RotationToZ(@this, @this.transform, duration, targetAngle, easingType, onCompleted);
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Z
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        
        public static void RotationToZ(this MonoBehaviour @this, Transform transform, float duration, float targetAngle, TEasingType easingType)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToZLinearIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToZQuadInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToZQuadOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToZQuadInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToZCubeInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToZCubeOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToZCubeInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToZBackInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToZBackOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToZBackInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToZExpoInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToZExpoOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToZExpoInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToZSineInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToZSineOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToZSineInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToZElasticInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToZElasticOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToZElasticInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToZBounceInIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToZBounceOutIteration(transform, duration, targetAngle));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToZBounceInOutIteration(transform, duration, targetAngle));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Старт вращению компонента трансформации к указанному углу по оси Z
        /// </summary>
        /// <param name="this">Пользовательский компонент</param>
        /// <param name="transform">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="easingType">Тип функции скорости</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        
        public static void RotationToZ(this MonoBehaviour @this, Transform transform, float duration, float targetAngle, TEasingType easingType, Action onCompleted)
        {
            switch (easingType)
            {
                case TEasingType.Linear:
                    {
                        @this.StartCoroutine(RotationToZLinearIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadIn:
                    {
                        @this.StartCoroutine(RotationToZQuadInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadOut:
                    {
                        @this.StartCoroutine(RotationToZQuadOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.QuadInOut:
                    {
                        @this.StartCoroutine(RotationToZQuadInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeIn:
                    {
                        @this.StartCoroutine(RotationToZCubeInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeOut:
                    {
                        @this.StartCoroutine(RotationToZCubeOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.CubeInOut:
                    {
                        @this.StartCoroutine(RotationToZCubeInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackIn:
                    {
                        @this.StartCoroutine(RotationToZBackInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackOut:
                    {
                        @this.StartCoroutine(RotationToZBackOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BackInOut:
                    {
                        @this.StartCoroutine(RotationToZBackInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoIn:
                    {
                        @this.StartCoroutine(RotationToZExpoInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoOut:
                    {
                        @this.StartCoroutine(RotationToZExpoOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ExpoInOut:
                    {
                        @this.StartCoroutine(RotationToZExpoInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineIn:
                    {
                        @this.StartCoroutine(RotationToZSineInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineOut:
                    {
                        @this.StartCoroutine(RotationToZSineOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.SineInOut:
                    {
                        @this.StartCoroutine(RotationToZSineInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticIn:
                    {
                        @this.StartCoroutine(RotationToZElasticInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticOut:
                    {
                        @this.StartCoroutine(RotationToZElasticOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.ElasticInOut:
                    {
                        @this.StartCoroutine(RotationToZElasticInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceIn:
                    {
                        @this.StartCoroutine(RotationToZBounceInIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceOut:
                    {
                        @this.StartCoroutine(RotationToZBounceOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                case TEasingType.BounceInOut:
                    {
                        @this.StartCoroutine(RotationToZBounceInOutIteration(transform, duration, targetAngle, onCompleted));
                    }
                    break;
                default:
                    break;
            }
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZLinearIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.Linear(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZLinearIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.Linear(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZQuadInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZQuadInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZQuadOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZQuadOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZQuadInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZQuadInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZCubeInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZCubeInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZCubeOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZCubeOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZCubeInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZCubeInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBackInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBackInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBackOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBackOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBackInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBackInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZExpoInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZExpoInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZExpoOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZExpoOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZExpoInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZExpoInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZSineInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZSineInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZSineOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZSineOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZSineInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZSineInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZElasticInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZElasticInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZElasticOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZElasticOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZElasticInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZElasticInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBounceInIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBounceInIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceIn(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBounceOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBounceOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBounceInOutIteration(this Transform @this, float duration, float targetAngle)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);
        }

        
        /// <summary>
        /// Подпрограмма вращения компонента трансформации по оси Z
        /// </summary>
        /// <param name="this">Компонент трансформации</param>
        /// <param name="duration">Время вращения</param>
        /// <param name="targetAngle">Целевой угол</param>
        /// <param name="onCompleted">Обработчик события окончания вращения</param>
        /// <returns>Перечислитель</returns>
        
        public static IEnumerator RotationToZBounceInOutIteration(this Transform @this, float duration, float targetAngle, Action onCompleted)
        {
            float time = 0;
            float startTime = 0;
            var startAngle = @this.eulerAngles.z;
            while (time < 1)
            {
                startTime += Time.unscaledDeltaTime;
                time = startTime / duration;
                @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceInOut(startAngle, targetAngle, time));
                yield return null;
            }

            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, targetAngle);

            onCompleted();
        }
        #endregion
    }
    
    /**@}*/
}
#endif
