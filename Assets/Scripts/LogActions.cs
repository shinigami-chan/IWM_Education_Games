using UnityEngine;
using System.Collections;
using System;

public enum Action {

    //system_log_action and game_log_action from the database 'mathgames'
    [Id(1)]CHOOSE_GAME,
    [Id(2)]SHOW_STATISTICS
    //...
}

public class Id : Attribute {

    int id;
    internal Id(int id)
    {
        this.id = id;
    }
}
