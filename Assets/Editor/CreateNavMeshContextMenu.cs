using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;

public class CreateNavMeshContextMenu {

    const int BIG_VALUE = 1000;
    const int SMALL_VALUE = -1000;  //MAX/MIN constants 
 
    [MenuItem("CONTEXT/Grid/Create Nav Waypoints")]
    private static void CreateNavWaypoints(MenuCommand menuCommand) {
        Debug.Log("Creating Nav Waypoints");

        var grid = menuCommand.context as Grid;
        var tilemaps = grid.GetComponentsInChildren<Tilemap>();

        var bounds = GetTotalBounds(tilemaps);

        NavTilemap navMap = new NavTilemap(bounds);

        UpdateTileCollision(navMap, tilemaps);
       

    }

    private static BoundsInt GetTotalBounds(Tilemap[] tilemaps) {
        BoundsInt bounds = new BoundsInt();
        bool doneFirst = false;

        foreach (Tilemap tilemap in tilemaps) {
            if(!doneFirst) {
                bounds = tilemap.cellBounds;
                doneFirst = true;
            } else {
                BoundsInt cellBounds = tilemap.cellBounds;

                bounds.xMin = Math.Min(bounds.xMin, cellBounds.xMin);
                bounds.yMin = Math.Min(bounds.yMin, cellBounds.yMin);
                bounds.xMax = Math.Max(bounds.xMax, cellBounds.xMax);
                bounds.yMax = Math.Max(bounds.yMax, cellBounds.yMax);
            }
        }

        return bounds;
    }

    private static void UpdateTileCollision(NavTilemap navMap, Tilemap[] tilemaps) {
        foreach(Tilemap tilemap in tilemaps) {
            if (!HasTileCollision(tilemap)) continue;

            for(int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++) {
                for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++) {
                    var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    if (tile) {
                        navMap.GetTile(x, y).solid = true;
                    }
                }
            }
        }
    }

    private static bool HasTileCollision(Component component) {
        return component.gameObject.GetComponent<TilemapCollider2D>() != null;
    }
}
