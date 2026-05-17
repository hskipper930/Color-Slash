using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehavior : MonoBehaviour
{
    public GameObject enemy;
    public string attackColor = "";
    GameObject levelHandler;
    LevelHandler lh;
    bool sca;

    void Start()
    {
        levelHandler = GameObject.FindGameObjectWithTag("LevelHandler");
        lh = levelHandler.GetComponent<LevelHandler>();
        sca = lh.secColorsActive;
    }

    //collissions
    void OnTriggerEnter2D(Collider2D coll)
    {
        //change the color if the attacked object is a color station
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "player") //damage the player
        {
            collidedWith.GetComponent<Attack>().TakeDamage();
        }
    }

    //destroy this game object after 0.1 seconds
    void FixedUpdate()
    {
        Destroy(this.gameObject, 1.0f);
    }
}