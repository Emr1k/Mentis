using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CanvasScript : MonoBehaviour
{
    bool isResuming = true;
    Resolution[] resolutions;
    public GameObject pauseScr;
    public AudioMixer audioMixer;
    public Dropdown resolutionsDropdown;
    public Image playerHealthBar;
    public Image enemyHealthBar;
    EnemyHealth enemyHealth;
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutoionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutoionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutoionIndex;
        resolutionsDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void PlayerSetHealth(float health)
    {
        playerHealthBar.fillAmount = health/100;
    }
    public void EnemySetHealth(float health)
    {
        enemyHealthBar.fillAmount = health / 100f;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && isResuming)
        {
            pauseScr.SetActive(true);
            Pause();
            isResuming = false;
        }
        if(Time.timeScale == 1f) { isResuming = true; }

    }
    public void Pause()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(5).GetComponent<PlayerCombat>().enabled = false;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(5).GetComponent<PlayerCombat>().enabled = true;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Koy");
    }
}
