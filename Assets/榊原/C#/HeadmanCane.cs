using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadmanCane : MonoBehaviour
{
    float count = 0.3f;
    private void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            count = 0.3f;
            Destroy(this.gameObject);
        }
    }



    //‰æ–ÊŠO‚Éo‚½‚çÁ‚¦‚é
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
