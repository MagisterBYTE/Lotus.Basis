# Lotus.Basis
Комплексное решение для платформы NET

## Состоит из следующий проектов:
 - `Lotus.Core` - модуль, содержащий код, развивающий в целом платформу NET и реализующий дополнительную базовую функциональность на уровне среды NET.
 - `Lotus.Math` - математический модуль реализующий работу с математическими и геометрическими структурами данных.
 - `Lotus.Algorithm` - модуль алгоритмов реализует распространены алгоритмы по поиску пути, интерполяции данных, работу с графами, алгоритмы сортировки и упорядочивания данных, заливку областей, а также базовые алгоритмы генерации контента.
 - `Lotus.Object3D` - модуль трехмерного объекта обеспечивает универсальное представление трёхмерного объекта, реализует загрузку и экспорт в различные форматы файлов и также представляет различные генераторы для создания параметрических трехмерных объектов.
 - `Lotus.Repository` - модуль репозитория определяет базовый интерфейс репозитория, интерфейс для реализации хранилища данных, а также унифицированные механизмы получения и запроса данных. Также модуль реализует хранилище данных через базу данных и для отдельного файла.
 - `Lotus.UnitMeasurement` - модуль единиц измерения обеспечивает начальную инфраструктуру для представления базовых типов измерения и соответствующую им систему единиц измерения.
 - `Lotus.Localization` - модуль реализующий универсальный унифицированный механизм локализации через различные сервисы локализации.

## Актуальные версии:
 - `Lotus.Core` - проект протестирован, актуальная версия **3.0.2** от **14.06.2024**, загружено на github и Nuget
 - `Lotus.Math` - проект протестирован, актуальная версия **3.0.2** от **14.06.2024**, загружено на github и Nuget
 - `Lotus.Algorithm` - проект протестирован, актуальная версия **3.0.2** от **14.06.2024**, загружено на github и Nuget
 - `Lotus.Object3D` - проект протестирован, актуальная версия **3.0.2** от **14.06.2024**, загружено на github и Nuget
 - `Lotus.Repository` - проект протестирован, актуальная версия **3.0.2** от **14.06.2024**, загружено на github и Nuget
 - `Lotus.UnitMeasurement` - проект протестирован, актуальная версия **3.0.2** от **14.06.2024**, загружено на github и Nuget
 - `Lotus.Localization` - проект протестирован, актуальная версия **3.0.2** от **14.06.2024**, загружено на github и Nuget

## Используемые директивы
* USE_WINDOWS - включает зависимость от платформы WPF
* USE_GDI - включает зависимость от библиотек System.Drawing
* USE_SHARPDX - включает зависимость от библиотек SharpDX (и соответствующих нативных библиотек)
* USE_HELIX - включает зависимость от библиотек Helix.WPF
* USE_ASSIMP - включает зависимость от библиотек AssimpNet (и соответствующих нативных библиотек)