using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody�^�̕ϐ�
    float axisH = 0.0f;         //����
    public LayerMask GroundLayer;



    public float speed = 3.0f;  //�ړ����x
    public float jump = 5.0f;   //�W�����v��
    public float rush = 2.0f;   //�ːi�̗�
    public int D_HP;          //�h���S����HP
    public int S_D_HP = 50;     //�����ł̃h���S��HP

    public static string gameState;//�Q�[���̏��

    //�G�̍U��
    public int Suraimu = 5;    //�X���C���̃_���[�W
    public int Goburin = 5;   //�S�u�����̃_���[�W

    //�t���O
    bool gojump = false;       //�W�����v����
    bool ongrond = false;       //�n�ʔ���
    public static bool gorush = false;       //�U������(�ːi)
    bool horizon = false;       //����
    bool inDamage = false;      //�_���[�W���t���O

    //�N�[���^�C��
    public bool isCountDown = true;//true = ���Ԃ��J�E���g�_�E���v�Z����
    public bool AnimeCount = true;
    float rush_time = 5.0f;          //�U��(�ːi)�N�[���^�C��
    public bool isTimeOver = false;//true = �^�C�}�[��~
    public bool animeOver = true;
    public float displayTime = 0;  //�\������
    public float Animetime = 0;
    public float animerushtime = 2.0f;

    float times = 0;               //���ݎ���
    float Anitimes = 0;

    //�A�j���[�V�����Ή�
    Animator animator; //�A�j���[�^�[
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string rushAnime = "PlayerRush";
    string nowAnime = "";
    string oldAnime = "";

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D�������Ă���
        rb = GetComponent<Rigidbody2D>();

        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        //�Q�[���̏�Ԃ��v���C���ɂ���
        gameState = "playing";

        D_HP = S_D_HP;

        if (isCountDown)
        {
            //�J�E���g�_�E��
            displayTime = rush_time;
        }
        if(AnimeCount)
        {
            Animetime = animerushtime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[�����ȊO�ƃ_���[�W���͉������Ȃ�
        if (gameState != "playing" || inDamage)
        {
            return;
        }

        //���������̃`�F�b�N
        axisH = Input.GetAxisRaw("Horizontal");

        //�����̒���
        if (axisH > 0.0f)
        {
            //�E�ړ�
            // Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(5, 5);
            horizon = true;
        }
        if (axisH < 0.0f)
        {
            //���ړ�
            //Debug.Log("���ړ�");
            transform.localScale = new Vector2(-5, 5);
            horizon = false;
        }
        //�L�����N�^�[�̃W�����v
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //�L�����N�^�[�̓ːi�U��
        if (ongrond)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Rush();
            }
        }
        if (isTimeOver == false)//���Ԍo��
        {
            times += Time.deltaTime;//�o�ߎ��Ԃ����Z
            if (isCountDown)
            {
                //�J�E���g�_�E��
                displayTime = rush_time - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;  //�t���O�����낷
                }
            }
            //  Debug.Log("TIMES:" + displayTime);
        }
    }



    private void FixedUpdate()
    {
        //�n�㔻��
        ongrond = Physics2D.Linecast(transform.position,
                                     transform.position - (transform.up * 0.1f),
                                     GroundLayer);

        //�Q�[�����ȊO�͉������Ȃ�
        if (gameState != "playing")
        {
            return;
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

        if (ongrond || axisH != 0 || gorush == true)
        {
            //�n��or���x���O�ł͂Ȃ�or�U�����ł͂Ȃ�
            //���x���X�V
            rb.velocity = new Vector2(axisH * speed, rb.velocity.y);
        }
        if (ongrond && gojump)
        {
            //�n�ォ�W�����v�L�[�������ꂽ�Ƃ�
            //�W�����v����
            Debug.Log("�W�����v");
            Vector2 jumpPw = new Vector2(0, jump);      //�W�����v������x�N�g��
            rb.AddForce(jumpPw, ForceMode2D.Impulse);   //�u�ԓI�ȗ͂�������
            gojump = false; //�W�����v�t���O�����낷
        }

        if (gorush && horizon == true)
        {
            //�n�ォ���N���b�N�������ꂽ�Ƃ����E����
            //�ːi����
            Debug.Log("�ːi");



            Vector2 rushPw = new Vector2(rush, 0);
            rb.AddForce(rushPw, ForceMode2D.Impulse);

            if (animeOver == false)//���Ԍo��
            {
                Anitimes += Time.deltaTime;//�o�ߎ��Ԃ����Z
                if (AnimeCount)
                {
                    //�J�E���g�_�E��
                    Animetime = animerushtime - Anitimes;
                    if (Animetime <= 0.0f)
                    {
                        Animetime = 0.0f;
                        animeOver = true;  //�t���O�����낷
                        gorush = false; //�U���t���O�����낷
                        displayTime = rush_time;
                        Animetime = animerushtime;
                        isTimeOver = false;
                        Anitimes = 0;
                        times = 0;
                    }
                   
                }
                Debug.Log("TIMES:" + Animetime);
            }
        }
        else if (gorush && horizon == false)
        {
            //�n�ォ���N���b�N�������ꂽ�Ƃ���������
            //�ːi����
            Debug.Log("�ːi");



            Vector2 rushPw = new Vector2(-rush, 0);
            rb.AddForce(rushPw, ForceMode2D.Impulse);

            if (animeOver == false)//���Ԍo��
            {
                Anitimes += Time.deltaTime;//�o�ߎ��Ԃ����Z
                if (AnimeCount)
                {
                    //�J�E���g�_�E��
                    Animetime = animerushtime - Anitimes;
                    if (Animetime <= 0.0f)
                    {
                        Animetime = 0.0f;
                        animeOver = true;  //�t���O�����낷
                        gorush = false; //�U���t���O�����낷
                        displayTime = rush_time;
                        Animetime = animerushtime;
                        isTimeOver = false;
                        Anitimes = 0;
                        times = 0;
                    }

                }
                Debug.Log("TIMES:" + Animetime);
            }
        }
    
        if (ongrond)
        {
            //�n��̂���
            if (axisH == 0)
            {
                nowAnime = stopAnime; //��~��
            }
            else
            {
                nowAnime = moveAnime; //�ړ�
            }
            if (gorush)
            {
                nowAnime = rushAnime;
                Debug.Log("�A�j���[�V�����I");
            }
        }
        else
        {
            //��
            nowAnime = jumpAnime;
        }



        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    //�A�j���[�V�����Đ�
        }
    }
    void Jump()//�W�����v
    {
        gojump = true; //�W�����v�t���O�𗧂Ă�
    }
    void Rush()//�ːi
    {
        if (displayTime == 0)
        {
            gorush = true; //�U��(�ːi)�t���O�𗧂Ă�
            animeOver = false;

        }



    }
    //�ڐG�J�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "damege_s")//�X���C��
        {
            Debug.Log("�ڐG");
            if (gorush == false)
            {
                D_HP -= Suraimu;    //HP�����炷
                GetDamage(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "damage_g")//�S�u����
        {
            Debug.Log("�ڐG");
            if (gorush == false)
            {
                D_HP -= Goburin;    //HP�����炷
                GetDamage(collision.gameObject);
            }
        }
        if (collision.CompareTag("tama"))
        {
                Destroy(collision.gameObject);
        }
    }
    //�_���[�W
    public void GetDamage(GameObject @object)
    {

        Debug.Log("Player HP" + D_HP);
        if (D_HP > 0)
        {
            //�ړ���~
            rb.velocity = new Vector2(0, 0);
            //�G�L�����̔��Α��Ƀq�b�g�o�b�N������
            Vector3 v = (transform.position - @object.transform.position).normalized;
            rb.AddForce(new Vector2(v.x * 5, v.y * 5), ForceMode2D.Impulse);
            //�_���[�W�t���OON
            inDamage = true;
            Invoke("DamageEnd", 0.25f);
        }
        else
        {
            //�Q�[���I�[�o�[
            GameOver();
        }
    }
    //�_���[�W�I��
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g���ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    //�Q�[���I�[�o�[
    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
        gameState = "gameover";
    }
}