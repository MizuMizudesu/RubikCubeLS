using System.Collections;
using Mgr;
using UnityEngine;

public class RubikCube : MonoBehaviour
{
	public enum Side
	{
		LeftColumn,
		CenterColumn,
		RightColumn,
		TopRow,
		CenterRow,
		BottomRow
	}

	public int rotationSpeed_default = 3;
	// 它应该是90的素数。如果它除以90，那么它应该是整数。

	private GameObject rubicCube_gameobject;
	private GameManager gameController;
	private float rotationSpeed;
	private bool allowMovement = true;

	private void Start()
	{
		rotationSpeed = rotationSpeed_default;
		rubicCube_gameobject = gameObject;
		gameController = FindObjectOfType<GameManager>();
	}

	public void StartGame()
	{
		ResetCube();
		StartCoroutine(RandomMove());
	}

	#region Reset subcubes to their original position

	public void ResetCube()
	{
		//重置魔方
		GameObject rear_gameobject = new GameObject("RearTemp");
		GameObject center_gameobject = new GameObject("FrontTemp");
		GameObject front_gameobject = new GameObject("CenterTemp");

		for (int i = 1; i <= 9; i++)
		{
			rubicCube_gameobject.transform.Find("Rear").Find(i.ToString()).GetComponent<SubcubeInfo>().Reset();
			rubicCube_gameobject.transform.Find("Center").Find(i.ToString()).GetComponent<SubcubeInfo>().Reset();
			rubicCube_gameobject.transform.Find("Front").Find(i.ToString()).GetComponent<SubcubeInfo>().Reset();
		}

		for (int j = 1; j <= 9; j++)
		{
			rear_gameobject.transform.Find(j.ToString()).parent = rubicCube_gameobject.transform.Find("Rear");
			center_gameobject.transform.Find(j.ToString()).parent = rubicCube_gameobject.transform.Find("Front");
			front_gameobject.transform.Find(j.ToString()).parent = rubicCube_gameobject.transform.Find("Center");
		}

		Destroy(rear_gameobject);
		Destroy(center_gameobject);
		Destroy(front_gameobject);
	}

	#endregion

	#region Slide Rubic Cube Methods

