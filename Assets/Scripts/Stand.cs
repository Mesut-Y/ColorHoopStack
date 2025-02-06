using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject targetPoint;  //halka çıkış noktası
    public GameObject[] sockets;
    public int emptySocket; //soketboşluğu
    public List<GameObject> _hooks = new(); //Standaki soketteki halkalar

    [SerializeField] private GameManager _GameManager;

    public GameObject GetLastHook()
    {
        
        return _hooks[^1]; //_hooks[_Cemberler.Count-1];

    }
}
