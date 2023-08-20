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
        #region �Ի�����ʾ����ʱ
        // �����ʾʱ�����0
        if (timerDisplay > 0)
        {
            // ����ʱ
            timerDisplay -= Time.deltaTime;
            // �������ʱ����
            if (timerDisplay <= 0)
            {
                // �رնԻ���
                dialogBox.SetActive(false);
            }
        }
        #endregion
    }

    /// <summary>
    /// ��ʾ�Ի���
    /// </summary>
    public void DisplayDialog()
    {
        dialogBox.SetActive(true);
        timerDisplay = displayTime;
    }
}
