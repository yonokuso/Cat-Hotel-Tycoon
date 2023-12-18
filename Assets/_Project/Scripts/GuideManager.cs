using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MessageBox;
    public Text GuideMessage;
    public static GuideManager instance;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        if(MessageBox != null)
        {
            HideGuideBox();
        }
    }

    public void SetGuideMessage(string message)
    {
        GuideMessage.text = message;
    }
    public void OpenGuideBox()
    {
        MessageBox.SetActive(true);
    }
    public void HideGuideBox()
    {
        MessageBox.SetActive(false);
    }
}
