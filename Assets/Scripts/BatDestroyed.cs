using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDestroyed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Potato")
        {
            Debug.Log("Potato Hit " + this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
