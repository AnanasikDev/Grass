using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] Transform[] grass;
    public void LookAt(Vector3 position)
    {
        foreach (Transform g in grass)
        {
            g.transform.LookAt(position);
            g.transform.localEulerAngles += Vector3.up * 180;
            if (GrassManager.instance.yOnly)
                g.transform.localEulerAngles = new Vector3(0, g.transform.localEulerAngles.y, 0);
        }
    }
}
