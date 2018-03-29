using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models
{
    public class Repository
    {
        LimeContext context;

        public Repository(LimeContext context)
        {
            this.context = context;
        }

        public void GenerateBoxes(string nameOfBox, decimal price)
        {
            //av 100 boxar så är:
            //standard 80st
            //premium 15st
            //Lyx 5st
            int[] random5 = GenerateRandomNumbers(5);
            int[] random15 = GenerateRandomNumbers(15, random5);
            for (int i = 1; i <= 100; i++)
            {
                int valueNumber = 1;
                if (numberIsInArray(random5, i))
                    valueNumber = 3;
                else if (numberIsInArray(random15, i))
                    valueNumber = 2;
                context.Add(new Boxes
                {
                    BoxId = i,
                    BoxType = nameOfBox,
                    BoxValue = valueNumber,
                    BoxPrice = price,
                });
            }
            context.SaveChanges();
        }

        private int[] GenerateRandomNumbers(int amount)
        {
            Random random = new Random();
            int[] numbers = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(1, 100 + 1);

                } while (numberIsInArray(numbers, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        private int[] GenerateRandomNumbers(int amount, int[] array)
        {
            Random random = new Random();
            int[] numbers = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(1, 100 + 1);

                } while (numberIsInArray(numbers, randomNumber) || numberIsInArray(array, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        private bool numberIsInArray(int[] numbers, int number)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                    return true;
            }
            return false;
        }

        public Boxes FindBoxById(int id)
        {
            return context.Boxes.Find(id);
        }

    }
}
