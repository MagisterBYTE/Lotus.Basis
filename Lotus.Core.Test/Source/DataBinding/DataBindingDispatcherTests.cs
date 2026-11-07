using System;
using System.ComponentModel;

namespace Lotus.Core.DataBinding
{
    /// <summary>
    /// Тесты для <see cref="XBindingDispatcher"/>.
    /// </summary>
    public class DataBindingDispatcherTests : IDisposable
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

        public DataBindingDispatcherTests()
        {
            XBindingDispatcher.ClearBindings();
        }

        public void Dispose()
        {
            XBindingDispatcher.ClearBindings();
        }

        /// <summary>
        /// Тест создания привязки через рефлексию.
        /// </summary>
        [Fact]
        public void CreateReflection_CreatesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");

            Assert.NotNull(binding);
            Assert.Equal("TestBinding", binding.Name);
            Assert.Equal(1, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест получения привязки по имени.
        /// </summary>
        [Fact]
        public void GetBinding_WithExistingName_ReturnsBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            var binding = XBindingDispatcher.GetBinding("TestBinding");

            Assert.NotNull(binding);
            Assert.Equal("TestBinding", binding.Name);
        }

        /// <summary>
        /// Тест получения привязки по несуществующему имени возвращает null.
        /// </summary>
        [Fact]
        public void GetBinding_WithNonExistentName_ReturnsNull()
        {
            var binding = XBindingDispatcher.GetBinding("NonExistent");
            Assert.Null(binding);
        }

        /// <summary>
        /// Тест удаления привязки по имени.
        /// </summary>
        [Fact]
        public void RemoveBinding_WithName_RemovesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            Assert.Equal(1, XBindingDispatcher.Bindings.Count);

            XBindingDispatcher.RemoveBinding("TestBinding");
            Assert.Equal(0, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест удаления привязки по объекту.
        /// </summary>
        [Fact]
        public void RemoveBinding_WithBindingObject_RemovesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            Assert.Equal(1, XBindingDispatcher.Bindings.Count);

            XBindingDispatcher.RemoveBinding(binding);
            Assert.Equal(0, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест очистки всех привязок.
        /// </summary>
        [Fact]
        public void ClearBindings_RemovesAllBindings()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("Binding1", model, "Name", view, "DisplayName");
            XBindingDispatcher.CreateReflection("Binding2", model, "Name", view, "DisplayName");
            Assert.Equal(2, XBindingDispatcher.Bindings.Count);

            XBindingDispatcher.ClearBindings();
            Assert.Equal(0, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест установки статуса включенности привязки.
        /// </summary>
        [Fact]
        public void SetBindingEnabled_SetsIsEnabled()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            Assert.True(binding.IsEnabled);

            XBindingDispatcher.SetBindingEnabled("TestBinding", false);
            Assert.False(binding.IsEnabled);
        }

        /// <summary>
        /// Тест установки объекта модели привязки.
        /// </summary>
        [Fact]
        public void SetBindingModel_SetsModelInstance()
        {
            var model1 = new TestModel { Name = "Test1" };
            var model2 = new TestModel { Name = "Test2" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model1, "Name", view, "DisplayName");
            XBindingDispatcher.SetBindingModel("TestBinding", model2);

            var model = XBindingDispatcher.GetBindingModel("TestBinding");
            Assert.Equal(model2, model);
        }

        /// <summary>
        /// Тест установки объекта представления привязки.
        /// </summary>
        [Fact]
        public void SetBindingView_SetsViewInstance()
        {
            var model = new TestModel { Name = "Test" };
            var view1 = new TestView();
            var view2 = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view1, "DisplayName");
            XBindingDispatcher.SetBindingView("TestBinding", view2);

            var view = XBindingDispatcher.GetBindingView("TestBinding");
            Assert.Equal(view2, view);
        }

        /// <summary>
        /// Тест получения значения модели привязки.
        /// </summary>
        [Fact]
        public void GetBindingModelValue_ReturnsModelValue()
        {
            var model = new TestModel { Name = "TestValue" };
            var view = new TestView();

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            var value = XBindingDispatcher.GetBindingModelValue("TestBinding");

            Assert.Equal("TestValue", value);
        }

        /// <summary>
        /// Тест получения значения представления привязки.
        /// </summary>
        [Fact]
        public void GetBindingViewValue_ReturnsViewValue()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView { DisplayName = "ViewValue" };

            XBindingDispatcher.CreateReflection("TestBinding", model, "Name", view, "DisplayName");
            var value = XBindingDispatcher.GetBindingViewValue("TestBinding");

            Assert.Equal("ViewValue", value);
        }

        /// <summary>
        /// Тест создания привязки через делегат.
        /// </summary>
        [Fact]
        public void CreateDelegate_CreatesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateDelegate<string, string>("TestBinding", model, "Name", view, "DisplayName");

            Assert.NotNull(binding);
            Assert.Equal("TestBinding", binding.Name);
            Assert.Equal(1, XBindingDispatcher.Bindings.Count);
        }

        /// <summary>
        /// Тест создания привязки через делегат с конвертером.
        /// </summary>
        [Fact]
        public void CreateDelegate_WithConverter_CreatesBinding()
        {
            var model = new TestModel { Name = "Test" };
            var view = new TestView();

            var binding = XBindingDispatcher.CreateDelegate<string, string>(
                "TestBinding", model, "Name", view, "DisplayName",
                TBindingMode.ViewData,
                s => s.ToUpper());

            Assert.NotNull(binding);
            Assert.NotNull(binding.OnConvertToView);
        }
    }
}
