using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromAnywhereSingleton : MonoBehaviour
{
    public static FromAnywhereSingleton instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Warning More than One Instance of Singleton Created");
        }
    }
}
