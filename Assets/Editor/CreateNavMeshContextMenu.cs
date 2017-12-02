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

        BuildWaypoints(grid, navMap);
        ConnectNeighboringWaypoints(navMap);
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
            var hasCollision = HasTileCollision(tilemap);

            for(int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++) {
                for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++) {
                    var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    if(tile) {
                        var navTile = navMap.GetTile(x, y);
                        
                        navTile.exists = true; 
                        if(hasCollision) {
                            navTile.solid = true;
                        }
                    }
                }
            }
        }
    }

    private static bool HasTileCollision(Component component) {
        return component.gameObject.GetComponent<TilemapCollider2D>() != null;
    }

    //TODO move this to NavTilemap?
    private static void BuildWaypoints(Grid grid, NavTilemap navMap) {
        GameObject waypoints = new GameObject("Waypoints");

        for(int x = navMap.cellBounds.xMin; x < navMap.cellBounds.xMax; x++) {
            for(int y = navMap.cellBounds.yMin; y < navMap.cellBounds.yMax; y++) {
                var tile = navMap.GetTile(x, y); 
                if(!tile.solid) {
                    String name = String.Format("({0}, {1})", x, y);
                    GameObject waypointObj = new GameObject(name);

                    waypointObj.transform.position = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    waypointObj.transform.SetParent(waypoints.transform);
            
                    Waypoint waypoint = waypointObj.AddComponent<Waypoint>();
                    waypoint.neighbors = new List<GameObject>();

                    tile.waypoint = waypoint;
                }
            }
        }
    }

    private static void ConnectNeighboringWaypoints(NavTilemap navMap) {
        for (int x = navMap.cellBounds.xMin; x < navMap.cellBounds.xMax; x++) {
            for (int y = navMap.cellBounds.yMin; y < navMap.cellBounds.yMax; y++) {
                ConnectNeighboringWaypoints(navMap, x, y);
            }
        }
    }

    private static void ConnectNeighboringWaypoints(NavTilemap navMap, int x, int y) {
        var currTile = navMap.GetTile(x, y);

        if (currTile.waypoint == null) return;

        for(int dx = -1; dx <=  1; dx++) {
            for(int dy = -1; dy <= 1; dy++) {
                if (dx == dy && dx == 0) continue;

                var shouldConnect = false;
                var tile = navMap.GetTile(x + dx, y + dy);
                if (tile != null && tile.waypoint != null) {
                    var isDiagonal = dx != 0 && dy != 0;
                    if(isDiagonal) {
                        var leftTile = navMap.GetTile(x + dx, y);
                        var rightTile = navMap.GetTile(x, y + dy);

                        shouldConnect = (leftTile == null || !leftTile.solid) && (rightTile == null || !rightTile.solid);
                    } else {
                        shouldConnect = true;
                    }
                }

                if(shouldConnect) {
                    currTile.waypoint.neighbors.Add(tile.waypoint.gameObject);
                }
            }
        }
    }
}
