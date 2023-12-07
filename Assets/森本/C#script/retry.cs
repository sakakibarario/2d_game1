using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retry : MonoBehaviour
{

    static public bool Sougenretry;
    static public bool Muraretry;
    static public bool Siroretry;
    public GameObject SougenretryButton;
    public GameObject MuraretryButton;
    public GameObject SiroretryButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //草原ステージでGameover
        if (Sougenretry)
        {
            SougenretryButton.gameObject.SetActive(true);
            MuraretryButton.gameObject.SetActive(false);
            SiroretryButton.gameObject.SetActive(false);
        }
        //村ステージでGameover
        if (Muraretry)
        {
            MuraretryButton.gameObject.SetActive(true);
            SougenretryButton.gameObject.SetActive(false);
            SiroretryButton.gameObject.SetActive(false);

            Muraretry = false;
        }
        //城ステージでGameover
        if (SiroretryButton)
        {
            SiroretryButton.gameObject.SetActive(true);
            SougenretryButton.gameObject.SetActive(false);
            MuraretryButton.gameObject.SetActive(false);

        }
    }
}
