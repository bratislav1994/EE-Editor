using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Projekat
{
    public class Observer : IObserver
    {
        private Canvas canvas;
        private Database serializer = new Database();
        private Point p1;
        private bool t = true;
        private Canvas draggedCanvasNode = null;
        private Canvas draggedCanvasSub = null;
        private ConnectivityNode draggedItemCN = null;
        private Substation sub = null;
        private double x = -1; // ako se pokusa prevuci kanvas izvan granice, sa ovim poljima pamtim staru vrednost i vracam ga
        private double y = -1;
        private bool draggingNode = false;
        private bool draggingSub = false;
        private bool addNodeCommand = false;
        private MainWindow main;

        public Observer(Canvas canvas, MainWindow main)
        {
            this.canvas = canvas;
            this.main = main;
        }

        public void NotifyObservers()
        {
            canvas.Children.Clear();
            AddSubstationsOnCanvas();
            PrintLines();
        }

        private void AddSubstationsOnCanvas()
        {
            int countWidth = 5;
            int countTop = 5;
            bool emptyPlace = true;
            canvas.AddHandler(Canvas.DragOverEvent, new DragEventHandler(DragOverSubCanvas));
            canvas.AddHandler(Canvas.DropEvent, new DragEventHandler(DropSubCanvas));

            foreach (Substation s in Singleton.Instance().Substations)
            {
                Canvas c = new Canvas();
                c.Height = 180;
                c.Width = 180;
                c.Name = s.mRID;
                c.AllowDrop = true;
                c.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#555555"));
                c.AddHandler(Canvas.MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseLeftButtonDownSubCanvas));

                TextBox text = new TextBox();
                text.Text = "substation id: " + s.mRID;
                text.IsEnabled = false;
                text.Height = 20;
                text.Width = 180;
                text.Background = new SolidColorBrush(Colors.Black);
                text.Foreground = new SolidColorBrush(Colors.White);
                Canvas.SetLeft(text, 0);
                Canvas.SetBottom(text, 0);
                c.Children.Add(text);

                if (s.x != -1 && s.y != -1)
                {
                    Canvas.SetLeft(c, s.x);
                    Canvas.SetTop(c, s.y);
                    AddNodesOnCanvas(s, c);
                    canvas.Children.Add(c);
                }
                else
                {
                    bool taken = false;
                    countWidth = 5;
                    countTop = 5;

                    while (!taken)
                    {
                        foreach (Substation sub in Singleton.Instance().Substations)
                        {
                            if (!s.mRID.Equals(sub.mRID))
                            {
                                if (countWidth + 180 > canvas.Width && countTop + 180 > canvas.Height)
                                {
                                    taken = false;
                                    emptyPlace = false;
                                    break;
                                }
                                else if (countWidth + 180 > canvas.Width)
                                {
                                    countWidth = 5;
                                    countTop += 5;
                                }

                                if (TakenCanvas.Check(countWidth, countTop, sub.x, sub.y, 180))
                                {
                                    taken = true;
                                    countWidth += 5;
                                }
                            }
                        }

                        if (!taken && emptyPlace)
                        {
                            Canvas.SetLeft(c, countWidth);
                            Canvas.SetTop(c, countTop);
                            s.x = countWidth;
                            s.y = countTop;
                            AddNodesOnCanvas(s, c);
                            canvas.Children.Add(c);
                            taken = true; // now is taken
                            break;
                        }
                        else if (!taken)
                        {
                            taken = true;
                            emptyPlace = true;
                        }
                        else
                        {
                            taken = false; // new iteration
                        }

                    }
                }
            }
        }

        private void AddNodesOnCanvas(Substation s, Canvas c)
        {
            int countWidth = 3;
            int countTop = 3;
            bool emptyPlace = true;

            for (int i = 0; i < s.connectivityNodes.Count; i++)
            {
                Canvas b = new Canvas();
                b.Name = s.connectivityNodes[i].mRID;
                b.AddHandler(Canvas.MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseLeftButtonDownMiniCanvas));
                b.AddHandler(Canvas.DragOverEvent, new DragEventHandler(CanvasDragOver));
                b.AddHandler(Canvas.DropEvent, new DragEventHandler(CanvasDrop));
                b.Width = 55;
                b.Height = 55;
                b.Background = new SolidColorBrush(Colors.LightSkyBlue);

                TextBox text2 = new TextBox();
                text2.Text = "Node\n" + s.connectivityNodes[i].mRID;
                text2.IsEnabled = false;
                text2.Height = 30;
                text2.Width = 55;
                text2.Background = new SolidColorBrush(Colors.Black);
                text2.Foreground = new SolidColorBrush(Colors.White);
                text2.FontSize = 9;
                Canvas.SetLeft(text2, 0);
                Canvas.SetBottom(text2, 20);
                b.Children.Add(text2);

                if (s.connectivityNodes[i].x != -1 && s.connectivityNodes[i].y != -1)
                {
                    Canvas.SetLeft(b, s.connectivityNodes[i].x);
                    Canvas.SetTop(b, s.connectivityNodes[i].y);
                    c.Children.Add(b);
                }
                else
                {
                    bool taken = false;
                    countWidth = 3;
                    countTop = 3;

                    while (!taken)
                    {
                        foreach (ConnectivityNode cn in s.connectivityNodes)
                        {
                            if (!s.connectivityNodes[i].mRID.Equals(cn.mRID))
                            {
                                if (countWidth + 55 > c.Width && countTop + 78 > c.Height)
                                {
                                    taken = false;
                                    emptyPlace = false;
                                    break;
                                }
                                else if (countWidth + 55 > c.Width)
                                {
                                    countWidth = 3;
                                    countTop += 5;
                                }

                                if (TakenCanvas.Check(countWidth, countTop, cn.x, cn.y, 55))
                                {
                                    taken = true;
                                    countWidth += 5;
                                }
                            }
                        }

                        if (!taken && emptyPlace)
                        {
                            Canvas.SetLeft(b, countWidth);
                            Canvas.SetTop(b, countTop);
                            s.connectivityNodes[i].x = countWidth;
                            s.connectivityNodes[i].y = countTop;
                            c.AllowDrop = true;
                            c.Children.Add(b);
                            taken = true; // now is taken
                            break;
                        }
                        else if (!taken)
                        {
                            taken = true;
                            emptyPlace = true;
                        }
                        else
                        {
                            taken = false; // new iteration
                        }
                    }
                }
            }
        }

        private void PrintLines()
        {
            foreach (ACLineSegment ACLine in Singleton.Instance().Lines)
            {
                Line line = new Line();
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.DarkGray;
                line.StrokeThickness = 1;
                line.Stroke = brush;

                ConnectivityNode origin = null;
                ConnectivityNode destination = null;

                foreach (Terminal terminal in Singleton.Instance().Terminals)
                {
                    if (terminal.mRID.Contains(ACLine.mRID))
                    {
                        if (origin == null)
                        {
                            foreach (Substation s in Singleton.Instance().Substations)
                            {
                                foreach (ConnectivityNode cn in s.connectivityNodes)
                                {
                                    if (cn.mRID.Equals(terminal.ConnectivityNode.mRID))
                                    {
                                        origin = cn;
                                        origin.X = cn.x + s.x + 30;
                                        origin.Y = cn.y + s.y + 30;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (Substation s in Singleton.Instance().Substations)
                            {
                                foreach (ConnectivityNode cn in s.connectivityNodes)
                                {
                                    if (cn.mRID.Equals(terminal.ConnectivityNode.mRID))
                                    {
                                        destination = cn;
                                        destination.X = cn.x + s.x + 30;
                                        destination.Y = cn.y + s.y + 30;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                // Bind line.(X1, Y1) to origin
                BindingOperations.SetBinding(line, Line.X1Property, new Binding { Source = origin, Path = new PropertyPath("X") });
                BindingOperations.SetBinding(line, Line.Y1Property, new Binding { Source = origin, Path = new PropertyPath("Y") });
                // Bind line.(X2,Y2) to destination
                BindingOperations.SetBinding(line, Line.X2Property, new Binding { Source = destination, Path = new PropertyPath("X") });
                BindingOperations.SetBinding(line, Line.Y2Property, new Binding { Source = destination, Path = new PropertyPath("Y") });

                canvas.Children.Add(line);

            }
        }

        private void MouseLeftButtonDownSubCanvas(object sender, MouseButtonEventArgs e)
        {
            if (!draggingSub && draggedCanvasNode == null)
            {
                draggedCanvasSub = new Canvas();
                sub = new Substation();
                draggingSub = true;
                draggedCanvasSub = (Canvas)sender;

                foreach (Substation s in Singleton.Instance().Substations)
                {
                    if (s.mRID.Equals(draggedCanvasSub.Name))
                    {
                        sub = s;
                        x = s.x; // pamtim pocetnu vrednost, ako prevucem preko nekog drugog vracam ga na pocetnu poziciju
                        y = s.y;
                        break;
                    }
                }

                if (!addNodeCommand) // posle prevlacenja cvora, aktivira se ova f-ja, da ne bi pamtio komandu bez potrebe(sa pomeranjem 0 0) 
                { //nece biti izvrsena
                    DragDrop.DoDragDrop(main, draggedCanvasSub, DragDropEffects.All);
                }

                addNodeCommand = false;

                if (draggedCanvasSub != null) // znaci da smo ga prevukli izvan kanvasa, sad treba da vidimo na mestu gde je ostao
                { // da li se preklapa ili ne sa nekim
                    CheckSubCoordinates();
                }

                draggedCanvasSub = null;
                draggingSub = false;
            }
        }

        private void DragOverSubCanvas(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            Point p = e.GetPosition(canvas);
            if (sub != null)
            {
                if (t)
                {
                    t = false;
                    p1 = new Point();
                    p1 = e.GetPosition(draggedCanvasSub);
                }

                Canvas.SetLeft(draggedCanvasSub, p.X - p1.X);
                Canvas.SetTop(draggedCanvasSub, p.Y - p1.Y);
                sub.x = p.X - p1.X;
                sub.y = p.Y - p1.Y;

                if (p.Y - p1.Y >= 220)
                {
                    Canvas.SetTop(draggedCanvasSub, 220);
                    sub.y = 220;
                }

                if (p.Y - p1.Y <= 0)
                {
                    Canvas.SetTop(draggedCanvasSub, 0);
                    sub.y = 0;
                }

                if (p.X - p1.X >= 415)
                {
                    Canvas.SetLeft(draggedCanvasSub, 415);
                    sub.x = 415;
                }

                if (p.X - p1.X <= 0)
                {
                    Canvas.SetLeft(draggedCanvasSub, 0);
                    sub.x = 0;
                }

                foreach (ConnectivityNode cn in sub.connectivityNodes)
                {
                    cn.X = sub.x + cn.x + 30;
                    cn.Y = sub.y + cn.y + 30;
                }

                e.Handled = true;
            }
        }

        private void DropSubCanvas(object sender, DragEventArgs e)
        {
            //base.OnDrop(e);

            AddDropSubCommand adoc = new AddDropSubCommand(sub, x, y);
            Singleton.Instance().comInvoker.AddAndExecute(adoc);

            if (draggedCanvasSub != null)
            {
                if (((Canvas)sender).Resources["taken"] == null)
                {
                    ((Canvas)sender).Resources.Add("taken", true);
                    draggingSub = false;
                }
            }

            t = true;
            e.Handled = true;
            CheckSubCoordinates();

            sub = null;

            draggedCanvasSub = null;
            draggedCanvasNode = null;
        }

        private void CheckSubCoordinates()
        {
            bool isTaken = false;

            if (sub != null)
            {
                foreach (Substation s in Singleton.Instance().Substations)
                {
                    if (!s.mRID.Equals(sub.mRID))
                    {
                        if (TakenCanvas.Check(sub.x, sub.y, s.x, s.y, 180))
                        {
                            isTaken = true;
                        }
                    }
                }

                if (isTaken)
                {
                    sub.x = x;
                    sub.y = y;
                    Singleton.Instance().NotifyObservers();
                }
            }
        }

        private void MouseLeftButtonDownMiniCanvas(object sender, MouseButtonEventArgs e)
        {
            if (!draggingNode && !draggingSub)
            {
                sub = null;
                draggedCanvasNode = new Canvas();
                draggedItemCN = new ConnectivityNode();
                draggingNode = true;
                draggedCanvasNode = (Canvas)sender;
                Canvas canvas = ((Canvas)((sender as Canvas).Parent));

                foreach (ConnectivityNode n in Singleton.Instance().Nodes)
                {
                    if (n.mRID.Equals((sender as Canvas).Name))
                    {
                        foreach (Substation s in Singleton.Instance().Substations)
                        {
                            foreach (ConnectivityNode cn in s.connectivityNodes)
                            {
                                if (cn.mRID.Equals(n.mRID))
                                {
                                    draggedItemCN = cn;
                                    x = cn.x;
                                    y = cn.y;
                                    break;
                                }
                            }
                        }

                        break;
                    }
                }

                addNodeCommand = true;
                DragDrop.DoDragDrop(main, draggedCanvasNode, DragDropEffects.All);

                bool isTaken = false;

                if (draggedItemCN != null)
                {
                    foreach (Substation substation in Singleton.Instance().Substations)
                    {
                        if (substation.mRID.Equals(canvas.Name))
                        {
                            foreach (ConnectivityNode cn in substation.connectivityNodes)
                            {
                                if (!cn.mRID.Equals(draggedItemCN.mRID))
                                {
                                    if (TakenCanvas.Check(draggedItemCN.x, draggedItemCN.y, cn.x, cn.y, 55))
                                    {
                                        isTaken = true;
                                    }
                                }
                            }

                            if (isTaken)
                            {
                                draggedItemCN.x = x;
                                draggedItemCN.y = y;
                                Singleton.Instance().NotifyObservers();
                            }

                            break;
                        }
                    }
                }

                canvas.AllowDrop = true;
                draggedCanvasNode = null;
                draggingNode = false;
            }
        }

        private void CanvasDragOver(object sender, DragEventArgs e)
        {
            canvas.AllowDrop = false;
            e.Effects = DragDropEffects.Move;

            if (draggedCanvasNode != null)
            {
                Point p = e.GetPosition((Canvas)((sender as Canvas).Parent));

                if (t)
                {
                    t = false;
                    p1 = new Point();
                    p1 = e.GetPosition(draggedCanvasNode);
                }

                Canvas.SetLeft(draggedCanvasNode, p.X - p1.X);
                Canvas.SetTop(draggedCanvasNode, p.Y - p1.Y);
                draggedItemCN.x = p.X - p1.X;
                draggedItemCN.y = p.Y - p1.Y;

                if (p.Y - p1.Y >= 105)
                {
                    Canvas.SetTop(draggedCanvasNode, 105);
                    draggedItemCN.y = 105;
                }

                if (p.Y - p1.Y <= 0)
                {
                    Canvas.SetTop(draggedCanvasNode, 0);
                    draggedItemCN.y = 0;
                }

                if (p.X - p1.X >= 125)
                {
                    Canvas.SetLeft(draggedCanvasNode, 125);
                    draggedItemCN.x = 125;
                }

                if (p.X - p1.X <= 0)
                {
                    Canvas.SetLeft(draggedCanvasNode, 0);
                    draggedItemCN.x = 0;
                }

                Canvas c = (Canvas)((sender as Canvas).Parent);

                foreach (Substation s in Singleton.Instance().Substations)
                {
                    if (s.mRID.Equals(c.Name))
                    {
                        draggedItemCN.X = s.x + draggedItemCN.x + 30;
                        draggedItemCN.Y = s.y + draggedItemCN.y + 30;
                    }
                }

                e.Handled = true;
            }
            else
            {
                if (draggedItemCN != null && draggedCanvasSub == null)
                {
                    Canvas.SetLeft(draggedCanvasNode, x);
                    Canvas.SetTop(draggedCanvasNode, y);
                    draggedItemCN.x = x;
                    draggedItemCN.y = y;
                }
            }
        }

        private void CanvasDrop(object sender, DragEventArgs e)
        {
            canvas.AllowDrop = true;
            //base.OnDrop(e);

            AddDropNodeCommand adoc = new AddDropNodeCommand(draggedItemCN, x, y);
            Singleton.Instance().comInvoker.AddAndExecute(adoc);

            if (draggedCanvasNode != null)
            {
                if (((Canvas)sender).Resources["taken"] == null)
                {
                    ((Canvas)sender).Resources.Add("taken", true);
                    draggingNode = false;
                }
            }

            t = true;
            e.Handled = true;

            if (draggedItemCN != null)
            {
                bool isTaken = false;
                foreach (Substation substation in Singleton.Instance().Substations)
                {
                    foreach (ConnectivityNode cn in substation.connectivityNodes)
                    {
                        if (cn.mRID.Equals(draggedItemCN.mRID))
                        {
                            if (!cn.mRID.Equals(draggedItemCN.mRID))
                            {
                                if (TakenCanvas.Check(draggedItemCN.x, draggedItemCN.y, cn.x, cn.y, 55))
                                {
                                    isTaken = true;
                                }
                            }
                        }

                        if (isTaken)
                        {
                            draggedItemCN.x = x;
                            draggedItemCN.y = y;
                            Singleton.Instance().NotifyObservers();
                        }

                        break;
                    }
                }
            }

            CheckSubCoordinates();

            sub = null;

            draggedCanvasSub = null;
            draggedCanvasNode = null;
        }
    }
}
