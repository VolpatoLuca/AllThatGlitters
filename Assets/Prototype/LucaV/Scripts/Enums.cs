public enum Direction
{
    north,
    west,
    south,
    east
}
[System.Serializable]
public enum GameState
{
    waitingInput,
    loading,
    playing,
    gameOver,
    pause
}