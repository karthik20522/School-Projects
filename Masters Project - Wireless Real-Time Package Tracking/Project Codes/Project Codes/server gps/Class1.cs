using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Threading;



class ClientConnectionPool 
{
	// Creates a synchronized wrapper around the Queue.
	private  Queue SyncdQ = Queue.Synchronized( new Queue() );
      
	public  void Enqueue(ClientHandler client) 
	{
		SyncdQ.Enqueue(client) ;
	}
        
	public  ClientHandler Dequeue() 
	{
		return (ClientHandler) ( SyncdQ.Dequeue() ) ;
	}
        
	public  int Count 
	{
		get { return SyncdQ.Count ; }
	}

	public object SyncRoot 
	{
		get { return SyncdQ.SyncRoot ; }
	}
        
} // class ClientConnectionPool
        
class ClientService 
{
        
	const int NUM_OF_THREAD = 10;
        
	private ClientConnectionPool ConnectionPool  ;
	private bool ContinueProcess = false ;
	private Thread [] ThreadTask  = new Thread[NUM_OF_THREAD]  ;
      
	public ClientService(ClientConnectionPool ConnectionPool) 
	{
		this .ConnectionPool = ConnectionPool ;
	}        

	public void Start() 
	{
		ContinueProcess = true ;
		// Start threads to handle Client Task
		for ( int i = 0 ; i < ThreadTask.Length ; i++) 
		{
			ThreadTask[i] = new Thread( new ThreadStart(this.Process) );
			ThreadTask[i].Start() ;
		}
	}
        
	private  void Process()  
	{
		while ( ContinueProcess ) 
		{	

			ClientHandler client  = null ;
			lock( ConnectionPool.SyncRoot ) 
			{
				if  ( ConnectionPool.Count > 0 )
					client = ConnectionPool.Dequeue() ;
			}		                         
			if ( client != null ) 
			{
				client.Process() ; // Provoke client
				// if client still connect, schedufor later processingle it 
				if ( client.Alive ) 
					ConnectionPool.Enqueue(client) ;
			}
                        
			Thread.Sleep(100) ;
		}         
	}
        
	public  void Stop() 
	{
		ContinueProcess = false ;        
		for ( int i = 0 ; i < ThreadTask.Length ; i++) 
		{
			if ( ThreadTask[i] != null &&  ThreadTask[i].IsAlive )  
				ThreadTask[i].Join() ;
		}
                        
		// Close all client connections
		while ( ConnectionPool.Count > 0 ) 
		{
			ClientHandler client = ConnectionPool.Dequeue() ;
			client.Close() ; 
			Console.WriteLine("Client connection is closed!") ;
		}
	}
        
} // class ClientService

public  class SynchronousSocketListener 
{
        
	private  const int  portNum = 10116 ;
  
	public  static  void StartListening() 
	{

		ClientService ClientTask  ;
        
		// Client Connections Pool
		ClientConnectionPool ConnectionPool = new ClientConnectionPool()  ;          
        
		// Client Task to handle client requests
		ClientTask = new ClientService(ConnectionPool) ;
        
		ClientTask.Start() ;
    
		TcpListener listener = new TcpListener(portNum);
		try 
		{
			listener.Start();
        
			int TestingCycle = 3 ; // Number of testing cycles
			int ClientNbr = 0 ;
        
			// Start listening for connections.
			Console.WriteLine("Waiting for a connection...");
			while ( TestingCycle > 0 ) 
			{
                      
				TcpClient handler = listener.AcceptTcpClient();
                        
				if (  handler != null)  
				{
					Console.WriteLine("Client#{0} accepted!", ++ClientNbr) ;
                                
					// An incoming connection needs to be processed.
					ConnectionPool.Enqueue( new ClientHandler(handler) ) ;
                                
					//--TestingCycle ;
				}
				else 
					break;                
			}
			listener.Stop();
              
			// Stop client requests handling
			ClientTask.Stop() ;
              
              
		} 
		catch (Exception e) 
		{
			Console.WriteLine(e.ToString());
		}
        
		Console.WriteLine("\nHit enter to continue...");
		Console.Read();
	}

  
	public  static  int Main(String[] args) 
	{
		StartListening();
		
		return 0;
	}
}


class ClientHandler 
{
	SqlConnection cnn = new SqlConnection("server=don;database=master;integrated security=SSPI");
	string delimStr = ",";
	char [] delimiter;
	string [] split = null;
	string [] RFID = null;

	private TcpClient ClientSocket ;
	private NetworkStream networkStream ;
	bool ContinueProcess = false ;
	private byte[] bytes; 		// Data buffer for incoming data.
	private StringBuilder sb =  new StringBuilder(); // Received data string.
	private string data = null; // Incoming data from the client.

	public ClientHandler (TcpClient ClientSocket) 
	{
		ClientSocket.ReceiveTimeout = 100 ; // 100 miliseconds
		this.ClientSocket = ClientSocket ;
		networkStream = ClientSocket.GetStream();
		bytes = new byte[ClientSocket.ReceiveBufferSize];
		ContinueProcess = true ;
	}

