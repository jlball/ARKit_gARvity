using System;
using System.Collections.Generic;
using UnityEngine.UI;


namespace UnityEngine.XR.iOS
{
	public class UnityARHitTestExample : MonoBehaviour
	{
		public Transform m_HitTransform;
		public float maxRayDistance = 100.0f;
		public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer
        public Slider sizeSlider;
        public Slider heightSlider;
        public GameObject mainCamera;
        public Material sunMaterial;
        public LayerMask sunCollisionLayer = 1 << 8;
        public Camera AR_Camera;
        public Button sunPlacerButton;
        public GameObject canvas;


        private bool sunPlaced = false;
        private Rigidbody rb;

        bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");
                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
                    Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                    return true;
                }
            }
            return false;
        }

        void Start() 
        {
            sunMaterial.color = new Color(sunMaterial.color.r, sunMaterial.color.g, sunMaterial.color.b, 0.5f);

            Button btn = sunPlacerButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);

            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

       //we will only use this script on the editor side, though there is nothing that would prevent it from working on device

            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward)); 
            RaycastHit hit;
                
            //Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), Color.red, 10000
            if (Physics.Raycast(ray, out hit, maxRayDistance, collisionLayer) && !sunPlaced)
            {
                    //we're going to get the position from the contact point
                    m_HitTransform.position = hit.point;
                    //and the rotation from the transform of the plane collider
                    m_HitTransform.rotation = hit.transform.rotation;
           
            if (Input.touchCount > 0)
                {

                    //sunPlaced = true;
                    
                    Debug.Log(sunPlaced);
                    //Ray sunRay = AR_Camera.ViewportPointToRay(Input.mousePosition);
                    //RaycastHit sunRayHit;

                    //Debug.Log("CLICK");    
                    //Debug.DrawRay(sunRay.origin, sunRay.direction, Color.cyan, 100.0f);            

                    //if (Physics.Raycast(sunRay, out sunRayHit, maxRayDistance, sunCollisionLayer))
                    //{
                    //    sunMaterial.color = new Color(sunMaterial.color.r, sunMaterial.color.g, sunMaterial.color.b, 1.0f);
                    //    sunPlaced = true;
                    //    Debug.Log(sunRayHit.collider.gameObject); 
                    //}
                }
            }

           
            if (!sunPlaced)
            {
                transform.localScale = new Vector3(sizeSlider.value, sizeSlider.value, sizeSlider.value);
                rb.mass = Mathf.Pow(sizeSlider.value, 3);

                transform.position = new Vector3(m_HitTransform.position.x, m_HitTransform.position.y + heightSlider.value, m_HitTransform.position.z);
            }
             
			//if (Input.touchCount > 0 && m_HitTransform != null)
			//{
			//	var touch = Input.GetTouch(0);
   //             if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) && !sunPlaced)
			//	{
			//		var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
			//		ARPoint point = new ARPoint {
			//			x = screenPosition.x,
			//			y = screenPosition.y
			//		};

   //                 // prioritize reults types
   //                 ARHitTestResultType[] resultTypes = {
   //                     ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
   //                     // if you want to use infinite planes use this:
   //                     //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
   //                     ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
   //                     ARHitTestResultType.ARHitTestResultTypeFeaturePoint
   //                 }; 
					
   //                 sunPlaced = true;

   //                 foreach (ARHitTestResultType resultType in resultTypes)
   //                 {
   //                     if (HitTestWithResultType (point, resultType))
   //                     {
   //                         return;
   //                     }
   //                 }
			//	}
			//}

        }

        void TaskOnClick()
        {
            sunPlaced = true;
            sunMaterial.color = new Color(sunMaterial.color.r, sunMaterial.color.g, sunMaterial.color.b, 1.0f);
            canvas.SetActive(false);
        }

	
	}
}

