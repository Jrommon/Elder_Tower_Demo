using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject fireBallAttack, thunderAttack;
    [SerializeField] private Transform magicAttackPosition;

    [SerializeField]
    private float speed = 4, jumpForce = 5;

    private State _state = State.IDLE;
    private MovementState _movementState = MovementState.STAND;
    private Vector2 _directionLooking = Vector2.right;
    private bool _jumpPowerUp;
    private bool _doubleJump;
    private bool _isMele = true;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector3 _scale;
    
    private GameObject _meleNormalAttackHitbox1;
    private GameObject _meleNormalAttackHitbox2;

    private GameObject _meleHeavyAttackHitbox1;
    private GameObject _meleHeavyAttackHitbox2;


    private readonly int _walkingAnimatorParameter = Animator.StringToHash("Walking");
    private readonly int _attackTypeAnimatorParameter = Animator.StringToHash("AttackType");
    private readonly int _isDeadAnimatorParameter = Animator.StringToHash("IsDead");
    private readonly int _hitAnimatorParameter = Animator.StringToHash("Hit");
    private readonly int _jumpingAnimatorParameter = Animator.StringToHash("Jumping");
    private readonly int _attackAnimatorParameter = Animator.StringToHash("Attack");

    public bool JumpPowerUp
    {
        get => _jumpPowerUp;
        set => _jumpPowerUp = true;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
        
        _meleNormalAttackHitbox1 = transform.GetChild(0).gameObject;
        _meleNormalAttackHitbox2 = transform.GetChild(1).gameObject;
        _meleHeavyAttackHitbox1 = transform.GetChild(2).gameObject;
        _meleHeavyAttackHitbox2 = transform.GetChild(3).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D.isKinematic = false;

        _state = State.IDLE;
        _movementState = MovementState.STAND;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directionMovement = Vector3.zero;
        _animator.SetBool(_walkingAnimatorParameter, false);
        _animator.SetBool(_jumpingAnimatorParameter, false);
        _animator.SetBool(_hitAnimatorParameter, false);
        _animator.SetBool(_isDeadAnimatorParameter, false);
        _animator.SetInteger(_attackTypeAnimatorParameter, 0);

        // _animator.SetTrigger(_attackAnimatorParameter);

        // _animator.SetInteger(_attackTypeAnimatorParameter, 0);

        AttackType attackType = AttackInput();
        if (Input.GetKeyDown(KeyCode.T)) 
                ToggleAttack();

        switch (_state)
        {
            case State.IDLE:
                _rigidbody2D.isKinematic = true;

                _state = State.MOVING;
                break;
            
            case State.MOVING:
                _rigidbody2D.isKinematic = false;

                _state = attackType switch
                {
                    AttackType.NONE => State.MOVING,
                    AttackType.NORMAL or AttackType.HEAVY or AttackType.SECONDARY => State.ATTACKING,
                    _ => _state
                };



                // Update direction if can move
                directionMovement = ManageMovementInputs().normalized;
                
                if (directionMovement.x != 0)
                {
                    // Walk animation
                    _animator.SetBool(_walkingAnimatorParameter, _rigidbody2D.velocity.y == 0);
                    
                    // Movement
                    transform.Translate(new Vector2(directionMovement.x, 0) * (speed * Time.deltaTime));
                    LookRight(directionMovement.x > 0);
                }
                else
                {
                    _animator.SetBool(_walkingAnimatorParameter, false);
                    _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                }
                
                // Jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_movementState is MovementState.WALK or MovementState.STAND || _doubleJump)
                        _movementState = MovementState.JUMP;
                    
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
                        _animator.SetBool(_jumpingAnimatorParameter, true);

                        // Comprobar si esta sobre una superficie, sino se engancha en paredes.
                        if (_rigidbody2D.velocity.y == 0)
                        {
                            _movementState = MovementState.STAND;

                        }

                        break;
                    
                    case MovementState.JUMP:
                        _animator.SetBool(_jumpingAnimatorParameter, true);
                        _rigidbody2D.velocity += new Vector2(0, jumpForce);
                        _movementState = MovementState.FALL;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            
            case State.ATTACKING:
                _animator.SetTrigger(_attackAnimatorParameter);

                switch (attackType)
                {
                    case AttackType.NORMAL:
                        if (_isMele)
                        {
                            _animator.SetInteger(_attackTypeAnimatorParameter, 1);
                        }
                        else
                        {
                            _animator.SetInteger(_attackTypeAnimatorParameter, 3);
                            var fire = Instantiate(
                                fireBallAttack,
                                new Vector3(
                                    magicAttackPosition.localPosition.x * _directionLooking.x,
                                    magicAttackPosition.localPosition.y,
                                    0),
                                Quaternion.identity);
                            MagicAttack magicAttack = fire.GetComponent<MagicAttack>();
                            magicAttack.Movement = 5 * _directionLooking;
                        }
                        break;
                    
                    case AttackType.HEAVY:
                        if (_isMele)
                        {
                            _animator.SetInteger(_attackTypeAnimatorParameter, 2);
                        }
                        else
                        {
                            _animator.SetInteger(_attackTypeAnimatorParameter, 3);
                            
                            var fire = Instantiate(
                                thunderAttack,
                                new Vector3(
                                    magicAttackPosition.localPosition.x * _directionLooking.x,
                                    magicAttackPosition.localPosition.y,
                                    0),
                                Quaternion.identity);
                            MagicAttack magicAttack = fire.GetComponent<MagicAttack>();
                            magicAttack.Movement = 5 * _directionLooking;

                        }
                        break;
                    
                    case AttackType.SECONDARY:
                        if (_isMele)
                        {
                            _animator.SetInteger(_attackTypeAnimatorParameter, 3);
                            var fire = Instantiate(
                                fireBallAttack,
                                new Vector3(
                                    magicAttackPosition.localPosition.x * _directionLooking.x,
                                    magicAttackPosition.localPosition.y,
                                0),
                                Quaternion.identity);
                            MagicAttack magicAttack = fire.GetComponent<MagicAttack>();
                            magicAttack.Movement = 5 * _directionLooking;
                        }
                        else
                        {
                            _animator.SetInteger(_attackTypeAnimatorParameter, 1);
                        }
                        break;
                    
                    default:
                        _state = State.MOVING;
                        break;
                }
                _state = State.MOVING;

                break;
        }
    }

    private void ToggleAttack() => _isMele = !_isMele;
    
    private void EnableMeleNormalAttackHitbox1() => _meleNormalAttackHitbox1.SetActive(true);
    private void EnableMeleNormalAttackHitbox2() => _meleNormalAttackHitbox2.SetActive(true);
    private void EnableHeavyAttackHitbox1() => _meleNormalAttackHitbox1.SetActive(true);
    private void EnableHeavyAttackHitbox2() => _meleHeavyAttackHitbox2.SetActive(true);
    public void DisableMeleAttackHitbox()
    {
        _meleNormalAttackHitbox1.SetActive(false);
        _meleNormalAttackHitbox2.SetActive(false);
        _meleHeavyAttackHitbox2.SetActive(false);
        _meleHeavyAttackHitbox2.SetActive(false);
    }

    private Vector2 ManageMovementInputs()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

        return direction;
    }

    private AttackType AttackInput()
    {
        if (Input.GetKey(KeyCode.E))
            return AttackType.NORMAL;
        

        if (Input.GetKey(KeyCode.R))
            return AttackType.HEAVY;

        if (Input.GetKey(KeyCode.F))
            return AttackType.SECONDARY;

        return AttackType.NONE;
    }
    
    private void LookRight(bool right)
    {
        transform.localScale = right ? _scale : new Vector3(-_scale.x, _scale.y, _scale.z);
        _directionLooking = right ? Vector2.right : Vector2.left;
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
    
    private enum AttackType
    {
        NONE,
        NORMAL,
        HEAVY,
        SECONDARY
    }
}

