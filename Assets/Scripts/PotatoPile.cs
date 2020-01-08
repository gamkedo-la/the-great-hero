using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPile : MonoBehaviour
{
	[SerializeField]
	protected List<GameObject> potates;

    int potatoToSteal;

    void Start()
    {
        potatoToSteal = potates.Count - 1;

        foreach (var potato in potates)
        {
            potato.GetComponent<Potato>().InitializePotato();
        }
    }

    public GameObject StealPotato()
	{
		GameObject potato = null;

		if (potates.Count > 0 && potatoToSteal > 0)
		{
			potato = potates[potatoToSteal];

            potato.GetComponent<Potato>().StealPotato();

            //Debug.Log("Stealing Potato " + potato.name);
            potatoToSteal--;
        }

		return potato;
	}

    public void PotatoStolenSuccessfully(GameObject potato)
    {
        potato.GetComponent<Potato>().PotatoStolen();
        potates.Remove(potato);
    }

    public void ReturnPotato(GameObject potato)
    {
        potato.GetComponent<Potato>().ReturnToPile();
        potates.Add(potato);
        potatoToSteal++;
    }
}
 