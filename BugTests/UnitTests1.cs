// BugTests/UnitTest1.cs
using BugPro;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BugTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestInitialState()
    {
        Bug bug = new Bug();
        Assert.AreEqual(Bug.State.NewDefect, bug.CurrentState);
    }

    [TestMethod]
    public void TestStartTriage()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        Assert.AreEqual(Bug.State.Triage, bug.CurrentState);
    }

    [TestMethod]
    public void TestNeedMoreInfo()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.NeedMoreInfo();
        Assert.AreEqual(Bug.State.NeedMoreInfo, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Triage, bug.CurrentState);
    }

    [TestMethod]
    public void TestNoTimeNow()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.NoTimeNow();
        Assert.AreEqual(Bug.State.NoTimeNow, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Triage, bug.CurrentState);
    }

    [TestMethod]
    public void TestSeparateSolution()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.SeparateSolution();
        Assert.AreEqual(Bug.State.SeparateSolution, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Triage, bug.CurrentState);
    }

    [TestMethod]
    public void TestProblemAnotherProduct()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.ProblemAnotherProduct();
        Assert.AreEqual(Bug.State.ProblemAnotherProduct, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Triage, bug.CurrentState);
    }

    [TestMethod]
    public void TestNotADefect()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.NotADefect();
        Assert.AreEqual(Bug.State.NotADefect, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Closed, bug.CurrentState);
    }

    [TestMethod]
    public void TestDontFix()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.DontFix();
        Assert.AreEqual(Bug.State.DontFix, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Closed, bug.CurrentState);
    }

    [TestMethod]
    public void TestDuplicate()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.Duplicate();
        Assert.AreEqual(Bug.State.Duplicate, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Closed, bug.CurrentState);
    }

    [TestMethod]
    public void TestNonReproducible()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.NonReproducible();
        Assert.AreEqual(Bug.State.NonReproducible, bug.CurrentState);
        bug.Ok();
        Assert.AreEqual(Bug.State.Closed, bug.CurrentState);
    }

    [TestMethod]
    public void TestFullWorkflow()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.StartFix();
        bug.TestByTester();
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.CurrentState);
    }

    [TestMethod]
    public void TestReopenWorkflow()
    {
        Bug bug = new Bug();
        bug.StartTriage();
        bug.StartFix();
        bug.TestByTester();
        bug.Reopen();
        Assert.AreEqual(Bug.State.Reopened, bug.CurrentState);
        bug.StartFix();
        bug.TestByProductTeam();
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.CurrentState);
    }
}