Queue<int> textile = new Queue<int>(Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));
Stack<int> medicaments = new Stack<int>(Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));
Dictionary<string, int> medicamentsAndCost = new Dictionary<string, int>()
{
    {"Patch", 30 }
    , {"Bandage", 40}
    ,{"MedKit", 100}
};
Dictionary<string, int> amountCreated = new Dictionary<string, int>();
while (textile.Count > 0 && medicaments.Count > 0)
{
    int firstText = textile.Peek();
    int lastMed = medicaments.Peek();
    int sum = firstText + lastMed;
    bool isMade = false;
    foreach (var item in medicamentsAndCost)
    {
        if (sum == item.Value)
        {
            if (!amountCreated.ContainsKey(item.Key))
            {
                amountCreated.Add(item.Key, 1);
            }
            else
            {
                amountCreated[item.Key]++;
            }
            textile.Dequeue();
            medicaments.Pop();
            isMade = true;
            break;
        }
    }
    if (!isMade)
    {
        if (sum >= 100)
        {
            if (!amountCreated.ContainsKey("MedKit"))
            {
                amountCreated.Add("MedKit", 1);
            }
            else
            {
                amountCreated["MedKit"]++;
            }
            int n = sum - 100;
            textile.Dequeue();
            medicaments.Pop();
            medicaments.Push(medicaments.Pop() + n);
        }
        else
        {
            textile.Dequeue();
            medicaments.Push(medicaments.Pop() + 10);
        }
    }
}
Console.WriteLine((textile.Count == 0 && medicaments.Count == 0) ? "Textiles and medicaments are both empty." :
                  (textile.Count == 0) ? "Textiles are empty." :
                  (medicaments.Count == 0) ? "Medicaments are empty." : "");
if (amountCreated.Count > 0)
{
    foreach (var item in amountCreated.OrderByDescending(x => x.Value)
        .ThenBy(x => x.Key))
    {
        Console.WriteLine($"{item.Key} - {item.Value}");
    }
}
Console.WriteLine(medicaments.Any() ? $"Medicaments left: {string.Join(", ", medicaments)}" :
                  textile.Any() ? $"Textiles left: {string.Join(", ", textile)}" : "");