using UnityEngine;
using System.Collections;
using System;

namespace USnd
{


    [CreateAssetMenu(menuName = "USnd/ Create Audio3DSettings Instance")]
    public class Audio3DSettings : ScriptableObject, ICloneable
    {

        public string spatialName = "";

        [Range(0, 1)]
        public float spatialBlend = 1;

        [Range(0, 1.1f)]
        public float reverbZoneMix = 1;

        [Range(0, 5)]
        public float dopplerLevel = 1;

        [Range(0, 360)]
        public int spread = 0;

        public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

        public float minDistance = 1;

        public float maxDistance = 500;

        //Adia
        //Adia
        public AnimationCurve customRolloffCurve = AnimationCurve.Linear(0, 1, 1, 0);

        public AnimationCurve spatialBlendCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 0));

        public AnimationCurve reverbZoneMixCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));

        public AnimationCurve spreadCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 0));


        public object Clone()
        {
            Audio3DSettings copy = Audio3DSettings.CreateInstance<Audio3DSettings>();

            copy.spatialName = (string)this.spatialName.Clone();
            copy.spatialBlend = this.spatialBlend;
            copy.reverbZoneMix = this.reverbZoneMix;
            copy.dopplerLevel = this.dopplerLevel;
            copy.spread = this.spread;
            copy.rolloffMode = this.rolloffMode;
            copy.minDistance = this.minDistance;
            copy.maxDistance = this.maxDistance;
            copy.customRolloffCurve = new AnimationCurve(this.customRolloffCurve.keys);
            copy.spatialBlendCurve = new AnimationCurve(this.spatialBlendCurve.keys);
            copy.reverbZoneMixCurve = new AnimationCurve(this.reverbZoneMixCurve.keys);
            copy.spreadCurve = new AnimationCurve(this.spreadCurve.keys);

            return copy;
        }

        public void Copy(Audio3DSettings newParam)
        {
            this.spatialName = (string)newParam.spatialName.Clone();
            this.spatialBlend = newParam.spatialBlend;
            this.reverbZoneMix = newParam.reverbZoneMix;
            this.dopplerLevel = newParam.dopplerLevel;
            this.spread = newParam.spread;
            this.rolloffMode = newParam.rolloffMode;
            this.minDistance = newParam.minDistance;
            this.maxDistance = newParam.maxDistance;
            this.customRolloffCurve = new AnimationCurve(newParam.customRolloffCurve.keys);
            this.spatialBlendCurve = new AnimationCurve(newParam.spatialBlendCurve.keys);
            this.reverbZoneMixCurve = new AnimationCurve(newParam.reverbZoneMixCurve.keys);
            this.spreadCurve = new AnimationCurve(newParam.spreadCurve.keys);
        }
    }

}
