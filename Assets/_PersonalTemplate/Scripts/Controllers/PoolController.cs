using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : ControllerBaseModel
{
    public List<PoolModel> AllPools;

    public override void Initialize()
    {
        for (int i = 0; i < AllPools.Count; i++)
        {
            AllPools[i].Initialize();
        }
    }

}
