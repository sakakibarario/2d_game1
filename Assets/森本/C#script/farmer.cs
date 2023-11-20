using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farmer : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 0.5f;      //速度
    public int hpMax = 10;          //農民のHP
    public float reactionDistance = 4.0f;//反応距離
    private int hp;

    private int rushdamage = 10;    //突進の攻撃力
    private bool inDamage = false;  //ダメージ判定

    bool isActive = false;

    public GameObject explode;  //エフェクト用

    public Transform Point;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D をとる
        rb = GetComponent<Rigidbody2D>();
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState != "playing")
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        //Player　のゲームオブジェクトを得る
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (isActive && hp > 0)
            {
                // PLAYERの位置を取得
                Vector2 targetPos = player.transform.position;
                // PLAYERのx座標
                float x = targetPos.x;
                // ENEMYは、地面を移動させるので座標は常に0とする
                float y = 0;
                // 移動を計算させるための２次元のベクトルを作る
                Vector2 direction = new Vector2(
                    x - transform.position.x, y).normalized;
                // ENEMYのRigidbody2Dに移動速度を指定する
                rb.velocity = direction * speed;
                //  Debug.Log("農民ムーブ");

                //反転
                if(transform.position.x < player.transform.position.x)
                {
                    transform.localScale = new Vector3(-5, 5, 1);
                    explode.transform.localScale = new Vector3(-3, 3, 1);
                }
                else if(transform.position.x == player.transform.position.x)
                {
                    transform.localScale = transform.localScale;
                    explode.transform.localScale = new Vector3(3, 3, 1);

                }
                else if(transform.position .x > player.transform.position.x)
                {
                    transform.localScale = new Vector3(5, 5, 1);
                    explode.transform.localScale = new Vector3(3, 3, 1);

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
            rb.velocity = Vector2.zero;
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
        // Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //突進攻撃との接触
        if (other.gameObject.tag == "rushWall")
        {
            //ダメージ
            hp -= rushdamage;
            inDamage = true;

        }
        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (hp <= 0)
        {
            Debug.Log("敵が倒れている");
            //移動停止
            rb.velocity = new Vector2(0, 0);
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

    //爆発エフェクト
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//Playerに当たったら
        {
            //ぶつかった位置にexplodeというprefabを配置する　斬撃エフェクト
            Instantiate(explode, Point.transform.position, Quaternion.identity);
            
        }

    }
}

