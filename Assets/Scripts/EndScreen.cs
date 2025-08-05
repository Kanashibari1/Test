using System;

public class EndScreen : Window
{
    public event Action RestartButtonClicked;

    public override void Close()
    {
        WindowGroup.alpha = 0f;
        Button.interactable = false;
    }

    public override void Open()
    {
        WindowGroup.alpha = 1f;
        Button.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
