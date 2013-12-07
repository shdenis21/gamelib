using System;
using gamelib.Client;
using gamelib.Common;
using UnityEngine;
using System.Collections.Generic;

	public class MainClient : MonoBehaviour
	{
	//DEBUG
	public bool debug = false;
	//подключение на старте
	public bool ConnectOnStart = true;
	//параметры подключения
	public string ip = "";
	public int port =0;
	//клас клиента
	private Client _client;
	//авторизация
	public IAuthen _authentiphications;
	//выполнение метода после подключения
	public AfterConnected aftercon;
	//id сесии клиента	
	public int Sesion =0;
	//префабы моделей
	public	System.Collections.Generic.List<Connection.prefabid> RemotesPrototypes = new System.Collections.Generic.List<Connection.prefabid>();
		
	void Start()
	{
	if(ConnectOnStart)
		{
			Run();
		}
	}
	
	void Run()
	{
	 _client = new Client(ip,port, null, _authentiphications, aftercon, RemotesPrototypes);
	}
	
	
	
	
	
		
	}


