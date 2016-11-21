using System.Collections;
using UnityEngine;

// Class for handling input from controllers

public class InputController : MonoBehaviour
{
    private Vector3 _lefttAxisX;
    private Vector3 _leftAxisY;
    private Vector3 _rightAxisX;
    private Vector3 _rightAxisY;

    /// <summary>
    /// Returns the current left horizontal input Axis.
    /// </summary>
    public Vector3 LeftAxisX
    {
        get { return _lefttAxisX; }
    }

    /// <summary>
    /// Returns the current left vertical input Axis.
    /// </summary>
    public Vector3 LeftAxisY
    {
        get { return _leftAxisY; }
    }

    /// <summary>
    /// Returns the current right horizontal input Axis.
    /// </summary>
    public Vector3 RightAxisX
    {
        get { return _rightAxisX; }
    }

    /// <summary>
    /// Returns the current right vertical input Axis.
    /// </summary>
    public Vector3 RightAxisY
    {
        get { return _rightAxisY; }
    }

    public bool IsFirePrimary
    {
        get;
        private set;
    }

    public bool IsFireSecondary
    {
        get;
        private set;
    }

    private void Update()
    {
    }

    private void UpdateButtons()
    {
        IsFirePrimary = Input.GetButtonDown("Fire1");
        IsFireSecondary = Input.GetButtonDown("Fire2");
    }
}