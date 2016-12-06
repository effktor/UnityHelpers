using UnityEngine;

public class KeepDistance : MonoBehaviour
{
    public Transform player;
    public float desiredDistance;

    private void Update()
    {
        this.transform.position = DistanceKepp(this.transform.position, player.position, desiredDistance);
    }

    public Vector3 DistanceKepp(Vector3 position, Vector3 target, float distance)
    {
        var offset  = position - target;
        if (offset.sqrMagnitude < distance * distance)
            return position + offset.normalized * (distance - offset.magnitude);
        return position;
    }
}