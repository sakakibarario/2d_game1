using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletethunder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//Player‚É“–‚½‚Á‚½‚ç
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Ground")//ground‚É“–‚½‚Á‚½‚ç
        {
            Destroy(this.gameObject);
        }
    }
}
