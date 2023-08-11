/*
 * Answer doesn't inherit from Monobehavior & will not be serialized by default.
 * To let Unity know that it is serializable we add [System.Serializable]
 */

[System.Serializable]
public class Answer
{
    public string answerText;
    public float moralityValue;
}