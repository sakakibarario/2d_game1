using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootDestroy : MonoBehaviour
{
    private float count = 1.0f;
    private void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = 1.0f;
            Destroy(this.gameObject);
        }
    }

    //‰æ–ÊŠO‚Éo‚½‚çÁ‚¦‚é
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("tama"))
    //    {
    //        Destroy(collision.gameObject);
    //    }

    //}

}
