using System.Collections;
using Mgr;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Game
{
	public class CubeRotator : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
	{
		private const float RotationSpeed = 360f; // 旋转速度
		
		[SerializeField] private Transform m_Camera;
		
		private Vector2 m_MouseStartPos;
		private Vector2 m_MouseEndPos;
		private bool m_IsDragging;
		private bool m_IsRotating;

		public void OnBeginDrag(PointerEventData eventData)
		{
			m_MouseStartPos = eventData.position;
			m_IsDragging = true;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (GameManager.Inst.TouchInput.Pickup) return;
			if (!m_IsDragging || m_IsRotating) return;

			m_MouseEndPos = eventData.position;
			m_IsDragging = false;

			Vector2 swipeDirection = m_MouseEndPos - m_MouseStartPos;
			float swipeThreshold = 5f; // 最小滑动距离
			if (swipeDirection.magnitude < swipeThreshold) return; // 忽略微小滑动

			// 判断主要滑动方向
			bool isHorizontal = Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y);

			if (isHorizontal)
			{
				float angle = swipeDirection.x > 0 ? 90f : -90f;
				StartCoroutine(RotateCube(Vector3.up, angle));
			}
		}

		private IEnumerator RotateCube(Vector3 axis, float angle)
		{
			m_IsRotating = true;
			float rotationProgress = 0f;
			Quaternion startRotation = m_Camera.rotation;
			Quaternion targetRotation = Quaternion.Euler(m_Camera.eulerAngles + axis * angle);

			while (rotationProgress < 1f)
			{
				rotationProgress += Time.deltaTime * (RotationSpeed / Mathf.Abs(angle));
				m_Camera.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationProgress);
				yield return null;
			}

			m_Camera.rotation = targetRotation;
			Vector3 euler = m_Camera.eulerAngles;
			m_Camera.eulerAngles = new Vector3(
				Mathf.Round(euler.x / 90) * 90,
				Mathf.Round(euler.y / 90) * 90,
				Mathf.Round(euler.z / 90) * 90
			);

			m_IsRotating = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
		}
	}
}