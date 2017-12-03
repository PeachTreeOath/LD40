using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeAggroCircle : MonoBehaviour {

    public CliqueEnum clique;

    private const float cMinRadius = 0.25f;
    private const float cLogBase = 3;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {

        float affinity = 0;
        float radius = 0;
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform transform in transforms) {

            if (transform.name.Contains("MiniMapIcon")) {

                affinity = LevelManager.instance.getCurrentAffiliation(clique);
                radius = cMinRadius + Mathf.Log(1 + affinity, cLogBase);
                transform.localScale = new Vector3(radius, radius, 0);
            }
        }
    }
}
