using aocUtils;

namespace day_14.inputs;

public class Robot
{
    private Coordinate2D startingPosition;
    private Coordinate2D velocity;

    public Robot(Coordinate2D startingPosition, Coordinate2D velocity)
    {
        this.startingPosition = startingPosition;
        this.velocity = velocity;
    }
    
    public Coordinate2D ComputePostitionAfterSeconds(int seconds)
    {
        return startingPosition + (velocity * seconds);
    }
}