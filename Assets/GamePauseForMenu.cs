using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseForMenu : MonoBehaviour
{
    bool timeFrozenForMenu = true;
    public GameObject titleMenuToHide;
    public GameObject TryAgain;
    public GameObject Win;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f; // freeze time
        TryAgain.SetActive(false);
        Win.SetActive(false);
    }

    void Update()
    {
        if(timeFrozenForMenu)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                timeFrozenForMenu = false;
                titleMenuToHide.SetActive(false);              
                Time.timeScale = 1.0f;
            }
        }
    }
}
