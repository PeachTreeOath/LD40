using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatIndicator : MonoBehaviour {

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerStateController.instance.CurrentState == transform.parent.GetComponent<EnemyController>().clique)
        {
            //Green
            spriteRenderer.color = new Color(0.0f, 1.0f, 0.0f, 0.2f);
            
        }
        else
        {
            //Red
            spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, 0.2f);
        }
	}


}
