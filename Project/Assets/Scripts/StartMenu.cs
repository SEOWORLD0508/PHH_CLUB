using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour
{
    public enum ButtonType
    {
        StartGame,
        Options,
        Quit
    }

    public ButtonType buttonType;

    public void OnButtonClick()
    {
        switch (buttonType)
        {
            case ButtonType.StartGame:
                SceneManager.LoadScene("SampleScene");
                break;
            case ButtonType.Options:
                // 대충 코드
                break;
            case ButtonType.Quit:
                Application.Quit();
                break;
        }
    }
}
