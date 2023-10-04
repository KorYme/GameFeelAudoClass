using System.Collections;
using IIMEngine.Effects;
using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectBounce : AEffect
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Object to Scale")]
        [SerializeField] private Transform _objectToScale;

        [Header("Modifiers")]
        [SerializeField] private AEffectModifierFloat _timeModifier;
        
        public Transform ObjectToScale => _objectToScale;

        [Header("Bounce")]
        [SerializeField] private float _bounceFactorX = 0.1f;
        [SerializeField] private float _bounceFactorY = 0.1f;
        [SerializeField] private float _bouncePeriod = 0.5f;
        [SerializeField] private AnimationCurve _bounceCurveX = AnimationCurve.Constant(0f, 1f, 0f);
        [SerializeField] private AnimationCurve _bounceCurveY = AnimationCurve.Constant(0f, 1f, 0f);

        public float BounceFactorX => _bounceFactorX;
        public float BounceFactorY => _bounceFactorY;
        
        [Header("Looping")]
        [SerializeField] private bool _isLooping = true;

        private Vector3 _scaleDelta = Vector3.zero;
        private float _timer = 0f;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnEffectReset()
        {
            //Reset Timer
            //Remove scale delta from objectToScale localScale
            //Reset scale delta X/Y
            _timer = 0f;
            ObjectToScale.localScale -= _scaleDelta;
            _scaleDelta = Vector3.zero;
        }

        protected override IEnumerator OnEffectEndCoroutine()
        {
            //TODO: Do not interrupt bouncing effect
            //Wait for bounce period
            //Remove scale delta from objectToScale localScale
            //Increment Timer with delta time
            //Calculating percentage between timer and bouncePeriod
            //Applying animation curve on percentage
            //Set scale delta X/Y according to percentage and bounceFactorX/bounceFactorY
            //Add scale delta from objectToScale localScale
            //Wait for next frame (with yield instruction)
            while (_timer < _bouncePeriod)
            {
                ObjectToScale.localScale -= _scaleDelta;
                _timer += Time.deltaTime;
                _scaleDelta = new Vector3(_bounceCurveX.Evaluate(_timer / _bouncePeriod) * _bounceFactorX, _bounceCurveY.Evaluate(_timer / _bouncePeriod) * _bounceFactorY, 1f);
                ObjectToScale.localScale += _scaleDelta;
                yield return null;
            }
        }

        protected override void OnEffectEnd()
        {
            //Reset Timer
            //Remove scale delta from objectToScale localScale
            //Reset scale delta X/Y
            _timer = 0f;
            ObjectToScale.localScale -= _scaleDelta;
            _scaleDelta = Vector3.zero;
        }
        
        protected override void OnEffectUpdate()
        {
            //Remove scale delta from objectToScale localScale
            //Increment timer with delta time (bonus : Applying factor to deltaTime using timeModifier)
            //If effect is looping, timer must loop between [0, bouncePeriod]
            //Calculating percentage between timer and bouncePeriod
            //Set scale delta X/Y according to percentage and bounceFactorX/bounceFactorY
            //Add scale delta from objectToScale localScale
            ObjectToScale.localScale -= _scaleDelta;
            _timer += Time.deltaTime;
            if (_isLooping && _timer > _bouncePeriod)
            {
                _timer = 0;
            }
            _scaleDelta = new Vector3(_bounceCurveX.Evaluate(_timer / _bouncePeriod) * _bounceFactorX, _bounceCurveY.Evaluate(_timer / _bouncePeriod) * _bounceFactorY, 1f);
            ObjectToScale.localScale += _scaleDelta;
        }
    }
}