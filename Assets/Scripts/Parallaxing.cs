using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds; //layers to be parallaxed
    private float[] parallaxScales; //proportion of cameras movement to move background by.
    public float smoothing = 1f; // how smoot is this parallax. Make sure value is bigger than 0.

    public Transform cam; //reference to the camera.
    private Vector3 previousCamPos; //the position of the camera in the previous frame.

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        //Assigning corresponding parallax scales.
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //Parallax is the opposite of the camera movement.
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            //Set the new position
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            //Create a target position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX,backgrounds[i].position.y,backgrounds[i].position.z);

            //fade between current position and the target position. Using lerp.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,backgroundTargetPos,smoothing*Time.deltaTime);
        }
        
        // set the latest position of the camera.
        previousCamPos = cam.position;
    }
}
