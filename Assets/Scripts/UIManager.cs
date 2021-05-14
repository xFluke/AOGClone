using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text manaDisplayText;

    public void UpdateManaDisplayText(int score) {
        manaDisplayText.text = "Mana: " + score;
    }
}
