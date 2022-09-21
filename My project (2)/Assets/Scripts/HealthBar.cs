using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float timeShown = 3f;
    public GameObject obj;
    public Slider slider;
    public Gradient color;
    public Image fill;
    private float currentTime;
    
    public HealthBar(Transform parent, GameObject bar)
    {

    }
    
    public void SetHealth(float healthPercentage)
    {
        Show();
        this.fill.color = this.color.Evaluate(healthPercentage);
        this.slider.value = healthPercentage;
    }

    public void Show()
    {
        this.currentTime = this.timeShown;
        this.obj.SetActive(true);
    }

    void Update() 
    {
        if(this.currentTime > 0) this.currentTime -= Time.deltaTime;
        else this.obj.SetActive(false);
    }
}
