using UnityEngine;

public class RoboCarSpawner : Spawner
{
	private readonly float TARGET_Z_POS = 69.149f;

	[Header("References")]
	[SerializeField]
	private PotatoPile potatoPile;

    public CollisionReporter collisionReporter;

    protected override void Start()
	{
        if (collisionReporter != null)
        {
            collisionReporter.OnTriggerEvent += Collided;
        }
        base.Start();//This must run so that ComonentPool is created
		
		componentPool.InitializeObjectPool<RoboCar>(pooledPrefab, pooledCount, transform);
	}

    private void Collided(Collider collision)
    {
        ShouldStartSpawning = true;
    }


    protected override void Spawn()
	{
		RoboCar roboCar = componentPool.GetObject() as RoboCar;

		Transform spawnTransform = GetSpawnLocation();
		roboCar.transform.position = spawnTransform.position;

		Vector3 targetPosition = spawnTransform.position;
		targetPosition.z = TARGET_Z_POS;
		
		roboCar.Initialize(potatoPile, spawnTransform, targetPosition);

		roboCar.DestroyedAction += OnRoboCarDestroyed;

		roboCar.gameObject.SetActive(true);
	}

	private void OnRoboCarDestroyed(RoboCar roboCar)
	{
		roboCar.DestroyedAction -= OnRoboCarDestroyed;

		//Return the object back to the pool
		componentPool.ReturnObject(roboCar);
	}

}