using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Variables
    public Transform attackPoint;
    public GameObject attackHB;
    public string currentColor;

    bool apOver = true;

    //colored attacks
    public GameObject attackBasic;
    public GameObject attackBlue;
    public GameObject attackRed;
    public GameObject attackYellow;
    public GameObject attackPurple;
    public GameObject attackOrange;
    public GameObject attackGreen;

    //Combat Details
    public int hp = 6;
    public float attPause;

    public float attackRate = 3;
    float nextAttackTime = 0.3f;

    PlayerMovement moveSpeed;

    public int health = 6;


    void Start()
    {
        currentColor = "clean";
        moveSpeed = this.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                AttackFunc();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void AttackFunc()
    {
        switch(currentColor)
        {
            case "red":
                Instantiate(attackRed, attackPoint.position, attackPoint.rotation);
                break;
            case "blue":
                Instantiate(attackBlue, attackPoint.position, attackPoint.rotation);
                break;
            case "yellow":
                Instantiate(attackYellow, attackPoint.position, attackPoint.rotation);
                break;
            case "green":
                Instantiate(attackGreen, attackPoint.position, attackPoint.rotation);
                break;
            case "purple":
                Instantiate(attackPurple, attackPoint.position, attackPoint.rotation);
                break;
            case "orange":
                Instantiate(attackOrange, attackPoint.position, attackPoint.rotation);
                break;
            case "coated":
                Instantiate(attackBasic, attackPoint.position, attackPoint.rotation);
                break;
            case "clean":
                Instantiate(attackHB, attackPoint.position, attackPoint.rotation);
                break;
            default:
                Instantiate(attackHB, attackPoint.position, attackPoint.rotation);
                break;
        }
        if (apOver)
        {
            StopCoroutine(PauseMovement(0.2f));
        }
        StartCoroutine(PauseMovement(0.2f));
    }

    public void SetColor(string color)
    {
        switch(color)
        {
            case "red":
                currentColor = "red";
                break;
            case "blue":
                currentColor = "blue";
                break;
            case "yellow":
                currentColor = "yellow";
                break;
            case "clean":
                currentColor = "clean";
                break;
            case "green":
                currentColor = "green";
                break;
            case "purple":
                currentColor = "purple";
                break;
            case "orange":
                currentColor = "orange";
                break;
            case "coated":
                currentColor = "coated";
                break;
            default:
                currentColor = "clean";
                break;
        }
    }
    IEnumerator PauseMovement(float dur)
    {
        PlayerMovement moveSpeed = this.gameObject.GetComponent<PlayerMovement>();
        moveSpeed.moveSpeed = 0.0f;

        yield return new WaitForSeconds(dur);

        moveSpeed.moveSpeed = 5.0f;
    }

    public void TakeDamage()
    {
        hp--;
        if(hp == 0)
        {
            moveSpeed.moveSpeed = 0.0f;
            Destroy(this.gameObject, 0.5f);
        }
    }
}

/* Credits of assistance or otherwise
 * Brackeys youtube channel for foundational coding
 */