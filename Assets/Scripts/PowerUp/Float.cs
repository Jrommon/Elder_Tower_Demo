using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class Float : MonoBehaviour
{
    [Header("Floating variables")]
    [SerializeField,Range(1,-1)] private int goingUp;
    [SerializeField] private float maxTime;
    [SerializeField] private float speed;
    
    private float _currentTime;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _currentTime = maxTime;
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Este script tiene un pequeño fallo y es que cuando se incia la aplicación Time.DeltaTime toma el  valor más alto posible
        //haciendo que el primer salto sea más alto de lo que debería y por lo tanto eleva al objeto en posición y positiva.
        //Esto tan solo ocurre cuando el objeto comienza en pantalla ya que si no esta en pantalla el comportamiento se desahibilita.
        
        if (_spriteRenderer.isVisible)
        {
            _currentTime -= Time.deltaTime;
            Vector2 desiredPosition = (Vector2)transform.position +
                                      (Vector2.up * (goingUp * speed * Time.deltaTime));
            
            _rigidbody2D.MovePosition( Vector2.Lerp(transform.position, desiredPosition,1) );

            if (_currentTime <= 0)
            {
                goingUp *= -1;
                _currentTime = maxTime;
            }
        }
    }
}
