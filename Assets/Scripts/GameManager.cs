using System;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class GameManager : MonoBehaviour
{
    GameObject selectedObject;
    GameObject selectedStand;
    Hoop _cember;
    public bool isMove;
    public AudioSource[] sounds;


    public int targetStandCount;
    int completedStandCount;

    [SerializeField]
    private GameObject vfx;

    private void Start()
    {
        vfx.SetActive(false);
    }

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

                        if (_stand._hoops.Count != 4 && _stand._hoops.Count != 0)   //stand dolu veya boş kontrolü
                        {
                            if (_cember.renk == _stand._hoops[^1].GetComponent<Hoop>().renk) //çember renkleri eşit mi kontrolü
                            {
                                selectedStand.GetComponent<Stand>().ChangingSocket(selectedObject);
                                _cember.HareketEt("pozisyondegistir", hit.collider.gameObject, _stand.GetProperSocket(), _stand.targetPoint);
                                _stand.emptySocket++;
                                _stand._hoops.Add(selectedObject);
                                _stand.CheckHoops(); //stand çemberi 4 oldu mu
                                selectedObject = null; //yeni çember taşıma işlemi için 
                                selectedStand = null;
                                sounds[0].Play();
                            }
                            else
                            {
                                _cember.HareketEt("soketegerigit");
                                selectedStand = null;
                                selectedObject = null;
                                sounds[1].Play();
                            }
                        }
                        else if (_stand._hoops.Count == 0) //stand boşsa çember yer değiştirebilir.
                        {
                            selectedStand.GetComponent<Stand>().ChangingSocket(selectedObject);
                            _cember.HareketEt("pozisyondegistir", hit.collider.gameObject, _stand.GetProperSocket(), _stand.targetPoint);
                            _stand.emptySocket++;
                            _stand._hoops.Add(selectedObject);
                            _stand.CheckHoops();
                            selectedObject = null; //yeni çember taşıma işlemi için 
                            selectedStand = null;
                            sounds[0].Play();
                        }
                    }
                    else if(selectedStand == hit.collider.gameObject)
                    {
                        _cember.HareketEt("soketegerigit");
                        selectedStand = null;
                        selectedObject = null;
                        sounds[1].Play();
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

    public void StandCompleted()
    {
        completedStandCount++;
        if (targetStandCount == completedStandCount)
        {
            Debug.Log("KAZANIDIN"); //kazandın paneli için tetik.
            sounds[2].Play();
            vfx.SetActive(true);
            Invoke("NextScene", 3);
        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
