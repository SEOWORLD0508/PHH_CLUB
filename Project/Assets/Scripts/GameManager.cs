using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Pause")]
    public bool Pause;
    public KeyCode PauseKey = KeyCode.Escape;
    [Header("Inventory")]
    public bool InventoryBool;

    [Header("Shop")]
    public bool ShopOnOff;

    public Transform InventoryBase;
    public Transform DescriptionBase;
    public TMP_Text NameText;
    public TMP_Text DescriptionText;
    public Image ItemSprite;
    public KeyCode InventoryKey = KeyCode.Tab;
    private static GameManager _instance;

    [Header("Gold")]
    public float Gold;
    public float goldDegree = 1f;
    public List<GameObject> ItemToSellList = new List<GameObject>();

    [Header("ReBirth")]
    public bool rebirth = false;
    public float GetGold()
    { 
        return Gold;
    }
    public void UpdateGoldUI()
    {
        // 돈 관련 UI 갱신 로직 추가
        Debug.Log($"Current Gold: {GameManager.Instance.Gold}");
    }
    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        if (Input.GetKeyDown(PauseKey))
        {
            Pause = !Pause;
            Time.timeScale  = Pause?0:1;
        }

        if (Input.GetKeyDown(InventoryKey))
        {
            InventoryBool = !InventoryBool;
        }
    }
    
    public void MoveScene(int _num){
        SceneManager.LoadScene(_num);

    }
    public void GameOver()
    {
        if (rebirth)
        {
            rebirth = false;
            FindObjectOfType<PlayerStatus>().health = 3;
            FindObjectOfType<PlayerStatus>().immune = true;
            FindObjectOfType<PlayerStatus>().immune = false;
        }
        else
        {
            print("GameOver");
            // 게임 오버 구현 해야함
        }

    }

}
