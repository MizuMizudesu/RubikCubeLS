using UnityEngine.UI;

namespace Localization
{
	public class LocalizationText : LocalizationBase
	{
		public Text Target;

		/// <summary> 刷新显示 </summary>
		public override void Refresh()
		{
			Target.text = GetValue();
		}
	}
}