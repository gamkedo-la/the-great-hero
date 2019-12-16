using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPile : MonoBehaviour
{
	[SerializeField]
	protected List<GameObject> potates;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public GameObject StealPotato()
	{
		GameObject potato = null;

		if (potates.Count > 0)
		{
			potato = potates[potates.Count - 1];

            Debug.Log("Stealing Potato " + potato.name);
			potates.Remove(potato);
		}

		return potato;
	}

    public void ReturnPotato(GameObject potato)
    {
        potates.Add(potato);
        potato.transform.SetParent(null);
        transform.position = startPos;
    }
}
 