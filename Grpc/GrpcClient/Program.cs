// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcTest;

var channel = GrpcChannel.ForAddress("https://localhost:7285");

// 透過 channel對遠端 grpc server發出 request
var client = new Greeter.GreeterClient(channel);
var reply = client.SayHello(new HelloRequest() // reply是強型別的 HelloReply
{
    Name = "Benson",
    Age = "5"
});

Console.WriteLine(reply.Message);
