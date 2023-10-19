using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public PanelHandler popupWindow;


    public void MainBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void OptionBtn()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.95f, 0.1f));
        seq.Append(transform.DOScale(1.05f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() => {
            popupWindow.Show();
        });
    }

    public void TitleBtn()
    {
        SceneManager.LoadScene("Title");
    }

    public void CloseBtn()
    {
        var seq = DOTween.Sequence();

        //seq.Append(transform.DOScale(0.95f, 0.1f));
        //seq.Append(transform.DOScale(1.05f, 0.1f));
        //seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() => {
            popupWindow.Hide();
        });
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}

