namespace Lift;

public class Dinglemouse
{
    public static Direction ElevatorMovementDirection = Direction.Up;
    public static int MaxCountOfPeople;
    public static List<int> PassengersDestinations = new();
    public static int CurrentFloor = 0;
    public static bool HasNotMovedAllPassengers = true;
    public static List<int> VisitedFloors = new();
    public static int[] TheLift(int[][] queues, int capacity)
    {
        MaxCountOfPeople = capacity;
        VisitedFloors.Add(0);

        while (HasNotMovedAllPassengers)
        {
            if (hasToStopAtFloor(ref queues))
            {
                VisitedFloors.Add(CurrentFloor);
                peopleExit();
                peopleEnter(ref queues);
            }
            moveElevator(ref queues);
            HasNotMovedAllPassengers = hasPeopleWaitingInQueues(ref queues);

        }
        VisitedFloors.Add(0);
        return VisitedFloors.ToArray();
    }
    private static void moveElevator(ref int[][] queues)
    {
        if (isAtTopFloor(ref queues) && ElevatorMovementDirection == Direction.Up)
        {
            ElevatorMovementDirection = Direction.Down;
            CurrentFloor--;
            return;
        }
        if (isAtGroundFloor(ref queues) && ElevatorMovementDirection == Direction.Down)
        {
            ElevatorMovementDirection = Direction.Up;
            CurrentFloor++;
            return;
        }
        if (ElevatorMovementDirection == Direction.Up)
        {
            CurrentFloor++;
            return;
        }
        CurrentFloor--;
    }
    private static bool isAtTopFloor(ref int[][] queues)
    {
        return CurrentFloor == queues.Count() - 1;
    }
    private static bool isAtGroundFloor(ref int[][] queues)
    {
        return CurrentFloor == 0;
    }
    private static void peopleEnter(ref int[][] queues)
    {
        if (PassengersDestinations.Count >= MaxCountOfPeople) return;
        int freeSpaces = MaxCountOfPeople - PassengersDestinations.Count;
        if (ElevatorMovementDirection == Direction.Up)
        {
            peopleEnterGoingUpwards(freeSpaces, ref queues);
            return;
        }
        peopleEnterGoingDownwards(freeSpaces, ref queues);
        return;
    }
    private static void peopleEnterGoingUpwards(int freeSpaces, ref int[][] queues)
    {
        var peopleWantingUpwards = queues[CurrentFloor].Where(x => x > CurrentFloor).ToList();
        if (freeSpaces >= peopleWantingUpwards.Count)
        {
            PassengersDestinations.AddRange(peopleWantingUpwards);
            queues[CurrentFloor] = queues[CurrentFloor].Where(x => !peopleWantingUpwards.Contains(x)).ToArray();
            return;
        }
        PassengersDestinations.AddRange(peopleWantingUpwards.Take(freeSpaces));
        queues[CurrentFloor].Where(x => x <= CurrentFloor).ToList().AddRange(peopleWantingUpwards.Skip(freeSpaces));
        queues[CurrentFloor] = queues[CurrentFloor].ToArray();
        return;
    }
    private static void peopleEnterGoingDownwards(int freeSpaces, ref int[][] queues)
    {
        var peopleWantingDownWards = queues[CurrentFloor].Where(x => x < CurrentFloor).ToList();
        if (freeSpaces >= peopleWantingDownWards.Count)
        {
            PassengersDestinations.AddRange(peopleWantingDownWards);
            queues[CurrentFloor] = queues[CurrentFloor].Where(x => !peopleWantingDownWards.Contains(x)).ToArray();
            return;
        }
        PassengersDestinations.AddRange(peopleWantingDownWards.Take(freeSpaces));
        queues[CurrentFloor].Where(x => x <= CurrentFloor).ToList().AddRange(peopleWantingDownWards.Skip(freeSpaces));
        queues[CurrentFloor] = queues[CurrentFloor].ToArray();
        return;
    }
    private static void peopleExit()
    {
        PassengersDestinations.RemoveAll(x => x == CurrentFloor);
    }
    private static bool hasPeopleWaitingInQueues(ref int[][] queues)
    {
        if (PassengersDestinations.Any()) return true;
        foreach (var queue in queues)
        {
            if (queue.Any())
                return true;
        }
        return false;
    }
    private static bool hasToStopAtFloor(ref int[][] queues)
    {
        if (PassengersDestinations.Any(x => x == CurrentFloor)) return true;
        if (Direction.Up == ElevatorMovementDirection)
            return queues[CurrentFloor].Any(x => x > CurrentFloor);
        return queues[CurrentFloor].Any(x => x < CurrentFloor);
    }
    public enum Direction
    {
        Up,
        Down
    }
}
