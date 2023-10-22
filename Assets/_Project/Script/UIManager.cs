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

    public void ShowPanel()
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

    public void ShowProduct()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.95f, 0.1f));
        seq.Append(transform.DOScale(1.05f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.1f));
        seq.Play().OnComplete(() => {
            gameObject.SetActive(true);
        });
        seq.Append(transform.DOScale(1.1f, 0.1f));
        seq.Append(transform.DOScale(1f, 0.1f));
        seq.Play();
    }

    public void ProductClose()
    {
        var seq = DOTween.Sequence();

        transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(1.1f, 0.1f));
        seq.Append(transform.DOScale(0.2f, 0.1f));

        seq.Play().OnComplete(() => {
            gameObject.SetActive(false);
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

