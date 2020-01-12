using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurretProjectiles : MonoBehaviour
{

    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public float spawnTime;
    private float spawnKey = 1;

    private GameObject effectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnKey == 1)
        {
            SpawnVFX();
        }
    }

    void SpawnVFX()
    {
        GameObject vfx;

        if(firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            spawnKey = 0;

            StartCoroutine(TimebeforeProjectile());
        }
        else
        {
            Debug.Log("No FirePoint");
        }
    }

    IEnumerator TimebeforeProjectile()
    {
        yield return new WaitForSeconds(spawnTime);
        spawnKey = 1;
    }

    void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }
}
