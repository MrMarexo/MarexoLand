using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    //colors
    [SerializeField] public static Color32 completeColor = new Color32(255, 255, 255, 255);
    [SerializeField] public static Color32 failedColor = new Color32(0, 0, 0, 255);
    [SerializeField] public static Color32 incompleteColor = new Color32(96, 87, 87, 255);
    [SerializeField] public static Color32 invisibleColor = new Color32(0, 0, 0, 0);
    [SerializeField] public static Color32 greyColor = new Color32(144, 144, 144, 255);

    [SerializeField] public static Color32 toggleGrayColor = new Color32(43, 43, 43, 255);

    [SerializeField] public static Color32 semiTransparentColor = new Color32(0, 0, 0, 90);

    [SerializeField] public static Color32 buttonTransparentWhite = new Color32(255, 255, 255, 142);
    [SerializeField] public static Color32 buttonTransparentGrey = new Color32(183, 183, 183, 63);

    [SerializeField] public static Color32 bkgRed = new Color32(70, 54, 54, 255);
    [SerializeField] public static Color32 bkgGrey = new Color32(154, 154, 154, 255);
    [SerializeField] public static Color32 bkgBrightRed = new Color32(115, 60, 60, 255);

    [SerializeField] public static Color32 disabledRedText = new Color32(55, 54, 54, 255);

    //public static Color32 shadowColor = new Color32(0, 0, 0, 90);
}
