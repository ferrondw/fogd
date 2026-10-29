using System;
using UnityEngine;
using Yakanashe.Yautl;

public class DekatsuyoEnemy : EnemyBase
{
    private bool _preSliced;

    private void Start()
    {
        var baseSize = transform.localScale;
        transform.localScale = Vector3.one;
        transform.ScaleTo(baseSize, 0.25f);
    }

    public override void OnSliced(float angle)
    {
        if (!_preSliced)
        {
            _preSliced = true;
            return;
        }

        SpawnShards(angle);
        Destroy(gameObject);
    }

    public override int GetScore()
    {
        return _preSliced ? 50 : 0;
    }
}