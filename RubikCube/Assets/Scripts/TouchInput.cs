using UnityEngine;

//这里是交互的脚本
public class TouchInput : MonoBehaviour
{
	public delegate void TouchEvent(Transform cubeTransform, Movement movement);

	public event TouchEvent OnTouchEvent;
	
	public bool Pickup { get; private set; } // 拾取到方块

	public enum Movement
	{
		Left,
		Right,
		Up,
		Down
	}

	private enum DragInputType
	{
		Mouse,
		Touch,
		Both
	}

	private const DragInputType DragType = DragInputType.Both;
	private Vector2 m_InitialPosition;
	private Vector2 m_EndPosition;
	private Transform m_CubeTransform;

	private void Update()
	{
		if (DragType == DragInputType.Both || DragType == DragInputType.Touch)
		{
			if (Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					if (touch.phase == TouchPhase.Began)
					{
						Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
						RaycastHit hit;
						if (Physics.Raycast(ray, out hit))
						{
							if (hit.collider)
							{
								//Debug.Log(hit.transform.gameObject.name);
								m_CubeTransform = hit.transform;
								m_InitialPosition = Input.mousePosition;
								Pickup = true;
							}
						}
					}

					if (touch.phase == TouchPhase.Ended)
					{
						m_EndPosition = touch.position;
						if (m_InitialPosition != new Vector2(0, 0))
						{
							if (Difference(m_InitialPosition.x, m_EndPosition.x) > Difference(m_InitialPosition.y, m_EndPosition.y))
							{
								if (m_InitialPosition.x > m_EndPosition.x)
								{
									Left();
								}
								else
								{
									Right();
								}
							}
							else if (Difference(m_InitialPosition.x, m_EndPosition.x) < Difference(m_InitialPosition.y, m_EndPosition.y))
							{
								if (m_InitialPosition.y > m_EndPosition.y)
								{
									Down();
								}
								else
								{
									Up();
								}
							}

							m_InitialPosition = new Vector2(0, 0);
							m_EndPosition = new Vector2(0, 0);
						}
						Pickup = false;
					}
				}
			}
		}

		if (DragType == DragInputType.Both || DragType == DragInputType.Mouse)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider != null)
					{
						//Debug.Log(hit.transform.gameObject.name);
						m_CubeTransform = hit.transform;
						m_InitialPosition = Input.mousePosition;
						Pickup = true;
					}
				}
			}

			if (Input.GetMouseButtonUp(0))
			{
				if (m_InitialPosition != new Vector2(0, 0))
				{
					m_EndPosition = Input.mousePosition;
					if (Difference(m_InitialPosition.x, m_EndPosition.x) > Difference(m_InitialPosition.y, m_EndPosition.y))
					{
						if (m_InitialPosition.x > m_EndPosition.x)
						{
							Left();
						}
						else
						{
							Right();
						}
					}
					else
					{
						if (m_InitialPosition.y > m_EndPosition.y)
						{
							Down();
						}
						else
						{
							Up();
						}
					}

					m_InitialPosition = new Vector2(0, 0);
					m_EndPosition = new Vector2(0, 0);
				}
				Pickup = false;
			}
		}
	}

	private float Difference(float first, float second)
	{
		return Mathf.Max(first, second) - Mathf.Min(first, second);
	}

	private void Left()
	{
		OnTouchEvent?.Invoke(m_CubeTransform, Movement.Left);
	}

	private void Right()
	{
		OnTouchEvent?.Invoke(m_CubeTransform, Movement.Right);
	}

	private void Up()
	{
		OnTouchEvent?.Invoke(m_CubeTransform, Movement.Up);
	}

	private void Down()
	{
		OnTouchEvent?.Invoke(m_CubeTransform, Movement.Down);
	}
}