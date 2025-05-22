using System;
using Localization;
using Singleton;
using UI;
using UnityEngine;

namespace Mgr
{
	public enum GameState
	{
		Idle = 0,
		Start = 1,
		Playing = 2,
		Pause = 3,
	}
	
	public class GameManager : Singleton<GameManager>
	{
		public ELanguage GameLanguage;

		public ColorChange colorChange;
		public AudioSource BgAudioSource;
		public bool BgAudioPlaying { get; private set; } = true;
		public event Action<float> OnTimerEvent;
		public event Action OnGameStart; 
		public GameState CurGameState { get; private set; }

		public TouchInput TouchInput;
		public RubikInputController RubikInputController;
		public RubikCube RubikCube;

		private float m_Timer;
		
		private void Start()
		{
			Application.targetFrameRate = 30;
			LocalizationMgr.SetLanguage(GameLanguage);
			UIManager.Inst.Init();
			UIManager.Inst.OpenPanel<MainMenuPanel>();
			RubikInputController.SetInput(false);
			CurGameState = GameState.Idle;
		}

		public void StartGame()
		{
			if (CurGameState == GameState.Idle)
			{
				// TODO：这里要获取上次魔方
				RubikCube.GetComponent<Animator>().enabled = false;
				RubikCube.transform.localPosition = Vector3.zero;  
				RubikCube.transform.localEulerAngles = Vector3.zero;
				CurGameState = GameState.Start;
				RubikCube.StartGame(); // 打乱魔方
				UIManager.Inst.OpenPanel<GamePanel>();	
			}
			else if (CurGameState == GameState.Pause)
			{
				ResumeGame();
			}
		}

		// 魔方打乱完成，开始
		public void OnStartGame()
		{
			if (CurGameState == GameState.Start)
			{
				m_Timer = 0;
				CurGameState = GameState.Playing;
				RubikInputController.SetInput(true);
			}
			OnGameStart?.Invoke();
		}
		
		public void PauseGame()
		{
			CurGameState = GameState.Pause;
			RubikInputController.SetInput(false);
			UIManager.Inst.OpenPanel<MainMenuPanel>();
		}

		public void ResumeGame()
		{
			CurGameState = GameState.Playing;
			RubikInputController.SetInput(true);
			UIManager.Inst.OpenPanel<GamePanel>();
			OnGameStart?.Invoke();
		}
		
		public void OpenSetting()
		{
			UIManager.Inst.OpenPanel<SettingPanel>();
		}
		
		// setting 返回主页
		public void BackToMainMenu()
		{
			RubikCube.GetComponent<Animator>().enabled = true;
			UIManager.Inst.OpenPanel<MainMenuPanel>();
		}

		private void Update()
		{
			if (CurGameState == GameState.Playing)
			{
				m_Timer += Time.deltaTime;
				OnTimerEvent?.Invoke(m_Timer);
			}
		}
		
		public void OnGameCompleted()
		{
			CurGameState = GameState.Idle;
			RubikInputController.SetInput(false);
			RubikCube.ResetCube();
			// TODO：游戏胜利，缺失分数显示
			UIManager.Inst.OpenPanel<MainMenuPanel>();
		}
		

		#region ---------------------------------- 设置界面 ----------------------------------------------

		public void ChangeColor(Color color)
		{
			colorChange.Change(color);
		}

		public void ChangeLanguage(ELanguage language)
		{
			GameLanguage = language;
			LocalizationMgr.SetLanguage(GameLanguage);
		}

		public void SetBgAudio(bool open, float volume = -1)
		{
			BgAudioSource.volume = Mathf.Approximately(volume, -1) ? BgAudioSource.volume : volume;
			if (open == BgAudioPlaying) return;
			
			GameManager.Inst.BgAudioPlaying = open;
			if (open) BgAudioSource.Play();
			else BgAudioSource.Pause();
		}

		#endregion //  ---------------------------------- 设置界面 ----------------------------------------------
	}
}