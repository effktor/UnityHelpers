using UnityEngine;

public class ProjectileFast : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _velocity;

    private Vector3 _previousPosition;
    private Vector3 _movementThisStep;

    private void Start()
    {
        _previousPosition = this.transform.position;

    }

    private void Update()
    {
        _movementThisStep = this.transform.position - _previousPosition;

        float movementSqrMagnitude = _movementThisStep.sqrMagnitude;
        float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

        RaycastHit hitInfo;

        if (Physics.Raycast(_previousPosition, _movementThisStep, out hitInfo, movementMagnitude, _layerMask.value))
        {
            //this.transform.position = hitInfo.point; // Used if you want to spawn effects or something at impact point
            Destroy(this.gameObject);
        }

        _previousPosition = this.transform.position;

        transform.Translate((new Vector3(0, 0, _velocity)) * Time.deltaTime);

        Debug.Log("UPDATE: " + fixedUpdate++);
    }

    private int fixedInt = 0;
    private int fixedUpdate = 0;

    private void FixedUpdate()
    {
        Debug.Log("FIXED: " + fixedInt++);
    }
}