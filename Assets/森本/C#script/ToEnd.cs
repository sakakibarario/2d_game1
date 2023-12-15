using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToEnd : MonoBehaviour
{
    public GameObject Endscene;
    public GameObject Button;

    public byte END_count;
    public byte END_counta;


    // Start is called before the first frame update
    void Start()
    {
        Endscene.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//Player‚ª“–‚½‚Á‚½‚ç
        {
            //Endscene.gameObject.SetActive(true);

            for (END_count = 0; END_count < 255; END_count++)
            {
                if(END_count == 50)
                {
                    END_counta++;
                    END_count = 0;
                }
                Endscene.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, END_counta);
            }
            Button.gameObject.SetActive(true);

        }
    }
}