using UnityEngine;
using UnityEngine.Events;
using Yakanashe.Yautl;

public class MenuEnemy : EnemyBase
{
    [SerializeField] private float startWobble = 20f;
    [SerializeField] private float wobbleDuration = 2f;
    [SerializeField] private UnityEvent onSlice;
    
    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -startWobble));
        transform.RotateTo(Quaternion.Euler(new Vector3(0, 0, startWobble)), wobbleDuration).SetLoops(-1, true);
    }

    public override void OnSliced(float angle)
    {
        SpawnShards(angle);
        Destroy(gameObject);
        onSlice.Invoke();
    }
    
    public override int GetScore()
    {
        return 0;
    }
}
