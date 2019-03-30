using System;

namespace MemoryLeak
{
    internal class IUseEvents
    {
        private IHaveEvents ItHasEvents;

        public string ID { get; set; }

        public IUseEvents(IHaveEvents ihe, string id)
        {
            ID = id;

            ItHasEvents = ihe;
            ItHasEvents.Razed += Razed;
        }

        public void Unsubscribe()
        {
            ItHasEvents.Razed -= Razed;
        }

        public void RaiseIt()
        {
            ItHasEvents.CauseToFire();
        }

        public void Razed(object sender, EventArgs e)
        {
            //Console.WriteLine($"{ID} noticed you had an event...");
        }
    }
}
