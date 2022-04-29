using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Obi
{
    /**
     * This component plugs a prefab instance at each cut in the rope. Optionally, it will also place a couple instances at the start/end of an open rope.
     */
    [RequireComponent(typeof(ObiRope))]
    [RequireComponent(typeof(ObiPathSmoother))]
    public class ObiRopePrefabPlugger : MonoBehaviour
    {
        public GameObject prefab;                   /**< prefab object being instantiated at the rope cuts.*/
        public Vector3 instanceScale = Vector3.one;
        public bool plugTears = true;
        public bool plugStart = false;
        public bool plugEnd = false;

        private List<GameObject> instances;         /**< instances of the prefab being rendered. */
        private ObiPathSmoother smoother;
        
        public GameObject DragObject = null;
        public GameObject AttachedObject = null;
        public GameObject Head;
        public GameObject Hip;
        public GameObject[] ObiRopes;
        public GameObject ObiRope;
        public GameObject[] limpsPos;
        public GameObject prefabHead;
        public GameObject prefabHip;
        public GameObject prefabLimb;
        public SpriteRenderer TargetLip;
        bool isbroke;
        public ObiGameOver _gameOverControl;




        void OnEnable()
        {
            instances = new List<GameObject>();
            smoother = GetComponent<ObiPathSmoother>();
            smoother.OnCurveGenerated += UpdatePlugs;
        }

        void OnDisable()
        {
            smoother.OnCurveGenerated -= UpdatePlugs;
            ClearPrefabInstances();
        }

        private GameObject GetOrCreatePrefabInstance(int index)
        {
            if (index < instances.Count)
                return instances[index];

            GameObject tearPrefabInstance = Instantiate(prefab);
            tearPrefabInstance.hideFlags = HideFlags.HideAndDontSave;
            instances.Add(tearPrefabInstance);
            return tearPrefabInstance;
        }

        public void ClearPrefabInstances()
        {
            for (int i = 0; i < instances.Count; ++i)
                DestroyImmediate(instances[i]);

            instances.Clear();
        }

        // Update is called once per frame
        void UpdatePlugs(ObiActor actor)
        {
            
            var rope = actor as ObiRopeBase;

            // cache the rope's transform matrix/quaternion:
            Matrix4x4 l2w = rope.transform.localToWorldMatrix;
            Quaternion l2wRot = l2w.rotation;

            int instanceIndex = 0;

            // place prefabs at the start/end of each curve:
            for (int c = 0; c < smoother.smoothChunks.Count; ++c)
            {
                ObiList<ObiPathFrame> curve = smoother.smoothChunks[c];

                if ((plugTears && c > 0) ||
                    (plugStart && c == 0))
                {
                    //var instance = GetOrCreatePrefabInstance(instanceIndex++);
                    //instance.SetActive(true);


                    //Anil
                    if (!isbroke)
                    {
                        Debug.LogError("GameOver");
                        if (DragObject != null)
                        {
                            DragObject.SetActive(false);
                        }

                        if (AttachedObject != null)
                            AttachedObject.SetActive(false);
                        Head.SetActive(false);
                        Hip.SetActive(false);
                        int j = ObiRope.GetComponents<ObiParticleAttachment>().Length;
                        //ObiRope.GetComponents<ObiParticleAttachment>()[j - 1].enabled = false;
                        ObiRope.GetComponents<ObiParticleAttachment>()[j - 2].enabled = false;
                        if (TargetLip != null)
                            TargetLip.flipY = true;
                        if (prefabHead != null)
                        {
                            Vector3 pos = Head.transform.position;
                            pos.z += 0.6f;
                            Instantiate(prefabHead, pos, Quaternion.Euler(0f, 180f, 0f));
                        }
                            
                        if (prefabHip != null)
                        {
                            Vector3 pos = Head.transform.position;
                            pos.z += 0.6f;
                            Instantiate(prefabHip, pos, Quaternion.Euler(0f, 180f, 0f));
                        }
                            
                        if (limpsPos.Length != 0)
                        {
                            foreach (GameObject obj in limpsPos)
                            {
                                Instantiate(prefabLimb, obj.transform.position, Quaternion.identity);
                            }
                        }
                        foreach (GameObject r in ObiRopes)
                        {
                            int i = r.GetComponents<ObiParticleAttachment>().Length;

                            r.GetComponents<ObiParticleAttachment>()[i - 1].enabled = false;
                            r.GetComponent<MeshRenderer>().enabled = false;
                        }

                        isbroke = true;
                        ObiRope.GetComponent<MeshRenderer>().enabled = false;
                        if (_gameOverControl != null)
                            _gameOverControl.CallGameOver();
                    }

                    //Anil end
                    //ObiPathFrame frame = curve[0];
                    //instance.transform.position = l2w.MultiplyPoint3x4(frame.position);
                    //instance.transform.rotation = l2wRot * (Quaternion.LookRotation(-frame.tangent, frame.binormal));
                    //instance.transform.localScale = instanceScale;
                }

                if ((plugTears && c < smoother.smoothChunks.Count - 1) ||
                    (plugEnd && c == smoother.smoothChunks.Count - 1))
                {
                    if (!isbroke)
                    {
                    }
                }

                // deactivate remaining instances:
                for (int i = instanceIndex; i < instances.Count; ++i)
                    instances[i].SetActive(false);
            }
        }
        
        public void die()
        {
            if (GameObject.Find("HeadPrefab(Clone)") == null)
            {
                if (DragObject != null)
                {
                    DragObject.SetActive(false);
                }
                if (AttachedObject != null)
                    AttachedObject.SetActive(false);
                Head.SetActive(false);
                Hip.SetActive(false);
                int j = ObiRope.GetComponents<ObiParticleAttachment>().Length;
                //ObiRope.GetComponents<ObiParticleAttachment>()[j - 1].enabled = false;
                ObiRope.GetComponents<ObiParticleAttachment>()[j - 2].enabled = false;
                if (TargetLip != null)
                    TargetLip.flipY = true;
                if (prefabHead != null)
                    Instantiate(prefabHead, Head.transform.position, Quaternion.Euler(0f, 180f, 0f));
                if (prefabHip != null)
                    Instantiate(prefabHip, Hip.transform.position, Quaternion.Euler(0f, 180f, 0f));
                if (limpsPos.Length != 0)
                {
                    foreach (GameObject obj in limpsPos)
                    {
                        Instantiate(prefabLimb, obj.transform.position, Quaternion.identity);
                    }
                }
                foreach (GameObject r in ObiRopes)
                {
        
                    int i = r.GetComponents<ObiParticleAttachment>().Length;
        
                    r.GetComponents<ObiParticleAttachment>()[i - 1].enabled = false;
                    r.GetComponent<MeshRenderer>().enabled = false;
                }
                isbroke = true;
                ObiRope.GetComponent<MeshRenderer>().enabled = false;
                if (_gameOverControl != null)
                    _gameOverControl.CallGameOver();
            }
        }
    }
    
    
    


}