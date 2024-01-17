using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsaDra : MonoBehaviour
{
    //アニメーションに使う
    Animator animator; //アニメーター
    static public bool OsamF = false;
    static public bool SougenD = false;


    // Start is called before the first frame update
    void Start()
    {
        //Animator をとってくる
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AniTutorialc.cnt == 5)
        {
            SougenD = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (OyaDra.OsaF && AniTutorialc.cnt == 3)
            {
                animator.Play("OsaDoraani");
                OsamF = true;
                AniTutorialc.cnt = 4;
            }
            else if (OsamF && AniTutorialc.cnt == 4)
            {
                animator.Play("OsaDraMove");
                AniTutorialc.cnt = 5;
            }
        }

    }
}
