using System.Collections;
using UnityEngine;


public class MovementLocomotion : CharacterMovement
{
    [SerializeField] private Animator _animator;

    [SerializeField] protected Rigidbody _rigidBody;

    [Header("Settings")] [SerializeField] private float _speed = 1f;

    [SerializeField] [Range(0, 1)] private float _smoothSpeed = 1f;

    [SerializeField] private float _gravity;

    private Vector3 _moveInput;
    private Vector3 _signedMoveDirection;

    private float xVelocity = 0f, yVelocity = 0f;
    private float xVelocitySpeed = 0f, yVelocitySpeed = 0f;

    [Range(0f, 1f)] [SerializeField] protected float smoothTime = 0.3f;

    [Range(0f, 1f)] [SerializeField] protected float smoothTimeVelocity = 0.3f;

    [Range(0f, 10f)] [SerializeField] protected float moveSpeed = 4f;

    protected Vector3 _moveBlend, _velocity;

    private Vector3 lastLeftStickInputAxis;
    private Vector3 leftStickInputAxis;

    private Vector3 fixedDeltaPosition;
    private Quaternion fixedDeltaRotation;

    [Range(1f, 4f)] [SerializeField] protected float gravityMultiplier = 2f;

    protected override void Reset()
    {
        base.Reset();

        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        UpdateMove();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MoveFixed();
    }


    private Vector3 refInput;

    private void UpdateMove()
    {
        refInput = this.transform.InverseTransformDirection(_input.LeftInputAxis.x, 0, _input.LeftInputAxis.z);


        _signedMoveDirection =
            this.transform.InverseTransformDirection(_input.RightInputAxis.x, 0, _input.RightInputAxis.z) * 4;

        leftStickInputAxis = new Vector2(refInput.x, refInput.z);

        leftStickInputAxis =
            new Vector2(Mathf.SmoothDamp(lastLeftStickInputAxis.x, leftStickInputAxis.x, ref xVelocity, smoothTime),
                Mathf.SmoothDamp(lastLeftStickInputAxis.y, leftStickInputAxis.y, ref yVelocity, smoothTime));

        _animator.SetFloat("X", leftStickInputAxis.x);

        _animator.SetFloat("Z", leftStickInputAxis.y);

        lastLeftStickInputAxis = leftStickInputAxis;
    }

    private Vector3 smoothVelocityLastFrame;

    private void MoveFixed()
    {
        var smoothVelocity =
            new Vector3(
                Mathf.SmoothDamp(smoothVelocityLastFrame.x, _input.LeftInputAxis.x, ref xVelocitySpeed,
                    smoothTimeVelocity), 0,
                Mathf.SmoothDamp(smoothVelocityLastFrame.z, _input.LeftInputAxis.z, ref yVelocitySpeed,
                    smoothTimeVelocity));

        var velocity = smoothVelocity * moveSpeed;


        _rigidBody.velocity = velocity;


        smoothVelocityLastFrame = smoothVelocity;
    }
}