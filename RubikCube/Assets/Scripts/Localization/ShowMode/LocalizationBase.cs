using System;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
	[Serializable]
	public class TranslationText
	{
		public ELanguage Language;
		public string Value;
	}
	
	public abstract class LocalizationBase : MonoBehaviour
	{
#if UNITY_EDITOR
		[SerializeField] protected ELanguage DebugType;
#endif // UNITY_EDITOR
		
		public List<TranslationText> Translations = new List<TranslationText>();
		
		private void Awake()
		{
			LocalizationMgr.AddLocalize(this);
		}

		private void OnEnable()
		{
			Refresh();
		}

		protected string GetValue()
		{
			ELanguage targetLanguage = LocalizationMgr.Language;
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				targetLanguage = DebugType;
			}
#endif // UNITY_EDITOR
			for (int i = 0; i < Translations.Count; i++)
			{
				if (Translations[i].Language == targetLanguage)
				{
					return Translations[i].Value;
				}
			}
			return string.Empty;
		}
		
		public abstract void Refresh();
		
		private void OnDestroy()
		{
			LocalizationMgr.RemoveLocalize(this);
		}
	}
}