using aocUtils;

namespace day_14.inputs;

public class Robot(Coordinate2D startingPosition, Coordinate2D velocity)
{
    public Coordinate2D ComputePostitionAfterSeconds(int seconds)
    {
        return startingPosition + velocity * seconds;
    }
}