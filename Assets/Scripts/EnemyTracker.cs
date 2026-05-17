using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public static int numberOfEnemies = 1;
    [SerializeField]  GameObject prismEnemySetter;
    static GameObject prismEnemy;

    void Awake()
    {
        prismEnemy = prismEnemySetter;
    }

    public static void OnRoomEnter(int roomNum)
    {
        switch (roomNum)
        {
            case 1:
                InstantiateNewPrism(new Vector2(4f, 2f));
                break;
            case 2:
                InstantiateNewPrism(new Vector2(1.5f, 16f));
                break;
            default:
                break;
        }
    }

    static void InstantiateNewPrism(Vector2 location)
    {
        Instantiate(prismEnemy, location, Quaternion.identity);
        numberOfEnemies++;
    }
}
