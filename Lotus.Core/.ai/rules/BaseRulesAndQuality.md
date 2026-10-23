# Правила качества кода Lotus.Core

Данный документ содержит правила и стандарты качества кода для проекта Lotus.Core. Эти правила должны соблюдаться при написании, рефакторинге и проверке кода.

## 1. Именование

### 1.1. Непубличные поля
- **Правило**: Все непубличные поля (private, protected, internal) должны иметь префикс `_` (нижнее подчеркивание).
- **Пример правильного кода**:
  ```csharp
  private string _name;
  protected internal int _count;
  internal List<T> _items;
  ```
- **Пример неправильного кода**:
  ```csharp
  private string name;
  protected internal int mCount;
  internal List<T> items;
  ```

### 1.2. Локальные переменные и аргументы методов
- **Правило**: Локальные переменные и аргументы методов должны быть в стиле `camelCase`.
- **Пример правильного кода**:
  ```csharp
  public void ProcessData(string itemName, int itemCount)
  {
      var currentItem = itemName;
      var totalCount = itemCount;
  }
  ```
- **Пример неправильного кода**:
  ```csharp
  public void ProcessData(string item_name, int item_count)
  {
      var current_item = item_name;
      var total_count = item_count;
  }
  ```

### 1.3. Публичные члены
- **Правило**: Публичные свойства, методы, классы и интерфейсы должны быть в стиле `PascalCase`.
- **Пример правильного кода**:
  ```csharp
  public class DataProcessor
  {
      public string ItemName { get; set; }
      public void ProcessData() { }
  }
  ```
### 1.4 Специальные требования
- **Правило**: Статические классы должны иметь префикс  `X`.
- **Пример правильного кода**:
  ```csharp
  public static class XMath
  {
      public static readonly Epsilion = 12.0; 
  }
  ```
- **Правило**: Перечисление enum иметь префикс  `T`.
- **Пример правильного кода**:
  ```csharp
  public enum TDimensionComponent
  {
    X = 1
  }
  ```
## 2. Критические ошибки

### 2.1. Логические ошибки
- **Правило**: Код должен быть проверен на логические ошибки, такие как неправильные сравнения, некорректные условия, неправильные присваивания.
- **Примеры проблем**:
  - Сравнение `a.R` с `b.G` вместо `a.R` с `b.R`
  - Присваивание `array = items` вместо `array = newArray`
  - Использование неправильного индекса (например, `[0]` вместо `[1]`)

### 2.2. Бесконечная рекурсия
- **Правило**: Методы не должны вызывать сами себя без условия выхода или с неправильным условием.
- **Пример проблемы**:
  ```csharp
  public override int GetHashCode()
  {
      return this.GetHashCode(); // Бесконечная рекурсия!
  }
  ```
- **Правильное решение**:
  ```csharp
  public override int GetHashCode()
  {
      return HashCode.Combine(_field1, _field2, _field3);
  }
  ```

### 2.3. Некорректные индексы
- **Правило**: Перед доступом к элементам коллекции по индексу необходимо проверять границы.
- **Пример проблемы**:
  ```csharp
  public void ProcessItems()
  {
      _currentTask = _tasks[_currentTaskIndex]; // Может вызвать IndexOutOfRangeException
  }
  ```
- **Правильное решение**:
  ```csharp
  public void ProcessItems()
  {
      if (_tasks.Count == 0) return;
      if (_currentTaskIndex >= 0 && _currentTaskIndex < _tasks.Count)
      {
          _currentTask = _tasks[_currentTaskIndex];
      }
  }
  ```

### 2.4. Null Reference Exceptions
- **Правило**: Перед использованием ссылочных типов необходимо проверять на null.
- **Пример проблемы**:
  ```csharp
  public override string ToString()
  {
      return $"Message: {Message}"; // Message может быть null
  }
  ```
- **Правильное решение**:
  ```csharp
  public override string ToString()
  {
      return $"Message: {Message ?? string.Empty}";
  }
  ```

## 3. Nullable Reference Types

### 3.1. Корректное использование
- **Правило**: Nullable reference types должны использоваться корректно. Свойства и параметры, которые могут быть null, должны быть помечены как nullable (`?`).
- **Пример правильного кода**:
  ```csharp
  public string? Name { get; set; }
  public void ProcessItem(ILotusTreeNode? node) { }
  ```

