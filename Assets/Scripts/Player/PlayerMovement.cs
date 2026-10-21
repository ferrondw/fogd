using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    [SerializeField] private new Camera camera;
    [SerializeField] private Score score;
    [SerializeField] private Vector3 mouseOffset;
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float minSlicingSpeed = 1;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var offsetMousePosition = camera.ScreenToWorldPoint(Input.mousePosition + mouseOffset);
        var positionDifference = offsetMousePosition - transform.position;
        
        // more efficient than setting it equal to GetMouseButton(0) i think, at least not a call every frame
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = offsetMousePosition;
            visual.SetActive(true);
            _collider.enabled = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            visual.SetActive(false);
            _collider.enabled = false;
        }
        
        _rigidbody.velocity = positionDifference * movementSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent<EnemyBase>(out var enemyBase) || _rigidbody.velocity.magnitude < minSlicingSpeed) return;
        
        var absoluteAngle = Vector2.Angle(_rigidbody.velocity, Vector2.up);
        var correctedAngle = _rigidbody.velocity.x > 0 ? absoluteAngle : 360 - absoluteAngle;
        
        enemyBase?.OnSliced(correctedAngle);
        // if I use enemyBase?.GivenScore, then private Score.score needs to be nullable too which I don't want
        if (enemyBase != null) score?.Add(enemyBase.GivenScore);
    }
}