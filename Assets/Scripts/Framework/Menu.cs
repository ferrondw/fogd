using UnityEngine;
using Yakanashe.Yautl;

public class Menu : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private CanvasGroup menuCanvasGroup;
    [SerializeField] private CanvasGroup gameCanvasGroup;
    
    public void StartGame()
    {
        spawner.Paused = false;
        
        menuCanvasGroup.FadeTo(0, 0.5f);
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
        
        gameCanvasGroup.FadeTo(1, 0.5f);
    }
}
