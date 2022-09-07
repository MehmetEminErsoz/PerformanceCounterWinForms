using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//PerfCounter için gerekli kütüphane
using System.Diagnostics;

namespace PerformanceCounterWinForms
{
    public partial class Form1 : Form
    {
        public PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories();
        PerformanceCounter perfc = new PerformanceCounter();
        public Form1()
        {
            InitializeComponent();


            foreach (var item in categories)
            {
                listBox1.Items.Add(item.CategoryName);
                //listBox3.Items.Add(item.GetCounters());
            }




        }

        public  void CounterValue()
        {

            var x = categories.Where(s => s.CategoryName == listBox1.SelectedItem.ToString()).ToArray();
            listBox3.Items.Clear();
            listBox2.Items.Clear();
            foreach (var cat in x)
            {
                try
                {
                    var instances = cat.GetInstanceNames();
                    if (instances != null /*&& instances.Length > 0*/)
                    {
                        foreach (var instance in instances)
                        {

                            listBox2.Items.Add(instance);
                            foreach (var counter in cat.GetCounters(instance))
                            {
                                //MessageBox.Show("Test");
                                listBox3.Items.Add(counter.CounterName);
                            }
                        }
                    }
                    else
                    {
                        foreach (var counter in cat.GetCounters())
                        {
                            listBox3.Items.Add(counter.CounterName);
                            MessageBox.Show("Test");
                        }
                    }
                }
                catch (Exception)
                {
                    //return Exception;
                    MessageBox.Show("Hata");
                }
            }
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = listBox1.SelectedItem.ToString();
            CounterValue();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            
            if (listBox2.SelectedItem!=null && listBox3.SelectedItem!=null && listBox1.SelectedItem!=null)
            {
                PerformanceCounter PC = new PerformanceCounter(listBox1.SelectedItem.ToString(), listBox3.SelectedItem.ToString(), listBox2.SelectedItem.ToString());
                perfc = PC;
                getCounterValue(PC);
                timer1.Start();
                flag = true;
            }
            else
            {
                MessageBox.Show("Alanları doldurunuz.");
            }
        }
        bool flag = false;
        public string getCounterValue(PerformanceCounter pc)
        {
            
            string a = pc.NextValue().ToString();
            label3.Text = a;
            return a;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag==true)
            {
                label3.Text = TickValue();
            }
            
        }

        public string TickValue()
        {
            return perfc.NextValue().ToString();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
