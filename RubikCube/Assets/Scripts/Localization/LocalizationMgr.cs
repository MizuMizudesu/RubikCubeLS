using System.Collections.Generic;

namespace Localization
{
	/// <summary> 语言种类 </summary>
	public enum ELanguage
	{
		CN = 0, // 中文
		EN = 1, // 英文
		ES = 2, // 西班牙
	}

	/// <summary> 本地化管理 </summary>
	public static class LocalizationMgr
	{
		private static readonly List<LocalizationBase> s_AllLanguage = new List<LocalizationBase>();

		public static ELanguage Language { get; private set; } = ELanguage.CN;

		/// <summary> 添加需要被本地化的 </summary>
		public static void AddLocalize(LocalizationBase localization)
		{
			s_AllLanguage.Add(localization);
		}

		/// <summary> 移除 </summary>
		public static void RemoveLocalize(LocalizationBase localization)
		{
			s_AllLanguage.Remove(localization);
		}

		/// <summary> 设置语言 </summary>
		public static void SetLanguage(ELanguage language)
		{
			Language = language;
			
			for (int i = 0; i < s_AllLanguage.Count; i++)
			{
				s_AllLanguage[i].Refresh();
			}
		}
	}
}
