using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPile : MonoBehaviour
{
	[SerializeField]
	protected List<GameObject> potates;


	public GameObject StealPotato()
	{
		GameObject potato = null;

		if (potates.Count > 0)
		{
			potato = potates[potates.Count - 1];

			//TODO put back when potatos are more functional
			//potates.Remove(potato);
		}

		return potato;
	}
}
 