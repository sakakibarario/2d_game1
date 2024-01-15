using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    //プレイヤーオブジェクト
    public GameObject player;

    //弾のプレハブオブジェクト
    public GameObject tama;

    Rigidbody2D rb;

    //1秒ごとに弾を発射するためのもの
    private float targetTime = 5.0f;
    private float currentTime = 0;

    public int hp = 30;
    public float reactionDistance = 4.0f;//反応距離

    private int T_Hp;

    //主人公の攻撃
    int buresball = Global.GBures;
    int rushdamage = Global.GRush;
    

    private bool inDamage = false;
    private bool isActive = false;


    public Enemygan bullet;

    //SE用
    [SerializeField]
    AudioSource ThrowAudioSource;

    //アニメーションに使う
    Animator animator; //アニメーター
    float kamaetime = 1.0f;//構えを取る時間をクールタイムから引く秒数
    float knifetime = 3.0f;//ナイフを持つ時間をクールタイムから引く秒数
    bool knifeflag = true;

    private void Start()
    {
        //Rigidbody2D をとる
        rb = GetComponent<Rigidbody2D>();
        T_Hp = hp;

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
            if (isActive && T_Hp > 0)
            {
                // PLAYERの位置を取得
               // Vector3 targetPos = player.transform.position;
                currentTime += Time.deltaTime;

                //アニメーション ナイフを持つ
                if(currentTime > (targetTime - knifetime)&& knifeflag )//2.0f
                {
                    animator.Play("KnifeThief");
                    knifeflag = false;
                }

                //アニメーション 攻撃前ののモーション
                if (currentTime > (targetTime - kamaetime)&& knifeflag == false )//4.0f
                {
                    animator.Play("kamaeThief");
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
                    make_naihu();

                    //SE 
                    ThrowAudioSource.Play();

                    //アニメーション 通常
                    animator.Play("StopThief");
                    knifeflag = true;

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
            T_Hp -= rushdamage;
            Debug.Log(T_Hp);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Fireball")
        {
            //ダメージ
            T_Hp -= buresball;
            Debug.Log(T_Hp);
            inDamage = true;
            Destroy(other.gameObject);//当たったブレスを消す
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (T_Hp <= 0)
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
        Enemygan.Naihu = true;

    }
}
