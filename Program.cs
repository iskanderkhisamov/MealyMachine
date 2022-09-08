State ChangeState(State current, Input input) => (current, input) switch
{
    (State.закрыта, Input.Открыть) => State.открыта,
    (State.открыта, Input.Закрыть) => State.закрыта,
    (State.закрыта, Input.Запереть) => State.заперта,
    (State.заперта, Input.Отпереть) => State.закрыта,
    _ => current
};

var inputValues = Enum.GetValues(typeof(Input));
var stateValues = Enum.GetValues(typeof(State));

var random = new Random();
var randomState = (State)stateValues.GetValue(random.Next(stateValues.Length));

var current = randomState;

var count = 1;
var stats = stateValues.Cast<object?>().ToDictionary(stateValue => stateValue.ToString(), inputValue => 0);


while (count < 11)
{
    var randomInput = (Input)inputValues.GetValue(random.Next(inputValues.Length));
    stats[current.ToString()]++;
    Console.Write($"ИТЕРАЦИЯ: {count}; ВХОДНОЙ СИГНАЛ: {randomInput} дверь; ТЕКУЩЕЕ СОСТОЯНИЕ: дверь {current}; ");
    current = ChangeState(current, randomInput);
    Console.WriteLine($"ВЫХОДНОЙ СИГНАЛ: дверь {current}");
    count++;
}


Console.WriteLine($"\nСтатистика: всего смен состояний");
foreach (var keyValuePair in stats)
{
    float proc = (int)Math.Round((double)(100 * keyValuePair.Value) / count);
    Console.WriteLine($"Дверь {keyValuePair.Key}: {keyValuePair.Value} раз ({proc}% времени)");
}

// 3 состояния (m,p)
internal enum State { открыта, закрыта, заперта }

// 4 перехода (n)
internal enum Input { Открыть, Закрыть, Запереть, Отпереть }