### 3.2. Проверка на null
- **Правило**: Перед использованием nullable значений необходимо выполнять проверку на null.
- **Пример правильного кода**:
  ```csharp
  if (node != null)
  {
      node.Process();
  }
  ```

### 3.3. Операторы null-forgiving и null-conditional
- **Правило**: Оператор `!` (null-forgiving) должен использоваться только когда есть абсолютная уверенность, что значение не null. Оператор `?` (null-conditional) предпочтительнее.
- **Пример правильного кода**:
  ```csharp
  var result = node?.GetValue() ?? defaultValue;
  ```

## 4. Производительность

### 4.1. Оптимизация поиска
- **Правило**: Для частых операций поиска использовать структуры данных с O(1) сложностью (HashSet, Dictionary) вместо O(n) (Array.IndexOf, List.Contains).
- **Пример проблемы**:
  ```csharp
  for (var i = 0; i < items.Length; i++)
  {
      if (Array.IndexOf(array, items[i]) == -1) // O(n²) сложность
      {
          Add(items[i]);
      }
  }
  ```
- **Правильное решение**:
  ```csharp
  var uniqueItems = new HashSet<TItem?>(_comparer);
  for (var i = 0; i < _count; i++)
  {
      uniqueItems.Add(_arrayOfItems[i]);
  }
  foreach (var item in items)
  {
      if (uniqueItems.Add(item)) // O(1) сложность
      {
          Add(item);
      }
  }
  ```

### 4.2. Устранение избыточных аллокаций
- **Правило**: Избегать создания временных объектов в циклах и часто вызываемых методах.
- **Пример проблемы**:
  ```csharp
  foreach (var item in items)
  {
      var temp = new StringBuilder(); // Создается в каждой итерации
      temp.Append(item);
  }
  ```

### 4.3. Оптимизация циклов
- **Правило**: Использовать наиболее эффективные конструкции для итераций.
- **Рекомендации**:
  - Для массивов использовать `for` с индексами
  - Для коллекций использовать `foreach` когда порядок не важен
  - Избегать LINQ в критичных по производительности местах

## 5. Обработка ошибок

### 5.1. Исключения вместо логирования
- **Правило**: Для некорректных аргументов и недопустимых состояний использовать исключения, а не логирование.
- **Пример проблемы**:
  ```csharp
  public void RemoveRange(int index, int count)
  {
      if (index < 0)
      {
          XLogger.LogErrorFormat("Index cannot be less than zero.");
          return;
      }
  }
  ```
- **Правильное решение**:
  ```csharp
  public void RemoveRange(int index, int count)
  {
      if (index < 0)
      {
          throw new ArgumentOutOfRangeException(nameof(index), "Index cannot be less than zero.");
      }
      if (count < 0)
      {
          throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be less than zero.");
      }
      if (index + count > _count)
      {
          throw new ArgumentException("The index and count are out of bounds for the array.");
      }
  }
  ```

### 5.2. Корректная обработка граничных случаев
- **Правило**: Все граничные случаи должны быть обработаны (пустые коллекции, null значения, отрицательные индексы и т.д.).
- **Пример правильного кода**:
  ```csharp
  public void ProcessItems()
  {
      if (_items == null || _items.Count == 0)
      {
          return;
      }
      // Обработка элементов
  }
  ```

## 6. Современные стандарты C#

### 6.1. Expression-bodied members
- **Правило**: Использовать expression-bodied members для простых свойств и методов.
- **Пример правильного кода**:
  ```csharp
  public string Name => _name;
  public int GetCount() => _items.Count;
  ```

### 6.2. Pattern matching
- **Правило**: Использовать pattern matching вместо множественных if-else или switch.
- **Пример правильного кода**:
  ```csharp
  if (obj is TypedefObject<TEnum> other)
  {
      return Equals(other);
  }
  ```

### 6.3. Современные конструкции C#
- **Правило**: Использовать современные возможности C#:
  - File-scoped namespaces (`namespace Lotus.Core;`)
  - Init-only properties (`public string Name { get; init; }`)
  - Nullable reference types
  - Record types где уместно
  - Collection expressions (`[]`, `[.. items]`)

## 7. Опечатки

