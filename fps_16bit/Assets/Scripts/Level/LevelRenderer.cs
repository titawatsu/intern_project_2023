using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;

    [SerializeField] private GameObject levelCellPrefab;

    public float CellSize = 1f;

    private void Start()
    {
        LevelCell[,] levelCells = levelGenerator.GetMaze();

        for (int x = 0; x < levelGenerator.levelWidth; x++)
        {
            for (int y = 0; y < levelGenerator.levelHeight; y++)
            {
                GameObject newCell = Instantiate(levelCellPrefab,
                    new Vector3((float)x * CellSize, 0f, (float)y * CellSize), Quaternion.identity, transform);

                LevelGameObject levelGameObject = newCell.GetComponent<LevelGameObject>();

                bool top = levelCells[x, y].topWall;
                bool left = levelCells[x, y].leftWall;
                bool right = false;
                bool bottom = false;

                if (x == levelGenerator.levelWidth - 1) right = true;
                if (y == 0) bottom = true;
                
                levelGameObject.Init(top, bottom, right, left);
            }
        }
    }
}
