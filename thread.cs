using System;
using System.Threading;


namespace MultiThreadExercise
{
    class Lab7
    {
        static void Main(string[] args)
		{
			//Createing a Print object and three Game objects.
			Print pt = new Print();

			Game g1 = new Game("11", 300);
			Game g2 = new Game("22", 300);
			Game g3 = new Game("33", 300);
			
			//Creating as many threads as you like. Each is told to execute 
			//pt.PrintName method.
			Thread firstThread = new Thread(pt.PrintName);
			Thread secondThread = new Thread(pt.PrintName);
			Thread thirdThread = new Thread(pt.PrintName);
			
			//Kick off each thread and each will start doing whatever it was 
			//told to do.
			firstThread.Start(g1);
			secondThread.Start(g2);
			thirdThread.Start(g3);
		}
	}


	class Print
	{		
		public void PrintName(object cid)
		{
			/*
				The lock keyword below ensures that one thread does not  
				enter a critical section of code while another thread is 
				in that section. If another thread tries to enter a locked 
				code, it will wait, block, until the object is released.
				
				Without the lock, a thread can call and execute this PrintName
				even another thread is still executing it.
			*/
			
			//lock(this)     // "this" represents the current object of Counter.
			//{
				
				Game aGame = (Game)cid; //Type cast the input object back to a Game object.
				Console.WriteLine("\nThis is {0}\n", aGame.GameName);
				for(int i = aGame.TotalCount; i >= 0; i--)
				{
					Console.Write(" " + aGame.GameName + " ");
					Thread.Sleep(60);
				}
				Console.WriteLine("\nBYE, {0}.....\n", aGame.GameName);
			//}
		}
	}
	
	class Game
	{
		//Every Game object has two properties. The 'get' can read a property's 
		//value and 'get' can write data into a property.
		public string GameName { get; set; }
		public int TotalCount { get; set; }
		
		//Constructor method for anyone to new a Game object.
		public Game(string aName, int max)
		{
			this.GameName = aName;
			this.TotalCount = max;
		}
	}
}