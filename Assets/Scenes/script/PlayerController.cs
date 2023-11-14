using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody型の変数
    float axisH = 0.0f;         //入力
    public LayerMask GroundLayer;
    public Slider slider;
    public GameObject tama;

    public float speed = 3.0f;  //移動速度
    public float jump = 5.0f;   //ジャンプ力
    public float rush = 2.0f;   //突進の力
    public int D_HP;          　//ドラゴンのHP
    public int S_D_HP = 50;     //草原でのドラゴンHP

    public static string gameState = "playing";//ゲームの状態

    //敵の攻撃
    public int Suraimu = 5;    //スライムのダメージ
    public int Goburin = 5;   //ゴブリンのダメージ
    public int touzokugan = 15;//盗賊の遠距離攻撃のダメージ

    //主人公の動き関係フラグ
    bool gojump = false;       //ジャンプ判定
    bool ongrond = false;       //地面判定
    public static bool gorush = false;       //攻撃判定(突進)
    bool Fireball_F = false;    // 火球攻撃判定
    static public bool horizon = true;       //向き
    bool inDamage = false;      //ダメージ中フラグ

    //クールタイム
    public bool isCountDown = true;//true = 時間をカウントダウン計算する
    public bool AnimeCount = true;
    float rush_time = 1.5f;          //攻撃(突進)クールタイム
    public bool isTimeOver = false;//true = タイマー停止
    public bool animeOver = true;
    public float displayTime = 0;  //表示時間
    public float Animetime = 0;
    public float animerushtime = 2.0f;

    //クールタイム火球
    public bool K_isCountDown = true; //true = 時間をカウントダウン計算する
    private float Onbures = 2.0f;     //攻撃（火球）クールタイム
    private bool K_isTimeOver = false;//true = タイマー停止
    public float buresutime = 0;      //表示時間
    float K_timesnow = 0;             //現在時間

    float times = 0;               //現在時間
    float Anitimes = 0;

    //アニメーション対応
    Animator animator; //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string rushAnime = "PlayerRush";
    string nowAnime = "";
    string oldAnime = "";

    //ゲームステータス管理フラグ
    static public bool pose = false;

    //技のフラグ
    static public bool SougenBoss = false;
    static public bool VillageBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;

        //Rigidbody2Dを持ってくる
        rb = GetComponent<Rigidbody2D>();

        //Animator をとってくる
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        //ゲームの状態をプレイ中にする
        gameState = "playing";

        D_HP = S_D_HP;

        if (isCountDown)
        {
            //カウントダウン
            displayTime = rush_time;
        }
        if(AnimeCount)
        {
            Animetime = animerushtime;
        }
        //突進
        Animetime = 0.0f;
        animeOver = true;  //フラグをおろす
        gorush = false; //攻撃フラグをおろす
        displayTime = rush_time;
        Animetime = animerushtime;
        isTimeOver = false;
        Anitimes = 0;
        times = 0;

        //火球
        buresutime = Onbures;
        K_timesnow = 0;
        K_isTimeOver = false;
        Fireball_F = false;

    }

    // Update is called once per frame
    void Update()
    {
       　//ゲームステータス管理
        if(pose)
        {
            gameState = "posing";
            rb.isKinematic = true;
        }
        else
        {
            gameState = "playing";
            rb.isKinematic = false;
        }

        //ゲーム中以外とダメージ中は何もしない
        if (gameState != "playing" || inDamage)
        {
            rb.velocity = new Vector2(0, 0);
            animator.Play(stopAnime);
           
            return;
        }

        //水平方向のチェック
        axisH = Input.GetAxisRaw("Horizontal");

        //向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            // Debug.Log("右移動");
            transform.localScale = new Vector2(5, 5);
            horizon = true;
        }
        if (axisH < 0.0f)
        {
            //左移動
            //Debug.Log("左移動");
            transform.localScale = new Vector2(-5, 5);
            horizon = false;
        }
        //キャラクターのジャンプ
        if (Input.GetButtonDown("Jump"))
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
        //キャラクターの火球
        if(Input.GetMouseButtonDown(1) && SougenBoss)
        {
            Fireball();
        }

        //時間経過タックル
        if (isTimeOver == false)
        {
            times += Time.deltaTime;//経過時間を加算
            if (isCountDown)
            {
                //カウントダウン
                displayTime = rush_time - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;  //フラグをおろす
                }
            }
        }
        //時間経過火球
        if(K_isTimeOver == false)
        {
            K_timesnow += Time.deltaTime;//経過時間を加算
            if(K_isCountDown)
            {
                //カウントダウン
                buresutime = Onbures - K_timesnow;
                if(buresutime <= 0.0f)
                {
                    buresutime = 0.0f;
                    K_isTimeOver = true;//フラグを下す
                }
            }

        }
    }



    private void FixedUpdate()
    {
        //地上判定
        ongrond = Physics2D.Linecast(transform.position,
                                     transform.position - (transform.up * 0.1f),
                                     GroundLayer);

        //ゲーム中以外は何もしない
        if (gameState != "playing")
        {
            return;
        }
        if (inDamage)
        {
            //ダメージ中点滅させる
            float val = Mathf.Sin(Time.time * 50);
            // Debug.Log(val);
            if (val > 0)
            {
                //スプライトを表示
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //スプライトを非表示
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;//ダメージ中は操作による移動はさせない
        }

        if (ongrond || axisH != 0 || gorush == true)
        {
            //地上or速度が０ではないor攻撃中ではない
            //速度を更新
            rb.velocity = new Vector2(axisH * speed, rb.velocity.y);
        }
        if (ongrond && gojump)
        {
            //地上かつジャンプキーが押されたとき
            //ジャンプする
            Debug.Log("ジャンプ");
            Vector2 jumpPw = new Vector2(0, jump);      //ジャンプさせるベクトル
            rb.AddForce(jumpPw, ForceMode2D.Impulse);   //瞬間的な力を加える
            gojump = false; //ジャンプフラグをおろす
        }

        if (gorush && horizon == true)
        {
            //地上かつ左クリックが押されたときかつ右向き
            //突進する
            Debug.Log("突進");



            Vector2 rushPw = new Vector2(rush, 0);
            rb.AddForce(rushPw, ForceMode2D.Impulse);

            if (animeOver == false)//時間経過
            {
                Anitimes += Time.deltaTime;//経過時間を加算
                if (AnimeCount)
                {
                    //カウントダウン
                    Animetime = animerushtime - Anitimes;
                    if (Animetime <= 0.0f)
                    {
                        Animetime = 0.0f;
                        animeOver = true;  //フラグをおろす
                        gorush = false; //攻撃フラグをおろす
                        displayTime = rush_time;
                        Animetime = animerushtime;
                        isTimeOver = false;
                        Anitimes = 0;
                        times = 0;
                    }
                   
                }
             //   Debug.Log("TIMES:" + Animetime);
            }
        }
        else if (gorush && horizon == false)
        {
            //地上かつ左クリックが押されたときかつ左向き
            //突進する
            Debug.Log("突進");

            Vector2 rushPw = new Vector2(-rush, 0);
            rb.AddForce(rushPw, ForceMode2D.Impulse);

            if (animeOver == false)//時間経過
            {
                Anitimes += Time.deltaTime;//経過時間を加算
                if (AnimeCount)
                {
                    //カウントダウン
                    Animetime = animerushtime - Anitimes;
                    if (Animetime <= 0.0f)
                    {
                        Animetime = 0.0f;
                        animeOver = true;  //フラグをおろす
                        gorush = false; //攻撃フラグをおろす
                        displayTime = rush_time;
                        Animetime = animerushtime;
                        isTimeOver = false;
                        Anitimes = 0;
                        times = 0;
                    }

                }
                Debug.Log("TIMES:" + Animetime);
            }
        }
        if(Fireball_F)
        {
            //主人公の座標を変数posに保存
            var posR = this.gameObject.transform.position + (transform.up * 1.5f) + transform.right * 1.2f;
            var posL = this.gameObject.transform.position + (transform.up * 1.5f) - transform.right * 1.2f;
            //弾のプレハブを作成
            var t = Instantiate(tama) as GameObject;
            //弾のプレハブの位置を位置にする
           
             if (horizon)
             {
                 t.transform.position = posR;
                 t.AddComponent<Playerboll>();
             }
             else
             {
                 t.transform.position = posL;
                 t.AddComponent<Playerboll2>();
             }
            Debug.Log("火球");
            buresutime = Onbures;   //カウントダウン時間のリセット
            K_timesnow = 0;         //表示時間のリセット
            K_isTimeOver = false;   //フラグをあげる
            Fireball_F = false;     //フラグをおろす
        }

        //アニメーション
        if (ongrond)
        {
            //地上のうえ
            if (axisH == 0)
            {
                nowAnime = stopAnime; //停止中
            }
            else
            {
                nowAnime = moveAnime; //移動
            }
            if (gorush)
            {
                nowAnime = rushAnime;
                Debug.Log("アニメーション！");
            }
        }
        else
        {
            //空中
            nowAnime = jumpAnime;
        }



        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    //アニメーション再生
        }
 
    }

    //主人公に動き
    void Jump()//ジャンプ
    {
        gojump = true; //ジャンプフラグを立てる
    }
    void Rush()//突進
    {
        if (displayTime == 0)
        {
            gorush = true; //攻撃(突進)フラグを立てる
            animeOver = false;

        }
    }
    void Fireball()//火球
    {
        if (buresutime == 0)
        {
            Fireball_F = true;
        }
    }


    //接触開始
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "damege_s")//スライム
        {
            Debug.Log("接触");
            if (gorush == false)
            {
                D_HP -= Suraimu;    //HPを減らす
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "damage_g")//ゴブリン
        {
            Debug.Log("接触");
            if (gorush == false)
            {
                D_HP -= Goburin;    //HPを減らす（ゴブリンの攻撃）
                GetDamage(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
        }
        if (collision.CompareTag("tama"))
        {
            D_HP -= touzokugan;     //HPを減らす（盗賊の攻撃）
            GetDamage(collision.gameObject);
            Destroy(collision.gameObject);
            slider.value = (float)D_HP / (float)S_D_HP; ;
            Debug.Log("slider.value : " + slider.value);
            GetDamage(collision.gameObject);
        }
        if(collision.gameObject.tag == "dead")
        {
            GameOver();
        }
    }

    //ダメージを受けた時の動き
    public void GetDamage(GameObject @object)
    {

        Debug.Log("Player HP" + D_HP);
        if (D_HP > 0)
        {
            //移動停止
            rb.velocity = new Vector2(0, 0);
            //敵キャラの反対側にヒットバックさせる
            Vector3 v = (transform.position - @object.transform.position).normalized;
            rb.AddForce(new Vector2(v.x * 5, v.y * 5), ForceMode2D.Impulse);
            //ダメージフラグON
            inDamage = true;
            Invoke("DamageEnd", 0.25f);
        }
        else
        {
            //ゲームオーバー
            GameOver();
        }
    }
    //ダメージ終了
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライト元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        gameState = "gameover";
        SceneManager.LoadScene("Gameover");
    }
}