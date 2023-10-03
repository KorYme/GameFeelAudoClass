using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateWalk : AKenneyState
    {
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
            //Force MoveSpeed to MoveSpeedMax
            Movable.MoveSpeed = Movable.MoveSpeedMax;
            //Force OrientDir to MoveDir
            Movable.OrientDir = Movable.MoveDir;
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
            //If MovementsData.TurnBackDecelerationDuration > 0 and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateTurnBackDecelerate
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

            //Force OrientDir to MoveDir
            Movable.OrientDir = Movable.MoveDir;
        }
    }
}