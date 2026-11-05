using System;
using UnityEngine;
using Yakanashe.Yautl;

public class BeerEnemy : EnemyBase
{
    [SerializeField] private float releaseBeerTimer = 1;
    [SerializeField] private SpriteRenderer beerWarning;

    private Vector2 _cachedLinearVelocity;
    private float _cachedAngularVelocity;
    
    private void Start()
    {
        Invoke(nameof(ReleaseBeer), releaseBeerTimer);

        _cachedLinearVelocity = rigidbody.linearVelocity;
        _cachedAngularVelocity = rigidbody.angularVelocity;

        rigidbody.bodyType = RigidbodyType2D.Static;
        beerWarning.ColorTo(Color.clear, 0.3f).SetLoops(-1, true);
    }

    private void ReleaseBeer()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        TweenRunner.Instance.KillAllFrom(beerWarning.transform);
        beerWarning.color = Color.clear;

        rigidbody.linearVelocity = _cachedLinearVelocity;
        rigidbody.angularVelocity = _cachedAngularVelocity;
    }

    public override void OnSliced(float angle)
    {
        SpawnShards(angle);
        Destroy(gameObject);
    }
    
    public override int GetScore()
    {
        return 0;
    }
}
