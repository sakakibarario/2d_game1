using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody型の変数
    float axisH = 0.0f;         //入力
    public LayerMask GroundLayer;

    public float speed = 3.0f;  //移動速度
    public float jump = 5.0f;   //ジャンプ力
    public float rush = 3.0f;   //突進の力

    //フラグ
    bool gojump  = false;       //ジャンプ判定
    bool ongrond = false;       //地面判定
    bool gorush  = false;       //攻撃判定(突進)
    bool horizon = false;       //向き

    //クールタイム
    public bool isCountDown = true;//true = 時間をカウントダウン計算する
    float rush_time = 5.0f;          //攻撃(突進)クールタイム
    public bool isTimeOver = false;//true = タイマー停止
    public float displayTime = 0;  //表示時間

    float times = 0;               //現在時間

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを持ってくる
        rb = GetComponent<Rigidbody2D>();

        if(isCountDown)
        {
            //カウントダウン
            displayTime = rush_time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //水平方向のチェック
        axisH = Input.GetAxisRaw("Horizontal");

        //向きの調整
        if(axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(5, 5);
            horizon = true;
        }
        if(axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-5, 5);
            horizon = false;
        }
        //キャラクターのジャンプ
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //キャラクターの突進攻撃
        if (ongrond)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Rush();
            }
        }
        if(isTimeOver ==false)//時間経過
        {
            times += Time.deltaTime;//経過時間を加算
            if(isCountDown)
            {
                //カウントダウン
                displayTime = rush_time - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;  //フラグをおろす
                }
            }
            Debug.Log("TIMES:" + displayTime);
        }

    }

    private void FixedUpdate()
    {
        //地上判定
        ongrond = Physics2D.Linecast(transform.position,
                                     transform.position - (transform.up * 0.1f),
                                     GroundLayer);
        if(ongrond || axisH != 0 || gorush==true)
        {
            //地上or速度が０ではないor攻撃中ではない
            //速度を更新
            rb.velocity = new Vector2(axisH * speed, rb.velocity.y);
        }
        if(ongrond && gojump)
        {
            //地上かつジャンプキーが押されたとき
            //ジャンプする
            Debug.Log("ジャンプ");
            Vector2 jumpPw = new Vector2(0, jump);      //ジャンプさせるベクトル
            rb.AddForce(jumpPw, ForceMode2D.Impulse);   //瞬間的な力を加える
            gojump = false; //ジャンプフラグをおろす
        }

        if(gorush && horizon == true)
        {
            //地上かつ左クリックが押されたときかつ右向き
            //突進する
            Debug.Log("突進");

            gorush = false; //攻撃フラグをおろす
            Vector2 rushPw = new Vector2(rush, 0);
            rb.AddForce(rushPw, ForceMode2D.Impulse);
            displayTime = rush_time;
            isTimeOver = false;
            times = 0;
        }
        else if (gorush && horizon == false)
        {
            //地上かつ左クリックが押されたときかつ左向き
            //突進する
            Debug.Log("突進");
            Vector2 rushPw = new Vector2(-rush, 0);
            rb.AddForce(rushPw, ForceMode2D.Impulse);
            gorush = false; //攻撃フラグをおろす
            displayTime = rush_time ;
            isTimeOver = false;
            times = 0;
        }
    }
    void Jump()
    {
        gojump = true; //ジャンプフラグを立てる
    }
    void Rush()
    {
        if(displayTime == 0)
        gorush = true; //攻撃(突進)フラグを立てる
        
    }
}
