using System.Collections;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
    public AudioClip[] FootstepsSFX;
    public AudioClip JumpSFX;
    public AudioClip LandSFX;

    private MovementScript currentMoveVelocity;
    private bool isGrounded = true;

    void Start()
    {
        currentMoveVelocity = GetComponent<MovementScript>();
        StartCoroutine(PlayFootSteps());
    }

    void Update()
    {
        bool groundedNow = currentMoveVelocity.isGrounded;

        if (isGrounded && !groundedNow)
        {
            AudioManagerScript.instance.PlaySFX(JumpSFX);
        }

        else if (!isGrounded && groundedNow)
        {
            AudioManagerScript.instance.PlaySFX(LandSFX);
        }

        isGrounded = groundedNow;
    }

    IEnumerator PlayFootSteps()
    {
        while (true)
        {
            if (currentMoveVelocity.currentMoveVelocity.magnitude > 0.1f && currentMoveVelocity.isGrounded)
            {
                int randomIndex = Random.Range(0, FootstepsSFX.Length);
                AudioManagerScript.instance.PlaySFX(FootstepsSFX[randomIndex]);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}