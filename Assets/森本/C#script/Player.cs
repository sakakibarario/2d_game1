using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    private float playerSpeed;
    public int hp;

    //Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 左キーを押したら左方向へ進む
        if (Input.GetKey(KeyCode.LeftArrow)) playerSpeed = -speed;
        // 右キーを押したら右方向へ進む
        else if (Input.GetKey(KeyCode.RightArrow)) playerSpeed = speed;
        // 何もおさなかったら止まる
        else playerSpeed = 0;

       // rigidbody2D.velocity = new Vector2(playerSpeed, rigidbody2D.velocity.y);
    }
}