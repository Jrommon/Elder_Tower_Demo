using System;
using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = false;
        
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
                _state = State.MOVING;
                break;
            
            case State.MOVING:
                Debug.Log(_movementState);
                
                // Update direction if can move
                if (Input.GetKey(KeyCode.W))
                {
                    _directionLooking = Vector2.up;
                }
                        
                if (Input.GetKey(KeyCode.A))
                {
                    directionMovement += Vector2.left;
                    _directionLooking = Vector2.left;
                }
                        
                if (Input.GetKey(KeyCode.S))
                {
                    _directionLooking = Vector2.down;
                }
                
                if (Input.GetKey(KeyCode.D))
                {
                    directionMovement += Vector2.right;
                    _directionLooking = Vector2.right;
                }
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
                        // Debug.Log("Movement state: standing");
                        // Debug.Log(directionMovement);
                        // Debug.Log(_directionLooking);
                        if (_rigidbody2D.velocity.y < 0)
                            _movementState = MovementState.FALL;


                        // if touches floor movsta = JUMP
                        
                        break;

                    case MovementState.FALL:
                        if (_rigidbody2D.velocity.y == 0)
                        {
                            _movementState = MovementState.STAND;
                        }
                        
                        break;
                    
                    case MovementState.JUMP:
                        Debug.Log("Hello");
                        if (_rigidbody2D.velocity.y == 0)
                            _rigidbody2D.velocity += new Vector2(0, 3*movementAcceleration);
                            // _rigidbody2D.AddForce(new Vector2(0, 50 * movementAcceleration)); 
                        
                        
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

    
    void LookAt(Vector2 lookDirection)
    {

    }

    void UpdateActions()
    {
        
    }

    void UpdateState()
    {
        
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

