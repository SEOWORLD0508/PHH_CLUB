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
                print("d");
                break;
            case ButtonType.Options:
                // 대충 코드
                Debug.Log("Options Button Clicked");
                break;
            case ButtonType.Quit:
                Application.Quit();
                break;
        }
    }
}
