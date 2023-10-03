using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateAccelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _speedPercent = 0f;

        #pragma warning restore 0414
        #endregion

        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read MoveDir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Force OrientDir to MoveDir
            Movable.OrientDir = Movable.MoveDir;
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StartAccelerationDuration
            _speedPercent = Movable.MoveSpeed / Movable.MoveSpeedMax;
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
            //If MovementsData.TurnBackDecelerationDuration > 0 => Go to StateTurnBackDecelerate
            //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate
            if (Vector2.Angle(Movable.MoveDir, Movable.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                if (MovementsData.TurnBackDecelerationDuration > 0)
                {
                    ChangeState(StateMachine.StateTurnBackDecelerate);
                    return;
                }
                else
                {
                    ChangeState(StateMachine.StateIdle);
                    return;
                }
            }
            //Increment _timer with deltaTime
            _speedPercent += Time.deltaTime / MovementsData.StartAccelerationDuration;
            //If _timer > MovementsData.StartAccelerationDuration
                //Go to StateWalk (acceleration is finished)

            if (_speedPercent >= 1)
            {
                ChangeState(StateMachine.StateWalk);
                return;
            }

            //Calculate percent using timer and MovementsData.StartAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            //Force OrientDir to MoveDir
            Movable.MoveSpeed = Mathf.Lerp(0, Movable.MoveSpeedMax, _speedPercent);
            Movable.OrientDir = Movable.MoveDir;
        }
    }
}