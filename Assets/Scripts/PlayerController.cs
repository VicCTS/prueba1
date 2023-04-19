using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable para la cantidad de vidas de Mario
    int playerHealth = 3;
    //Variable para la velocidad de movimiento
    public float playerSpeed = 5.5f;
    //Variable para la fuerza del salto
    public float jumpForce = 3f;

    //Variable para almacenar un texto
    string texto = "Hello World";

    //Variable para acceder al SpriteRenderer
    private SpriteRenderer spriteRenderer;
    //Variable para acceder al RigidBody2D
    private Rigidbody2D rBody;
    //Variable para acceder al GroundSensor
    private GroundSensor sensor;
    //Variable para acceder al Animator
    public Animator anim;

    //Variable para almacenar el input de movimiento
    float horizontal;

    GameManager gameManager;
    SFXManager sfxManager;

    //Variable para el prefab
    public GameObject bulletPrefab;
    //variable para la posicion desde donde se dispara el prefab
    public Transform bulletSpawn;

    public Transform attackHitBox;
    public float attackRange;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Asignamos la variables del SpriteRender con el componente que tiene este objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Asignamos la variable del Rigidbody2D con el componente que tiene este objeto
        rBody = GetComponent<Rigidbody2D>();
        //Buscamos un Objeto por su nombre, cojemos el Componente GroundSensor de este objeto y lo asignamos a la variable
        sensor = GameObject.Find("GroundSensor").GetComponent<GroundSensor>();
        //Asignamos la variable del Animator con el componente que tiene este objeto
        anim = GetComponent<Animator>();
        //Buscamos el objeto del GameManager y lo asignamos a la variable del GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();

        //Le damos un valor de 10 a la variable
        playerHealth = 10;
        //Hacemos un debug con el texto de la variable "texto"
        Debug.Log(texto);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameOver == false)
        {
            horizontal = Input.GetAxis("Horizontal");

            //transform.position += new Vector3(horizontal, 0, 0) * playerSpeed * Time.deltaTime; 

            if(horizontal < 0)
            {
                //spriteRenderer.flipX = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                anim.SetBool("IsRunning", true);
            }
            else if(horizontal > 0)
            {
                //spriteRenderer.flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }

            if(Input.GetButtonDown("Jump") && sensor.isGrounded)
            {
                rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("IsJumpinng", true);
            }

            if(Input.GetKeyDown(KeyCode.F) && gameManager.canShoot)
            {
                Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                Attack();
            }
        }    
        
    }

    void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * playerSpeed, rBody.velocity.y);
    }

    void Attack()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackHitBox.position, attackRange, enemyLayer);

        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            Destroy(enemiesInRange[i].gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Coin")
        {
            gameManager.AddCoin();
            sfxManager.CoinSound();
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.tag == "PowerUp")
        {
            gameManager.canShoot = true;
            Destroy(collider.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBox.position, attackRange);
    }
}
