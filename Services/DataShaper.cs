﻿using Entities.Models;
using Services.Contracts;
using System.Dynamic;
using System.Reflection;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T>
        where T : class
    {
        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            Properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string feildsString)
        {
            var requiredProperties = GetRequiredProperties(feildsString);
            return FetchData(entities, requiredProperties);
        }

        public ShapedEntity ShapeData(T entity, string feildsString)
        {
            var requiredProperties = GetRequiredProperties(feildsString);
            return FetchDataForEntity(entity, requiredProperties);
        } 

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFields = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(','
                    ,StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in fields)
                {
                    var property = Properties
                        .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                    if(property is null)
                        continue;
                    requiredFields.Add(property);
                }
            }
            else
            {
                requiredFields = Properties.ToList();
            }

            return requiredFields;
        }

        private ShapedEntity FetchDataForEntity(T entity,
            IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedOpject = new ShapedEntity();

            foreach(var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedOpject.Entity.TryAdd(property.Name, objectPropertyValue);
            }

            var objectProperty = entity.GetType().GetProperty("Id");
            shapedOpject.Id = (int)objectProperty.GetValue(entity);

            return shapedOpject;
        }

        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities,
            IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity>();
            foreach(var entity in entities)
            {
                var shapedObject = FetchDataForEntity (entity, requiredProperties);
                shapedData.Add(shapedObject);
            }

            return shapedData;
        }
    }
}
