using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHeadman : MonoBehaviour
{
    public GameObject bird;//birdを取得
    public GameObject cane;//caneを取得
    public GameObject player;//playerを取得
    public GameObject Object;//HPber用

    private bool stop = false;//動き制御

    private int Headman_HP;//村長のHP
    public int HP = 100;    //最大HP

    //主人公の攻撃
    private int rushdamage = 10;    //突進の攻撃力
    private int buresball = 30;     //火球の攻撃力


    private bool inDamage = false;  //ダメージ判定
    private float reactionDistance = 15.0f;//反応距離

    bool isActive = false;//動き出しフラグ
    private bool OnAttack = false;

    //bird攻撃time関係
    private float create_bird_count = 0.0f;
    private float create_bird_time = 5.0f;

    //cane攻撃time関係
    private float create_cane_count = 0.0f;
    private float create_cane_time = 3.0f;


    //アニメション
    Animator animator; //アニメーター
    public string attackanime = "EnemyHeadmanAttack";//鳥攻撃のモーション
    public string Caneanime = "EnemyHeadmanCane";    //杖攻撃のモーション
    public string Stopanime = "EnemyHeadmanStop";    //静止モーション
    public string Downanime = "EnemyHeadmanDown";    //ダウンモーション

    //SE用斬撃
    [SerializeField]
    AudioSource swordAudioSource;
    //SE用杖たたく
    [SerializeField]
    AudioSource tueAudioSource;

    GameObject[] tagObjects;//オブジェクトカウント用

    //フェード用
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    //パーティクル用
    static public bool particleonV = false;
    //ゆっくりけす
    private byte transparent_count;

    //HPバーのシェイダー
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        Object.gameObject.SetActive(false);
        stop = true;
        animator = GetComponent<Animator>();
        Headman_HP = HP;//最大HPを設定
        OnAttack = true;
        transparent_count = 255;

        slider.value = 1;
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
                if (isActive && Headman_HP > 0)
                {

                    create_bird_count += Time.deltaTime;//カウント
                    create_cane_count += Time.deltaTime;//カウント
                                                        //Debug.Log(create_bird_count);
                    if (OnAttack)
                    {
                        if (create_bird_count > create_bird_time)
                        {
                            StartCoroutine(Bird_attack());//コルーチン開始
                        }
                        else if (create_cane_count > create_cane_time)
                        {
                            StartCoroutine(Cane_attack());//コルーチン開始
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
                        Object.gameObject.SetActive(true);//HPバーをアクティブにする
                    }
                }
            }
            else if (isActive)
            {
                isActive = false;//非アクティブ
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
    }
    
    //コルーチン
    private IEnumerator Bird_attack()
    {
        create_bird_count = 0.0f;//リセット
        OnAttack = false;

        //アニメション
        animator.Play(attackanime);
        //SE
        tueAudioSource.Play();

        yield return new WaitForSeconds(0.2f);//0.2静止
        animator.Play(Stopanime);
        
        for(int i =0; i<10;i++)
        {
            if (HeadmanBird.HeadmanDown)
            {
                yield break;
            }
            //Debug.Log("鳥生成");
            float vecX = Random.Range(128f, 147f);//ランダムを（）範囲で設定
            var t = Instantiate(bird) as GameObject;//オブジェクトを作成

            //弾のプレハブの位置を敵の位置にする
            t.transform.position = new Vector3(vecX, 10.0f, 0);
            
            t.AddComponent<HeadmanBird>();//birdの動きを決める
            
            yield return new WaitForSeconds(0.2f);
        }
    
        yield return new WaitForSeconds(0.5f);//0.5静止

        OnAttack = true;
        create_cane_count = 2.0f;//リセット

        //Debug.Log("終了");
        yield break;//コルーチン終了
        
    }
    private IEnumerator Cane_attack()
    {
        //アニメーション
        animator.Play(Caneanime);

        //SE
        swordAudioSource.Play();

        yield return new WaitForEndOfFrame();//１フレーム静止
        var pos = this.gameObject.transform.position + transform.up *2.0f - transform.right*2.0f; //位置設定
        Instantiate(cane, pos, Quaternion.identity);//作成

        create_cane_count =0.0f;//リセット

        yield return new WaitForSeconds(0.5f);//０．５静止
        animator.Play(Stopanime);

        yield break;//コルーチン終了

    }
    //接触管理
        private void OnTriggerEnter2D(Collider2D collider)
        {
        Debug.Log("OntriggerEnter2D:" + collider.gameObject.name);

        //突進攻撃との接触
        if (collider.gameObject.tag == "rushWall")
        {
            //ダメージ
            Headman_HP -= rushdamage;
            Debug.Log(Headman_HP);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();

            slider.value = (float)Headman_HP / (float)HP;
        }
        if (collider.gameObject.tag =="Fireball")
        {
            //ダメージ
            Headman_HP -= buresball;
            Debug.Log(Headman_HP);
            Destroy(collider.gameObject);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
            slider.value = (float)Headman_HP / (float)HP;
        }
        EnemyDamage();//倒れているか調べる
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Headman_HP <= 0)
        {
            TimeCounter.BossdownT = false;//タイめーを止めるフラグ
            Debug.Log("敵が倒れている");
            PlayerController.VillageBoss = true;//村ステージクリア
            PlayerController.stop = false;      //主人公の動きを止める
            PlayerController.gameState = "gameclear";//クリア
            HeadmanBird.HeadmanDown = true;     //downフラグをあげる
            stop = false;//村長停止
            StartCoroutine(Bossdown());//downコルーチン
        }
    }
    IEnumerator Bossdown()
    {
        Debug.Log("ゲームクリア");

        yield return new WaitForSeconds(0.2f);
        this.enabled = false;
        //Destroy(gameObject, 0.2f);//0.2かけて敵を消す

        particleonV = true;
        Global.Clear = true;
        animator.Play("EnemyHeadmanDown");
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


}
