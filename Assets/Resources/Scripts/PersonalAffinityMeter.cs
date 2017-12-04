using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalAffinityMeter : MonoBehaviour {

    SpriteMask mask;
    EnemyController enemyController;
    RectTransform spriteRectTransform;
    SpriteRenderer spriteRenderer;

    float originalY;
    float originalHeight;

	// Use this for initialization
	void Start () {
        mask = GetComponent<SpriteMask>();
        spriteRectTransform = GetComponentInChildren<RectTransform>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyController = GetComponentInParent<EnemyController>();
        originalY = spriteRectTransform.localPosition.y;
        originalHeight = spriteRenderer.bounds.size.y;
        

    }
	
	// Update is called once per frame
	void Update () {
        displayAffinity();
	}

    void displayAffinity()
    {
        float affinity = enemyController.personalAffinity;
        float maxAffinity = enemyController.personalAffinityMax;

        float affinityPercent = affinity / maxAffinity;

        Vector3 spritePos = spriteRectTransform.transform.localPosition;

        //Debug.Log("=====MaskTest=====");
        //Debug.Log("origHeight: " + originalY);
        spritePos.Set(spritePos.x, originalY - 1f + affinityPercent * 0.9f, spritePos.z);

        spriteRectTransform.localPosition = spritePos;
        //Rect spriteRect = spriteRectTransform.rect;
        //spriteRect.y = originalY + originalHeight * .5f;

        //spriteRectTransform.rect.Set(spriteRect.x, spriteRect.y, spriteRect.width, spriteRect.height);



        //Vector2 maskPos = mask.rectTransform.localPosition;
        //Rect maskRect = mask.rectTransform.rect;
        //float maskHeight = mask.rectTransform.rect.height;

        //Debug.Log("=====MaskTest=====");
        //Debug.Log("origY: " + originalY);
        //Debug.Log("maskRect: " + maskRect.x + " " + maskRect.y + " " + maskRect.height + " " + maskRect.width);
        //mask.rectTransform.rect.Set(maskRect.x,originalY + .5f, maskRect.width, maskRect.height);

        //Rect maskRect2 = mask.rectTransform.rect;
        //Debug.Log("maskRect2: " + maskRect2.x + " " + maskRect2.y + " " + maskRect2.height + " " + maskRect2.width);
        //maskPos.y = maskHeight / 2f;
        //mask.transform.position = maskPos;

        //Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();

        //foreach (Transform transform in transforms)
        //{

        //    if (transform.name.Contains("MiniMapIcon"))
        //    {

        //        affinity = LevelManager.instance.getCurrentAffiliation(clique);
        //        radius = cMinRadius + affinity * cResizeRate;
        //        transform.localScale = new Vector3(radius, radius, 0);
        //    }
        //}
    }
}
