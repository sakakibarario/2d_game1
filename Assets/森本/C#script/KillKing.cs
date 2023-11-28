using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillKing : MonoBehaviour
{
    public GameObject Endscene;
    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //éÂêlåˆê⁄êG
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Instantiate(explode, this.transform.position, Quaternion.identity);
        }
    }
}
