using System.Collections.Generic;
using Singleton;
using UI;
using UnityEngine;

namespace Mgr
{
	public class UIManager : Singleton<UIManager>
	{
		[SerializeField] private List<Panel> m_AllPanel;
		
		private readonly List<Panel> m_ActivePanel = new List<Panel>();

		public void Init()
		{
			for (int i = 0; i < m_AllPanel.Count; i++)
			{
				m_AllPanel[i].Close();
			}
		}

		public void OpenPanel<T>() where T : Panel
		{
			for (int i = 0; i < m_ActivePanel.Count; i++)
			{
				m_ActivePanel[i].Close();
			}
			m_ActivePanel.Clear();
			
			Panel panel = FindPanel<T>();
			panel.Open();
			m_ActivePanel.Add(panel);
		}

		public T FindPanel<T>() where T : Panel
		{
			for (int i = 0; i < m_AllPanel.Count; i++)
			{
				if (m_AllPanel[i].GetType() == typeof(T))
				{
					return m_AllPanel[i] as T;
				}
			}

			return null;
		}
	}
}