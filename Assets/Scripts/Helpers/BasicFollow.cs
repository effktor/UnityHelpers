using System.Collections;
using UnityEngine;

public class BasicFollow : MonoBehaviour
{
    public Transform objectToFollow;
    public float offset;
    public float smooth;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, objectToFollow.position - (objectToFollow.forward * offset), smooth);

        this.transform.LookAt(objectToFollow);
    }
}