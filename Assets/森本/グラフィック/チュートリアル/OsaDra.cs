using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsaDra : MonoBehaviour
{
    //アニメーションに使う
    Animator animator; //アニメーター
    static public bool OsamF = false;

    // Start is called before the first frame update
    void Start()
    {
        //Animator をとってくる
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(OsamF)
            {
                animator.Play("OsaDraMove");
            }

            if (OyaDra.OsaF)
            {
                animator.Play("OsaDoraani");
                OsamF = true;
            }
        }

    }
}
