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

        public void PlaceTileOn(int  posX, int posY, GridTile tile)
        {
            Tiles[posX, posY] = tile;
        }

        public bool HasTileOn(int x, int y)
        {
            return Tiles[x, y] != null;
        }
    }

}

