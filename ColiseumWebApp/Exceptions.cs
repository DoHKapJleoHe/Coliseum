namespace ColiseumWebApp;

public class ExperimentNotFoundException : Exception
{
    public ExperimentNotFoundException() { }

    public ExperimentNotFoundException(string msg) : base(msg) { }
}