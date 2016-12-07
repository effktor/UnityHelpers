using System.Collections;
using UnityEngine;
using Rewired;

public class InputRewired : InputController
{

    private Player _playerInput;

    protected override void Start()
    {
        base.Start();
        _playerInput = ReInput.players.GetPlayer(0);
    }

    protected override void Update()
    {
        base.Update();

        UpdateButtons();
        UpdateAxis();
    }

    private void UpdateButtons()
    {
        IsFirePrimary = Input.GetButtonDown("Fire1");
        IsFireSecondary = Input.GetButtonDown("Fire2");
    }

    private void UpdateAxis()
    {
        _lefttAxis.x = _playerInput.GetAxis("MoveX");
        _lefttAxis.z = _playerInput.GetAxis("MoveY");

        _rightAxis.x = _playerInput.GetAxis("AimX");
        _rightAxis.z = _playerInput.GetAxis("AimY");
    }




}