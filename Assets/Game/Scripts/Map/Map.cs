using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	[SerializeField] private Transform vector1;
	[SerializeField] private Transform vector2;
	[SerializeField] private Transform vector3;
	[SerializeField] private Transform vector4;

	public bool CheckPositonSpawn(Vector3 transformCheck)
	{
		if (transformCheck.z > vector1.position.z &&
			transformCheck.x < vector2.position.x &&
			transformCheck.z < vector3.position.z &&
			transformCheck.x > vector4.position.x)
		{
			return true;
		}
		return false;
	}
}

