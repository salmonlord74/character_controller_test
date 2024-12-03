using UnityEngine;

public class DataChangeInvoker : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("InvokeDataChange", 0f, 1f); // 立即开始，每 3 秒调用一次
    }

    void InvokeDataChange()
    {
        DataManager.Instance.DataChange();
    }

    void OnDestroy()
    {
        // 取消调用，防止内存泄漏
        CancelInvoke("InvokeDataChange");
    }
}
