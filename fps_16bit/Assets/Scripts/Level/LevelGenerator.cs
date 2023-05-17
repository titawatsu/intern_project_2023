using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Range(5, 500)] public int levelWidth = 5, levelHeight = 5;
    public int startX, startY;
    LevelCell[,] levelCell;
    
    Vector2Int currentCell;

    public LevelCell[,] GetMaze()
    {
        levelCell = new LevelCell[levelWidth, levelHeight];

        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                levelCell[x, y] = new LevelCell(x, y);
            }
        }
        CarvePath(startX, startY);

        return levelCell;
    }
    List<Direction> directions = new List<Direction>
    {
        Direction.Up, Direction.Down, Direction.Left, Direction.Right,
    };

    List<Direction> GetRandomDirections()
    {
        List<Direction> dir = new List<Direction>(directions);

        List<Direction> rndDir = new List<Direction>();

        while (dir.Count > 0)
        {
            int rnd = Random.Range(0, dir.Count);
            rndDir.Add(dir[rnd]);
            dir.RemoveAt(rnd);
        }

        return rndDir; 
    }

    bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x > levelWidth - 1 || y > levelHeight - 1 || levelCell[x, y].visited) return false;
        else return true;
    }

    Vector2Int CheckNeighbour()
    {
        List<Direction> rndDir = GetRandomDirections();

        for (int i = 0; i < rndDir.Count; i++)
        {
            Vector2Int neighbour = currentCell;

            switch (rndDir[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
            }

            if (IsCellValid(neighbour.x, neighbour.y)) return neighbour;
        }

        return currentCell;
    }

    void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x)
        {
            levelCell[primaryCell.x, primaryCell.y].leftWall = false;
        } else if (primaryCell.x < secondaryCell.x)
        {
            levelCell[primaryCell.x, primaryCell.y].leftWall = false;
        } else if (primaryCell.y < secondaryCell.y)
        {
            levelCell[primaryCell.x, primaryCell.y].topWall = false;
        } else if (primaryCell.y > secondaryCell.y)
        {
            levelCell[primaryCell.x, primaryCell.y].topWall = false;
        }
    }

    void CarvePath(int x, int y)
    {
        if (x < 0 || y < 0 || x > levelWidth - 1 || y > levelHeight - 1)
        {
            x = y = 0;
            Debug.LogWarning("Starting position is out of bounds, defaulting to 0, 0");
        }

        currentCell = new Vector2Int(x, y);
        List<Vector2Int> path = new List<Vector2Int>();

        bool deadEnd = false;
        while (!deadEnd)
        {
            Vector2Int nextCell = CheckNeighbour();
            if (nextCell == currentCell)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbour();
                    
                    if (nextCell != currentCell) break;
                }

                if (nextCell == currentCell)
                    deadEnd = true;
            }
            else
            {
                BreakWalls(currentCell,nextCell);
                levelCell[currentCell.x, currentCell.y].visited = true;
                currentCell = nextCell;
                path.Add(currentCell);
            }
        }
    }
}



public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class LevelCell
{
    public bool visited;
    public int x, y;

    public bool topWall;
    public bool leftWall;

    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }

    public LevelCell(int x, int y)
    {
        this.x = x;
        this.y = y;

        visited = false;
        topWall = leftWall = true;
    }
}
