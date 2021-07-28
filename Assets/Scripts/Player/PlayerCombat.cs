using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;
    [HideInInspector] public float currentHealth = 100f;
    public CanvasScript healthBar;
    PlayerMovement playerMovement;

    private EnemyHealth enemyHealth;
    bool canAttack = true;
    Animator anim;

    //Cooldown
    public Image cdImage1;
    public Text cdText1;

    bool isInCd1 = false;
    float cdTime1 = 5f;
    float cdTimer1;

    public Image cdImage2;
    public Text cdText2;

    bool isInCd2 = false;
    float cdTime2 = 10f;
    float cdTimer2;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        //CD
        cdImage1.fillAmount = 0.0f;
        cdText1.gameObject.SetActive(false);

        cdImage2.fillAmount = 0.0f;
        cdText2.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!anim.IsInTransition(0) && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
            ) { canAttack = true; }
        
        if (Input.GetMouseButtonDown(0) && canAttack && canSpell1()) { Attack1(); }
        if (Input.GetMouseButtonDown(1) && canAttack && canSpell2()) { Attack2(); }

        //CD
        if (isInCd1) { ApplyCooldown1(); }
        if (isInCd2) { ApplyCooldown2(); }

    }
    void Attack1()
    {
        anim.SetTrigger("Attack1");

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider enemy in hits)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damageCount);
        }
        canAttack = false;
    }
    void Attack2()
    {
        anim.SetTrigger("Attack2");

        //playerMovement.velocity.y = Mathf.Sqrt(playerMovement.jumpHeight * -1f * playerMovement.gravity);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider enemy in hits)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damageCount * 2);
        }
        canAttack = false;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        healthBar.PlayerSetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        GetComponentInParent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCombat2>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    //Cooldown
    void ApplyCooldown1()
    {
        cdTimer1 -= Time.deltaTime;

        if (cdTimer1 < 0f)
        {
            isInCd1 = false;
            cdImage1.fillAmount = 0.0f;
            cdText1.gameObject.SetActive(false);
        }
        else
        {
            cdText1.text = Mathf.RoundToInt(cdTimer1).ToString();
            cdImage1.fillAmount = cdTimer1 / cdTime1;
        }
    }
    void ApplyCooldown2()
    {
        cdTimer2 -= Time.deltaTime;

        if (cdTimer2 < 0f)
        {
            isInCd2 = false;
            cdImage2.fillAmount = 0.0f;
            cdText2.gameObject.SetActive(false);
        }
        else
        {
            cdText2.text = Mathf.RoundToInt(cdTimer2).ToString();
            cdImage2.fillAmount = cdTimer2 / cdTime2;
        }
    }

    public bool canSpell1()
    {
        if (isInCd1)
        {
            return false;
        }
        else
        {
            isInCd1 = true;
            cdImage1.gameObject.SetActive(true);
            cdText1.gameObject.SetActive(true);
            cdTimer1 = cdTime1;
            return true;
        }
    }
    public bool canSpell2()
    {
        if (isInCd2)
        {
            return false;
        }
        else
        {
            isInCd2 = true;
            cdImage2.gameObject.SetActive(true);
            cdText2.gameObject.SetActive(true);
            cdTimer2 = cdTime2;
            return true;
        }
    }
}
