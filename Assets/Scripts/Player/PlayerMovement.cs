using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    
    private State _state = State.IDLE;
    private MovementState _movementState = MovementState.STAND;
    private bool _jump = false;
    private Vector2 _directionLooking = new Vector2(0, 0);

    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _state = State.IDLE;
        _movementState = MovementState.STAND;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        switch (_state)
        {
            case State.IDLE:
                _state = State.MOVING;
                break;
            
            case State.MOVING:
                
                // Update direction if can move
                if (Input.GetKey(KeyCode.W))
                {
                    _directionLooking = Vector2.up;
                }
                        
                if (Input.GetKey(KeyCode.A))
                {
                    direction += Vector3.left;
                    _directionLooking = Vector2.left;
                }
                        
                if (Input.GetKey(KeyCode.S))
                {
                    _directionLooking = Vector2.down;
                }
                
                if (Input.GetKey(KeyCode.D))
                {
                    direction += Vector3.right;
                    _directionLooking = Vector2.left;
                }
                direction = direction.normalized;

                if (Input.GetKey(KeyCode.Space))
                {
                    _jump = true;
                }
                
                switch (_movementState)
                {
                    case MovementState.STAND:
                        if (direction != Vector3.zero)
                        {
                            _movementState = MovementState.WALK;
                        }
                        // if touches floor movsta = JUMP
                        
                        break;
                    
                    case MovementState.WALK:
                        if (direction == Vector3.zero)
                        {
                            _movementState = MovementState.STAND;
                        }

                        transform.position += direction * (movementSpeed * Time.deltaTime);
                        break;
                    
                    case MovementState.FALL:
                        // on collision with floor state = stand.
                        _movementState = MovementState.STAND;
                        break;
                    
                    case MovementState.JUMP:
                        _rigidbody2D.AddForce(new Vector2(0, 2 * movementSpeed));
                        _movementState = MovementState.FALL;
                        break;
                    
                }
                
                break;
            
            case State.ATTACKING:
                break;
        }
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

