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
    class ChangeBarier : State
    {
        private InterfaceManager manager;
        private int index;
        public ChangeBarier(State nextState, int index, InterfaceManager manager)
            : base(nextState)
        {
            this.index = index;
            this.manager = manager;
        }
        public override void doAfter()
        {
            if (manager.interval.currentBarier.points.Count < 3)
            {
                throw new BarierIsUncompleted("Завершите ввод текущего препятствия");
            }     
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
            if (manager.interval.currentBarier.points.Count < 3)
            {
                manager.bariersListBox.SelectedIndex = manager.interval.bariers.FindIndex(x => x == manager.interval.currentBarier);                
                throw new BarierIsUncompleted("Завершите ввод текущего препятствия");

            }
        }
        override public void doBefore()
        {
            manager.interval.addBarier();
            manager.graphic = manager.interval.currentBarier;
            manager.updateBarierListBox();
            manager.bariersListBox.SelectedIndex = manager.bariersListBox.Items.Count - 1; 
        }
    }

    class InputSyntheticBarier : State
    {
        private InterfaceManager manager;
        public InputSyntheticBarier(State nextState, InterfaceManager manager)
            : base(nextState)
        {
            this.manager = manager;
        }
        public override void doAfter()
        {
            if (manager.interval.currentBarier.points.Count < 2)
            {
                manager.bariersListBox.SelectedIndex = manager.interval.bariers.FindIndex(x => x == manager.interval.currentBarier);
                throw new BarierIsUncompleted("Завершите ввод текущего препятствия");

            }
        }
        override public void doBefore()
        {
            manager.interval.addBarier();
            manager.graphic = manager.interval.currentBarier;
            manager.updateBarierListBox();
            manager.bariersListBox.SelectedIndex = manager.bariersListBox.Items.Count - 1;
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
