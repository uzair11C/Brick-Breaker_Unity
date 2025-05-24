[System.Serializable]
public class LevelData
{
    public int rows;
    public int columns;
    public int[] layout;

    public int GetBrickType(int row, int col)
    {
        return layout[row * columns + col];
    }
}
