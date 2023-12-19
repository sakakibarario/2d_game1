using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject thunder;
    public GameObject Player;
    public GameObject DangerArea1;
    public GameObject DangerArea2;
    public GameObject DangerArea3;

    //プレイヤーオブジェクト
    public GameObject player;
    //弾のプレハブオブジェクト
    public GameObject tama;

    private bool stop = false;//動き制御

    Rigidbody2D rb;

    //5秒ごとに弾を発射するためのもの
    private float targetTime = 5.0f;
    private float currentTime = 0;
    private bool count = true;

    public int hp = 300;
    public float reactionDistance = 20.0f;//反応距離

    private int Hero_Hp;

    //主人公の攻撃
    private int rushdamage = 10;    //突進の攻撃力
    private int buresball = 30;     //火球の攻撃力

    private bool inDamage = false;
    private bool isActive = false;

    private int oldHP;//HP記憶用
    private int rnd;//ランダム格納用
    public int i;//for用

    private bool DamageT = true;//falseの時サンダー攻撃以外しない

    //サンダーランダム用りすと
    int start = 1;
    int end = 3;
    List<int> numbers = new List<int>();
    bool random = false;

    //アニメーションに使う
    Animator animator; //アニメーター
    public string stopAnime = "StopMove";
    public string attack    = "attack";
    public string attackT   = "attackT";
    public string HeroDown = "EnemyHeroDown";

    //パーティクル用
    static public bool particleonC = false;
    //ゆっくり消す用
    private byte transparent_count;

    //フェード用
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    private void Start()
    {
        //Rigidbody2D をとる
        rb = GetComponent<Rigidbody2D>();
        Hero_Hp = hp;
        oldHP = hp;
        animator = GetComponent<Animator>();
        stop = true;
        transparent_count = 255;

        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        if (stop)
        {
            //Player　のゲームオブジェクトを得る
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && DamageT)
            {
                if (isActive && Hero_Hp > 0)
                {
                    if (count)
                    {
                        currentTime += Time.deltaTime;
                        //Debug.Log(currentTime);
                    }
                    if (targetTime < currentTime)
                    {
                        StartCoroutine(Slashing()); //斬撃コルーチン
                    }

                    if (Hero_Hp != oldHP)
                    {
                        DamageT = false;//すべての動作を停止
                        StartCoroutine(CountT());//カウントサンダーコルーチン
                        oldHP = Hero_Hp;
                    }

                }
                else
                {
                    //プレイヤーとの距離を求める
                    float dist = Vector2.Distance(transform.position, player.transform.position);
                    if (dist < reactionDistance)
                    {
                        isActive = true; //アクティブにする
                    }
                }
            }
            else if (isActive)
            {
                isActive = false;
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
            if (random)//リストの配列作成
            {
                for (int i = start; i <= end; i++)
                {
                    numbers.Add(i);
                }
                random = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //突進攻撃との接触
        if (other.gameObject.tag == "rushWall" && DamageT)
        {
            //ダメージ
            Hero_Hp -= rushdamage;
            Debug.Log(Hero_Hp);
            inDamage = true;
        }
        if (other.gameObject.tag == "Fireball" && DamageT)
        {
            //ダメージ
            Hero_Hp -= buresball;
            Debug.Log(Hero_Hp);
            Destroy(other.gameObject);
            inDamage = true;
        }
        EnemyDamage();//倒れているか調べる
    }

    //コルーチン
    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Hero_Hp <= 0)
        {
            TimeCounter.BossdownT = false;//タイめーを止めるフラグ
            StartCoroutine(Bossdown());
            DangerArea1.gameObject.SetActive(false);//倒れている場合は実行しない
            DangerArea2.gameObject.SetActive(false);//倒れている場合は実行しない
            DangerArea3.gameObject.SetActive(false);//倒れている場合は実行しない
            Debug.Log("敵が倒れている");
            stop = false;
            PlayerController.stop = false;
            PlayerController.gameState = "gameclear";
            Deletethunder.HeroDown = true;
        }
    }
    IEnumerator Bossdown()
    {
       
        Debug.Log("ゲームクリア");

        yield return new WaitForSeconds(0.2f);
        this.enabled = false;
        //Destroy(gameObject, 0.2f);//0.2かけて敵を消す

        particleonC = true;//パーティクル用フラグをあげる
        Global.Clear = true;
        animator.Play(HeroDown);
        for (; transparent_count > 0; transparent_count--)
        {
            //ボスをゆっくり消す
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, transparent_count);
            Debug.Log(transparent_count);
            yield return new WaitForSeconds(0.01f);
            if (transparent_count == 0)
            {
                yield return new WaitForSeconds(3.0f);
                Destroy(gameObject);

                Initiate.Fade(sceneName, fadeColor, fadeSpeed);
                yield break;
            }
        }
      
    }
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライト元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator Slashing()//斬撃コルーチン
    {
        count = false;//斬撃のカウントを停止
        currentTime = 0;//タイムリセット
        for (i = 0; i < 3; i++)
        {
            //Debug.Log(i);
            animator.Play(attack);//斬撃モーション
            yield return new WaitForSeconds(0.2f);//待機
            //敵の座標を変数posに保存
            var pos = this.gameObject.transform.position;
            //弾のプレハブを作成
            var t = Instantiate(tama) as GameObject;
            //弾のプレハブの位置を敵の位置にする
            t.transform.position = pos;
            t.AddComponent<HeroGan>();//動きをセット
            yield return new WaitForSeconds(0.1f);//待機
            animator.Play(stopAnime);//元に戻す
            yield return new WaitForSeconds(0.5f);//待機
            if (i == 2)
            {
                count = true;//カウント開始
                yield break;//終了
            }
        }
    }

    IEnumerator CountT()//カウンターサンだーコルーチン
    {

        for (i = 0; i < 3; i++)
        {
            int index = Random.Range(0, numbers.Count);//ランダム取得

            rnd = numbers[index];//使えるように
            //Debug.Log(rnd);

            numbers.RemoveAt(index);//取得した情報を消す

            if (rnd == 1)
            {
                //危険エリア表示
                DangerArea1.gameObject.SetActive(true);
                //コルーチン呼び出し
                StartCoroutine(Thunder1());
            }
            if (rnd == 2)
            {
                //危険エリア表示
                DangerArea2.gameObject.SetActive(true);
                //コルーチン呼び出し
                StartCoroutine(Thunder2());
            }
            if (rnd == 3)
            {   //危険エリア表示
                DangerArea3.gameObject.SetActive(true);
                //コルーチン呼び出し
                StartCoroutine(Thunder3());

            }
            yield return new WaitForSeconds(1.0f);//待機
            if (i == 2)
            {
                //Debug.Log("終了サンダー");
                random = true;  //リスト初期化の開始
                DamageT = true;//動き開始
                if(currentTime < 4.0f)
                {
                    currentTime = 4.0f;
                }
                yield break;
            }
        }


    }

    private IEnumerator Thunder1()
    {
        animator.Play(attackT);
       
        yield return new WaitForSeconds(2.0f);//待機

        //Point1の座標を変数posに保存
        var pos = Point1.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea1.gameObject.SetActive(false);
        animator.Play(stopAnime);
        
    }
    private IEnumerator Thunder2()
    {
        animator.Play(attackT);
        
        yield return new WaitForSeconds(2.0f);//待機
        //Point1の座標を変数posに保存
        var pos = Point2.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea2.gameObject.SetActive(false);
        animator.Play(stopAnime);
        
    }
    private IEnumerator Thunder3()
    {
        animator.Play(attackT);
       
        yield return new WaitForSeconds(2.0f);//待機
        //Point1の座標を変数posに保存
        var pos = Point3.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea3.gameObject.SetActive(false);
        animator.Play(stopAnime);
        
    }

}
