using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace YJ.PocketGame
{
    public class Meteor : MonoBehaviour
    {
        private Vector3 targetPosition;
        private float speed = 10.0f; // 메테오가 떨어지는 속도

        public void Initialize(Vector3 target)
        {
            targetPosition = target;
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                                                             //   효과 및 데미지 처리
                Debug.Log("메테오 도착");
                                                              // 폭발 효과나 데미지 로직 추가
                Destroy(gameObject); // 메테오 파괴
            }
        }

    }
}
