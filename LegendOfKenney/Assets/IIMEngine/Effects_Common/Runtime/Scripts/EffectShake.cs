using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectShake : AEffect
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        [SerializeField] private Transform _objectToShake;
        public Transform ObjectToShake => _objectToShake;

        [SerializeField] private float _shakePowerX = 0.1f;
        [SerializeField] private float _shakePowerY = 0.1f;
        [SerializeField] private float _shakePeriod = 0.1f;

        private Vector3 _positionDelta = Vector3.zero;

        private float _timer = 0f;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnEffectStart()
        {
            //Reset Timer
            //Remove position delta from objectToShake localPosition
            //Reset position delta X/Y
            _timer = 0f;
            ObjectToShake.localPosition -= _positionDelta;
            _positionDelta = Vector3.zero;
        }

        protected override void OnEffectUpdate()
        {
            //Remove position delta from objectToShake localPosition
            //Increment timer with delta time
            //Calculating percentage between timer and shakePeriod (using Mathf.PingPong)
            //Set positionDelta X/Y according to percentage and shakePowerX/shakePowerY
            //Add position Delta to objectToShake localPosition
            ObjectToShake.localPosition -= _positionDelta;
            _timer += Time.deltaTime;
            float percent = (Mathf.PingPong(_timer / _shakePeriod, _shakePeriod) * 2) - 1;
            _positionDelta = new Vector3(percent * _shakePowerX, percent * _shakePowerY, 0);
            ObjectToShake.localPosition += _positionDelta;
        }

        protected override void OnEffectEnd()
        {
            //Reset Timer
            //Remove position delta from objectToShake localPosition
            //Reset position delta X/Y
            _timer = 0f;
            ObjectToShake.localPosition -= _positionDelta;
            _positionDelta = Vector3.zero;
        }
    }
}