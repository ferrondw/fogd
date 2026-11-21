using System;
using UnityEngine;
using Yakanashe.Yautl;

public class HugesagiEnemy : EnemyBase
{
    [SerializeField] private int amountToSlice = 5;
    
    private int _slicedAmount;

    private void Start()
    {
        var baseSize = transform.localScale;
        transform.localScale = Vector3.one;
        transform.ScaleTo(baseSize, 0.25f);
    }

    public override void OnSliced(float angle)
    {
        if (_slicedAmount < amountToSlice)
        {
            _slicedAmount++;
            return;
        }

        SpawnShards(angle);
        Destroy(gameObject);
    }

    public override int GetScore()
    {
        return _slicedAmount < amountToSlice ? 50 : 10;
    }
}