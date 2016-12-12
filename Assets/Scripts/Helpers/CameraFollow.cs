using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followObject;
    public float speed = 0.01f;
    public float distance = 5f;
    public float height;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var newPos = this.transform.position;
        var behind = followObject.position - (followObject.forward * distance);
        newPos.y = Mathf.Lerp(newPos.y, followObject.position.y + height, speed);
        newPos = Vector3.Lerp(newPos, newPos.StayAtDistance(followObject.position, distance + height), speed);
        this.transform.position = newPos;
        this.transform.LookAt(followObject);
    }

    public Vector3 DistanceKepp(Vector3 position, Vector3 target, float distance)
    {
        var offset  = position - target;
        if (offset.sqrMagnitude < distance * distance)
            return position + offset.normalized * (distance - offset.magnitude);
        return target + offset.normalized * (distance);
    }
}