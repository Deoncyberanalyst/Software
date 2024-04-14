namespace SimpleReactionMachine
{
    public interface INterface
    {
        void CoinInserted();
        void GoStopPressed();
        void Tick();
    }

    public class SimpleReactionController : IController
    {
        private INterface currentState;
        private IGui _igui;
        private IRandom _rng;
        private double timer;

        public SimpleReactionController()
        {
            currentState = new NoCoinState(this);
        }

        public void NextState(INterface state)
        {
            currentState = state;
        }

        public void Connect(IGui gui, IRandom rng)
        {
            _igui = gui;
            _rng = rng;
        }

        public void Init()
        {
            currentState = new NoCoinState(this);
            _igui.SetDisplay("Insert coin");
        }

        public void CoinInserted()
        {
            this.currentState.CoinInserted();
        }

        public void GoStopPressed()
        {
            this.currentState.GoStopPressed();
        }

        public void Tick()
        {
            this.currentState.Tick();
        }


        class NoCoinState : INterface
        {
            private SimpleReactionController _controller;

            public NoCoinState(SimpleReactionController _controller)
            {
                this._controller = _controller;
            }
            public void CoinInserted()
            {
                _controller.NextState(new HasCoinState(_controller));
                _controller._igui.SetDisplay("Press Go!");
            }
            public void GoStopPressed() { }
            public void Tick() { }
            public void Init() { }
        }


        class HasCoinState : INterface
        {
            private SimpleReactionController _controller;

            public HasCoinState(SimpleReactionController _controller)
            {
                this._controller = _controller;
            }
            public void CoinInserted() { }

            public void GoStopPressed()
            {
                _controller._igui.SetDisplay("Wait...");
                _controller.timer = _controller._rng.GetRandom(1, 3);
                _controller.NextState(new DelayStart(_controller));
            }

            public void Tick() { }
            public void Init()
            {
                _controller._igui.SetDisplay("Press GO!");
            }

        }

        class DelayStart : INterface
        {
            private SimpleReactionController _controller;

            public DelayStart(SimpleReactionController _controller)
            {
                this._controller = _controller;
            }

            public void CoinInserted() { }
            public void GoStopPressed()
            {
                _controller._igui.SetDisplay("Insert Coin");
                _controller.NextState(new NoCoinState(_controller));
            }

            public void Tick()
            {
                if (_controller.timer <= 0) _controller.NextState(new StartTimer(_controller));
                _controller.timer -= 0.01;
            }
        }

        class StartTimer : INterface
        {
            private SimpleReactionController _controller;

            public StartTimer(SimpleReactionController _controller)
            {
                this._controller = _controller;
            }

            public void CoinInserted() { }
            public void GoStopPressed()
            {
                _controller.NextState(new StopTimer(_controller));
                _controller.timer = 3;
            }

            public void Tick()
            {
                _controller._igui.SetDisplay(($"{Math.Round(_controller.timer, 2)}"));
                _controller.timer += 0.01;

                if (_controller.timer >= 2)
                {
                    _controller._igui.SetDisplay(($"{Math.Round(_controller.timer, 2)}"));
                    _controller.timer = 3;
                    _controller.NextState(new StopTimer(_controller));
                }
            }
        }

        class StopTimer : INterface
        {
            private SimpleReactionController _controller;

            public StopTimer(SimpleReactionController _controller)
            {
                this._controller = _controller;
            }
            public void CoinInserted()
            {
                _controller._igui.SetDisplay("Press Go!");
            }

            public void GoStopPressed()
            {
                _controller._igui.SetDisplay("Insert coin");
                _controller.NextState(new NoCoinState(_controller));
            }
            public void Tick()
            {
                if (_controller.timer <= 0)
                {
                    _controller._igui.SetDisplay("Insert coin");
                    _controller.NextState(new NoCoinState(_controller));
                }

                _controller.timer -= 0.01;
            }

        }
    }
}