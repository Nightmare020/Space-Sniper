using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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

    public struct Properties
    {
        public Sex sex;
        public HeadHairProperties headHair;
        public Races race;
        public FacialHairProperties facialHair;
    }

    public enum Order
    {
        Sex,
        Race,
        HeadHair,
        FacialHair
    }

    public static Properties SetNPC(int debug = -1, TextMeshProUGUI debugTxt = null)
    {
        Properties properties = new()
        {
            sex = (Sex)Random.Range(0, (int)Sex.Total),
            headHair = (HeadHairProperties)Random.Range(0, (int)HeadHairProperties.Total),
            race = (Races)Random.Range(0, (int)Races.Total),
        };
        if (properties.sex != Sex.Female)
            properties.facialHair = (FacialHairProperties)Random.Range(0, (int)FacialHairProperties.Total);
        else
            properties.facialHair = FacialHairProperties.CleanShaven;
        

        //Debug
        if(debug != -1 && debugTxt != null)
        {
            //Debug Messages
            debugTxt.text += "\n" + properties.sex.ToString() + "\n" +
                properties.race.ToString() + "\n" +
                properties.headHair.ToString() + "\n" +
                properties.facialHair.ToString();
        }

        return properties;
    }

    public int CompareProperties(TextMeshProUGUI txt, Properties targetProperty, Properties property, int curProperty, bool print = false)
    {
        //Print Logic
        if (print)
        {
            if (curProperty == -1)
            {
                txt.text = string.Empty;
                if (property.sex != Sex.Female)
                {
                    txt.text = "You shot a " +
                    property.sex.ToString()
                    + " who is " +
                    property.race.ToString()
                    + " has " +
                    property.headHair.ToString() + " hair"
                    + ", and" +
                    property.facialHair.ToString();
                }
                else
                {
                    txt.text = "You shot a " +
                    property.sex.ToString()
                    + " who is " +
                    property.race.ToString()
                    + ", and has " +
                    property.headHair.ToString();
                }
            }
            else
            {
                txt.text = string.Empty;
                switch (curProperty)
                {
                    case 1:
                        txt.text = property.sex.ToString();
                        break;

                    case 2:
                        txt.text = property.race.ToString();
                        break;

                    case 3:
                        txt.text = property.headHair.ToString();
                        break;

                    case 4:
                        if (property.sex != Sex.Female)
                        {
                            txt.text = property.facialHair.ToString();
                        }
                        curProperty++;
                        GameManager.instance.IncProperty();
                        CompareProperties(txt, property, targetProperty, curProperty);
                        break;

                }
            }
        }

        //Compare Logic
        if (curProperty == 1 && 
            (targetProperty.sex == property.sex && 
            targetProperty.race == property.race && 
            targetProperty.headHair == property.headHair && 
            targetProperty.facialHair == property.facialHair))
        {
            txt.text = "You Win! (Very lucky)";
            return -1;
        }

        switch (curProperty)
        {
            case 1:
                if (targetProperty.sex == property.sex)
                {
                    txt.text = "Sex Matches";
                    curProperty++;
                    GameManager.instance.IncProperty();
                    return CompareProperties(txt, targetProperty, property, curProperty);
                }
                else
                {
                    return 0;
                }

            case 2:
                if (targetProperty.race == property.race)
                {
                    txt.text += ", race Matches";
                    curProperty++;
                    GameManager.instance.IncProperty();
                    return CompareProperties(txt, targetProperty, property, curProperty);
                }
                else
                {
                    return 0;
                }

            case 3:
                if (targetProperty.headHair == property.headHair)
                {
                    txt.text += ", headHair Matches";
                    curProperty++;
                    GameManager.instance.IncProperty();
                    return CompareProperties(txt, targetProperty, property, curProperty);
                }
                else
                {
                    return 0;
                }

            case 4:
                if (property.sex == targetProperty.sex && targetProperty.sex != Sex.Female)
                {
                    if (targetProperty.facialHair == property.facialHair)
                    {
                        txt.text += ", facialHair Matches";
                        curProperty++;
                        GameManager.instance.IncProperty();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    curProperty++;
                    GameManager.instance.IncProperty();
                    return 1;
                }
        }

        return 0;
    }
}
