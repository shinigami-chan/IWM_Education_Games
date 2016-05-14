using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
public class Dbg : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------
	public   string          LogFile = "log.txt";
	public   bool            EchoToConsole = true;
	public   bool            AddTimeStamp = true;
	
	//-------------------------------------------------------------------------------------------------------------------------
	private  StreamWriter    OutputStream;
	
	//-------------------------------------------------------------------------------------------------------------------------
	static   Dbg             Singleton = null;
	
	//-------------------------------------------------------------------------------------------------------------------------
	public static Dbg Instance
	{
		get { return Singleton; }
	}

	void Awake()
	{
		if (Singleton != null)
		{
			UnityEngine.Debug.LogError("Multiple Dbg Singletons exist!");
			return;
		}
		
		Singleton = this;
		
		#if !FINAL
		// Open the log file to append the new log to it.
		OutputStream = new StreamWriter( LogFile, true );
		#endif
	}

	void OnDestory()
	{
		#if !FINAL
		if ( OutputStream != null )
		{
			OutputStream.Close();
			OutputStream = null;
		}
		#endif
	}
}