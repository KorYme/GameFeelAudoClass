using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateDecelerate : AKenneyState
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
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StopDecelerationDuration
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
            //If there is MoveDir
            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
                //If MovementsData.TurnBackDecelerationDuration > 0 => Go to StateTurnBackDecelerate
                //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate
                //Else Go to StateAccelerate if MovementsData.StartAccelerationDuration > 0f
                //Else Go to StateWalk
            if (Movable.MoveDir != Vector2.zero)
            {
                if (Vector2.Angle(Movable.MoveDir, Movable.OrientDir) > MovementsData.TurnBackAngleThreshold)
                {
                    if (MovementsData.TurnBackDecelerationDuration > 0)
                    {
                        ChangeState(StateMachine.StateTurnBackDecelerate);
                        return;
                    }
                    else if (MovementsData.TurnBackAccelerationDuration > 0)
                    {
                        ChangeState(StateMachine.StateTurnBackAccelerate);
                        return;
                    }
                }
                else if (MovementsData.StartAccelerationDuration > 0f)
                {
                    ChangeState(StateMachine.StateAccelerate);
                    return;
                }
                else
                {
                    ChangeState(StateMachine.StateWalk);
                    return;
                }
            }

            //Increment _timer with deltaTime
            _timer += Time.deltaTime;
            //If _timer > MovementsData.StopDecelerationDuration
            //Go to StateIdle (deceleration is finished)
            if (_timer > MovementsData.StopDecelerationDuration)
            {
                ChangeState(StateMachine.StateIdle);
                return;
            }

            //Calculate percent using timer and MovementsData.StopDecelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            Movable.MoveSpeed = (_timer / MovementsData.StartAccelerationDuration) * Movable.MoveSpeedMax;
        }
    }
}