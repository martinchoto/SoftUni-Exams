
using System.Text;

namespace SoftUniKindergarten
{
    public class Kindergarten
    {
        public Kindergarten(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            Registry = new List<Child>();
        }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Child> Registry { get; set; }
        public bool AddChild(Child child)
        {
            if (Registry.Count < Capacity)
            {
                Registry.Add(child);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveChild(string childFullName)
        {
            string[] split = childFullName.Split(' ');
            string firstName = split[0];
            string lastName = split[1];
            var firstChildName = Registry.FirstOrDefault(x => x.FirstName == firstName);
            var secondChildName = Registry.FirstOrDefault(y => y.LastName == lastName);
            if (Registry.Contains(firstChildName) && Registry.Contains(secondChildName))
            {
                Registry.Remove(firstChildName);
                Registry.Remove(secondChildName);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int ChildrenCount { get { return Registry.Count; } }
        public Child GetChild(string childFullName)
        {
            string[] split = childFullName.Split(' ');
            string firstName = split[0];
            string lastName = split[1];
            var firstChildName = Registry.FirstOrDefault(x => x.FirstName == firstName);
            var secondChildName = Registry.FirstOrDefault(y => y.LastName == lastName);
            if (!Registry.Contains(firstChildName) && !Registry.Contains(secondChildName))
            {
                return null;
            }
            return firstChildName;
        }
        public string RegistryReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Registered children in {Name}:");
            var sortedList = Registry.OrderByDescending(x => x.Age)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.LastName).ToList();
            foreach (var child in sortedList)
            {
                sb.AppendLine(child.ToString());
            }
            return sb.ToString().Trim();
        }

    }
}