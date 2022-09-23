using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformModel : ObjectModel
{
    [SerializeField] Vector3 DefaultScaleValue;
    public PlatformMovementType MovementType;

    public void ResetPlatform()
    {
        transform.localScale = DefaultScaleValue;

    }
}
