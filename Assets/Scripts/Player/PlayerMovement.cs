using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Score score;
    [SerializeField] private Vector3 mouseOffset;
    [SerializeField] private float movementSpeed = 10;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var offsetMousePosition = camera.ScreenToWorldPoint(Input.mousePosition + mouseOffset);
        var positionDifference = offsetMousePosition - transform.position;
        
        _rigidbody.velocity = positionDifference * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<EnemyBase>(out var enemyBase)) return;
        
        var absoluteAngle = Vector2.Angle(_rigidbody.velocity, Vector2.up);
        var correctedAngle = _rigidbody.velocity.x > 0 ? absoluteAngle : 360 - absoluteAngle;
        
        enemyBase.OnSliced(correctedAngle);
        score?.Add(enemyBase.GetScore());
    }
}