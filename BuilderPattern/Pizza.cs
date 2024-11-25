namespace BuilderPattern;
public class Pizza
{
    private string Size { get; set; }
    private string Crust { get; set; }
    private List<string> Toppings { get; set; }

    public Pizza(string size, string crust, List<string> toppings)
    {
        if (string.IsNullOrWhiteSpace(size))
            throw new ArgumentException("Size cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(crust))
            throw new ArgumentException("Crust cannot be null or empty.");

        if (toppings == null || toppings.Count == 0)
            throw new ArgumentException("A pizza must have at least one topping.");

        Size = size;
        Crust = crust;
        Toppings = toppings;
    }

    public string CookPizza()
    {
        return $"Cooked Pizza of Size: {Size}, Crust: {Crust}, Toppings: {string.Join(", ", Toppings)}";
    }
}

