using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBall : MonoBehaviour
{
    float count = 5.0f;
    private void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            count = 5.0f;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
   

    }

    //‰æ–ÊŠO‚Éo‚½‚çÁ‚¦‚é
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
