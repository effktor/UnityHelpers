using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementCharacteController : CharacterMovement
{
    [SerializeField]
    private CharacterController _controller;

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
        _controller.Move(_input.LeftInputAxis);
    }
}