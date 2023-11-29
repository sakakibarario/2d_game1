using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropBall : MonoBehaviour
{
    public GameObject ball;
    //�e�̃v���n�u�I�u�W�F�N�g
    public GameObject tama;


    private float count = 1.0f;
    private int vecX;
    public int hp = 50;
    public float reactionDistance = 8.0f;//��������
    private float targetTime = 5.0f;
    private float currentTime = 0;


    private int Torent_Hp;

    //��l���̍U��
    private int rushdamage = Global.GRush;
    private int buresball = Global.GBures;

    private bool inDamage = false;
    private bool isActive = false;

    //SE�p
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
        //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && Torent_Hp > 0)
            {
                currentTime += Time.deltaTime;

                if (targetTime < currentTime)
                {
                    currentTime = 0;
                    //�G�̍��W��ϐ�pos�ɕۑ�
                    var pos = this.gameObject.transform.position;
                    //�e�̃v���n�u���쐬
                    var t = Instantiate(tama) as GameObject;
                    //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
                    t.transform.position = pos;
                    make_naihu();
                    //SE�t����
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
                //�v���C���[�Ƃ̋��������߂�
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true; //�A�N�e�B�u�ɂ���
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
        }

        if (inDamage)
        {
            //�_���[�W���_�ł�����
            float val = Mathf.Sin(Time.time * 50);
            // Debug.Log(val);
            if (val > 0)
            {
                //�X�v���C�g��\��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //�X�v���C�g���\��
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;//�_���[�W���͑���ɂ��ړ��͂����Ȃ�
        }
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (other.gameObject.tag == "rushWall")
        {
            //�_���[�W
            Torent_Hp -= rushdamage;
            Debug.Log(Torent_Hp);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Fireball")
        {
            //�_���[�W
            Torent_Hp -= buresball;
            Debug.Log(Torent_Hp);
            inDamage = true;
            Destroy(other.gameObject);//���������u���X������
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Torent_Hp <= 0)
        {
            Debug.Log("�G���|��Ă���");
            PlayerController.SougenBoss = true;

            PlayerController.gameState = ("gameclear");

            Debug.Log("�Q�[���N���A");
            SceneManager.LoadScene("GameClear");
            Destroy(gameObject, 0.2f);//0.2�����ēG������

        }
    }
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g���ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    void make_naihu()
    {
        EnemyBossGan.Naihu = true;
    }
}




