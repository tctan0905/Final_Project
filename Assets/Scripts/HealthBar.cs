using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    PlayerController playerhealth;
    public Text txthealth;
    private void Start()
    {
      
    }
    public void setmaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        txthealth.text = slider.value + "/200";
    }


    public void setHealth(int health)
    {
        slider.value = health;
        txthealth.text = slider.value + "/200";

    }
    public void setHealthEnemy(int health)
    {
        slider.value = health;

    }

}
