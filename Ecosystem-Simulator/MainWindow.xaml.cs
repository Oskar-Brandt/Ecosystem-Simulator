﻿using Ecosystem_Simulator.Animals.MediumAnimals;
using Ecosystem_Simulator.Animals.SmallAnimals;
using Ecosystem_Simulator.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ecosystem_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CellGrid cellGrid;

        private Cell[,] cells;

        private State currentState;
        private List<State> states;

        private int numRows;
        private int numColumns;

        public MainWindow()
        {
            InitializeComponent();
            setGrid(25, 25, 55, 25, 25);
            states = new List<State>();

            beginNewState(cellGrid.InitState);
        }

        private void DrawCells()
        {
            CanvasGrid.Children.Clear();
            int rectSize = 22;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numColumns; col++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = rectSize,
                        Height = rectSize,
                        Stroke = Brushes.Gray
                    };

                    rect.Fill = Brushes.LightGreen;
                    Cell currentCell = cells[row, col];

                    if (currentCell.PlantInCell != null)
                    {
                        if(currentCell.PlantInCell is Dandelion)
                        {
                            rect.Fill = Brushes.Yellow;
                        }
                    }

                    if (currentCell.AnimalInCell != null)
                    {
                        if(currentCell.AnimalInCell is Rabbit)
                        {
                            rect.Fill = Brushes.LightGray;
                        }
                        else if(currentCell.AnimalInCell is Fox)
                        {
                            rect.Fill = Brushes.OrangeRed;
                        }
                    }


                    Canvas.SetLeft(rect, col * rectSize);
                    Canvas.SetTop(rect, row * rectSize);
                    CanvasGrid.Children.Add(rect);
                }
            }
        }

        private void setGrid(int gridHeight, int gridWidth, int initialRabbits, int initialFoxes, int initialDandelions)
        {
            cellGrid = new CellGrid(gridHeight, gridWidth, initialRabbits, initialFoxes, initialDandelions);
        }

        private void beginNewState(State newState)
        {

            currentState = newState;
            cells = newState.Cells;
            states.Add(newState);

            numRows = cells.GetLength(0);
            numColumns = cells.GetLength(1);

            DrawCells();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            State nextState = cellGrid.generateNextState(currentState);

            beginNewState(nextState);
        }
    }
}
