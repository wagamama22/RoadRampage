using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delayBeforeLoad = 2f; // seconds before loading next scene
    [SerializeField] string gameOverSceneName = "GameOver"; // name of GameOver scene

    // Load next level in build order
    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No more levels. Returning to main menu.");
            SceneManager.LoadScene(0); // load first scene (menu)
        }
    }

    // Restart current level
    public void RestartLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    // Load Game Over scene
    public void LoadGameOver()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    // Load any scene by name
    public void LoadSceneByName(string RoadRampage)
    {
        SceneManager.LoadScene(RoadRampage);
    }

    // Delayed load (useful for death animations)
    public void LoadSceneWithDelay(string Level1)
    {
        StartCoroutine(LoadAfterDelay(Level1));
    }

    private IEnumerator LoadAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneByQuit()
    {
        Application.Quit();
        print("game closed");
        EditorApplication.isPlaying = false;
    }

}
#endif