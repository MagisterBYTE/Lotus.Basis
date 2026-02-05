using System;
using System.ComponentModel;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.DataBinding
{
    /// <summary>
    /// Тесты для <see cref="XBindingDispatcher"/>.
    /// </summary>
    [TestFixture]
    public class DataBindingDispatcherTests
    {
        /// <summary>
        /// Тестовый класс модели.
        /// </summary>
        private class TestModel : INotifyPropertyChanged
        {
            private string _name = string.Empty;

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Тестовый класс представления.
        /// </summary>
        private class TestView : INotifyPropertyChanged
        {
            private string _displayName = string.Empty;

            public string DisplayName
            {
                get => _displayName;
                set
                {
                    _displayName = value;
                    OnPropertyChanged(nameof(DisplayName));
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [SetUp]
        public void SetUp()
        {
            XBindingDispatcher.ClearBindings();
        }

        [TearDown]
        public void TearDown()
        {
            XBindingDispatcher.ClearBindings();
        }

        /// <summary>
        /// Тест создания привязки через рефлексию.
        /// </summary>
        [Test]
        public void CreateReflection_CreatesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");

            ClassicAssert.IsNotNull(binding);
            ClassicAssert.AreEqual("TestBinding", binding.Name);
            ClassicAssert.AreEqual(1, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест получения привязки по имени.
        /// </summary>
        [Test]
        public void GetBinding_WithExistingName_ReturnsBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            var binding = XBindingDispatcher.GetBinding("TestBinding");

            ClassicAssert.IsNotNull(binding);
            ClassicAssert.AreEqual("TestBinding", binding.Name);
        }

        /// <summary>
        /// Тест получения привязки по несуществующему имени возвращает null.
        /// </summary>
        [Test]
        public void GetBinding_WithNonExistentName_ReturnsNull()
        {
            var binding = XBindingDispatcher.GetBinding("NonExistent");
            ClassicAssert.IsNull(binding);
        }

        /// <summary>
        /// Тест удаления привязки по имени.
        /// </summary>
        [Test]
        public void RemoveBinding_WithName_RemovesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            ClassicAssert.AreEqual(1, XBindingDispatcher.Bindings.Count);

            XBindingDispatcher.RemoveBinding("TestBinding");
            ClassicAssert.AreEqual(0, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест удаления привязки по объекту.
        /// </summary>
        [Test]
        public void RemoveBinding_WithBindingObject_RemovesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            ClassicAssert.AreEqual(1, XBindingDispatcher.Bindings.Count);

            XBindingDispatcher.RemoveBinding(binding);
            ClassicAssert.AreEqual(0, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест очистки всех привязок.
        /// </summary>
        [Test]
        public void ClearBindings_RemovesAllBindings()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("Binding1", model, "Name", view, "DisplayName");
            XBindingDispatcher.CreateReflection("Binding2", model, "Name", view, "DisplayName");
            ClassicAssert.AreEqual(2, XBindingDispatcher.Bindings.Count);

            XBindingDispatcher.ClearBindings();
            ClassicAssert.AreEqual(0, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест установки статуса включенности привязки.
        /// </summary>
        [Test]
        public void SetBindingEnabled_SetsIsEnabled()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            ClassicAssert.IsTrue(binding.IsEnabled);

            XBindingDispatcher.SetBindingEnabled("TestBinding", false);
            ClassicAssert.IsFalse(binding.IsEnabled);
        }

        /// <summary>
        /// Тест установки объекта модели привязки.
        /// </summary>
        [Test]
        public void SetBindingModel_SetsModelInstance()
        {
            var model1 = new TestModel { Name = "Test1" };
            var model2 = new TestModel { Name = "Test2" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model1, "Name", view, "DisplayName");
            XBindingDispatcher.SetBindingModel("TestBinding", model2);

            var model = XBindingDispatcher.GetBindingModel("TestBinding");
            ClassicAssert.AreEqual(model2, model);
        }

        /// <summary>
        /// Тест установки объекта представления привязки.
        /// </summary>
        [Test]
        public void SetBindingView_SetsViewInstance()
        {
            var model = new TestModel { Name = "Test" };
            var view1 = new TestView();
            var view2 = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view1, "DisplayName");
            XBindingDispatcher.SetBindingView("TestBinding", view2);

            var view = XBindingDispatcher.GetBindingView("TestBinding");
            ClassicAssert.AreEqual(view2, view);
        }

        /// <summary>
        /// Тест получения значения модели привязки.
        /// </summary>
        [Test]
        public void GetBindingModelValue_ReturnsModelValue()
        {
            var model = new TestModel { Name = "TestValue" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            var value = XBindingDispatcher.GetBindingModelValue("TestBinding");

            ClassicAssert.AreEqual("TestValue", value);
        }

        /// <summary>
        /// Тест получения значения представления привязки.
        /// </summary>
        [Test]
        public void GetBindingViewValue_ReturnsViewValue()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView { DisplayName = "ViewValue" };

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            var value = XBindingDispatcher.GetBindingViewValue("TestBinding");

            ClassicAssert.AreEqual("ViewValue", value);
        }

        /// <summary>
        /// Тест создания привязки через делегат.
        /// </summary>
        [Test]
        public void CreateDelegate_CreatesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateDelegate<string, string>("TestBinding", model, "Name", view, "DisplayName");

            ClassicAssert.IsNotNull(binding);
            ClassicAssert.AreEqual("TestBinding", binding.Name);
            ClassicAssert.AreEqual(1, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест создания привязки через делегат с конвертером.
        /// </summary>
        [Test]
        public void CreateDelegate_WithConverter_CreatesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateDelegate<string, string>(
                "TestBinding", model, "Name", view, "DisplayName",
                TBindingMode.ViewData,
                s => s.ToUpper());

            ClassicAssert.IsNotNull(binding);
            ClassicAssert.IsNotNull(binding.OnConvertToView);
        }
    }
}
