using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemiesGeneral
{
    [Header("Boss Settings")]
    //Points
    [SerializeField] private Transform restingPoint;
    [SerializeField] private Transform startLPoint;
    [SerializeField] private Transform startRPoint;
    //Movement Speed
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private AudioSource _audioSource;

    private bool isResting=false;
    private void Awake()
    {
        StartCoroutine(attackPattern());
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isResting) 
        {
            goToPoint(restingPoint.position);
            _rigidbody.velocity = Vector2.zero;
        }
        
    }
    private IEnumerator attackPattern()
    {
        int coin = trowCoin();
        isResting = false;

        if (coin== 1)
        {
            meleeAttack();
            yield return new WaitForSeconds(0.5f);
            isResting = true;
            yield return new WaitForSeconds(3);
            
            StartCoroutine(attackPattern());
        }
        else
        {
            
            _animator.SetBool("IsDashing",true);
            startDashAttack(trowCoin());
            yield return new WaitForSeconds(3);
            _animator.SetBool("IsDashing", false);
            isResting = true;
            yield return new WaitForSeconds(3);
            StartCoroutine(attackPattern());
        }
    }


    private int trowCoin()
    {
        return Random.Range(0,2)+1;
    }

    private void goToPoint( Vector2 goPoint)
    {
        transform.position = Vector3.Lerp(transform.position, goPoint, speed*Time.deltaTime);
    }   

    private void meleeAttack()
    {
        lookAtTarget();
        _animator.SetTrigger("MeleeAtack");
        _rigidbody.AddForce((target.position-transform.position).normalized*speed, ForceMode2D.Impulse);
    }
    
    public void lookAtTarget()
    {
        if (target.position.x > transform.position.x)
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
        }
        else
        {
            transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, 0);
        }
    }
    
    private void startDashAttack(int coin)
    {
        _audioSource.Play();
;        if (coin == 1)
        {
            transform.position = startLPoint.position;
            _rigidbody.AddForce(Vector3.right * speed*2, ForceMode2D.Impulse);
            transform.rotation = new Quaternion(0,0,0,0);
            
        }
        else
        {
            transform.position = startRPoint.position;
            _rigidbody.AddForce(Vector3.left * speed*2, ForceMode2D.Impulse);
            transform.rotation = new Quaternion(0, 180, 0, 0);
            
        }
        
    }
}
