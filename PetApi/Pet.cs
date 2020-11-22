using System;

namespace PetApi
{
    public class Pet
    {
        public Pet()
        {
        }
        
        public Pet(string name, string type, string color, int price)
        {
            Name = name;
            Type = type;
            Color = color;
            Price = price;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Pet)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Color, Price);
        }
        
        protected bool Equals(Pet other)
        {
            return Name == other.Name && Type == other.Type && Color == other.Color && Price == other.Price;
        }
    }
}