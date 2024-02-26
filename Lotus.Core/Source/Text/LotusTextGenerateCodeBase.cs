namespace Lotus.Core
{
    /** \addtogroup CoreText
	*@{*/
    /// <summary>
    /// Базовый класс генератора для генерации/редактирования программного кода.
    /// </summary>
    public abstract class CTextGenerateCodeBase : CTextList
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        protected CTextGenerateCodeBase(int capacity = 24)
            : base(capacity)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="str">Строка.</param>
        protected CTextGenerateCodeBase(string str)
            : base(str)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление разделителя для части.
        /// </summary>
        public virtual void AddDelimetrPart()
        {
        }

        /// <summary>
        /// Добавление разделителя для секции.
        /// </summary>
        public virtual void AddDelimetrSection()
        {
        }

        /// <summary>
        /// Добавление открытия блока.
        /// </summary>
        public virtual void AddBlockOpen()
        {
            Add("{");
        }

        /// <summary>
        /// Добавление закрытия блока.
        /// </summary>
        /// <param name="addingText">Добавляемый текст.</param>
        public virtual void AddBlockClose(string? addingText = null)
        {
            if (addingText != null)
            {
                Add("}" + addingText);
            }
            else
            {
                Add("}");
            }
        }
        #endregion

        #region Namespace methods
        /// <summary>
        /// Добавление используемых пространств имён.
        /// </summary>
        /// <param name="namespaces">Список пространства имён.</param>
        public virtual void AddNamespaceUsing(params string[] namespaces)
        {

        }

        /// <summary>
        /// Добавление открытие пространства имени.
        /// </summary>
        /// <param name="spaceName">Имя пространства имён.</param>
        public virtual void AddNamespaceOpen(string spaceName)
        {

        }

        /// <summary>
        /// Добавление закрытия пространства имени.
        /// </summary>
        public virtual void AddNamespaceClose()
        {

        }
        #endregion

        #region File desc methods 
        /// <summary>
        /// Добавление стандартного заголовка для файлов кода проекта Lotus.
        /// </summary>
        /// <param name="moduleName">Имя модуля.</param>
        /// <param name="subsystemName">Имя подсистемы.</param>
        public virtual void AddFileHeader(string moduleName, string subsystemName)
        {
            AddDelimetrPart();
            Add("// Проект: LotusPlatform");
            Add("// Раздел: " + moduleName);
            Add("// Подраздел: " + subsystemName);
            Add("// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>");
            AddDelimetrSection();
        }

        /// <summary>
        /// Добавление имени файла и краткое описание.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="briefDesc">Краткое описание.</param>
        public virtual void AddFileBriefDesc(string fileName, string briefDesc)
        {
            Add("/** \\file " + fileName);
            Add("*\t\t" + briefDesc);
            Add("*/");
            AddDelimetrSection();
        }

        /// <summary>
        /// Добавление версии и даты изменения файла.
        /// </summary>
        /// <param name="version">Версия файла.</param>
        /// <param name="date">Дата файла.</param>
        public void AddFileVersion(string version = "1.0.0.0", string date = "04.04.2020")
        {
            Add("// Версия: " + version);
            Add("// Последнее изменение от " + date);
            AddDelimetrPart();
        }
        #endregion

        #region Types methods 
        /// <summary>
        /// Добавление декларации публичного класса.
        /// </summary>
        /// <param name="className">Имя класса.</param>
        public virtual void AddClassPublic(string className)
        {
        }

        /// <summary>
        /// Добавление декларации статического публичного класса.
        /// </summary>
        /// <param name="className">Имя класса.</param>
        public virtual void AddClassStaticPublic(string className)
        {

        }

        /// <summary>
        /// Добавление окончание декларации класса.
        /// </summary>
        public virtual void AddClassEndDeclaration()
        {

        }
        #endregion

        #region Fields methods 
        /// <summary>
        /// Добавление декларации константного публичного поля.
        /// </summary>
        /// <param name="typeName">Имя типа.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значения поля.</param>
        public virtual void AddFieldConstPublic(string typeName, string fieldName, string value)
        {
        }

        /// <summary>
        /// Добавление декларации константного публичного поля типа String.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значения поля.</param>
        public virtual void AddFieldConstPublicString(string fieldName, string value)
        {
        }

        /// <summary>
        /// Добавление декларации статического поля только для чтения.
        /// </summary>
        /// <param name="typeName">Имя типа.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значения поля.</param>
        public virtual void AddFieldStaticReadonlyPublic(string typeName, string fieldName, string value)
        {

        }
        #endregion

        #region Methods methods
        /// <summary>
        /// Добавление декларации конструктора.
        /// </summary>
        /// <param name="constructor">Конструктор.</param>
        /// <param name="baseBody">Базовый конструктор.</param>
        public virtual void AddConstructor(string constructor, string? baseBody)
        {

        }

        /// <summary>
        /// Добавление окончание декларации конструктора.
        /// </summary>
        public virtual void AddConstructorClose()
        {

        }

        /// <summary>
        /// Добавление метода вместе с телом.
        /// </summary>
        /// <param name="signatureMethod">Сигнатура метода.</param>
        /// <param name="bodyMethods">Список инструкций метода.</param>
        public virtual void AddMethodWithBody(string signatureMethod, params string[] bodyMethods)
        {

        }
        #endregion

        #region Comments methods 
        /// <summary>
        /// Добавление комментария.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public virtual void AddComment(string text)
        {

        }

        /// <summary>
        /// Добавление комментария для секции или раздела.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public virtual void AddCommentSection(string text)
        {

        }

        /// <summary>
        /// Добавление стандартного краткого комментария.
        /// </summary>
        /// <param name="delimetrSectionBefore">Статус добавления разделителя секции перед комментарием.</param>
        /// <param name="text">Текст комментария.</param>
        /// <param name="delimetrSectionAfter">Статус добавления разделителя секции после комментария .</param>
        public virtual void AddCommentSummary(bool delimetrSectionBefore, string text, bool delimetrSectionAfter)
        {

        }

        /// <summary>
        /// Добавление стандартного краткого комментария для полей и свойств.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public virtual void AddCommentSummaryForData(string text)
        {

        }

        /// <summary>
        /// Добавление стандартного расширенного комментария для полей и свойств.
        /// </summary>
        /// <param name="text">Текст комментария.</param>
        public virtual void AddCommentRemarksForData(string text)
        {

        }

        /// <summary>
        /// Добавление команды Doxygen - добавить в группу.
        /// </summary>
        /// <param name="groupName">Имя группы.</param>
        public virtual void AddDoxygenAddToGroup(string groupName)
        {
            AddDelimetrSection();
            Add("//! \\addtogroup " + groupName);
            Add("*@{*/");
        }

        /// <summary>
        /// Добавление команды Doxygen - окончание группы.
        /// </summary>
        public virtual void AddDoxygenEndGroup()
        {
            AddDelimetrSection();
            Add("/**@}*/");
            AddDelimetrSection();
        }
        #endregion
    }
    /**@}*/
}