#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine.Networking;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityLocalization
    *@{*/
    /// <summary>
    /// Базовый сервис web-переводчика
    /// </summary>
    public abstract class TranslatorBase
    {
        #region Fields
        protected string _abbrsFrom = "ru";
        protected string _abbrsTo = "en";
        protected string _originalText;
        protected string _translateText;
        protected List<string> _originalList;
        protected List<string> _replacementList;
        #endregion

        #region Properties
        /// <summary>
        /// Аббревиатура исходного языка
        /// </summary>
        public string AbbrsFrom
        {
            get { return _abbrsFrom; }
            set { _abbrsFrom = value; }
        }

        /// <summary>
        /// Аббревиатура переведенного языка
        /// </summary>
        public string AbbrsTo
        {
            get { return _abbrsTo; }
            set { _abbrsTo = value; }
        }

        /// <summary>
        /// Оригинальный текст
        /// </summary>
        public string OriginalText
        {
            get { return _originalText; }
            set { _originalText = value; }
        }

        /// <summary>
        /// Переведенный текст
        /// </summary>
        public string TranslateText
        {
            get { return _translateText; }
            set { _translateText = value; }
        }

        /// <summary>
        /// Список оригинальных строк которые на переводятся и берутся из списка для замены
        /// </summary>
        public List<string> OriginalList
        {
            get { return _originalList; }
        }

        /// <summary>
        /// Список для замены оригинальных строк
        /// </summary>
        public List<string> ReplacementList
        {
            get { return _replacementList; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public TranslatorBase()
        {
            _originalList = new List<string>();
            _replacementList = new List<string>();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Перевод текста
        /// </summary>
        public abstract void Translate();
        #endregion
    }


    /// <summary>
    /// Сервис web-переводчика Yandex.Translate
    /// </summary>
    [Obsolete("Yandex Translate API v1.5 отключён, DeserializeFromString не реализован. Требуется новый провайдер перевода")]
    public class CTranslatorYandex : TranslatorBase
    {
        #region Constants
        /// <summary>
        /// Формат запроса
        /// </summary>
        private const string FormatQuery = "https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&lang={1}-{2}&text={3}";

        /// <summary>
        /// API Key Yandex
        /// </summary>
        private const string APIKey = "test";
        #endregion

        #region Main methods
        /// <summary>
        /// Перевод текста
        /// </summary>
        public override void Translate()
        {
            // Получаем ответ от сервера
            using (UnityWebRequest result = GetResponseFromServer())
            {
                if (result != null)
                {
                    // Ждем пока ответ полностью не придёт от сервера
                    while (!result.isDone)
                    {
                    }

                    // Результат
                    _translateText = DeserializeFromString(result);
                }
            }
        }

        /// <summary>
        /// Получить ответ от сервера перевода Yandex
        /// </summary>
        /// <returns>Ответ от сервера</returns>
        protected UnityWebRequest GetResponseFromServer()
        {
            var query = string.Format(FormatQuery, APIKey, _abbrsFrom, _abbrsTo, _originalText);

            return new UnityWebRequest(query);
        }

        /// <summary>
        /// Десереализация данных ответа сервера
        /// </summary>
        /// <param name="request">Ответ от сервера</param>
        /// <returns>Текст перевода</returns>
        protected string DeserializeFromString(UnityWebRequest request)
        {
            //var result = JsonUtility.FromJson<CResponseYandex>(request.downloadHandler.text);

            //if (string.IsNullOrEmpty(request.downloadHandler.text))
            //{
            //  Debug.Log(request.error);
            //  return "";
            //}
            //else
            //{
            //  var text = result.text[0];

            //  if (text.StartsWith("\""))
            //  {
            //      return text;
            //  }

            //  return text.TrimEnd('"');
            //}

            return "";
        }
        #endregion
    }


    /// <summary>
    /// Формат ответа Yandex в формате JSON
    /// </summary>
    public class CResponseYandex
    {
        /// <summary>
        /// Код ответа
        /// </summary>
        public int code;

        /// <summary>
        /// Язык переведенного текста
        /// </summary>
        public string lang;

        /// <summary>
        /// Переведенный текст
        /// </summary>
        public string[] text;
    }

    /// <summary>
    /// Формат ответа Yandex в формате JSON
    /// </summary>
    [Obsolete("Опечатка в имени. Используйте CResponseYandex")]
    public class CResponceYandex : CResponseYandex
    {
    }


    /// <summary>
    /// Вспомогательный тип для хранения текущего перевода
    /// </summary>
    [Serializable]
    public class CTextTranslate
    {
        /// <summary>
        /// Ключ локализации
        /// </summary>
        public int IDKeyLocalize;

        /// <summary>
        /// Текст по умолчанию
        /// </summary>
        public string Default;

        /// <summary>
        /// Переведенный текст
        /// </summary>
        public string Translate;
    }

    /**@}*/
}
#endif
