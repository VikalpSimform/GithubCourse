public class SumCalculator
{
    public int Sum(int firstNumber, int secondNumber)
    {
        if (firstNumber == 0 || secondNumber == 0)
        {
            throw new ArgumentException("Both numbers must be non-zero.");
        }

        return firstNumber + secondNumber;
    }
}
