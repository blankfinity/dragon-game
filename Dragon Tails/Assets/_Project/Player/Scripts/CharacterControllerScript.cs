using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class CharacterControllerScript : MonoBehaviour
{

    Rigidbody rigidBody;
    new Collider collider;

    public GameObject fireBase;
    public GameObject firePrefab;
    AnimationScript animator;

    bool isFlying;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponent<AnimationScript>();
        animator.SetAnimation(AnimationScript.AnimationState.Idle);
        StartFlying();
    }

    // Update is called once per frame
    void Update()
    {
        aimToTapLocation();
    }

    private void FixedUpdate()
    {
        Fly();
    }

    void StartFlying()
    {
        isFlying = true;
        rigidBody.useGravity = false;

    }

    void StopFlying()
    {
        isFlying = false;
        rigidBody.useGravity = true;
        animator.SetAnimation(AnimationScript.AnimationState.Idle);

    }

    void Fly()
    {
        animator.SetAnimation(AnimationScript.AnimationState.Fly);        
    }

    IEnumerator Attack(bool isFiring)
    {
        if (isFiring)
        {
            animator.SetAnimation(AnimationScript.AnimationState.Fire);
            yield return new WaitForSeconds(0.7f);
            GameObject fire = Instantiate(firePrefab);
            fire.transform.SetParent(fireBase.transform);
            fire.transform.localPosition = Vector3.zero;
            //fire.transform.position = fire.transform.position + (fire.transform.forward *0.5f);
            fire.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //Physics.IgnoreCollision(collider, fire.GetComponent<ParticleSystem>().);
            Destroy(fire, 5);
            Quaternion rot = fire.transform.rotation;
            while (fire != null)
            {
                fire.transform.rotation = rot;
                yield return new WaitForSeconds(0.1f);
            }

        }
    }

    void aimToTapLocation()
    {
        Vector2 touchPos = new Vector2(-1, -1);
        if (CnInputManager.TouchCount > 0)
        {
            touchPos = CnInputManager.GetTouch(0).position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchPos = Input.mousePosition;
        }
        if (touchPos.x != -1)
        {
            Ray WorldPos = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;
            if (Physics.Raycast(WorldPos, out hit, 500f))
            {
                fireBase.transform.LookAt(hit.point);
            }
            else
            {
                fireBase.transform.LookAt(Camera.main.ScreenToWorldPoint(touchPos) + Camera.main.transform.forward * 50f);
            }
            StartCoroutine("Attack", true);
        }
    }
}
