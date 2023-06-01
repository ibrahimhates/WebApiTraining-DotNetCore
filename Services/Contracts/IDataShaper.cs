using Entities.Models;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities,string feildsString);
        ShapedEntity ShapeData(T entities,string feildsString);
    }
}
