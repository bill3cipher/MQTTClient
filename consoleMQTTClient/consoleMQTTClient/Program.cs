using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace consoleMQTTClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MqttClient client = new MqttClient("broker.hivemq.com");

            byte code = client.Connect("5730213017");

            ushort msgId = client.Subscribe(new string[] { "PSUParking", "Smarthome" },
                new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                             MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }

        private static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
            var message = Encoding.UTF8.GetString(e.Message);
            if (message.Contains(","))
            {
                string[] msgArr = message.Split(',');
                var sensorID = msgArr[0];
                var distance = msgArr[1];
               

                if(sensorID.Equals("S1") && int.Parse(distance) < 150)
                {
                    //Turn air confitioner ON
                    Console.WriteLine("Light - Red");

                }else
                {
                    //Turn air conditioner OFF
                    Console.WriteLine("Light - Green");
                }
            }

        }
    }
}
