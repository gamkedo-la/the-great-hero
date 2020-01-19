using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotatoPile : MonoBehaviour
{
    [SerializeField]
    private PotatoGuage potatoGuage;

    [SerializeField]
	protected List<GameObject> potatoes;

    bool timeFrozenForMenu = true;
    public GameObject TryAgain;
    public bool Lost = false;
    int potatoToSteal;

    void Start()
    {
        Lost = false;

        potatoGuage.SetPotatoGauge(potatoes.Count);

        potatoToSteal = potatoes.Count - 1;

        foreach (var potato in potatoes)
        {
            potato.GetComponent<Potato>().InitializePotato();
        }
    }

    void Update()
    {
        if(potatoes.Count == 0)
        {
            Lost = true;
            Time.timeScale = 0.0f;
            TryAgain.SetActive(true);
            if (timeFrozenForMenu && Lost == true)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
                {
                    SceneManager.LoadScene("MainScene");
                }
            }
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
 