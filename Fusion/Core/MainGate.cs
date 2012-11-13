using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Reflection;
using System.ServiceModel;
using CommonTypes;
using System.Threading;
using Implements;

namespace Core
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerSession)]
    public class MainGateService : IMainGate
    {
        private DateTime cTime = DateTime.Now;
        public bool Autentificate(string login, string password)
        {
            return true;
        }

        public void PushEvent(BaseEvent baseEvent)
        {
            try
            {
                if (cTime.Day != DateTime.Now.Day)
                {
                    cTime = DateTime.Now;
                    InstantLogger.log("", "To be continued...", InstantLogger.TypeMessage.important);
                    InstantLogger.LogFileInit();
                    InstantLogger.log("", "...Continuing", InstantLogger.TypeMessage.important);
                }
                baseEvent.Time = DateTime.Now;
                //if (Core.Instance.Module != null)
                //{
                //    Core.Instance.Module.PushEvent(baseEvent);
                //}
                //else
                //{
                //    InstantLogger.log(baseEvent.ToString(), "Fail to transfer to Core.Instance.Module",
                //                      InstantLogger.TypeMessage.error);
                //}
                if (PushEventToClients(baseEvent))
                {
                    InstantLogger.log("", "Done processing", InstantLogger.TypeMessage.normal);
                }
                else
                {
                    InstantLogger.log(baseEvent.ToString(), "No listeners for admission",
                                      InstantLogger.TypeMessage.error);
                }
            }
            catch (Exception e)
            {
                InstantLogger.log(string.Format("Exception:\n{0}\nProcessing:\n{1}", e, baseEvent.ToString()),
                                  "Exception caught while processing", InstantLogger.TypeMessage.death);
            }
            return;
        }

        #region Callback

        private static readonly List<IMainGateCallback> subscribers = new List<IMainGateCallback>();
        private class TaskInfo
        {
            public BaseEvent baseEvent;
            public IMainGateCallback callback;
            public TaskInfo(BaseEvent _baseEvent, IMainGateCallback _callback)
            {
                baseEvent = _baseEvent;
                callback = _callback;
            }
        }
        private static void OnEventTask(Object stateInfo)
        {
            try
            {
                TaskInfo ti = (TaskInfo) stateInfo;
                if (((ICommunicationObject)ti.callback).State == CommunicationState.Opened) ti.callback.OnEvent(ti.baseEvent);

            }
            catch (Exception e)
            {
                InstantLogger.err("pool exception:\n{0}", e.ToString());
            }
        }

        public bool PushEventToClients(BaseEvent baseEvent)
        {
            bool result = false;
            subscribers.ForEach(delegate(IMainGateCallback callback)
            {
                if (((ICommunicationObject)callback).State != CommunicationState.Opened)
                {
                    InstantLogger.log(
                        callback.ToString() + " is \"" +
                        ((ICommunicationObject)callback).State.ToString() + "\"",
                        "Dead callback is removed", InstantLogger.TypeMessage.warning);
                    subscribers.Remove(callback);
                }
            });
            try
            {
                bool firstLoop = true;
                foreach (var callback in subscribers)
                {
                    if (((ICommunicationObject) callback).State == CommunicationState.Opened)
                    {
                        TaskInfo ti = new TaskInfo(baseEvent, callback);
                        result = System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(OnEventTask), ti);
                        if (firstLoop)
                        {
                            InstantLogger.log(baseEvent.ToString(), "message is delivered -- OnEvent processing initiated",
                                              InstantLogger.TypeMessage.unimportant);
                            firstLoop = false;

                        }
                        //callback.OnEvent(baseEvent);
                        result = true;
                    }
                    else
                    {
                        InstantLogger.log(baseEvent.ToString(), "message is not delivered",
                                          InstantLogger.TypeMessage.important);
                        //subscribers.Remove(callback);
                    }
                }
            }
            catch (Exception e)
            {
                InstantLogger.err("source exception:\n{0}", e.ToString());
                result = false;
            }
            //subscribers.ForEach(delegate(IMainGateCallback callback)
            //{
            //    if (((ICommunicationObject)callback).State == CommunicationState.Opened)
            //    {
            //        InstantLogger.log(baseEvent.ToString(), "message is delivered", InstantLogger.TypeMessage.unimportant);
            //        callback.OnEvent(baseEvent);
            //        result = true;
            //    }
            //    else
            //    {
            //        InstantLogger.log(baseEvent.ToString(), "message is not delivered", InstantLogger.TypeMessage.important);
            //        //subscribers.Remove(callback);
            //    }

            //});
            
            return result;
        }

        public bool Subscribe()
        {
            try
            {
                IMainGateCallback chGate = OperationContext.Current.GetCallbackChannel<IMainGateCallback>();
                bool isFound = false;
                foreach (var callback in subscribers)
                {
                    if (callback == chGate)
                    {
                        isFound = true;
                    }
                }
                if (!isFound)
                {
                    InstantLogger.log(chGate.ToString(), "listener is subscribed", InstantLogger.TypeMessage.important);
                    subscribers.Add(chGate);
                }
                return true;
            }
            catch (Exception e)
            {
                InstantLogger.err("subscribe exception:\n{0}", e.ToString());
                return false;
            }
        }

        public bool Unsubscribe()
        {
            //try
            //{
            //    IMainGateCallback chGate = OperationContext.Current.GetCallbackChannel<IMainGateCallback>();
            //    for (int i = 0; i < subscribers.Count; i++)
            //    {
            //        var callback = subscribers[i];
            //        if (callback == chGate)
            //        {
            //            subscribers.Remove(callback);
            //            i--;
            //            InstantLogger.log(chGate.ToString(), "listener is unsubscribed",
            //                              InstantLogger.TypeMessage.important);
            //        }
            //    }
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    InstantLogger.err("unsubscribe exception:\n{0}", e.ToString());
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}