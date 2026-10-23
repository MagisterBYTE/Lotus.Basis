#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityTween
    *@{*/
    /// <summary>
    /// Шаблонный список однотипных аниматоров
    /// </summary>
    /// <typeparam name="TTweenObject">Класс для хранения и управления анимации обобщенным типом</typeparam>
    /// <typeparam name="TAnimatableType">Тип анимационного значения</typeparam>
    public class ListTweenGenericObject<TTweenObject, TAnimatableType> where TTweenObject : TweenGenericObject<TAnimatableType>
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        internal List<TTweenObject> _tweens;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Название анимации
        /// </summary>
        public string Name
        {
            get { return _tweens[0]._name; }
            set
            {
                for (var i = 0; i < _tweens.Count; i++)
                {
                    if (_tweens[i] != null)
                    {
                        _tweens[i]._name = value;
                    }
                }
            }
        }

        /// <summary>
        /// Текущее значение переменной
        /// </summary>
        public TAnimatableType Value
        {
            get { return _tweens[0]._value; }
            set
            {
                for (var i = 0; i < _tweens.Count; i++)
                {
                    if (_tweens[i] != null)
                    {
                        _tweens[i]._value = value;
                    }
                }
            }
        }

        /// <summary>
        /// Начальное значение переменной
        /// </summary>
        public TAnimatableType StartValue
        {
            get { return _tweens[0]._startValue; }
            set
            {
                for (var i = 0; i < _tweens.Count; i++)
                {
                    if (_tweens[i] != null)
                    {
                        _tweens[i]._startValue = value;
                    }
                }
            }
        }

        /// <summary>
        /// Целевое значение переменной
        /// </summary>
        public TAnimatableType TargetValue
        {
            get { return _tweens[0]._targetValue; }
            set
            {
                for (var i = 0; i < _tweens.Count; i++)
                {
                    if (_tweens[i] != null)
                    {
                        _tweens[i]._targetValue = value;
                    }
                }
            }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _tweens[0]._wrapMode; }
            set
            {
                for (var i = 0; i < _tweens.Count; i++)
                {
                    if (_tweens[i] != null)
                    {
                        _tweens[i]._wrapMode = value;
                    }
                }
            }
        }

        /// <summary>
        /// Время(по умолчанию) в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _tweens[0]._duration; }
            set
            {
                for (var i = 0; i < _tweens.Count; i++)
                {
                    if (_tweens[i] != null)
                    {
                        _tweens[i]._duration = value;
                    }
                }
            }
        }

        /// <summary>
        /// Статус игнорирования масштабирования времени
        /// </summary>
        public bool IgnoreTimeScale
        {
            get { return _tweens[0]._ignoreTimeScale; }
            set
            {
                for (var i = 0; i < _tweens.Count; i++)
                {
                    if (_tweens[i] != null)
                    {
                        _tweens[i]._ignoreTimeScale = value;
                    }
                }
            }
        }

        /// <summary>
        /// Список аниматоров
        /// </summary>
        public List<TTweenObject> Tweens
        {
            get { return _tweens; }
        }
        #endregion
    }

    /// <summary>
    /// Класс для хранения и управления списком аниматоров целых значений
    /// </summary>
    [Serializable]
    public class CListTweenInteger : ListTweenGenericObject<CTweenInteger, int>
    {
    }

    /**@}*/
}
#endif
