using UnityEngine;

public class MomongaEnemy : EnemyBase
{
    [SerializeField] private GameObject dekatsuyoPrefab;
    
    public override void OnSliced(float angle)
    {
        var dekatsuyo = Instantiate(dekatsuyoPrefab, transform.position, transform.rotation);

        var dekatsuyoRigidbody = dekatsuyo?.GetComponent<Rigidbody2D>();
        if (!dekatsuyoRigidbody) return;

        dekatsuyoRigidbody.linearVelocity = rigidbody.linearVelocity;
        dekatsuyoRigidbody.angularVelocity = rigidbody.angularVelocity;
        
        Destroy(gameObject);
    }

    public override int GetScore()
    {
        return 0;
    }
}
