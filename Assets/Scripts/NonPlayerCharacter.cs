using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4f;
    public GameObject dialogBox;
    float timerDisplay;

    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = 0;
    }

    void Update()
    {
        #region 对话框显示倒计时
        // 如果显示时间大于0
        if (timerDisplay > 0)
        {
            // 倒计时
            timerDisplay -= Time.deltaTime;
            // 如果倒计时结束
            if (timerDisplay <= 0)
            {
                // 关闭对话框
                dialogBox.SetActive(false);
            }
        }
        #endregion
    }

    /// <summary>
    /// 显示对话框
    /// </summary>
    public void DisplayDialog()
    {
        dialogBox.SetActive(true);
        timerDisplay = displayTime;
    }
}
