using UnityEngine;
using System.Collections;

public class NavTilemap {

    public class NavTile {
        public bool solid = false;
    }

    private BoundsInt navBounds;
    private NavTile[,] tiles;

    public BoundsInt cellBounds { get { return navBounds; } }

    public NavTilemap(BoundsInt bounds) {
        navBounds = bounds;
        tiles = new NavTile[bounds.size.x, bounds.size.y];

        for(int x = 0; x < tiles.GetLength(0); x++) {
            for(int y = 0; y < tiles.GetLength(1); y++) {
                tiles[x, y] = new NavTile();
            }
        }
    }

    public NavTile GetTile(int x, int y) {
        int navX = x - navBounds.xMin;
        int navY = y - navBounds.yMin;

        return tiles[navX, navY];
    }

    public NavTile GetTile(Vector3Int location) {
        return GetTile(location.x, location.y);
    }
}
