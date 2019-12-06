using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    private Rigidbody rb;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
