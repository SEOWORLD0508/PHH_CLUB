using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //How much time elapses between position changes?
    public float interval;

    //How far does the camera move from the original position?
    public float offset;

    //Original camera position prior to shake, used for reverting after effect
    private Vector3 originalPos;

    //The temporary location moved to during each iteration of the shake coroutine
    private Vector3 shakePos;

    //The time elapsed in the timer
    private float elapsed;

    //The duration of the effect
    public float duration;

    //Is the effect currently occurring?
    private bool shaking = false;

    // Update is called once per frame
    void Update()
    {
        //Checks if effect is not running
        if (shaking == false)
        {
            //Set original position
            originalPos = transform.position;
        }

        //Checks for condition and if the effect is not currently playing (optional)


        //Do code if effect is happening
        if (shaking == true)
        {
            //Track the time that has passed
            elapsed += Time.deltaTime;
            if (elapsed >= duration)
            {
                //If the elapsed time is greater than or equal to the duration then stop the effect
                StopShake();
            }
        }
    }

    public void ShakeStart()
    {
        if (shaking == false)
        {
            //Set up variables
            shaking = true;
            elapsed = 0f;

            //Start the effect
            StartCoroutine("Shake");

            //Sets interval to appropriate time based on fixed timescale (set to 60FPS for best consistency)
            if (interval < Time.fixedDeltaTime)
            {
                interval = Time.fixedDeltaTime;
            }
        }
    }

    //Coroutine containing shake code
    IEnumerator Shake()
    {
        //All occurs while effect is active
        while (shaking == true)
        {
            //Calculate camera offset using original position and offset value
            shakePos = originalPos + new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0f);

            //Apply offset
            transform.SetPositionAndRotation(shakePos, Quaternion.identity);

            //Waits for appropriate interval until continuing
            yield return new WaitForSeconds(interval);
        }
    }

    //Resets code after effect has finished
    void StopShake()
    {
        //Stops running coroutine
        StopCoroutine("Shake");

        //Resets shaking bool variable
        shaking = false;

        //Resets camera position to original (before effect started)
        transform.SetPositionAndRotation(originalPos, Quaternion.identity);
    }
}