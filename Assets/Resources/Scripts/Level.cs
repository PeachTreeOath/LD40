using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int furryCount;
    public int skaterCount;
    public int footballerCount;
    public int nobodyCount;

    public void Enable()
    {
        GetComponentInChildren<Grid>().enabled = true;
        GetComponentInChildren<WaypointGroup>().enabled = true;
    }


    public void Disable()
    {
        GetComponentInChildren<Grid>().enabled = false;
        GetComponentInChildren<WaypointGroup>().enabled = false;
    }
}
