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
        if(AniTutorialc.cnt == 4)
        {
            SIRO.SetActive(true);
            KURO.SetActive(true);
            KUSA.SetActive(true);
        }
        if(AniTutorialc.cnt == 5)
        {
            ToHome = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
           if(OsaDra.SougenD && AniTutorialc.cnt == 4)
           {
                animator.Play("SougenDra");
                AniTutorialc.cnt = 5;

                Ara2.SetActive(false);
                Ara3.SetActive(true);

           }
           else if(ToHome && AniTutorialc.cnt == 5)
            {
                Initiate.Fade(sceneName, fadeColor, fadeSpeed);
            }
        }

    }
}

