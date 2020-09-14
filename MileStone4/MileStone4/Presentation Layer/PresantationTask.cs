using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MileStone4.Interface_Layer;

namespace MileStone4.Presentation_Layer
{
    class PresantationTask
    {
        public bool visible { get; set; }

        public string title { get; set; }
        public string description { get; set; }


        private DateTime duedate;

        public DateTime dueDate
        {
            get { return duedate.Date; }
            set { duedate = value; }
        }


        public string columnName { get; set; }

        public int ID { get; set; }

        public PresantationTask(ILTask task)
        {
            this.title = task.title;
            this.description = task.description;
            this.dueDate = task.dueDate;
            this.columnName = task.columnName;
            this.ID = task.ID;
            this.visible = task.visible;
        }



        public WrapPanel toPanel()
        {
            WrapPanel ans = new WrapPanel();
            if (!this.visible)
                ans.Visibility = Visibility.Collapsed;
            ans.Orientation = Orientation.Horizontal;
            ans.DataContext = this;
            Border border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(1, 1, 1));
            Label l = new Label();

            Binding binding = new Binding("columnName");

            BindingOperations.SetBinding(l, Label.ContentProperty, binding);

            l.Width = 90;
            l.BorderThickness = new Thickness(1);
            l.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(1, 1, 1));
            ans.Children.Add(l);



            TextBlock t = new TextBlock();
            t.Width = 100;
            binding = new Binding("title");

            BindingOperations.SetBinding(t, TextBlock.TextProperty, binding);

            t.Margin = new Thickness(10);

            t.TextWrapping = TextWrapping.Wrap;
            border.Child = t;

            ans.Children.Add(border);

            border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(1, 1, 1));
            t = new TextBlock();
            t.Width = 300;
            binding = new Binding("description");

            BindingOperations.SetBinding(t, TextBlock.TextProperty, binding);

            t.Margin = new Thickness(10);

            t.TextWrapping = TextWrapping.Wrap;
            border.Child = t;

            ans.Children.Add(border);


            l = new Label();
            binding = new Binding("dueDate");

            BindingOperations.SetBinding(l, Label.ContentProperty, binding);
            l.Width = 80;
            l.BorderThickness = new Thickness(1);
            l.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(1, 1, 1));

            ans.Children.Add(l);



            border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(1, 1, 1));
            Button button = new Button();
        
            button.Content = "move task";
            button.DataContext = this;

            BoardGui temp = new BoardGui(false);
            button.Click += new RoutedEventHandler(temp.move_Click);
            temp.Close();
            border.Child = button;
            ans.Children.Add(border);


            return ans;
        }



        public void move_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(150, 20, 20));
             int id = ((PresantationTask)((Button)sender).DataContext).ID;
             InterfaceLayer.MoveToNext(id);
            ((Button)sender).Content = this.ID;

        }

    }
}
