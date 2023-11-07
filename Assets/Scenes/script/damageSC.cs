using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageSC : MonoBehaviour
{
    public BoxCollider2D rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<BoxCollider2D>();
        rd.enabled = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gorush == true)
        {
            rd.enabled = true;
        }
        else
        {
            rd.enabled = false;
        }
    }
}
