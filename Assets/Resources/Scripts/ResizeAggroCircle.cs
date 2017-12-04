using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeAggroCircle : MonoBehaviour {

    public CliqueEnum clique;

    public const float cResizeRate = 0.05f;

    private const float cMinRadius = 0.25f;

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
                radius = cMinRadius + affinity * cResizeRate;
                transform.localScale = new Vector3(radius, radius, 0);
            }
            if (transform.name.Contains("ThreatIndicator"))
            {

                affinity = LevelManager.instance.getCurrentAffiliation(clique);
                radius = cMinRadius + affinity * cResizeRate;
                transform.localScale = new Vector3(radius, radius, 0);
            }
        }
    }
}
