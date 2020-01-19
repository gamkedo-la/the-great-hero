using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    Animator GateAnimator;
    // Start is called before the first frame update
    void Start()
    {
        GateAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other)
        {
            GateAnimator.SetTrigger("Opened");
        }
    }
}
