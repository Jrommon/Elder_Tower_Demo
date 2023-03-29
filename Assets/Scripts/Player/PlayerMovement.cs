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
    [SerializeField] private Transform respawnPosition;
    [SerializeField] private int magicSpeed;
    
    [Header("Indicators")]
    [SerializeField] private SpriteRenderer magicReadyIndicator;
    [SerializeField] private SpriteRenderer weaponsReadyIndicator;
    [SerializeField] private Sprite sword, magic;
    
    [SerializeField] private float speed = 4, jumpForce = 5;

    [Header("Life")] 
    [SerializeField] private int health = 3;
    
    private State _state = State.IDLE;
    private MovementState _movementState = MovementState.STAND;
    private Vector2 _directionLooking = Vector2.right;
    [SerializeField]private bool _jumpPowerUp;
    [SerializeField] private bool _doubleJump;
    private int jumpNumber = 0;
    private bool _magicReady = true;
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
    private readonly int _fallingAnimatorParameter = Animator.StringToHash("Falling");
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
        _animator.SetBool(_fallingAnimatorParameter, false);
        _animator.SetBool(_hitAnimatorParameter, false);
        _animator.SetBool(_isDeadAnimatorParameter, false);
        _animator.SetInteger(_attackTypeAnimatorParameter, 0);

        magicReadyIndicator.enabled = _magicReady;
        weaponsReadyIndicator.sprite = _isMele ? sword : magic;
        
        AttackType attackType = AttackInput();
        if (Input.GetMouseButtonDown(2))
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
                if(health >0)
                {
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
                        if (_movementState is MovementState.WALK or MovementState.STAND)
                            _movementState = MovementState.JUMP;

                        if ((_movementState is MovementState.FALL or MovementState.JUMP) && _jumpPowerUp && jumpNumber <= 1)
                        {
                            if (jumpNumber == 1)
                                _doubleJump = true;
                            
                            _movementState = MovementState.JUMP;
                        }

                    }

                }
                
                
                switch (_movementState)
                {
                    case MovementState.STAND:
                        _doubleJump = false;
                        jumpNumber = 0;
                        if (Input.GetKey(KeyCode.E))
                            _state = State.ATTACKING;
                        
                        
                        if (_rigidbody2D.velocity.y < 0)
                            _movementState = MovementState.FALL;
                        
                        break;

                    case MovementState.FALL:
                        _animator.SetBool(_fallingAnimatorParameter, true);

                        // Comprobar si esta sobre una superficie, si no se engancha en paredes.
                        if (_rigidbody2D.velocity.y == 0)
                            _movementState = MovementState.STAND;
                        
                        break;
                    
                    case MovementState.JUMP:
                        _animator.SetBool(_jumpingAnimatorParameter, true);
                        switch (jumpNumber)
                        {
                            case 0:
                                jumpNumber++;
                                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                                _rigidbody2D.velocity += new Vector2(0, jumpForce);
                                break;
                            
                            case 1 when _doubleJump:
                                jumpNumber++;
                                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                                _rigidbody2D.velocity += new Vector2(0, jumpForce);
                                break;
                        }

                        if (_rigidbody2D.velocity.y <= 0)
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
                            if (_magicReady)
                            {
                                FireballAttack();
                            }
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

                            if (_magicReady)
                            {
                                ThunderAttack();
                            }

                        }
                        break;
                    
                    case AttackType.SECONDARY:
                        if (_isMele)
                        {
                            _animator.SetInteger(_attackTypeAnimatorParameter, 3);
                            if (_magicReady)
                            {
                                FireballAttack();
                            }
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Void"))
        {
            substractHealth(health);
        }
        else if (col.collider.CompareTag("Enemy"))
        {
            substractHealth(1);
        }
    }

    private void substractHealth(int healthTaken)
    {
        health -= healthTaken;

        /*_animator.SetBool(_hitAnimatorParameter, true);
        _animator.SetInteger(_attackTypeAnimatorParameter, 99);
        
        if (health <= 0)
        {
            _animator.SetBool(_isDeadAnimatorParameter, true);
        }*/

        if (health <= 0)
        {
            _animator.Play("Player_Dead");
        }
        else
        {
            _animator.Play("Player_Take_Hit");
        }
    }

    private void Respawn()
    {
        transform.position = respawnPosition.position;
        health = 3;
    }

    private void FireballAttack()
    {
        var fire = Instantiate(
            fireBallAttack,
            new Vector3(
                magicAttackPosition.position.x,
                magicAttackPosition.position.y,
                0),
            Quaternion.identity);
        MagicAttack magicAttack = fire.GetComponent<MagicAttack>();
        magicAttack.Movement = magicSpeed * _directionLooking;
        if (_directionLooking.x < 0)
        {
            magicAttack.Flip = true;
        }

        _magicReady = false;
    }
    
    private void ThunderAttack()
    {
        var thunder = Instantiate(
            thunderAttack,
            new Vector3(
                magicAttackPosition.position.x,
                magicAttackPosition.position.y,
                0),
            Quaternion.identity);
        MagicAttack magicAttack = thunder.GetComponent<MagicAttack>();
        magicAttack.Movement = magicSpeed * _directionLooking;
        if (_directionLooking.x < 0)
        {
            magicAttack.Flip = true;
        }

        _magicReady = false;
    }

    private void ToggleAttack() => _isMele = !_isMele;
    
    private void EnableMeleNormalAttackHitbox1() => _meleNormalAttackHitbox1.SetActive(true);
    private void EnableMeleNormalAttackHitbox2() => _meleNormalAttackHitbox2.SetActive(true);
    private void EnableHeavyAttackHitbox1() => _meleHeavyAttackHitbox1.SetActive(true);
    private void EnableHeavyAttackHitbox2() => _meleHeavyAttackHitbox2.SetActive(true);
    public void DisableMeleAttackHitbox()
    {
        _meleNormalAttackHitbox1.SetActive(false);
        _meleNormalAttackHitbox2.SetActive(false);
        _meleHeavyAttackHitbox1.SetActive(false);
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
        if (Input.GetMouseButton(0))
            return AttackType.NORMAL;
        

        if (Input.GetMouseButton(1))
            return AttackType.HEAVY;

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Fireball");
            return AttackType.SECONDARY;
        }

        

        return AttackType.NONE;
    }
    
    public void EnableMagic()
    {
        _magicReady = true;
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

