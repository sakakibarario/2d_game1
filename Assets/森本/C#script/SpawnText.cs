using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnText : MonoBehaviour
{
    public GameObject KakyuText;
    public GameObject HisyouText;

    static public bool ones = true;
    static public bool onev = true;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.SougenBoss)//ëêå¥CLEAR
        {
            if (ones)
            {
                KakyuText.gameObject.SetActive(true);
                HisyouText.gameObject.SetActive(false);
                ones = false;
            }
            else if (ones == false)
            {
                KakyuText.gameObject.SetActive(false);
                HisyouText.gameObject.SetActive(false);
            }

            if (PlayerController.VillageBoss)//ë∫CLEAR
            {
                if(onev)
                {
                    HisyouText.gameObject.SetActive(true);
                    KakyuText.gameObject.SetActive(false);
                    onev = false;
                }
                else if(onev)
                {
                    HisyouText.gameObject.SetActive(false);
                    KakyuText.gameObject.SetActive(false);
                }          
            }
        }
    }

// Update is called once per frame
void Update()
    {

    }
}
