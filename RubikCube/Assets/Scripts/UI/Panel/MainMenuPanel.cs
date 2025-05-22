using Mgr;
using UnityEngine.UI;

namespace UI
{
	public class MainMenuPanel : Panel
	{
		public Button StartBtn;
		public Button SettingBtn;
		
		private void OnEnable()
		{
			StartBtn.onClick.AddListener(StartGame);
			SettingBtn.onClick.AddListener(OpenSettingPanel);
		}

		private void OnDisable()
		{
			StartBtn.onClick.RemoveListener(StartGame);
			SettingBtn.onClick.RemoveListener(OpenSettingPanel);
		}

		private void StartGame()
		{
			GameManager.Inst.StartGame();
		}

		private void OpenSettingPanel()
		{
			GameManager.Inst.OpenSetting();
		}
	}
}