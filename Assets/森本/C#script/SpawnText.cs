using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnText : MonoBehaviour
{
    public GameObject KakyuText;
    public GameObject HisyouText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.SougenBoss)//ëêå¥CLEAR
        {
            KakyuText.gameObject.SetActive(true);
            HisyouText.gameObject.SetActive(false);

            if (PlayerController.VillageBoss)//ë∫CLEAR
            {
                HisyouText.gameObject.SetActive(true);
                KakyuText.gameObject.SetActive(false);
            }
        }
    }
}
