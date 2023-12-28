using CardLib;
using DbLib;
using Microsoft.Extensions.Hosting;
using SandboxLib;

namespace ExperimentWorker;

public class Worker : IHostedService
{
    private const int IterCount = 100;
    private readonly Sandbox _sandbox;
    private readonly IReaderWriter _readerWriter;
    private readonly string _action;
    public Worker(Sandbox sandbox, IReaderWriter readerWriter, string action)
    {
        _sandbox = sandbox;
        _readerWriter = readerWriter;
        _action = action;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        int winCount = 0;

        if (_action.Equals("generate"))
        {
            Console.WriteLine("Generating results!");
            for (int i = 0; i < IterCount; i++)
            {
                if (_sandbox.DoOneExperiment(new Deck(), i).Result)
                    winCount++;
                
                Console.WriteLine("Experiments done: " + i);
            }
        }
        else
        {
            var decks = _readerWriter.ReadFromDatabase();
            foreach (var deck in decks)
            {
                if (_sandbox.DoOneExperimentNoShuffle(deck))
                    winCount++;
            }
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