// BugPro/Program.cs
using Stateless;

namespace BugPro;

public class Bug
{
    public enum State
    {
        NewDefect,
        Triage,
        NeedMoreInfo,
        NoTimeNow,
        SeparateSolution,
        ProblemAnotherProduct,
        Fixing,
        ProblemResolution,
        TestingByTester,
        TestingByProductTeam,
        TestingByDeveloper,
        Closed,
        Reopened,
        NotADefect,
        DontFix,
        Duplicate,
        NonReproducible
    }

    public enum Trigger
    {
        StartTriage,
        NeedMoreInfo,
        Ok,
        NoTimeNow,
        SeparateSolution,
        ProblemAnotherProduct,
        StartFix,
        ProblemResolved,
        TestByTester,
        TestByProductTeam,
        TestByDeveloper,
        Close,
        Reopen,
        NotADefect,
        DontFix,
        Duplicate,
        NonReproducible
    }

    private StateMachine<State, Trigger> _machine;

    public Bug()
    {
        _machine = new StateMachine<State, Trigger>(State.NewDefect);

        _machine.Configure(State.NewDefect)
            .Permit(Trigger.StartTriage, State.Triage);

        _machine.Configure(State.Triage)
            .Permit(Trigger.NeedMoreInfo, State.NeedMoreInfo)
            .Permit(Trigger.NoTimeNow, State.NoTimeNow)
            .Permit(Trigger.SeparateSolution, State.SeparateSolution)
            .Permit(Trigger.ProblemAnotherProduct, State.ProblemAnotherProduct)
            .Permit(Trigger.StartFix, State.Fixing)
            .Permit(Trigger.NotADefect, State.NotADefect)
            .Permit(Trigger.DontFix, State.DontFix)
            .Permit(Trigger.Duplicate, State.Duplicate)
            .Permit(Trigger.NonReproducible, State.NonReproducible);

        _machine.Configure(State.NeedMoreInfo)
            .Permit(Trigger.Ok, State.Triage);

        _machine.Configure(State.NoTimeNow)
            .Permit(Trigger.Ok, State.Triage);

        _machine.Configure(State.SeparateSolution)
            .Permit(Trigger.Ok, State.Triage);

        _machine.Configure(State.ProblemAnotherProduct)
            .Permit(Trigger.Ok, State.Triage);

        _machine.Configure(State.NotADefect)
            .Permit(Trigger.Ok, State.Closed);

        _machine.Configure(State.DontFix)
            .Permit(Trigger.Ok, State.Closed);

        _machine.Configure(State.Duplicate)
            .Permit(Trigger.Ok, State.Closed);

        _machine.Configure(State.NonReproducible)
            .Permit(Trigger.Ok, State.Closed);

        _machine.Configure(State.Fixing)
            .Permit(Trigger.ProblemResolved, State.ProblemResolution)
            .Permit(Trigger.TestByTester, State.TestingByTester)
            .Permit(Trigger.TestByProductTeam, State.TestingByProductTeam)
            .Permit(Trigger.TestByDeveloper, State.TestingByDeveloper);

        _machine.Configure(State.ProblemResolution)
            .Permit(Trigger.Ok, State.Triage);

        _machine.Configure(State.TestingByTester)
            .Permit(Trigger.Close, State.Closed)
            .Permit(Trigger.Reopen, State.Reopened);

        _machine.Configure(State.TestingByProductTeam)
            .Permit(Trigger.Close, State.Closed)
            .Permit(Trigger.Reopen, State.Reopened);

        _machine.Configure(State.TestingByDeveloper)
            .Permit(Trigger.Close, State.Closed)
            .Permit(Trigger.Reopen, State.Reopened);

        _machine.Configure(State.Closed)
            .Permit(Trigger.Reopen, State.Reopened);

        _machine.Configure(State.Reopened)
            .Permit(Trigger.StartFix, State.Fixing);
    }

    public State CurrentState => _machine.State;

    public void StartTriage() => _machine.Fire(Trigger.StartTriage);
    public void NeedMoreInfo() => _machine.Fire(Trigger.NeedMoreInfo);
    public void Ok() => _machine.Fire(Trigger.Ok);
    public void NoTimeNow() => _machine.Fire(Trigger.NoTimeNow);
    public void SeparateSolution() => _machine.Fire(Trigger.SeparateSolution);
    public void ProblemAnotherProduct() => _machine.Fire(Trigger.ProblemAnotherProduct);
    public void StartFix() => _machine.Fire(Trigger.StartFix);
    public void ProblemResolved() => _machine.Fire(Trigger.ProblemResolved);
    public void TestByTester() => _machine.Fire(Trigger.TestByTester);
    public void TestByProductTeam() => _machine.Fire(Trigger.TestByProductTeam);
    public void TestByDeveloper() => _machine.Fire(Trigger.TestByDeveloper);
    public void Close() => _machine.Fire(Trigger.Close);
    public void Reopen() => _machine.Fire(Trigger.Reopen);
    public void NotADefect() => _machine.Fire(Trigger.NotADefect);
    public void DontFix() => _machine.Fire(Trigger.DontFix);
    public void Duplicate() => _machine.Fire(Trigger.Duplicate);
    public void NonReproducible() => _machine.Fire(Trigger.NonReproducible);
}

class Program
{
    static void Main(string[] args)
    {
        Bug bug = new Bug();
        Console.WriteLine($"Текущее состояние: {bug.CurrentState}");
        bug.StartTriage();
        Console.WriteLine($"После StartTriage: {bug.CurrentState}");
        bug.StartFix();
        Console.WriteLine($"После StartFix: {bug.CurrentState}");
        bug.TestByTester();
        Console.WriteLine($"После TestByTester: {bug.CurrentState}");
        bug.Close();
        Console.WriteLine($"После Close: {bug.CurrentState}");
    }
}
