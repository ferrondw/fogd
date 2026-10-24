using UnityEngine;

public class StarEnemy : EnemyBase
{
    [SerializeField] private GameObject spawnOnSlice;
    [SerializeField] private int amountToSpawn = 5;
    [SerializeField] private Vector2 randomVelocity = new(-2, 2);
    [SerializeField] private Vector2 randomAngularVelocity = new(-270, 270);
    [SerializeField] private Vector2 spawnOffset = new(0, 5);
    
    public override void OnSliced(float angle)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            var spawnPosition = transform.position + (Vector3)spawnOffset;
            var randomRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            var enemy = Instantiate(spawnOnSlice, spawnPosition, randomRotation);

            if (!enemy.TryGetComponent<Rigidbody2D>(out var enemyRigidbody)) continue;
            
            var randomX = Random.Range(randomVelocity.x, randomVelocity.y);
            var randomY = Random.Range(randomVelocity.x, randomVelocity.y);
                
            enemyRigidbody.velocity = new Vector2(randomX, randomY);
                
            enemyRigidbody.angularVelocity = Random.Range(randomAngularVelocity.x, randomAngularVelocity.y);
        }
        
        SpawnShards(angle);
        Destroy(gameObject);
    }

    public override int GetScore()
    {
        return 50;
    }
}
