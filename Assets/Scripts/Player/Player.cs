using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Menu menu;
    [SerializeField] private Score score;
    [SerializeField] private Vector3 mouseOffset;
    [SerializeField] private float movementSpeed = 10;

    private Rigidbody2D _rigidbody;

    public Vector2 cameraXBounds;
    public Vector2 cameraYBounds;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        cameraXBounds = new Vector2(camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x,
            camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
        cameraYBounds = new Vector2(camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y,
            camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);
    }

    private void Update()
    {
        var offsetMousePosition = camera.ScreenToWorldPoint(Input.mousePosition + mouseOffset);
        var clampedMousePosition = new Vector3(Mathf.Clamp(offsetMousePosition.x, cameraXBounds.x, cameraXBounds.y),
            Mathf.Clamp(offsetMousePosition.y, cameraYBounds.x, cameraYBounds.y));
        var positionDifference = clampedMousePosition - transform.position;

        _rigidbody.linearVelocity = positionDifference * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<EnemyBase>(out var enemyBase)) return;

        if (other.CompareTag("GameOver"))
        {
            if(score.Get() > PlayerPrefs.GetInt("highscore")) PlayerPrefs.SetInt("highscore", score.Get());
            menu.GameOver(score.Get());
        }

        var scoreToAdd = enemyBase.GetScore();

        var absoluteAngle = Vector2.Angle(_rigidbody.linearVelocity, Vector2.up);
        var correctedAngle = _rigidbody.linearVelocity.x > 0 ? absoluteAngle : 360 - absoluteAngle;

        enemyBase.OnSliced(correctedAngle);
        if(scoreToAdd != 0) score?.Add(scoreToAdd);
    }
}