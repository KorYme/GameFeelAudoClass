using IIMEngine.Movements2D;
using System.Threading;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateTurnBackDecelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _timer = 0f;

        #pragma warning restore 0414
        #endregion
        
        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Read Move Orient
            // - Write Move Speed
            // - Write Move IsTurningBack
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Set IsTurningBack to true
            Movable.IsTurningBack = true;
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackDecelerationDuration
            _timer = (Movable.MoveSpeed / Movable.MoveSpeedMax) * MovementsData.StopDecelerationDuration;
        }

        protected override void OnStateExit(AKenneyState nextState)
        {
            //Set IsTurningBack to false
            Movable.IsTurningBack = false;
        }
        
        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked
            if (Movable.AreMovementsLocked)
            {
                ChangeState(StateMachine.StateIdle);
                return;
            }
            //If there is MoveDir and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
                //Go to StateAccelerate
            if (Movable.MoveDir != Vector2.zero && Vector2.Angle(Movable.MoveDir, Movable.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                ChangeState(StateMachine.StateAccelerate);
                return;
            }
            //Increment _timer with deltaTime
            _timer += Time.deltaTime;
            //If _timer > MovementsData.TurnBackDecelerationDuration
                //Go to StateTurnBackAccelerate if there is MoveDir
                //Go to StateIdle otherwise
            if (_timer > MovementsData.TurnBackDecelerationDuration)
            {
                if (Movable.MoveDir == Vector2.zero)
                {
                    ChangeState(StateMachine.StateIdle);
                    return;
                }
                else
                {
                    ChangeState(StateMachine.StateTurnBackAccelerate);
                    return;
                }
            }
            //Calculate percent using timer and MovementsData.TurnBackDecelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            Movable.MoveSpeed = (_timer / MovementsData.StartAccelerationDuration) * Movable.MoveSpeedMax;
        }
    }
}