using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProperties : MonoBehaviour
{
    public enum Sex
    {
        Male,
        Female,
        Total
    }

    public enum HeadHairProperties
    {
        Blonde,
        Brunette,
        Redhead,
        Total
    }

    public enum Races
    {
        Black,
        White,
        Asian,
        Hispanic,
        Total
    }

    public enum FacialHairProperties
    {
        Bearded,
        CleanShaven,
        Moustache,
        Total
    }

    public enum BodyType
    {
        Skinny,
        Average,
        Muscular,
        Obese,
        Total
    }

    public struct Properties
    {
        public Sex sex;
        public HeadHairProperties headHair;
        public Races race;
        public FacialHairProperties facialHair;
        public BodyType body;
    }

    public Properties SetNPC()
    {
        Properties properties = new Properties();

        properties.sex = (Sex)Random.Range(0, (int)Sex.Total);
        properties.headHair = (HeadHairProperties)Random.Range(0, (int)HeadHairProperties.Total);
        properties.race = (Races)Random.Range(0, (int)Races.Total);
        properties.facialHair = (FacialHairProperties)Random.Range(0, (int)FacialHairProperties.Total);
        properties.body = (BodyType)Random.Range(0, (int)BodyType.Total);

        //Debug Messages
        Debug.Log(properties.sex);
        Debug.Log(properties.headHair);
        Debug.Log(properties.race);
        Debug.Log(properties.facialHair);
        Debug.Log(properties.body);

        return properties;
    }
}
