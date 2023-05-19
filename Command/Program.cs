Dogsitter owner = new();
owner.SetOnStart(new SimpleCommand("Сидеть!"));
Сynologist cynologist = new();
owner.SetOnFinish(new ComplexCommand(cynologist, "Рядом!"));

owner.WalkWithDog();


//Мы начинаем прогулку с собакой
//Собака выучила легкую команду: Сидеть!
//Бегаем с собакой
//"Рядом!" - сложная команда, поэтому обратимся к кинологу..
//Кинолог научил собаку команде: Рядом!
//Мы заканчиваем прогулку с собакой


public interface ICommand
{
    void Execute();
}

// Команда способна выполнять простые операции самостоятельно
class SimpleCommand : ICommand
{
    private string _teach = string.Empty;

    public SimpleCommand(string t)
    {
        this._teach = t;
    }

    public void Execute()
    {
        Console.WriteLine($"Собака выучила легкую команду: {this._teach}");
    }
}

// Команда делегирует более сложные операции другим объектам
class ComplexCommand : ICommand
{
    private Сynologist _cynologist;

    // Данные о контексте, необходимые для запуска методов получателя
    private string _teach;

    public ComplexCommand(Сynologist c, string t)
    {
        this._cynologist = c;
        this._teach = t;
    }

    public void Execute()
    {
        Console.WriteLine($"\"{this._teach}\" - сложная команда, поэтому обратимся к кинологу..");
        this._cynologist.TeachDog(this._teach);
    }
}

// Классы Получателей содержат некую важную бизнес-логику
class Сynologist
{
    public void TeachDog(string t)
    {
        Console.WriteLine($"Кинолог научил собаку команде: {t}");
    }
}

// Отправитель связан с одной или несколькими командами. Он отправляет запрос команде
class Dogsitter
{
    private ICommand _onStart;

    private ICommand _onFinish;

    // Инициализация команд
    public void SetOnStart(ICommand command)
    {
        this._onStart = command;
    }

    public void SetOnFinish(ICommand command)
    {
        this._onFinish = command;
    }

    // Отправитель не зависит от классов конкретных команд и получателей.
    // Отправитель передаёт запрос получателю косвенно, выполняя команду.
    public void WalkWithDog()
    {
        Console.WriteLine("Мы начинаем прогулку с собакой");

        if (this._onStart is ICommand)
        {
            this._onStart.Execute();
        }

        Console.WriteLine("Бегаем с собакой");

        if (this._onFinish is ICommand)
        {
            this._onFinish.Execute();
        }

        Console.WriteLine("Мы заканчиваем прогулку с собакой");
    }
}