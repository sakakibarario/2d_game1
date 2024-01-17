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

    bool ToHome = false;

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
        if(AniTutorialc.cnt == 5)//草原の背景を出す
        {
            SIRO.SetActive(true);
            KURO.SetActive(true);
            KUSA.SetActive(true);
        }
        if(AniTutorialc.cnt == 6)
        {
            ToHome = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
           if(OsaDra.SougenD && AniTutorialc.cnt == 5)//草原のドラゴンが左から登場
           {
                animator.Play("SougenDra");
                AniTutorialc.cnt = 6;

                Ara2.SetActive(false);
                Ara3.SetActive(true);

           }
           else if(ToHome && AniTutorialc.cnt == 6)//Homeにシーンを切り替える
            {
                Initiate.Fade(sceneName, fadeColor, fadeSpeed);
            }
        }

    }
}

