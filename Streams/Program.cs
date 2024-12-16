
File.WriteAllText("file1.txt", "Hello, World");

var content = File.ReadAllText("file1.txt");

Console.WriteLine(content);

Console.WriteLine("Press any key to start");

Console.ReadKey(); 
var video = File.ReadAllBytes("test-video.mp4");

Console.WriteLine($"Read file with size: {(double)video.Length / 1024 / 1024 / 1024 :F2} GB");

Console.WriteLine("Press any key to finish");
Console.ReadKey();