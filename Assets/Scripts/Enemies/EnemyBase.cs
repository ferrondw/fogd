using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{
    public int GivenScore { get; private set; } = 10;
    
    [SerializeField] private GameObject enemyShard;

    protected new Rigidbody2D rigidbody { get; private set; }

    private readonly int _angle = Shader.PropertyToID("_Angle");
    private readonly Vector2 _randomVelocity = new(-2, 2); // added to both x/y components
    private readonly float _slicingAngleMultiplier = 2.5f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void OnSliced(float angle)
    {
        var (formerHalf, formerHalfMaterial, formerHalfRigidbody) = InstantiateShard();
        var (latterHalf, latterHalfMaterial, latterHalfRigidbody) = InstantiateShard();

        var slicingRadians = Mathf.Deg2Rad * (180 - angle);
        var slicingVector = new Vector2(Mathf.Sin(slicingRadians), -Mathf.Cos(slicingRadians)) * _slicingAngleMultiplier;

        formerHalfRigidbody.velocity = rigidbody.velocity + RandomVelocity() + slicingVector;
        latterHalfRigidbody.velocity = rigidbody.velocity + RandomVelocity() + slicingVector;

        // set the shader to the 2 halves
        formerHalfMaterial?.SetFloat(_angle, angle + transform.eulerAngles.z);
        latterHalfMaterial?.SetFloat(_angle, angle + transform.eulerAngles.z + 180);
    }

    private (GameObject, Material, Rigidbody2D) InstantiateShard()
    {
        var shard = Instantiate(enemyShard, transform.position, transform.rotation);
        var shardMaterial = shard.GetComponent<SpriteRenderer>()?.material;
        var shardRigidbody = shard.GetComponent<Rigidbody2D>();
        return (shard, shardMaterial, shardRigidbody);
    }

    private Vector2 RandomVelocity()
    {
        var randomX = Random.Range(_randomVelocity.x, _randomVelocity.y);
        var randomY = Random.Range(_randomVelocity.x, _randomVelocity.y);
        return new Vector2(randomX, randomY);
    }
}