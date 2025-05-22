#if UNITY_EDITOR
using UnityEditor;

namespace Localization
{
	public static class LocalizationEditor
	{
		[MenuItem("CONTEXT/LocalizationBase/Debug", false, -int.MaxValue)]
		private static void AddUIComponentTable(MenuCommand command)
		{
			if (command.context is LocalizationBase local)
			{
				local.Refresh();
			}
		}
	}
}
#endif // UNITY_EDITOR