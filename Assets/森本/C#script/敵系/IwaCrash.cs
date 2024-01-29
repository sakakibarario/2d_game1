using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwaCrash : MonoBehaviour
{
    //アニメーションに使う
    Animator animator; //アニメーター

    private float targetTime = 2.0f;
    private float currentTime = 0;
    private float kamaetime = 1.5f;
    private bool Iwaflag = false;

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
            if (currentTime > (targetTime - kamaetime))//1.0f
            {
                animator.Play("岩崩れかけ");
            }
            if (targetTime < currentTime)
            {
                Destroy(this.gameObject);
                Iwaflag = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("のった");
        if (collision.gameObject.tag == "Player")
        {
            Iwaflag = true;
            Debug.Log("Player当たった");
        }

    }
}
