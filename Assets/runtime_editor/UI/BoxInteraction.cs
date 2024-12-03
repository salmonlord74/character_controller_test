// BoxInteraction.cs
using UnityEngine;
using TMPro;

public class BoxInteraction : MonoBehaviour
{
    public float interactDistance = 3f; // 交互距离
    public LayerMask boxLayer; // 设置箱子所在的 Layer
    public TextMeshProUGUI interactText; // UI 文本，拖拽到 Inspector 中

    private GameObject currentBox = null; // 当前目标箱子

    void Update()
    {
        RaycastHit hit;
        // 从摄像机的位置向前发射射线
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, boxLayer))
        {
            // 如果射线命中并且命中的是箱子
            if (hit.collider.CompareTag("Box"))
            {
                currentBox = hit.collider.gameObject;
                // 显示交互提示
                if (interactText != null)
                {
                    interactText.text = "Press E to open the box";
                    interactText.gameObject.SetActive(true);
                }

                // 检测是否按下 E 键
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenBox(currentBox);
                }
                return; // 如果有命中，结束 Update
            }
        }

        // 如果没有命中箱子，隐藏交互提示
        currentBox = null;
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void OpenBox(GameObject box)
    {
        // 在这里添加开启箱子的逻辑，例如播放动画、显示内容等
        Debug.Log("开启箱子: " + box.name);

        // 更新成就进度
        AchievementManager.Instance.UpdateAchievementProgress("open_box", 1);

        // 隐藏交互提示
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }

        // 其他逻辑，例如销毁箱子、给予奖励等
        //Destroy(box);
    }
}
