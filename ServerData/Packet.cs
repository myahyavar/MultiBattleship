﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ServerData
{
    [Serializable]
    public class Packet
    {
        public List<string> data;
        public int packetInt;
        public bool packetBool = false;
        public string senderID;
        public char senderWho;
        public bool goodShoot;
        public string field;
        public PacketType packetType;

        public Packet(PacketType type, string senderID)
        {
            data = new List<string>();
            this.senderID = senderID;
            this.packetType = type;
        }

        public Packet(PacketType type, string senderID, char senderWho)
        {
            data = new List<string>();
            this.senderID = senderID;
            this.packetType = type;
            this.senderWho = senderWho;
        }

        public Packet(byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetBytes);

            Packet p = (Packet)bf.Deserialize(ms);
            ms.Close();
            this.data = p.data;
            this.packetInt = p.packetInt;
            this.packetBool = p.packetBool;
            this.senderID = p.senderID;
            this.packetType = p.packetType;
            this.senderWho = p.senderWho;
        }


        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, this);
            byte[] bytes = ms.ToArray();
            ms.Close();

            return bytes;
        }


        public static string GetIP4Adress()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

            return (from ip in ips
                    where ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                    select ip)
                   .FirstOrDefault()
                   .ToString();
        }
    }
}