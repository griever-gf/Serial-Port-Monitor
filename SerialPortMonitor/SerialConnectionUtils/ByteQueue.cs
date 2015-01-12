using System;
using System.Collections.Generic;
using System.Text;

namespace SerialConnectionUtils
{
    /// <summary>
    /// Describes class for control of byte packets queues
    /// </summary>
    public class ByteQueue : IDisposable
    {
        /// <summary>
        /// Internal class buffer for storing byte packets
        /// </summary>
        private List<byte[]> Buffer = new List<byte[]>();

        /// <summary>
        /// Returns current length of the queue
        /// </summary>
        /// <returns>Length of byte queue</returns>
        public int Length()
        {
            int Result = 0;
            for (int i = 0; i < Buffer.Count; i++)
                Result += Buffer[i].Length;
            return Result;
        }

        /// <summary>
        /// Clearing queue
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Buffer.Count; i++)
                Buffer[i] = null;
            Buffer.Clear();
        }

        /// <summary>
        /// Getting element of the queue the set serial number
        /// </summary>
        /// <param name="Index">Serial number</param>
        /// <returns>Value of the element</returns>
        public byte this[int Index]
        {
            get
            {
                int i = 0;
                while (Index >= Buffer[i].Length)
                {
                    Index -= Buffer[i].Length;
                    i++;
                }
                return Buffer[i][Index];
            }
        }

        /// <summary>
        /// Adds new packet to the end of the queue
        /// </summary>
        /// <param name="Array">Packet in the form of array of bytes</param>
        public void Push(byte[] Array)
        {
            Buffer.Add(Array);
        }

        /// <summary>
        /// Adds new packet to the end of the queue
        /// </summary>
        /// <param name="Array">Packet in the form of array of bytes</param>
        /// <param name="Length">Number of elements, that needs to be added</param>
        public void Push(byte[] Array, int Length)
        {
            byte[] Temp = new byte[Length];
            System.Array.Copy(Array, Temp, Length);
            Buffer.Add(Temp);
        }

        /// <summary>
        /// Removes the stated number of elements from the start of the queue
        /// </summary>
        /// <param name="Length">Number of elements, that needs to be removed</param>
        public void Remove(int Length)
        {
            int Offset = 0;
            while (Length - Offset > Buffer[0].Length)
            {
                Offset += Buffer[0].Length;
                Buffer[0] = null;
                Buffer.RemoveAt(0);
            }
            if (Length - Offset > 0)
            {
                byte[] Temp = new byte[Buffer[0].Length - (Length - Offset)];
                Array.Copy(Buffer[0], Buffer[0].Length - Temp.Length, Temp, 0, Temp.Length);
                if (Temp.Length > 0)
                    Buffer[0] = Temp;
                else
                    Buffer.RemoveAt(0);
            }
        }

        /// <summary>
        /// Extracts the set number of elements from the start of the queue
        /// </summary>
        /// <param name="Length">Number of elements for extraction</param>
        /// <returns>Array byte, consisting of extracted elements</returns>
        public byte[] Pop(int Length)
        {
            byte[] Result = new byte[Length];
            int Offset = 0;
            while (Length - Offset > Buffer[0].Length)
            {
                Array.Copy(Buffer[0], 0, Result, Offset, Buffer[0].Length);
                Offset += Buffer[0].Length;
                Buffer[0] = null;
                Buffer.RemoveAt(0);
            }
            if (Length - Offset > 0)
            {
                Array.Copy(Buffer[0], 0, Result, Offset, Length - Offset);

                byte[] Temp = new byte[Buffer[0].Length - (Length - Offset)];
                Array.Copy(Buffer[0], Buffer[0].Length - Temp.Length, Temp, 0, Temp.Length);
                Buffer[0] = Temp;
            }
            return Result;
        }

        /// <summary>
        /// Extracts the set number of elements from the start of the queue, not removing them from the queue
        /// </summary>
        /// <param name="Length">Number of elements for extraction</param>
        /// <returns>Array byte, consisting of extracted elements</returns>
        public byte[] Peek(int Length)
        {
            byte[] Result = new byte[Length];
            int i = 0;
            int Offset = 0;
            while (Length - Offset > Buffer[i].Length)
            {
                Array.Copy(Buffer[i], 0, Result, Offset, Buffer[i].Length);
                Offset += Buffer[i].Length;
                i++;
            }
            if (Length - Offset > 0)
                Array.Copy(Buffer[i], 0, Result, Offset, Length - Offset);
            return Result;
        }

        /// <summary>
        /// Extracts, starting from the set serial number, frm the queue the set number of elements, not removing them from the queue
        /// </summary>
        /// <param name="Offset">Serial number of the element for extraction</param>
        /// <param name="Length">Number of elements for extraction</param>
        /// <returns>Array byte, consisting of extracted elements</returns>
        public byte[] Peek(int Offset, int Length)
        {
            byte[] Result = new byte[Length];
            int i = 0;
            while (Offset >= Buffer[i].Length)
            {
                Offset -= Buffer[i].Length;
                i++;
            }
            if (Offset + Length - 1 < Buffer[i].Length)
                Array.Copy(Buffer[i], Offset, Result, 0, Length);
            else
            {
                int ResultOffset = Buffer[i].Length - Offset;
                Array.Copy(Buffer[i], Offset, Result, 0, ResultOffset);
                i++;
                while (Length - ResultOffset > Buffer[i].Length)
                {
                    Array.Copy(Buffer[i], 0, Result, ResultOffset, Buffer[i].Length);
                    ResultOffset += Buffer[i].Length;
                    i++;
                }
                if (Length - ResultOffset > 0)
                    Array.Copy(Buffer[i], 0, Result, ResultOffset, Length - ResultOffset);
            }
            return Result;
        }

        /// <summary>
        /// Clears the queue and removes the copy of the class from memory
        /// </summary>
        public void Dispose()
        {
            Buffer.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
