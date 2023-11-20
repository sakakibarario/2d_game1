using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArtillery : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 30.0f;

    private int vec_x_mai = -1;
    private float vec_y_pra = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec1 = new Vector3(vec_x_mai, vec_y_pra, 0).normalized;
        rb.velocity = moveVec1 * moveSpeed;
    }
}
