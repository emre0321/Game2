using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformModel : ObjectModel
{
    public int INDEX;


    public PlatformMovementType MovementType;
    public bool IsLastPlatform;
    public bool IsReferancePlatform;
    public bool IsPlatformMoving;
    public bool IsPlatformThin;
    public Collider MainCollider;
    [SerializeField] float PlatformMovementSpeed;
    [SerializeField] Vector3 DefaultScaleValue;
    [SerializeField] Rigidbody PlatformRb;

    IEnumerator CurrentMovementCoroutine;
    bool CollisionLimitor;

    public void ResetPlatform()
    {
        //MovementType = PlatformMovementType.None;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = DefaultScaleValue;
        IsReferancePlatform = false;
        IsPlatformMoving = false;
        IsPlatformThin = false;
        IsLastPlatform = false;
        CollisionLimitor = false;
        MainCollider.enabled = true;
    }


    public void SetPlatformMovement(bool isStart)
    {
        if (isStart)
        {
            IsPlatformMoving = true;
            CurrentMovementCoroutine = PlatformMovement();
            StartCoroutine(CurrentMovementCoroutine);
        }
        else
        {
            IsPlatformMoving = false;
            StopCoroutine(CurrentMovementCoroutine);
        }

    }

    public IEnumerator PlatformMovement()
    {
        while (true)
        {
            switch (MovementType)
            {
                case PlatformMovementType.Left:
                    transform.Translate(Vector3.right * Time.deltaTime * PlatformMovementSpeed);
                    //transform.position = Vector3.MoveTowards(transform.position,new Vector3(Time.deltaTime,transform.position.y,transform.position.z),Time.deltaTime);
                    break;
                case PlatformMovementType.Right:
                    transform.Translate(Vector3.left * Time.deltaTime * PlatformMovementSpeed);
                    //transform.position = Vector3.MoveTowards(transform.position, new Vector3(-Time.deltaTime, transform.position.y, transform.position.z), Time.deltaTime);
                    break;

            }
            yield return null;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (GameController.Instance.CurrentGameState != GameStates.Gameplay)
            return;

        if (CollisionLimitor == true)
            return;

        if (other.GetComponent<PlayerModel>())
        {
            CollisionLimitor = true;

            if (IsPlatformMoving || IsPlatformThin)
            {
                GameController.ChangeGameState(GameStates.LevelFail);
            }
            else
            {
                LevelController.Instance.SetPlayerTargetPlatform(this);
            }

            if (IsReferancePlatform && !IsLastPlatform && IsPlatformThin == false)
            {
                Debug.Log("GENERATE PLATFORM WITH = " +  INDEX);
                LevelController.Instance.GenerateMovingPlatform();
            }

        }
    }

    public void Drop()
    {
        StartCoroutine(SetDrop());
    }

    IEnumerator SetDrop()
    {
        PlatformRb.isKinematic = false;
        yield return new WaitForSeconds(3f);
        ResetPlatform();
        PlatformRb.isKinematic = true;
        SetDeactive();
    }
}
