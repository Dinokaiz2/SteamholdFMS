using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace SteamholdFMS
{
    public static class UltraKeyboard
    {
        private static KeyboardState oldState;
        private static KeyboardState newState;

        public static KeyboardState GetState()
        {
            return Keyboard.GetState();
        }

        /**
         * Get the changes since the last time getChanges() was called.
        **/
        public static KeyboardState getChanges()
        {
            oldState = newState;
            newState = Keyboard.GetState();
            List<Keys> oldStateKeys = oldState.GetPressedKeys().ToList<Keys>();
            List<Keys> newStateKeys = newState.GetPressedKeys().ToList<Keys>();
            KeyboardState changes = new KeyboardState(newStateKeys.Except(oldStateKeys).ToArray<Keys>());
            return changes;
        }
    }
}
