using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static int CoinInt; // 코인
    public static int CryInt; // 크리스탈
    public static Text CoinText; // 코인을 표시할 오브젝트
    public static Text CryText; // 크리스탈을 표시할 오브젝트
    public static GameObject Message; // 메시지 오브젝트
    public static Text MSG; // 메시지 내용

    public static float Timer; // 타이머
    public static bool TimeSet; // 타이머 작동여부


    // Start is called before the first frame update
    void Start()
    {
        CoinText = GameObject.Find("Canvas").transform.Find("재화").transform.Find("CatCoin").transform.Find("Text").gameObject.GetComponent<Text>();
        CryText = GameObject.Find("Canvas").transform.Find("재화").transform.Find("Crystal").transform.Find("Text").gameObject.GetComponent<Text>();

        Message = GameObject.Find("Canvas").transform.Find("재화").transform.Find("Message").gameObject; // Canvas-Message
        MSG = GameObject.Find("Canvas").transform.Find("재화").transform.Find("Message").transform.Find("Text").gameObject.GetComponent<Text>();


        CoinInt = PlayerPrefs.GetInt("CatCoin", 0); // PlayerPrefs 내에 저장되어있는 'Coin'을 불러와 CoinInt에 저장합니다. 만약에 저장된 정보가 없다면 0을 저장합니다.
        CryInt = PlayerPrefs.GetInt("Crystal", 0);

        Message.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("CatCoin", CoinInt); // CoinInt를 PlayerPrefs 내에 저장되어있는 'CatCoin'에 저장합니다.
        PlayerPrefs.SetInt("Crystal", CryInt);

        CoinText.text = CoinInt.ToString(); //CoinText의 Text에 CoinInt를 출력합니다.
        CryText.text = CryInt.ToString();

        if (TimeSet == true)
        {
            Timer += Time.deltaTime; // 타이머 작동
            if (Timer > 2.0f) // 2초 지나면
            {
                Message.SetActive(false);
                MSG.text = null;
                Timer = 0;
                TimeSet = false;
            }
        }
    }

    public void GetMoney() //돈을 얻습니다.
    {
        CoinInt += 40;
        Debug.Log("코인을 얻었다!");
    }

    public void lostMoney() // 돈을 잃습니다.
    {
        if (CoinInt >= 40)
        {
            CoinInt -= 40;
            Debug.Log("코인을 사용했다.");
        }
        else
        {
            Message.SetActive(true); // 메시지 오브젝트를 활성화합니다. 
            MSG.text = "코인이 부족합니다.".ToString(); // MSG의 Text를 "돈이 부족합니다."로 출력합니다.
            TimeSet = true; // TimeSet를 true로 합니다.
        }
    }


    [SerializeField]
    private string coin_Sound2;

    public void Coin2()
    {
        SoundManager.instance.PlaySE(coin_Sound2);

    }
}
