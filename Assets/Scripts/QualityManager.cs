using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityManager : MonoBehaviour
{
    public void SetQuality (int qualityIndex) {
        UnityEngine.QualitySettings.SetQualityLevel(qualityIndex);
    }
}
