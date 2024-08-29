using UnityEngine;
using UnityEngine.SceneManagement;

public class Btntype : MonoBehaviour
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
                SceneManager.LoadScene("SampleScene");  // StartGame 버튼이 눌리면 게임 씬을 로드
                break;
            case ButtonType.Options:
                // Options 메뉴를 실행하는 코드를 여기에 추가 (예: 옵션 창을 활성화)
                Debug.Log("Options Button Clicked");
                break;
            case ButtonType.Quit:
                Application.Quit();  // Quit 버튼이 눌리면 게임 종료
                break;
        }
    }
}
