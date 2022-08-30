using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets
{

    public enum HandGesture
    {
        Victory,
        Fist,
        ThumbsUp
    }

    public class HGR
    {
        public static bool Recognized(HandGesture gesture)
        {
            return false;
        }
    }
}