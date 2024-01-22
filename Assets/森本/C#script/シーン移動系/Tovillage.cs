using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tovillage : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CoolTime.KakyuCoolTimeflag = true;
    }
    public void OnClickStartButton()
    {
        Initiate.Fade(sceneName, fadeColor, fadeSpeed);
        //SceneManager.LoadScene("map2V");
        retry.Muraretry = true;
        retry.Sougenretry = false;
        retry.Siroretry = false;
    }
}