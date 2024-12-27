using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public PlayerMovement movement;
    public float levelRestartDelay = 0.5f;
    public float moveNextLevel = 0.5f;

    public float delayBetweenSuperJumps = 0.5f;

    public float movement_enabled = 0.5f;

    public void EndGame()
    {
        movement.enabled = false; 
        
        Invoke("ResetLevel" , levelRestartDelay);
    }

    public void NextLevel()
    {
        movement.enabled = false; 

        Invoke("Nextlevel" , moveNextLevel);
    }
   
    private void ResetLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Nextlevel() 
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentLevel + 1);
    }
    
    
    

}
