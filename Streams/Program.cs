
using System.IO;

File.WriteAllText("file1.txt", "Hello, World");

var content = File.ReadAllText("file1.txt");

Console.WriteLine(content);

//Console.WriteLine("Press any key to start");

//Console.ReadKey(); 
//var video = File.ReadAllBytes("test-video.mp4");

//Console.WriteLine($"Read file with size: {(double)video.Length / 1024 / 1024 / 1024 :F2} GB");

//Console.WriteLine("Press any key to finish");
//Console.ReadKey();

Console.WriteLine("Press any key to start");
Console.ReadKey();

// Read bytes from a video file using a stream
using (var fileStream = new FileStream("test-video.mp4", FileMode.Open, FileAccess.Read))
{
    var buffer = new byte[8192];
    int bytesRead;
    long totalBytesRead = 0;

    Console.WriteLine("Reading file in chunks...");

    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
    {
        totalBytesRead += bytesRead;

        // Simulate processing the chunk of data
        Console.WriteLine($"Read {bytesRead} bytes, total: {totalBytesRead / (1024 * 1024)} MB...");
    }

    Console.WriteLine($"Finished reading the file. Total size: {totalBytesRead / (1024 * 1024 * 1024)} GB");
}

Console.WriteLine("Press any key to finish");
Console.ReadKey();