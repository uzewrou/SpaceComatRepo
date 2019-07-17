using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
[Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f;
[Tooltip("In m")] [SerializeField] float xRange = 25f;
[Tooltip("In m")] [SerializeField] float yRange = 15f;
[SerializeField] float positionPitchFactor = -2f;
[SerializeField] float controlPitchFactor = -2f;
[SerializeField] float positionYawFactor = 1.5f;
[SerializeField] float controlRollFactor = -20f;
float xThrow, yThrow;
bool isControlEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void ProcessTranslation()
	{
	 xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        //Offsets
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * xSpeed * Time.deltaTime;

        //X raw
        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);


        //Y raw
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z); //change only Z, not Y and X.
    }

	void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }


	void OnPlayerDeath()
	{
	isControlEnabled = false;
	}
    // Update is called once per frame
    void Update()
    {


		if (isControlEnabled)
		{
		ProcessTranslation();
		ProcessRotation();
		}
	
    }
}
