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
    private Transform clawTransform;

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
    private GameObject potato; //stealing for if needing to return

	private State carState = State.Approach;

	private Transform startPositionTransform;
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


	public void Initialize(PotatoPile potatoPile, Transform spawnLocationTransform, Vector3 targetPosition)
	{
		potatoes = potatoPile;

		hitPoints = maxHitPoints;

		startPositionTransform = spawnLocationTransform;
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
					transform.position = Vector3.MoveTowards(transform.position, startPositionTransform.position, (moveSpeed * 2f) * Time.deltaTime);
					if(Vector3.Distance(transform.position, startPositionTransform.position) <= 0.1f)
					{
						//We have escaped

						//Do something now that we have successfully stolen a potato
						potato = null;

						//Remove Robo cart from scene for performance
						//Side note, only 3 of them could successfully escape before we lose the game
						Destroyed();
					}
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
        if(potato)
        {
            potatoes.ReturnPotato(potato);
            potato = null;
        }
		StopAllCoroutines();
		initialized = false;

		DestroyedAction?.Invoke(this);
	}

	private IEnumerator StealPotatoRoutine()
	{
        //TODO: check if there potato is already used
		potato = potatoes.StealPotato();

		if (potato != null)
		{
			Vector3 dir = potato.transform.position - transform.position;
			dir = Vector3.ProjectOnPlane(dir, Vector3.up);

			armPivotTransform.forward = dir;

            while(Vector3.Distance(clawTransform.position, potato.transform.position) > 0.1f)
            {
                Vector3 stealDir = potato.transform.position - clawTransform.position;
                potato.transform.position -= stealDir.normalized * 1.5f * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            potato.transform.SetParent(transform);
			potatoes.PotatoStolenSuccessfully(potato);
		}


		carState = State.Escape;

		stealRoutine = null;
	}

}
