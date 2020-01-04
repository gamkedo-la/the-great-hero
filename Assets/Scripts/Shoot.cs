using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public OVRInput.Button whichButton;

    //Bullet
    public GameObject bullet;
    public GameObject bulletSpawn;
    public Vector3 bulletSpawnLocation;

    //Audio
    public AudioClip shootCannonSFX;
    public AudioSource cannonAudioSource;

    //Particle FX
    public ParticleSystem muzzleFlashParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnLocation = bulletSpawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bulletSpawnLocation = bulletSpawn.transform.position;

        if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(whichButton))
        {
            
            GameObject shot = Instantiate(bullet, bulletSpawnLocation, transform.rotation);
            shot.transform.SetParent(FromAnywhereSingleton.instance.transform);

            MutateSFXPitch();
            cannonAudioSource.PlayOneShot(shootCannonSFX);

            muzzleFlashParticleSystem.Play();
        }
    }

    private void MutateSFXPitch()
    {
        cannonAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
    }

}
