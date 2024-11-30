using NBomber.CSharp;
using NBomber.Http.CSharp;

var httpClient = HttpClientFactory.Create();

var controllerApiScenario = Scenario.Create("Controller API", async _ =>
{
    var request = Http.CreateRequest(
        HttpMethod.Get.ToString(), "http://localhost:5147/notify-mom/?type=push");

    var response = await Http.Send(httpClient, request);

    return response;
})
.WithWarmUpDuration(TimeSpan.FromSeconds(5))
.WithLoadSimulations(Simulation.KeepConstant(20, TimeSpan.FromSeconds(30)));

var minimalApiScenario = Scenario.Create("Minimal API", async _ =>
{
    var request = Http.CreateRequest(
        HttpMethod.Get.ToString(), "http://localhost:5000/notify-mom/?type=push");
    
    var response = await Http.Send(httpClient, request);
    
    return response;
})
.WithWarmUpDuration(TimeSpan.FromSeconds(5))
.WithLoadSimulations(Simulation.KeepConstant(20, TimeSpan.FromSeconds(30)));

NBomberRunner
    .RegisterScenarios(controllerApiScenario, minimalApiScenario)
    .Run();