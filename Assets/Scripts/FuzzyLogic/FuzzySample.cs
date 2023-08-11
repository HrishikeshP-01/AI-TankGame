using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuzzySample : MonoBehaviour
{
    private const string labelText = "{0} true";

    // We use animation curve to create the graphs for each values. Time is considered as health.
    public AnimationCurve critical;
    public AnimationCurve hurt;
    public AnimationCurve healthy;

    public InputField healthInput;

    public Text healthyLabel;
    public Text hurtLabel;
    public Text criticalLabel;

    private float criticalValue = 0.0f;
    private float hurtValue = 0.0f;
    private float healthyValue = 0.0f;

    private void SetLabels()
    {
        healthyLabel.text = string.Format(labelText, healthyValue);
        hurtLabel.text = string.Format(labelText, hurtValue);
        criticalLabel.text = string.Format(labelText, criticalValue);
    }

    private void Start()
    {
        SetLabels();
    }

    // Evaluates all the curves & returns float values
    public void EvaluateStatements()
    {
        if (string.IsNullOrEmpty(healthInput.text)) { return; }

        float inputValue = float.Parse(healthInput.text);

        healthyValue = healthy.Evaluate(inputValue);
        hurtValue = hurt.Evaluate(inputValue);
        criticalValue = critical.Evaluate(inputValue);

        SetLabels();
    }
}
