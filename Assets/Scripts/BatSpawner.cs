using System;
using UnityEngine;

public class BatSpawner : Spawner
{
	private readonly float TARGET_Z_POS = 69.149f;

	[Header("References")]
	[SerializeField]
	private PotatoPile potatoPile;

	public CollisionReporter collisionReporter;

	protected override void Start()
	{
		if(collisionReporter != null)
		{
			collisionReporter.OnTriggerEvent += Collided;
		}

		//Create component pool
		base.Start();//This must run so that ComonentPool is created

		componentPool.InitializeObjectPool<BatAI>(pooledPrefab, pooledCount, transform);
	}

	private void Collided(Collider collision)
	{
		ShouldStartSpawning = true;
	}

	protected override void Spawn()
	{
		BatAI batAI = componentPool.GetObject() as BatAI;

		//Get spawn location
		Transform spawnTransform = GetSpawnLocation();
		//Set spawn location to new bat
		batAI.transform.position = spawnTransform.position;

		batAI.Initialize(potatoPile);

		batAI.DestroyedAction += OnBatDestroyed;

		batAI.gameObject.SetActive(true);
	}

	private void OnBatDestroyed(BatAI bat)
	{
		//If we miss this, it will stack up
		bat.DestroyedAction -= OnBatDestroyed;

		//Return the object to the pool
		componentPool.ReturnObject(bat);
	}
}
