using UnityEngine;

public class SubcubeInfo : MonoBehaviour
{
	public string parentName = "Undefined";
	public string cubePosition = "Undefined";

	public Vector3 initialPosition;

	private void Awake()
	{
		cubePosition = transform.name;
		parentName = transform.parent.name;
		initialPosition = transform.position;
	}

	public void Reset()
	{
		transform.name = cubePosition;
		transform.parent = GameObject.Find(parentName + "Temp").transform;
		transform.position = initialPosition;
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}