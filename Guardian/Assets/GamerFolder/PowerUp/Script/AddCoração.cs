using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoração : MonoBehaviour


{
    public GameObject efeitoCoração;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Instantiate(efeitoCoração, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            collision.GetComponent<Character>().life++;
            Destroy(transform.gameObject);
           
        
        }
    }


}
