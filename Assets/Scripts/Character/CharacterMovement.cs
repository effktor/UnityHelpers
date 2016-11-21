using System.Collections;
using UnityEngine;

// TODO:
// Add support for movement on other axis, and constrains on axis

public abstract class CharacterMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    protected InputController _input;

    protected virtual void Reset()
    {
        _input = GetComponent<InputController>();
    }

    protected virtual void Start()
    {
        if (_input == null)
            Debug.LogError("Input Controller is missing, assign one in the inspector");
    }

    protected virtual void Update()
    {
    }
}