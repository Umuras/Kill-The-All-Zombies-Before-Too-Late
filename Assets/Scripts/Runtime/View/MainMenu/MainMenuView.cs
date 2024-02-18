using DG.Tweening;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MainMenuView : EventView
{
    public TextMeshProUGUI gameTitleText;
    private Button PlayButton;
    private Button ExitButton;

    protected override void Start()
    {
        gameTitleText.DOColor(Color.red, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnPlayButtonClick()
    {
        dispatcher.Dispatch(MainMenuEvent.Play);
    }

    public void OnExitButtonClick()
    {
        dispatcher.Dispatch(MainMenuEvent.Exit);
    }
}
