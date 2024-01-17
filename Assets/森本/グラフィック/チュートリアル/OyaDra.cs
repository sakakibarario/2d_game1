using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyaDra : MonoBehaviour
{
    //アニメーションに使う
    Animator animator; //アニメーター
    static public bool OsaF = false;
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
        if (AniTutorialc.cnt == 2)
        {
            OsaF = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (AniTutorialc.OyaF )
            {
                animator.Play("Dragonani");        
                AniTutorialc.OyaF = false;
                
                AniTutorialc.cnt = 2;
                Ara1.SetActive(false);
                Ara2.SetActive(true);
                
            }

        }
    }
}
