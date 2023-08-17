using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace MyInputSystems {
    public class TestInput : MonoBehaviour {
        static bool moveIsInput;

        public TestInput () {
            moveIsInput = false;
        }

        private void Update() {
            if ( moveIsInput ){
                Debug.Log("test");    
            }
        }

        public void testInput (InputAction.CallbackContext context) {
            Debug.Log(context.phase);
            Debug.Log("test");
        }

        public void testMove (InputAction.CallbackContext context) {
            if (context.performed){
                Debug.Log("input");
                moveIsInput = true; 
            } else if (context.canceled){
                moveIsInput = false;
            }
        }
    }
}