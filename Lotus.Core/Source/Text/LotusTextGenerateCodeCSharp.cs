namespace Lotus.Core
{
    /** \addtogroup CoreText
	*@{*/
    /// <summary>
    /// Простой генератор программного кода на языке C# на основе текстовых данных.
    /// </summary>
    public class CTextGenerateCodeCSharp : CTextGenerateCodeBase
    {
        #region Static fields
        /// <summary>
        /// Разделитель для части.
        /// </summary>
        public static readonly CTextLine DelimetrPart = new CTextLine("//", '=', 120);

        /// <summary>
        /// Разделитель для секции.
        /// </summary>
        public static readonly CTextLine DelimetrSection = new CTextLine("//", '-', 120);
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public CTextGenerateCodeCSharp(int capacity = 24)
            : base(capacity)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="str">Строка.</param>
        public CTextGenerateCodeCSharp(string str)
            : base(str)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление разделителя для части.
        /// </summary>
        public override void AddDelimetrPart()
        {
            CTextLine delimetr_part = DelimetrPart.Duplicate();
            delimetr_part.Index = _lines.Count;
            delimetr_part.Owned = this;
            delimetr_part.Indent = _currentIndent;
            _lines.Add(delimetr_part);
        }

        /// <summary>
        /// Добавление разделителя для секции.
        /// </summary>
        public override void AddDelimetrSection()
        {
            CTextLine delimetr_section = DelimetrSection.Duplicate();
            delimetr_section.Index = _lines.Count;
            delimetr_section.Owned = this;
            delimetr_section.Indent = _currentIndent;
            _lines.Add(delimetr_section);
        }
        #endregion

        #region Namespace methods
        /// <summary>
        /// Добавление используемых пространств имён.
        /// </summary>
        /// <param name="namespaces">Список пространства имён.</param>
        public override void AddNamespaceUsing(params string[] namespaces)
        {
            if (namespaces != null)
            {
                for (var i = 0; i < namespaces.Length; i++)
                {
                    Add("using " + namespaces[i] + ";");
                }
            }
        }

        /// <summary>
        /// Добавление открытие пространства имени.
        /// </summary>
        /// <param name="spaceName">Имя пространства имён.</param>
        public override void AddNamespaceOpen(string spaceName)
        {
            Add("namespace " + spaceName);
            Add("{");
        }

        /// <summary>
        /// Добавление закрытия пространства имени.
        /// </summary>
        public override void AddNamespaceClose()
        {
            Add("}");
        }
        #endregion

        #region ОПИСАНИЯ ФАЙЛА 
        #endregion

        #region File desc methods
        /// <summary>
        /// Добавление декларации публичного класса.
        /// </summary>
        /// <param name="className">Имя класса.</param>
        public override void AddClassPublic(string className)
        {
            Add("public class " + className);
            Add("{");
        }

        /// <summary>
        /// Добавление декларации статического публичного класса.
        /// </summary>
        /// <param name="className">Имя класса.</param>
        public override void AddClassStaticPublic(string className)
        {
            Add("public static class " + className);
            Add("{");
        }

        /// <summary>
        /// Добавление окончание декларации класса.
        /// </summary>
        public override void AddClassEndDeclaration()
        {
            Add("}");
        }
        #endregion

        #region Types methods
        /// <summary>
        /// Добавление декларации константного публичного поля.
        /// </summary>
        /// <param name="typeName">Имя типа.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значения поля.</param>
        public override void AddFieldConstPublic(string typeName, string fieldName, string value)
        {
            Add("public const " + typeName + " " + fieldName + " = " + value + ";");
        }

        /// <summary>
        /// Добавление декларации константного публичного поля типа String.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значения поля.</param>
        public override void AddFieldConstPublicString(string fieldName, string value)
        {
            Add("public const String " + fieldName + " = \"" + value + "\";");
        }

        /// <summary>
        /// Добавление декларации статического поля только для чтения.
        /// </summary>
        /// <param name="typeName">Имя типа.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значения поля.</param>
        public override void AddFieldStaticReadonlyPublic(string typeName, string fieldName, string value)
        {
            Add("public static readonly " + typeName + " " + fieldName + " = " + value + ";");
        }
        #endregion

        #region Methods methods 
        /// <summary>
        /// Добавление декларации конструктора.
        /// </summary>
        /// <param name="constructor">Конструктор.</param>
        /// <param name="baseBody">Базовый конструктор.</param>
        public override void AddConstructor(string constructor, string? baseBody)
        {
            Add(constructor);
            if (baseBody != null)
            {
                CurrentIndent += 2;
                Add($": base({baseBody})");
                CurrentIndent -= 2;
            }
            Add("{");
        }

        /// <summary>
        /// Добавление окончание декларации конструктора.
        /// </summary>
        public override void AddConstructorClose()
        {
            Add("}");
        }

        /// <summary>
        /// Добавление метода вместе с телом.
        /// </summary>
        /// <param name="signatureMethod">Сигнатура метода.</param>
        /// <param name="bodyMethods">Список инструкций метода.</param>
        public override void AddMethodWithBody(string signatureMethod, params string[] bodyMethods)
        {
            Add(signatureMethod);
            Add("{");
            CurrentIndent++;
            foreach (string method in bodyMethods)
            {
                Add(method);
            }
            CurrentIndent--;
            Add("}");
        }
        #endregion

        #region Comments methods
        /// <summary>
        /// Добавление комментария.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public override void AddComment(string text)
        {
            if (text.IsExists())
            {
                Add("// " + text);
            }
            else
            {
                Add("//");
            }
        }

        /// <summary>
        /// Добавление комментария для секции или раздела.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public override void AddCommentSection(string text)
        {
            Add("//");
            if (text.IsExists())
            {
                Add("// " + text);
            }
            else
            {
                Add("//");
            }
            Add("//");
        }

        /// <summary>
        /// Добавление стандартного краткого комментария XML.
        /// </summary>
        /// <param name="delimetrSectionBefore">Статус добавления разделителя секции перед комментарием.</param>
        /// <param name="text">Текст комментария.</param>
        /// <param name="delimetrSectionAfter">Статус добавления разделителя секции после комментария .</param>
        public override void AddCommentSummary(bool delimetrSectionBefore, string text, bool delimetrSectionAfter)
        {
            if (delimetrSectionBefore)
            {
                AddDelimetrSection();
            }
            Add("/// <summary>");
            Add("/// " + text);
            Add("/// </summary>");
            if (delimetrSectionAfter)
            {
                AddDelimetrSection();
            }
        }

        /// <summary>
        /// Добавление стандартного краткого комментария XML для полей и свойств.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public override void AddCommentSummaryForData(string text)
        {
            Add("/// <summary>");
            Add("/// " + text);
            Add("/// </summary>");
        }

        /// <summary>
        /// Добавление стандартного расширенного комментария XML для полей и свойств.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public override void AddCommentRemarksForData(string text)
        {
            Add("/// <remarks>");
            Add("/// " + text);
            Add("/// </remarks>");
        }
        #endregion
    }
    /**@}*/
}