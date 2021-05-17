using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text manaDisplayText;
    [SerializeField] GameObject insufficientManaText;

    private void Start() {
        manaDisplayText.text = "Mana: " + FindObjectOfType<GameManager>().GetCurrentMana().ToString();
    }

    public void UpdateManaDisplayText(int score) {
        manaDisplayText.text = "Mana: " + score;
    }

    public void ShowInsufficientMana() {
        StartCoroutine(BrieflyShowInsufficientMana());
    }

    IEnumerator BrieflyShowInsufficientMana() {
        insufficientManaText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        insufficientManaText.SetActive(false);
    }
}
