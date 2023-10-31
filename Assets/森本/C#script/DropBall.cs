using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropBall : MonoBehaviour
{
    public GameObject ball;
    private float count = 1.0f;
    private int vecX;
    public int hp = 100;
    public float reactionDistance = 4.0f;//反応距離

    private int Torent_Hp;

    private int rushdamage = 10;
    private bool inDamage = false;
    private bool isActive = false;

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
                // PLAYERの位置を取得
                Vector2 targetPos = player.transform.position;
                // PLAYERのx座標
                float x = targetPos.x;
                // ENEMYは、地面を移動させるので座標は常に0とする
                float y = 0;
                // 移動を計算させるための２次元のベクトルを作る
                Vector2 direction = new Vector2(
                    x - transform.position.x, y).normalized;
                ////1秒経つごとに弾を発射
                //currentTime += Time.deltaTime;

                //if (targetTime < currentTime)
                //{
                //    currentTime = 0;
                //    //敵の座標を変数posに保存
                //    var pos = this.gameObject.transform.position;

                //    //弾のプレハブを作成
                //    var t = Instantiate(tama) as GameObject;

                //    //弾のプレハブの位置を敵の位置にする
                //    t.transform.position = pos;

                //    //敵からプレイヤーに向かうベクトルを作る
                //    //プレイヤーの位置から敵の位置(弾の位置)を引く
                //    Vector2 vec = player.transform.position - pos;

                //    //弾のRigidBody2Dコンポネントのvelocityにさっき求めたベクトルを入れて力を加える
                //    t.GetComponent<Rigidbody2D>().velocity = vec;
                //}
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
        count -= Time.deltaTime;
        if(count <= 0)
        {
            vecX = Random.Range(210, 229);

            Instantiate(ball, new Vector3(vecX, -4.3f, 0), Quaternion.identity);

            count = 1.0f;
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
    }
    EnemyDamage();//倒れているか調べる
}

void EnemyDamage()
{
    Invoke("DamageEnd", 0.25f);
    if (Torent_Hp <= 0)
    {
        Debug.Log("敵が倒れている");
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
}




