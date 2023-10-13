using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CardLib;

namespace DbLib;

[Table("Decks")]
public class DeckEntity
{
    [Key]
    public int Id { get; set; }

    public string Deck { get; set; }

}

[Table("Users")]
public class UserEntity
{
    [Key] 
    public int Id { get; set; }

    public string Name { get; set; }
}