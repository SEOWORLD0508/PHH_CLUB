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
        targetObject.SetActive(true); // �ٸ� ������Ʈ ��Ÿ����
        parentObject.SetActive(false); // ��ư�� ���Ե� ������Ʈ �����

        // ������Ʈ�� �÷��̾� ���� ��ġ�� �̵�
        targetObject.transform.position = playerView.position;
    }
}
