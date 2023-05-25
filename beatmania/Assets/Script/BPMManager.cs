using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMManager : MonoBehaviour
{
    [SerializeField] private List<float> fl_bpm;
    [SerializeField] private int n_bpmlist = 0;
    [SerializeField] private float f_bpm_correction_value = 600;

    private void Start()
    {
        n_bpmlist = 0;
    }

    public float NowBPM()
    {
        return fl_bpm[n_bpmlist] / f_bpm_correction_value;
    }

    public void SetBPM(float _bpm)
    {
        fl_bpm.Add(_bpm);
    }
}
