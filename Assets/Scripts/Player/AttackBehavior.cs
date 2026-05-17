using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public string attackColor = "";
    GameObject levelHandler;
    LevelHandler lh;
    bool sca;

    void Start()
    {
        //grab player
        player = GameObject.Find("player");
        attackColor = player.GetComponent<Attack>().currentColor;
        levelHandler = GameObject.FindGameObjectWithTag("LevelHandler");
        lh = levelHandler.GetComponent<LevelHandler>();
        sca = lh.secColorsActive;
    }

    //collissions
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Hit Something!");
        //change the color if the attacked object is a color station
        GameObject collidedWith = coll.gameObject;
        if (sca)
        {
            switch (collidedWith.tag)
            {
                case "colorStationRed":// Red
                    switch (attackColor)
                    {
                        case "clean":
                            player.GetComponent<Attack>().SetColor("red");
                            break;

                        case "red":
                            break;

                        case "blue":
                            player.GetComponent<Attack>().SetColor("purple");
                            break;

                        case "yellow":
                            player.GetComponent<Attack>().SetColor("orange");
                            break;
                        default:
                            player.GetComponent<Attack>().SetColor("coated");
                            break;
                    }
                    break;

                case "colorStationBlue"://Blue
                    switch (attackColor)
                    {
                        case "clean":
                            player.GetComponent<Attack>().SetColor("blue");
                            break;

                        case "blue":
                            break;

                        case "red":
                            player.GetComponent<Attack>().SetColor("purple");
                            break;

                        case "yellow":
                            player.GetComponent<Attack>().SetColor("green");
                            break;

                        default:
                            player.GetComponent<Attack>().SetColor("coated");
                            break;
                    }
                    break;

                case "colorStationYellow": //Yellow
                    switch (attackColor)
                    {
                        case "yellow":
                            break;

                        case "clean":
                            player.GetComponent<Attack>().SetColor("yellow");
                            break;

                        case "blue":
                            player.GetComponent<Attack>().SetColor("green");
                            break;

                        case "red":
                            player.GetComponent<Attack>().SetColor("orange");
                            break;

                        default:
                            player.GetComponent<Attack>().SetColor("coated");
                            break;
                    }
                    break;

                case "cleanStation":
                    player.GetComponent<Attack>().SetColor("clean");
                    break;
                default:
                    break;
            }
        }
        if (!sca)
        {
            switch (collidedWith.tag)
            {
                case "colorStationRed":
                    player.GetComponent<Attack>().SetColor("red");
                    break;

                case "colorStationBlue":
                    player.GetComponent<Attack>().SetColor("blue");
                    break;
                case "colorStationYellow":
                    player.GetComponent<Attack>().SetColor("yellow");
                    break;
                case "cleanStation":
                    player.GetComponent<Attack>().SetColor("clean");
                    break;
            }
        }

        if (collidedWith.tag == "inkBlot") //damage the inkblot enemy
        {
            //Debug.Log("Hit the enemy!");
            enemy = collidedWith;
            switch (enemy.GetComponent<InkblotBehavior>().currentColor)
            {
                case "red":
                    if (attackColor == "red")
                    {
                        enemy.GetComponent<InkblotBehavior>().TakeDamage();
                    }
                    break;
                case "blue":
                    if (attackColor == "blue")
                    {
                        enemy.GetComponent<InkblotBehavior>().TakeDamage();
                    }
                    break;
                case "yellow":
                    if (attackColor == "yellow")
                    {
                        enemy.GetComponent<InkblotBehavior>().TakeDamage();
                    }
                    break;
                case "purple":
                    if (attackColor == "purple")
                    {
                        enemy.GetComponent<InkblotBehavior>().TakeDamage();
                    }
                    break;
                case "orange":
                    if (attackColor == "orange")
                    {
                        enemy.GetComponent<InkblotBehavior>().TakeDamage();
                    }
                    break;
                case "green":
                    if (attackColor == "green")
                    {
                        enemy.GetComponent<InkblotBehavior>().TakeDamage();
                    }
                    break;
                default:
                    break;
            }
        }

        if (collidedWith.tag == "Pillar1" || collidedWith.tag == "Pillar2" || collidedWith.tag == "Pillar3" || collidedWith.tag == "Pillar4")
        {
            //Debug.Log("Hit a Pillar");
            GameObject pillar = collidedWith;
            if (pillar.GetComponent<PillarBehavior>().pillarColor == attackColor)
            {
                pillar.GetComponent<PillarBehavior>().StartBreaking();
            }
        }

        if (collidedWith.tag == "Boss")
        {
            GameObject bossEnemy = collidedWith;
            bossEnemy.GetComponent<BossBehavior>().TakeDamage();
        }
    }

    //destroy this game object after 0.1 seconds
    void FixedUpdate()
    {
        Destroy(this.gameObject, 1.0f);
    }
}
