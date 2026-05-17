using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarBehavior : MonoBehaviour
{
    [SerializeField] private Material weakenedMaterial;

    public GameObject bossHandler;
    public GameObject levelHandler;

    //Colors
    Color red;
    Color orange;
    Color yellow;
    Color green;
    Color blue;
    Color purple;
    public string pillarColor = "red";

    public int numOfHitsReq = 1;
    public int timesHit = 0;
    private bool isTakingDamage = false;

    private float timeSinceFirstAttack = 0.0f;
    public float breakingTimer = 3.0f;

    private IEnumerator takingDamage;

    // Start is called before the first frame update
    void Start()
    {
        blue = levelHandler.GetComponent<LevelHandler>().blue;
        red = levelHandler.GetComponent<LevelHandler>().red;
        yellow = levelHandler.GetComponent<LevelHandler>().yellow;
        purple = levelHandler.GetComponent<LevelHandler>().purple;
        orange = levelHandler.GetComponent<LevelHandler>().orange;
        green = levelHandler.GetComponent<LevelHandler>().green;
        if (blue == null)
        {
            Debug.Log("Could not grab blue color");
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!isTakingDamage)
        {
            StopCoroutine(TakingDamage(breakingTimer));
        }

        if(timesHit > numOfHitsReq)
        {
            StopCoroutine(TakingDamage(breakingTimer));
        }
    }

    public void ModifyPillar(string color, int hitsReq)
    {
        pillarColor = color;
        numOfHitsReq = hitsReq;
        switch (pillarColor)
        {
            case "red":
                this.gameObject.GetComponent<SpriteRenderer>().color = levelHandler.GetComponent<LevelHandler>().red;
                break;
            case "blue":
                this.gameObject.GetComponent<SpriteRenderer>().color = levelHandler.GetComponent<LevelHandler>().blue;
                break;
            case "yellow":
                this.gameObject.GetComponent<SpriteRenderer>().color = levelHandler.GetComponent<LevelHandler>().yellow;
                break;
            case "purple":
                this.gameObject.GetComponent<SpriteRenderer>().color = purple = levelHandler.GetComponent<LevelHandler>().purple;
                break;
            case "orange":
                this.gameObject.GetComponent<SpriteRenderer>().color = orange = levelHandler.GetComponent<LevelHandler>().orange;
                break;
            case "green":
                this.gameObject.GetComponent<SpriteRenderer>().color = levelHandler.GetComponent<LevelHandler>().green;
                break;
            default:
                this.gameObject.GetComponent<SpriteRenderer>().color = red;
                break;
        }

        //Debug.Log("Set color to " + pillarColor + ", requiring " + numOfHitsReq + " hits");
    }

    public void StartBreaking()
    {
        if (!isTakingDamage) 
        {
            StartCoroutine(TakingDamage(breakingTimer));
        }
        timesHit++;
    }

    IEnumerator TakingDamage(float timer)
    {
        Debug.Log("Pillar is taking damage");
        isTakingDamage = true;
        yield return new WaitForSeconds(timer);
        if(timesHit == numOfHitsReq)
        {
            bossHandler.GetComponent<BossHandler>().StunBoss();
            Destroy(this.gameObject);
        }
        isTakingDamage = false;
        timesHit = 0;
    }
}
