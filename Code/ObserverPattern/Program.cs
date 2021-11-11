using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    public enum OrderState
    {
        CRETAED,CONFIRMED,CANCELLED,CLOSED
    }
    
    public   class Order
    {
       public event Action<string> OrderStateChanged;//event
        // OrderClosed - Event
        string orderId;
        OrderState currentState;
        public Order()
        {
            orderId = Guid.NewGuid().ToString();
            currentState = OrderState.CRETAED;
        }
        public void ChangeState(OrderState newState)
        {
            this.currentState = newState;
            NotifyAll();
        }
        void NotifyAll()
        {
            if (OrderStateChanged != null)
            {
                this.OrderStateChanged.Invoke(this.orderId);//one->Many (Multicast Delegate Instance)
            }
        }

        ////Subscribe,Register
        //public void Add_OrderStateChanged(Action observerAddress)
        //{
        //    this.OrderStateChanged += observerAddress;//System.Delegate.Combine
        //}
        ////UnSubScribe
        //public void Remove_OrderStateChanged(Action observerAddress)
        //{
        //    this.OrderStateChanged -= observerAddress;//System.Delegate.Remove
        //}

    }

    public class AuditSystem
    {

    }
    public class EmailNotifificationSystem
    {
        public void SendMail(string evtData) { Console.WriteLine($"Email Sent  {evtData}"); }
    }
    public class SMSNotificationSystem
    {
        public void SendSMS(string evtData) {
            Console.WriteLine($"SMS Sent  {evtData}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmailNotifificationSystem _emailSystem = new EmailNotifificationSystem();
            SMSNotificationSystem _smsSystem = new SMSNotificationSystem();

            Action<string> _emailObserver = new Action<string>(_emailSystem.SendMail);

            Action<string> _smsObserver = new Action<string>(_smsSystem.SendSMS);

            Order _order1 = new Order();
            _order1.OrderStateChanged += _emailObserver;// Add_OrderStateChanged(_emailObserver)
            _order1.OrderStateChanged += _smsObserver;

            _order1.ChangeState(OrderState.CONFIRMED);
            System.Threading.Tasks.Task.Delay(1000).Wait();
            _order1.ChangeState(OrderState.CANCELLED);
            System.Threading.Tasks.Task.Delay(3000).Wait();
            _order1.ChangeState(OrderState.CONFIRMED);
            System.Threading.Tasks.Task.Delay(5000).Wait();
            _order1.ChangeState(OrderState.CLOSED);
        }
    }
}
