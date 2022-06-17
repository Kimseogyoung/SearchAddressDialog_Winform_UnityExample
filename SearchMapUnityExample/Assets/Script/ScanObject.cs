using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanObject : MonoBehaviour
{
    public string objName;
    public int objID;
    Renderer renderers;
    List<Material> materialList = new List<Material>();
    Material outline;

    public bool isOutline = false;
    public void OnOutLine()
    {
        if (isOutline == true) return;
        isOutline = true;
        if (outline == null)
            outline = new Material(Shader.Find("Custom/Outline"));
        renderers = this.GetComponent<Renderer>();

        materialList.Clear();

            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outline);
        

        renderers.materials = materialList.ToArray();
    }
    public void Run()
    {   
        Comopent();
    }
    public void Clear()
    {
        if (isOutline == false) return;
        isOutline = false;
        Renderer renderer = this.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);
        materialList.Remove(outline);

        renderer.materials = materialList.ToArray();
    }
    virtual public void Comopent()
    {

    }
}
