using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuwa : MonoBehaviour
{
    public int hp = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ìñÇΩÇ¡ÇΩéûÇÃèàóù
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("enemy"))
    //    {
    //        //Destroy(collision.gameObject);
    //        hp -= 10;
    //        Debug.Log(hp);
    //    }
    //}

}
