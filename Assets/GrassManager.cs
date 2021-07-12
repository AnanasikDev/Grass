using System.Collections;
using System.Linq;
using System;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    [SerializeField] int a = 4; // Кол-во групп. Каждые a кадры повторяются
    [SerializeField] int maxDistance;
    public bool yOnly = false;
    [SerializeField] Grass[] grass;
    WaitForEndOfFrame tick;

    [SerializeField] Transform followingObject; // Объект, на который смотрит трава
    public static GrassManager instance { get; private set; }
    void Awake() => instance = this;
    private void Start()
    {
        tick = new WaitForEndOfFrame();
        StartCoroutine("RotationUpdate");
    }
    IEnumerator RotationUpdate()
    {
        Grass[] sortedGrass = grass.Where(g => (g.transform.position - followingObject.position).sqrMagnitude < maxDistance * maxDistance)
            .ToArray(); //.OrderBy(g => (g.transform.position - followingObject.position).sqrMagnitude).ToArray();
        for (int frame = 0; frame < a; frame++)
        {
            Grass[] currentGrassGroup = sortedGrass.SubArray(frame * sortedGrass.Length / a, sortedGrass.Length / a);
            foreach (Grass g in currentGrassGroup)
            {
                g.LookAt(followingObject.position);
            }
            yield return tick;
        }
        /*foreach (Grass g in sortedGrass)
        {
            g.LookAt(followingObject.position);
        }
        yield return tick;*/
        yield return RotationUpdate();
    }
}
public static class Extensions
{
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
        T[] result = new T[length];
        Array.Copy(array, offset, result, 0, length);
        return result;
    }
}