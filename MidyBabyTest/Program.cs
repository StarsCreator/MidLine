using System;
using System.IO.Ports;
using System.Threading;

namespace MidyBabyTest
{
  class Program
  {
    static void Main(string[] args)
    {

      var port = new SerialPort("COM7", 115200);
      port.DataReceived += Port_DataReceived;
      port.DtrEnable = true;
      //port.RtsEnable = true;
      port.Open();

      var buf = Console.Read();



      var bytes = new byte[]
      {
            0xA2, //global command
            0x00, //button num      | header
            0x00, //press event     |
            0x03, //events count    |
            0x01, // behavior id
            0xb0, // cc midi event
            0x02, // channel
            0x01, // cc num
            0x0F, // 127 val           
            0x02, // behavior id
            0xc0, // pc midi event
            0x03, // channel
            0x03, // pc num
            0xAA,  // not matter 
            0x01, // behavior id
            0xb0, // cc midi event
            0x01, // channel
            0x02, // cc num
            0x0A // 127 val
      };

      //var bytes = new byte[]
      //{
      //  0xA1,
      //  0x01,
      //  0x01,      //byte expPedalChannel1;
      //  0x01,      //byte expPedalControl1;
      //  0x01,      //byte expPedalChannel2;
      //  0x02,      //byte expPedalControl2;
      //  0x1e,      //byte buttonHoldTimeout;
      //  0x14       //byte buttonClickTimeout;
      //};


      //bool allowMidiUsb: 1;
      //bool allowMidiThru: 1;
      //bool allowExp1: 1;
      //bool allowExp2: 1;

      //var bytes = new byte[]
      //{
      //  0xA4,
      //  0x00
      //};

      port.Write(bytes,0,bytes.Length);

      while (buf != 'q')
      {

        buf = Console.Read();
        //port.Write(bytes, 0, bytes.Length);
        //Thread.Sleep(1000);
        
      }

    }

    private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      var sp = (SerialPort)sender;
      string indata = sp.ReadExisting();
      //Console.WriteLine("Data Received:");
      Console.Write(indata);
    }
  }
}
