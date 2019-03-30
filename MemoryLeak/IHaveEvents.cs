using System;
using System.Reflection;

namespace MemoryLeak
{
    internal class IHaveEvents
    {
        public event EventHandler Razed;

        public void DisplayTargets()
        {
            var targets = Razed?.GetInvocationList();
            foreach (var t in targets)
            {
                var tobj = t.Target as IUseEvents;
                tobj.RaiseIt();
                Console.WriteLine(tobj.ID);
            }
        }

        public void RemoveAllRazedEvents()
        {
            Type t = typeof(IHaveEvents);
            EventInfo evtinfo = t.GetEvent("Razed");

            var targets = Razed.GetInvocationList();
            foreach (var targ in targets)
            {
                Console.WriteLine("Found event registration");
                evtinfo.RemoveEventHandler(this, targ);
            }
        }

        protected virtual void OnRazed(EventArgs e)
        {
            Razed?.Invoke(this, e);
        }

        internal void CauseToFire()
        {
            OnRazed(EventArgs.Empty);
        }
    }
}
