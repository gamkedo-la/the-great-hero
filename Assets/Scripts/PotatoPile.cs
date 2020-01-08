using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPile : MonoBehaviour
{
    [SerializeField]
    private PotatoGuage potatoGuage;

    [SerializeField]
	protected List<GameObject> potatoes;

    int potatoToSteal;

    void Start()
    {
        potatoGuage.SetPotatoGauge(potatoes.Count);

        potatoToSteal = potatoes.Count - 1;

        foreach (var potato in potatoes)
        {
            potato.GetComponent<Potato>().InitializePotato();
        }
    }

    public GameObject StealPotato()
	{
		GameObject potato = null;

		if (potatoes.Count > 0 && potatoToSteal >= 0)
		{
			potato = potatoes[potatoToSteal];

            potato.GetComponent<Potato>().StealPotato();

            //Debug.Log("Stealing Potato " + potato.name);
            potatoToSteal--;
        }

		return potato;
	}

    public void PotatoStolenSuccessfully(GameObject potato)
    {
        potato.GetComponent<Potato>().PotatoStolen();
        potatoes.Remove(potato);

        potatoGuage.SetPotatoGauge(potatoes.Count);
    }

    public void ReturnPotato(GameObject potato)
    {
        potato.GetComponent<Potato>().ReturnToPile();
        potatoToSteal++;
    }
}
 