using System.Collections.Concurrent;
using CardLib;

namespace ColiseumWebApp;

public interface IStorage
{
    public void SetDeck(Card[]? halfDeck);
    public Task<Color> GetColor();
    public void SetIndex(int index);
}

public class Storage : IStorage
{
    private Card[] _deck;
    private int _index;

    private bool _isCardNumberAvailable = false;
    private bool _isDeckAvailable = false;

    private TaskCompletionSource<bool> _deckCompletionSource = null!;
    private TaskCompletionSource<bool> _cardCompletionSource = null!;
    private object _deckLock = new();
    private object _indexLock = new();
    
    public void SetDeck(Card[]? halfDeck)
    {
        lock (_deckLock)
        { 
            _deck = halfDeck;
            if (!_isDeckAvailable)
            {
                _isDeckAvailable = true;
                _deckCompletionSource?.SetResult(true);
            }
        }
    }

    public async Task<Color> GetColor()
    {
        await WaitForCard();
        await WaitForDeck();

        _isCardNumberAvailable = false;
        _isDeckAvailable = false;
        _cardCompletionSource = null!;
        _deckCompletionSource = null!;

        return _deck[_index].Color;
    }

    public void SetIndex(int index)
    {
        lock (_indexLock)
        {
            _index = index;   
            if (!_isCardNumberAvailable)
            {
                _isCardNumberAvailable = true;
                _cardCompletionSource?.SetResult(true);
            }
        }
    }

    private Task WaitForCard()
    {
        lock (_indexLock)
        {
            if (_isCardNumberAvailable)
            {
                return Task.CompletedTask;
            }
                
            _cardCompletionSource = new TaskCompletionSource<bool>();
        }

        return _cardCompletionSource.Task;
    }

    private Task WaitForDeck()
    {
        lock (_deckLock)
        {
            if (_isDeckAvailable)
            {
                return Task.CompletedTask;
            }

            _deckCompletionSource = new TaskCompletionSource<bool>();
        }

        return _deckCompletionSource.Task;
    }
}