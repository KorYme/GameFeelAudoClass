using IIMEngine.Movements2D;
using LOK.Core.UserProfiles;
using LOK.Core.Interactions;
using UnityEngine;
using Rewired;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyController : MonoBehaviour
    {
        [SerializeField] private GameObject _kenneyRoot = null;

        private IMove2DDirWriter _moveDirWriter;
        private Interactor _interactor;

        Player _currentPlayer;

        const string ACTION_NAME_INTERACTION = "Interact";
        const string ACTION_NAME_MOVE_HORIZONTAL = "MoveHorizontal";
        const string ACTION_NAME_MOVE_VERTICAL = "MoveVertical";

        private void Start()
        {
            _moveDirWriter = _kenneyRoot.GetComponent<IMove2DDirWriter>();
            _interactor = _kenneyRoot.GetComponent<Interactor>();
            _currentPlayer = ReInput.players.GetPlayer("Player0");
        }

        void Update()
        {
            if (UIPopupPasswordValidator.Instance.IsOpened
                || UIPopupPasswordEnterUserName.Instance.IsOpened) {
                _moveDirWriter.MoveDir = Vector2.zero;
            } else {
                _MovePlayerFromInputs();
            }

            _ManageInputAction();
        }

        private void _ManageInputAction()
        {
            if (UIPopupPasswordValidator.Instance.IsOpened) return;
            if (UIPopupPasswordEnterUserName.Instance.IsOpened) return;

            if (_GetInputDownAction()) {
                Vector3 position = _kenneyRoot.transform.position;
                IInteractable interactable = _interactor.FindClosestInteractableNearBy(position);
                if (_interactor.CanInteract && interactable != null) {
                    interactable.Interact();
                }
            }
        }

        private void _MovePlayerFromInputs()
        {
            //TODO: Write MoveDir according to inputs
            //You can use _GetInputAxisMovement()
            //Don't forget to normalize ;)
            _moveDirWriter.MoveDir = _GetInputAxisMovement().normalized;
        }

        private bool _GetInputDownAction() => _currentPlayer.GetButtonDown(ACTION_NAME_INTERACTION);
        private Vector2 _GetInputAxisMovement() => _currentPlayer.GetAxis2D(ACTION_NAME_MOVE_HORIZONTAL, ACTION_NAME_MOVE_VERTICAL);
    }
}