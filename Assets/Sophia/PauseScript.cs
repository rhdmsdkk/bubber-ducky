using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject player;
    private GameObject[] bubbles;
    private GameObject[] obstacles;

    private void Start()
    {
        bubbles = GameObject.FindGameObjectsWithTag("Bubble");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

}
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        player.transform.position = Globals.playerStartingPosition;
        Globals.percentageComplete = 0f;
        BubbleCountScript.numBubbles = 3;
        
        foreach (GameObject bub in bubbles)
        {
            bub.SetActive(true);
        }
        foreach (GameObject obs in obstacles)
        {
            obs.SetActive(true);
        }
        //insert UI update
    }
    public void ExitToHome()
    {
        //set scene to home screen
    }
}