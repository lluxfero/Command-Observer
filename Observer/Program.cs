TemperatureSensor sensor = new();

TemperatureDisplay display1 = new("Дисплей 1");
TemperatureDisplay display2 = new("Дисплей 2");
TemperatureDisplay display3 = new("Дисплей 3");

sensor.Register(display1);
sensor.Register(display2);
sensor.Register(display3);

sensor.Temperature = 25.0f;
sensor.Temperature = 27.0f;
sensor.Temperature = 29.0f;
sensor.Temperature = 31.0f;
sensor.Temperature = 33.0f;

sensor.Unregister(display2);

Console.WriteLine();
sensor.Temperature = 35.0f;


//Дисплей 1: Текущая температура: 25°C
//Дисплей 2: Текущая температура: 25°C
//Дисплей 3: Текущая температура: 25°C
//Дисплей 1: Текущая температура: 27°C
//Дисплей 2: Текущая температура: 27°C
//Дисплей 3: Текущая температура: 27°C
//Дисплей 1: Текущая температура: 29°C
//Дисплей 2: Текущая температура: 29°C
//Дисплей 3: Текущая температура: 29°C
//Дисплей 1: Текущая температура: 31°C
//Дисплей 2: Текущая температура: 31°C
//Дисплей 3: Текущая температура: 31°C
//Дисплей 1: Текущая температура: 33°C
//Дисплей 2: Текущая температура: 33°C
//Дисплей 3: Текущая температура: 33°C

//Дисплей 1: Текущая температура: 35°C
//Дисплей 3: Текущая температура: 35°C

public interface ISubject
{
    void Register(IObserver observer);
    void Unregister(IObserver observer);
    void Notify();
}

public interface IObserver
{
    void Update(float temperature);
}

public class TemperatureSensor : ISubject
{
    private float temperature;
    private readonly List<IObserver> observers = new();

    public float Temperature
    {
        get { return temperature; }
        set
        {
            temperature = value;
            Notify();
        }
    }

    public void Register(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Unregister(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver observer in observers)
        {
            observer.Update(temperature);
        }
    }
}

public class TemperatureDisplay : IObserver
{
    private readonly string name;

    public TemperatureDisplay(string name)
    {
        this.name = name;
    }

    public void Update(float temperature)
    {
        Console.WriteLine($"{name}: Текущая температура: {temperature}°C");
    }
}