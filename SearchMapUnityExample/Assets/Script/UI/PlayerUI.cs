using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public Image[] itemImages;

    public GameObject itemGridObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHP(float hp)
    {
        hpText.text = "hp : " + hp;
    }
}
