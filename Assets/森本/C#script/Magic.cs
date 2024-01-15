using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    //プレイヤーオブジェクト
    public GameObject player;
    private SpriteRenderer sr = null;

    //弾のプレハブオブジェクト
    public GameObject tama;

    Rigidbody2D rb;

    //4秒ごとに弾を発射するためのもの
    private float targetTime = 4.0f;
    private float currentTime = 0;

    public int hp = 20;
    public float reactionDistance = 4.0f;//反応距離

    private int C_Hp;

    //主人公の攻撃
    private int rushdamage = 10;    //突進の攻撃力
    private int buresball = 30;     //火球の攻撃力

    private bool inDamage = false;
    private bool isActive = false;


    //SE用
    [SerializeField]
    AudioSource tamaAudioSource;

    //アニメーションに使う
    Animator animator; //アニメーター
    float kamaetime = 2.0f;//構えを取る時間をクールタイムから引く秒数


    private void Start()
    {
        //Rigidbody2D をとる
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        C_Hp = hp;

        //Animator をとってくる
        animator = GetComponent<Animator>();

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
            if (isActive && C_Hp > 0 && sr.isVisible)
            {
                // PLAYERの位置を取得
                // Vector3 targetPos = player.transform.position;
                currentTime += Time.deltaTime;

                //アニメーション 攻撃前ののモーション
                if (currentTime > (targetTime - kamaetime))
                {
                    animator.Play("kamaeMagic");
                }

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
                    //SE
                    tamaAudioSource.Play();

                    //アニメーション 通常
                    animator.Play("Magic");

                }
            }
            else if (isActive == false)
            {
                //プレイヤーとの距離を求める
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    Debug.Log("アクティブ");
                    isActive = true; //アクティブにする
                }
            }
            else
            {
                rb.Sleep();//停止
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
            C_Hp -= rushdamage;
            Debug.Log(C_Hp);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Fireball")
        {
            //ダメージ
            C_Hp -= buresball;
            Debug.Log(C_Hp);
            Destroy(other.gameObject);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (C_Hp <= 0)
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
}
