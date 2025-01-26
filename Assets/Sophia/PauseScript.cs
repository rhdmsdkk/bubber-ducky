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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        player.transform.position = Globals.playerStartingPosition;
        Globals.introOccurring = true;
        Globals.isRestart = true;
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
        pauseMenu.SetActive(false);
        
        Time.timeScale = 1f;
    }

    public void ExitToHome()
    {
        Restart();
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(Globals.currentScene);
    }
}