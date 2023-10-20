using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CardLib;

namespace DbLib;

[Table("Decks")]
public class DeckEntity
{
    [Key]
    public int Id { get; set; }
    public List<CardEntity> Deck { get; set; }
}

[Table("Cards")]
public class CardEntity
{
    [Key] 
    public int Id { get; set; }
    public int Number { get; set; }
    public string Color { get; set; }
    
    [ForeignKey("Deck")]
    public int DeckId { get; set; }
    public DeckEntity Deck { get; set; }
}