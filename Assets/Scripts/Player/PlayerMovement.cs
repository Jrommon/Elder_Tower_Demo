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

    [SerializeField]
    private Attack1 _atack1;
    
    [SerializeField]
    private Attack2 _atack2;
    

    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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

                if (_rigidbody2D.velocity.x < -maxSpeed)
                {
                    _rigidbody2D.AddForce(directionMovement.x > 0 ? directionMovement * movementAcceleration : Vector2.zero);
                }
                else if (_rigidbody2D.velocity.x > maxSpeed)
                {
                    _rigidbody2D.AddForce(directionMovement.x < 0 ? directionMovement * movementAcceleration : Vector2.zero);
                }
                else
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
                        if (Input.GetKey(KeyCode.E))
                            _state = State.ATTACKING;
                        
                        
                        if (_rigidbody2D.velocity.y < 0)
                            _movementState = MovementState.FALL;

                        break;

                    case MovementState.FALL:
                        
                        // Comprobar si esta sobre una superficie, sino se engancha en paredes.
                        if (_rigidbody2D.velocity.y == 0)
                        {
                            _movementState = MovementState.STAND;
                        }
                        
                        break;
                    
                    case MovementState.JUMP:
                        if (_rigidbody2D.velocity.y == 0)
                            _rigidbody2D.velocity += new Vector2(0, 3*movementAcceleration);

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

    private void OnAttack11Enter(Collider2D col)
    {
        throw new NotImplementedException();
    }
    
    private void OnAttack12Enter(Collider2D col)
    {
        throw new NotImplementedException();
    }

    private static Vector2 ManageMovementInputs()
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

