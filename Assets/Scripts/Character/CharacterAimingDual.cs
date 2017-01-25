using System.Collections;
using UnityEngine;


public class CharacterAimingDual : CharacterAiming
{
    public Transform CrosshairObject;

    public Transform TempObjectViz;

    private Vector3 _aimTarget;
    private Vector3 _crosshair;
    private Vector3 _lerp;
    private Vector3 _position;
    private Quaternion _rotation;


    [SerializeField]
    [Range(1, 20)]
    private float _crosshairDistance = 6f;

    [SerializeField]
    [Range(0, 1)]
    private float _crosshairSmooth = 0.1f;

    [SerializeField]
    [Range(0, 1)]
    private float _lookAtSmooth = 0.05f;

    [SerializeField]
    [Range(0, 1)]
    private float _deadzone = 0.5f;

    private float _crosshairSmoothCalculate;

    protected override void Start()
    {
        base.Start();

        _aimTarget = this.transform.forward * _crosshairDistance;
        _rotation = this.transform.rotation;
        _crosshairSmoothCalculate = _crosshairDistance;


        inputVector = this.transform.forward * 0.5f;

        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    protected override void Update()
    {
        base.Update();

        GetComponent<Animator>().SetFloat("HorAimAngle", UpperBodyAim());
        CalculateAimDistance(_input.RightInputAxis);

        _position = this.transform.position;


        //if (_input.LeftInputAxis.magnitude > 0.1f)
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _rotation, _lookAtSmooth);


        //CrosshairObject.position = PositionCrosshair(_input.RightInputAxis, this.transform.rotation);


        if (_input.RightInputAxis.magnitude > 0.3f)
            inputVector = _input.RightInputAxis;

        CrosshairObject.position = Vector3.Lerp(CrosshairObject.position, this.transform.position + (5 * inputVector),
            0.1f);
        _rotation = LookAtCrosshair(CrosshairObject.position);
    }

    Vector3 inputVector;
    public float smoothTime = 1F;
    private float yVelocity = 0.0F;

    private void CalculateAimDistance(Vector3 input)
    {
        var inputMagnitudeSmooth = Mathf.Lerp(0, _crosshairDistance, input.magnitude);
        _crosshairSmoothCalculate = 2 + inputMagnitudeSmooth;
    }

    private Vector3 PositionCrosshair(Vector3 input, Quaternion rotation)
    {
        if (input.magnitude < _deadzone)
        {
            input = Vector3.zero;
            _aimTarget = _position + (_rotation * Vector3.forward) * _crosshairSmoothCalculate;
            _lerp = Vector3.Lerp(_crosshair, _position + (_rotation * Vector3.forward) * _crosshairSmoothCalculate,
                _crosshairSmooth);
        }
        else
        {
            input = input.normalized * ((input.magnitude - _deadzone) / (1 - _deadzone));
            _aimTarget = DistanceKepp(_aimTarget + input, _position, _crosshairSmoothCalculate);
            _lerp = Vector3.Lerp(_crosshair, _aimTarget + (_rotation * Vector3.forward) * _crosshairSmoothCalculate,
                _crosshairSmooth);
        }

        _rotation = LookAtCrosshair(_aimTarget);
        _crosshair = DistanceKepp(_lerp, _position, _crosshairSmoothCalculate);


        return _crosshair;
    }

    private Quaternion LookAtCrosshair(Vector3 target)
    {
        Vector3 direction = (target - _position).normalized;
        return Quaternion.LookRotation(direction);
    }


    public Vector3 DistanceKepp(Vector3 position, Vector3 target, float distance)
    {
        Vector3 offset = position - target;
        if (offset.sqrMagnitude > distance * distance)
            return position + offset.normalized * (distance - offset.magnitude);
        return position;
    }


    private float UpperBodyAim()
    {
        float angle = Vector3.Angle(this.transform.position, CrosshairObject.position);

        Vector3 forwardA = this.transform.rotation * Vector3.forward;
        Vector3 forwardB = (CrosshairObject.position - this.transform.position);

        float angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        float angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

        float signedAngle = Mathf.DeltaAngle(angleA, angleB);

        return signedAngle;
    }
}