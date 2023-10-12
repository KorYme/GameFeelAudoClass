using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Camera.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Camera/Camera Effect Shake")]
    public class MMF_Camera_EffectShake : MMF_Feedback
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [MMFInspectorGroup("Shake", true)]
        [SerializeField] private float _shakeDuration = 1.0f;
        [SerializeField] private float _shakePower = 0.1f;
        [SerializeField] private float _shakePeriod = 0.05f;

        private CameraEffect _cameraEffect = new CameraEffect();

#pragma warning restore 0414
        #endregion

        //TODO: Override FeedbackDuration Property (using _shakeDuration)
        public override float FeedbackDuration { get => _shakeDuration; set => _shakeDuration = value; }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            Owner.StartCoroutine(_CoroutineShake());
        }

        private IEnumerator _CoroutineShake()
        {
            //Add _cameraEffect into CameraEffects
            //Implements shake effects using _shakeDuration / _shakePeriod / _shakePower
            CameraGlobals.Effects.AddEffect(_cameraEffect);
            float timer = 0f;
            while (timer < _shakeDuration)
            {
                timer += Time.deltaTime;

                float percent = Mathf.PingPong(timer, _shakePeriod) / _shakePeriod;
                _cameraEffect.PositionDelta = new Vector3(1, 1, 0) * percent * _shakePower;
                yield return null;
            }
        }
    }
}