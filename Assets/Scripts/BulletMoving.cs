using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    public GameObject bullet;
    public Rigidbody rb;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bullet = this.gameObject;
        rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
