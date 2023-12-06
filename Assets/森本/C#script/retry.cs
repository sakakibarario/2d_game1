using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retry : MonoBehaviour
{
    bool Sougenretry;
    bool Muraretry;
    public GameObject SougenretryButton;
    public GameObject MuraretryButton;

    // Start is called before the first frame update
    void Start()
    {
        Sougenretry = PlayerController.SougenBoss;
        Muraretry = PlayerController.VillageBoss;
    }

    // Update is called once per frame
    void Update()
    {
        if (Sougenretry == false)
        {
            SougenretryButton.gameObject.SetActive(true);
            Sougenretry = true;
        }
        if(Muraretry == false)
        {
            MuraretryButton.gameObject.SetActive(true);
            Muraretry = true;
        }
    }
}
