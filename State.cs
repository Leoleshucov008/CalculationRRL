using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationRRL
{
    public abstract class State
    {
        private State next;
        public State(State nextState)
        {
            next = nextState;
        }
        ~State()
        { }
        public abstract void doAfter();
        public abstract void doBefore();
        public State getNext()
        {
            return next;
        }
    }

    class InputIntervalParameters : State
    {
        private InterfaceManager manager;
        public InputIntervalParameters(State nextState, InterfaceManager manager)
            : base(nextState)
        {
            this.manager = manager;
        }
        override public void doAfter()
        {
            manager.resetGraph();
            manager.showGraph();
        }
        override public void doBefore()
        {
            
        }
    }

    class InputProfilePoints : State
    {
        private InterfaceManager manager;
        public InputProfilePoints(State nextState, InterfaceManager manager)
            : base(nextState)
        {
            this.manager = manager;
        }
        public override void doAfter()
        {
            CurveInfo c = manager.getCurveInfo("profile");
            c.isAllowEdit = false;
        }

        override public void doBefore()
        {
            
        }
    }

    class InputBarier : State
    {
        private InterfaceManager manager;
        public InputBarier(State nextState, InterfaceManager manager)
            : base(nextState)
        {
            this.manager = manager;
        }
        public override void doAfter()
        {
            CurveInfo c = manager.getCurveInfo("profile");
            c.isAllowEdit = false;
            
        }
        override public void doBefore()
        {
            manager.addNewBarier();
        }
    }

    class FinalState : State
    {
        private InterfaceManager manager;
        public FinalState(InterfaceManager manager) : base(null)
        {
            this.manager = manager; 
        }

        public override void doAfter()
        {
            //exit
        }
        override public void doBefore()
        {

        }
    }
}
