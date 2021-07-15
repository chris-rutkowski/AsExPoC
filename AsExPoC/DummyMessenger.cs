using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsExPoC
{
    public class DummyMessenger
    {
        private readonly List<EventHandler<string>> _onMessageListeners;

        public DummyMessenger()
        {
            _onMessageListeners = new List<EventHandler<string>>();
        }

        public void Add(EventHandler<string> listener) => _onMessageListeners.Add(listener);

        public async void StartReceiving()
        {
            var messages = new List<string> { "hey", "you ok?", "I consdier quitting", "exit" };

            foreach (var message in messages)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                foreach (var listener in _onMessageListeners)
                {
                    listener.Invoke(this, message);
                }
            }
        }
    }
}
