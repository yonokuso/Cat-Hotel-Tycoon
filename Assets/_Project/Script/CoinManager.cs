using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static int CoinInt; // ����
    public static int CryInt; // ũ����Ż
    public static Text CoinText; // ������ ǥ���� ������Ʈ
    public static Text CryText; // ũ����Ż�� ǥ���� ������Ʈ
    public static GameObject Message; // �޽��� ������Ʈ
    public static Text MSG; // �޽��� ����

    public static float Timer; // Ÿ�̸�
    public static bool TimeSet; // Ÿ�̸� �۵�����


    // Start is called before the first frame update
    void Start()
    {
        CoinText = GameObject.Find("Canvas").transform.Find("��ȭ").transform.Find("CatCoin").transform.Find("Text").gameObject.GetComponent<Text>();
        CryText = GameObject.Find("Canvas").transform.Find("��ȭ").transform.Find("Crystal").transform.Find("Text").gameObject.GetComponent<Text>();

        Message = GameObject.Find("Canvas").transform.Find("��ȭ").transform.Find("Message").gameObject; // Canvas-Message
        MSG = GameObject.Find("Canvas").transform.Find("��ȭ").transform.Find("Message").transform.Find("Text").gameObject.GetComponent<Text>();


        CoinInt = PlayerPrefs.GetInt("CatCoin", 0); // PlayerPrefs ���� ����Ǿ��ִ� 'Coin'�� �ҷ��� CoinInt�� �����մϴ�. ���࿡ ����� ������ ���ٸ� 0�� �����մϴ�.
        CryInt = PlayerPrefs.GetInt("Crystal", 0);

        Message.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("CatCoin", CoinInt); // CoinInt�� PlayerPrefs ���� ����Ǿ��ִ� 'CatCoin'�� �����մϴ�.
        PlayerPrefs.SetInt("Crystal", CryInt);

        CoinText.text = CoinInt.ToString(); //CoinText�� Text�� CoinInt�� ����մϴ�.
        CryText.text = CryInt.ToString();

        if (TimeSet == true)
        {
            Timer += Time.deltaTime; // Ÿ�̸� �۵�
            if (Timer > 2.0f) // 2�� ������
            {
                Message.SetActive(false);
                MSG.text = null;
                Timer = 0;
                TimeSet = false;
            }
        }
    }

    public void GetMoney() //���� ����ϴ�.
    {
        CoinInt += 40;
        Debug.Log("������ �����!");
    }

    public void lostMoney() // ���� �ҽ��ϴ�.
    {
        if (CoinInt >= 40)
        {
            CoinInt -= 40;
            Debug.Log("������ ����ߴ�.");
        }
        else
        {
            Message.SetActive(true); // �޽��� ������Ʈ�� Ȱ��ȭ�մϴ�. 
            MSG.text = "������ �����մϴ�.".ToString(); // MSG�� Text�� "���� �����մϴ�."�� ����մϴ�.
            TimeSet = true; // TimeSet�� true�� �մϴ�.
        }
    }


    [SerializeField]
    private string coin_Sound2;

    public void Coin2()
    {
        SoundManager.instance.PlaySE(coin_Sound2);

    }
}
