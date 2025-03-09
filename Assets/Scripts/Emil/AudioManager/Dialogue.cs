using UnityEngine;

public enum ClientName {
    Client1,
    Client2,
    Client3,
    Client4,
    Client5
}

public enum DialogueType {
    MasterInsultMale,
    MasterInsultFemale,
    CorrectShot,
    MasterCorrect,
    FirstTargetClue,
    SecondTargetClue,
    ThirdTargetClue,
    FourthTargetClue,
    None
}

public enum DescriptionHelper
{
    Zero,
    One,
    Two,
    Three,
    SexClue,
    RaceClue,
    HeadHairClue,
    FacialHairClue
}

[System.Serializable]
public class Dialogue {
    [Header("Client")]
    public ClientName client;

    [Header("Clips")]
    public Sound masterInsult;
    public Sound followingMaleInsult;
    public Sound followingFemaleInsult;
    public Sound correctShot;
    public Sound masterShot;
    public Sound firstTargetClue;
    public Sound secondTargetClue;
    public Sound thirdTargetClue;
    public Sound fourthTargetClue;
}