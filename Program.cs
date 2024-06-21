using System.Security.Cryptography;
using Count_Min_Sketch;
using Murmur;

Console.WriteLine("Hello, World!");

List<HashAlgorithm> algorithms = new List<HashAlgorithm>()
{
    MurmurHash.Create128(),
    MD5.Create(),
    SHA256.Create()
};

CountMinSketch CountMin = new CountMinSketch(algorithms.Count, 1024, algorithms);

Random rand = new Random();

int[] actualFreq = new int[100];

for(int i = 0; i < 1000000; i++)
{
    int randomInt = rand.Next(actualFreq.Length);
    CountMin.InsertData(randomInt);
    actualFreq[randomInt] += 1;
}

for(int i =0; i < actualFreq.Length; i++)
{
    var count = CountMin.GetCount(i);
    var error = 1 - (count / actualFreq[i]);
    Console.WriteLine($"Data: ${i} >>> Actual Freq {actualFreq[i]} ===> Count Min {count} ==> Error {error}");
}