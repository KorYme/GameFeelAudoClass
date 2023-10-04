using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Conditions
{
    public class EffectConditionHasOrientDirX : AEffectCondition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private EntityTarget _target;
        private IMovable2D _iMovable;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnConditionInit()
        {
            _iMovable = _target.FindFirstResult<IMovable2D>();
        }

        public override bool IsValid()
        {
            //TODO: Check if target OrientDir.X is not null (using OrientReader)
            return _iMovable.OrientDir.x != 0;
        }
    }
}