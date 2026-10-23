#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityTween
    *@{*/
    /// <summary>
    /// Центральный диспетчер для хранения и управления ресурсами анимации
    /// </summary>
    [Serializable]
    public class LotusTweenDispatcher : LotusSystemSingleton<LotusTweenDispatcher>, ISerializationCallbackReceiver,
        ILotusSingleton, ILotusMessageHandler
    {
        #region Static properties
        /// <summary>
        /// Хранилище анимационных кривых
        /// </summary>
        public static LotusTweenCurveStorage CurveStorage
        {
            get { return Instance._curveStorage; }
        }

        /// <summary>
        /// Хранилище анимационных спрайтов
        /// </summary>
        public static LotusTweenSpriteStorage SpriteStorage
        {
            get { return Instance._spriteStorage; }
        }
        #endregion

        #region Fields
        // Системные параметры
        [SerializeField]
        internal bool _isMainInstance;
        [SerializeField]
        internal TSingletonDestroyMode _destroyMode;
        [SerializeField]
        internal bool _isDontDestroy;
        [SerializeField]
        internal string _groupMessage = "Tween";

        // Основные параметры
        [SerializeField]
        internal LotusTweenCurveStorage _curveStorage;
        [SerializeField]
        internal LotusTweenSpriteStorage _spriteStorage;
        #endregion

        #region ILotusSingleton properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Статус основного экземпляра
        /// </summary>
        public bool IsMainInstance
        {
            get { return _isMainInstance; }
            set { _isMainInstance = value; }
        }

        /// <summary>
        /// Статус удаления игрового объекта
        /// </summary>
        /// <remarks>
        /// При дублировании будет удалятся либо непосредственного игровой объект либо только компонент
        /// </remarks>
        public TSingletonDestroyMode DestroyMode
        {
            get { return _destroyMode; }
            set { _destroyMode = value; }
        }

        /// <summary>
        /// Не удалять объект когда загружается новая сцена
        /// </summary>
        public bool IsDontDestroy
        {
            get { return _isDontDestroy; }
            set { _isDontDestroy = value; }
        }
        #endregion

        #region ILotusMessageHandler properties
        /// <summary>
        /// Группа которой принадлежит данный обработчик событий
        /// </summary>
        public string MessageHandlerGroup
        {
            get { return _groupMessage; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Включение хранилища
        /// </summary>
        public void OnEnable()
        {
        }
        #endregion

        #region ISerializationCallbackReceiver methods
        /// <summary>
        /// Процесс перед сериализацией объекта
        /// </summary>
        public void OnBeforeSerialize()
        {
        }

        /// <summary>
        /// Процесс после сериализацией объекта
        /// </summary>
        public void OnAfterDeserialize()
        {
        }
        #endregion

        #region ILotusMessageHandler methods
        /// <summary>
        /// Основной метод для обработки сообщения
        /// </summary>
        /// <param name="args">Аргументы сообщения</param>
        /// <returns>Статус успешности обработки сообщений</returns>
        public int OnMessageHandler(CMessageArgs args)
        {
            return XMessageHandlerResultCode.NEGATIVE_RESULT;
        }
        #endregion
    }
    /**@}*/
}
#endif
