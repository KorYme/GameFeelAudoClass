﻿using System.Collections;
using System.Data.Common;
using IIMEngine.Effects;
using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectRotate : AEffect
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Object to Rotate")]
        [SerializeField] private Transform _objectToRotate;

        public Transform ObjectToRotate => _objectToRotate;

        [Header("Modifiers")]
        [SerializeField] private AEffectModifierFloat _timeModifier;

        [Header("Rotation")]
        [SerializeField] private float _rotationAngle = 10f;
        [SerializeField] private float _rotationPeriod = 0.5f;
        [SerializeField] private float _rotationStopDelay = 0.5f;
        [SerializeField] private AnimationCurve _rotationCurve = AnimationCurve.Constant(0f, 1f, 0f);

        public float RotationAngle => _rotationAngle;
        
        [Header("Looping")]
        [SerializeField] private bool _isLooping = true;

        private Vector3 _eulerAnglesDelta = Vector3.zero;
        private float _timer = 0f;

        #pragma warning restore 0414
        #endregion
        


        protected override void OnEffectReset()
        {
            //Reset Timer
            //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
            //Reset rotation delta Z
            _timer = 0f;
            _objectToRotate.localEulerAngles -= _eulerAnglesDelta; // PROBLEME
            _eulerAnglesDelta.z = 0;
        }

        protected override IEnumerator OnEffectEndCoroutine()
        {
            //TODO: Do not interrupt rotation
            //Wait for rotation stop delay (while loop)
                //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
                //Increment Timer with delta time
                //Calculating percentage between timer and rotationPeriod
                //Applying animation curve on percentage
                //Set eulerAngles delta Z according to percentage and rotationAngle
                //Add rotation delta from objectToRotate localRotation (using eulerAngles)
                //Wait for next frame (with yield instruction)

            while (_timer < _rotationStopDelay)
            {
                _objectToRotate.localEulerAngles -= _eulerAnglesDelta;
                _timer += Time.deltaTime;
                _eulerAnglesDelta = Vector3.forward * _rotationCurve.Evaluate(_timer / _rotationPeriod) * RotationAngle;
                _objectToRotate.localEulerAngles += _eulerAnglesDelta;
                yield return null;
            }
        }
        
        protected override void OnEffectEnd()
        {
            //Reset Timer
            //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
            //Reset rotation delta Z
            _timer = 0f;
            _objectToRotate.localEulerAngles = Vector3.zero;
            _eulerAnglesDelta.z = 0;
        }

        protected override void OnEffectUpdate()
        {
            //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
            //Increment timer with delta time (bonus : Applying factor to deltaTime using timeModifier)
            //If effect is looping, timer must loop between [0, rotationPeriod]
            //Calculating percentage between timer and rotationPeriod
            //Set eulerAngles Z according to percentage and rotationAngle
            //Add rotation delta from objectToRotate localRotation (using eulerAngles)
            _objectToRotate.localEulerAngles -= _eulerAnglesDelta;
            _timer += Time.deltaTime;
            if (_isLooping && _timer >= _rotationPeriod)
            {
                _timer = 0;
            }
            _eulerAnglesDelta = Vector3.forward * _rotationCurve.Evaluate(_timer / _rotationPeriod) * RotationAngle;
            _objectToRotate.localEulerAngles += _eulerAnglesDelta;
        }
    }
}