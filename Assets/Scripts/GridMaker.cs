using GameControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3
{
    public class GridMaker : MonoBehaviour
    {
        public static GridMaker Instance { get; private set; }

        [SerializeField] private Tilemap tileMap;
        [SerializeField] private TileBase skipTile;

        [SerializeField] private List<GridTile> tileObjects;

        private Camera mainCamera;
        private GridBase grid;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
        }
        private void Start()
        {
            grid = new GridBase(tileMap.cellBounds.size.x, tileMap.cellBounds.size.y);
            int indexX = 0;
            int indexY = 0;
            mainCamera = Camera.main;

            for (int i = tileMap.cellBounds.xMin; i < tileMap.cellBounds.xMax; i++)
            {
                for (int j = tileMap.cellBounds.yMin; j < tileMap.cellBounds.yMax; j++)
                {
                    TileBase _tile = tileMap.GetTile(new Vector3Int(i, j));
                    if (_tile != null && _tile == skipTile)
                    {
                        // TODO : Add fillers 
                        grid.PlaceTileOn(indexX, indexY, null);
                    }
                    else
                    {
                        Vector3 worldPos = tileMap.CellToWorld(new Vector3Int(i, j)) + new Vector3(0.5f, 0.5f, 0);
                        GameObject gameObject = Instantiate(tileObjects[Random.Range(0, tileObjects.Count)].gameObject, worldPos, new Quaternion());
                        GridTile tileToPlace = gameObject.GetComponent<GridTile>();
                        tileToPlace.x = indexX;
                        tileToPlace.y = indexY;
                        grid.PlaceTileOn(indexX, indexY, tileToPlace);
                    }
                    indexY++;
                }
                indexY = 0;
                indexX++;
            }
        }

        public GridTile GetTileOnWorldPosition(Vector3 pos)
        {
            Vector3Int tilePos = tileMap.WorldToCell(pos);
            tilePos.x -= tileMap.cellBounds.xMin;
            tilePos.y -= tileMap.cellBounds.yMin;
            return grid.GetTileAt(tilePos.x, tilePos.y);
        }

        public void SwapTiles(GridTile fromTile, Vector2Int to)
        {
            // have tile to swap with
            GridTile toTile = grid.GetTileAt(to.x, to.y);
            if (toTile != null)
            {
                Vector3 fromPos = fromTile.transform.position;
                fromTile.transform.position = toTile.transform.position;
                toTile.transform.position = fromPos;
                grid.SwapTiles(fromTile, toTile);
            }
        }
    }

}
