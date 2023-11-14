using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    //最大HPと現在のHP
    int maxHp = 50;
    int currentHp;

    //Sliderを入れる
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //Sliderを満タンにする
        slider.value = 1;

        //現在のHPを最大HPと同じにする
        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
    }

    //CollederオブジェクトのIsTriggerにチェックを入れること
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enemyタグのオブジェクトに触れると発動
        if (collision.CompareTag("Enemy"))
        {
            //ダメージは10
            int damage = 10;
            Debug.Log("damage : " + damage);

            //現在のHPからダメージを引く
            currentHp = currentHp - damage;
            Debug.Log("After curretHp : " + currentHp);

            //最大HPにおける現在のHPをSliderに反映
            //int同士の割り算は小数点以下は0になるので、
            //floatをつけてfloat変数として震わせる。
            slider.value = (float)currentHp / (float)maxHp; ;
            Debug.Log("slider.value : " + slider.value);
        }
    }

}
