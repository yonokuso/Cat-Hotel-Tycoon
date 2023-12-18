using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    // IAP CataLog Product �Է��� ID�� ���ڿ��� �����Ѵ�.
    private const string _test01 = "com.testCompany.testgame.test01";
    private const string _test11 = "com.testCompany.testgame.test11";

    // ���� ���� �� ����Ǵ� �Լ�
    public void OnpurchaseComplete(Product product)
    {
        if (product.definition.id == _test01)
        {
            // ���� ����
            // GameManager.inst.AddFlag = false;
            Debug.Log("���� �����߽��ϴ�.");
        }

        else if (product.definition.id == _test11)
        {
            // �򸣸� �����Ѵ�.
            // GameManager.inst.Addchu(1);
            Debug.Log("�򸣸� �����߽��ϴ�.");
        }
    }


    // ���� ���� �� ����Ǵ� �Լ�
    public void OnpurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + "�� " + reason + "�� ������ ���Ÿ� �����Ͽ����ϴ�.");
    }
}
