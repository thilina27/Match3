using Unity.Mathematics;
using UnityEngine;

namespace Match3
{
    public class GridBase
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public GridTile[,] Tiles;

        public GridBase(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new GridTile[Width, Height];
        }

        public void PlaceTileOn(int posX, int posY, GridTile tile)
        {
            Tiles[posX, posY] = tile;
        }

        public bool HasTileOn(int x, int y)
        {
            if (Width <= x || 0 > x || 0 > y || Height <= y) return false;
            return Tiles[x, y] != null;
        }

        public GridTile GetTileAt(int x, int y)
        {
            if (HasTileOn(x, y)) 
            {
                return Tiles[x, y]; 
            }
            return null;
        }

        public void SwapTiles(GridTile tile, GridTile other)
        {
            int x = tile.x;
            int y = tile.y;

            Tiles[other.x, other.y] = tile;
            Tiles[x, y] = other;

            tile.x = other.x;
            tile.y = other.y;

            other.x = x;
            other.y = y;
        }
    }

}

