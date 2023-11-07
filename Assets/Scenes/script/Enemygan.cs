using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemygan : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5.0f;
    Vector3 moveVec = new Vector3( 0, 0, 0);
    private SpriteRenderer sr = null;

    static public bool Naihu = false;
    

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float add_move = moveSpeed * Time.deltaTime;
        transform.Translate(moveVec * add_move);

        if(Naihu)
        {
            Premake();
            Naihu = false;
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public float target(GameObject target)
    {
        float x = target.transform.position.x - this.transform.position.x;
        float y = target.transform.position.y - this.transform.position.y;
        float rad = Mathf.Atan2(y, x);
        return rad * Mathf.Rad2Deg;
    }

    public void moveZ(float z, float speed, Rigidbody2D obj)
    {
        Vector2 move;
        move.x = Mathf.Cos(Mathf.Deg2Rad * z);
        move.y = Mathf.Sin(Mathf.Deg2Rad * z);
        obj.velocity = move * speed;
    }
    public void Premake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float p_z = target(player);
        moveZ(p_z, moveSpeed, rb);
    }
}
