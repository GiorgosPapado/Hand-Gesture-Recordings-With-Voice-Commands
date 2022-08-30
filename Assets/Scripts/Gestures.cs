using UnityEditor;
using UnityEngine;

namespace Assets
{
    public enum ControllerGesture
    {
        INFINITY,
        L,
        O,
        M,
        e,
        LessThan,
        U,
        I
    }

    public class ControllerGestureRecognizer
    {
        public static bool GetGesture(ControllerGesture gesture)
        {
            return false;    
        }        
    }
}