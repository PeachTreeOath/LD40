using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOrientation : MonoBehaviour {
    
    private float originalXScale;
    private Vector2 positionLastFrame;
    private float lastUpdateTime = 0.0f;
    private float updateDelay = 0.25f;

	// Use this for initialization
	void Start () {
        originalXScale = transform.localScale.x;
	}

    void Update()
    {
        if(positionLastFrame == Vector2.zero)
        {
            positionLastFrame = transform.position;
        }
        else if(Time.time >= lastUpdateTime + updateDelay)
        {
            lastUpdateTime = Time.time;
            Vector2 direction = positionLastFrame - (Vector2)transform.position;
            Debug.Log(direction);
            UpdateDirection(direction);
            positionLastFrame = transform.position;
        }
        
    }

    void UpdateDirection(Vector2 direction)
    {
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(originalXScale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-originalXScale, transform.localScale.y, transform.localScale.z);
        }
    }
}
