using UnityEngine;
using Yakanashe.Yautl;

public class UsagiEnemy : EnemyBase
{
    public override void OnSliced(float angle)
    {
        base.OnSliced(angle);
        Destroy(gameObject);
    }
}