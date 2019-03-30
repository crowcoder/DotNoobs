using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace MemoryLeak
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;

            do
            {
                input = Console.ReadLine();

                switch (input)
                {
                    case "a":
                        Console.WriteLine("Hello");
                        break;
                    case "b":
                        #region instructions
                        /*
                         * Take snapshot.
                         * Enter "b", take snapshot when complete.
                         * Stop debugging, show no new MemoryStreams in memory.
                         * Run again, with break point at bottom of if block.
                         * Take snapshot and note the memorystream object.
                         * */
                        #endregion
                        WithoutDispose();
                        break;
                    case "c":
                        #region instructions
                        /*
                         * Take snapshot.
                         * Enter "c", take snapshot when complete.
                         * Stop debugging, show no new MemoryStreams in memory.
                         * Same result as not disposing!
                         * */
                        #endregion
                        WithDispose();
                        break;
                    case "d":
                        #region instructions
                        /*
                         * Take snapshot.
                         * Enter "d", take snapshot when complete.
                         * Enter "z" to force GC, note how Finalizer
                         * runs even though Dispose() is not called.
                         * */
                        #endregion
                        WithFinalizer();
                        break;
                    case "e":
                        #region instructions
                        /*
                         * Take snapshot.
                         * Enter "e", take snapshot when complete.
                         * Enter "z" to force GC, note how Process memory 
                         * does not go down despite GC.
                         * */
                        #endregion
                        AllocateUnmanaged();
                        break;
                    case "f":
                        #region instructions
                        /*
                         * Take snapshot.
                         * Enter "f", 
                         * GC a couple times.
                         * Take snapshot.
                         * Observe how IHaveEvents is still in memory and all
                         * 50 IUseEvents are still in memory.
                         * */
                        #endregion
                        IHaveEvents ihe = new IHaveEvents();
                        EventPileUp(ihe);
                        Console.WriteLine("Done");
                        break;

                    case "g":
                        /*
                         * Take snapshot.
                         * Enter "g". 
                         * Take snapshot.
                         * Observe how all IUseEvent objects are gone.
                         * */
                        IHaveEvents ihe2 = new IHaveEvents();
                        EventPileUp(ihe2);
                        ihe2.RemoveAllRazedEvents();
                        Console.WriteLine("Done");
                        break;
                    case "z":
                        GC.Collect();
                        break;
                    default:
                        break;
                }
            } while (input != "x");
        }

        private static void EventPileUp(IHaveEvents ihe)
        {
            for (int i = 0; i < 50; i++)
            {
                IUseEvents iue = new IUseEvents(ihe, i.ToString());
            }

            ihe.DisplayTargets();
        }

        //b
        static void WithoutDispose()
        {
            for (int i = 0; i < 1000; i++)
            {
                byte[] bytes = new byte[1048576];
                MemoryStream ms = new MemoryStream(bytes);
            }

            Console.WriteLine("Done");
        }

        //c
        static void WithDispose()
        {
            for (int i = 0; i < 1000; i++)
            {
                byte[] bytes = new byte[1048576];
                MemoryStream ms = new MemoryStream(bytes);
                ms.Dispose();
            }

            Console.WriteLine("Done");
        }

        //d
        static void WithFinalizer()
        {
            for (int i = 0; i < 10; i++)
            {
                FinalizerClass fc = new FinalizerClass();
            }

        }

        //e
        static void AllocateUnmanaged()
        {
            for (int i = 0; i < 10; i++)
            {
                IntPtr pointer = Marshal.AllocHGlobal(1048576);
                Thread.Sleep(500);
            }

            Console.WriteLine("Done");
        }
    }
}
