using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoburin : MonoBehaviour
{

    #region//�C���X�y�N�^�[�Őݒ肷��
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("��ʊO�ł��s������")] public bool nonVisibleAct;
    [Header("�ڐG����")] public EnemyCollisionCheck checkCollision;
    [Header("�S�u������hp")] public int hp;
    #endregion

    #region//�v���C�x�[�g�ϐ�
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
            hp_g -= rushdamage;
            Debug.Log(hp_g);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        //�΋��U���Ƃ̐ڐG
        if(other.gameObject.tag == "Fireball")
        {
            //�_���[�W
            hp_g -= buresball;
            Debug.Log(hp_g);
            inDamage = true;
            Destroy(other.gameObject);
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (hp_g <= 0)
        {
            Debug.Log("�G���|��Ă���");
            //�ړ���~
            rb.velocity = new Vector2(0, 0);
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
}

