using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;
    
    

    private void Start()
    {
        
        
        
            gameStartButton.onClick.AddListener(GameStart);
        
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    
}
