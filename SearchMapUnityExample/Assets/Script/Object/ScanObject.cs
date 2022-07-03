using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanObject : MonoBehaviour
{
    public enum Type
    {
        AddressSearcher, EnemySpawner, TreasureBox, Store
    }
    public string objName;
    public int objID;
    Renderer renderers;
    List<Material> materialList = new List<Material>();
    Material outline;

    public bool isOutline = false;
    protected Board board;
    protected Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezePositionX| RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezeRotation;

    }
    public void SetBoard(Board board)
    {
        this.board = board;
    }
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
        Component();
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
    virtual public void Component()
    {

    }
}