	public  void Process() 
	{

		try 
		{
			int BytesRead = networkStream.Read(bytes, 0, (int) bytes.Length);
			if ( BytesRead > 0 ) 
				// There might be more data, so store the data received so far.
				sb.Append(Encoding.ASCII.GetString(bytes, 0, BytesRead));   
			else 
				// All the data has arrived; put it in response.
				ProcessDataReceived() ;
                                
		}                        
		catch  ( IOException ) 
		{ 
			// All the data has arrived; put it in response.
			ProcessDataReceived() ;
		}	  
		catch  ( SocketException ) 
		{
			networkStream.Close() ;
			ClientSocket.Close();			
			ContinueProcess = false ; 
			Console.WriteLine( "Conection is broken!");
		}                

	}  // Process()

	private void ProcessDataReceived() 
	{
		delimiter =  delimStr.ToCharArray();
		if ( sb.Length > 0 ) 
		{
			bool bQuit = ( String.Compare( sb.ToString(),  "quit", true ) == 0 ) ;

			data = sb.ToString() ;
                        			
			sb.Length  =  0 ; // Clear buffer

			Console.WriteLine( "Text received from client:") ;
			Console.WriteLine(data) ;
		
			StringBuilder response = new StringBuilder(  ) ;
			response.Append( "Received at " ) ;
			response.Append( DateTime.Now.ToString() ) ;
			response.Append( "\r\n" ) ;
			response.Append( data ) ;
string sql;
			
			if (!bQuit)
			{int y;
//				if (data.Substring(0,4) == "RFID")
				if (data.IndexOf("RFID") != -1)

				{
					split = data.Split(delimiter);

					for(y=0;y<split.Length;y++)
					{
						RFID = data.Split(delimiter,y);
						//Console.WriteLine("{0}",RFID[y]);
					}					

					Console.WriteLine("{0}",RFID[0]);
					Console.WriteLine("{0}",RFID[1]);
					Console.WriteLine("{0}",RFID[2]);
					Console.WriteLine("{0}",RFID[3]);

				
				SqlCommand InsertCommand = new SqlCommand();
				InsertCommand.Connection = cnn;
				cnn.Open();
						InsertCommand.Connection = cnn;
					
						sql = "INSERT INTO truck_geo VALUES (" + "'" + RFID[0] + "'"+ "," + "'" + RFID[1]+ "'" + "," + "'" + RFID[2]+ "'" + "," + "'" + System.DateTime.Parse(RFID[3]) + "')";
						InsertCommand.CommandText = sql;
						try
						{
							InsertCommand.ExecuteNonQuery();
							Console.WriteLine("Inserted Values");
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
						finally
						{}

		

					for(y=6;y<=RFID.Length;y++)
					{
						Console.WriteLine("{0}",RFID[y-1]);
						sql = "update rfid_info set flag_no=2 where rfid_no='"+RFID[y-1]+"'";
						InsertCommand.CommandText = sql;
						try
						{
							InsertCommand.ExecuteNonQuery();
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
						finally
						{}
					}	
							Console.WriteLine("Updated Values");
					cnn.Close();
				
				}
				else
				{
					for (int x = 1; x <= 4; x++) 
					{
						split = data.Split(delimiter, x);
						//	Console.WriteLine("\ncount = {0,2} ..............", x);
				
					}
					Console.WriteLine("{0}",split[0]);
					Console.WriteLine("{0}",split[1]);
					Console.WriteLine("{0}",split[2]);
					Console.WriteLine("{0}",split[3]);
			

					SqlCommand InsertCommand = new SqlCommand();
					InsertCommand.Connection = cnn;
		//			string sql;
					sql = "INSERT INTO truck_geo VALUES (" + "'" + split[0] + "'"+ "," + "'" + split[1]+ "'" + "," + "'" + split[2]+ "'" + "," + "'" + System.DateTime.Parse(split[3]) + "')";
					InsertCommand.CommandText = sql;
					try
					{
						cnn.Open();
						InsertCommand.ExecuteNonQuery();
						Console.WriteLine("Inserted Values");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
					finally
					{
						cnn.Close();
					}
				}
			/*	foreach (string s in split) 
				{
					Console.WriteLine("{0}", s);
				}*/
				networkStream.Close() ;
				ClientSocket.Close();	
				ContinueProcess = false ; 
			}
			// Echo the data back to the client.
		//	byte[] sendBytes = Encoding.ASCII.GetBytes(response.ToString());
		//	networkStream.Write(sendBytes, 0, sendBytes.Length);
		
			// Client stop processing  
			if ( bQuit  )  
			{
				networkStream.Close() ;
				ClientSocket.Close();	
				ContinueProcess = false ; 
			}
		}
	}        
        
	public void Close() 
	{
		networkStream.Close() ;
		ClientSocket.Close();        
	}
        
	public  bool Alive 
	{
		get 
		{
			return  ContinueProcess ;
		}
	}
        
} // class ClientHandler 

