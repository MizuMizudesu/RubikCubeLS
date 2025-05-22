using Rubik;
using UnityEditor;

#if UNITY_EDITOR
namespace Other
{
	
	public static class RubikCubeColorEditor
	{
		[MenuItem("CONTEXT/RubikCubeColor/Set Color", false, int.MaxValue)]
		private static void AddUIComponentTable(MenuCommand command)
		{
			if (command.context is RubikCubeColor cubeColor)
			{
				cubeColor.SelectDefaultScheme();
			}
		}
	}
}
#endif // UNITY_EDITOR