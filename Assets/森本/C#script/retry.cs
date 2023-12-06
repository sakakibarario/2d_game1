using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retry : MonoBehaviour
{
    bool Sougenretry;
    bool Muraretry;
    bool Siroretry;
    public GameObject SougenretryButton;
    public GameObject MuraretryButton;
    public GameObject SiroretryButton;

    // Start is called before the first frame update
    void Start()
    {
        Sougenretry = PlayerController.SougenBoss;
        Muraretry = PlayerController.VillageBoss;
        //Siroretry = 

    }

    // Update is called once per frame
    void Update()
    {
        //草原ステージでGameover
        if (Sougenretry == false)
        {
            MuraretryButton.gameObject.SetActive(false);//村リトライを一応falseにする
            SiroretryButton.gameObject.SetActive(false);//城リトライを一応falseにする
            SougenretryButton.gameObject.SetActive(true);
            Sougenretry = true;
        }
        //村ステージでGameover
        else if (Muraretry == false)
        {
            SougenretryButton.gameObject.SetActive(false);//草原リトライを一応falseにする
            SiroretryButton.gameObject.SetActive(false);//城リトライを一応falseにする
            MuraretryButton.gameObject.SetActive(true);
            Muraretry = true;
        }
        //城ステージでGameover
        else if(SiroretryButton == false)
        {
            SougenretryButton.gameObject.SetActive(false);//草原リトライを一応falseにする
            MuraretryButton.gameObject.SetActive(false);//村リトライを一応falseにする
            SiroretryButton.gameObject.SetActive(true);
            Siroretry = true;
        }
    }
}
