using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartLevelZero()
    {
        SceneManager.LoadScene("LevelZero");
    }

    public void StartLevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void StartLevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void StartLevelThree()
    {
        SceneManager.LoadScene("LevelThree");
    }

    public void LoadHelpScene()
    {
        SceneManager.LoadScene("Help");
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        
    }
}
