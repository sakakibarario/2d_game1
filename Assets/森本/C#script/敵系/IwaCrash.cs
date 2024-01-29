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
    private bool Iwaflag = false;

    //ブロックが復活する時間
    private float revival = 2.0f;

    //復活するまでの秒数を図る時間
    private float revtime = 0.0f;

    private bool revflag = false;

    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        //Animator をとってくる
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Iwaflag)
        {
            currentTime += Time.deltaTime;
            Debug.Log(currentTime);
            if (currentTime > (targetTime - kamaetime))//1.0f
            {
                animator.Play("岩崩れかけ");//崩れかけのAnimation生成
            }
            if (targetTime < currentTime)//2.0f
            {
               Iwa.gameObject.SetActive(false);
                //col.enabled = false;
                //Iwaflag = false;

                revflag = true;//復活flagを上げる
            }
            if(4 <currentTime)
            {
                Debug.Log("復活");
                Iwa.gameObject.SetActive(true);
                //col.enabled = true;
            }
        }

        //if(revflag)
        //{
        //    revtime += Time.deltaTime;
        //    Debug.Log(revtime);

        //    if(revtime>revival)
        //    {
        //        this.gameObject.SetActive(true);
        //        col.enabled = true;
        //    }
        //}
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
