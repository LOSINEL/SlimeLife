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
    public ToolType toolType;
    [SerializeField] int damage;
}