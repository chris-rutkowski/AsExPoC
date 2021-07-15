using System;
using System.Threading.Tasks;

namespace AsExPoC
{
    public class DummyService
    {
        private readonly DummyMessenger _dummyMessenger;

        public DummyService()
        {
            _dummyMessenger = new DummyMessenger();
        }

        // Tried: public void Run() => throw new ForcedExitException(); // Awesome! Exited with error code 8

        public async void Run()
        {
            // Tried: throw new ForcedExitException();
            // Terrible: Exited with error code 134, with the following stacktrace
            // Unhandled exception. AsExPoC.ForcedExitException: Exception of type 'AsExPoC.ForcedExitException' was thrown.
            // at AsExPoC.DummyService.TestExceptionSync() in / Users / chris / Developer / Projects / AsExPoC / AsExPoC / DummyService.cs:line 12
            // at AsExPoC.DummyService.Run() in / Users / chris / Developer / Projects / AsExPoC / AsExPoC / DummyService.cs:line 9
            // at System.Threading.Tasks.Task.<> c.< ThrowAsync > b__140_1(Object state)
            // at System.Threading.QueueUserWorkItemCallbackDefaultContext.Execute()
            // at System.Threading.ThreadPoolWorkQueue.Dispatch()
            // at System.Threading._ThreadPoolWaitCallback.PerformWaitCallback()

            _dummyMessenger.StartReceiving();

            // Bot below ends almost the same:

            // Terrible: Exited with error code 134, with the following stacktrace
            // Unhandled exception. AsExPoC.ForcedExitException: Exception of type 'AsExPoC.ForcedExitException' was thrown.
            // at AsExPoC.DummyService.<>c.<Run>b__2_0(Object _, String message) in /Users/chris/Developer/Projects/AsExPoC/AsExPoC/DummyService.cs:line 36
            // at AsExPoC.DummyMessenger.StartReceiving() in /Users/chris/Developer/Projects/AsExPoC/AsExPoC/DummyMessenger.cs:line 27
            // at System.Threading.Tasks.Task.<>c.<ThrowAsync>b__140_1(Object state)
            // at System.Threading.QueueUserWorkItemCallbackDefaultContext.Execute()
            // at System.Threading.ThreadPoolWorkQueue.Dispatch()
            // at System.Threading._ThreadPoolWaitCallback.PerformWaitCallback() 

            _dummyMessenger.Add((_, message) => {
                Console.WriteLine($"received message {message}");

                if (message == "exit") 
                    throw new ForcedExitException();
            });

            _dummyMessenger.Add(async (_, message) => {
                Console.WriteLine($"received message {message}");
                await Task.Delay(50); // simulate some asynchronous message handlers 

                if (message == "exit") 
                    throw new ForcedExitException();
            });
        }

        // Tried: calling simply from Main
        //public void TestExceptionSync() => throw new ForcedExitException();
    }
}
