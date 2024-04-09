namespace ControllingApi.Data;

public readonly struct Option<T>
{
    private readonly T _value;
    private readonly bool _hasValue;

    private Option(T value)
    {
        this._value = value;
        _hasValue = true;
    }

    public static Option<T> Some(T value)
    {
        return new Option<T>(value);
    }

    public static Option<T> None()
    {
        return new Option<T>();
    }

    public static Option<T> None<U>()
    {
        return new Option<T>();
    }

    public bool HasValue => _hasValue;

    public T Value
    {
        get
        {
            if (!_hasValue)
                throw new InvalidOperationException("Option does not have a value.");
            return _value;
        }
    }
}