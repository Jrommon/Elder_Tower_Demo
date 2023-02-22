using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementAcceleration = 2, maxSpeed = 5;
    
    
    private State _state = State.IDLE;
    private MovementState _movementState = MovementState.STAND;
    private Vector2 _directionLooking = new Vector2(0, 0);
    private bool _doubleJump;
    private bool _jumpPowerUp;

    public bool JumpPowerUp
    {
        get => _jumpPowerUp;
        set => _jumpPowerUp = true;
    }

    [SerializeField]
    private Attack1 _atack1;
    
    [SerializeField]
    private Attack2 _atack2;
    

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector3 _scale;

    private readonly int WalkingAnimatorParameter = Animator.StringToHash("Walking");
    private readonly int AttackTypeAnimatorParameter = Animator.StringToHash("AttackType");
    private readonly int IsDeadAnimatorParameter = Animator.StringToHash("IsDead");
    private readonly int HitAnimatorParameter = Animator.StringToHash("Hit");
    private readonly int JumpingAnimatorParameter = Animator.StringToHash("Jumping");
    private readonly int AttackAnimatorParameter = Animator.StringToHash("Attack");




    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;

    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D.isKinematic = false;

        // _atack1.OnAttack11 += OnAttack11Enter;
        // _atack1.OnAttack12 += OnAttack12Enter;
        
        
        _state = State.IDLE;
        _movementState = MovementState.STAND;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directionMovement = Vector3.zero;
        _animator.SetBool(WalkingAnimatorParameter, false);

        Debug.Log(_state);
        Debug.Log(_movementState);

        switch (_state)
        {
            case State.IDLE:
                _rigidbody2D.isKinematic = true;

                _state = State.MOVING;
                break;
            
            case State.MOVING:
                Debug.Log(_movementState);
                _rigidbody2D.isKinematic = false;
                
                // Update direction if can move
                directionMovement = ManageMovementInputs();
                
                directionMovement = directionMovement.normalized;
                
                if (directionMovement.x != 0)
                {
                    if (_rigidbody2D.velocity.y == 0)
                        _animator.SetBool(WalkingAnimatorParameter, true);
                    
                }
                else
                {
                    _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                }

                if (Math.Abs(_rigidbody2D.velocity.x) < maxSpeed)
                {
                    _rigidbody2D.AddForce(directionMovement * movementAcceleration);

                }


                if (Input.GetKey(KeyCode.Space))
                {
                    if (_movementState is MovementState.WALK or MovementState.STAND)
                    {
                        _movementState = MovementState.JUMP;
                    }
                }
                
                switch (_movementState)
                {
                    case MovementState.STAND:
                        _doubleJump = _jumpPowerUp;
                        if (Input.GetKey(KeyCode.E))
                            _state = State.ATTACKING;
                        
                        
                        if (_rigidbody2D.velocity.y < 0)
                        {
                            _movementState = MovementState.FALL;
                        }
                        break;

                    case MovementState.FALL:
                        _animator.SetBool(JumpingAnimatorParameter, true);

                        // Comprobar si esta sobre una superficie, sino se engancha en paredes.
                        if (_rigidbody2D.velocity.y == 0)
                        {
                            _movementState = MovementState.STAND;
                            _animator.SetBool(JumpingAnimatorParameter, false);

                        }

                        _doubleJump = _doubleJump ? true : false;
                        
                        
                        break;
                    
                    case MovementState.JUMP:
                        _animator.SetBool(JumpingAnimatorParameter, true);
                        _rigidbody2D.velocity += new Vector2(0, 1.5f * movementAcceleration);
                        _movementState = MovementState.FALL;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                break;
            
            case State.ATTACKING:
                
                break;
        }
        Debug.Log(_state);
        Debug.Log(_movementState);
    }

    private void Fire()
    {
        // GameObject go = Instantiate(prefabffire);
        // Fire goScript = go.GetComponent<Fire>();
        // goScript.Position = _fireLaunchPoint.position;
        //
        // if (transform.localScale.x < 0)
        // {
        //     goScript.Direction = 2 * speed * Vector2.left;
        // }
        // else
        // {
        //     goScript.Direction = 2 * speed * Vector2.right;
        //
        // }
        // TODO
    }

    private Vector2 ManageMovementInputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return Vector2.up;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            return Vector2.down;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            return Vector2.left;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            return Vector2.right;
        }

        return Vector2.zero;
    }
    
    private void LookRight(bool right)
    {
        transform.localScale = right ? _scale : new Vector3(-_scale.x, _scale.y, _scale.z);
    }

    public enum State
    {
        IDLE,
        MOVING,
        ATTACKING
    }
    
    public enum MovementState
    {
        STAND,
        WALK,
        JUMP,
        FALL
    }
}

