using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject followObj;

    public void SetFollow(GameObject obj)
    {
        followObj = obj;
    }

    void LateUpdate()
    {
        if (followObj == null) return;

        Vector2 followPos = followObj.transform.position;
        transform.position = new Vector3(followPos.x, followPos.y, transform.position.z);
    }

}
