using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterEnemyState : AEnemyState {

    private bool playerAggro = false;
    
    
    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public override Sprite GetStateSprite()
    {
        return ResourceLoader.instance.skaterSprite;
    }

    public override float GetStateSpeed()
    {
        return 1.0f;
    }

    /// <summary>
    /// Updates against the enemy's transform
    /// </summary>
    /// <param name="transform"></param>
    public override void DoUpdate(EnemyController enemy)
    {
        
        if(enemy.navigation.HasTarget())
        {
            

        }
        else
        {
            GameObject waypoints = GameObject.Find("Waypoints" + GlobalPersistentStats.instance.level);
            
            int index = Random.Range(0, waypoints.transform.childCount);
            GameObject target = waypoints.transform.GetChild(index).gameObject;
            enemy.navigation.MoveTo(target, GetStateSpeed());
        }


        //Interup skater to chase player until out of aggro range.
        if (Vector2.Distance(enemy.transform.position, PlayerController.instance.transform.position)
            <= 2.0f)
        {
            if (!playerAggro)
            {
                playerAggro = true;
                enemy.navigation.MoveTo(PlayerController.instance.gameObject, GetStateSpeed());
            }
        }
        else
        {
            if(playerAggro)
            {
                playerAggro = false;
                enemy.navigation.Stop();
            }
        }
        

        //float step = GetStateSpeed() * Time.deltaTime;
        //enemy.rbody.MovePosition(Vector2.MoveTowards(enemy.transform.position, PlayerController.instance.transform.position, step));
    }
}
