using UnityEngine;

public class ClockManager : MonoBehaviour
{
    private Transform m_handTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localRotation.Set(m_handTransform.rotation.x, m_handTransform.rotation.y, 0.4f, m_handTransform.rotation.w);
        gameObject.transform.localPosition.Set(2f, 400f, 0f);
    }
}
