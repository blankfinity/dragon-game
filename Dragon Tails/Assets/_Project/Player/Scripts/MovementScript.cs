using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class MovementScript : MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;


    private Quaternion m_CharacterTargetRot;
    private Transform character;
    private Rigidbody rigidBody;

    public float FlightSpeed;
    public float TurnSpeed;
    public float MoveSpeed;

    public float MaxHeight = 90;
    public float MinHeight = 0;

    public void Start()
    {
        character = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();
		m_CharacterTargetRot = transform.localRotation;
    }


    public void FixedUpdate()
    {
        FlightMovement();
		LookRotation();
    }


    public void FlightMovement()
    {
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, MinHeight, MaxHeight), transform.position.z);

        Vector3 movementVector = Vector3.zero;
        movementVector = transform.TransformDirection(inputVector.x * MoveSpeed, inputVector.y * MoveSpeed, FlightSpeed);
        rigidBody.velocity = movementVector * Time.deltaTime;
    }

    public void LookRotation()
    {
        float yRot = CnInputManager.GetAxis("Mouse X") * XSensitivity;
        float xRot = CnInputManager.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(-xRot, yRot, 0f);		

        if (clampVerticalRotation)
            m_CharacterTargetRot = ClampRotationAroundXAxis(m_CharacterTargetRot);

        if (smooth)
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = m_CharacterTargetRot;
        }
        character.localEulerAngles = new Vector3(character.localRotation.eulerAngles.x, character.localRotation.eulerAngles.y, 0);
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}

