using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image cdImage;

    public Text cdText;

    bool isInCd = false;
    float cdTime;
    float cdTimer;
    void Start()
    {
        cdImage.fillAmount = 0.0f;
        cdText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isInCd) { ApplyCooldown(); }    
    }

    void ApplyCooldown()
    {
        cdTimer -= Time.deltaTime;

        if (cdTimer < 0f)
        {
            isInCd = false;
            cdImage.fillAmount = 0.0f;
            cdText.gameObject.SetActive(false);
        }
        else
        {
            cdText.text = Mathf.RoundToInt(cdTimer).ToString();
            cdImage.fillAmount = cdTimer / cdTime;
        }
    }

    public bool canSpell()
    {
        if (isInCd)
        {
            return false;
        }
        else
        {
            isInCd = true;
            cdImage.gameObject.SetActive(true);
            cdTimer = cdTime;
            return true;
        }
    }
}
