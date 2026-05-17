using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    bool isFlashing = false;

    public GameObject bossHandler;
    public GameObject levelHandler;

    GameObject sequencePillar;
    string spColor;
    int spHits;
    bool checkForInitGen = true;

    //Everything to do with flashing
    [SerializeField] Material redFlash;
    [SerializeField] Material orangeFlash;
    [SerializeField] Material yellowFlash;
    [SerializeField] Material greenFlash;
    [SerializeField] Material blueFlash;
    [SerializeField] Material purpleFlash;
    Material flashMaterial;
    Material originalMaterial;
    public float flashTime;
    SpriteRenderer mouthSR;
    private Coroutine bossRoarFlash;

    //spawning enemies
    [SerializeField] Transform spawnOne;
    [SerializeField] Transform spawnTwo;
    [SerializeField] Transform spawnThree;
    [SerializeField] Transform spawnFour;

    [SerializeField] GameObject spawnGOOne;
    [SerializeField] GameObject spawnGOTwo;
    [SerializeField] GameObject spawnGOThree;
    [SerializeField] GameObject spawnGOFour;

    [SerializeField] GameObject minion;


    // Start is called before the first frame update
    void Start()
    {
        mouthSR = GetComponent<SpriteRenderer>();
        originalMaterial = mouthSR.material;

        spawnOne = spawnGOOne.GetComponent<Transform>();
        spawnTwo = spawnGOTwo.GetComponent<Transform>();
        spawnThree = spawnGOThree.GetComponent<Transform>();
        spawnFour = spawnGOFour.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (bossHandler.GetComponent<BossHandler>().pillarsGenerated && checkForInitGen)
        {
            SetSequencePillar();
            checkForInitGen = false;
        }
        if (!isFlashing)
        {
            StopCoroutine(BossFlash(0, 0.0f));
        }
    }

    public void BossAttack()
    {
        Transform target;

        int chooseTarget = Random.Range(1, 5);
        switch (chooseTarget)
        {
            case 1:
                target = spawnOne;
                break;
            case 2:
                target = spawnTwo;
                break;
            case 3:
                target = spawnThree;
                break;
            case 4:
                target = spawnFour;
                break;
            default:
                target = spawnOne;
                break;
        }

        Instantiate(minion, target);
    }

    public void SetSequencePillar()
    {
        sequencePillar = bossHandler.GetComponent<BossHandler>().GetSequencePillar();
        spHits = sequencePillar.GetComponent<PillarBehavior>().numOfHitsReq;
        spColor = sequencePillar.GetComponent<PillarBehavior>().pillarColor;
        switch (spColor)
        {
            case "red":
                flashMaterial = redFlash;
                break;
            case "orange":
                flashMaterial = orangeFlash;
                break;
            case "yellow":
                flashMaterial = yellowFlash;
                break;
            case "green":
                flashMaterial = greenFlash;
                break;
            case "blue":
                flashMaterial = blueFlash;
                break;
            case "purple":
                flashMaterial = purpleFlash;
                break;
            default:
                flashMaterial = redFlash;
                break;
        }

        Debug.Log("Set Sequence Pillar. Pillar Color is " + spColor + ". It requires " + spHits + ".");
    }

    public void RoarFunction()
    {
        if (bossRoarFlash != null)
        {
            StopCoroutine(bossRoarFlash);
        }
        //starts by calculating time for each flash
        //set flash material
        flashTime = 1.0f / spHits;
        if (!bossHandler.GetComponent<BossHandler>().isStunned)
        {
            Debug.Log("Starting Coroutine");
            bossRoarFlash = StartCoroutine(BossFlash(spHits, flashTime));
        }
    }
    public void StopRoar()
    {
        if (bossRoarFlash != null)
        {
            StopCoroutine(bossRoarFlash);
        }
    }

    public void TakeDamage()
    {
        bossHandler.GetComponent<BossHandler>().BossTakeDamage();
    }

    public IEnumerator BossFlash(int flashes, float flashTime)
    {
        Debug.Log("No. of flashes: " + flashes);
        isFlashing = true;
        //for loop (number of loops equals flashes)
        for (int i = 1; i <= flashes; i++)
        {
            mouthSR.material = flashMaterial; Debug.Log("Blink");
            yield return new WaitForSeconds(flashTime);
            mouthSR.material = originalMaterial;
            //Debug.Log("Roaring");
            yield return new WaitForSeconds(flashTime);
        }
        //Debug.Log("Finished Roar");
        isFlashing = false;
        bossRoarFlash = null;
        StopRoar();
    }

    public void DestroyBoss()
    {
        Destroy(this.gameObject, 0.5f);
    }
}