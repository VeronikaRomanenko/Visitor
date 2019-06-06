using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Building building = new Building();
            building.AddFloor(new Floor() { Number = 1 });
            building.AddFloor(new Floor() { Number = 2 });
            building.floors[0].AddRoom(new Room() { Number = 1 });
            building.floors[0].AddRoom(new Room() { Number = 2 });
            building.floors[0].AddRoom(new Room() { Number = 3 });
            building.floors[0].AddRoom(new Room() { Number = 4 });
            building.floors[1].AddRoom(new Room() { Number = 1 });
            building.floors[1].AddRoom(new Room() { Number = 2 });
            building.floors[1].AddRoom(new Room() { Number = 3 });
            building.floors[1].AddRoom(new Room() { Number = 4 });
            building.Accept(new Electrician());
        }
    }
    interface IElement
    {
        bool checkElectricity();
        void Accept(IVisitor visitor);
    }
    class Room : IElement
    {
        public int Number { set; get; }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitRoom(this);
        }

        public bool checkElectricity()
        {
            int n = new Random().Next(10);
            Thread.Sleep(50);
            return n > 6 ? true : false;
        }
    }
    class Floor : IElement
    {
        public int Number { set; get; }
        public List<Room> rooms { get; private set; } = new List<Room>();
        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        public bool checkElectricity()
        {
            int n = new Random().Next(10);
            Thread.Sleep(50);
            return n > 6 ? true : false;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitFloor(this);
        }
    }
    class Building : IElement
    {
        public List<Floor> floors { get; private set; } = new List<Floor>();

        public void Accept(IVisitor visitor)
        {
            visitor.VisitBuilding(this);
        }

        public void AddFloor(Floor floor)
        {
            floors.Add(floor);
        }

        public bool checkElectricity()
        {
            int n = new Random().Next(10);
            Thread.Sleep(50);
            return n > 6 ? true : false;
        }
    }
    interface IVisitor
    {
        void VisitRoom(Room room);
        void VisitFloor(Floor floor);
        void VisitBuilding(Building building);
    }
    class Electrician : IVisitor
    {
        public void VisitBuilding(Building building)
        {
            if (building.checkElectricity())
            {
                Console.WriteLine($"Здание в порядке");
            }
            else
            {
                Console.WriteLine($"Здание не в порядке");
            }
            foreach (Floor floor in building.floors)
            {
                VisitFloor(floor);
            }
        }

        public void VisitFloor(Floor floor)
        {
            if (floor.checkElectricity())
            {
                Console.WriteLine($"Этаж {floor.Number} в порядке");
            }
            else
            {
                Console.WriteLine($"Этаж {floor.Number} не в порядке");
            }
            foreach (Room room in floor.rooms)
            {
                VisitRoom(room);
            }
        }

        public void VisitRoom(Room room)
        {
            if (room.checkElectricity())
            {
                Console.WriteLine($"Комната {room.Number} в порядке");
            }
            else
            {
                Console.WriteLine($"Комната {room.Number} не в порядке");
            }
        }
    }
}