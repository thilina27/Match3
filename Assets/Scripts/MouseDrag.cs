using Match3;
using UnityEngine;

namespace GameControl
{
    [RequireComponent(typeof(Collider2D), typeof(GridTile))]
    public class MouseDrag : MonoBehaviour
    {
        public enum DIRECTION
        {
            NONE, LEFT, UP, RIGHT, DOWN
        }

        private GridTile tile;

        private void Awake()
        {
            tile = GetComponent<GridTile>();
        }
        private void OnMouseDrag()
        {
            Vector3 mousePos = GameInputs.Instance.MousePosInWorld();
            mousePos.z = transform.position.z;
            Vector3 dir = transform.position - mousePos;
            if (dir.magnitude >= 0.4f && !GameManager.Instance.Drag)
            {
                GameManager.Instance.Drag = true;
                Debug.Log(GetTileDirection(dir));
                GridMaker.Instance.SwapTiles(tile, GetTileDirection(dir));
            }
        }

        public Vector2Int GetTileDirection(Vector3 dir)
        {
            Vector2Int otherTile = new Vector2Int();
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                otherTile.y = tile.y;
                if (dir.x > 0)
                {
                    otherTile.x = tile.x - 1;
                }
                else
                {
                    otherTile.x = tile.x + 1;
                }
            }
            else
            {
                otherTile.x = tile.x;
                if (dir.y > 0)
                {
                    otherTile.y = tile.y - 1;
                }
                else
                {
                    otherTile.y = tile.y + 1;
                }
            }

            return otherTile;
        }
    }

}
