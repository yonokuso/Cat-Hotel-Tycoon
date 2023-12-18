using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int CoinInt; // ����
    public int CryInt; // ũ����Ż
    private Text CoinText; // ������ ǥ���� ������Ʈ
    private Text CryText; // ũ����Ż�� ǥ���� ������Ʈ
    private GameObject Message; // �޽��� ������Ʈ
    private Text MSG; // �޽��� ����
    private float Timer; // Ÿ�̸�
    private bool TimeSet; // Ÿ�̸� �۵�����


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        CoinText = GameObject.Find("UICanvas").transform.Find("��ȭ").transform.Find("CatCoin").transform.Find("Text").gameObject.GetComponent<Text>();
        CryText = GameObject.Find("UICanvas").transform.Find("��ȭ").transform.Find("Crystal").transform.Find("Text").gameObject.GetComponent<Text>();

        CoinInt = 500; // PlayerPrefs ���� ����Ǿ��ִ� 'Coin'�� �ҷ��� CoinInt�� �����մϴ�. ���࿡ ����� ������ ���ٸ� 0�� �����մϴ�.
        CryInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("CatCoin", CoinInt); // CoinInt�� PlayerPrefs ���� ����Ǿ��ִ� 'CatCoin'�� �����մϴ�.
        PlayerPrefs.SetInt("Crystal", CryInt);

        CoinText.text = CoinInt.ToString(); //CoinText�� Text�� CoinInt�� ����մϴ�.   
        CryText.text = CryInt.ToString();

        if (TimeSet == true) // TimeSet�� True��
        {
            Timer += Time.deltaTime; // Ÿ�̸Ӱ� �۵��մϴ�.
            if (Timer > 2.0f) // 2�ʰ� ������
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
    public void GetMoney(int money) //���� ����ϴ�.
    {
        CoinInt += money;
        Debug.Log($"{money} ������ �����!");
    }

    public void lostMoney(int money) // ���� �ҽ��ϴ�.
    {
        if (CoinInt >= money)
        {
            CoinInt -= money;
            Debug.Log("������ ����ߴ�.");
        }
        else
        {
            GuideManager.instance.SetGuideMessage("������ �����մϴ�");
            GuideManager.instance.OpenGuideBox();
            
            TimeSet = true;
        }
    }
}
