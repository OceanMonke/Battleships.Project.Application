using System;

namespace Battleships.Project.Application
{
    public class Game
    {
        public static Player P1 { get; set; }
        public static Player P2 { get; set; }

        

        public static void PlayRound()
        {
            var gridCellRef = P1.Shoot();
            var result = P2.ProcessShot(gridCellRef);
            P1.ProcessShotResult(gridCellRef, result);

            if (!P2.Lost)
            {
                gridCellRef = P2.Shoot();
                result = P1.ProcessShot(gridCellRef);
                P2.ProcessShotResult(gridCellRef, result);
            }

            P1.GridOutput();
            P2.GridOutput();
        }

        public static void PlayGame()
        {
            while (!P1.Lost && !P2.Lost)
            {
                PlayRound();
            }

            if (P1.Lost)
            {
                Console.WriteLine(P2.Name + " has won!");
            }
            else if (P2.Lost)
            {
                Console.WriteLine(P1.Name + " has won!");
            }

            
        }

        public static void Match()
        {

            P1 = new("P1");
            P2 = new("P2");

            P1.ShipsPlacing();
            P2.ShipsPlacing();

            P1.ShowGrid();
            P2.ShowGrid();

            PlayGame();
        }
    }
}