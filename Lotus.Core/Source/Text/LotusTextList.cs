using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lotus.Core
{
    /** \addtogroup CoreText
    *@{*/
    /// <summary>
    /// Список строк текстовых данных.
    /// </summary>
    public class CTextList
    {
        #region Static fields
        #endregion

        #region Fields
        protected internal ListArray<CTextLine> _lines;
        protected internal int _currentIndent;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Уровень вложенности строки при добавлении новых строк.
        /// </summary>
        public int CurrentIndent
        {
            get { return _currentIndent; }
            set
            {
                _currentIndent = value;
            }
        }

        /// <summary>
        /// Список строк.
        /// </summary>
        public ListArray<CTextLine> Lines
        {
            get { return _lines; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public CTextList(int capacity = 24)
        {
            _lines = new ListArray<CTextLine>(capacity);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="str">Строка.</param>
        public CTextList(string str)
        {
            _lines = new ListArray<CTextLine>();
            _lines.Add(str);
            _lines[0].Index = 0;
            _lines[0].Owned = this;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="strings">Строка.</param>
        public CTextList(IEnumerable<string> strings)
        {
            _lines = [.. strings];
            _lines[0].Index = 0;
            _lines[0].Owned = this;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление строки текстовых данных.
        /// </summary>
        /// <param name="line">Строка текстовых данных.</param>
        public void Add(CTextLine line)
        {
            line.Index = _lines.Count;
            line.Owned = this;
            line.Indent = _currentIndent;
            _lines.Add(line);
        }

        /// <summary>
        /// Добавление пустой новой строки.
        /// </summary>
        public void AddNewLine()
        {
            var line = new CTextLine(XString.NewLine);
            line.Index = _lines.Count;
            line.Owned = this;
            line.Indent = _currentIndent;
            _lines.Add(line);
        }

        /// <summary>
        /// Добавление пустой строки.
        /// </summary>
        public void AddEmptyLine()
        {
            var line = new CTextLine(string.Empty);
            line.Index = _lines.Count;
            line.Owned = this;
            line.Indent = _currentIndent;
            _lines.Add(line);
        }

        /// <summary>
        /// Удаление строк с начала до первого совпадения.
        /// </summary>
        /// <param name="lineTo">Строка сопадения.</param>
        public void RemoveToLine(string lineTo)
        {
            var index = 0;
            bool isFind = false;
            foreach (var item in _lines)
            {
                if (item.RawString.Contains(lineTo))
                {
                    isFind = true;
                    break;
                }
                index++;
            }

            if (isFind && index != 0)
            {
                _lines.RemoveRange(0, index);
            }
        }

        /// <summary>
        /// Замена первого вхождения.
        /// </summary>
        /// <param name="oldLine">Искомая строка.</param>
        /// <param name="newLine">Новая строка.</param>
        public void ReplaceFirst(string oldLine, string newLine)
        {
            foreach (var item in _lines)
            {
                if (item.RawString.Trim() == oldLine)
                {
                    item.RawString = newLine;
                    break;
                }
            }
        }

        /// <summary>
        /// Замена всех вхождений.
        /// </summary>
        /// <param name="oldLine">Искомая строка.</param>
        /// <param name="newLine">Новая строка.</param>
        public void ReplaceAll(string oldLine, string newLine)
        {
            foreach (var item in _lines)
            {
                if (item.RawString.Trim() == oldLine)
                {
                    item.RawString = newLine;
                }
            }
        }

        /// <summary>
        /// Добавить точку в конце комментария.
        /// </summary>
        public void AddDotToComment()
        {
            const string summaryBegin = "<summary>";
            const string summaryEnd = "</summary>";
            const string remarksBegin = "<remarks>";
            const string remarksEnd = "</remarks>";
            const string param = "/// <param";
            const string paramEnd = "</param>";
            const string returnsBegin = "<returns>";
            const string returnsEnd = "</returns>";
            const string typeparamBegin = "/// <typeparam";
            const string typeparamEnd = "</typeparam>";

            for (var i = 0; i < _lines.Count; i++)
            {
                var prev = i > 0 ? _lines[i - 1].RawString.TrimEnd() : string.Empty;
                var current = _lines[i].RawString.TrimEnd();
                var next = i < _lines.Count - 1 ? _lines[i + 1].RawString.TrimEnd() : string.Empty;

                if (current.Contains("///") && (prev.Contains(summaryBegin) || next.Contains(summaryEnd)))
                {
                    if (string.IsNullOrEmpty(current) == false && current.Last() != '.')
                    {

                        _lines[i].RawString = (current) += ".";
                        continue;
                    }
                }

                if (current.Contains("///") && (prev.Contains(remarksBegin) && next.Contains(remarksEnd)))
                {
                    if (string.IsNullOrEmpty(current) == false && current.Last() != '.')
                    {
                        _lines[i].RawString = current += ".";
                        continue;
                    }
                }

                if (current.Contains(param))
                {
                    var comment = current.ExtractString(">", paramEnd);
                    if (string.IsNullOrEmpty(comment) == false && comment.Last() != '.')
                    {
                        _lines[i].RawString = current.Replace(comment, comment + ".");
                        continue;
                    }
                }

                if (current.Contains(returnsBegin))
                {
                    var comment = current.ExtractString(">", returnsEnd);
                    if (string.IsNullOrEmpty(comment) == false && comment.Last() != '.')
                    {
                        _lines[i].RawString = current.Replace(comment, comment + ".");
                        continue;
                    }
                }

                if (current.Contains(typeparamBegin))
                {
                    var comment = current.ExtractString(">", typeparamEnd);
                    if (string.IsNullOrEmpty(comment) == false && comment.Last() != '.')
                    {
                        _lines[i].RawString = current.Replace(comment, comment + ".");
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Удалить пустые скобки.
        /// </summary>
        public void RemoveEmptyBraces()
        {
            var findOpen = -1;
            for (var i = 0; i < _lines.Count; i++)
            {
                var prev2 = i > 1 ? _lines[i - 2].RawString.Trim() : string.Empty;
                var prev = i > 0 ? _lines[i - 1].RawString.Trim() : string.Empty;
                var current = _lines[i].RawString.Trim();

                if (current.Contains("{") && prev.Contains("{") && prev2.Contains("namespace Lotus.Core"))
                {
                    findOpen = i;
                    break;
                }
            }

            if (findOpen > 0)
            {
                Lines.RemoveAt(findOpen);
            }

            var findClose = -1;
            for (var i = 0; i < _lines.Count; i++)
            {
                var prev = i > 0 ? _lines[i - 1].RawString.Trim() : string.Empty;
                var current = _lines[i].RawString.Trim();
                var next = i < _lines.Count - 1 ? _lines[i + 1].RawString.Trim() : string.Empty;

                if (current.Contains("}") && prev.Contains("/**@}*/") && next.Contains("}"))
                {
                    findClose = i;
                    break;
                }
            }

            if (findClose > 0)
            {
                Lines.RemoveAt(findClose);
            }
        }

        /// <summary>
        /// Удалить пустые регионы
        /// </summary>
        public void RemoveRegions()
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                var current = _lines[i].RawString.Trim();

                if (current.Contains("#region "))
                {
                    _lines[i].RawString = current.Replace("=", "").Replace("  ", " ");
                    if (_lines[i].RawString.Contains("МЕТОДЫ"))
                    {
                        var text = _lines[i].RawString.RemoveFirstOccurrence("#region");
                        var texts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (texts.Length > 1)
                        {
                            var tabs = _lines[i].GetTabsStart();
                            _lines[i].RawString = XString.Depths[tabs] + $"#region {texts[1]} methods";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Удалить табы
        /// </summary>
        public void RemoveTabs()
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                var current = _lines[i].RawString;
                if (string.IsNullOrEmpty(current) == false && current.Length > 2 && current[0] == '\t')
                {
                    _lines[i].RawString = current.Substring(1);
                }
            }
        }

        /// <summary>
        /// Вставить пустую строку перед указанным пространством имени.
        /// </summary>
        /// <param name="nameSpace"></param>
        public void InsertEmptyLineBeforeNamespace(string nameSpace)
        {
            var indexPaste = -1;
            for (var i = 0; i < _lines.Count; i++)
            {
                var prev = i > 0 ? _lines[i - 1].RawString.Trim() : string.Empty;
                var current = _lines[i].RawString.Trim();

                if (current.Contains(nameSpace) && string.IsNullOrEmpty(prev) == false)
                {
                    indexPaste = i;
                    break;
                }
            }

            if (indexPaste > 0)
            {
                Lines.Insert(indexPaste, string.Empty);
            }
        }
        #endregion

        #region Limited methods
        /// <summary>
        /// Установить длины строк.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется последним символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        public void SetLength(int length)
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                _lines[i].SetLength(length);
            }
        }

        /// <summary>
        /// Установить длины строк.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется указанным символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="symbol">Символ.</param>
        public void SetLength(int length, char symbol)
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                _lines[i].SetLength(length, symbol);
            }
        }

        /// <summary>
        /// Установить длины строк с указанным последним символом.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется последним символом, но последний 
        /// символ всегда указанный
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="symbol">Символ.</param>
        public void SetLengthAndLastChar(int length, char symbol)
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                _lines[i].SetLengthAndLastChar(length, symbol);
            }
        }

        /// <summary>
        /// Установить длины строк с учетом начальных символов табуляции.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется последним символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="tabsEquiv">Размер одного символа табуляции.</param>
        public void SetLengthWithTabs(int length, int tabsEquiv = 4)
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                _lines[i].SetLengthWithTabs(length, tabsEquiv);
            }
        }

        /// <summary>
        /// Установить длины строк с учетом начальных символов табуляции.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется указанным символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="symbol">Символ.</param>
        /// <param name="tabsEquiv">Размер одного символа табуляции.</param>
        public void SetLengthWithTabs(int length, char symbol, int tabsEquiv = 4)
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                _lines[i].SetLengthWithTabs(length, symbol, tabsEquiv);
            }
        }

        /// <summary>
        /// Установить длины строк с учетом начальных символов табуляции только для разделителей.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется последним символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="tabsEquiv">Размер одного символа табуляции.</param>
        public void SetLengthWithTabsOnlyDelimetrs(int length, int tabsEquiv = 4)
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].RawString.Contains("//---------"))
                {
                    _lines[i].SetLengthWithTabs(length, tabsEquiv);
                }
                if (_lines[i].RawString.Contains("//========"))
                {
                    _lines[i].SetLengthWithTabs(length, tabsEquiv);
                }

            }
        }
        #endregion

        #region Load/save methods
        /// <summary>
        /// Сохранения списка строк текстовых данных в файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public virtual void Save(string fileName)
        {
            // Формируем правильный путь
#if UNITY_2017_1_OR_NEWER
			var path = XFilePath.GetFileName(XCoreSettings.ASSETS_PATH, fileName, ".cs");
#else
            var path = XFilePath.GetFileName(Environment.CurrentDirectory, fileName, ".cs");
#endif
            // Создаем поток для записи
            var stream_writer = new StreamWriter(path);

            // Записываем данные
            for (var i = 0; i < _lines.Count - 1; i++)
            {
                stream_writer.WriteLine(_lines[i].RawString);
            }

            stream_writer.Write(_lines.ItemLast!.RawString);

            stream_writer.Close();

#if UNITY_EDITOR
			// Обновляем в редакторе
			UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
#endif
        }
        #endregion
    }
    /**@}*/
}