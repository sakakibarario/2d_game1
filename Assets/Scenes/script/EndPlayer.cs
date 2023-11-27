using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class EndPlayer : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody型の変数

    //
    private int rush = 8;
    private float speed = 2.5f;

    //フラグ関係
    private bool move = false;
    private bool Onrush = false;

    //アニメーション対応
    Animator animator; //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string rushAnime = "PlayerRushend";
    string nowAnime = "";
    string oldAnime = "";

    //SE用
    [SerializeField]
    AudioSource flameAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを持ってくる
        rb = GetComponent<Rigidbody2D>();

        //Animator をとってくる
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        move = true;//フラグをあげる
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (move)
        {
            rb.velocity = new Vector2(1.0f * speed, rb.velocity.y);
            animator.Play(moveAnime);
        }
        else if(Onrush)
        {
            animator.Play(rushAnime);
        }
        else
        {  
            animator.Play(stopAnime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "kingStart")
        {
            move = false;
           
            StartCoroutine(Rush());
        }
    }
    IEnumerator Rush()
    {
        rb.velocity = new Vector2(0, 0);//速度を止める
        yield return new WaitForSeconds(1.0f);//待機
       
        Vector2 rushPw = new Vector2(rush, 0);
        rb.AddForce(rushPw, ForceMode2D.Impulse);
        Onrush = true;//フラグを上げる
        yield return new WaitForSeconds(1.0f);//待機

        rb.velocity = new Vector2(0, 0);//止める
        move = true;//フラグを上げる
        Onrush = false;//フラグををろす
        
        yield break;
    }
}