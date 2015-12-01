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
            manager.interval = new Interval(manager.R, manager.hMin, manager.hMax, manager.lambda, manager.zedGraphPane);
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
               
        }

        override public void doBefore()
        {
            manager.graphic = manager.interval.profile;    
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
            
        }
        override public void doBefore()
        {
                manager.interval.addBarier();
                manager.graphic = manager.interval.currentBarier;
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
