using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject enemyShard;
    
    protected new Rigidbody2D rigidbody { get; private set; }
    
    private readonly int _angle = Shader.PropertyToID("_Angle");
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void OnSliced(float angle)
    {
        Debug.Log($"Sliced at {angle} degrees");
        
        var formerHalf = Instantiate(enemyShard, transform.position, transform.rotation);
        var formerHalfMaterial = formerHalf.GetComponent<SpriteRenderer>()?.material;
        
        var latterHalf = Instantiate(enemyShard, transform.position, transform.rotation);
        var latterHalfMaterial = latterHalf.GetComponent<SpriteRenderer>()?.material;

        formerHalf.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 2);
        latterHalf.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 2);
        
        formerHalfMaterial?.SetFloat(_angle, angle + transform.eulerAngles.z);
        latterHalfMaterial?.SetFloat(_angle, angle + transform.eulerAngles.z + 180);
    }
}