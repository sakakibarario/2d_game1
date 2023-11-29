using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoburin : MonoBehaviour
{

    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("ゴブリンのhp")] public int hp;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private int hp_g;
    private int rushdamage = Global.GRush;
    private int buresball = Global.GBures;

    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool inDamage = false;

    int xVector;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        col = GetComponent<BoxCollider2D>();

        hp_g = hp;
    }

    void FixedUpdate()
    {
        if (PlayerController.gameState != "playing")
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        if (sr.isVisible || nonVisibleAct)
            {
                if (checkCollision.isOn)
                {
                  rightTleftF = true;
                xVector = 1;
            }
                else if(checkCollision.isOn == false)
                {
                     rightTleftF = false;
                 xVector = -1;
            }
               
                if (rightTleftF)
                {
                    
                    transform.localScale = new Vector3(-7, 7, 1);
                }
                else
                {
                    transform.localScale = new Vector3(7, 7, 1);
                }
                rb.velocity = new Vector2(xVector * speed, -gravity);
            }
            else
            {
                rb.Sleep();
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
            hp_g -= rushdamage;
            Debug.Log(hp_g);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        //火球攻撃との接触
        if(other.gameObject.tag == "Fireball")
        {
            //ダメージ
            hp_g -= buresball;
            Debug.Log(hp_g);
            inDamage = true;
            Destroy(other.gameObject);
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (hp_g <= 0)
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
}