### 7.1. Имена методов и переменных
- **Правило**: Все имена методов и переменных должны быть написаны без опечаток.
- **Примеры исправлений**:
  - `AssingTimePeriod` → `AssignTimePeriod`
  - `DublicateListPeriod` → `DuplicateListPeriod`
  - `RecursiveFileSysteInfo` → `RecursiveFileSystemInfo`
  - `new_arary` → `new_array`
  - `elapsed_millsecond` → `elapsed_millisecond`

### 7.2. Комментарии
- **Правило**: Комментарии должны быть без грамматических ошибок и опечаток.
- **Примеры исправлений**:
  - "не нежные" → "ненужные"
  - "сущност.и" → "сущности"
  - "или или" → "или"
  - "Без имение" → "Без имени"
  - "Отчистить" → "Очистить"
  - "Comprare" → "Compare"

## 8. Дублирование кода

### 8.1. Устранение дублирования
- **Правило**: Избегать дублирования кода. Выносить повторяющуюся логику в отдельные методы.
- **Пример проблемы**:
  ```csharp
  if (parent is TreeNodeObservable parentVm)
  {
      parentVm.SetCheckedStatus(true);
  }
  // ...
  if (parent is TreeNodeObservable parentVm1)
  {
      parentVm1.SetCheckedStatus(null);
  }
  // ...
  if (parent is TreeNodeObservable parentVm2)
  {
      parentVm2.SetCheckedStatus(false);
  }
  ```
- **Правильное решение**:
  ```csharp
  if (parent is TreeNodeObservable parentVm)
  {
      if (allChecked)
      {
          parentVm.SetCheckedStatus(true);
      }
      else if (someChecked)
      {
          parentVm.SetCheckedStatus(null);
      }
      else
      {
          parentVm.SetCheckedStatus(false);
      }
  }
  ```

### 8.2. Дублированные присваивания
- **Правило**: Избегать дублированных присваиваний одной и той же переменной.
- **Пример проблемы**:
  ```csharp
  node = parameterExpression;
  return node; // Должно быть: return parameterExpression;
  ```

## 9. Комментарии

### 9.1. Завершение точкой
- **Правило**: Все комментарии должны заканчиваться точкой.
- **Пример правильного кода**:
  ```csharp
  /// <summary>
  /// Получение индекса расположения узла дерева.
  /// </summary>
  /// <param name="this">Текущий узел.</param>
  /// <returns>Индекс расположения или -1.</returns>
  ```
- **Пример неправильного кода**:
  ```csharp
  /// <summary>
  /// Получение индекса расположения узла дерева
  /// </summary>
  /// <param name="this">Текущий узел</param>
  /// <returns>Индекс расположения или -1</returns>
  ```

### 9.2. XML-комментарии
- **Правило**: XML-комментарии должны быть полными и корректными для всех публичных членов.
- **Требования**:
  - `<summary>` для описания члена
  - `<param>` для всех параметров
  - `<returns>` для методов, возвращающих значения
  - `<remarks>` для дополнительной информации
  - `<exception>` для исключений

### 9.3. Актуальность комментариев
- **Правило**: Комментарии должны быть актуальными и отражать текущее состояние кода.
- **Правило**: Удалять устаревшие или неактуальные комментарии.
- **Правило**: Комментарии должны быть полезными и объяснять "почему", а не "что" (код сам объясняет "что").

## 10. Дополнительные рекомендации

### 10.1. Структура кода
- Использовать регионы (`#region`) для организации больших классов
- Группировать связанные методы и свойства
- Следовать единому стилю форматирования

### 10.2. Тестирование
- Покрывать критичный код unit-тестами
- Тестировать граничные случаи
- Тестировать обработку ошибок

### 10.3. Документация
- Поддерживать актуальную документацию
- Использовать XML-комментарии для публичного API
- Документировать сложную бизнес-логику

## Применение правил

При проверке кода необходимо:
1. Проверить соответствие всем правилам именования
2. Выявить критические ошибки и потенциальные проблемы
3. Проверить корректное использование nullable reference types
4. Оптимизировать производительность где это возможно
5. Убедиться в корректной обработке ошибок
6. Применить современные стандарты C#
7. Исправить все опечатки
8. Устранить дублирование кода
9. Проверить, что все комментарии заканчиваются точкой