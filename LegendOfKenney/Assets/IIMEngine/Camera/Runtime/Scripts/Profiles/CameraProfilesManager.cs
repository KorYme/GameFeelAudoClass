using System.Collections;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraProfilesManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        private CameraProfile _defaultProfile = null;
        private CameraProfile _currentProfile = null;

        private Vector3 _position = Vector3.zero;
        private Vector3 _destination = Vector3.zero;
        private Quaternion _rotation = Quaternion.identity;
        private float _size = 0f;

        [Header("POIs")]
        [SerializeField] private int _maxPOIs = 10;
        [SerializeField] private float _POIDestinationDistanceThreshold = 0.3f;
        [SerializeField] private float _POIDestinationCompensateThreshold = 0.01f;
        private CameraPOI[] _cacheCameraPOIs;
        private POIMovementsState _POIMovementsState = POIMovementsState.Snap;
        
        private enum POIMovementsState
        {
            Snap = 0,
            Compensate
        } 

        public bool IsTransitionActive { get; private set; }

        private Coroutine _currentCoroutine = null;

        private UnityEngine.Camera _camera = null;

        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            CameraGlobals.Profiles = this;
            _cacheCameraPOIs = new CameraPOI[_maxPOIs];
        }

        public void Init(UnityEngine.Camera camera, Transform cameraTransform)
        {
            _camera = camera;
            _position = cameraTransform.position;
            _rotation = cameraTransform.rotation;
            _size = camera.orthographicSize;
        }

        public void ManualUpdate(UnityEngine.Camera camera, Transform cameraTransform)
        {
            // /!\ Must be implemented into CameraFollow QRCode
            // --------------------------------------------------------
            //If Profile follow a group and transition not active
                //If Profile use POIs (Points of interest)
                    //Calculate Centroid with POIs Destination
                    //If Destination is too far (> POIDestinationDistanceThreshold)
                        //Compensate Destination with CameraProfile.FollowPOIsDamping
                    //Else
                        //Snap Destination to Centroid
                    //Tips : you can use enum POIMovementsState for compensation
                //Else
                    //Calculate Centroid from follow groups.
                
                //Optional : Clamp Destination with Camera Bounds
                //Lerp position with destination using CameraProfile.FollowLerpSpeed
            // --------------------------------------------------------
            //       LA
            cameraTransform.position = new Vector3(_position.x, _position.y, cameraTransform.position.z);
            cameraTransform.rotation = _rotation;
            camera.orthographicSize = _size;
        }

        public IEnumerator SetProfileAndWaitForTransition(CameraProfile profile, CameraProfileTransition transition = null)
        {
            SetProfile(profile, transition);
            while (IsTransitionActive) {
                yield return null;
            }
        }

        public void SetProfile(CameraProfile profile, CameraProfileTransition transition = null)
        {
            _currentProfile = profile;
            if (profile.ProfileType == CameraProfileType.Default) {
                _defaultProfile = profile;
            }

            if (null != _currentCoroutine) {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(_CoroutineChangeProfile(_currentProfile, transition));
        }

        public void ResetToDefaultProfile(CameraProfileTransition transition = null)
        {
            if (_defaultProfile == null) return;
            if (_currentProfile.ProfileType == CameraProfileType.Default) return;

            _currentProfile = _defaultProfile;

            if (null != _currentCoroutine) {
                StopCoroutine(_currentCoroutine);
            }

            StartCoroutine(_CoroutineChangeProfile(_currentProfile, transition));
        }

        private IEnumerator _CoroutineChangeProfile(CameraProfile profile, CameraProfileTransition transition = null)
        {
            IsTransitionActive = true;
            if (transition != null)
            {
                //TODO: Implements Transition 
                //Lerp between current position and profile position
                //Lerp between current rotation and profile rotation
                //Lerp between current size and profile Orthographic Size
                //Use profile animation curve to improve transition
                float timer = 0f;
                Vector3 initialPosition = _position;
                Quaternion initialRotation = _rotation;
                float initialSize = _size;
                while (timer < transition.Duration)
                {
                    timer += Time.deltaTime;
                    _position = Vector3.Lerp(initialPosition, profile.Position, transition.Curve.Evaluate(timer/transition.Duration));
                    _rotation = Quaternion.Lerp(initialRotation, profile.Rotation, transition.Curve.Evaluate(timer/transition.Duration));
                    _size = Mathf.Lerp(initialSize, profile.OrthographicSize, transition.Curve.Evaluate(timer/transition.Duration));
                    yield return null;
                }
            }
            _position = profile.Position;
            _rotation = profile.Rotation;
            _size = profile.OrthographicSize;
            IsTransitionActive = false;
        }
    }
}