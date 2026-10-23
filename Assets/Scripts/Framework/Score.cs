using TMPro;
using UnityEngine;
using Yakanashe.Yautl;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private float popoutDuration = 0.4f;
    [SerializeField] private float popoutMultiplier = 1.8f;
    [SerializeField] private Vector2 randomPopoutRotation = new (-30f, 30f);

    private int score;

    public void Add(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreDisplay.text = $"{score}";

        var scoreTransform = scoreDisplay.transform;
        
        // little bob thingy
        scoreTransform.eulerAngles = new Vector3(0, 0, Random.Range(randomPopoutRotation.x, randomPopoutRotation.y));
        scoreTransform.ScaleTo(Vector3.one * popoutMultiplier, 0).OnComplete(() =>
        {
            scoreTransform.ScaleTo(Vector3.one, popoutDuration, EaseType.InCubic);
            scoreTransform.RotateTo(Quaternion.identity, popoutDuration, EaseType.InCubic);
        });
    }
}
