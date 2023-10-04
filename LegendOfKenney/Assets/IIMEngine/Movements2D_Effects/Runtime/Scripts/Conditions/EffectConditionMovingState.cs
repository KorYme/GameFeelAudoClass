using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Conditions
{
    public class EffectConditionMovingState : AEffectCondition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Target")]
        [SerializeField] private EntityTarget _target;
        private IMovable2D _iMovable;

        public enum MoveCheckState
        {
            Moving = 0,
            NotMoving,
        }
        
        [Header("Move Check State")]
        [SerializeField] private MoveCheckState _moveCheckState = MoveCheckState.Moving;

        #pragma warning restore 0414
        #endregion
        
        protected override void OnConditionInit()
        {
            _iMovable = _target.FindFirstResult<IMovable2D>();
        }

        public override bool IsValid()
        {
            //TODO: Check if target is moving (using MoveLockedReader and MoveDirReader)
            return !_iMovable.AreMovementsLocked && _iMovable.MoveSpeed > 0;
        }
    }
}