using Mgr;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class GamePanel : Panel
	{
		public GameObject Root;
		public Button BackBtn;
		public TextMeshProUGUI TimeTxt;

		private void OnEnable()
		{
			BackBtn.onClick.AddListener(BackToMainMenu);
			GameManager.Inst.OnTimerEvent -= OnOnTimerEvent;
			GameManager.Inst.OnGameStart -= OnOnGameStart;
			
			GameManager.Inst.OnTimerEvent += OnOnTimerEvent;
			GameManager.Inst.OnGameStart += OnOnGameStart;
			Root.SetActive(false);
		}

		private void OnDisable()
		{
			BackBtn.onClick.RemoveListener(BackToMainMenu);
		}
		
		private void BackToMainMenu()
		{
			GameManager.Inst.PauseGame();
		}
		
		private void OnOnGameStart()
		{
			Root.SetActive(true);
		}
		
		private void OnOnTimerEvent(float time)
		{
			int min = (int)time / 60;
			int sec = (int)time % 60;
			TimeTxt.text = $"{min:D2}:{sec:D2}";
		}
	}
}