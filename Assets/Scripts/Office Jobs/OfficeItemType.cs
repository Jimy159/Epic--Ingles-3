using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeItemType : MonoBehaviour
{
    public enum Type
    {
        Doctor,
        Firefighter,
        PoliceOfficer,
        Teacher,
        Vet,
        Farmer,
        Chef,
        Pilot,
        Builder,
        MailCarrier,
        Waiter,
        Mechanic,
        Artist,
        Singer,
        Musician,
        Dancer,
        Actor,
        Photographer,
        Astronaut,
        Scientist,
        Gardener,
        Dentist,
        Explorer,
        Judge
    }
    public Type type;
    public SpatialInteractable interactable;
}
