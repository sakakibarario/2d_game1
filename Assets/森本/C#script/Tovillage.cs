using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tovillage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        retry.Muraretry = true;
        retry.Sougenretry = false;
        retry.Siroretry = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("map2V");
    }
}
