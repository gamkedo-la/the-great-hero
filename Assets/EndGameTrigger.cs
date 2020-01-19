using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    bool timeFrozenForMenu = true;
    public GameObject titleMenuToHide;
    public GameObject TryAgain;
    public GameObject Win;

    private bool Won = false;

    // Start is called before the first frame update
    void Start()
    {
        Won = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeFrozenForMenu && Won == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            Won = true;
            Time.timeScale = 0.0f; // freeze time
            TryAgain.SetActive(false);
            Win.SetActive(false);
        }
    }
}
