//Sarah Dykstra - 20817609
//BME 121 Fall 2019
//October 20, 2019
//Two classes - one to represent each die and one to represent all of the dice in a game of Yahtzee
using System;
using static System.Console;

namespace Bme121
{
    class YahtzeeDice 
    {
		Die[] dice = new Die[5]; // set of all five dice
		
		public YahtzeeDice() // constructor
		{
			for(int i = 0; i < 5; i++)
			{
				dice[i] = new Die(); // instantiates each index of the array
			}
		}
		
		public void Roll() // gives a random value to any unrolled dice
		{
			for(int i = 0; i < 5; i++)
			{
				if(!dice[i].rolled)
				{
					dice[i].Roll();
				}
			}
		}
		
		public void Unroll(string faces) // makes a die available to be rolled again
		{
			if(faces.ToLower() == "all")
			{
				for(int i = 0; i < 5; i++)
				{
					dice[i].Unroll();
				}
			}
			else 
			{
				try 
				{
					for(int index = 0; index < faces.Length; index++) // loops through each number of the string one at a time
					{
						int num = int.Parse(faces.Substring(index, 1));
						bool foundNum = false; // indicates whether the specified face has been found and unrolled yet
						for(int i = 0; i < 5; i++) // checks all five dice
						{
							if(!foundNum && dice[i].result == num && dice[i].rolled == true) // checks if the value has not yet been found, the value matches, and the die has not already been unrolled
							{
								dice[i].Unroll();
								foundNum = true;
							}
						}
					}
				}
				catch (FormatException)
				{
					WriteLine("Input string in incorrect format");
				}	
			}
		}
		
		public int Sum() // returns the sum of all of the values of the dice
		{
			int sum = 0;
			for(int i = 0; i < 5; i++) // loops through all 5 dice
			{
				sum += dice[i].result;
			}
			return sum;
		}
		
		public int Sum(int face) // returns the sum of all dice with a given value
		{
			int sum = 0;
			for(int i = 0; i < 5; i++) // loops through all five dice
			{
				if(dice[i].result == face)
				{
					sum += face;
				}
			}
			return sum;
		}
		
		public bool IsRunOf(int length) // checks if there exists a run of a given length
		{
			for(int face = 1; face < 8 - length; face++) // checks all possible starting values for the run
			{
				bool broken = false; // indicates whether the series being checked has been broken (i.e. missing a value)
				for(int index = face; index < face + length; index++) // checks all values in the set with the given starting value
				{
					if(!broken) // only keeps checking if the run has not yet been broken
					{
						bool found = false;
						for(int i = 0; i < 5; i++) // checks all five dice for the value
						{
							if(dice[i].result == index)
							{
								found = true;
							}
						}
						if(!found) // checks if a certain value in the run being checked is not present in the set of dice
						{
							broken = true;
						}
						else if (index == face + length - 1) // if the end of the run has been reached and all values being checked are there
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		
		public bool IsSetOf(int size) // checks if a set of a given number exists
		{
			for(int i = 1; i <= 6; i++)
			{
				if(Sum(i) >= size * i)
				{
					return true;
				}
			}
			return false;
		}
		
		public bool IsFullHouse() //checks if a full house is present
		{
			bool two = false;
			bool three = false;
			for(int i = 1; i <= 6; i++)
			{
				if(Sum(i) == 3 * i) // if there's three of the number being checked
				{
					three = true;
				}
				if(Sum(i) == 2 * i) // if there's two of the number being checked
				{
					two = true;
				}
			}
			return two && three;
		}
		
		public override string ToString() // returns the values of the dice
		{
			string result = "";
			for(int i = 0; i < 5; i++)
			{
				result += $"{dice[i].result}, ";
			}
			return result.Substring(0, result.Length - 2);
		}
	}
	
	class Die // class to represent each individual die
	{
		public int result {get; private set;}
		public bool rolled {get; private set;}
		public Die() 
		{
			this.result = 0;
			this.rolled = false;
		}
		public void Unroll() // allows the die to be rolled again
		{
			this.rolled = false;
		}
		public void Roll() // assigns a new random value to a die
		{
			Random rGen = new Random();
			result = (int)(rGen.NextDouble() * 6 + 1);
			this.rolled = true;
		}
	}
}
