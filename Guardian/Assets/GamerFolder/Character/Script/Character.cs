using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    
    public int life;
    public Transform skin;
    public Transform cam;
    public Text Contador;
    public AudioClip bossBattleMusic;
    public AudioClip youwin;



    // Start is called before the first frame update
    void Start()
    {
      
       
    }

    // Update is called once per frame
    void Update()
    {
     
        if (life <=0 && !transform.name.Equals("BossBrain")) 
        
        { 

        skin.GetComponent<Animator>().Play("Die", -1);

        }
        if (transform.CompareTag("Player"))
        {
            Contador.text = "x" + life.ToString();
            if (SceneManager.GetActiveScene().name.Equals("Level5")) 
            {
                cam.GetComponent<Animator>().enabled = false;
                cam.GetComponent<Camera>().orthographicSize = 10.3f;
                cam.position = new Vector3(11.95f,6.4f, -6);
                cam.parent = null;
                SceneManager.MoveGameObjectToScene(cam.gameObject, SceneManager.GetActiveScene());


                if (GameObject.Find("BossBrain").GetComponent<Character>().life > 0)
                {
                    if (cam.GetComponent<AudioSource>().clip != bossBattleMusic)
                    {
                        cam.GetComponent<AudioSource>().clip = bossBattleMusic;
                        cam.GetComponent<AudioSource>().Play();
                    }
                }
                else 
                {
                    GameObject.Find("YouWin").GetComponent<GameOver>().enabled = true;
                    GetComponent<PlayerController>().enabled = false;
                    GetComponent<CapsuleCollider2D>().enabled = false;
                   GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                    if (cam.GetComponent<AudioSource>().clip != null)
                    {
                        cam.GetComponent<AudioSource>().Stop();
                        cam.GetComponent<AudioSource>().clip = null;
                        cam.GetComponent<AudioSource>().PlayOneShot(youwin);
                    }

                }

            }


        }

        if (transform.name.Equals("BossBrain"))
        {

            transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(1.78f, (life * 1.09f / 30f));

            if (life <= 0)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

              

               

            }
        }
    }
    public void PlayerDamage(int value) 
    {

       life = life - value;
        skin.GetComponent<Animator>().Play("PlayerDamage", 1);
        cam.GetComponent<Animator>().Play("MainCamera", -1);
        GetComponent<PlayerController>().audioSource.PlayOneShot(GetComponent<PlayerController>().damageSound,0.5f);
    
    }
}
