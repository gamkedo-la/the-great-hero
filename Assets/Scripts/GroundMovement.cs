using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public GameObject Movement;
    public float forwardSpeed;

    public bool ShouldMove { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //Remove this to stop movement
        ShouldMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldMove)
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        }
    }
}
