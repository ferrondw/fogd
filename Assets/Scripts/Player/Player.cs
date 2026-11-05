using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Menu menu;
    [SerializeField] private Score score;
    [SerializeField] private Vector3 mouseOffset;

    private Vector2 _positionLastFrame;
    private float _angle;
    private Vector2 _cameraXBounds;
    private Vector2 _cameraYBounds;

    private void Start()
    {
        _cameraXBounds = new Vector2(camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x,
            camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
        _cameraYBounds = new Vector2(camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y,
            camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        var desiredPosition = camera.ScreenToWorldPoint(Input.mousePosition + mouseOffset);
        var clampedPosition = new Vector3(Mathf.Clamp(desiredPosition.x, _cameraXBounds.x, _cameraXBounds.y),
            Mathf.Clamp(desiredPosition.y, _cameraYBounds.x, _cameraYBounds.y));
        
        transform.position = clampedPosition;

        _angle = Mathf.Atan2(_positionLastFrame.y - transform.position.y, _positionLastFrame.x - transform.position.x) * 180 / Mathf.PI;
        
        _positionLastFrame = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<EnemyBase>(out var enemyBase)) return;

        if (other.CompareTag("GameOver"))
        {
            if (score.Get() > PlayerPrefs.GetInt("highscore")) PlayerPrefs.SetInt("highscore", score.Get());
            menu.GameOver(score.Get());
        }

        var scoreToAdd = enemyBase.GetScore();

        var correctedAngle = _angle + 180;

        print($"Slicing on {correctedAngle}");
        enemyBase.OnSliced(correctedAngle);
        if (scoreToAdd != 0) score?.Add(scoreToAdd);
    }
}