using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateTurnBackAccelerate : AKenneyState
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
            // - Write Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackAccelerationDuration
            _timer = (Movable.MoveSpeed / Movable.MoveSpeedMax) * MovementsData.StopDecelerationDuration;
        }

        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked
            if (Movable.AreMovementsLocked)
            {
                ChangeState(StateMachine.StateIdle);
                return;
            }
            //If there is no MoveDir
                //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
                //Go to StateIdle otherwise
            if (Movable.MoveDir == Vector2.zero)
            {
                if (MovementsData.StopDecelerationDuration > 0)
                {
                    ChangeState(StateMachine.StateDecelerate);
                    return;
                }
                else
                {
                    ChangeState(StateMachine.StateIdle);
                    return;
                }
            }
            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateTurnBackDecelerate
            if (Vector2.Angle(Movable.MoveDir, Movable.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                ChangeState(StateMachine.StateTurnBackDecelerate);
                return;
            }
            //Increment _timer with deltaTime
            _timer += Time.deltaTime;
            //If _timer > MovementsData.TurnBackAccelerationDuration
            //Go to StateWalk (acceleration is finished)
            if (_timer > MovementsData.TurnBackAccelerationDuration)
            {
                ChangeState(StateMachine.StateWalk);
                return;
            }
            //Calculate percent using timer and MovementsData.TurnBackAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            Movable.MoveSpeed = (_timer / MovementsData.StartAccelerationDuration) * Movable.MoveSpeedMax;
            //Force OrientDir to MoveDir
            Movable.OrientDir = Movable.MoveDir;
        }
    }
}