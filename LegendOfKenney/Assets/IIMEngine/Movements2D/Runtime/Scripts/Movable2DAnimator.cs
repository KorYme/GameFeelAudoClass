using UnityEngine;

namespace IIMEngine.Movements2D
{
    public class Movable2DAnimator : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private GameObject _movableGameObject;

        [Header("Parameters")]
        [SerializeField] private string _isMovingParameter = "IsMoving";
        private int _isMovingParameterHash;
        
        [Header("Animation Speed")]
        [SerializeField] private float _animatorSpeedMin = 0.5f;
        [SerializeField] private float _animatorSpeedMax = 1f;
        
        #pragma warning restore 0414
        #endregion

        IMovable2DReader _movableReader;
        Animator _animator;

        private void Awake()
        {
            //Convert _isMovingParameter to Hash 
            //(Improve performance when calling Animator.SetParameter)
            _isMovingParameterHash = Animator.StringToHash(_isMovingParameter);

            //Find Movable Interfaces inside _movableGameObject needed to check if object is moving
            //(You'll probably need to check if object movements are locked and if object move speed > 0)
            _movableReader = _movableGameObject.GetComponent<IMovable2DReader>();

            //Find Animator (attached to this gameObject)
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //Check if object is moving (store it inside a bool)
            //Bonus : Get Object movement speed and speed max to interpolate animator speed
            //Set animator parameter bool "IsMoving" according to movements infos
            _animator.SetBool(_isMovingParameterHash, _movableReader?.MoveSpeed > 0);
            _animator.speed = Mathf.Lerp(_animatorSpeedMin, _animatorSpeedMax, _movableReader.MoveSpeed / _movableReader.MoveSpeedMax);
        }
    }
}