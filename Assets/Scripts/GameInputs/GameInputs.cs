using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameControl
{
    public class GameInputs : MonoBehaviour
    {
        public static GameInputs Instance { get; private set; }

        private GameControls gameControls;
        private Camera cam;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
            gameControls = new GameControls();
        }

        private void Start()
        {
            cam = Camera.main;
            gameControls.Game.Enable();
            gameControls.Game.MouseClicked.canceled += MouseClicked_canceled;
        }

        private void MouseClicked_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            GameManager.Instance.Drag = false;
        }

        public bool IsMouseHold()
        {
            return gameControls.Game.MouseClicked.IsPressed();
        }

        public Vector3 MousePosInWorld()
        {
            return cam.ScreenToWorldPoint(gameControls.Game.MousePosition.ReadValue<Vector2>());
        }
    }
}

