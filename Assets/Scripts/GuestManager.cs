using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public GameObject HotelEnterPosition;
    public GameObject CatSpawnPosition;
    public GameObject CatPrefab;
    public static GuestManager _this;

    public int GuestNum = 1;
    public int CurrentGuestNum = 0;
    private float SpawnTime = 1.0f;


    void Start()
    {
        if (_this == null) _this = this;
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTime);

            if (CurrentGuestNum < GuestNum)
            {
                Instantiate(CatPrefab, CatSpawnPosition.transform.position, Quaternion.identity);
                CurrentGuestNum++;
            }
        }
    }
}
