using System.Text.Json.Serialization;
using BLL.PlayerTypes;

namespace BLL.Contracts;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(HumanPlayer), "human")]
[JsonDerivedType(typeof(AiPlayer), "ai")]
public interface IPlayer
{
    public string Name { get; }
}