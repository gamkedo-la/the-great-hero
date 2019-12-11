using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboCar : MonoBehaviour
{

	[SerializeField]
	private Transform armPivotTransform;

	[SerializeField]
	private float moveSpeed;

	private Vector3 startPosition;
	private Vector3 targetPosition;

	private bool initialized;

	public void Initialize(Vector3 targetPosition)
	{
		startPosition = transform.position;
		this.targetPosition = targetPosition;

		initialized = true;
	}


	private void Update()
	{
		if(initialized)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
		}
	}

}
