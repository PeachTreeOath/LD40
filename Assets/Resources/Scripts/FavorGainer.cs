using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavorGainer : MonoBehaviour
{

    void OnTriggerStay2D(Collider2D col)
    {
        EnemyController enemy = col.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.ChangeFavor();
        }
    }

}
