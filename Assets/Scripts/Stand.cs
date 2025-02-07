using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject targetPoint;  //halka çıkış noktası
    public GameObject[] sockets;
    public int emptySocket; //boş soket indexi
    public List<GameObject> _hoops = new(); //Standaki soketteki halkalar

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
}
