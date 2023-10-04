using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Modifiers
{
    public class EffectModifierMoveSpeed : AEffectModifierFloat
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private EntityTarget _targetGameObject;
        private IMovable2D _iMovable;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnModifierInit()
        {
            _iMovable = _targetGameObject.FindFirstResult<IMovable2D>();
        }

        public override float GetValue()
        {
            //TODO: Calculate and return Percentage according to MoveSpeed and MoveSpeedMax
            return _iMovable.MoveSpeed / _iMovable.MoveSpeedMax;
        }
    }
}