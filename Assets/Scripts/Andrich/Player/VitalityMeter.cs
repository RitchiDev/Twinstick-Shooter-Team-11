using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalityMeter : MonoBehaviour
{
    [SerializeField] private GameObject m_Mask = null;
    private float m_OriginalMeterSize;
    private RectTransform m_RectTransform;

    void Start()
    {
        m_RectTransform = m_Mask.GetComponent<RectTransform>();
        m_OriginalMeterSize = m_RectTransform.sizeDelta.x;
    }

    public void UpdateMeter(float newDividedByOld)
    {
        m_RectTransform.sizeDelta = new Vector2(m_OriginalMeterSize * newDividedByOld, m_RectTransform.sizeDelta.y);
    }
}
