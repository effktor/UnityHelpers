using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    /// <summary>
    /// Keeps the Vector3 at distance from target Vector3.
    /// </summary>
    public static Vector3 StayAway(this Vector3 position, Vector3 target, float distance)
    {
        Vector3 offset = position - target;
        if (offset.sqrMagnitude < distance * distance)
            return position + offset.normalized * (distance - offset.magnitude);
        return position;
    }

    /// <summary>
    /// Keeps the Vector3 close to target Vector3.
    /// </summary>
    public static Vector3 StayClose(this Vector3 position, Vector3 target, float distance)
    {
        Vector3 offset = position - target;
        if (offset.sqrMagnitude > distance * distance)
            return position + offset.normalized * (distance - offset.magnitude);
        return position;
    }


    /// <summary>
    /// Keeps the Vector3 at the same distance from target.
    /// </summary>
    public static Vector3 StayAtDistance(this Vector3 position, Vector3 target, float distance)
    {
        Vector3 offset = position - target;
        if (offset.sqrMagnitude < distance * distance)
            return position + offset.normalized * (distance - offset.magnitude);
        if (offset.sqrMagnitude > distance * distance)
            return position + offset.normalized * (distance - offset.magnitude);
        return position;
    }
}