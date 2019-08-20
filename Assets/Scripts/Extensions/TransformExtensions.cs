using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
	public static Transform GetClosestObject(this Transform transform, ref List<GameObject> scaryObjects)
	{
		if (scaryObjects.Count <= 0)
		{
			return null;
		}

		Transform closestObject = null;
		float currentClosestDistance = float.MaxValue; //or Mathf.Infinity to get the max value

		foreach (GameObject iteam in scaryObjects)
		{
			float distance = Vector2.Distance(transform.position, iteam.transform.position);
			if (distance < currentClosestDistance)
			{
				currentClosestDistance = distance;
				closestObject = iteam.transform;
			}
		}

		return closestObject;
	}
}

