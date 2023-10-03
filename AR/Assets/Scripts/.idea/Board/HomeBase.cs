using System;
using System.Collections;
using System.Collections.Generic;

namespace DefaultNamespace;

public class HomeBase
{
    private Team team;
    private int noOfPawns; //Do we want to store the pawns in here or just the number?
    
    public HomeBase() {}

    public void Leave(Pawn pawn)
    {
        noOfPawns--;
    }

    public void Return(Pawn pawn)
    {
        noOfPawns++;
    }
}