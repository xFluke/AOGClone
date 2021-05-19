using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationUI : MonoBehaviour
{
    [SerializeField] GameObject unitInformationCanvas;
    [SerializeField] Text unitNameText;
    [SerializeField] Text hpText;

    Quaternion desiredRotation;
    private void Start() {
        desiredRotation = unitInformationCanvas.transform.rotation;
        Debug.Log("Information Canvas: " + unitInformationCanvas.transform.rotation);  
    }

    public void Initialize(UnitNames unitName) { 
        unitNameText.text = unitName.ToString();
    }

    public void ShowUnitInformation() {
        unitInformationCanvas.SetActive(true);

        unitInformationCanvas.transform.rotation = desiredRotation;
    }

    public void HideUnitInformation() {
        unitInformationCanvas.SetActive(false);
    }
    
}
