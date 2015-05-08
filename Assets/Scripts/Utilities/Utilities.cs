

using UnityEngine;

namespace NXT.Controllers
{
    public static class Utility
    {
        public static T GetComponentInParent<T>(this Transform transform) where T : Component
        {
            T component = ((Component)transform).GetComponent<T>();
            if (component!=null)
                return component;
            Transform parent = transform.parent;
            if (parent!=null)
                return ((Component)parent).GetComponentInParent<T>();
            return (T)null;
        }

        public static T GetChildComponent<T>(this Transform transform) where T : Component
        {
            for (int index = 0; index < transform.childCount; ++index)
            {
                T componentInChildren = ((Component)transform.GetChild(index)).GetComponentInChildren<T>();
                if (componentInChildren!= null)
                    return componentInChildren;
            }
            return (T)null;
        }

        public static float RestrictPositiveAngle(float angle)
        {
            if ((double)angle < 0.0)
                angle += 360f;
            if ((double)angle > 360.0)
                angle -= 360f;
            return angle;
        }

        public static float RestrictAngle(float angle)
        {
            if ((double)angle < -360.0)
                angle += 360f;
            if ((double)angle > 360.0)
                angle -= 360f;
            return angle;
        }

        public static float RestrictInnerAngle(float angle)
        {
            if ((double)angle < -180.0)
                angle += 360f;
            if ((double)angle > 180.0)
                angle -= 360f;
            return angle;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            return Mathf.Clamp(Utility.RestrictAngle(angle), min, max);
        }

        public static bool InLayerMask(int layer, int layerMask)
        {
            return (1 << layer & layerMask) == 1 << layer;
        }

        public static Camera FindCamera()
        {
            if (Camera.main != null)
                return Camera.main;
            for (int index = 0; index < Camera.allCameras.Length; ++index)
            {
                if (Camera.allCameras[index].GetComponent<CameraController>()!=null)
                    return Camera.allCameras[index];
            }
            Debug.LogError((object)"No camera exists with the CameraController component. Has this component been added to a camera?");
            return (Camera)null;
        }
    }
}
