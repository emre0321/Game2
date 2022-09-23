using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] InitiliazeType InitType;
    [SerializeField] List<ObjectModel> InitializeElements;

    private void Awake()
    {
        if(InitType == InitiliazeType.OnAwake)
        {
            SetInit();
        }
    }

    private void Start()
    {
        if (InitType == InitiliazeType.OnStart)
        {
            SetInit();
        }
    }

    public void SetInit()
    {
        for (int i = 0; i < InitializeElements.Count; i++)
        {
            InitializeElements[i].Initialize();
        }
    }

}
