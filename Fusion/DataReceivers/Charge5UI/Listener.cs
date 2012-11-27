using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using ConnectionProvider;
using Core;
using Converter;
using CommonTypes;
using ConnectionProvider.MainGate;
using Implements;
using Charge5Classes;
//using System.ServiceModel;
//using System.Windows.Forms;

namespace Charge5UI
{
    class Listener : IEventListener
    {
        public Listener()
        {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        

        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("Listener"))
            {
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("Charge5.PatternNames"))
                    {
                        l.msg(fxe.ToString());
                        var patternList = new List<string>();
                        try
                        {
                            foreach (var argument in fxe.Arguments)
                            {
                                patternList.Add((string)argument.Value);
                            }
                            Pointer.PMainWindow.Dispatcher.Invoke(new Action(delegate()
                            {
                                Pointer.PMainWindow.cbPattern.ItemsSource = patternList;
                            }));
                            if (Pointer.PPatternEditor != null)
                            {
                                Pointer.PPatternEditor.Dispatcher.Invoke(new Action(delegate()
                                {
                                    Pointer.PPatternEditor.lstPatterns.ItemsSource = patternList;
                                    Pointer.PPatternEditor.ConsolePush(fxe.ToString());
                                }));
                            }
                        }
                        catch (Exception e)
                        {
                            l.err("Charge5.PatternNames: \n{0}", e.ToString());
                        }
                    }   

                    if (fxe.Operation.StartsWith("Charge5.RespLoadPattern"))
                    {
                        l.msg(fxe.ToString());
                        try
                        {
                            if ((bool)fxe.Arguments["Loaded"])
                            {
                                Pointer.PMainWindow.Dispatcher.Invoke(new Action(delegate()
                                {
                                    Pointer.PMainWindow.lblPattern.Content = "паттерн успешно загружен";
                                    Pointer.PMainWindow.lblPattern.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                }));
                            }
                            else
                            {
                                Pointer.PMainWindow.Dispatcher.Invoke(new Action(delegate()
                                {
                                    Pointer.PMainWindow.lblPattern.Content = "Ошибка загрузки паттерна";
                                    Pointer.PMainWindow.lblPattern.Background = new SolidColorBrush(Color.FromArgb(127, 255, 0, 0));
                                }));
                            }
                        }
                        catch (Exception e)
                        {
                            l.err("Charge5.RespLoadPattern: \n{0}", e.ToString());
                        }
                    }

                    if (fxe.Operation.StartsWith("Charge5.ResultCalc"))
                    {
                        
                        l.msg(fxe.ToString());
                        try
                        {
                            Pointer.PMainWindow.Dispatcher.Invoke(new Action(delegate()
                            {
                                if ((bool)fxe.Arguments["IsFound"])
                                {
                                    Pointer.PMainWindow.lblMDlm.Content = fxe.Arguments["MDlm"].ToString();
                                    Pointer.PMainWindow.lblMDlms.Content = fxe.Arguments["MDlms"].ToString();
                                    Pointer.PMainWindow.lblMFom.Content = fxe.Arguments["MFom"].ToString();
                                    Pointer.PMainWindow.lblMHi.Content = fxe.Arguments["MHi"].ToString();
                                    Pointer.PMainWindow.lblMLi.Content = fxe.Arguments["MLi"].ToString();
                                    Pointer.PMainWindow.lblMSc.Content = fxe.Arguments["MSc"].ToString();
                                    Pointer.PMainWindow.lblStatus.Content = "Расчет выполнен ...";
                                    Pointer.PMainWindow.lblStatus.Background =
                                        new SolidColorBrush(Color.FromArgb(127, 0, 100, 0));
                                }
                                else
                                {
                                    Pointer.PMainWindow.lblStatus.Content = "Входные значения в таблицах не найдены";
                                    Pointer.PMainWindow.lblStatus.Background =
                                        new SolidColorBrush(Color.FromArgb(127, 255, 0, 0));
                                }
                            }));
                        
                        }
                        catch (Exception e)
                        {
                            l.err("Charge5.ResultCalc: \n{0}", e.ToString());
                        }
                    }

                    if (fxe.Operation.StartsWith("Charge5.Tables"))
                    {
                        l.msg(fxe.ToString());
                        var patternList = new List<string>();
                        try
                        {
                           if (Pointer.PPatternEditor != null)
                            {
                                Pointer.PPatternEditor.Dispatcher.Invoke(new Action(delegate()
                                {
                                    Pointer.PPatternEditor.ConsolePush(fxe.ToString());
                                    Pointer.PPatternEditor.StatusChange("Получен паттерн " + fxe.Operation.Split('.').Last());
                                }));
                            }
                        }
                        catch (Exception e)
                        {
                            l.err("Charge5.PatternNames: \n{0}", e.ToString());
                        }
                    }

                }
            }
        }
    }
}
