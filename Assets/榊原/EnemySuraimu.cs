using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuraimu : MonoBehaviour
{
    public float speed = 0.01f;
    public float travel = 0.5f;
    public int hpMax = 10;
    public int damage = 5;

    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = hpMax;
    }



    // Update is called once per frame
    void Update()
    {
        var v = Input.GetAxis("Vertical");

        var velocity = new Vector3(travel, v) * speed;
        transform.localPosition += velocity;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーと衝突した場合
        if (collision.name.Contains("Player"))
        {
            // プレイヤーにダメージを与える
            //var player = collision.GetComponent<PlayerController>();
            //player.Damage(damage);
            return;
        }





    }
}
