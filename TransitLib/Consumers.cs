using AbstractPlayer;
using MassTransit;

namespace ColiseumWebApp;

public class PickCardConsumer : IConsumer<PickCard>
{
    private Player _player;
    private IStorage _storage;

    public PickCardConsumer(Player player, IStorage storage)
    {
        _player = player;
        _storage = storage;
    }

    public Task Consume(ConsumeContext<PickCard> context)
    {
        var deck = context.Message.ToHalfDeck();
        var index = _player.PickCard(deck);

        _storage.SetDeck(deck);
        
        return context.Publish(new CardPicked
        {
            Index = index,
            Name = _player.Name
        }, context.CancellationToken);
    }
}

public class CardPickedConsumer : IConsumer<CardPicked>
{
    private Player _player;
    private IStorage _storage;
    
    public CardPickedConsumer(Player player, IStorage storage)
    {
        _player = player;
        _storage = storage;
    }
    
    public Task Consume(ConsumeContext<CardPicked> context)
    {
        _storage.SetIndex(context.Message.Index);
        
        return Task.CompletedTask;
    }
}

