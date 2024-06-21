using System;
using System.Reflection;
using System.Security.Cryptography;

namespace Count_Min_Sketch
{
	public class CountMinSketch
	{
		private readonly int RowCount;
		private readonly int ColCount;
		private int[,] CountTable;
		private List<HashAlgorithm> HashFunctions;
		private BytesConverter bytesConverter;
		public CountMinSketch(int row, int col, List<HashAlgorithm> hashFunctions)
		{
			this.RowCount = row;
			this.ColCount = col;
			CountTable = new int[row, col];
			this.HashFunctions = hashFunctions;
            this.bytesConverter = new BytesConverter();
        }

		public void InsertData(object data)
		{
			int row = 0;
			foreach(int index in GetIndex(data))
			{
				this.CountTable[row, index] = this.CountTable[row, index] + 1;
				row++;
            }
		}

		public int GetCount(object key)
		{
            int row = 0;
			int min = Int32.MaxValue;

            foreach (int index in GetIndex(key))
            {
				min = Math.Min(min, this.CountTable[row, index]);
                row++;
            }

			return min;
        }

        private IEnumerable<int> GetIndex(object data)
        {
            for (int i = 0; i < this.RowCount; i++)
            {
                int index;

                try
                {
                    byte[] hash = this.ComputeHash(data, HashFunctions[i]);
                    long value = BitConverter.ToUInt32(hash);
                    index = (int)(value % this.ColCount);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    yield break;
                }
                yield return index;
            }
        }

        private byte[] ComputeHash(object data, HashAlgorithm hashFn)
		{
            byte[] bytes = this.bytesConverter.GetBytes(data);
            byte[] hash = hashFn.ComputeHash(bytes);

			return hash;
        }
	}
}

