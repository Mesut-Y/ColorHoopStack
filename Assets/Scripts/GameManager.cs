using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngineInternal;

public class GameManager : MonoBehaviour
{
    GameObject selectedObject;
    GameObject selectedStand;
    Hoop _cember;
    public bool isMove;

    //they are initialized for after
    public int targetStandCount;
    int completedStandCount;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    //Debug.Log("Standa tıklandı.");

                    if (selectedObject != null && selectedStand != hit.collider.gameObject)
                    { // çember hedef bir yere gönderilecek
                        Stand _stand = hit.collider.GetComponent<Stand>(); //yeni seçilen stand
                        selectedStand.GetComponent<Stand>().ChangingSocket(selectedObject); 

                        _cember.HareketEt("pozisyondegistir", hit.collider.gameObject, _stand.GetProperSocket(), _stand.targetPoint);
                        _stand.emptySocket++;
                        _stand._hoops.Add(selectedObject);

                        selectedObject = null; //yeni çember taşıma işlemi için 
                        selectedStand = null;
                    }
                    else if( selectedStand == hit.collider.gameObject)
                    {
                        _cember.HareketEt("soketegerigit");
                        selectedStand = null;
                        selectedObject = null; 
                    }
                    else
                    {   
                        Stand _stand = hit.collider.GetComponent<Stand>();
                        selectedObject = _stand.GetLastHoop();
                        _cember = selectedObject.GetComponent<Hoop>();
                        isMove = true;

                        if (_cember.hareketEdebilirmi)
                        {
                            _cember.HareketEt("secim", null, null, _cember._aitOlduguStand.GetComponent<Stand>().targetPoint);
                            selectedStand = _cember._aitOlduguStand;
                        }
                    }

                }
            }
        }
    }
}
