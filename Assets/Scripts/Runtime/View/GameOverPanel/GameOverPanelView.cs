using DG.Tweening;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelView : EventView
{
    public TextMeshProUGUI gameOverText;
    public Image playerDeathScreen;
    public TextMeshProUGUI highScoreText;
    public Button tryAgainButton;

    protected override void Start()
    {
        playerDeathScreen.DOFade(1, 2).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1.25f, () =>
            {
                gameOverText.gameObject.SetActive(true);
                highScoreText.gameObject.SetActive(true);
                tryAgainButton.gameObject.SetActive(true);
                gameOverText.DOColor(Color.red, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
            });
        });
        
    }

    public void OnRestartGame()
    {
        dispatcher.Dispatch(GameOverPanelEvent.TryAgain);
    }
}
