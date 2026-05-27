using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;

namespace BugTests
{
    [TestClass]
    public class BugWorkflowTests
    {
        private Bug _bug;

        [TestInitialize]
        public void Setup()
        {
            _bug = new Bug();
        }

        [TestMethod]
        public void NewBug_ShouldStartInCreatedState()
        {
            Assert.IsTrue(_bug.State == BugState.New, "Initial state must be New");
        }

        [TestMethod]
        public void Triage_ShouldBeReachableFromNew()
        {
            _bug.Fire(BugTrigger.StartTriage);
            Assert.AreEqual(BugState.Triage, _bug.State, "After StartTriage state should be Triage");
        }

        [TestMethod]
        public void NeedInfo_ShouldTransitionFromTriage()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.NeedMoreInfo);
            Assert.AreEqual(BugState.NeedInfo, _bug.State);
        }

        [TestMethod]
        public void NeedInfo_CanGoBackToTriage()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.NeedMoreInfo);
            _bug.Fire(BugTrigger.ReturnToTriage);
            Assert.AreEqual(BugState.Triage, _bug.State);
        }

        [TestMethod]
        public void AssignToDev_MovesToInProgress()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.AssignToDev);
            Assert.AreEqual(BugState.InProgress, _bug.State);
        }

        [TestMethod]
        public void Fix_ChangesStateFromInProgressToFixed()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.AssignToDev);
            _bug.Fire(BugTrigger.Fix);
            Assert.AreEqual(BugState.Fixed, _bug.State);
        }

        [TestMethod]
        public void Verify_ClosesFixedBug()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.AssignToDev);
            _bug.Fire(BugTrigger.Fix);
            _bug.Fire(BugTrigger.Verify);
            Assert.AreEqual(BugState.Closed, _bug.State);
        }

        [TestMethod]
        public void Reopen_ReturnsToReopenedState()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.AssignToDev);
            _bug.Fire(BugTrigger.Fix);
            _bug.Fire(BugTrigger.Reopen);
            Assert.AreEqual(BugState.Reopened, _bug.State);
        }

        [TestMethod]
        public void ReopenedBug_CanBeAssignedAgain()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.AssignToDev);
            _bug.Fire(BugTrigger.Fix);
            _bug.Fire(BugTrigger.Reopen);
            _bug.Fire(BugTrigger.AssignToDev);
            Assert.AreEqual(BugState.InProgress, _bug.State);
        }

        [TestMethod]
        public void Triage_MarkingNotABug_LeadsToNotABugState()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.MarkNotABug);
            Assert.AreEqual(BugState.NotABug, _bug.State);
        }

        [TestMethod]
        public void NotABug_CanBeClosed()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.MarkNotABug);
            _bug.Fire(BugTrigger.Close);
            Assert.AreEqual(BugState.Closed, _bug.State);
        }

        [TestMethod]
        public void Duplicate_MarkingWorks()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.MarkDuplicate);
            Assert.AreEqual(BugState.Duplicate, _bug.State);
        }

        [TestMethod]
        public void CannotReproduce_StateReachable()
        {
            _bug.Fire(BugTrigger.StartTriage);
            _bug.Fire(BugTrigger.MarkCannotReproduce);
            Assert.AreEqual(BugState.CannotReproduce, _bug.State);
        }
    }
}