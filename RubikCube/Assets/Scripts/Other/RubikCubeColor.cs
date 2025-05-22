using UnityEngine;

namespace Rubik
{
	public class RubikCubeColor : MonoBehaviour
	{
		private enum SideName
		{
			Rear,
			Front,
			Left,
			Right,
			Top,
			Bottom
		}

		public UnityEngine.Color topColor;
		public UnityEngine.Color bottomColor;
		public UnityEngine.Color leftColor;
		public UnityEngine.Color rightColor;
		public UnityEngine.Color frontColor;
		public UnityEngine.Color backColor;

		public void SelectDefaultScheme()
		{
			SetColor(SideName.Top, topColor);
			SetColor(SideName.Bottom, bottomColor);
			SetColor(SideName.Left, rightColor); // 这里左右在GameObject界面对象搞错了，所以要反着来，想恢复正常需要更改GameObject对象名字
			SetColor(SideName.Right, leftColor); // 这里左右在GameObject界面对象搞错了，所以要反着来
			SetColor(SideName.Front, frontColor);
			SetColor(SideName.Rear, backColor);
		}

		private void SetColor(SideName sideName, UnityEngine.Color color)
		{
			switch (sideName)
			{
				case SideName.Rear:
					SetColorRearSide(color);
					break;
				case SideName.Front:
					SetColorFrontSide(color);
					break;
				case SideName.Left:
					SetColorAllOtherSide("Left", color);
					break;
				case SideName.Right:
					SetColorAllOtherSide("Right", color);
					break;
				case SideName.Top:
					SetColorAllOtherSide("Top", color);
					break;
				case SideName.Bottom:
					SetColorAllOtherSide("Bottom", color);
					break;
			}
		}

		private void SetColorRearSide(UnityEngine.Color color)
		{
			for (int i = 1; i <= 9; i++)
			{
				if (transform.Find("Rear").Find(i.ToString()).Find("Front"))
				{
					transform.Find("Rear").Find(i.ToString()).Find("Front").GetComponent<MeshRenderer>().material.color =
						color;
				}
			}
		}

		private void SetColorFrontSide(UnityEngine.Color color)
		{
			for (int i = 1; i <= 9; i++)
			{
				if (transform.Find("Front").Find(i.ToString()).Find("Front"))
				{
					transform.Find("Front").Find(i.ToString()).Find("Front").GetComponent<MeshRenderer>().material.color =
						color;
				}
			}
		}

		private void SetColorAllOtherSide(string sideName, UnityEngine.Color color)
		{
			for (int i = 1; i <= 9; i++)
			{
				if (transform.Find("Rear").Find(i.ToString()).Find(sideName))
				{
					transform.Find("Rear").Find(i.ToString()).Find(sideName).GetComponent<MeshRenderer>().material.color =
						color;
				}

				if (transform.Find("Front").Find(i.ToString()).Find(sideName))
				{
					transform.Find("Front").Find(i.ToString()).Find(sideName).GetComponent<MeshRenderer>().material
						.color = color;
				}

				if (transform.Find("Center").Find(i.ToString()).Find(sideName))
				{
					transform.Find("Center").Find(i.ToString()).Find(sideName).GetComponent<MeshRenderer>().material
						.color = color;
				}
			}
		}
	}
}