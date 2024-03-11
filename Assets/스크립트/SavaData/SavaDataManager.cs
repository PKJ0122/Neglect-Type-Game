using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavaDataManager : MonoBehaviour
{
    [SerializeField] PlayerSavaData sD;

    public PlayerSavaData SD { get => sD; set => sD = value; }
}
