using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyaDra : MonoBehaviour
{
    //アニメーションに使う
    Animator animator; //アニメーター
    static public bool OsaF = false;
    static public bool OsaDs = false;
    public GameObject Ara1;
    public GameObject Ara2;

    // Start is called before the first frame update
    void Start()
    {
        //Animator をとってくる
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AniTutorialc.cnt == 2)
        {
            OsaDs = true; //２番目に流れる DesDra のフラグ OsaDs を true にする
        }

        if (AniTutorialc.cnt == 3)
        {
            OsaF = true;//OsaDra.cs の OsaF を true にする
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (AniTutorialc.OyaF )//親のドラゴンが登場 １番目
            {
                animator.Play("Dragonani");        
                AniTutorialc.OyaF = false;//２回目が流れないようにfalseにする
                
                AniTutorialc.cnt = 2;
                
            }
            else if(OsaDs && AniTutorialc.cnt == 2)//２番目
            {
                Ara1.SetActive(false);//あらすじ1が消える
                Ara2.SetActive(true); //あらすじ2が出現

                animator.Play("DesDra");//親のドラゴンが消える
                AniTutorialc.cnt = 3;

            }

        }
    }
}
