using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    //�b�J�E���g�_�E��
    private float countdownsecond = 0;

    //���J�E���g�_�E��
    public int countdownminute = 1;


    //���Ԃ�\������Text�^�̕ϐ�
    public Text timeText;

    //�|�[�Y���Ă邩�ǂ���
    public static bool isPose = false;

    //Uodate is called once per frame
    void Update()
    {
        //�N���b�N���ꂽ�Ƃ�
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //�|�[�Y���ɃN���b�N���ꂽ�Ƃ�
            if(isPose)
            {
                //�|�[�Y��Ԃ���������
                isPose = false;
                PlayerController.pose = false;
                //SE
                GetComponent<AudioSource>().Play();
            }
            //�i�s���ɃN���b�N���ꂽ�Ƃ�
            else
            {
                //�|�[�Y��Ԃɂ���
                isPose = true;
                PlayerController.pose = true;
                //SE
                GetComponent<AudioSource>().Play();
            }
        }

        //�|�[�Y�����ǂ���
        if(isPose)
        {
            //�|�[�Y���ł��邱�Ƃ�\��
            //timeText.text = "�|�[�Y��";
          

            //�J�E���g�_�E�����Ȃ�
            return;
        }
        
            //���Ԃ��J�E���g����
            countdownsecond -= Time.deltaTime;

            if (countdownsecond <=0 && countdownminute != 0)
            {
                countdownminute--;
                countdownsecond = 60.0f;
            }


            //���Ԃ�\������
            timeText.text = countdownminute.ToString("00") + ":" + countdownsecond.ToString("f2");
        

        //countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdownsecond<=0 && countdownminute <= 0)
        {
            timeText.text = "GAME OVER";
            PlayerController.gameState = "gameover";
            SceneManager.LoadScene("Gameover");
        }
    }
}
