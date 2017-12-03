using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour {

    public GameObject entity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmosSelected()
    {
        DrawGizmosForWaypoint(Color.red, Color.blue);
    }

    void OnDrawGizmos()
    {
        DrawGizmosForWaypoint(Color.green, Color.black);
        
    }

    private void DrawGizmosForWaypoint(Color colorBox, Color colorLine)
    {
        Gizmos.color = colorLine;
        Gizmos.DrawLine(entity.transform.position, transform.position);
        Gizmos.color = colorBox;
        Gizmos.DrawCube(transform.position, new Vector3(0.25f, 0.25f, 1f));
    }


}
