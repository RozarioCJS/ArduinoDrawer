using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ArduinoDrawer
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports=SerialPort.GetPortNames();
            cmbPort.Items.AddRange(ports);
            cmbPort.SelectedIndex = 0;
            btnPortDisconnect.Enabled = false;
        }

        private void btnPortConnect_Click(object sender, EventArgs e)
        {
            btnPortConnect.Enabled = false;
            btnPortDisconnect.Enabled=true;

            try
            {
                serialPort1.PortName = cmbPort.Text;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                serialPort1.Open();

            }
            catch(Exception ex){
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPortDisconnect_Click(object sender, EventArgs e)
        {
            btnPortConnect.Enabled = true;
            btnPortDisconnect.Enabled = false;

            try
            {
                serialPort1.Close();
                txtMonitor.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // SwitchState, xAxis, yAxis in arduino code

            if (serialPort1.IsOpen)
            {
                string data = serialPort1.ReadLine(); // Serial.println() prints \r and \n at the end, Serial.println() does not
                string[] parts = data.Split(',');

               

                if (parts.Length == 3)
                {
                    string switchState = parts[0].Trim();
                    int xAxis = int.Parse(parts[1].Trim());
                    int yAxis = int.Parse(parts[2].Trim()); 

                    DrawPoint(xAxis, yAxis);
                }
                else
                {
                    MessageBox.Show("Invalid output format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void DrawPoint(int x, int y)
        {

            Graphics g = pictureBox1.CreateGraphics();


            g.DrawRectangle(Pens.Black, x, y, 1, 1);


            g.Dispose();
        }
    }
}
