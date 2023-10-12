using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public PlayerMovement thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    public Vector3 respawnPoint;
    public float respawnLength;
    public GameObject checkpointA;
    public GameObject checkpointB;
    public GameObject checkpointC;

    public ParticleSystem deathEffect;
    public Image deathScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer.a)
        {
            respawnPoint = checkpointA.transform.position;
        }
        
        if (thePlayer.b)
        {
            respawnPoint = checkpointB.transform.position;
        }
        
        if (thePlayer.c)
        {
            respawnPoint = checkpointC.transform.position;
        } 


        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }
        } 
        
        if (invincibilityCounter <= 0)
        {
            playerRenderer.enabled = true;
        }

        if (isFadeToBlack)
        {
            deathScreen.color = new Color(deathScreen.color.r, deathScreen.color.g, deathScreen.color.b, Mathf.MoveTowards(deathScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (deathScreen.color.a == 1)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            deathScreen.color = new Color(deathScreen.color.r, deathScreen.color.g, deathScreen.color.b, Mathf.MoveTowards(deathScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (deathScreen.color.a == 0)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void HurtPlayer (int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Respawn();
            }else
            {
                thePlayer.Knockback(direction);

                invincibilityCounter = invincibilityLength;

                playerRenderer.enabled = false;

                flashCounter = flashLength;
            }
        }
    }

    public void HealPlayer (int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //public void SetSpawnPoint(Vector3 newPosition)
    //{
        
    //}

    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        deathEffect.transform.position = thePlayer.transform.position;
        deathEffect.Play();

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        thePlayer.transform.position = respawnPoint;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;
    } 

}
