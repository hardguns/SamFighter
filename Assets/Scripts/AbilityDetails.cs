using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityDetails : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler { 

    public GameObject detailsPanel;
    public string detailsText;
    public Text detailsTextComp;

    void Start()
    {
         
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse over");
        detailsTextComp.text = detailsText;
        var pos = Input.mousePosition;
        detailsPanel.transform.position = new Vector3(Input.mousePosition.x + 430, Input.mousePosition.y + 20, Input.mousePosition.z);
        detailsPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detailsPanel.SetActive(false);
    }
}
