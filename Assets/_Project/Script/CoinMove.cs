using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinMove : MonoBehaviour
{
    public GameObject coin;
    [SerializeField]
    private string coin_Sound2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Debug.Log("¹Ù´Ú °¨Áö");
            coin.transform.DOJump(new Vector3(2,0), 200f, 2, 2f);
            // DOJump(Vector3 to, float jumpPower, int numJumps, float duration, bool snapping(true))
        }
        
        SoundManager.instance.PlaySE(coin_Sound2);

        Invoke("coinActive", 2);

    }

    void coinActive()
    {
        coin.SetActive(false);
    }

}
