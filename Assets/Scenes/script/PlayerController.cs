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
    public CapsuleCollider2D bx;

    public float speed = 3.0f;  //移動速度
    public float jump = 6.0f;   //ジャンプ力
    public float rush = 2.0f;   //突進の力
    public int D_HP;          　//ドラゴンのHP
    private int Max_D_HP;       //最大HP保存用

    private int S_D_HP = 50;     //草原でのドラゴンHP
    private int V_D_HP = 100;    //村でのHP
    private int C_D_HP = 150;    //城でのHP

    public static string gameState = "playing";//ゲームの状態
    static public bool stop = false;

    #region//敵の攻撃
    private int Suraimu = 5;    //スライムのダメージ
    private int Goburin = 5;    //ゴブリンのダメージ
    private int touzokugan = 15;//盗賊の遠距離攻撃のダメージ
    private int artillery = 25; //大砲の攻撃
    private int bird = 5;       // 鳥の攻撃
    private int cane = 20;      // 杖の攻撃
    private int stone = 10;     //子供の石攻撃
    private int famer = 15;     //農民の攻撃
    private int mercenary =20;  //傭兵の攻撃
    private int arrow = 15;     //弓使いの攻撃
    private int knight = 30;     //騎士の攻撃
    private int Explosion = 40;  //特攻兵
    private int witch = 20;      //魔女の攻撃
    private int caliver = 30;    //騎兵の攻撃
    private int toge = 10;　　　　//針の攻撃
    private int thunder = 40;     //雷攻撃
    private int heroattack = 30; //斬撃攻撃
    private int TLeaf = 10;      //葉っぱ攻撃
    #endregion

    #region//主人公の動き関係フラグ
    bool gojump = false;                     //ジャンプ判定
    bool ongrond = false;                    //地面判定
    public static bool gorush = false;       //攻撃判定(突進)
    bool Fireball_F = false;                 // 火球攻撃判定
    static public bool horizon = true;       //向き
    bool inDamage = false;                   //ダメージ中フラグ
    bool inrecovery = false;                 //回復中フラグ

    //技のフラグ
    static public bool SougenBoss = false;
    static public bool VillageBoss = false;
    static public bool CastleBoss = false;

    //回復アイテム
    private int meat = Global.GRecoveryMeat;

    //clear後の動き
    public int CJump = 0;
    #endregion

    //クールタイム
    public bool isCountDown = true;//true = 時間をカウントダウン計算する
    public bool AnimeCount = true;
    float rush_time = 1.5f;          //攻撃(突進)クールタイム
    static public bool isTimeOver = false;//true = タイマー停止
    public bool animeOver = true;
    public float displayTime = 0;  //表示時間
    public float Animetime = 0;
    public float animerushtime = 2.0f;

    //クールタイム火球
    public bool K_isCountDown = true; //true = 時間をカウントダウン計算する
    private float Onbures = 2.0f;     //攻撃（火球）クールタイム
    static public bool K_isTimeOver = false;//true = タイマー停止
    public float buresutime = 0;      //表示時間
    float K_timesnow = 0;             //現在時間

    float times = 0;               //現在時間
    float Anitimes = 0;

    //アニメーション対応
    Animator animator; //アニメーター
    public string stopAnime  = "PlayerStop";
    public string moveAnime  = "PlayerMove";
    public string jumpAnime  = "PlayerJump";
    public string rushAnime  = "PlayerRush";
    public string clearAnime = "PlayerClear";
    string nowAnime = "";
    string oldAnime = "";

    //ゲームステータス管理フラグ
    static public bool pose = false;

    //SE用
    [SerializeField]
    AudioSource flameAudioSource;

    //シーン切り替え用
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        stop = true;

        //Rigidbody2Dを持ってくる
        rb = GetComponent<Rigidbody2D>();

        //Animator をとってくる
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        //ゲームの状態をプレイ中にする
        gameState = "playing";

        if(SougenBoss)
        {
            D_HP = V_D_HP;
            Max_D_HP = V_D_HP;
            slider.maxValue = 2;
            slider.value = 2;
            if (VillageBoss)
            {
                D_HP = C_D_HP;
                Max_D_HP = C_D_HP;
                slider.maxValue = 3;
                slider.value = 3;
            }
        }
        else
        {
            slider.value = 1;
            D_HP = S_D_HP;
            Max_D_HP = S_D_HP;
        }
        if (isCountDown)
        {
            //カウントダウン
            displayTime = rush_time;
        }
        if(AnimeCount)
        {
            Animetime = animerushtime;
        }
        if(K_isCountDown)
        {
            buresutime = Onbures;
        }
        //突進
        Animetime = 0.0f;
        animeOver = true;  //フラグをおろす
        gorush = false; //攻撃フラグをおろす
        isTimeOver = false;
        Anitimes = 0;
        times = 0;

        //火球
        K_timesnow = 0;
        K_isTimeOver = false;
        Fireball_F = false;

    }

    // Update is called once per frame
    void Update()
    {
       
      
        //ゲーム中以外とダメージ中は何もしない
        if (stop)
        {
            //ゲームステータス管理
            if (pose)
            {
                Time.timeScale = 0;
                gameState = "posing";
                rb.isKinematic = true;
            }
            else
            {
                Time.timeScale = 1;
                gameState = "playing";
                rb.isKinematic = false;
            }
            //ゲーム中以外とダメージ中は何もしない
            if (gameState != "playing")
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
            if (Input.GetMouseButtonDown(1) && SougenBoss)
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
            if (K_isTimeOver == false)
            {
                K_timesnow += Time.deltaTime;//経過時間を加算
                if (K_isCountDown)
                {
                    //カウントダウン
                    buresutime = Onbures - K_timesnow;
                    if (buresutime <= 0.0f)
                    {
                        buresutime = 0.0f;
                        K_isTimeOver = true;//フラグを下す
                    }
                }

            }
        }
        else
        {
            if (Global.Clear)
            {
                StartCoroutine(ClearMove());
            }
            else
            {
                animator.Play(stopAnime);
                Debug.Log("stop");
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
        if (stop)
        {
            if(gameState != "playing")
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
            if (inrecovery)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 3, 255);
                //回復中点滅させる
                float val = Mathf.Sin(Time.time * 20);
                Debug.Log(val);
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
            if (VillageBoss && gojump)
            {
                Debug.Log("ジャンプ");
                Vector2 jumpPw2 = new Vector2(0, jump);      //ジャンプさせるベクトル
                rb.AddForce(jumpPw2, ForceMode2D.Impulse);   //瞬間的な力を加える
                gojump = false; //ジャンプフラグをおろす
            }

            if (gorush && horizon == true)
            {
                //地上かつ左クリックが押されたときかつ右向き
                //突進する
                Debug.Log("突進");

                //SE　突進
                GetComponent<AudioSource>().Play();

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
                            rb.velocity = Vector2.zero;//追加
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

                //SE 突進
                GetComponent<AudioSource>().Play();

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
                            rb.velocity = Vector2.zero;//追加
                        }

                    }
                    Debug.Log("TIMES:" + Animetime);
                }
            }
            if (Fireball_F)
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
                flameAudioSource.Play();
                Debug.Log("火球");
                buresutime = Onbures;   //カウントダウン時間のリセット
                K_timesnow = 0;         //表示時間のリセット
                K_isTimeOver = false;   //フラグをあげる
                Fireball_F = false;     //フラグをおろす
                                        //SE　火球
                GetComponent<AudioSource>().Play();

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
        else
        {
            if (Global.Clear)
            {
                StartCoroutine(ClearMove());
            }
            else
            {
                animator.Play(stopAnime);
                Debug.Log("stop");
            }
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


    //接触開始ダメージ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inDamage)
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
            if (collision.CompareTag("TLeaf"))
            {
                D_HP -= TLeaf;     //HPを減らす（盗賊の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("artillery"))
            {
                D_HP -= artillery;     //HPを減らす（大砲の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("bird"))
            {
                D_HP -= bird;       //HPを減らす（村長の鳥）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.gameObject.tag == "cane")
            {
                D_HP -= cane;       //HPを減らす（村長の杖）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("stone"))
            {
                D_HP -= stone;       //HPを減らす（村長の杖）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("stone"))
            {
                D_HP -= stone;       //HPを減らす（石の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("arrow"))
            {
                D_HP -= arrow;       //HPを減らす（弓使いの攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("famer"))
            {
                D_HP -= famer;       //HPを減らす（村長の杖）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("mercenary"))
            {
                D_HP -= mercenary;       //HPを減らす（傭兵の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("knight"))
            {
                D_HP -= knight;       //HPを減らす（騎士の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("Explosion"))
            {
                D_HP -= Explosion;       //HPを減らす（特攻兵の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("caliver"))
            {
                D_HP -= caliver;       //HPを減らす（騎兵の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("witch"))
            {
                D_HP -= witch;       //HPを減らす（魔女の攻撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.gameObject.tag == ("upper"))
            {
                D_HP -= toge;       //HPを減らす（魔女の攻撃）
                GetDamage(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("heroattack"))
            {
                D_HP -= heroattack;       //HPを減らす（勇者の斬撃）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("thunder"))
            {
                D_HP -= thunder;       //HPを減らす（勇者の魔法雷）
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.gameObject.tag == "meat")//Hpを回復
            {
                if(D_HP != Max_D_HP)
                {
                    D_HP += meat;
                    Destroy(collision.gameObject);//当たったオブジェクトを削除
                    if(D_HP > Max_D_HP)
                    {
                        D_HP = Max_D_HP;
                    }
                    slider.value = (float)D_HP / (float)S_D_HP; ;
                    Debug.Log("slider.value : " + slider.value);
                    GetRecovery();
                } 
            }
        }
        if (collision.gameObject.tag == "dead")
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
            Vector3 v = (this.transform.position - @object.transform.position).normalized;
            rb.AddForce(new Vector2(v.x * 5, v.y * 5), ForceMode2D.Impulse);
            //ダメージフラグON
            inDamage = true;
            Invoke("DamageEnd", 0.5f);
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
    //回復中
    void GetRecovery()
    {
        inrecovery = true;
        Invoke("RecoveryEnd", 0.5f);
    }
    //回復終了
    void RecoveryEnd()
    {
        //回復フラグおろす
        inrecovery = false;
        //スプライトをもとに戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        gameState = "gameover";
        stop = false;
        rb.velocity = Vector2.zero;
        StartCoroutine(GameOverT());
    }
    IEnumerator GameOverT()
    {
        
        new Vector3(transform.position.x, 0, 0);
        bx.enabled = false;
        transform.localRotation = new Quaternion(180.0f, 0.0f, 0.0f, 0.0f);
        rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
       

        
        yield return new WaitForSeconds(2.0f);
        Initiate.Fade(sceneName, fadeColor, fadeSpeed);
        //SceneManager.LoadScene("Gameover");
    }
    IEnumerator ClearMove()
    {
        bx.enabled = false;
        rb.isKinematic = true;
        Debug.Log("ああああああああああああああああああああ");
        this.transform.position = new Vector3(transform.position.x,-2.2f, 0);
        animator.Play(clearAnime);
        yield break;
    }
}