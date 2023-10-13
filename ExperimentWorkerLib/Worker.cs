using CardLib;
using Microsoft.Extensions.Hosting;
using SandboxLib;

namespace ExperimentWorker;

public class Worker : IHostedService
{
    private const int IterCount = 1;
    private Sandbox _sandbox;

    public Worker(Sandbox sandbox)
    {
        _sandbox = sandbox;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        int winCount = 0;

        for (int i = 0; i < IterCount; i++)
        {
            if (_sandbox.DoOneExperiment(new Deck()))
                winCount++;
        }
        
        float res = ((float)winCount / IterCount) * 100;
        Console.WriteLine(res);
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}