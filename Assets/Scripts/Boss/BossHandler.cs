using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHandler : MonoBehaviour
{
    public string pillarOneColor;
    public string pillarTwoColor;
    public string pillarThreeColor;
    public string pillarFourColor;

    public int pillarOneHits = 0;
    public int pillarTwoHits = 0;
    public int pillarThreeHits = 0;
    public int pillarFourHits = 0;

    public GameObject pillarOne;
    public GameObject pillarTwo;
    public GameObject pillarThree;
    public GameObject pillarFour;

    public bool pillarsGenerated = false;

    private GameObject currentPillar;

    private GameObject sequencePillar;

    public GameObject bossEnemy;
    public int bossHP = 20;
    public bool isStunned = false;

    private Coroutine bossAttPattern;

    BossHealth bossHealth;
    //This script generates all of the parameters and variables for the puzzle part of the boss fight.
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= 4; i++)
        {
            if(i == 1)
            {
                currentPillar = pillarOne;
            }
            if (i == 2)
            {
                currentPillar = pillarTwo;
            }
            if (i == 3)
            {
                currentPillar = pillarThree;
            }
            if (i == 4)
            {
                currentPillar = pillarFour;
            }
            GeneratePillarStats();
        }
        pillarsGenerated = true;

        StartAttackPattern();

        bossHealth = this.gameObject.GetComponent<BossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePillarStats()
    {
        string pillarColor;
        int pillarHits;
        int colorInt = Random.Range(1, 7); //Generate random integer
        switch (colorInt) //switch statement sets the healthColor fields [0-2]
        {
            case 1:
                pillarColor = "red";
                break;
            case 2:
                pillarColor = "blue";
                break;
            case 3:
                pillarColor = "yellow";
                break;
            case 4:
                pillarColor = "purple";
                break;
            case 5:
                pillarColor = "orange";
                break;
            case 6:
                pillarColor = "green";
                break;
            default:
                pillarColor = "red";
                break;
        }

        pillarHits = Random.Range(2, 6);

        currentPillar.GetComponent<PillarBehavior>().ModifyPillar(pillarColor, pillarHits);
    }

    void StartAttackPattern()
    {
        if(bossAttPattern != null)
        {
            StopCoroutine(bossAttPattern);
        }

        bossAttPattern = StartCoroutine(AttackPattern());
    }

    public void StunBoss()
    {
        StartCoroutine(BossStunned());
    }

    public GameObject GetSequencePillar()
    {
        if(pillarOne != null)
        {
            sequencePillar = pillarOne;
        }
        else if(pillarTwo != null)
        {
            sequencePillar = pillarTwo;
        }
        else if(pillarThree != null)
        {
            sequencePillar = pillarThree;
        }
        else if(pillarFour != null)
        {
            sequencePillar = pillarFour;
        }

        return sequencePillar;
    }

    public void BossTakeDamage()
    {
        if (isStunned)
        {
            Debug.Log("Boss took Damage");
            bossHP--;
            bossHealth.BossBarDamage(1);
        }
        if(bossHP <= 0)
        {
            bossEnemy.GetComponent<BossBehavior>().DestroyBoss();
            SceneManager.LoadScene("You Win");
        }
    }

    public IEnumerator AttackPattern()
    {
        while(bossHP > 0 && !isStunned)
        {
            if (!isStunned)
            {
                bossEnemy.GetComponent<BossBehavior>().RoarFunction();
                Debug.Log("Called Roar Function");
            }
            
            yield return new WaitForSeconds(9.0f);

            /* if(!isStunned)
            {
                bossEnemy.GetComponent<BossBehavior>().BossAttack();
                Debug.Log("Called for Attack 2");
            }

            yield return new WaitForSeconds(5.0f); */

            if (!isStunned)
            {
                bossEnemy.GetComponent<BossBehavior>().BossAttack();
                Debug.Log("Called for Attack 1");
            }

            yield return new WaitForSeconds(5.0f);
        }
    }

    public IEnumerator BossStunned()
    {
        Debug.Log("Boss is Stunned");
        isStunned = true; //sets isStunned to True
        bossAttPattern = null;
        bossEnemy.GetComponent<BossBehavior>().StopRoar();
        Debug.Log("Stopped Roar");
        yield return new WaitForSeconds(5.0f); // wait for 5 seconds
        bossEnemy.GetComponent<BossBehavior>().SetSequencePillar();
        Debug.Log("Boss is no longer stunned. HP Remaining: " + bossHP);
        isStunned = false; //set isStunned to false
        StartAttackPattern();
        
    }
}
