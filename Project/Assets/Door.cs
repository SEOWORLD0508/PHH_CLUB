using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Door : MonoBehaviour
{
    [SerializeField]
    TMP_Text t;
    [SerializeField]
    float doorDis;
    PlayerMovement p;
    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyController>() == null)
        {
            t.gameObject.SetActive(Vector3.Distance(transform.position, p.transform.position) < doorDis);
            t.text = GameManager.Instance.inRoom ? "<color=yellow>F</color>�� ���� �� ����" : "<color=yellow>F</color>�� ���� �� ����";
        }
        else t.gameObject.SetActive(false);
    }
}
