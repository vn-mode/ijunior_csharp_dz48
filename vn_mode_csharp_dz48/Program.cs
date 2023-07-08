using System;
using System.Collections.Generic;

public class Fish
{
    private string _name;
    private int _age;

    public Fish(string name, int age)
    {
        _name = name;
        _age = age;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetAge()
    {
        return _age;
    }

    public void AgeIncrease()
    {
        _age++;
    }
}

public class Aquarium
{
    private const string FullAquariumMessage = "Аквариум полон, нельзя добавить еще одну рыбу.";
    private const string DeathMessageTemplate = "Рыба {0} умерла от старости.";
    private const string FishInfoTemplate = "Имя: {0}, Возраст: {1}";
    private const string FishRemovedMessageTemplate = "Рыба {0} была удалена из аквариума.";
    private const string FishNotFoundMessageTemplate = "Рыба с именем {0} не найдена в аквариуме.";

    private readonly int _capacity;
    private readonly int _deathAge;
    private List<Fish> _fishes = new List<Fish>();

    public Aquarium(int capacity, int deathAge)
    {
        _capacity = capacity;
        _deathAge = deathAge;
    }

    public void AddFish(Fish fish)
    {
        if (_fishes.Count < _capacity)
        {
            _fishes.Add(fish);
        }
        else
        {
            Console.WriteLine(FullAquariumMessage);
        }
    }

    public void RemoveFish(string name)
    {
        if (_fishes.RemoveAll(fish => fish.GetName() == name) > 0)
        {
            Console.WriteLine(string.Format(FishRemovedMessageTemplate, name));
        }
        else
        {
            Console.WriteLine(string.Format(FishNotFoundMessageTemplate, name));
        }
    }

    public void Live()
    {
        for (int i = _fishes.Count - 1; i >= 0; i--)
        {
            _fishes[i].AgeIncrease();

            if (_fishes[i].GetAge() > _deathAge)
            {
                Console.WriteLine(string.Format(DeathMessageTemplate, _fishes[i].GetName()));
                _fishes.RemoveAt(i);
            }
        }
    }

    public void ShowFishes()
    {
        foreach (var fish in _fishes)
        {
            Console.WriteLine(string.Format(FishInfoTemplate, fish.GetName(), fish.GetAge()));
        }
    }
}

class Program
{
    private const string CommandPromptMessage = "Введите команду (add, remove, show, quit):";
    private const string AddFishPromptMessage = "Введите имя рыбы: ";
    private const string RemoveFishPromptMessage = "Введите имя рыбы для удаления: ";
    private const string UnknownCommandMessage = "Неизвестная команда, попробуйте еще раз.";
    private const string AddCommand = "add";
    private const string RemoveCommand = "remove";
    private const string ShowCommand = "show";
    private const string QuitCommand = "quit";

    static void Main(string[] args)
    {
        var aquarium = new Aquarium(3, 10);

        while (true)
        {
            ExecuteCommand(aquarium);
            aquarium.Live();
        }
    }

    private static void ExecuteCommand(Aquarium aquarium)
    {
        Console.WriteLine(CommandPromptMessage);
        var command = Console.ReadLine().Trim().ToLower();

        switch (command)
        {
            case AddCommand:
                AddFish(aquarium);
                break;

            case RemoveCommand:
                RemoveFish(aquarium);
                break;

            case ShowCommand:
                aquarium.ShowFishes();
                break;

            case QuitCommand:
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine(UnknownCommandMessage);
                break;
        }
    }

    private static void AddFish(Aquarium aquarium)
    {
        Console.Write(AddFishPromptMessage);
        var name = Console.ReadLine();
        aquarium.AddFish(new Fish(name, 0));
    }

    private static void RemoveFish(Aquarium aquarium)
    {
        Console.Write(RemoveFishPromptMessage);
        var name = Console.ReadLine();
        aquarium.RemoveFish(name);
    }
}
