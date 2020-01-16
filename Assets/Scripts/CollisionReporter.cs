using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionReporter : MonoBehaviour
{
	public delegate void OnCollision(Collision collision);
	public event OnCollision OnCollitionEvent;

	public delegate void OnTrigger(Collider collision);
	public event OnTrigger OnTriggerEvent;

	private void OnCollisionEnter(Collision collision)
	{
		OnCollitionEvent?.Invoke(collision);
	}

	private void OnTriggerEnter(Collider other)
	{
		OnTriggerEvent?.Invoke(other);
	}
}
