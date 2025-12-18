using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        SingletonMusicConfig();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void SingletonMusicConfig()
    {
        int musicPlayerCount = FindObjectsByType<MusicPlayer>(FindObjectsSortMode.None).Length;
        if (musicPlayerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
