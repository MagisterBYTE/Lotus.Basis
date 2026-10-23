#if UNITY_2017_1_OR_NEWER
using System;

using Lotus.Core.Inspector;
using Lotus.Core.Serialization;

using UnityEngine;
using UnityEngine.UI;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityTween
    *@{*/
    /// <summary>
    /// Компонент для хранения и управления анимацией спрайтов для стандартного компонента изображения
    /// </summary>
    /// <remarks>
    /// Реализация компонента для хранения (используется хранилища анимационных спрайтов) и управления
    /// анимацией спрайтов для стандартного компонента изображения.
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/Tween/Animation Sprite")]
    public class LotusTweenSpriteImage : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        [LotusDisplayName(nameof(Tween))]
        [LotusSerializeMember(nameof(Tween))]
        internal CTweenSprite _tweenSprite;

        [SerializeField]
        [LotusDisplayName(nameof(IsUpdate))]
        [LotusSerializeMember(nameof(IsUpdate))]
        internal bool _isUpdate;

        [NonSerialized]
        internal Image _uiImage;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Статус обновления анимации в методе <see cref="Update"/>
        /// </summary>
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }

        //
        // ПАРАМЕТРЫ АНИМАЦИИ
        //
        /// <summary>
        /// Аниматор спрайтов
        /// </summary>
        public CTweenSprite Tween
        {
            get { return _tweenSprite; }
        }

        /// <summary>
        /// Имя группы анимационных цепочек
        /// </summary>
        public string GroupName
        {
            get { return _tweenSprite.GroupName; }
        }

        /// <summary>
        /// Индекс анимационной цепочки в группе
        /// </summary>
        public int GroupSpriteIndex
        {
            get { return _tweenSprite.GroupSpriteIndex; }
            set { _tweenSprite.GroupSpriteIndex = value; }
        }

        /// <summary>
        /// Имя анимационной цепочки из группы
        /// </summary>
        public string GroupSpriteName
        {
            get { return _tweenSprite.GroupSpriteName; }
        }

        /// <summary>
        /// Индекс анимационной цепочки из хранилища анимаций спрайтов
        /// </summary>
        public int StorageSpriteIndex
        {
            get { return _tweenSprite.StorageSpriteIndex; }
            set { _tweenSprite.StorageSpriteIndex = value; }
        }

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        public TTweenWrapMode WrapMode
        {
            get { return _tweenSprite.WrapMode; }
            set { _tweenSprite.WrapMode = value; }
        }

        /// <summary>
        /// Начальный кадр
        /// </summary>
        public int StartFrame
        {
            get { return _tweenSprite.StartFrame; }
            set { _tweenSprite.StartFrame = value; }
        }

        /// <summary>
        /// Целевое кадр
        /// </summary>
        public int TargetFrame
        {
            get { return _tweenSprite.TargetFrame; }
            set { _tweenSprite.TargetFrame = value; }
        }

        /// <summary>
        /// Время в течение которого должно измениться значение переменной от начального к конечной
        /// </summary>
        public float Duration
        {
            get { return _tweenSprite.Duration; }
            set { _tweenSprite.Duration = value; }
        }

        /// <summary>
        /// Нормализованное время прохождение анимации в пределах от 0 до 1
        /// </summary>
        public float NormalizeTime
        {
            get { return _tweenSprite.NormalizeTime; }
        }

        /// <summary>
        /// Номер текущего кадра
        /// </summary>
        public int FrameIndex
        {
            get { return _tweenSprite.FrameIndex; }
        }

        /// <summary>
        /// Текущий проигрываемый спрайт
        /// </summary>
        public Sprite FrameSprite
        {
            get
            {
                if (_tweenSprite.StorageSpriteIndex == -1)
                {
                    return null;
                }

                return LotusTweenDispatcher.SpriteStorage.GetFrameSprite(
                    _tweenSprite.GroupIndex, _tweenSprite.GroupSpriteIndex, _tweenSprite.FrameIndex);
            }
        }

        /// <summary>
        /// Статус анимации
        /// </summary>
        public bool IsPlay
        {
            get { return _tweenSprite.IsPlay; }
        }

        /// <summary>
        /// Установка/снятие паузы анимации
        /// </summary>
        public bool IsPause
        {
            get { return _tweenSprite.IsPause; }
            set { _tweenSprite.IsPause = value; }
        }

        /// <summary>
        /// Статус проигрывания анимации вперед
        /// </summary>
        public bool IsForward
        {
            get { return _tweenSprite.IsForward; }
        }

        /// <summary>
        /// Событие для нотификации о начале анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationStart
        {
            get { return _tweenSprite.OnAnimationStart; }
            set { _tweenSprite.OnAnimationStart = value; }
        }

        /// <summary>
        /// Событие для нотификации об окончании анимации. Аргумент - название анимации
        /// </summary>
        public Action<string> OnAnimationCompleted
        {
            get { return _tweenSprite.OnAnimationCompleted; }
            set { _tweenSprite.OnAnimationCompleted = value; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
        }

        /// <summary>
        /// Включение скрипта
        /// </summary>
        public void OnEnable()
        {
        }

        /// <summary>
        /// Старт скрипта
        /// </summary>
        public void Start()
        {
            _uiImage = GetComponent<Image>();
        }

        /// <summary>
        /// Обновление скрипта каждый кадр
        /// </summary>
        public void Update()
        {
            if (_isUpdate)
            {
                UpdateAnimation();
            }
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация анимационных цепочек
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Анимационная цепочка</returns>
        public CTweenSpriteData this[int index]
        {
            get { return LotusTweenDispatcher.SpriteStorage[_tweenSprite.GroupIndex, index]; }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Первичная инициализация данных и параметров компонента
        /// </summary>
        public void Init()
        {
            if (_tweenSprite == null)
            {
                _tweenSprite = new CTweenSprite();
            }
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        /// <param name="animationName">Имя проигрываемой анимации</param>
        public void StartAnimation(string animationName)
        {
            _tweenSprite.StartAnimation(animationName);
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        /// <param name="index">Индекс проигрываемой анимации</param>
        public void StartAnimation(int index)
        {
            _tweenSprite.StartAnimation(index);
        }

        /// <summary>
        /// Старт анимации
        /// </summary>
        public void StartAnimation()
        {
            _tweenSprite.StartAnimation();
        }

        /// <summary>
        /// Обновление анимации
        /// </summary>
        public void UpdateAnimation()
        {
            _tweenSprite.UpdateAnimation();
            if (_uiImage != null)
            {
                _uiImage.sprite = LotusTweenDispatcher.SpriteStorage.GetFrameSprite(
                    _tweenSprite.GroupIndex, _tweenSprite.GroupSpriteIndex, _tweenSprite.FrameIndex);
            }
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            _tweenSprite.StopAnimation();
            if (_uiImage != null)
            {
                _uiImage.sprite = LotusTweenDispatcher.SpriteStorage.GetFrameSprite(
                    _tweenSprite.GroupIndex, _tweenSprite.GroupSpriteIndex, _tweenSprite.FrameIndex);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Запуск анимации
        /// </summary>
        [ContextMenu("Play")]
        [LotusMethodCall("Play", 0, TMethodCallMode.OnlyPlay)]
        public void PlayInEditor()
        {
            StartAnimation();
        }
#endif
        #endregion
    }
    /**@}*/
}
#endif
