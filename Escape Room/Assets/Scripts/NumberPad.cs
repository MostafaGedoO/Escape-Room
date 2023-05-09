using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberPad : MonoBehaviour
{
    [SerializeField] private GameObject keyCard;
    [SerializeField] private TextMeshProUGUI passwordText;
    [SerializeField] private string correctNumber = "5070";
    
    public static NumberPad Instance;

    private bool done;
    private string password;
    private int count;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetNumber(string number)
    {
        if (!done)
        {
            StopAllCoroutines();
            
            if (count == 0)
            {
                passwordText.color = Color.black;
                passwordText.text = "";
            }
            
            passwordText.text += "*"; 
            count++; 
            password += number;
            
            if (count == 4)
            {
                if (string.Equals(password, correctNumber))
                {
                    keyCard.SetActive(true);
                    passwordText.text = "Correct";
                    passwordText.color = Color.green;
                    done = true;
                }
                else
                {
                    count = 0;
                    password = "";
                    StartCoroutine(RestPassword());
                }
            }
        }
    }

    IEnumerator RestPassword()
    {
        passwordText.color = Color.red;
        passwordText.text = "Wrong Password";
        yield return new WaitForSeconds(1);
        passwordText.color = Color.black;
        passwordText.text = "";
    }
}
