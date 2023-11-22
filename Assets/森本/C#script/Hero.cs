using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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




    Rigidbody2D rb;

    //5秒ごとに弾を発射するためのもの
    private float targetTime = 5.0f;
    private float currentTime = 0;

    public int hp = 300;
    public float reactionDistance = 20.0f;//反応距離

    private int A_Hp;

    //主人公の攻撃
    private int rushdamage = 10;    //突進の攻撃力
    private int buresball = 30;     //火球の攻撃力

    private bool inDamage = false;
    private bool isActive = false;


    public EnemyArrow bullet;

    //アニメーションに使う
    private Animator anim = null;
    Animator animator; //アニメーター
    public string stopAnime = "StopMove";
    public string attack    = "attack";
    public string attackT   = "attackT";

    private void Start()
    {
        //Rigidbody2D をとる
        rb = GetComponent<Rigidbody2D>();
        A_Hp = hp;

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        //Player　のゲームオブジェクトを得る
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && A_Hp > 0)
            {
                currentTime += Time.deltaTime;

                if (targetTime < currentTime)
                {
                    currentTime = 0;
                    //敵の座標を変数posに保存
                    var pos = this.gameObject.transform.position;
                    //弾のプレハブを作成
                    var t = Instantiate(tama) as GameObject;
                    //弾のプレハブの位置を敵の位置にする
                    t.transform.position = pos;
                    t.AddComponent<HeroGan>();
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

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //突進攻撃との接触
        if (other.gameObject.tag == "rushWall")
        {
            //ダメージ
            A_Hp -= rushdamage;
            Debug.Log(A_Hp);
            inDamage = true;
        }
        if (other.gameObject.tag == "Fireball")
        {
            //ダメージ
            A_Hp -= buresball;
            Debug.Log(A_Hp);
            Destroy(other.gameObject);
            inDamage = true;
        }
        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (A_Hp <= 0)
        {
            Debug.Log("敵が倒れている");
            Destroy(gameObject, 0.2f);//0.2かけて敵を消す
        }
    }
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライト元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int rnd = 1;

        if (collision.gameObject.tag == "Player")//Playerに当たったら
        {
            rnd = Random.Range(1, 4);

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
        }
    } 
    private IEnumerator Thunder1()
    {
        yield return new WaitForSeconds(3.0f);

        //Point1の座標を変数posに保存
        var pos = Point1.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea1.gameObject.SetActive(false);

    }
    private IEnumerator Thunder2()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1の座標を変数posに保存
        var pos = Point2.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea2.gameObject.SetActive(false);
    }
    private IEnumerator Thunder3()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1の座標を変数posに保存
        var pos = Point3.gameObject.transform.position;
        //弾のプレハブを作成
        var t = Instantiate(thunder) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        //危険エリア非表示
        DangerArea3.gameObject.SetActive(false);
    }

}
