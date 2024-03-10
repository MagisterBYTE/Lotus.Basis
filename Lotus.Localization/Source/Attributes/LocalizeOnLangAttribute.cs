using System;

namespace Lotus.Localization
{
    /// <summary>
    /// Атрибут для локализации на конкретный язык.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class LocalizeOnLangAttribute : Attribute
    {
        /// <summary>
        /// Название языка/культуры в формате ISO 639-1 или ISO 639-3.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Текст на соответствующем языке.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Имя целевого элемента.
        /// </summary>
        public string? TargetName { get; set; }

        public LocalizeOnLangAttribute(string lang, string text)
        {
            Lang = lang;
            Text = text;
        }

        public LocalizeOnLangAttribute(string lang, string text, string targetName)
        {
            Lang = lang;
            Text = text;
            TargetName = targetName;
        }
    }
}