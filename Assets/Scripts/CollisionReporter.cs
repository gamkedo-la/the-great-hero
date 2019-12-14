using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionReporter : MonoBehaviour
{
	public delegate void OnCollision(Collision collision);
	public event OnCollision OnCollitionEvent;

	private void OnCollisionEnter(Collision collision)
	{
		OnCollitionEvent?.Invoke(collision);
	}
}
