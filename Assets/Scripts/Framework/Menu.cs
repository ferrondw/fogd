using UnityEngine;
using Yakanashe.Yautl;
using Yakapedia;

public class Menu : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private CanvasGroup menuCanvasGroup;
    [SerializeField] private CanvasGroup gameCanvasGroup;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    
    public void StartGame()
    {
        spawner.Paused = false;
        
        menuCanvasGroup.FadeTo(0, 0.5f);
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
        
        gameCanvasGroup.FadeTo(1, 0.5f);
    }

    public void GameOver()
    {
        spawner.Paused = true;
        
        gameCanvasGroup.FadeTo(0, 0.5f);
        gameOverCanvasGroup.FadeTo(1, 0.5f);
        gameOverCanvasGroup.interactable = true;
        gameOverCanvasGroup.blocksRaycasts = true;
    }

    public void ReloadScene()
    {
        Utility.ReloadCurrentScene();
    }
}
