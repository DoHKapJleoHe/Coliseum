using CardLib;
using DbLib;
using Microsoft.Extensions.Hosting;
using SandboxLib;

namespace ExperimentWorker;

public class Worker : IHostedService
{
    private const int IterCount = 100;
    private Sandbox _sandbox;
    private readonly IReaderWriter _readerWriter;
    public Worker(Sandbox sandbox, IReaderWriter readerWriter)
    {
        _sandbox = sandbox;
        _readerWriter = readerWriter;
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

        winCount = 0;
        var decks = _readerWriter.ReadFromDatabase();
        foreach (var deck in decks)
        {
            if (_sandbox.DoOneExperimentNoShuffle(deck))
                winCount++;
        }
        
        res = ((float)winCount / IterCount) * 100;
        Console.WriteLine(res);
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}