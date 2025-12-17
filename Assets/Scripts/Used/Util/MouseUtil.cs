using UnityEngine;

public static class MouseUtil
{
    private static Camera camera;

    public static void SetCamera(Camera cam)
    {
        camera = cam;
    }

    public static bool IsReady => camera != null;

    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        if (camera == null)
        {
            Debug.LogError("MouseUtil: Camera not set");
            return Vector3.zero;
        }

        Plane dragPlane = new(camera.transform.forward, new Vector3(0, 0, zValue));
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (dragPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }
}
