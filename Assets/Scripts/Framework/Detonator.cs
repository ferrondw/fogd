using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private float destroyAfterSeconds = 3f;

    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    private void LateUpdate()
    {
        if (Time.time - startTime < destroyAfterSeconds) return;
        Destroy(gameObject);
    }
}