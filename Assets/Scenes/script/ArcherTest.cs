using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTest : MonoBehaviour
{
    //プレイヤーオブジェクト
    public GameObject player;

    //弾のプレハブオブジェクト
    public GameObject tama;

    Rigidbody2D rb = null;

    //5秒ごとに弾を発射するためのもの
    private float targetTime = 5.0f;
    private float currentTime = 0;

    public int hp = 30;
    public float reactionDistance = 4.0f;//反応距離

    private int A_Hp;

    //主人公の攻撃
    private int rushdamage = 10;    //突進の攻撃力
    private int buresball = 30;     //火球の攻撃力

    private bool inDamage = false;
    private bool isActive = false;


    public EnemyArrow bullet;
    private SpriteRenderer sr = null;

    //アニメーションに使う
    Animator animator; //アニメーター
    bool aniflag = false;
    int aniTime = 0;

    //SE用
    [SerializeField]
    AudioSource archerAudioSource;

    private void Start()
    {
        //Rigidbody2D をとる
        rb = GetComponent<Rigidbody2D>();
        A_Hp = hp;
        sr = GetComponent<SpriteRenderer>();

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
        if (player != null )
        {
            if (isActive && A_Hp > 0 && sr.isVisible)
            {
                // PLAYERの位置を取得
                // Vector3 targetPos = player.transform.position;
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
                    make_naihu();

                    //SE
                    archerAudioSource.Play();

                    //アニメーション
                    animator.Play("Archer");
                    aniflag = true;
                }
            }
            else if(isActive == false)
            {
                //プレイヤーとの距離を求める
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true; //アクティブにする
                }
            }
            //else
            //{
            //    rb.Sleep();
            //}
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

        if(aniflag)//アニメーションを戻すまでの時間を刻む
        {
            aniTime++;
        }

        if (aniTime == 150)//アニメーションを戻す
        {
            animator.Play("StopArcher");
            aniTime = 0;
            aniflag = false;
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
            //SE
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Fireball")
        {
            //ダメージ
            A_Hp -= buresball;
            Debug.Log(A_Hp);
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

    void make_naihu()
    {
        EnemyArrow.Naihu = true;

    }
}
