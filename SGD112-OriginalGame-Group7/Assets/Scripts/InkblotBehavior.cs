using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkblotBehavior : MonoBehaviour
{
    //AI
    public GameObject player;
    public float speed = 1.5f;
    private Transform target;
    bool apOver = true;

    //Enemy stats
    public int hp = 3;
    public string currentColor = "";
    string[] healthColors = {"", "", ""};

    //Color preparations
    public GameObject levelHandler;
    bool useSecondaryColors;
    Color blue;
    Color red;
    Color yellow;
    Color purple;
    Color orange;
    Color green;

    //Attack Objects
    public Transform attackOrigin;
    public GameObject redAttack;
    public GameObject orangeAttack;
    public GameObject yellowAttack;
    public GameObject greenAttack;
    public GameObject blueAttack;
    public GameObject purpleAttack;

    //combat
    public float attackRate = 1.5f;
    float nextAttackTime = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        target = player.GetComponent<Transform>();
        SpriteRenderer colren = GetComponent<SpriteRenderer>();
        levelHandler = GameObject.FindGameObjectWithTag("LevelHandler");
        /*if(levelHandler != null)
        {
            Debug.Log("Level Handler Found");
        }*/
        blue = levelHandler.GetComponent<LevelHandler>().blue;
        red = levelHandler.GetComponent<LevelHandler>().red;
        yellow = levelHandler.GetComponent<LevelHandler>().yellow;
        purple = levelHandler.GetComponent<LevelHandler>().purple;
        orange = levelHandler.GetComponent<LevelHandler>().orange;
        green = levelHandler.GetComponent<LevelHandler>().green;
        useSecondaryColors = levelHandler.GetComponent<LevelHandler>().secColorsActive;
        GenerateHealth();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, target.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, target.position) < 1)
        {
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(PauseMovement(0.5f));
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void GenerateHealth()
    {
        if (!useSecondaryColors)
        {
            Debug.Log("Using primary colors");
            for (int h = 2; h >= 0; h--)
            {
                int colInt = Random.Range(1, 4); //Generate random integer
                switch (colInt) //switch statement sets the healthColor fields [0-2]
                {
                    case 1:
                        healthColors[h] = "red";
                        break;
                    case 2:
                        healthColors[h] = "blue";
                        break;
                    case 3:
                        healthColors[h] = "yellow";
                        break;
                    default:
                        healthColors[h] = "red";
                        break;
                }
            }
        }
        if (useSecondaryColors)
        {
            Debug.Log("Using secondary colors");
            for (int h = 2; h >= 0; h--)
            {
                int colInt = Random.Range(1, 7); //Generate random integer
                switch (colInt) //switch statement sets the healthColor fields [0-2]
                {
                    case 1:
                        healthColors[h] = "red";
                        break;
                    case 2:
                        healthColors[h] = "blue";
                        break;
                    case 3:
                        healthColors[h] = "yellow";
                        break;
                    case 4:
                        healthColors[h] = "purple";
                        break;
                    case 5:
                        healthColors[h] = "orange";
                        break;
                    case 6:
                        healthColors[h] = "green";
                        break;
                    default:
                        healthColors[h] = "red";
                        break;
                }
            }
        }
        Debug.Log(healthColors[0] + ", " + healthColors[1] + ", " + healthColors[2]);

        SetCurrentColor();
    }

    public void TakeDamage()
    {
        hp--;
        if (hp == 0)
        {
            speed = 0;
            Destroy(this.gameObject, 0.5f);
        }
        else
        {
            SetCurrentColor();
        }
    }

    void SetCurrentColor()
    {
        currentColor = healthColors[(hp - 1)];
        switch (currentColor)
        {
            case "red":
                this.gameObject.GetComponent<SpriteRenderer>().color = red;
                break;
            case "blue":
                this.gameObject.GetComponent<SpriteRenderer>().color = blue;
                break;
            case "yellow":
                this.gameObject.GetComponent<SpriteRenderer>().color = yellow;
                break;
            case "purple":
                this.gameObject.GetComponent<SpriteRenderer>().color = purple;
                break;
            case "orange":
                this.gameObject.GetComponent<SpriteRenderer>().color = orange;
                break;
            case "green":
                this.gameObject.GetComponent<SpriteRenderer>().color = green;
                break;
            default:
                this.gameObject.GetComponent<SpriteRenderer>().color = red;
                break;
        }
    }

    public void MakeAttack()
    {
        switch (currentColor)
        {
            case "red":
                Instantiate(redAttack, attackOrigin.position, attackOrigin.rotation);
                break;
            case "orange":
                Instantiate(orangeAttack, attackOrigin.position, attackOrigin.rotation);
                break;
            case "yellow":
                Instantiate(yellowAttack, attackOrigin.position, attackOrigin.rotation);
                break;
            case "green":
                Instantiate(greenAttack, attackOrigin.position, attackOrigin.rotation);
                break;
            case "blue":
                Instantiate(blueAttack, attackOrigin.position, attackOrigin.rotation);
                break;
            case "purple":
                Instantiate(purpleAttack, attackOrigin.position, attackOrigin.rotation);
                break;
            default:
                Debug.Log("Didn't instantiate attack");
                break;
        }
        StopCoroutine(PauseMovement(0.0f));
    }

    IEnumerator PauseMovement(float dur)
    {
        speed = 0.0f;

        yield return new WaitForSeconds(dur);
        speed = 1.5f;
        MakeAttack();
    }

}
/* Credits
 * Basic Enemy follow AI from Blackthornprod Youtube Channel (Video https://www.youtube.com/watch?v=rhoQd6IAtDo&list=PLdG25zdeO5RGetQFf1IcxLW82zDmifvKO&index=11)
 */