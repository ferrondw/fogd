using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject enemyShard;
    
    private new Rigidbody2D rigidbody;
    private readonly int _angle = Shader.PropertyToID("_Angle");
    private readonly Vector2 _randomVelocity = new(-2, 2); // added to both x/y components
    private readonly float _slicingAngleMultiplier = 2.5f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public abstract void OnSliced(float angle);
    public abstract int GetScore(); // public field is not serializable in the editor nor accessible in the inheritor

    protected void SpawnShards(float angle)
    {
        var (formerHalf, formerHalfMaterial, formerHalfRigidbody) = InstantiateShard();
        var (latterHalf, latterHalfMaterial, latterHalfRigidbody) = InstantiateShard();

        var slicingRadians = Mathf.Deg2Rad * (180 - angle);
        var slicingVector = new Vector2(Mathf.Sin(slicingRadians), -Mathf.Cos(slicingRadians)) * _slicingAngleMultiplier;

        formerHalfRigidbody.velocity = rigidbody.velocity + RandomVelocity() + slicingVector;
        formerHalfRigidbody.angularVelocity = rigidbody.angularVelocity;
        
        latterHalfRigidbody.velocity = rigidbody.velocity + RandomVelocity() + slicingVector;
        latterHalfRigidbody.angularVelocity = rigidbody.angularVelocity;

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