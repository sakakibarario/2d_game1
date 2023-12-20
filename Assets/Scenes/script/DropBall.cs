using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropBall : MonoBehaviour
{
    public GameObject ball;
    //弾のプレハブオブジェクト
    public GameObject tama;

    //root攻撃の座標取得オブジェクト
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject Point21;
    public GameObject Point22;
    public GameObject Point23;
    public GameObject Point31;
    public GameObject Point32;
    public GameObject Point33;
    public GameObject Point41;
    public GameObject Point42;
    public GameObject Point43;
    public GameObject DangerArea1;
    public GameObject DangerArea2;
    public GameObject DangerArea3;
    public GameObject DangerArea4;

    private float count = 5.0f;     //root作成カウント用
    public int hp = 50;             //ＭＡＸｈｐ
    public float reactionDistance = 8.0f;//反応距離
    private float targetTime = 5.0f;
    private float currentTime = 0;
    private bool stop = false;//動き制御

    private int Torent_Hp;

    //主人公の攻撃
    private int rushdamage = Global.GRush;
    private int buresball = Global.GBures;

    private bool inDamage = false;
    private bool isActive = false;
    private bool isLeafAttack = false;

    //SE用
    [SerializeField]
    AudioSource leafAudioSource;

    [SerializeField]
    AudioSource rootAudioSource;

    //パーティクル用
    static public bool particleon = false;

    //フェード用
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;
    public byte transparent_count;

    //根っこ用りすと
    int start = 1;
    int end = 4;
    List<int> numbers = new List<int>();
    bool random = false;
    public int i = 0;
    private int rnd = 0;
    bool DamageT = false;

    //アニメーション用
    Animator animator; //アニメーター
    public string stopAnime = "EnemyTrentoStop";
    public string downAnime = "EnemyTrentoDown";

    // Start is called before the first frame update
    void Start()
    {
        Torent_Hp = hp;
        stop = true;
        DamageT = true;
        count = 2.0f;
        transparent_count = 255;

        //Animator をとってくる
        animator = GetComponent<Animator>();

        for (int i = start; i <= end; i++)//リストの代入
        {
            numbers.Add(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState != "playing")
        {
            return;
        }
        if (stop)
        {
            //Player　のゲームオブジェクトを得る
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (isActive && Torent_Hp > 0)
                {
                    currentTime += Time.deltaTime;//かうんとアップ

                    if (targetTime < currentTime)
                    {
                        StartCoroutine(LeafAttack());                        
                    }

                    if (DamageT)
                    {
                        count -= Time.deltaTime;
                        if (count < 0)
                        {
                            Debug.Log("開始");
                            
                            StartCoroutine(RootRandom());
                        }
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

            if(isLeafAttack)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 3, 255);//赤色に点滅
                //回復中点滅させる
                float val = Mathf.Sin(Time.time * 10);
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
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//倒れているか調べる
    }
    


    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Torent_Hp <= 0)
        {
            TimeCounter.BossdownT = false;//タイめーを止めるフラグ
            Debug.Log("敵が倒れている");
            DangerArea1.gameObject.SetActive(false);//危険エリアを消す
            DangerArea2.gameObject.SetActive(false);//危険エリアを消す
            DangerArea3.gameObject.SetActive(false);//危険エリアを消す
            DangerArea4.gameObject.SetActive(false);//危険エリアを消す
            stop = false;                   //ボスの動きを止める
            this.enabled = false;           //機能を消す
            PlayerController.stop = false;  //主人公の動きを止める
            PlayerController.gameState = "gameclear";
            PlayerController.SougenBoss = true;//フラグをあげる
            StartCoroutine(Bossdown());//ボスdown時の挙動
        }
    }
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライト元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator Bossdown()
    {
        Debug.Log("ゲームクリア");

        yield return new WaitForSeconds(0.2f);
        animator.Play(downAnime);
        //Destroy(gameObject, 0.2f);//0.2かけて敵を消す

        ////パーティクル開始--------------------
        particleon = true;
        Global.Clear = true;
        for (; transparent_count > 0; transparent_count--)
        {
            //ボスをゆっくり消す
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, transparent_count);
            Debug.Log(transparent_count);
            yield return new WaitForSeconds(0.01f);
            if (transparent_count == 1)
            {
                yield return new WaitForSeconds(3.0f);
                Destroy(gameObject);
                //yield return new WaitForSeconds(5.5f);
                Initiate.Fade(sceneName, fadeColor, fadeSpeed);
                yield break;
            }
        }
        
        ////------------------------------------

       
    }

    void make_naihu()
    {
        EnemyBossGan.Naihu = true;
    }
    IEnumerator LeafAttack()
    {
        currentTime = 0;
        //isLeafAttack = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 10, 3, 255);//赤色に点滅

        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

        //フラグおろす
        //isLeafAttack = false;

        //スプライトをもとに戻す
        //gameObject.GetComponent<SpriteRenderer>().enabled = true;
        
        yield return new WaitForSeconds(0.2f);


        //敵の座標を変数posに保存
        var pos = this.gameObject.transform.position + transform.up * -1.7f;
        //弾のプレハブを作成
        var t = Instantiate(tama) as GameObject;
        //弾のプレハブの位置を敵の位置にする
        t.transform.position = pos;
        t.AddComponent<TorentLeaf>();
        //make_naihu();
        //SE葉っぱ
        leafAudioSource.Play();
        //currentTime = 0;

        yield break;
    }
    IEnumerator RootRandom()
    {
        if (stop)
        {
            count = 5.0f;
            DamageT = false;

            for (i = 0; i < 4; i++)
            {
                int index = Random.Range(0, numbers.Count);//ランダム取得

                rnd = numbers[index];//使えるように
                Debug.Log(rnd);

                numbers.RemoveAt(index);//取得した情報を消す

                if (rnd == 1)
                {
                    if (stop)
                    {
                        //危険エリア表示
                        DangerArea1.gameObject.SetActive(true);
                        //コルーチン呼び出し
                        StartCoroutine(Root1());
                    }
                }
                if (rnd == 2)
                {
                    if (stop)
                    {
                        //危険エリア表示
                        DangerArea2.gameObject.SetActive(true);
                        //コルーチン呼び出し
                        StartCoroutine(Root2());
                    }
                }
                if (rnd == 3)
                {
                    if (stop)
                    {
                        //危険エリア表示
                        DangerArea3.gameObject.SetActive(true);
                        //コルーチン呼び出し
                        StartCoroutine(Root3());
                    }
                }
                if (rnd == 4)
                {
                    if (stop)
                    {
                        //危険エリア表示
                        DangerArea4.gameObject.SetActive(true);
                        //コルーチン呼び出し
                        StartCoroutine(Root4());
                    }
                }
                yield return new WaitForSeconds(1.0f);//待機
                if (i == 3)
                {
                    random = true;  //リスト初期化の開始
                    DamageT = true;
                    yield break;
                }
            }
        }
        else
        {
            yield break;
        }

    }
    private IEnumerator Root1()
    {
        if (stop)
        {
            yield return new WaitForSeconds(2.0f);//待機

            //Point1の座標を変数posに保存
            var pos1 = Point1.gameObject.transform.position;
            var pos2 = Point2.gameObject.transform.position;
            var pos3 = Point3.gameObject.transform.position;

            //弾のプレハブを作成
            var t1 = Instantiate(ball) as GameObject;
            var t2 = Instantiate(ball) as GameObject;
            var t3 = Instantiate(ball) as GameObject;
            //弾のプレハブの位置を敵の位置にする
            //SE
            if (stop)
            {
                rootAudioSource.Play();
                t1.transform.position = pos1;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t2.transform.position = pos2;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t3.transform.position = pos3;
                yield return new WaitForSeconds(0.2f);
                //危険エリア非表示
                DangerArea1.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator Root2()
    {
        if (stop)
        {


            yield return new WaitForSeconds(2.0f);//待機

            //Point1の座標を変数posに保存
            var pos1 = Point21.gameObject.transform.position;
            var pos2 = Point22.gameObject.transform.position;
            var pos3 = Point23.gameObject.transform.position;

            //弾のプレハブを作成
            var t1 = Instantiate(ball) as GameObject;
            var t2 = Instantiate(ball) as GameObject;
            var t3 = Instantiate(ball) as GameObject;
            if (stop)
            {
                //弾のプレハブの位置を敵の位置にする
                //SE
                rootAudioSource.Play();
                t1.transform.position = pos1;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t2.transform.position = pos2;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t3.transform.position = pos3;
                yield return new WaitForSeconds(0.2f);
                //危険エリア非表示
                DangerArea2.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator Root3()
    {
        if (stop)
        {


            yield return new WaitForSeconds(2.0f);//待機

            //Point1の座標を変数posに保存
            var pos1 = Point31.gameObject.transform.position;
            var pos2 = Point32.gameObject.transform.position;
            var pos3 = Point33.gameObject.transform.position;

            //弾のプレハブを作成
            var t1 = Instantiate(ball) as GameObject;
            var t2 = Instantiate(ball) as GameObject;
            var t3 = Instantiate(ball) as GameObject;
            if (stop)
            {
                //弾のプレハブの位置を敵の位置にする
                //SE
                rootAudioSource.Play();
                t1.transform.position = pos1;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t2.transform.position = pos2;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t3.transform.position = pos3;
                yield return new WaitForSeconds(0.2f);
                //危険エリア非表示
                DangerArea3.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator Root4()
    {
        if (stop)
        {


            yield return new WaitForSeconds(2.0f);//待機

            //Point1の座標を変数posに保存
            var pos1 = Point41.gameObject.transform.position;
            var pos2 = Point42.gameObject.transform.position;
            var pos3 = Point43.gameObject.transform.position;

            //弾のプレハブを作成
            var t1 = Instantiate(ball) as GameObject;
            var t2 = Instantiate(ball) as GameObject;
            var t3 = Instantiate(ball) as GameObject;
            if (stop)
            {
                //弾のプレハブの位置を敵の位置にする
                //SE
                rootAudioSource.Play();
                t1.transform.position = pos1;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t2.transform.position = pos2;
                yield return new WaitForSeconds(0.2f);
                //SE
                rootAudioSource.Play();
                t3.transform.position = pos3;
                yield return new WaitForSeconds(0.2f);
                //危険エリア非表示
                DangerArea4.gameObject.SetActive(false);
            }
        }
    }
}




