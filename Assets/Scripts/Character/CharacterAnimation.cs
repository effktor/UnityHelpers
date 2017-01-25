using System.Collections;
using UnityEngine;


public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private InputController _input;

    [SerializeField]
    private Animator _animator;

    private void Reset()
    {
        _input = GetComponent<InputController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("InputMagnitude", _input.LeftInputAxis.magnitude);

        Vector3 directionLocal = transform.InverseTransformDirection(_input.LeftInputAxis);

        float angle = Mathf.Atan2(directionLocal.x, directionLocal.z) * Mathf.Rad2Deg;


//        _animator.SetFloat("X", directionLocal.x);
//        _animator.SetFloat("Z", directionLocal.z);

        if (_input.LeftInputAxis.magnitude < 0.1f)
        {
            _animator.SetBool("IsStopRU", true);
        }
        else
        {
            _animator.SetBool("IsStopRU", false);
            _animator.SetFloat("WalkStopAngle", angle);
            _animator.SetFloat("WalkStartAngle", angle);
        }
    }
}