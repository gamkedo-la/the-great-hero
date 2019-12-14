using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletTerminator : MonoBehaviour
{

	[SerializeField]
	private GameObject bulletDeathFXPrefab;

	private void OnCollisionEnter(Collision collision)
	{
		//Spawn animation
		if(bulletDeathFXPrefab != null)
		{
			Instantiate(bulletDeathFXPrefab, gameObject.transform.position, Quaternion.identity);
		}

		this.gameObject.SetActive(false);
	}
}
