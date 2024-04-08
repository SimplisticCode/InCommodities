
using System;
using System.Collections.Generic;
using System.Linq;

public struct Option<T>
{
    private readonly T value;
    private readonly bool hasValue;

    private Option(T value)
    {
        this.value = value;
        hasValue = true;
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

    public bool HasValue => hasValue;

    public T Value
    {
        get
        {
            if (!hasValue)
                throw new InvalidOperationException("Option does not have a value.");
            return value;
        }
    }
}