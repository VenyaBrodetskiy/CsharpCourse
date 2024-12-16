
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

const string sourceFilePath = "test-video.mp4";
const string destinationFilePath = "test-video-copy.mp4";

// Read bytes from a video file using a stream
using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
{
    var buffer = new byte[64 * 8192];
    int bytesRead;
    long totalBytesCopied = 0;

    Console.WriteLine("Copying file in chunks...");

    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
    {
        destinationStream.Write(buffer, 0, bytesRead);
        totalBytesCopied += bytesRead;

        Console.WriteLine($"Copied {totalBytesCopied / (1024 * 1024)} MB...");
    }

    Console.WriteLine("File copy completed successfully.");
    Console.WriteLine($"Total size copied: {totalBytesCopied / (1024 * 1024 * 1024.0):F2} GB");
}

Console.WriteLine("Press any key to continue with simple copy");
Console.ReadKey();

Console.WriteLine("Copying file...");
File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
Console.WriteLine("File copy completed successfully.");

Console.WriteLine("Press any key to finish");
Console.ReadKey();