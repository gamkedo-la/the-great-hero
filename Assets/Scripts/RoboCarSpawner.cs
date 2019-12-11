using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboCarSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject roboCarPrefab;
	[SerializeField]
	private Transform spawnLocation;

	[SerializeField]
	[Tooltip("Spawned every x serconds")]
	private float spawnRate;
	

	[SerializeField]
	private Transform potatoHolderTransform;

	private float timer = 0f;


	private void Update()
	{
		timer += Time.deltaTime;

		if(timer > spawnRate)
		{
			//Spawn Robo Car

			timer = timer - spawnRate;
		}
	}

}
