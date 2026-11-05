using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Vector2 minMaxPitch = new(0.6f, 1.4f);

    public void RandomizePitch() => source.pitch = Random.Range(minMaxPitch.x, minMaxPitch.y);
}
