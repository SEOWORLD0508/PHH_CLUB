using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject targetObject; // 나타날 오브젝트
    public GameObject parentObject; // 버튼이 포함된 부모 오브젝트
    public Button myButton; // 버튼
    public Transform playerView; // 플레이어 시점의 위치

    void Start()
    {
        myButton.onClick.AddListener(ToggleObjects);
    }

    void ToggleObjects()
    {
        targetObject.SetActive(true); // 다른 오브젝트 나타내기
        parentObject.SetActive(false); // 버튼이 포함된 오브젝트 숨기기

        // 오브젝트를 플레이어 시점 위치로 이동
        targetObject.transform.position = playerView.position;
    }
}
