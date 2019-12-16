using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboCar : MonoBehaviour
{
	enum State
	{
		Approach,
		Steal,
		Escape
	}


	[SerializeField]
	private Transform armPivotTransform;

	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float stealTime;

	[SerializeField]
	private int hitPoints;
	[SerializeField]
	private List<CollisionReporter> collisionReporters;

	public Action<RoboCar> DestroyedAction;

	private PotatoPile potatoes;
	private Coroutine stealRoutine;

	private State carState = State.Approach;

	private Vector3 startPosition;
	private Vector3 targetPosition;

	private bool initialized;
	private bool takenDamageThisFrame;
	private int maxHitPoints;

	private void Awake()
	{
		foreach (var reporter in collisionReporters)
		{
			reporter.OnCollitionEvent += OnCollisionHandler;
		}

		maxHitPoints = hitPoints;
	}


	public void Initialize(PotatoPile potatoPile, Vector3 targetPosition)
	{
		potatoes = potatoPile;

		hitPoints = maxHitPoints;

		startPosition = transform.position;
		this.targetPosition = targetPosition;

		stealRoutine = null;

		initialized = true;
	}


	private void Update()
	{
		if (initialized)
		{
			switch (carState)
			{
				case State.Approach:
					transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

					if (Vector3.Distance(transform.position, targetPosition) == 0)
					{
						carState = State.Steal;
					}
					break;

				case State.Steal:
					if (stealRoutine == null)
					{
						stealRoutine = StartCoroutine(StealPotatoRoutine());
					}
					break;

				case State.Escape:
					transform.position = Vector3.MoveTowards(transform.position, startPosition, (moveSpeed * 2f) * Time.deltaTime);
					break;
			}


			takenDamageThisFrame = false;
		}
	}


	private void OnCollisionHandler(Collision collision)
	{
		if (initialized)
		{
			TakeDamage();
		}
	}

	private void TakeDamage()
	{
		if (takenDamageThisFrame == false)
		{
			takenDamageThisFrame = true;

			hitPoints--;

			if (hitPoints <= 0)
			{
				Destroyed();
			}

			//Play on hit animatino/vfx
		}
	}

	private void Destroyed()
	{
		//Play death animation

		StopAllCoroutines();
		initialized = false;

		DestroyedAction?.Invoke(this);
	}

	private IEnumerator StealPotatoRoutine()
	{
		GameObject potato = potatoes.StealPotato();

		if (potato != null)
		{
			Vector3 dir = potato.transform.position - transform.position;
			dir = Vector3.ProjectOnPlane(dir, Vector3.up);

			armPivotTransform.forward = dir;

            potato.transform.SetParent(transform);

		}

		yield return new WaitForSeconds(stealTime);

		carState = State.Escape;

		stealRoutine = null;
	}

}
