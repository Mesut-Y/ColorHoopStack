using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject targetPoint;  //halka çıkış noktası
    public GameObject[] sockets;
    public int emptySocket; //boş soket indexi
    public List<GameObject> _hoops = new(); //Standaki soketteki halkalar
    int completedHoops;

    [SerializeField] private GameManager _GameManager;

    public GameObject GetLastHoop()
    {
        return _hoops[^1]; //_hoops[_Cemberler.Count-1];
    }

    public GameObject GetProperSocket()
    {
        return sockets[emptySocket];
    }

    public void ChangingSocket(GameObject silinecekObje)
    {
        _hoops.Remove(silinecekObje);
        if(_hoops.Count != 0)
        {
            emptySocket--;
            _hoops[^1].GetComponent<Hoop>().hareketEdebilirmi = true;
        }
        else
        {
            emptySocket = 0; //soket indexi
        }


    }

    public void CheckHoops()
    {
        if (_hoops.Count == 4)
        {
            string renk = _hoops[0].GetComponent<Hoop>().renk;

            foreach (var item in _hoops)
            {
                if (renk == item.GetComponent<Hoop>().renk)
                    completedHoops++;
            }
            if (completedHoops == 4)
            {
                Debug.Log("Tamamlandı.");
            }
            else
            {
                Debug.Log("Tamamlandı.");
                completedHoops = 0;
            }
        }
    }
}
