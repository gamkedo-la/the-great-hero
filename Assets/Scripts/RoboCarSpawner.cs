using UnityEngine;

public class RoboCarSpawner : Spawner
{
	private readonly float TARGET_Z_POS = 69.149f;

	[Header("References")]
	[SerializeField]
	private PotatoPile potatoPile;


	protected override void Start()
	{
		base.Start();

		componentPool.InitializeObjectPool<RoboCar>(pooledPrefab, pooledCount, transform);
	}


	protected override void Spawn()
	{
		//Component component = componentPool.GetObject() as RoboCar;
		RoboCar roboCar = componentPool.GetObject() as RoboCar;

		//RoboCar roboCar = component.GetComponent<RoboCar>();

		Vector3 spawnLocation = GetSpawnLocation();
		roboCar.transform.position = spawnLocation;

		spawnLocation.z = TARGET_Z_POS;
		roboCar.Initialize(potatoPile, spawnLocation);

		roboCar.DestroyedAction += OnRoboCarDestroyed;

		roboCar.gameObject.SetActive(true);
	}

	private void OnRoboCarDestroyed(RoboCar roboCar)
	{
		roboCar.DestroyedAction -= OnRoboCarDestroyed;

		componentPool.ReturnObject(roboCar);
	}

}