	// All player action on the rubic cube is performed from these region
	// These methods execute Coroutine in order to move rotation smooth and visible for player
	// Naming of these method is Slide + (slide movement) + which column or row. 
	// For example if you want to slide right center row then method should be SlideRightCenter
	// For example if you want to slide up right column then method should be SlideUpRight
	// These method is perform from only one perspective (rear view of Cube). Like player is standing on the back side of the cube 
	public void SlideUpLeft()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.LeftColumn, new Vector3(90, 0, 0), false));
	}

	public void SlideDownLeft()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.LeftColumn, new Vector3(-90, 0, 0), true));
	}

	public void SlideUpRight()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.RightColumn, new Vector3(90, 0, 0), false));
	}

	public void SlideDownRight()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.RightColumn, new Vector3(-90, 0, 0), true));
	}

	public void SlideUpCenter()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.CenterColumn, new Vector3(90, 0, 0), false));
	}

	public void SlideDownCenter()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.CenterColumn, new Vector3(-90, 0, 0), true));
	}

	public void SlideLeftTop()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.TopRow, new Vector3(0, 90, 0), false));
	}

	public void SlideRightTop()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.TopRow, new Vector3(0, -90, 0), true));
	}

	public void SlideLeftCenter()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.CenterRow, new Vector3(0, 90, 0), false));
	}

	public void SlideRightCenter()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.CenterRow, new Vector3(0, -90, 0), true));
	}

	public void SlideLeftBottom()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.BottomRow, new Vector3(0, 90, 0), false));
	}

	public void SlideRightBottom()
	{
		if (allowMovement)
			StartCoroutine(RotateSide(Side.BottomRow, new Vector3(0, -90, 0), true));
	}

	public void SlideLeftWholeRear()
	{
		if (allowMovement)
			StartCoroutine(RotateWholeSide(transform.Find("Rear"), true));
	}

	public void SlideRightWholeRear()
	{
		if (allowMovement)
			StartCoroutine(RotateWholeSide(transform.Find("Rear"), false));
	}

	public void SlideLeftWholeCenter()
	{
		if (allowMovement)
			StartCoroutine(RotateWholeSide(transform.Find("Center"), true));
	}

	public void SlideRightWholeCenter()
	{
		if (allowMovement)
			StartCoroutine(RotateWholeSide(transform.Find("Center"), false));
	}

	public void SlideLeftWholeFront()
	{
		if (allowMovement)
		{
			StartCoroutine(RotateWholeSide(transform.Find("Front"), true));
		}
	}

	public void SlideRightWholeFront()
	{
		if (allowMovement)
			StartCoroutine(RotateWholeSide(transform.Find("Front"), false));
	}

	#endregion

	#region Completed Puzzle Method

	// This method is check if the game is completed or not.
	// It execute after every action made on the rubic cube.
	// It checks very simple way to compare current information of subcube with initial subcube's information. 
	// Every sub-cube of rubic cube has script called "SubcudeInfo" which has the details of the cube's basic information
	private void IsCompleted()
	{
		Transform[] checkTransforms = new Transform[3];
		checkTransforms[0] = rubicCube_gameobject.transform.Find("Rear");
		checkTransforms[1] = rubicCube_gameobject.transform.Find("Center");
		checkTransforms[2] = rubicCube_gameobject.transform.Find("Front");

		bool completed = false;
		bool condition1, condition2;

		for (int j = 0; j < checkTransforms.Length; j++)
		{
			for (int i = 1; i <= 9; i++)
			{
				condition1 = checkTransforms[j].Find(i.ToString()).GetComponent<SubcubeInfo>().parentName ==
				             checkTransforms[j].name;
				condition2 = checkTransforms[j].Find(i.ToString()).GetComponent<SubcubeInfo>().cubePosition ==
				             checkTransforms[j].Find(i.ToString()).name;
				if (condition1 && condition2)
				{
					completed = true;
				}
				else
				{
					return;
				}
			}
		}

		if (completed && gameController.CurGameState == GameState.Playing)
		{
			gameController.OnGameCompleted();
		}
	}

	#endregion

	#region Random Rotation For Start Game

	public IEnumerator RandomMove()
	{
		yield return StartCoroutine(RotationRandom(3, 5));
		yield return StartCoroutine(RotationRandom(5, 6));
		yield return StartCoroutine(RotationRandom(7, 7));
		yield return StartCoroutine(RotationRandom(9, 8));
		yield return StartCoroutine(RotationRandom(11, 9));
		
		rotationSpeed = rotationSpeed_default;
		int savedCubeSpeed = PlayerPrefs.GetInt("CubeSpeed");
		PlayerPrefs.SetInt("CubeSpeed", savedCubeSpeed);
		allowMovement = true;
		gameController.OnStartGame();
		
		yield break;
		
		if (PlayerPrefs.GetInt("Testing") == 0)
		{
			if (PlayerPrefs.GetInt("Difficulty") == 3)
			{
				StartCoroutine(RotationRandom(100, 7));
			}
			else if (PlayerPrefs.GetInt("Difficulty") == 2)
			{
				StartCoroutine(RotationRandom(50, 8));
			}
			else
			{
				StartCoroutine(RotationRandom(25, 9));
			}
		}
		else
		{
			StartCoroutine(RotationRandom(2, 10));
		}
	}

	private IEnumerator RotationRandom(int numberOfRotation, int speed)
	{
		rotationSpeed = speed;
		PlayerPrefs.SetInt("CubeSpeed", 4);
		for (int i = 0; i < numberOfRotation; i++)
		{
			if (gameController.CurGameState == GameState.Idle)
			{
				break;
			}

			int ran = Random.Range(1, 12);
			if (ran == 1)
				yield return RotateSide(Side.LeftColumn, new Vector3(90, 0, 0), false);
			else if (ran == 2)
				yield return RotateSide(Side.LeftColumn, new Vector3(-90, 0, 0), true);
			else if (ran == 3)
				yield return RotateSide(Side.RightColumn, new Vector3(90, 0, 0), false);
			else if (ran == 4)
				yield return RotateSide(Side.RightColumn, new Vector3(-90, 0, 0), true);
			else if (ran == 5)
				yield return RotateSide(Side.CenterColumn, new Vector3(90, 0, 0), false);
			else if (ran == 6)
				yield return RotateSide(Side.CenterColumn, new Vector3(-90, 0, 0), true);
			else if (ran == 7)
				yield return RotateSide(Side.TopRow, new Vector3(0, 90, 0), false);
			else if (ran == 8)
				yield return RotateSide(Side.TopRow, new Vector3(0, -90, 0), true);
			else if (ran == 9)
				yield return RotateSide(Side.CenterRow, new Vector3(0, 90, 0), false);
			else if (ran == 10)
				yield return RotateSide(Side.CenterRow, new Vector3(0, -90, 0), true);
			else if (ran == 11)
				yield return RotateSide(Side.BottomRow, new Vector3(0, 90, 0), false);
			else if (ran == 12)
				yield return RotateSide(Side.BottomRow, new Vector3(0, -90, 0), true);
			else if (ran == 13)
				yield return RotateWholeSide(transform.Find("Rear"), true);
			else if (ran == 14)
				yield return RotateWholeSide(transform.Find("Rear"), false);
			else if (ran == 15)
				yield return RotateWholeSide(transform.Find("Center"), true);
			else if (ran == 16)
				yield return RotateWholeSide(transform.Find("Center"), false);
			else if (ran == 17)
				yield return RotateWholeSide(transform.Find("Front"), true);
			else if (ran == 18)
				yield return RotateWholeSide(transform.Find("Front"), false);
		}
	}

	#endregion

	#region Rotation

	private void GetRotationSpeed()
	{
		if (PlayerPrefs.GetInt("CubeSpeed") == 3)
		{
			rotationSpeed = 10f;
		}
		else if (PlayerPrefs.GetInt("CubeSpeed") == 2)
		{
			rotationSpeed = 5f;
		}
		else if (PlayerPrefs.GetInt("CubeSpeed") == 1)
		{
			rotationSpeed = rotationSpeed_default; // 3
		}
	}

	private IEnumerator RotateWholeSide(Transform selectedAxix, bool clockwise)
	{
		allowMovement = false;
		GetRotationSpeed();
		
		Quaternion targetRotation = selectedAxix.localRotation;
		targetRotation *= clockwise ? Quaternion.Euler(0, 0, 90) : Quaternion.Euler(0, 0, -90);
		
		while (true)
		{
			selectedAxix.transform.localRotation = Quaternion.Slerp(
				selectedAxix.transform.localRotation,
				targetRotation,
				rotationSpeed * Time.deltaTime * 3
			);
			
			if (Quaternion.Angle(selectedAxix.transform.localRotation, targetRotation) < 0.1f)
			{
				selectedAxix.transform.localRotation = targetRotation;
				break;
			}

			yield return null;
		}
		
		/*for (int i = 0; i < 90 / rotationSpeed; i++)
		{
			yield return new WaitForSeconds(0.001f);
			if (clockwise)
			{
				selectedAxix.Rotate(new Vector3(0, 0, rotationSpeed));
			}
			else
			{
				selectedAxix.Rotate(new Vector3(0, 0, -rotationSpeed));
			}
		}*/

		ChangeNamingOfSide(selectedAxix, clockwise);
		IsCompleted();
		allowMovement = true;
	}

	private IEnumerator RotateSide(Side side, Vector3 rotation, bool clockwise)
	{
		// 这里是确保旋转基于 RubikCube 的本地坐标系
		GameObject tempGameObject = new GameObject("RotateGameObject");
		tempGameObject.transform.SetParent(rubicCube_gameobject.transform); // 绑定到 RubikCube
		tempGameObject.transform.localPosition = Vector3.zero;
		tempGameObject.transform.localRotation = Quaternion.identity;
		
		allowMovement = false;
		GetRotationSpeed();
		GameObject topGameObject =
			MoveToNewGameObject(rubicCube_gameobject.transform.Find("Rear"), side, tempGameObject.transform);
		GameObject centerGameObject =
			MoveToNewGameObject(rubicCube_gameobject.transform.Find("Center"), side, tempGameObject.transform);
		GameObject bottomGameObject =
			MoveToNewGameObject(rubicCube_gameobject.transform.Find("Front"), side, tempGameObject.transform);

		Quaternion targetRotation = tempGameObject.transform.localRotation;
		if (side.ToString().IndexOf("Row") == -1)
		{
			targetRotation *= clockwise ? Quaternion.Euler(-90, 0, 0) : Quaternion.Euler(90, 0, 0);
		}
		else
		{
			targetRotation *= clockwise ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);
		}

		while (true)
		{
			tempGameObject.transform.localRotation = Quaternion.Slerp(
				tempGameObject.transform.localRotation,
				targetRotation,
				rotationSpeed * Time.deltaTime * 3
			);
			
			if (Quaternion.Angle(tempGameObject.transform.localRotation, targetRotation) < 0.1f)
			{
				tempGameObject.transform.localRotation = targetRotation;
				break;
			}

			yield return null;
		}
		
		/*
		for (int i = 0; i < 90 / rotationSpeed; i++)
		{
			yield return null;
			if (side.ToString().IndexOf("Row") == -1)
			{
				float x = clockwise ? -rotationSpeed : rotationSpeed;
				tempGameObject.transform.Rotate(new Vector3(x, 0, 0));
			}
			else
			{
				float y = clockwise ? -rotationSpeed : rotationSpeed;
				tempGameObject.transform.Rotate(new Vector3(0, y, 0));
			}
		}*/

		if (clockwise)
		{
			MoveGameObjectToRubic(gameObject, topGameObject, side, 3, clockwise);
			MoveGameObjectToRubic(gameObject, centerGameObject, side, 2, clockwise);
			MoveGameObjectToRubic(gameObject, bottomGameObject, side, 1, clockwise);
		}
		else
		{
			MoveGameObjectToRubic(gameObject, topGameObject, side, 1, clockwise);
			MoveGameObjectToRubic(gameObject, centerGameObject, side, 2, clockwise);
			MoveGameObjectToRubic(gameObject, bottomGameObject, side, 3, clockwise);
		}

		Destroy(tempGameObject);
		IsCompleted();
		allowMovement = true;
	}

	private GameObject MoveToNewGameObject(Transform selectedZAxix, Side side, Transform parent)
	{
		GameObject tempGameObject = new GameObject(side.ToString());
		tempGameObject.transform.parent = parent;

		switch (side)
		{
			case Side.LeftColumn:
				selectedZAxix.Find("1").parent = tempGameObject.transform;
				selectedZAxix.Find("4").parent = tempGameObject.transform;
				selectedZAxix.Find("7").parent = tempGameObject.transform;
				break;
			case Side.CenterColumn:
				selectedZAxix.Find("2").parent = tempGameObject.transform;
				selectedZAxix.Find("5").parent = tempGameObject.transform;
				selectedZAxix.Find("8").parent = tempGameObject.transform;
				break;
			case Side.RightColumn:
				selectedZAxix.Find("3").parent = tempGameObject.transform;
				selectedZAxix.Find("6").parent = tempGameObject.transform;
				selectedZAxix.Find("9").parent = tempGameObject.transform;
				break;
			case Side.TopRow:
				selectedZAxix.Find("1").parent = tempGameObject.transform;
				selectedZAxix.Find("2").parent = tempGameObject.transform;
				selectedZAxix.Find("3").parent = tempGameObject.transform;
				break;
			case Side.CenterRow:
				selectedZAxix.Find("4").parent = tempGameObject.transform;
				selectedZAxix.Find("5").parent = tempGameObject.transform;
				selectedZAxix.Find("6").parent = tempGameObject.transform;
				break;
			case Side.BottomRow:
				selectedZAxix.Find("7").parent = tempGameObject.transform;
				selectedZAxix.Find("8").parent = tempGameObject.transform;
				selectedZAxix.Find("9").parent = tempGameObject.transform;
				break;
		}

		return tempGameObject;
	}

	private void MoveGameObjectToRubic(GameObject rubicCube, GameObject movedGameObject, Side side, int movedPosition,
		bool clockwise)
	{
		Transform firstTransform = null, secondTransform = null, thirdTransform = null;

		switch (side)
		{
			case Side.LeftColumn:
				firstTransform = movedGameObject.transform.Find("1");
				secondTransform = movedGameObject.transform.Find("4");
				thirdTransform = movedGameObject.transform.Find("7");
				break;
			case Side.CenterColumn:
				firstTransform = movedGameObject.transform.Find("2");
				secondTransform = movedGameObject.transform.Find("5");
				thirdTransform = movedGameObject.transform.Find("8");
				break;
			case Side.RightColumn:
				firstTransform = movedGameObject.transform.Find("3");
				secondTransform = movedGameObject.transform.Find("6");
				thirdTransform = movedGameObject.transform.Find("9");
				break;
			case Side.TopRow:
				firstTransform = movedGameObject.transform.Find("1");
				secondTransform = movedGameObject.transform.Find("2");
				thirdTransform = movedGameObject.transform.Find("3");
				break;
			case Side.CenterRow:
				firstTransform = movedGameObject.transform.Find("4");
				secondTransform = movedGameObject.transform.Find("5");
				thirdTransform = movedGameObject.transform.Find("6");
				break;
			case Side.BottomRow:
				firstTransform = movedGameObject.transform.Find("7");
				secondTransform = movedGameObject.transform.Find("8");
				thirdTransform = movedGameObject.transform.Find("9");
				break;
		}

		if (movedPosition == 1)
		{
			firstTransform.name = firstTransform.name;
			secondTransform.name = firstTransform.name;
			thirdTransform.name = firstTransform.name;
		}
		else if (movedPosition == 2)
		{
			firstTransform.name = secondTransform.name;
			secondTransform.name = secondTransform.name;
			thirdTransform.name = secondTransform.name;
		}
		else if (movedPosition == 3)
		{
			firstTransform.name = thirdTransform.name;
			secondTransform.name = thirdTransform.name;
			thirdTransform.name = thirdTransform.name;
		}

		if (clockwise)
		{
			firstTransform.parent = rubicCube.transform.Find("Rear");
			secondTransform.parent = rubicCube.transform.Find("Center");
			thirdTransform.parent = rubicCube.transform.Find("Front");
		}
		else
		{
			firstTransform.parent = rubicCube.transform.Find("Front");
			secondTransform.parent = rubicCube.transform.Find("Center");
			thirdTransform.parent = rubicCube.transform.Find("Rear");
		}
	}

	private void ChangeNamingOfSide(Transform selectedSideTransform, bool clockwise)
	{
		string[] numbers = new string[9];
		if (clockwise)
		{
			numbers[0] = "7";
			numbers[1] = "4";
			numbers[2] = "1";
			numbers[3] = "8";
			numbers[4] = "5";
			numbers[5] = "2";
			numbers[6] = "9";
			numbers[7] = "6";
			numbers[8] = "3";
		}
		else
		{
			numbers[0] = "3";
			numbers[1] = "6";
			numbers[2] = "9";
			numbers[3] = "2";
			numbers[4] = "5";
			numbers[5] = "8";
			numbers[6] = "1";
			numbers[7] = "4";
			numbers[8] = "7";
		}

		if (numbers.Length != 9)
		{
			Debug.LogError("Each sides has nine cubes so number string length should be nine.");
		}

		for (int i = 1; i <= 9; i++)
		{
			selectedSideTransform.Find(i.ToString()).name = numbers[i - 1] + ".";
		}

		for (int i = 1; i <= 9; i++)
		{
			selectedSideTransform.Find(i + ".").name = i.ToString();
		}
	}

	#endregion
}