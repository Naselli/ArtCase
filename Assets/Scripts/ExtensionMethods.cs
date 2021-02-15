using UnityEngine;

namespace DefaultNamespace {
    static public class ExtensionMethods {

        public static Vector3 GetLookDirection( Vector3 v ) {
            return new Vector3( v.x , 0f , v.z );
        }
    }
}