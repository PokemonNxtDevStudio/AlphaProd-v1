using System.Collections;
using UnityEngine;

namespace NXT
{
    public class AOEMonitor : MonoBehaviour
    {

        public GameObject UIAOEPrefab;

        private bool m_isActive = false;
        private GameObject m_trainer;
        public GameObject thunder;
        private GameObject cachedPrefab;
        void Start()
        {
            /////////
            if (m_trainer != null)
                m_trainer = null;

            cachedPrefab = (GameObject)Instantiate(UIAOEPrefab, transform.position, Quaternion.Euler(90, 0, 0));
            cachedPrefab.transform.parent = this.gameObject.transform;
            

            //TODO: Register to trainer gameObject
            EventHandler.RegisterEvent(this.gameObject, EventAOE.TRIGGER_ON, TriggerOn);
            EventHandler.RegisterEvent<Vector3>(this.gameObject, EventAOE.EXECUTE, Excecute);
         
        }

        void SetTrainer(GameObject trainer)
        {
            m_trainer = trainer;
        }

      

        void TriggerOn()
        {
            m_isActive = true;
            StartCoroutine(TriggerOnCo());
        }

        IEnumerator TriggerOnCo()
        {
            Debug.Log("Activating AOE");
            cachedPrefab.SetActive(true);
           
            RaycastHit hit;
            while (m_isActive)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
                    cachedPrefab.transform.position = hit.point + new Vector3(0, 5, 0);

                    if(Input.GetMouseButtonDown(1))
                    {
        
                          EventHandler.ExecuteEvent<Vector3>(this.gameObject, EventAOE.EXECUTE, hit.point);
                          Instantiate(thunder, hit.point, Quaternion.identity);
                    }

                }
                yield  return null;
            }
            yield  return null;
        }

        void Excecute(Vector3 pos)
        {
            m_isActive = false;
            cachedPrefab.SetActive(false);
        }

    }

}