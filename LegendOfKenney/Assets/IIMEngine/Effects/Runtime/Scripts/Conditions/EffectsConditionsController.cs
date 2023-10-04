using System.Linq;
using UnityEngine;

namespace IIMEngine.Effects
{
    public class EffectsConditionsController : MonoBehaviour
    {
        #region DO NOT MODIFY
        
        [Header("Conditions")]
        [SerializeField] private AEffectCondition[] _conditions;

        private AEffect[] _effects;
        
        #endregion

        private void Awake()
        {
            //Find All effects attached to this gameObject
            //Call ConditionInit() method for all conditions stored
            _effects = GetComponents<AEffect>();
            _conditions.ToList().ForEach(condition => condition.ConditionInit());
        }

        private void Update()
        {
            //TODO: call Play() method in attached playing effects if ALL conditions are valid
            //TODO: call Stop() method in attached non playing effects if conditions are not valid
            if (_conditions.All(x => x.IsValid()))
            {
                _effects.ToList().ForEach(effect => effect.Play());
            }
            else
            {
                _effects.ToList().ForEach(effect => effect.Stop());
            }
        }
    }
}