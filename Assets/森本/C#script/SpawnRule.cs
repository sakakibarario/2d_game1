using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRule : MonoBehaviour
{
    public GameObject rule;
    bool Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Text)
        {
            if (collision.gameObject.tag == "Player")//Player‚É“–‚½‚Á‚½‚ç
            {
                rule.gameObject.SetActive(true);
            }
        }
        Text = false;
    }    
}
