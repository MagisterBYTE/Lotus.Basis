#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityConstants
    *@{*/
    /// <summary>
    /// Статический класс содержащий константы используемых тэгов объектов Unity
    /// </summary>
    public static class XTag
    {
        /// <summary>
        /// Тэг по умолчанию
        /// </summary>
        public const string UNTAGGED = "Untagged";

        /// <summary>
        /// Тэг - Respawn
        /// </summary>
        public const string RESPAWN = "Respawn";

        /// <summary>
        /// Тэг - Finish
        /// </summary>
        public const string FINISH = "Finish";

        /// <summary>
        /// Тэг только для режима редактора
        /// </summary>
        public const string EDITOR_ONLY = "EditorOnly";

        /// <summary>
        /// Тэг - MainCamera
        /// </summary>
        public const string MAIN_CAMERA = "MainCamera";

        /// <summary>
        /// Тэг - Player
        /// </summary>
        public const string PLAYER = "Player";

        /// <summary>
        /// Тэг - GameController
        /// </summary>
        public const string GAME_CONTROLLER = "GameController";

        /// <summary>
        /// Тэг - Person
        /// </summary>
        public const string PERSON = "Person";

        /// <summary>
        /// Тэг - Space
        /// </summary>
        public const string SPACE = "Space";
    }
    /**@}*/
}
#endif
