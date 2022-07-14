using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public enum ToolType
    {
        Hand, // ¸Ç¼Õ
        Axe, // µµ³¢
        PickAxe, // °î±ªÀÌ
        Scythe, // ³´
        Shovel // »ð
    }
    public ToolType toolType;
    [SerializeField] int damage;
}