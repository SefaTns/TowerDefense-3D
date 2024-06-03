using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class KaleKapisi : MonoBehaviour
{

    public Slider slider;

    [SerializeField] private float doorHealt;
    private bool canTrigger = true;
    private float waitTime = 2.4f;
    private float endtime = 60.0f;
    Spawner spawner;

    public GameObject panelHeader;
    public GameObject panelLose;
    public GameObject panelWin3;
    public GameObject panelWin2;
    public GameObject panelWin1;

    public void Start()
    {
        slider.maxValue = doorHealt;
        slider.value = doorHealt;
        Invoke("TooglePanels", endtime);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && canTrigger)
        {
            StartCoroutine(TriggerCooldown());
            slider.value-= 1;
            var enemyDamage = other.gameObject.GetComponent<EnemyScript>();

            this.DoorHealt -= enemyDamage.Damage;
            Debug.Log("Kapi Can : " + this.DoorHealt);

            if (this.DoorHealt <= 0)
            {
                if (this.gameObject.CompareTag("leftDoor") || this.gameObject.CompareTag("rightDoor"))
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private IEnumerator TriggerCooldown()
    {
        canTrigger = false;
        yield return new WaitForSeconds(waitTime);
        canTrigger = true;
    }

    void TooglePanels()
    {
        Time.timeScale = 0f;
        panelHeader.SetActive(false);

        if(slider.value<5)
        {
            panelLose.SetActive(true);
        }
        else if ( slider.value>=5 && slider.value < 10)
        {
            panelWin1.SetActive(true);
        }
        else if (slider.value >= 10 && slider.value < 15)
        {
            panelWin2.SetActive(true);
        }
        else if (slider.value >= 15 && slider.value < 20)
        {
            panelWin3.SetActive(true);
        }
    }


    public float DoorHealt
    {
        get { return this.doorHealt; }
        set { this.doorHealt = value; }
    }


}
