using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ_Semaphore_Pool
	{
	public partial class Form1: Form
		{
		private BindingList<ThreadWork> Sors1;
		private BindingList<ThreadWork> Sors2;
		private BindingList<ThreadWork> Sors3;
		private System.Windows.Forms.Timer timer;
		//private BindingSource Capacity;
		public int Id { get; set; }
		//Semaphore s;

		public Form1()
			{
			InitializeComponent();
			Sors1 = new BindingList<ThreadWork>();
			Sors2 = new BindingList<ThreadWork>();
			Sors3 = new BindingList<ThreadWork>();

			listBox1.DataSource = Sors1;
			listBox2.DataSource = Sors2;
			listBox3.DataSource = Sors3;

			timer = new System.Windows.Forms.Timer();
			timer.Interval = 300;
			timer.Tick += Timer_Tick;
			numericUpDown1.Value = 3;
			}


		private void Timer_Tick(object sender, EventArgs e)
			{
			Sors2.ResetBindings();
			Sors3.ResetBindings();
			if(Sors2.Count > 0)
				{
				foreach(var item in Sors2)
					{
					if(item.Status != "створено")
						{						
						Sors3.Add(item);
						Sors2.Remove(item);
						Sors2.ResetBindings();
						Sors3.ResetBindings();
						return;
						}
					}							
				}
			}
		private void Form1_Load(object sender, EventArgs e)
			{
			timer.Start();
			}

		private void button1_Click(object sender, EventArgs e)
			{
			ThreadWork threadWork = new ThreadWork(++Id, Convert.ToInt32(numericUpDown1.Value));
			numericUpDown1.Enabled= false;
			Sors1.Add(threadWork);
			Sors1.ResetBindings();
			}

		private void listBox1_DoubleClick(object sender, EventArgs e)
			{
			//if (s==null)
			//	s = new Semaphore(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown1.Value), "My_Semaphore");

			if(listBox1.SelectedItem != null)
				{
				ThreadWork threadWork = listBox1.SelectedItem as ThreadWork;
				if(threadWork.RunThread.ThreadState == System.Threading.ThreadState.Unstarted)
					{
					threadWork.RunThread.Start();
					}
				//else threadWork.RunThread.Resume();
				Sors1.Remove(threadWork);
				Sors2.Add(threadWork);
				}
			}	

		private void listBox3_DoubleClick(object sender, EventArgs e)
			{
			if(listBox3.SelectedItem != null)
				{
				ThreadWork threadWork = listBox3.SelectedItem as ThreadWork;
				threadWork.Stop = true;
				Sors3.Remove(threadWork);
				}
			}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
			{
			foreach(var item in Sors3)
				{
				item.Stop = true;
				}
			foreach(var item in Sors2)
				{
				item.Stop = true;
				}
			}
		}
	}
