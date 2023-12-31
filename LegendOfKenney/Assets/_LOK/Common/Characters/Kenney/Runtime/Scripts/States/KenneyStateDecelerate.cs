﻿using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateDecelerate : AKenneyState
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
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Read Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StopDecelerationDuration
            _speedPercent = Movable.MoveSpeed / Movable.MoveSpeedMax;
        }

        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked
            //If there is MoveDir
            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
                //If MovementsData.TurnBackDecelerationDuration > 0 => Go to StateTurnBackDecelerate
                //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate
                //Else Go to StateAccelerate if MovementsData.StartAccelerationDuration > 0f
                //Else Go to StateWalk
            //Decrement _speedPercent with deltaTime
            //Calculate percent using _speedPercent and MovementsData.StopDecelerationDuration
            //If _speedPercent > MovementsData.StopDecelerationDuration
            //Go to StateIdle (deceleration is finished)
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            if (Movable.AreMovementsLocked || MovementsData.StopDecelerationDuration <= 0)
            {
                ChangeState(StateMachine.StateIdle);
                return;
            }
            if (Movable.MoveDir != Vector2.zero)
            {
                if (Vector2.Angle(Movable.MoveDir, Movable.OrientDir) > MovementsData.TurnBackAngleThreshold)
                {
                    ChangeState(StateMachine.StateTurnBackDecelerate);
                    return;
                }
                else 
                {
                    ChangeState(StateMachine.StateAccelerate);
                    return;   
                }
            }
            _speedPercent -= Time.deltaTime / MovementsData.StopDecelerationDuration;
            if (_speedPercent <= 0)
            {
                ChangeState(StateMachine.StateIdle);
                return;
            }
            Movable.MoveSpeed = Movable.MoveSpeedMax * _speedPercent;
        }
    }
}