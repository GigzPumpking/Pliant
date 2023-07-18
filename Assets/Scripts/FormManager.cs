using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // to make use of UI image class and sprite and make use of UI through code.

/// <summary>
/// This form manager script is used to navigate the transformation menu to the characters forms
/// can easily be managed in the future whether forms are added and reduced.
/// 
/// -At start the the previous saved form will be selected otherwise start at the first form
/// 
/// -Next/Prev choic will cycle through the forms saved the database from front to back or vice versa
/// updating the form after called
/// 
/// -UpdateForm will change the selected form for quick transformation calling and future use.
/// </summary>
/// 

public class FormManager : MonoBehaviour
{
    public CharacterForm characterForm; //database of the forms the character will cycle through in the menu
    public SpriteRenderer formSprite; //the actual sprite for the given form
    public Image imageSprite; //the icon for the transformation menu icon
    public Image nextSprite; //the icon for the transformation menu icon of next to select
    public Image prevSprite; //the icon for the transformation menu icon of previous select

    private int selectedForm = 0, nextForm = 0, prevForm = 0;

    // Start is called before the first frame update
    void Start()
    {
        // If the player had a previous selected previous load that form otherwise restart at the first form.
        if (!PlayerPrefs.HasKey("selectedForm"))
        {
            selectedForm = 0;
            prevForm = characterForm.formCount - 1;
            nextForm = selectedForm + 1;
        }
        else
            Load();

        UpdateForm(selectedForm, nextForm, prevForm);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When called will cycle to the next form in the order unless at the end of the form database in which case it loops around to start
    public void NextChoice()
    {
        selectedForm++;

        if (selectedForm >= characterForm.formCount)
            selectedForm = 0;

        prevForm = selectedForm - 1;

        if (prevForm < 0)
            prevForm = characterForm.formCount - 1;

        nextForm = selectedForm + 1;

        if (nextForm >= characterForm.formCount)
            nextForm = 0;

        UpdateForm(selectedForm, nextForm, prevForm);
        Save();
    }

    //When called will cycle to the previous form in the order unless at the first of the form database in which case it loops around to end
    public void PrevChoice()
    {
        selectedForm--;

        if (selectedForm < 0)
            selectedForm = characterForm.formCount - 1;

        prevForm = selectedForm-1;

        if (prevForm < 0)
            prevForm = characterForm.formCount - 1;

        nextForm = selectedForm + 1;

        if (nextForm >= characterForm.formCount)
            nextForm = 0;

        UpdateForm(selectedForm, nextForm, prevForm);
        Save();
    }

    // Update the current form icon and sprite to be used when finished selecting from the menu when called
    private void UpdateForm(int selectedForm, int nextForm, int prevForm)
    {
        Form form = characterForm.GetForm(selectedForm);
        Form nform = characterForm.GetForm(nextForm);
        Form pform = characterForm.GetForm(prevForm);

        formSprite.sprite = form.formSprite;
        imageSprite.sprite = form.imageSprite;
        nextSprite.sprite = nform.imageSprite;
        prevSprite.sprite = pform.imageSprite;
    }

    //Load currently saved form data from when last saved in player's pref when called
    private void Load()
    {
        selectedForm = PlayerPrefs.GetInt("selectedForm");
        nextForm = PlayerPrefs.GetInt("nextForm");
        prevForm = PlayerPrefs.GetInt("prevForm");
    }

    //Save current selected form data when called for loading in future
    private void Save()
    {
        PlayerPrefs.SetInt("selectedForm", selectedForm);
        PlayerPrefs.SetInt("nextForm", nextForm);
        PlayerPrefs.SetInt("prevForm", prevForm);
    }

    public string SelectForm()
    {
        return "";
    }
}
