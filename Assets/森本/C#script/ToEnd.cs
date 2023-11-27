using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToEnd : MonoBehaviour
{
    public GameObject Endscene;
    public GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//Player‚ª“–‚½‚Á‚½‚ç
        {
            Endscene.gameObject.SetActive(true);
            Button.gameObject.SetActive(true);
        }

    }
}
