using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMaker : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileBase skipTile;

    [SerializeField] private List<GameObject> tileObjects;

    private Camera mainCamera;
    private GridBase grid;

    private void Start()
    {
        grid = new GridBase(tileMap.cellBounds.size.x , tileMap.cellBounds.size.y);
        int indexX = 0;
        int indexY = 0;
        mainCamera = Camera.main;
        // Debug.Log(tileMap.cellBounds.ToString());
        for(int i = tileMap.cellBounds.xMin;  i < tileMap.cellBounds.xMax; i++)
        {
            for (int j = tileMap.cellBounds.yMin; j < tileMap.cellBounds.yMax; j++)
            {
                TileBase _tile = tileMap.GetTile(new Vector3Int(i, j));
                if (_tile != null && _tile == skipTile)
                {
                    Debug.Log("Skip " + i + " " + j);
                }
                else
                {
                    Vector3 worldPos = tileMap.CellToWorld(new Vector3Int(i, j)) + new Vector3(0.5f, 0.5f, 0);
                    GameObject gameObject = Instantiate(tileObjects[0], worldPos, new Quaternion());
                    GridTile tileToPlace = new GridTile();
                    tileToPlace.x = i;
                    tileToPlace.y = j;
                    tileToPlace.tileGameObject = gameObject;
                    grid.PlaceTileOn(indexX, indexY, tileToPlace);
                }
                indexY ++;
            }
            indexY = 0;
            indexX++;
        }
    }
}
