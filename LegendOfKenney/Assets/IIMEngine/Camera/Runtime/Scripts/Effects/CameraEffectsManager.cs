using System.Collections.Generic;
using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraEffectsManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        public Vector3 PositionDelta { get; set; } = Vector3.zero;
        public float SizeDelta { get; set; } = 0;

        private List<CameraEffect> _activeEffects = new List<CameraEffect>();
        
        #pragma warning restore 0414
        #endregion

        public void AddEffect(CameraEffect effect)
        {
            //TODO: Add effect to active effects
            _activeEffects.Add(effect);
        }

        public void RemoveEffect(CameraEffect effect)
        {
            //TODO: Remove effect to active effects
            _activeEffects.Remove(effect);
        }
        
        private void Awake()
        {
            CameraGlobals.Effects = this;
        }

        public void ManualUpdate(UnityEngine.Camera camera, Transform cameraTransform)
        {
            //Reset Position Delta to Zero
            //Reset Size Delta to Zero
            //Sum all effects together into PositionDelta and SizeDelta
            //Add PositionDelta to camera position
            //Add SizeDelta to camera orthographic size
            
            PositionDelta = Vector3.zero;
            SizeDelta = 0;
            _activeEffects.ForEach(effect => {
                PositionDelta += effect.PositionDelta;
                SizeDelta += effect.SizeDelta;
            });
            cameraTransform.position += PositionDelta;
            camera.orthographicSize += SizeDelta;
        }
    }
}