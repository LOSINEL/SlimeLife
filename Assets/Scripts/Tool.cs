using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public enum ToolType
    {
        Hand, // �Ǽ�
        Axe, // ����
        PickAxe, // ���
        Scythe, // ��
        Shovel // ��
    }
    public int toolType;
    [SerializeField] int damage;
}