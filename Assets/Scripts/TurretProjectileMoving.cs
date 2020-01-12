using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileMoving : MonoBehaviour
{
    private Rigidbody rb;
    public float turretProjectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * turretProjectileSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
