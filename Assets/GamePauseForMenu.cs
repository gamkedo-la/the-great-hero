using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseForMenu : MonoBehaviour
{
    bool timeFrozenForMenu = true;
    public GameObject titleMenuToHide;
    public GameObject TryAgain;
    public GameObject Win;
    public bool Starting;

    // Start is called before the first frame update
    void Start()
    {
        Starting = true;
        Time.timeScale = 0.0f; // freeze time
        TryAgain.SetActive(false);
        Win.SetActive(false);
    }

    void Update()
    {
        if (timeFrozenForMenu && Starting == true)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                Starting = false;
                timeFrozenForMenu = false;
                titleMenuToHide.SetActive(false);              
                Time.timeScale = 1.0f;
            }
        }
    }
}
