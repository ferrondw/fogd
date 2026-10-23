public class UsagiEnemy : EnemyBase
{
    public override void OnSliced(float angle)
    {
        SpawnShards(angle);
        Destroy(gameObject);
    }

    public override int GetScore()
    {
        return 10;
    }
}