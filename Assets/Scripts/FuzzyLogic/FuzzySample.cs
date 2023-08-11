using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuzzySample : MonoBehaviour
{
    private const string labelText = "{0} true";

    // We use animation curve to create the graphs for each values. Time is considered as health.
    public AnimationCurve critical;
    public AnimationCurve hurt;
    public AnimationCurve healthy;

    //public InputField healthInput;
    public GameObject healthInput;

    /*
    public Text healthyLabel;
    public Text hurtLabel;
    public Text criticalLabel;
    */
    public GameObject healthyLabel;
    public GameObject hurtLabel;
    public GameObject criticalLabel;

    private float criticalValue = 0.0f;
    private float hurtValue = 0.0f;
    private float healthyValue = 0.0f;

    private void SetLabels()
    {
        healthyLabel.GetComponent<TMP_Text>().text = string.Format(labelText, healthyValue);
        hurtLabel.GetComponent<TMP_Text>().text = string.Format(labelText, hurtValue);
        criticalLabel.GetComponent<TMP_Text>().text = string.Format(labelText, criticalValue);
    }

    private void Start()
    {
        SetLabels();
    }

    // Evaluates all the curves & returns float values
    public void EvaluateStatements()
    {
        string inputtext = healthInput.GetComponent<TMP_InputField>().text;
        if (inputtext == null) { return; }

        float inputValue = float.Parse(inputtext);

        healthyValue = healthy.Evaluate(inputValue);
        hurtValue = hurt.Evaluate(inputValue);
        criticalValue = critical.Evaluate(inputValue);

        SetLabels();
    }
}
