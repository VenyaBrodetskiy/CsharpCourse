// See https://aka.ms/new-console-template for more information
using BuilderPattern;

var toppings = new List<string> { "Cheese", "Pepperoni", "Olives" };
var pizza = new Pizza("Large", "Thin", toppings);

var cookedPizza = pizza.CookPizza();
Console.WriteLine(cookedPizza);

var pizzaBuilder = new PizzaNew.PizzaBuilder();
var pizzaNew = pizzaBuilder
    .WithSize("Large")
    .WithCrust("Thin")
    .AddTopping("Cheese")
    .AddTopping("Pepperoni")
    .AddTopping("Olives")
    .Build();

var cookedPizzaNew = pizzaNew.CookPizza();
Console.WriteLine(cookedPizzaNew);