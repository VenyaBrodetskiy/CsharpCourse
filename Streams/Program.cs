
using System;
using System.IO.Compression;

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
const string destinationFilePath = "test-video-zip.mp4.gz";

// Read bytes from a video file using a stream
using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
using (var gzipStream = new GZipStream(destinationStream, CompressionMode.Compress))
{
    var buffer = new byte[16 * 1024 * 8192];
    int bytesRead;
    long totalBytesCompressed = 0;

    Console.WriteLine("Compressing file in chunks...");

    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
    {
        gzipStream.Write(buffer, 0, bytesRead);
        totalBytesCompressed += bytesRead;

        Console.WriteLine($"Compressed {totalBytesCompressed / (1024 * 1024)} MB...");
    }

    Console.WriteLine("File compression completed successfully.");
    Console.WriteLine($"Original size: {totalBytesCompressed / (1024 * 1024 * 1024.0):F2} GB");
    Console.WriteLine($"Compressed file saved as: {destinationFilePath}");
}

Console.WriteLine("Press any key to decompress and verify the file.");
Console.ReadKey();

// Decompress the file to verify correctness
const string decompressedFilePath = "test-video-decompressed.mp4";
using (var compressedStream = new FileStream(destinationFilePath, FileMode.Open, FileAccess.Read))
using (var decompressedStream = new FileStream(decompressedFilePath, FileMode.Create, FileAccess.Write))
using (var gzipDecompressStream = new GZipStream(compressedStream, CompressionMode.Decompress))
{
    var buffer = new byte[64 * 1024]; // 64 KB buffer for decompression
    int bytesRead;

    Console.WriteLine("Decompressing file...");

    while ((bytesRead = gzipDecompressStream.Read(buffer, 0, buffer.Length)) > 0)
    {
        decompressedStream.Write(buffer, 0, bytesRead);
    }

    Console.WriteLine($"Decompressed file saved as: {decompressedFilePath}");
}

Console.WriteLine("Press any key to finish.");
Console.ReadKey();


Console.WriteLine("Press any key to start");
Console.ReadKey();

const string destinationZipFilePath = "test-video-zip-2.zip";

// Create a proper .zip archive containing the source file
using (var zipArchive = ZipFile.Open(destinationZipFilePath, ZipArchiveMode.Create))
{
    Console.WriteLine($"Adding file {sourceFilePath} to the .zip archive...");
    zipArchive.CreateEntryFromFile(sourceFilePath, Path.GetFileName(sourceFilePath));
    Console.WriteLine("File successfully added to the .zip archive.");
}

Console.WriteLine($"Compressed file saved as: {destinationZipFilePath}");
Console.WriteLine("Press any key to finish");
Console.ReadKey();