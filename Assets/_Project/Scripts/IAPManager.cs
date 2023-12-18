using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    // IAP CataLog Product 입력한 ID를 문자열로 선언한다.
    private const string _test01 = "com.testCompany.testgame.test01";
    private const string _test11 = "com.testCompany.testgame.test11";

    // 구매 성공 시 실행되는 함수
    public void OnpurchaseComplete(Product product)
    {
        if (product.definition.id == _test01)
        {
            // 광고 제거
            // GameManager.inst.AddFlag = false;
            Debug.Log("광고를 제거했습니다.");
        }

        else if (product.definition.id == _test11)
        {
            // 츄르를 구매한다.
            // GameManager.inst.Addchu(1);
            Debug.Log("츄르를 구매했습니다.");
        }
    }


    // 구매 실패 시 실행되는 함수
    public void OnpurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + "가 " + reason + "의 이유로 구매를 실패하였습니다.");
    }
}
