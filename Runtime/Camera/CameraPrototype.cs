using UnityEngine;

namespace Redeev.PrototypingTools
{
    public class CameraPrototype : MonoBehaviour
    {
        [SerializeField] Transform target;

        public Vector3 offset = new(-3f, 6f, -3f);
        public float fieldOfView = 55f;

        private Vector3 lastOffset = new(-1000f, -1000f, -1000f);
        private float lastFieldOfView = -1000f;

        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        private void Start()
        {
            CenterCamera();
        }

        private void CenterCamera()
        {
            transform.SetPositionAndRotation(target.position + offset, Quaternion.LookRotation(target.position - transform.position));
        }

        private void Update()
        {
            if (!target) return;

            if (lastFieldOfView != fieldOfView)
            {
                cam.fieldOfView = fieldOfView;
                lastFieldOfView = fieldOfView;
            }
            if (lastOffset != offset)
            {
                CenterCamera();
                lastOffset = offset;
            }

            transform.position += (target.position + offset - transform.position) * 20f * Time.deltaTime;
        }
    }
}