using System.Collections;
using UnityEngine;

public class InputUnity : InputController
{
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
        _lefttAxis.x = Input.GetAxis("Horizontal");
        _lefttAxis.z = Input.GetAxis("Vertical");
    }
}