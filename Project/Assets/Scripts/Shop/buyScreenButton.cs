using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject targetObject; // ��Ÿ�� ������Ʈ
    public GameObject parentObject; // ��ư�� ���Ե� �θ� ������Ʈ
    public Button myButton; // ��ư
    public Transform playerView; // �÷��̾� ������ ��ġ



    void Start()
    {
        myButton.onClick.AddListener(ToggleObjects);
    }
    
    

    void ToggleObjects()
    {
        if (GameManager.Instance.Pause == true) return;
        targetObject.SetActive(true); // �ٸ� ������Ʈ ��Ÿ����
        parentObject.SetActive(false); // ��ư�� ���Ե� ������Ʈ �����
        GameManager.Instance.tabOn = true;

        // ������Ʈ�� �÷��̾� ���� ��ġ�� �̵�
        targetObject.transform.position = playerView.position;
    }
}
