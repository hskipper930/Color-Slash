using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    Vector2 movement;

    //AK - player aiming and attacking script
    [SerializeField] Camera cam;
    Vector2 mousePos;
    [SerializeField] GameObject playerAim;
    [SerializeField] Rigidbody2D aimRB;

    void Start()
    {
        //AK- Sets the Aiming device to the center of the player object.
        Transform pAim = transform.Find("playerAim");
        pAim.localPosition = new Vector2(0, 0);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //AK - player aiming
        //get mouse position and set it to the game world
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //AK - player aiming
        Vector2 attackDir = mousePos - aimRB.position;
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg + 90f;
        aimRB.rotation = angle;

        //AK- The playerAim child flies into the -y; this part of the script is to reset it to keep it stationary (hopefully)
        playerAim.transform.position = transform.position + new Vector3 (0, 0, 0);
    }

}

/* Credits (For things used to assist or otherwise)
 * AK- Aiming the attack script parts- the Brackeys youtube channel
 * AK- For keeping the playerAim aiming device conected to the parent- https://www.youtube.com/watch?v=SZChVvy4enQ
 */
