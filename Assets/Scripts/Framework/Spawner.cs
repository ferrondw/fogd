using Random = UnityEngine.Random;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private float timeBetweenSpawns;
    
    [SerializeField] private Vector2 minMaxVerticalSpawnVelocity;
    [SerializeField] private Vector2 minMaxHorizontalSpawnVelocity;

    private float _lastSpawnTime;

    private void Update()
    {
        if (!(Time.time - _lastSpawnTime > timeBetweenSpawns)) return;

        SpawnRandomEnemy();
        _lastSpawnTime = Time.time; // reset timer
    }

    private void SpawnRandomEnemy()
    {
        var enemyInstance = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)]);
        var enemyRigidbody = enemyInstance.GetComponent<Rigidbody2D>(); // need to TryGet but for now it's fine
        
        var horizontalSpawnOffset = Random.Range(-camera.orthographicSize, camera.orthographicSize);
        
        // always make left spawning enemies move to the right, and right spawning enemies move to the left
        var horizontalSpawnVelocity = Random.Range(minMaxHorizontalSpawnVelocity.x, minMaxHorizontalSpawnVelocity.y) * (horizontalSpawnOffset > 0 ? -1 : 1);
        var verticalSpawnVelocity = Random.Range(minMaxVerticalSpawnVelocity.x, minMaxVerticalSpawnVelocity.y);

        var spawnPoint = new Vector3(camera.transform.position.x + horizontalSpawnOffset, camera.transform.position.y - camera.orthographicSize);
        enemyInstance.transform.position = spawnPoint;
        enemyRigidbody.velocity = new Vector2(horizontalSpawnVelocity, verticalSpawnVelocity);
        enemyRigidbody.angularVelocity = Random.Range(50, 360) * (Random.value > 0.5f ? -1 : 1);
    }
}