using UnityEngine;
[CreateAssetMenu(fileName = "Crime data", menuName = "MAIN/CrimeData")]
public class LevelData : ScriptableObject
{
    public Sprite crimeImg;
    public Sprite suspectImg;
    public CrimeOptions[] crimeOptions;
}

[System.Serializable]
public class CrimeOptions
{
    public string question;
    public Option[] options = new Option[4];
}


[System.Serializable]
public class Option
{
    public string label;
    public float correctPercentage;
}