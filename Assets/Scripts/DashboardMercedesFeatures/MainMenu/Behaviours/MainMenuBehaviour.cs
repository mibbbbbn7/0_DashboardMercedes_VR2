using System;
using System.Collections;
using System.Collections.Generic;
using DashboardMercedes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum Pages
{
    Splash,
    MainMenu,
    Play,
    Settings,
    Credits,
    Quit,
}


public class MainMenuBehaviour : BaseMonoBehaviour<IMainMenuFeatureInternal>
{

    [SerializeField] private CanvasGroup _menuCanvasGroup;
    [SerializeField] private float _fadeDuration = 1;
    [SerializeField] private List<MenuButtonsData> _myMenuButtons = new();

    private Action<Pages> _myOpenPageAction;

    protected override void ManagedAwake()
    {
        base.ManagedAwake();

        foreach (var item in _myMenuButtons)
        {
            if (item.Button != null)
            {
                item.Button.onClick.AddListener(() => DoOnClick(item.PageToOpen));
            }
            _myOpenPageAction += item.OnPageToOpenEvent;
        }
    }

    private void DoOnClick(Pages targetPage)
    {
        StartCoroutine(MyFaderoutine(targetPage));
    }

    private IEnumerator MyFaderoutine(Pages targetPage)
    {
        float currentTime = 0;

        while(currentTime <= _fadeDuration / 2f)
        {
            _menuCanvasGroup.alpha = Mathf.Lerp(1, 0, currentTime / (_fadeDuration / 2));
            currentTime += Time.deltaTime;
            yield return null;
        }

        _menuCanvasGroup.alpha = 0;

        _myOpenPageAction?.Invoke(targetPage);

        currentTime = 0;

        while (currentTime <= _fadeDuration / 2f)
        {
            _menuCanvasGroup.alpha = Mathf.Lerp(0, 1, currentTime / (_fadeDuration / 2f));
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}

[Serializable]
public class MenuButtonsData
{
    public Button Button;
    public Pages PageToOpen;

    public GameObject Page;

    public void OnPageToOpenEvent(Pages pageOnClick)
    {
        switch (PageToOpen)
        {
            case Pages.Play:
                if(pageOnClick == Pages.Play)
                {
                    Debug.Log("Play The Game");
                }
                return;
            case Pages.Quit:
                if (pageOnClick == Pages.Quit)
                {
#if !UNITY_EDITOR
                    Application.Quit();
#else
                    EditorApplication.isPlaying = false;
#endif
                }
                return;
        }

        if(Page != null)
        {
            Page.SetActive(PageToOpen == pageOnClick);
        }
    }
}
