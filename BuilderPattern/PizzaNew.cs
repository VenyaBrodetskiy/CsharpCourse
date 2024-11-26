namespace BuilderPattern;

public class PizzaNew
{
    private string Size { get; set; }
    private string Crust { get; set; }
    private List<string> Toppings { get; set; } = [];

    private PizzaNew() { }

    public string CookPizza()
    {
        return $"Cooked Pizza of Size: {Size}, Crust: {Crust}, Toppings: {string.Join(", ", Toppings)}";
    }

    public class PizzaBuilder
    {
        private string _size = "Medium"; 
        private string _crust = "Regular";
        private readonly List<string> _toppings = [];

        public PizzaBuilder WithSize(string size)
        {
            _size = size;
            return this;
        }

        public PizzaBuilder WithCrust(string crust)
        {
            _crust = crust;
            return this;
        }

        public PizzaBuilder AddTopping(string topping)
        {
            _toppings.Add(topping);
            return this;
        }

        public Pizza Build()
        {
            if (_toppings.Count == 0)
                throw new ArgumentException("A pizza must have at least one topping.");

            return new Pizza(_size, _crust, _toppings);
        }
    }
}
