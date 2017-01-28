using UnityEngine;
using System.Collections;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.AddOn.Fx;


public static class GlobalData {

    public static float[] samples {get;set;}
    public static float[] processedSamples { get; set; }
    public static TAG_INFO tagInfo { get; set; }
    public static BASS_SAMPLE sinfo { get; set; }

}
