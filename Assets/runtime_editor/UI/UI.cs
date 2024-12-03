using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayNumber : MonoBehaviour
{
    public TextMeshProUGUI numberText;

    void Start()
    {
        if (DataManager.Instance != null && DataManager.Instance.playerData != null)
        {
            // 订阅数据变化事件
            DataManager.Instance.DataChanged += UpdateUI;

            // 初始化UI
            UpdateUI();
        }
        else
        {
            Debug.LogError("Player 或 PlayerData 没有被分配！");
        }
    }

    void Update()
    {
        if (DataManager.Instance != null && DataManager.Instance.playerData != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
                DataManager.Instance.IncrementCoin(1);

            if (Input.GetKeyDown(KeyCode.T))
                DataManager.Instance.SetCardCrazy(true);

            if (Input.GetKeyDown(KeyCode.F))
                DataManager.Instance.SetCardCrazy(false);
        }
    }

    void UpdateUI()
    {
        numberText.text = "Coin: " + DataManager.Instance.playerData.coin.ToString();
        // 如果需要，可以更新其他UI元素
        //Debug.Log($"UI Updated: Coin = {DataManager.Instance.playerData.coin}");
    }

    void OnDestroy()
    {
        if (DataManager.Instance != null && DataManager.Instance.playerData != null)
        {
            // 取消订阅事件，防止内存泄漏
            DataManager.Instance.DataChanged -= UpdateUI;
        }
    }



}

