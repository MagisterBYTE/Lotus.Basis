#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;

namespace Lotus.Core
{
    /// <summary>
    /// Статический класс для тестирования подсистемы параметрических объектов модуля базового ядра.
    /// </summary>
    public static class XCoreParametersTesting
    {
        /// <summary>
        /// Тестирование подсистемы параметрических объектов.
        /// </summary>
        [Test]
        public static void TestParameters()
        {
            // Просто объект
            // "Человек":
            // {
            //	"Имя" : "Даниил"
            //  "Фамилия" : "Дементьев"
            //  "Возраст" : 39
            // }

            var parameterHuman = new CParameters("Человек",
                new CParameterString("Имя", "Даниил"),
                new CParameterString("Фамилия", "Дементьев"),
                new CParameterInteger("Возраст", 39));

            parameterHuman.SaveToJson("D:\\parameterHuman.json", true);

            // Просто объект
            // "Человек":
            // {
            //	"Имя" : "Даниил"
            //  "Фамилия" : "Дементьев"
            //  "Возраст" : 39
            //  "Дети"
            //  [
            //	  {
            //	    "Имя" : "Яна"
            //      "Фамилия" : "Дементьева"
            //      "Возраст" : "2"
            //	  }
            //  ]
            // }

            var parameterHuman2 = new CParameters("Человек",
                new CParameterString("Имя", "Даниил"),
                new CParameterString("Фамилия", "Дементьев"),
                new CParameterInteger("Возраст", 39));

            var parameterChild = parameterHuman2.AddListParameter("Дети");
            parameterChild.Value.Add(new CParameters("Человек",
                new CParameterString("Имя", "Яна"),
                new CParameterString("Фамилия", "Дементьева"),
                new CParameterInteger("Возраст", 2)));
            parameterChild.Value.Add(new CParameters("Человек",
                new CParameterString("Имя", "Яна"),
                new CParameterString("Фамилия", "Дементьева"),
                new CParameterInteger("Возраст", 2)));

            var parameterIds = parameterHuman2.AddListTemplate<int>("Ids");
            for (var i = 0; i < 1000; i++)
            {
                parameterIds.Value.Add(i + 1);
            }


            parameterHuman2.SaveToJson("D:\\parameterHuman2.json", true);
        }
    }
}