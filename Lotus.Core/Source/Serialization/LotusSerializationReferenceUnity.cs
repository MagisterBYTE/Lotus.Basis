#if UNITY_2017_1_OR_NEWER
namespace Lotus.Core.Serialization
{
	/// <summary>
	/// Заглушка ссылочной связи сериализации для Unity.
	/// Полная реализация может быть расширена позже.
	/// </summary>
	public class SerializeReferenceUnity
	{
		/// <summary>
		/// Выполняет связывание ссылочных данных после десериализации.
		/// </summary>
		public virtual void Link()
		{
		}
	}
}
#endif
