using flat_space.CollisionStates;
using System.Timers;

namespace flat_space
{
    public class Blink : BaseCollision
    {
        private int _caseSwitch = 0;
        private int _counter = 0;
        private Timer _timer;
        private celestialbody _celestialbody;

        public Blink(IOnCollisionState context) : base(context)
        {
        }

        public override void OnCollision(celestialbody celestialbody)
        {
            this._celestialbody = celestialbody;
            _celestialbody.color = _celestialbody.oldColor;
            _timer = new Timer(2000);
            _timer.Elapsed += new ElapsedEventHandler(doBlink);
            _timer.Interval = 600;
            _timer.Start();
        }

        private void doBlink(object source, ElapsedEventArgs e)
        {
            _caseSwitch++;
            _counter++;
            switch (_caseSwitch)
            {
                case 1:
                    _celestialbody.color = "white";
                    break;
                case 2:
                    _celestialbody.color = _celestialbody.oldColor;
                    break;
            }

            if (_caseSwitch == 2) _caseSwitch = 0;
            if (_counter == 6)
            {
                _celestialbody.color = _celestialbody.oldColor;
                _timer.Stop();
                _counter = 0;
            }

        }
    }
}