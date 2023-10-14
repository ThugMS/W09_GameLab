using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    #region PublicVariables
    public bool isTimeStop = false;

    public Volume volume;
    #endregion

    #region PrivateVariables
    private ChromaticAberration m_chromaticAberration;
    private ColorAdjustments m_colorAdjustments;
    #endregion

    #region PublicMethod
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        volume = GameObject.Find("GlobalVolume").gameObject.GetComponent<Volume>();

        volume.profile.TryGet(out m_chromaticAberration);
        volume.profile.TryGet(out m_colorAdjustments);
    }

    private void Update()
    {
        if (isTimeStop)
        {
            isTimeStop = false;
            StartCoroutine(nameof(IE_ReturnTime));
        }
    }
    public void TimeStopEffect()
    {
        isTimeStop = true;

        m_colorAdjustments.postExposure.Override(2f);
        m_chromaticAberration.intensity.Override(1f);

        Time.timeScale = 0f;
    }
    #endregion

    #region PrivateMethod
    private IEnumerator IE_ReturnTime()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
        m_colorAdjustments.postExposure.Override(0f);
        m_chromaticAberration.intensity.Override(0f);
    }
    #endregion
}
