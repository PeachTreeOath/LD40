using UnityEngine;
using System.Collections;

public class NavTilemap {

    public class NavTile {
        public NavTile(Vector3Int location) {
            this.location = location;
        }

        public Vector3Int location { get; private set; }
        public bool solid = false;
        public bool exists = false;
        public Waypoint waypoint;
    }

    private BoundsInt navBounds;
    private NavTile[,] tiles;

    public BoundsInt cellBounds { get { return navBounds; } }

    public NavTilemap(BoundsInt bounds) {
        navBounds = bounds;
        tiles = new NavTile[bounds.size.x, bounds.size.y];

        for(int x = 0; x < tiles.GetLength(0); x++) {
            for(int y = 0; y < tiles.GetLength(1); y++) {
                tiles[x, y] = new NavTile(new Vector3Int(x, y, 0));
            }
        }
    }

    public NavTile GetTile(int x, int y) {
        int navX = x - navBounds.xMin;
        int navY = y - navBounds.yMin;

        if (navX < 0 || navX >= tiles.GetLength(0) || navY < 0 || navY >= tiles.GetLength(1)) {
            return null;
        }

        return tiles[navX, navY];
    }

    public NavTile GetTile(Vector3Int location) {
        return GetTile(location.x, location.y);
    }
}
