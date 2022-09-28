using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public PoolModel PlatformPool;

    [SerializeField] PlatformModel ReferancePlatform;
    [SerializeField] PlatformModel MovingPlatform;

    [SerializeField] float ToleranceOffset;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //SAG TUSLA PLATFORMLARI OLUSTUR
            GeneratePlatforms();
        }

        if (Input.GetMouseButtonDown(0))
        {
            // SOL TUSLA KESME ISLEMI
            CalculateCutProcess();

        }
    }

    public void GeneratePlatforms()
    {
        ReferancePlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        ReferancePlatform.SetActive();

        MovingPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        MovingPlatform.transform.position = ReferancePlatform.transform.position + new Vector3(0, 0, 3);
        MovingPlatform.SetActive();
    }

    public void CalculateCutProcess()
    {
        bool isLeft = ReferancePlatform.transform.position.x - MovingPlatform.transform.position.x < 0 ? true : false;

        float xPositionDiff = Mathf.Abs(ReferancePlatform.transform.position.x - MovingPlatform.transform.position.x); // iki kubun arasindaki total mesafe farki
        Debug.Log("X POSS DIF =" + xPositionDiff);

        Debug.Log("FAZLALIK VAR");
        float overOffset = (MovingPlatform.transform.localScale.x / 2) - xPositionDiff;

        float newXPivot = ReferancePlatform.transform.position.x + xPositionDiff / 2;
        float newXScale = xPositionDiff + (2 * overOffset);

        // SAHNEDE KALAN PARCA
        PlatformModel newReferancePlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        newReferancePlatform.SetActive();
        newReferancePlatform.transform.position = new Vector3(isLeft == true ? newXPivot : -newXPivot, 0, 3);
        newReferancePlatform.transform.localScale = new Vector3(newXScale, 1, 3);

        MovingPlatform.SetDeactive();

        // ASAGI DUSEN PARCA

        PlatformModel dropPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        dropPlatform.SetActive();

        float dropPlatformScaleX = Mathf.Abs(ReferancePlatform.transform.localScale.x - newReferancePlatform.transform.localScale.x);
        float dropPlatformPositionX =isLeft == false ? -(ReferancePlatform.transform.localScale.x / 2 + dropPlatformScaleX / 2) + ReferancePlatform.transform.position.x : ((ReferancePlatform.transform.localScale.x / 2) + (dropPlatformScaleX / 2)) + ReferancePlatform.transform.position.x;
        dropPlatform.transform.localScale = new Vector3(dropPlatformScaleX,1,3);
        dropPlatform.transform.position = new Vector3(dropPlatformPositionX, 0, 3);




        #region METHOD 1 ( OLD )

        //    float xPositionDiff = ReferancePlatform.transform.position.x - MovingPlatform.transform.position.x; // iki kubun arasindaki total mesafe farki

        //    float spaceOffset = xPositionDiff - (MovingPlatform.transform.localScale.x / 2);

        //    float calculatedPosX = ((ReferancePlatform.transform.position.x + spaceOffset) + (MovingPlatform.transform.position.x - spaceOffset)) / 2;

        //    PlatformModel tempPlatform = PlatformPool.GetDeactiveItem<PlatformModel>(); // SAHNEDE KALACAK OLAN KUP
        //    tempPlatform.SetActive();
        //    tempPlatform.transform.position = new Vector3(calculatedPosX, 0, MovingPlatform.transform.position.z);
        //    tempPlatform.transform.localScale = new Vector3(calculatedPosX, 1, 3);

        //    PlatformModel secondPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        //    secondPlatform.SetActive();
        //    secondPlatform.transform.localScale = new Vector3(MovingPlatform.transform.localScale.x - calculatedPosX, 1, 3);
        //    float secondPlatformX = (MovingPlatform.transform.position.x + calculatedPosX) / 2;
        //    secondPlatform.transform.position = new Vector3(calculatedPosX + secondPlatformX, 0, MovingPlatform.transform.position.z);

        //    MovingPlatform.SetDeactive();

        // secondCube.AddComponent<Rigidbody>();

        #endregion

    }
}
