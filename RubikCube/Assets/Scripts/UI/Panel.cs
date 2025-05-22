using UnityEngine;

namespace UI
{
	public abstract class Panel : MonoBehaviour
	{
		public virtual void Open()
		{
			gameObject.SetActive(true);
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}