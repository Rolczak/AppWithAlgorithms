using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace OiM_UWP.Views
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class HuffmanView : Page
    {
        int i = 0;
        public HuffmanView()
        {
            this.InitializeComponent();
        }
        private void calculate(object sender, RoutedEventArgs e)
        {
            List<HuffmanNode> nodeList = getList();
            HuffmanCoding huffmanCoding = new HuffmanCoding();
            huffmanCoding.getTreeFromList(nodeList);
            huffmanCoding.setCodeToTheTree("", nodeList[0]);
            while (nodeList.Count > 1)  
            {
                HuffmanNode node1 = nodeList[0];    
                nodeList.RemoveAt(0);             
                HuffmanNode node2 = nodeList[0];   
                nodeList.RemoveAt(0);  
                nodeList.Add(new HuffmanNode(node1, node2));   
                nodeList.Sort();
            }
            resultGrid.Children.Clear();
            i = 0;
            showTree(0, nodeList[0]);
            resultGrid.Width = i * 50;
            drawLines(nodeList[0]);

            treeScrollViewer.Visibility = Visibility.Visible;
            treeScrollViewer.Opacity = 0;
            treeScrollViewer.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
            treeScrollViewer.Opacity = 1;

            codingTable.Visibility = Visibility.Visible;
            codingTable.Opacity = 0;
            codingTable.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
            codingTable.Opacity = 1;

            codingTable.Children.Clear();
            showCodeInList(nodeList[0]);
        }

        private void showCodeInList(HuffmanNode node)
        {
            if(node.isLeaf)
            {
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Padding = new Thickness(2), Background = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorLight1"]), Margin= new Thickness(2,2,2,2) };
                TextBlock textBlock = new TextBlock() { Text = node.symbol+" = "+node.code,HorizontalAlignment= HorizontalAlignment.Center };
                stackPanel.Children.Add(textBlock);
                codingTable.Children.Add(stackPanel);
            }
            else
            {
                if (node.leftNode != null)
                    showCodeInList(node.leftNode);
                if (node.rightNode != null)
                    showCodeInList(node.rightNode);
            }
            
        }
        private List<HuffmanNode> getList()
        {
            try
            {
                List<HuffmanNode> nodeList = new List<HuffmanNode>();
                char[] text = messageTextBox.Text.ToCharArray();
                for (int i = 0; i < text.Length; i++)
                {
                    string read = text[i].ToString();
                    if (nodeList.Exists(x => x.symbol == read))
                        nodeList[nodeList.FindIndex(y => y.symbol == read)].frequencyIncrease();
                    else
                        nodeList.Add(new HuffmanNode(read));  
                }
                nodeList.Sort(); 
                return nodeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void showTree(int level, HuffmanNode node)
        {

            if (node.leftNode != null)
                showTree(level + 1, node.leftNode);
            TextBlock text = new TextBlock() { Text = node.symbol };
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Grid grid = new Grid() { Height = 50, Width = 50 };
            Ellipse ellipse = new Ellipse()
            {
                Stroke = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorLight3"]),
                StrokeThickness = 2,
                Fill = (Brush)Application.Current.Resources["AppBarBackgroundThemeBrush"]
            };
            
            grid.Children.Add(ellipse);
            grid.Children.Add(text);
            node.xCord = i * 50;
            node.yCord = level * 50;
           
            resultGrid.Children.Add(grid);
            Canvas.SetLeft(grid, node.xCord);
            Canvas.SetTop(grid, node.yCord);
            i++;
            if (node.rightNode != null)
                showTree(level + 1, node.rightNode);
        }

        private void drawLines(HuffmanNode  node)
        {
            if (node.leftNode != null)
            {
                Line line = new Line()
                {
                    X1 = node.xCord + 25,
                    Y1 = node.yCord + 25,
                    X2 = node.leftNode.xCord + 25,
                    Y2 = node.leftNode.yCord + 25,
                    Stroke = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorLight3"]),
                    StrokeThickness = 1,

                };
                resultGrid.Children.Add(line);
                TextBlock textBlock = new TextBlock() { Text = "0" };
                resultGrid.Children.Add(textBlock);
                Canvas.SetLeft(textBlock, ((line.X1 + line.X2) / 2) - 10);
                Canvas.SetTop(textBlock, ((line.Y1 + line.Y2) / 2)-25);
                Canvas.SetZIndex(line, -1);
                drawLines(node.leftNode);
            }
            if (node.rightNode != null)
            {
                Line line = new Line()
                {
                    X1 = node.xCord + 25,
                    Y1 = node.yCord + 25,
                    X2 = node.rightNode.xCord + 25,
                    Y2 = node.rightNode.yCord + 25,
                    Stroke = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorLight3"]),
                    StrokeThickness = 1,

                };
                resultGrid.Children.Add(line);
                TextBlock textBlock = new TextBlock() { Text = "1" };
                resultGrid.Children.Add(textBlock);
                Canvas.SetLeft(textBlock, ((line.X1 + line.X2)/2)+10);
                Canvas.SetTop(textBlock, ((line.Y1 + line.Y2) / 2)-25);
                Canvas.SetZIndex(line, -1);
                drawLines(node.rightNode);
            }

        }

        private int size(HuffmanNode node)
        {
            if (node == null)
                return 0;
            return 1 + size(node.leftNode) + size(node.rightNode);
        }

        private int height(HuffmanNode node)
        {
            if (node == null)
                return -1;
            return 1 + Math.Max(height(node.leftNode), height(node.rightNode));
        }

    }
}
