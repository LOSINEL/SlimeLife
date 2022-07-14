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
    public Mesh[] toolMesh;
    public Material[] toolMaterial;
    public ToolType toolType;
    public void ToolChanged(Item _item, ToolType _toolType)
    {
        switch (_toolType)
        {
            case ToolType.Hand:
            case ToolType.Axe:
            case ToolType.Scythe:
            case ToolType.Shovel:
                gameObject.GetComponent<MeshFilter>().mesh = toolMesh[(int)_toolType];
                gameObject.GetComponent<MeshCollider>().sharedMesh = toolMesh[(int)_toolType];
                gameObject.GetComponent<MeshRenderer>().material = toolMaterial[0];
                break;
            case ToolType.PickAxe:
                gameObject.GetComponent<MeshFilter>().mesh = toolMesh[(int)_toolType];
                gameObject.GetComponent<MeshCollider>().sharedMesh = toolMesh[(int)_toolType];
                gameObject.GetComponent<MeshRenderer>().material = toolMaterial[1];
                break;
        }
    }
}