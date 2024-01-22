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
            SougenD = true;//SougenDra.cs の SougenD を true にする
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (OyaDra.OsaF && AniTutorialc.cnt == 3)//３番目
            {
                animator.Play("OsaDoraani");//ドラゴンが逆を向く
                OsamF = true;//ドラゴンが逃げるフラグ OsamF を true にする
                AniTutorialc.cnt = 4;
            }
            else if (OsamF && AniTutorialc.cnt == 4)//４番目
            {
                animator.Play("OsaDraMove");//幼いドラゴンが逃げる
                AniTutorialc.cnt = 5;
            }
        }

    }
}
