using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    public AudioSource audioSource;
    public AudioClip attack1Sound;
    public AudioClip attack2Sound;
   
    public AudioClip damageSound;
    public AudioClip dashSound;

    public Transform flooCollider;
   public Transform skin;
   public int comboNum;
   public float comboTime;
   public float dashTime;
    public string currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        currentLevel = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentLevel.Equals(SceneManager.GetActiveScene().name)) 
        {

            currentLevel = SceneManager.GetActiveScene().name;
            transform.position = GameObject.Find("Spawn").transform.position;
        
        
        }

        if(GetComponent<Character>().life <= 0)
        {
            rb.simulated = false;
            this.enabled = false;
        
        }
        //Dash do Player
        dashTime = dashTime + Time.deltaTime;
        if(Input.GetButtonDown("Fire2")&& dashTime > 1)
        {

            audioSource.PlayOneShot(dashSound, 0.5f);  //som do dash



            dashTime = 0;
            skin.GetComponent<Animator>().Play("PlayerDash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 680,0));//adiciona força no dash
        }

        //Combo do Player
        comboTime = comboTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire1")&& comboTime > 0.5f)
        {

            comboNum++;
            if (comboNum > 2) {

                comboNum = 1;
            }

            comboTime = 0;

            skin.GetComponent<Animator>().Play("PlayerAttack" + comboNum, -1);

            if (comboNum == 1) 
            {

                audioSource.PlayOneShot(attack1Sound, 0.5f);

            }
            if (comboNum == 2)
            {

                audioSource.PlayOneShot(attack2Sound, 0.5f);

            }

        }
        if (comboTime >= 1) 
        {

            comboNum = 0;
        
        }

        //Jump do Player
        if (Input.GetButtonDown("Jump") && flooCollider.GetComponent<FloorCollider>().canJump ==true) {

            skin.GetComponent<Animator>().Play("PlayerJump",-1);
            rb.velocity = Vector2.zero;
            flooCollider.GetComponent<FloorCollider>().canJump = false;
            rb.AddForce(new Vector2(0, 960));//adiciona força no pulo.
        
        
        
        }
        //Velocidade do Player
        vel = new Vector2(Input.GetAxisRaw("Horizontal")*8.5f, rb.velocity.y);//controla a velocidade do player

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);

            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }

    }


    private void FixedUpdate()
    {

        if (dashTime > 0.5)
        {
            rb.velocity = vel;
        }
    }
}


