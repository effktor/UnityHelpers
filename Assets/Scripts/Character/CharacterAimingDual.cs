using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


public class CharacterAimingDual : CharacterAiming
{
    public Transform CrosshairObject;

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


    protected override void Start()
    {
        base.Start();

        _aimTarget = this.transform.forward * _crosshairDistance;
        _rotation = this.transform.rotation;
    }

    protected override void Update()
    {
        base.Update();

        _position = this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _rotation, _lookAtSmooth);
        CrosshairObject.position = PositionCrosshair(_input.RightInputAxis, this.transform.rotation);
    }


    private Vector3 PositionCrosshair(Vector3 input, Quaternion rotation)
    {
        if (input.magnitude < _deadzone)
        {
            input = Vector3.zero;
            _aimTarget = _position + (_rotation * Vector3.forward) * _crosshairDistance;
            _lerp = Vector3.Lerp(_crosshair, _position + (_rotation * Vector3.forward) * _crosshairDistance,
                _crosshairSmooth);
        }
        else
        {
            input = input.normalized * ((input.magnitude - _deadzone) / (1 - _deadzone));
            _aimTarget = DistanceKepp(_position + input, _position, _crosshairDistance);
            _lerp = Vector3.Lerp(_crosshair, _aimTarget + (_rotation * Vector3.forward) * _crosshairDistance,
                _crosshairSmooth);
        }

        _rotation = LookAtCrosshair(_aimTarget);
        _crosshair = DistanceKepp(_lerp, _position, _crosshairDistance);

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
}