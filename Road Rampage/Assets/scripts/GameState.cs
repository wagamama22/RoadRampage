using UnityEngine;

public static class GameState
{
    public static int PlayerHealth { get; set; } = 0; // 0 means not initialized, Player will set to 100 first time
    public static int SpawnCounter { get; set; } = 0; // Starts at 0 every new game session
}
