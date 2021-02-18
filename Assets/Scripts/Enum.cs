using System;
using System.Collections;
using System.Collections.Generic;


public class EnumHolder
{
    [Flags]
    public enum EntityType
    {

        None = 0,
        Clear = 1,
        Self = 2,
        Character = 4,
        Door = 8, 
        EmployeeDoor = 16,
        Register = 32, 
        CookingStation = 64,
        Wall = 128,
        Supply = 256,
        Container = 512,
        Length = 1024
    }


    public enum Facing
    {
        Up,
        Down,
        Left,
        Right
    }
}
