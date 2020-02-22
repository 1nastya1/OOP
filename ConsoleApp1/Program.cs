using System;
using System.Collections.Generic;


namespace ConsoleApp1
{
    public abstract class Cake
    {
        public string Name;
    }
   public abstract class Muffins
   {
        public string Name;
   }
   public abstract class AbstractFastory
   {
        public abstract List<Cake> MakeCake(int a);
        public abstract List<Muffins> MakeMuffins(int b);

   }

    public class KievCake : Cake
    {
        public KievCake(string name)
        {
            Name = name;
        }
    }
    public  class FrenchMuffins : Muffins
    {
        public FrenchMuffins(string name)
        {
            Name = name;
        }
    }
    public class Spartak : AbstractFastory
    {
         public override List<Cake> MakeCake(int a)
         {
            List<Cake> cakes = new List<Cake>();
        
             for (int i = 0; i<a; i++)
             {
                cakes.Add(new KievCake(String.Format("a KievCake {0}", i+1)));
             }
            return cakes;
         }
        public override List<Muffins> MakeMuffins(int b)
        {
            List<Muffins> muffins = new List<Muffins>();
            for(int i =0; i<b; i++)
            {
                muffins.Add(new FrenchMuffins(String.Format("a French muffin{0}", i + 1)));
            }
            return muffins;
        }
    }

    class VeryBigCake
    {
        private static VeryBigCake instance;
        private static readonly object Locker = new object();
        public string weight;
        public string height;

        private VeryBigCake()
        {
            weight = "8 кг";
            height = "60 см";
        }

        public static VeryBigCake getInstance()
        {
            if (instance == null)
            {
                lock (Locker)
                {
                    if(instance == null)
                    {
                        instance = new VeryBigCake();
                        Console.WriteLine("Мы испечем такой большой торт для Вас!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Такой торт уже испечен");
            }
            return instance;
        }
    }

    abstract class Builder
    {
        public abstract void addSugar();
        public abstract void addEgg();
        public abstract void addMilk();
        public abstract void addVanila();
        public abstract void addColor();
        public abstract Cream GetResult();
    }
    class Cream
    {
        List<object> ingredients = new List<object>();
        public void Add(string ingr)
        {
            ingredients.Add(ingr);
        }
    }
    class CreamFirst : Builder
    {
        Cream cream = new Cream();
        public override void addEgg()
        {
            cream.Add("Яйца");
        }
        public override void addSugar()
        {
            cream.Add("Сахар");
        }
        public override void addMilk()
        {
            cream.Add("Молоко");
        }
        public override void addVanila()
        {
            cream.Add("Ванилин");
        }
        public override void addColor()
        {
            cream.Add("Краситель");
        }
        public override Cream GetResult()
        {
            Console.WriteLine("Крем готов!");
            return cream;
        }
    }
    class Director
    {
        Builder builder;
        public Director(Builder builder)
        {
            this.builder = builder;
        }
        public void Construct()
        {
            builder.addSugar();
            builder.addEgg();
            builder.addMilk();
            builder.addVanila();
            builder.addColor();
        }
    }
    public interface IConsigment
    {
        IConsigment Clone();
        void getInfo();
    }
    public class Prototype : IConsigment
    {
        int con;
        public Prototype(int c)
        {
            con = c;
        }
        public IConsigment Clone()
        {
            return new Prototype(this.con);
        }
        public void getInfo()
        {
            Console.WriteLine("Номер партии: " + con);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var abtrfact = new Spartak();
            Console.WriteLine("Введите количество Киевских тортов, которое вам нужно");
            Console.Write("здесь: ");
            int a = Convert.ToInt32(Console.ReadLine());
            abtrfact.MakeCake(a).ForEach((a)=>Console.WriteLine(a.Name));
            Console.WriteLine("Введите количество французских маффинов которое вам нужно");
            Console.Write("здесь: ");
            int b = Convert.ToInt32(Console.ReadLine());
            abtrfact.MakeMuffins(b).ForEach((b) => Console.WriteLine(b.Name));
            VeryBigCake v1 = VeryBigCake.getInstance();
            VeryBigCake v2 = VeryBigCake.getInstance();

            Builder builder = new CreamFirst();
            Director director = new Director(builder);
            director.Construct();
            Cream product = builder.GetResult();

            IConsigment consigment = new Prototype(14256);
            consigment.getInfo();
            IConsigment consigment1 = consigment.Clone();
            consigment1.getInfo();
            Console.ReadKey();
        }

    }
}
