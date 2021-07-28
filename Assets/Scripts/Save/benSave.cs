﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class benSave : MonoBehaviour
{
    EnemyHealth health;
    public Text winText;
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
        GameObject.FindGameObjectWithTag("Music").SetActive(false);
    }

    void Update()
    {
        if (health.currentHealth <= 0) { PlayerPrefs.SetInt("benWin", 1);
            winText.gameObject.SetActive(true);
            Invoke(nameof(Return), 2f);
        }
    }

    void Return()
    {
        SceneManager.LoadScene("Koy");
        GameObject.FindGameObjectWithTag("Music").SetActive(true);

    }
}
    
