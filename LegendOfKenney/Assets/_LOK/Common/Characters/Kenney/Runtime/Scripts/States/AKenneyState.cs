using IIMEngine.Movements2D;
using LOK.Core.Room;
using UnityEditorInternal;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public abstract class AKenneyState
    {
        public KenneyStateMachine StateMachine { get; private set; }

        public KenneyMovementsData MovementsData => StateMachine.MovementsData;
        protected IMovable2DWriter Movable => StateMachine.IMovable;
        
        public void ChangeState(AKenneyState state) => StateMachine.ChangeState(state);

        public void StateInit(KenneyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            OnStateInit();
        }
        
        public void StateEnter(AKenneyState previousState)
        {
            OnStateEnter(previousState);
            RoomEvents.OnRoomEnter += OnRoomEnter;
        }

        public void StateExit(AKenneyState nextState)
        {
            OnStateExit(nextState);
            RoomEvents.OnRoomEnter -= OnRoomEnter;
        }

        public void StateUpdate()
        {
            OnStateUpdate();
        }
        
        private void OnRoomEnter(Room room, RoomEnterPoint roomEnterPoint)
        {
            if (Movable?.MoveDir == Vector2.zero) {
                ChangeState(StateMachine.StateIdle);
            }
        }

        protected virtual void OnStateInit() { }
        protected virtual void OnStateEnter(AKenneyState previousState) { }
        protected virtual void OnStateExit(AKenneyState nextState) { }
        protected virtual void OnStateUpdate() { }
    }
}