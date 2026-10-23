#if UNITY_2017_1_OR_NEWER
namespace System.Runtime.CompilerServices
{
	/// <summary>
	/// Polyfill для поддержки <c>init</c>-аксессоров в Unity / netstandard2.1.
	/// </summary>
	internal static class IsExternalInit
	{
	}
}
#endif
