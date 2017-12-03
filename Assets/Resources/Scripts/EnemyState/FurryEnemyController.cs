using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryEnemyController : EnemyController {

    private bool playerAggro = false;
    
    public override float GetStateSpeed()
    {
        return 1.0f;
    }

    public void Update()
    {
        
        if(navigation.HasTarget())
        {
            

        }
        else
        {
            GameObject waypoints = GameObject.Find("Waypoints" + GlobalPersistentStats.instance.level);
            
            int index = Random.Range(0, waypoints.transform.childCount);
            GameObject target = waypoints.transform.GetChild(index).gameObject;
            navigation.MoveTo(target, GetStateSpeed());
        }


        //Interup skater to chase player until out of aggro range.
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position)
            <= 2.0f)
        {
            if (!playerAggro)
            {
                playerAggro = true;
                navigation.MoveTo(PlayerController.instance.gameObject, GetStateSpeed());
            }
        }
        else
        {
            if(playerAggro)
            {
                playerAggro = false;
                navigation.Stop();
            }
        }
        

        //float step = GetStateSpeed() * Time.deltaTime;
        //enemy.rbody.MovePosition(Vector2.MoveTowards(enemy.transform.position, PlayerController.instance.transform.position, step));
    }
}
