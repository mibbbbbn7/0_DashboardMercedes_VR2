using UnityEngine;

public class PositionResetterInLateUpdate : MonoBehaviour
{
    public bool Dirty { get; set; }

    private void LateUpdate()
    {
        if (Dirty)
        {
            Dirty = false;
            transform.localPosition = Vector3.zero;
        }
    }
}
