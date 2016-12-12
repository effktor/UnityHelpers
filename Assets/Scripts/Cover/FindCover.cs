using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FindCover : MonoBehaviour
{
/*
                Find position on nav mesh
                Find end position on navmesh on opposite side
                Check height of cover to see if we can get over the cover


                BUG: If a wall is behind a cover, we would not detect the wall as a wall, need to check the height of the object to tag it as a wall



            */


    public Transform DebugShowStart;
    public Transform DebugShowEnd;

    public LineRenderer LineRenderer;
    public float DistanceToCover = 1f;

    private void Start() { LineRenderer = GetComponent<LineRenderer>(); }

    public Transform target;
    private NavMeshHit hit;
    private NavMeshHit hit2;
    private NavMeshHit hit3;
    private bool blocked = false;
    private bool blocked2 = false;
    private bool blocked3 = false;

    void Update()
    {
        blocked = NavMesh.Raycast(transform.position, target.position, out hit, NavMesh.AllAreas);
        Debug.DrawLine(transform.position, target.position, blocked ? Color.red : Color.green);
        if (blocked)
            Debug.DrawRay(hit.position, Vector3.up, Color.red);


        blocked2 = NavMesh.Raycast(target.position, transform.position, out hit2, NavMesh.AllAreas);
        Debug.DrawLine(target.position, transform.position, blocked2 ? Color.red : Color.green);
        if (blocked2)
            Debug.DrawRay(hit2.position, Vector3.up, Color.red);

        if (blocked && blocked2)
        {
            blocked3 = NavMesh.SamplePosition(Vector3.Lerp(hit.position, hit2.position, 0.5f) + (Vector3.up * 1),
                out hit3, 1f, NavMesh.AllAreas);
            //Debug.Log(hit3.position.y);
            if (blocked3 && hit3.position.y < 1.5f)
            {
                Debug.DrawRay(hit3.position, Vector3.up, Color.blue);
            }
        }

        var direction = target.position - this.transform.position;

        if (blocked && blocked2 && blocked3)
        {

            var midpoint = Vector3.Lerp(hit.position, hit2.position, 0.5f);
            midpoint.y = 2;
            DebugShowStart.position = hit.position - (direction.normalized * DistanceToCover);
            DebugShowEnd.position = hit2.position + (direction.normalized * DistanceToCover);

            //DebugShowEnd.GetComponent<Renderer>().enabled = true;

            LineRenderer.SetPosition(0, new Vector3(transform.position.x, 1, transform.position.z));
            LineRenderer.SetPosition(1, hit.position - (direction.normalized * DistanceToCover));
            LineRenderer.SetPosition(2, new Vector3(hit3.position.x, 2, hit3.position.z));
            LineRenderer.SetPosition(3, hit2.position + (direction.normalized * DistanceToCover));
            LineRenderer.enabled = true;
        }
        else if (blocked && !blocked3)
        {
            DebugShowStart.position = hit.position - (direction.normalized * DistanceToCover);
            DebugShowEnd.GetComponent<Renderer>().enabled = false;

            LineRenderer.enabled = false;
        }
        else if (!blocked)
        {
            DebugShowStart.position = this.transform.position + direction.normalized * 3;
            DebugShowEnd.GetComponent<Renderer>().enabled = false;
            LineRenderer.enabled = false;
        }
    }

    private void FindCoverPosition() { }

    private void RayForwardForCover() { }
}