using TMPro;

namespace Localization
{
	public class LocalizationTMP : LocalizationBase
	{
		public TextMeshProUGUI Target;

		/// <summary> 刷新显示 </summary>
		public override void Refresh()
		{
			Target.text = GetValue();
		}
	}
}