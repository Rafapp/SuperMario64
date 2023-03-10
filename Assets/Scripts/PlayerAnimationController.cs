using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed;
    private float lerp;

    private Animator anim;
    private PlayerController playerController;

    private bool jumping;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        jumping = false;
        anim.SetFloat("Blend", 0f);
    }
    private void Update()
    {
        // Handle jump key
        if (playerController.jumpInput && playerController.isGrounded) {
            jumping = true;
            StartCoroutine(Jump());
        }

        if (!jumping)
        {
            if (playerController.horizontalInput + playerController.verticalInput != 0)
            {
                lerp = .5f;
            }
            else lerp = 0f; // 0 Is for idle
        }

        // Set the blend to current lerp value
        anim.SetFloat("Blend", lerp, lerpSpeed, Time.deltaTime);
    }
    private IEnumerator Jump() {
        lerp = 1f;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + .125f);
        jumping = false;
    }
}
