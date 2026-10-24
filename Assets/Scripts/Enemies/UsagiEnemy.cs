using UnityEngine;

public class UsagiEnemy : EnemyBase
{
    [SerializeField] private int givenScore = 10;
    
    public override void OnSliced(float angle)
    {
        SpawnShards(angle);
        Destroy(gameObject);
    }

    public override int GetScore()
    {
        return givenScore;
    }
}