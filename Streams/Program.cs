
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
long fileSizeInBytes;
using (var fileStream = new FileStream("test-video.mp4", FileMode.Open, FileAccess.Read))
{
    fileSizeInBytes = fileStream.Length;
    var buffer = new byte[fileStream.Length];
    var bytesRead = fileStream.Read(buffer, 0, buffer.Length);
    Console.WriteLine($"Read {bytesRead} bytes from the file");
}

Console.WriteLine($"Read file with size: {(double)fileSizeInBytes / 1024 / 1024 / 1024:F2} GB");

Console.WriteLine("Press any key to finish");
Console.ReadKey();