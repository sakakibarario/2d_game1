using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropBall : MonoBehaviour
{
    public GameObject ball;
    //弾のプレハブオブジェクト
    public GameObject tama;


    private float count = 1.0f;
    private int vecX;
    public int hp = 50;
    public float reactionDistance = 8.0f;//反応距離
    private float targetTime = 5.0f;
    private float currentTime = 0;


    private int Torent_Hp;

    //主人公の攻撃
    private int rushdamage = Global.GRush;
    private int buresball = Global.GBures;

    private bool inDamage = false;
    private bool isActive = false;

    //SE用
    [SerializeField]
    AudioSource leafAudioSource;

    [SerializeField]
    AudioSource rootAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        Torent_Hp = hp;
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
            if (isActive && Torent_Hp > 0)
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
                    make_naihu();
                    //SE葉っぱ
                    leafAudioSource.Play();
                }

                count -= Time.deltaTime;
                if (count <= 0)
                {
                    vecX = Random.Range(210, 229);

                    Instantiate(ball, new Vector3(vecX, -4.3f, 5), Quaternion.identity);

                    //SE
                    rootAudioSource.Play();

                    count = 2.0f;
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
            Torent_Hp -= rushdamage;
            Debug.Log(Torent_Hp);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Fireball")
        {
            //ダメージ
            Torent_Hp -= buresball;
            Debug.Log(Torent_Hp);
            inDamage = true;
            Destroy(other.gameObject);//当たったブレスを消す
        }
        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Torent_Hp <= 0)
        {
            Debug.Log("敵が倒れている");
            PlayerController.SougenBoss = true;

            PlayerController.gameState = ("gameclear");

            Debug.Log("ゲームクリア");
            SceneManager.LoadScene("GameClear");
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
        EnemyBossGan.Naihu = true;
    }
}




