using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeAggroCircle : MonoBehaviour {

    public CliqueEnum clique;

    public const float cResizeRate = 0.1f;

    private const float cMinRadius = 0.5f;

    // Use this for initialization
    void Start () {
		
    }

    // Update is called once per frame
    void Update () {

        float affinity = 0;
        float radius = 0;

        affinity = gameObject.GetComponentInParent<EnemyController>().GetPersonalAffinity();
        radius = cMinRadius + affinity * cResizeRate;
        gameObject.transform.localScale = new Vector3(radius, radius, 0);

        if (PlayerStateController.instance.CurrentState == transform.parent.GetComponent<EnemyController>().clique) {
            //Green
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 0.6f);

        } else {
            //Red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
        }
    }
}
