using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody型の変数
    float axisH = 0.0f;         //入力

    public float speed = 3.0f;  //移動速度

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2Dを持ってくる
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //水平方向のチェック
        axisH = Input.GetAxisRaw("Horizontal");

        //向きの調整
        if(axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(5, 5);
        }
        if(axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-5, 5);
        }
    }

    private void FixedUpdate()
    {
        //速度を更新
        rb.velocity = new Vector2(axisH * speed, rb.velocity.y);
    }
}
