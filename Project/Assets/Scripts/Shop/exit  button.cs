using UnityEngine;
using UnityEngine.UI;

public class NewButtonController : MonoBehaviour
{
    public GameObject targetObject; // ��Ÿ�� ������Ʈ
    public GameObject parentObject; // ��ư�� ���Ե� �θ� ������Ʈ
    public Button myButton; // ��ư
    public Transform playerView; // �÷��̾� ������ ��ġ

    void Awake()
    {
        // �ʱ�ȭ �ڵ�
        myButton.onClick.AddListener(SwitchObjects);
    }

    void SwitchObjects()
    {
        parentObject.SetActive(false); // ��ư�� ���Ե� ������Ʈ �����
        targetObject.SetActive(true); // �ٸ� ������Ʈ ��Ÿ����

        // ������Ʈ�� �÷��̾� ���� ��ġ�� �̵�
        targetObject.transform.position = playerView.position;
    }
}
