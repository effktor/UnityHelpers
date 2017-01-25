using System.Collections;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class PerformAction : MonoBehaviour
{
    [SerializeField] private InputController _input;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private FindCover _findCover;

    [SerializeField] private Animator _animator;

    [SerializeField] private Collider _collider;

    private Vector3 desiredVelocity;

    private void OnEnable()
    {
        _input.OnFireOnePressed += CheckForActionType;
    }

    private void OnDisable()
    {
    }

    private void Reset()
    {
        if (_input == null)
            _input = GetComponent<InputController>();

        if (_findCover == null)
            _findCover = GetComponent<FindCover>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (_collider == null)
            _collider = GetComponent<Collider>();


    }

    private bool isButtonPressed;

    private void CheckForActionType()
    {
        // IS the action a slide or cover move
        // The distance
        Debug.Log("Pressed Button");
        isButtonPressed = true;

        _collider.enabled = false;
        _rigidbody.useGravity = false;



        // Grab a free Sequence to use
        var mySequence = DOTween.Sequence();
        // Add a movement tween at the beginning
        mySequence.Append( _rigidbody.DOMove(_findCover.DebugShowStart.position, CalculateTime(this.transform.position, _findCover.DebugShowStart.position, 10)).SetEase(Ease.InQuad));
        mySequence.AppendCallback(MyCallback);
        // Add a rotation tween as soon as the previous one is finished
        mySequence.Append( _rigidbody.DOMove(_findCover.hit3.position, CalculateTime(_findCover.DebugShowStart.position,_findCover.hit3.position, 10)).SetEase(Ease.InQuad));

        mySequence.AppendCallback(MyCallbackLand);
        mySequence.Append( _rigidbody.DOMove(_findCover.DebugShowEnd.position, CalculateTime(_findCover.hit3.position, _findCover.DebugShowEnd.position, 10)).SetEase(Ease.InQuad));


        _animator.SetTrigger("Sprint");
        mySequence.Play().SetUpdate(false);



    }

    private float CalculateTime(Vector3 start, Vector3 end, float Speed)
    {
        return Vector3.Distance(start, end) / Speed;
    }

    private void MyCallback()
    {
        Debug.Log("TRIGGER COVER JUMP");
        _animator.SetTrigger("Jump");

    }

    private void MyCallbackLand()
    {
        Debug.Log("TRIGGER COVER JUMP");
        _animator.SetBool("JumpLand", true);

    }

    private void DoCoverJump(Vector3 FirstTarget, Vector3 SecondTarget, Vector3 ThirdTarget)
    {
        desiredVelocity = (_findCover.DebugShowEnd.position - this.transform.position).normalized * 1;
    }

    private void DoSlide()
    {
    }

    private bool IsCloseToPosition(Vector3 pos, Vector3 target)
    {
        return Vector3.Distance(pos, target) < 0.1f;
    }

    private void Update()
    {
//        if(IsCloseToPosition(this.transform.position, _findCover.DebugShowStart.position))
//            desiredVelocity = (_findCover.DebugShowStart.position - this.transform.position).normalized * 10;

    }

    private void FixedUpdate()
    {
//        if (isButtonPressed)
//        {
//            _rigidbody.velocity = desiredVelocity;
//            //Debug.Log("Move Object");
//        }
    }

//    private void MovePlayerTowards(Vector3 target)
//    {
//        if (isObstacle)
//        {
//            float sqrMag = (target - this.transform.position).sqrMagnitude;
//            Vector3 velInput = desiredVelocity * 8;
//
//            if (hasStartOstacble)
//            {
//                sqrMagStart = (vizPosStart - this.transform.position).sqrMagnitude;
//            }
//
//            if (hasEndObstacle)
//            {
//            }
//
//            if (RayforFloor() && !isObstacleAction)
//            {
//                isObstacleAction = true;
//            }
//
//            if (sqrMagStart < 0.5f * 0.5f && !hasDoneObstacle && hasStartOstacble)
//            {
//                _animator.SetTrigger("ObstacleJump");
//                directionalVector = (vizPos - this.transform.position).normalized * speed;
//                desiredVelocity = directionalVector;
//                hasDoneObstacle = true;
//            }
//
//            if (Vector3.Distance(this.transform.position, targetLocation) < 0.1f)
//            {
//
//                desiredVelocity = Vector3.zero;
//                isObstacle = false;
//
//                _collider.enabled = true;
//                _rigidBody.useGravity = true;
//                isObstacleAction = false;
//                _player.PlayerState = Player.State.Alive;
//                hasDoneObstacle = false;
//                _renderer.enabled = false;
//
//                //}
//
//                //_animator.SetTrigger("ObstacleEnd");
//
//                if (hasStartOstacble)
//                {
//                }
//            }
//
//            // make sure you update the lastSqrMag
//            lastSqrMag = sqrMag;
//        }
//    }
}