using strange.extensions.mediation.impl;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MainMenuView : EventView
{
    private Button PlayButton;
    private Button ExitButton;

    public void OnPlayButtonClick(int x, string s)
    {
        dispatcher.Dispatch(MainMenuEvent.Play,x);
    }

    public void OnExitButtonClick()
    {
        dispatcher.Dispatch(MainMenuEvent.Exit);
    }
}
