using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tocastle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        retry.Siroretry = true;
        retry.Muraretry = false;
        retry.Sougenretry = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("map3C");
        retry.Siroretry = true;
        retry.Muraretry = false;
        retry.Sougenretry = false;
    }

}