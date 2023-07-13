using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    public CharacterForm characterForm;
    public SpriteRenderer formSprite;
    public Image imageSprite;

    private int selectedForm = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedForm"))
            selectedForm = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextChoice()
    {
        selectedForm++;

        if (selectedForm >= characterForm.formCount)
            selectedForm = 0;

        UpdateForm(selectedForm);
        Save();
    }

    public void PrevChoice()
    {
        selectedForm--;

        if (selectedForm < 0)
            selectedForm = characterForm.formCount - 1;

        UpdateForm(selectedForm);
        Save();
    }

    private void UpdateForm(int selectedForm)
    {
        Form form = characterForm.GetForm(selectedForm);
        formSprite.sprite = form.formSprite;
        imageSprite.sprite = form.formSprite;
    }

    private void Load() => selectedForm = PlayerPrefs.GetInt("selectedForm");

    private void Save() => PlayerPrefs.SetInt("selectedForm", selectedForm);
}
