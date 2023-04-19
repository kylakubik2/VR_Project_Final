using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MovePlayer : MonoBehaviour
{
    // this value 
    public SteamVR_Action_Vector2 moveValue;
    // this is the maximum speed the player can go
    public float maxSpeed;
    // this value will be used as a multiplier to indicate how much control the joystick movement has on speed
    public float sensitivity;
    // this rigidbody will be used to determine when the player is close to a collider
    public Rigidbody head;

    private float speed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (head.SweepTest(Player.instance.hmdTransform.TransformDirection(Vector3.forward), out hit, 0.5f))
        {

        } else
        {
            // if the joystick is moved forward
            if (moveValue.axis.y > 0)
            {
                // create a new vector3 that gets the direction the HMD is facing (not sure what HMD is)
                Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, moveValue.axis.y));
                // multiplies the joystick value by the sensitivity value
                speed = moveValue.axis.y * sensitivity;
                // ensures the speed doesn't go above the max speed
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                // move the player prefab along the horizontal plane in the direction specified earlier
                transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            }
        }
    }
}
