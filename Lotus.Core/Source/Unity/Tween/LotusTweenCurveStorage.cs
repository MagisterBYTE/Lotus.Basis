#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Xml;

using Lotus.Core.Inspector;
using Lotus.Core.Serialization;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityTween
    *@{*/
    /// <summary>
    /// Класс для хранения анимационной кривой
    /// </summary>
    [Serializable]
    [LotusSerializeAliasType("Curve")]
    public class CTweenCurveData : ILotusSerializeToXml
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        //[LotusColumn(48)]
        [LotusDisplayName(nameof(Name))]
        internal string _name;

        [SerializeField]
        //[LotusColumn(48)]
        [LotusDisplayName(nameof(Curve))]
        internal AnimationCurve _curve;

        // Данные в режиме редактора
#if UNITY_EDITOR
        [SerializeField]
        [HideInInspector]
        internal bool _expanded;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Имя анимационной кривой
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Анимационная кривая содержащая ключи. Время и значения должны быть в пределах от 0 до 1
        /// </summary>
        public AnimationCurve Curve
        {
            get { return _curve; }
            set { _curve = value; }
        }

        /// <summary>
        /// Длина времени (по умолчанию равна 1)
        /// </summary>
        public float TimeLength
        {
            get { return _curve[_curve.length - 1].time; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CTweenCurveData()
        {
            _name = string.Empty;
            _curve = AnimationCurve.Linear(0, 0, 1, 1);
        }
        #endregion

        #region ILotusSerializeToXml methods
        /// <summary>
        /// Запись данных анимационной кривой в формате элемента XML
        /// </summary>
        /// <param name="writer">Средство записи данных формата XML</param>
        public void WriteToXml(XmlWriter writer)
        {
#if UNITY_EDITOR
            writer.WriteStartAttribute("Name");
            writer.WriteValue(_name);
            writer.WriteEndAttribute();

            writer.WriteStartAttribute("PostWrapMode");
            writer.WriteValue(_curve.postWrapMode.ToString());
            writer.WriteEndAttribute();

            writer.WriteStartAttribute("PreWrapMode");
            writer.WriteValue(_curve.preWrapMode.ToString());
            writer.WriteEndAttribute();

            writer.WriteStartAttribute("CountKeys");
            writer.WriteValue(_curve.keys.Length.ToString());
            writer.WriteEndAttribute();

            for (var i = 0; i < _curve.keys.Length; i++)
            {
                writer.WriteStartElement("Key");

                writer.WriteStartAttribute("InTangent");
                writer.WriteValue(_curve.keys[i].inTangent);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("OutTangent");
                writer.WriteValue(_curve.keys[i].outTangent);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("Time");
                writer.WriteValue(_curve.keys[i].time);
                writer.WriteEndAttribute();

                writer.WriteStartAttribute("Value");
                writer.WriteValue(_curve.keys[i].value);
                writer.WriteEndAttribute();

                writer.WriteEndElement();
            }
#endif
        }

        /// <summary>
        /// Чтение данных анимационной кривой в формате элемента XML
        /// </summary>
        /// <param name="reader">Средство чтении данных формата XML</param>
        public void ReadFromXml(XmlReader reader)
        {
            Name = reader.ReadStringFromAttribute("Name", "NoName");
            _curve.postWrapMode = reader.ReadEnumFromAttribute<WrapMode>("PostWrapMode");
            _curve.preWrapMode = reader.ReadEnumFromAttribute<WrapMode>("PreWrapMode");
            var countKey = reader.ReadIntegerFromAttribute("CountKeys", 1);

            var keys = new List<Keyframe>();
            var indexKey = 0;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Key")
                    {
                        var key = new Keyframe();
                        key.inTangent = reader.ReadSingleFromAttribute("InTangent");
                        key.outTangent = reader.ReadSingleFromAttribute("OutTangent");
                        key.time = reader.ReadSingleFromAttribute("Time");
                        key.value = reader.ReadSingleFromAttribute("Value");
                        keys.Add(key);
                        indexKey++;

                        if (indexKey == countKey)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            _curve.keys = keys.ToArray();
        }
        #endregion
    }


    /// <summary>
    /// Хранилище (ресурс) анимационных кривых
    /// </summary>
    /// <remarks>
    /// Ресурс представляет методы добавления, удаления анимационных кривых, доступ к кривой по индексу, вычисление
    /// значения кривой по ключу времени, а также методы по сохранению данных всех анимационных кривых в файл и
    /// последующей загрузки
    /// </remarks>
    [Serializable]
    [LotusDescriptionType("Хранилище(ресурс) анимационных кривых")]
    public class LotusTweenCurveStorage : ScriptableObject, ILotusResourceable
    {
        #region Fields
        [LotusHeaderGroupBox("Curve Data")]
        [SerializeField]
        [LotusDisplayName("Curves")]
        //[LotusReorderable]
        //[LotusInLine]
        internal List<CTweenCurveData> _curves;
        #endregion

        #region Properties
        /// <summary>
        /// Список анимационных кривых
        /// </summary>
        public List<CTweenCurveData> Curves
        {
            get { return _curves; }
            set { _curves = value; }
        }

        /// <summary>
        /// Количество анимационных кривых
        /// </summary>
        public int Count
        {
            get { return _curves.Count; }
        }

        /// <summary>
        /// Список имен анимационных кривых
        /// </summary>
        public string[] ListNames
        {
            get
            {
                var names = new string[_curves.Count];

                for (var i = 0; i < _curves.Count; i++)
                {
                    names[i] = _curves[i].Name;
                }

                return names;
            }
        }
        #endregion

        #region ILotusResourceable methods
        /// <summary>
        /// Первичное создание хранилища анимационных кривых
        /// </summary>
        public void Create()
        {
            if (_curves == null)
            {
                _curves = new List<CTweenCurveData>();
            }
        }

        /// <summary>
        /// Первичная безопасная инициализация несериализуемых данных
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// Принудительный сброс записанных данных на диск
        /// </summary>
        public void Flush()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление новой анимационной кривой для хранения
        /// </summary>
        public void AddCurve()
        {
            _curves.Add(new CTweenCurveData());
        }

        /// <summary>
        /// Удаления анимационной кривой
        /// </summary>
        /// <param name="index">Индекс удаляемой кривой</param>
        public void RemoveCurve(int index)
        {
            _curves.RemoveAt(index);
        }

        /// <summary>
        /// Вычисление значения на кривой
        /// </summary>
        /// <param name="index">Индекс кривой</param>
        /// <param name="time">Время в диапазоне от 0 до 1</param>
        /// <returns>Соответствующие значение</returns>
        public float Evaluate(int index, float time)
        {
            return _curves[index]._curve.Evaluate(time);
        }

        /// <summary>
        /// Вычисление значения на кривой
        /// </summary>
        /// <param name="curveName">Имя анимационной кривой</param>
        /// <param name="time">Время в диапазоне от 0 до 1</param>
        /// <returns>Соответствующие значение</returns>
        public float Evaluate(string curveName, float time)
        {
            var curveStorages = Curves;

            for (var i = 0; i < curveStorages.Count; i++)
            {
                if (curveStorages[i].Name == curveName)
                {
                    return curveStorages[i]._curve.Evaluate(time);
                }
            }

            return 0f;
        }

        /// <summary>
        /// Получение индекса кривой по имени
        /// </summary>
        /// <param name="curveName">Имя анимационной кривой</param>
        /// <returns>Индекс анимационной кривой</returns>
        public int GetIndexCurve(string curveName)
        {
            var result = 0;
            var curveStorages = Curves;

            for (var i = 0; i < curveStorages.Count; i++)
            {
                if (curveStorages[i].Name == curveName)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Получение времени анимационной кривой
        /// </summary>
        /// <param name="index">Индекс анимационной кривой</param>
        /// <returns>Время</returns>
        public float GetTimeCurve(int index)
        {
            return Curves[index].Curve[Curves[index].Curve.length - 1].time;
        }

        /// <summary>
        /// Получение времени анимационной кривой
        /// </summary>
        /// <param name="curveName">Имя анимационной кривой</param>
        /// <returns>Время</returns>
        public float GetTimeCurve(string curveName)
        {
            var curveStorages = Curves;

            for (var i = 0; i < curveStorages.Count; i++)
            {
                if (curveStorages[i].Name == curveName)
                {
                    return curveStorages[i].Curve[curveStorages[i].Curve.length - 1].time;
                }
            }

            return 1f;
        }

        /// <summary>
        /// Сохранение списка анимационных кривых в отдельный файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        [LotusHeaderGroupBox("Curve Manager")]
        //[LotusMethodArgFile(DisplayName = "Save to file XML", Extension = "XML")]
        public void Save(string fileName)
        {
            if (fileName.IsExists())
            {
                XSerializationDispatcher.SaveTo(fileName, _curves);
            }
        }

        /// <summary>
        /// Загрузка новых анимационных кривых из текстового ресурса
        /// </summary>
        /// <param name="fileAsset">Текстовый ресурс - данные в формате XML</param>
        [Obsolete("Загрузка из XML не реализована. Используйте Save / ручное заполнение Curves")]
        [LotusMethodCall("Load new from file XML")]
        public void LoadNew(TextAsset fileAsset)
        {
            if (fileAsset != null)
            {
                //_curves = XSerializationDispatcher.LoadFrom<CTweenCurveData>(fileAsset.text);
            }
        }

        /// <summary>
        /// Загрузка(добавление) анимационных кривых из текстового ресурса
        /// </summary>
        /// <param name="fileAsset">Текстовый ресурс - данные в формате XML</param>
        [Obsolete("Загрузка из XML не реализована. Используйте Save / ручное заполнение Curves")]
        [LotusMethodCall("Load add from file XML")]
        public void LoadAdd(TextAsset fileAsset)
        {
            if (fileAsset != null)
            {
                //_curves.AddRange(XSerializationDispatcher.LoadListFromStringXml<CTweenCurveData>(fileAsset.text));
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
