using UnityEngine;

namespace Game.Utils
{
    public static class GridUtility
    {
        // Convert grid coordinates (int tileX, tileY) to world position (tile center)
        // tileSize: world units per tile (default 1)
        public static Vector3 TileToWorld(Vector2Int tile, float tileSize = 1f)
        {
            return new Vector3(tile.x * tileSize, tile.y * tileSize, 0f);
        }

        // Convert world position to grid (rounds to nearest int)
        public static Vector2Int WorldToTile(Vector3 worldPos, float tileSize = 1f)
        {
            int x = Mathf.RoundToInt(worldPos.x / tileSize);
            int y = Mathf.RoundToInt(worldPos.y / tileSize);
            return new Vector2Int(x, y);
        }

        // Compute default visual offset to apply to the transform so a center-pivot, PxP sprite
        // visually lines up relative to the tile.
        // spritePixelHeight: e.g. 32
        // pixelsPerUnit: e.g. 16
        // tileSize: e.g. 1
        // This returns the offset to add to TileToWorld(...) to position the object's transform
        // so visuals sit correctly. You can override in inspector per-entity.
        public static Vector3 DefaultVisualOffsetY(int spritePixelHeight = 32, int pixelsPerUnit = 16, float tileSize = 1f)
        {
            float spriteUnitHeight = (float)spritePixelHeight / pixelsPerUnit; // e.g. 2 units
            float offsetY = (spriteUnitHeight - tileSize) * 0.5f;
            return new Vector3(0f, offsetY, 0f);
        }
    }
}
