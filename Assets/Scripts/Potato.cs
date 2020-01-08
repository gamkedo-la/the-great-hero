using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour
{
	public enum PotatoState
	{
		Safe,
		BeingTaken,
		ReturningToPile,
		Stolen,
	}

	public PotatoState State { get; protected set; }

	private Coroutine returnToPileCoroutine = null;
	private Vector3 positionOnPile;

	public void InitializePotato()
	{
		//Initial state of potato
		State = PotatoState.Safe;

		//Starting position on Potato Pile
		positionOnPile = transform.position;
	}

	public void StealPotato()
	{
		if (State == PotatoState.Safe || State == PotatoState.ReturningToPile)
		{
			State = PotatoState.BeingTaken;

			if (returnToPileCoroutine != null)
			{
				StopCoroutine(returnToPileCoroutine);
			}
		}
	}

	public void ReturnToPile()
	{
		if (State == PotatoState.BeingTaken)
		{
			State = PotatoState.ReturningToPile;

			//Return the potate back to the pile      
			returnToPileCoroutine = StartCoroutine(ReturnToPileRoutine());
		}
	}

	public void PotatoStolen()
	{
		if (State == PotatoState.BeingTaken)
		{
			State = PotatoState.Stolen;
		}
	}

	private IEnumerator ReturnToPileRoutine()
	{
		while(Vector3.Distance(transform.position, positionOnPile) >= 0.1f)
		{
			//Move back to pile
			transform.position = Vector3.MoveTowards(transform.position, positionOnPile, 0.1f);

			yield return null;
		}

		State = PotatoState.Safe;

		returnToPileCoroutine = null;
	}
}
