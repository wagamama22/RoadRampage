using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LastLevelTracker
{
    // Stores the last level the player was actually playing
    public static int LastPlayedIndex = -1;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delayBeforeLoad = 2f;
    [SerializeField] string gameOverSceneName = "GameOver";

    //  Load next level in build order
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
            SceneManager.LoadScene(0);
        }
    }

    // Restart the last played level
    public void RestartLevel()
    {
        // Safety check — if somehow nothing was saved, avoid crashing
        if (LastLevelTracker.LastPlayedIndex < 0)
        {
            Debug.LogWarning("No last played level saved. Loading menu instead.");
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(LastLevelTracker.LastPlayedIndex);
    }

    //  Load Game Over and SAVE the last played level
    public void LoadGameOver()
    {
        // Save the level the player was on BEFORE GameOver
        LastLevelTracker.LastPlayedIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(gameOverSceneName);
    }

    //  Load any scene by name (manual navigation)
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //  Delayed load (death animations, transitions)
    public void LoadSceneWithDelay(string sceneName)
    {
        StartCoroutine(LoadAfterDelay(sceneName));
    }

    private IEnumerator LoadAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneName);
    }

    //  Quit game
    public void LoadSceneByQuit()
    {
        Application.Quit();
        Debug.Log("Game closed");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}