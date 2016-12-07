using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementCharacterController : CharacterMovement
{
    [SerializeField]
    private CharacterController _controller;

    [Header("Settings")]
    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    [Range(0, 1)]
    private float _smoothSpeed = 1f;

    [SerializeField]
    private float _gravity;

    private Vector3 _moveInput;

    protected override void Reset()
    {
        base.Reset();
        _controller = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();

        if (_controller == null)
            Debug.LogError("Character Controller is missing, assign one in the inspector");
    }

    protected override void Update()
    {
        base.Update();

        _moveInput = Vector3.Lerp(_controller.velocity, _input.LeftInputAxis * _speed, _smoothSpeed);

        _moveInput.y += _gravity * Time.deltaTime;

        _controller.Move(_moveInput * Time.deltaTime);
    }
}