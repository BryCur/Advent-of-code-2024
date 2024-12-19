using aocUtils;

namespace day_13;

public class ClawMachine
{
    private Coordinate2D buttonA;
    private Coordinate2D buttonB;
    private Coordinate2D target;

    private decimal clickA;
    private decimal clickB;

    public ClawMachine(Coordinate2D buttonA, Coordinate2D buttonB, Coordinate2D target)
    {
        this.buttonA = buttonA;
        this.buttonB = buttonB;
        this.target = target;

        GetSolution();
    }

    public bool IsTargetAccessible()
    {
        return decimal.IsInteger(clickA) && decimal.IsInteger(clickB);
    }

    public void GetSolution()
    {
        
        /*
         * solution for
         * c: coins for btnA
         * d: coins for btnB
         * Ax/Ay : offset on x/y axis of btn A
         * Bx/By : offset on x/y axis of btn B
         * Tx/Ty : x/y position of target
         *
         * 1) cAx + dBx = Tx
         * 2) cAy + dBy = Ty
         */
        clickB = (decimal)(target.getY() * buttonA.getX() - buttonA.getY() * target.getX()) / (buttonA.getX() * buttonB.getY() - buttonB.getX() * buttonA.getY());
        clickA = (target.getX() - clickB * buttonB.getX()) / buttonA.getX();
    }
    
    public (decimal ACoins, decimal BCoins) GetSolutionWithOffset(decimal offset)
    {
        decimal newXTarget = target.getX() + offset;
        decimal newYTarget = target.getY() + offset;

        decimal determinant = (buttonA.getX() * buttonB.getY()) - (buttonB.getX() * buttonA.getY());
        decimal aNum = (buttonB.getY() * newXTarget) - (newYTarget * buttonB.getX());
        decimal bNum = ((newYTarget * buttonA.getX()) - (buttonA.getY() * newXTarget));

        if (bNum % determinant is not 0 && aNum % determinant is not 0) return ((decimal)0.5, (decimal)0.5);
        decimal newClickB = bNum / determinant;
        decimal newClickA2 = aNum / determinant;
        
        return (newClickA2, newClickB);
    }
    
    public decimal GetClickA() => clickA;
    public decimal GetClickB() => clickB;
    
    public Coordinate2D GetButtonA() => buttonA;
    public Coordinate2D GetButtonB() => buttonB;
    public Coordinate2D GetTarget() => target;
}