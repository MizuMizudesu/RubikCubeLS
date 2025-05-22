using Localization;
using Mgr;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class SettingPanel : Panel
	{
		public Button m_BackGame;
		[Space]
		public Slider m_DisturbSlider;
		public Slider m_FlipSlider;
		[Space]
		public Slider m_ColorSlider;
		[Space]
		public Slider m_VolumeSlider;
		public Button m_BgVolumeBtn;
		public GameObject m_BgAudioOpen;
		public GameObject m_BgAudioClose;
		
		[Space]
		public TMP_Dropdown m_LanguageDropdown;
		
		private readonly Color[] m_Colors =
		{
			Color.white,
			new Color(0f, 100 / 255f, 255 / 255f, 165 / 255f),
			new Color(205 / 255f, 166 / 255f, 107 / 255f, 165 / 255f),
			new Color(131 / 255f, 173 / 255f, 45 / 255f, 165 / 255f),
			new Color(5 / 255f, 250 / 255f, 230 / 255f, 165 / 255f),
		};
		
		private void OnEnable()
		{
			m_BackGame.onClick.AddListener(OnBackGame);
			m_DisturbSlider.onValueChanged.AddListener(OnDisturbChange);
			m_FlipSlider.onValueChanged.AddListener(OnFlipChange);
			m_ColorSlider.onValueChanged.AddListener(OnColorChange);
			m_VolumeSlider.onValueChanged.AddListener(OnVolumeChange);
			m_BgVolumeBtn.onClick.AddListener(OnBgVolumeClick);
			m_LanguageDropdown.onValueChanged.AddListener(OnLanguageChange);
			
			m_BgAudioOpen.SetActive(GameManager.Inst.BgAudioPlaying);
			m_BgAudioClose.SetActive(!GameManager.Inst.BgAudioPlaying);
			m_VolumeSlider.value = 1;
		}

		private void OnDisable()
		{
			m_BackGame.onClick.RemoveListener(OnBackGame);
			m_ColorSlider.onValueChanged.RemoveListener(OnColorChange);
			m_FlipSlider.onValueChanged.RemoveListener(OnFlipChange);
			m_ColorSlider.onValueChanged.RemoveListener(OnColorChange);
			m_VolumeSlider.onValueChanged.RemoveListener(OnVolumeChange);
			m_BgVolumeBtn.onClick.RemoveListener(OnBgVolumeClick);
			m_LanguageDropdown.onValueChanged.RemoveListener(OnLanguageChange);
		}

		private void OnLanguageChange(int value)
		{
			GameManager.Inst.ChangeLanguage((ELanguage)value);
		}

		private void OnBgVolumeClick()
		{
			bool open = !GameManager.Inst.BgAudioPlaying;
			m_BgAudioOpen.SetActive(open);
			m_BgAudioClose.SetActive(!open);
			GameManager.Inst.SetBgAudio(open);
		}

		private void OnVolumeChange(float value)
		{
			GameManager.Inst.SetBgAudio(GameManager.Inst.BgAudioPlaying, value);
		}

		private void OnFlipChange(float arg0)
		{
			
		}

		private void OnDisturbChange(float arg0)
		{
			
		}
		
		private void OnBackGame()
		{
			GameManager.Inst.BackToMainMenu();
		}
		
		private void OnColorChange(float value)
		{
			GameManager.Inst.ChangeColor(m_Colors[(int)value]);
		}
	}
}