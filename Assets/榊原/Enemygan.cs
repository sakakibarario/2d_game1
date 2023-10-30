using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemygan : MonoBehaviour
{
    private SpriteRenderer sr = null;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
   

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
