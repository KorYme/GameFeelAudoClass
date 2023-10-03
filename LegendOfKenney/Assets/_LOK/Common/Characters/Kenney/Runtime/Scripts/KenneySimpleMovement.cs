using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneySimpleMovement : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;

#pragma warning restore 0414
        #endregion

        IMovable2D _iMovable2DWriter;

        private void Awake()
        {
            //Find Movable Interfaces (at the root of this gameObject)
            //For this script, you will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Write Move Orient
            // - Write Move Speed
            _iMovable2DWriter = GetComponent<IMovable2D>();
        }

        private void Update()
        {
            //If Movements are locked
            //Force MoveSpeed to 0

            if (_iMovable2DWriter.AreMovementsLocked)
            {
                _iMovable2DWriter.MoveSpeed = 0;
                return;
            }

            //If there is MoveDir
                //Set MoveSpeed to MovementsData.SpeedMax
                //Set Move OrientDir to Movedir
            //Else
                //Set MoveSpeed to 0

            if (_iMovable2DWriter.MoveDir != Vector2.zero)
            {
                _iMovable2DWriter.MoveSpeed = _movementsData.SpeedMax;
                _iMovable2DWriter.OrientDir = _iMovable2DWriter.MoveDir;
            }
            else
            {
                _iMovable2DWriter.MoveSpeed = 0f;
            }
        }
    }
}