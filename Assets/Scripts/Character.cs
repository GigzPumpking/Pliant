using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
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

    private void UpdateForm(int selectedForm)
    {
        Form form = characterForm.GetForm(selectedForm);
        formSprite.sprite = form.formSprite;
    }

    private void Load() => selectedForm = PlayerPrefs.GetInt("selectedForm");
}
