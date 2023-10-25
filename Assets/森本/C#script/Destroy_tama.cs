using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_tama : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プレイヤーとtamaのタグがあったら消える
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("tama"))
        {
            Destroy(collision.gameObject);
        }
    }

    //画面外に出たら消える
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
