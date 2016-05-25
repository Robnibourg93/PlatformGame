using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Button : MonoBehaviour
    {
        private bool pushed = false;
        public float speed = 2f;
        Vector3 desiredPosition = new Vector3(0f,3.4f);

        void Start() {

        }

        void Update() {
            if (pushed)
            {
                if (transform.localPosition.y <= -1f) {
                    pushed = false;
                }
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition - desiredPosition, Time.deltaTime * speed);
            }
        }

        void OnTriggerEnter(Collider c)
        {

            if (c.tag == "Player")
            {
                pushed = true;
            }
        }

    }
}
