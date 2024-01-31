using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SougenDra : MonoBehaviour
{
    //アニメーションに使う
    Animator animator; //アニメーター
    public GameObject SIRO;
    public GameObject KURO;
    public GameObject KUSA;

    public GameObject Ara2;
    public GameObject Ara3;

    static public bool ToHome = false;

    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Animator をとってくる
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AniTutorialc.cnt == 6)
        {
            ToHome = true;//Homeシーンにフェードさせるためのフラグをtrueにする
        }

        if (Input.GetMouseButtonDown(0))
        {
           if(OsaDra.SougenD && AniTutorialc.cnt == 5)//５番目
           {
                animator.Play("SougenDra");//草原のドラゴンが左から登場 
                AniTutorialc.cnt = 6;

                SIRO.SetActive(true);//草原の背景を出す
                KURO.SetActive(true);
                KUSA.SetActive(true);

                Ara2.SetActive(false);//あらすじ2を消す
                Ara3.SetActive(true);//あらすじ3が出現

           }
           else if(ToHome && AniTutorialc.cnt == 6)// ６番目
            {
                Initiate.Fade(sceneName, fadeColor, fadeSpeed);//Homeにシーンを切り替える フェードアウト
            }
        }

    }
}

