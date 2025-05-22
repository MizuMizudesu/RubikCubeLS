using UnityEngine;
using static TouchInput;

public class RubikInputController : MonoBehaviour
{
	public RubikCube rubikCube;
	public TouchInput touchInput;
	public GameObject rubikCubeInput;

	private void OnEnable()
	{
		touchInput.OnTouchEvent += OnGetInput;
	}

	private void OnDisable()
	{
		touchInput.OnTouchEvent -= OnGetInput;
	}

	public void SetInput(bool on)
	{
		for (int i = 0; i < rubikCubeInput.transform.childCount; i++)
		{
			rubikCubeInput.transform.GetChild(i).GetComponent<BoxCollider>().enabled = on;
		}
	}

	//这里是还原魔方的过程
	private void OnGetInput(Transform cubeTransform, Movement movement)
	{
		if (cubeTransform.name.IndexOf("Rear") != -1)
		{
			switch (movement)
			{
				case Movement.Left:
					switch (cubeTransform.name)
					{
						case "Rear-1":
						case "Rear-2":
						case "Rear-3":
							rubikCube.SlideLeftTop();
							break;
						case "Rear-4":
						case "Rear-5":
						case "Rear-6":
							rubikCube.SlideLeftCenter();
							break;
						case "Rear-7":
						case "Rear-8":
						case "Rear-9":
							rubikCube.SlideLeftBottom();
							break;
					}

					break;
				case Movement.Right:
					switch (cubeTransform.name)
					{
						case "Rear-1":
						case "Rear-2":
						case "Rear-3":
							rubikCube.SlideRightTop();
							break;
						case "Rear-4":
						case "Rear-5":
						case "Rear-6":
							rubikCube.SlideRightCenter();
							break;
						case "Rear-7":
						case "Rear-8":
						case "Rear-9":
							rubikCube.SlideRightBottom();
							break;
					}

					break;
				case Movement.Up:
					switch (cubeTransform.name)
					{
						case "Rear-1":
						case "Rear-4":
						case "Rear-7":
							rubikCube.SlideUpLeft();
							break;
						case "Rear-2":
						case "Rear-5":
						case "Rear-8":
							rubikCube.SlideUpCenter();
							break;
						case "Rear-3":
						case "Rear-6":
						case "Rear-9":
							rubikCube.SlideUpRight();
							break;
					}

					break;
				case Movement.Down:
					switch (cubeTransform.name)
					{
						case "Rear-1":
						case "Rear-4":
						case "Rear-7":
							rubikCube.SlideDownLeft();
							break;
						case "Rear-2":
						case "Rear-5":
						case "Rear-8":
							rubikCube.SlideDownCenter();
							break;
						case "Rear-3":
						case "Rear-6":
						case "Rear-9":
							rubikCube.SlideDownRight();
							break;
					}

					break;
			}
		}
		else if (cubeTransform.name.IndexOf("Front") != -1)
		{
			switch (movement)
			{
				case Movement.Left:
					switch (cubeTransform.name)
					{
						case "Front-1":
						case "Front-2":
						case "Front-3":
							rubikCube.SlideLeftTop();
							break;
						case "Front-4":
						case "Front-5":
						case "Front-6":
							rubikCube.SlideLeftCenter();
							break;
						case "Front-7":
						case "Front-8":
						case "Front-9":
							rubikCube.SlideLeftBottom();
							break;
					}

					break;
				case Movement.Right:
					switch (cubeTransform.name)
					{
						case "Front-1":
						case "Front-2":
						case "Front-3":
							rubikCube.SlideRightTop();
							break;
						case "Front-4":
						case "Front-5":
						case "Front-6":
							rubikCube.SlideRightCenter();
							break;
						case "Front-7":
						case "Front-8":
						case "Front-9":
							rubikCube.SlideRightBottom();
							break;
					}

					break;
				case Movement.Up:
					switch (cubeTransform.name)
					{
						case "Front-1":
						case "Front-4":
						case "Front-7":
							rubikCube.SlideDownRight();
							break;
						case "Front-2":
						case "Front-5":
						case "Front-8":
							rubikCube.SlideDownCenter();
							break;
						case "Front-3":
						case "Front-6":
						case "Front-9":
							rubikCube.SlideDownLeft();
							break;
					}

					break;
				case Movement.Down:
					switch (cubeTransform.name)
					{
						case "Front-1":
						case "Front-4":
						case "Front-7":
							rubikCube.SlideUpRight();
							break;
						case "Front-2":
						case "Front-5":
						case "Front-8":
							rubikCube.SlideUpCenter();
							break;
						case "Front-3":
						case "Front-6":
						case "Front-9":
							rubikCube.SlideUpLeft();
							break;
					}

					break;
			}
		}
		else if (cubeTransform.name.IndexOf("Left") != -1)
		{
			switch (movement)
			{
				case Movement.Left:
					switch (cubeTransform.name)
					{
						case "Left-1":
						case "Left-2":
						case "Left-3":
							rubikCube.SlideLeftTop();
							break;
						case "Left-4":
						case "Left-5":
						case "Left-6":
							rubikCube.SlideLeftCenter();
							break;
						case "Left-7":
						case "Left-8":
						case "Left-9":
							rubikCube.SlideLeftBottom();
							break;
					}

					break;
				case Movement.Right:
					switch (cubeTransform.name)
					{
						case "Left-1":
						case "Left-2":
						case "Left-3":
							rubikCube.SlideRightTop();
							break;
						case "Left-4":
						case "Left-5":
						case "Left-6":
							rubikCube.SlideRightCenter();
							break;
						case "Left-7":
						case "Left-8":
						case "Left-9":
							rubikCube.SlideRightBottom();
							break;
					}

					break;
				case Movement.Up:
					switch (cubeTransform.name)
					{
						case "Left-1":
						case "Left-4":
						case "Left-7":
							rubikCube.SlideRightWholeFront();
							break;
						case "Left-2":
						case "Left-5":
						case "Left-8":
							rubikCube.SlideRightWholeCenter();
							break;
						case "Left-3":
						case "Left-6":
						case "Left-9":
							rubikCube.SlideRightWholeRear();
							break;
					}

					break;
				case Movement.Down:
					switch (cubeTransform.name)
					{
						case "Left-1":
						case "Left-4":
						case "Left-7":
							rubikCube.SlideLeftWholeFront();
							break;
						case "Left-2":
						case "Left-5":
						case "Left-8":
							rubikCube.SlideLeftWholeCenter();
							break;
						case "Left-3":
						case "Left-6":
						case "Left-9":
							rubikCube.SlideLeftWholeRear();
							break;
					}

					break;
			}
		}
		else if (cubeTransform.name.IndexOf("Right") != -1)
		{
			switch (movement)
			{
				case Movement.Left:
					switch (cubeTransform.name)
					{
						case "Right-1":
						case "Right-2":
						case "Right-3":
							rubikCube.SlideLeftTop();
							break;
						case "Right-4":
						case "Right-5":
						case "Right-6":
							rubikCube.SlideLeftCenter();
							break;
						case "Right-7":
						case "Right-8":
						case "Right-9":
							rubikCube.SlideLeftBottom();
							break;
					}

					break;
				case Movement.Right:
					switch (cubeTransform.name)
					{
						case "Right-1":
						case "Right-2":
						case "Right-3":
							rubikCube.SlideRightTop();
							break;
						case "Right-4":
						case "Right-5":
						case "Right-6":
							rubikCube.SlideRightCenter();
							break;
						case "Right-7":
						case "Right-8":
						case "Right-9":
							rubikCube.SlideRightBottom();
							break;
					}

					break;
				case Movement.Up:
					switch (cubeTransform.name)
					{
						case "Right-1":
						case "Right-4":
						case "Right-7":
							rubikCube.SlideLeftWholeRear();
							break;
						case "Right-2":
						case "Right-5":
						case "Right-8":
							rubikCube.SlideLeftWholeCenter();
							break;
						case "Right-3":
						case "Right-6":
						case "Right-9":
							rubikCube.SlideLeftWholeFront();
							break;
					}

					break;
				case Movement.Down:
					switch (cubeTransform.name)
					{
						case "Right-1":
						case "Right-4":
						case "Right-7":
							rubikCube.SlideRightWholeRear();
							break;
						case "Right-2":
						case "Right-5":
						case "Right-8":
							rubikCube.SlideRightWholeCenter();
							break;
						case "Right-3":
						case "Right-6":
						case "Right-9":
							rubikCube.SlideRightWholeFront();
							break;
					}

					break;
			}
		}
		else if (cubeTransform.name.IndexOf("Top") != -1)
		{
		}
		else if (cubeTransform.name.IndexOf("Bottom") != -1)
		{
		}
	}
}