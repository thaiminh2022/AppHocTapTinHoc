using UnityEngine.SceneManagement;
using UnityEngine;

public class UiUtilities : MonoBehaviour
{

    public static UiUtilities instance;
    private void Awake()
    {
        instance = this;
    }


    // Gameobjects
    public void EnableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void DisableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    // screen
    public void ExitApp()
    {
        Application.Quit();
    }
    public void RestartScreen()
    {
        Loader.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SwitchScene(string name)
    {
        Loader.LoadScene(name);
    }
    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Game
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
