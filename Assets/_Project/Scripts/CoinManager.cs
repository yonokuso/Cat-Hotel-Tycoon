using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int CoinInt; // 코인
    public int CryInt; // 크리스탈
    private Text CoinText; // 코인을 표시할 오브젝트
    private Text CryText; // 크리스탈을 표시할 오브젝트
    private GameObject Message; // 메시지 오브젝트
    private Text MSG; // 메시지 내용
    private float Timer; // 타이머
    private bool TimeSet; // 타이머 작동여부


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        CoinText = GameObject.Find("UICanvas").transform.Find("재화").transform.Find("CatCoin").transform.Find("Text").gameObject.GetComponent<Text>();
        CryText = GameObject.Find("UICanvas").transform.Find("재화").transform.Find("Crystal").transform.Find("Text").gameObject.GetComponent<Text>();

        CoinInt = 500; // PlayerPrefs 내에 저장되어있는 'Coin'을 불러와 CoinInt에 저장합니다. 만약에 저장된 정보가 없다면 0을 저장합니다.
        CryInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("CatCoin", CoinInt); // CoinInt를 PlayerPrefs 내에 저장되어있는 'CatCoin'에 저장합니다.
        PlayerPrefs.SetInt("Crystal", CryInt);

        CoinText.text = CoinInt.ToString(); //CoinText의 Text에 CoinInt를 출력합니다.   
        CryText.text = CryInt.ToString();

        if (TimeSet == true) // TimeSet이 True면
        {
            Timer += Time.deltaTime; // 타이머가 작동합니다.
            if (Timer > 2.0f) // 2초가 지나면
            {
                MSG.text = null;
                Timer = 0;
                TimeSet = false;
            }
        }
    }

    public bool CanBuy(int money)
    {
        if (CoinInt >= money) return true;
        else return false;
    }
    public void GetMoney(int money) //돈을 얻습니다.
    {
        CoinInt += money;
        Debug.Log($"{money} 코인을 얻었다!");
    }

    public void lostMoney(int money) // 돈을 잃습니다.
    {
        if (CoinInt >= money)
        {
            CoinInt -= money;
            Debug.Log("코인을 사용했다.");
        }
        else
        {
            GuideManager.instance.SetGuideMessage("코인이 부족합니다");
            GuideManager.instance.OpenGuideBox();
            
            TimeSet = true;
        }
    }
}
