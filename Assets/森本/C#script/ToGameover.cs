using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ToGameover : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Gameover");
        }
    }
    //Enemy�^�O�Ɠ����������Q�[���I�[�o�[��ʂɈړ�
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag=="Enemy")
    //    {
    //        SceneManager.LoadScene("Gameover");

    //    }
    //}

}
