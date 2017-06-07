using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DZ_Semaphore_Pool
	{
	class ThreadWork: IComparable<ThreadWork>
		{
		public Thread RunThread;		
		public bool Stop { get; set; }
		public string Status { get; set; }
		public int CountSecond { get; set; }
		DateTime StartTime { get; set; }
		public int Id { get; set; }

		static Semaphore sem; 

		public ThreadWork(int id_, int count)
			{
			RunThread = new Thread(Run);
			//RunThread.IsBackground = true;
			
			Stop = true;
			Status = "створено";
			Id = id_;
			if(sem==null)
				{
				sem = new Semaphore(count, count);
				}			
			}
		 
		void Run()
			{
			Status = "чекає";
			sem.WaitOne();
			Stop = false;
			StartTime = DateTime.Now;
			
			//Id = Thread.CurrentThread.ManagedThreadId;
			do
				{
				TimeSpan tt = StartTime - DateTime.Now;				
				Status = $"{tt.Seconds}.{tt.Milliseconds}";					
				Thread.Sleep(333);
				} while(!Stop);
			sem.Release();
			}

		public int CompareTo(ThreadWork other)
			{
			if(other == null) return 1;
			return this.RunThread.ManagedThreadId.CompareTo(other.RunThread.ManagedThreadId);
			}

		public override bool Equals(object obj)
			{
			ThreadWork threadWork = obj as ThreadWork;
			if(threadWork == null) return false;
			if(obj == null) return false;
			return this.RunThread.ManagedThreadId == threadWork.RunThread.ManagedThreadId;
			}

		public override int GetHashCode()
			{
			return this.RunThread.ManagedThreadId.GetHashCode();
			}

		public override string ToString()
			{
			return $"Потік {Id.ToString()} --> {Status}";
			}
		}
	}
