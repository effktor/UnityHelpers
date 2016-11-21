using System.Collections;
using UnityEngine;

// Class for handling input from controllers

public abstract class InputController : MonoBehaviour
{
    protected Vector3 _lefttAxis;
    protected Vector3 _rightAxis;

    /// <summary>
    /// Returns the current left input Axis.
    /// </summary>
    public Vector3 LeftInputAxis
    {
        get { return _lefttAxis; }
    }

    /// <summary>
    /// Returns the current right input Axis.
    /// </summary>
    public Vector3 RightInputAxis
    {
        get { return _rightAxis; }
    }

    public bool IsFirePrimary
    {
        get;
        protected set;
    }

    public bool IsFireSecondary
    {
        get;
        protected set;
    }

    protected virtual void Update()
    {
    }
}