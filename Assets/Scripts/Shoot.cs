using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet;
    public GameObject bulletSpawn;
    public Vector3 bulletSpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnLocation = bulletSpawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bulletSpawnLocation = bulletSpawn.transform.position;


        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, bulletSpawnLocation, transform.rotation);
        }
    }
}
