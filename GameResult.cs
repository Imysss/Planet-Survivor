using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public enum InfoType { Level, Kill, Time }
    public InfoType type;

    TextMeshProUGUI myText;
}
