using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwaCrash : MonoBehaviour
{
    //アニメーションに使う
    Animator animator; //アニメーター

    public GameObject Iwa;

    //ブロックが消えるまでの時間
    private float targetTime = 2.0f;

    //カウントしている時間を入れる変数
    private float currentTime = 0;

    //崩れかけのAnimationを流すまでの時間を上の変数から引くの変数
    private float kamaetime = 1.5f;

    //Playerがのったら上げるフラグ
    static public bool Iwaflag = false;

    static public bool Iwakieflag = false;

    void Start()
    {
        //Animator をとってくる
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Iwaflag)
        {
            currentTime += Time.deltaTime;
            if (currentTime > (targetTime - kamaetime))//1.0f
            {
                animator.Play("岩崩れかけ");//崩れかけのAnimation生成
            }
            if (targetTime < currentTime)//2.0f
            {
               Iwa.gameObject.SetActive(false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("のった");
        if (collision.gameObject.tag == "Player")
        {
            Iwaflag = true;
            //Debug.Log("Player当たった");
        }
    }
}
