using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    public float batSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(FromAnywhereSingleton.instance.transform.position);

        transform.position += transform.forward * Time.deltaTime * batSpeed;
        //FromAnywhereSingleton.instance.transform.position 
    }
}
