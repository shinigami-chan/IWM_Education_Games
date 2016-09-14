using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum Action {

    //system_log_action and game_log_action from the database 'mathgames'
    [Id(1)]START_BALLOON_GAME,
    [Id(2)]START_NUMBER_LINE_GAME,
    [Id(3)]SHOW_STATISTICS
    //...
}

public static class EnumExtensions
{
    public static Id GetAttribute<Id>(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        return type.GetField(name) // I prefer to get attributes this way
            .GetCustomAttributes(false)
            .OfType<Id>()
            .SingleOrDefault();
    }
}

public class Id : Attribute {

    internal Id(int id)
    {
        this.id = id;
    }

    public double id { get; private set; }
}
