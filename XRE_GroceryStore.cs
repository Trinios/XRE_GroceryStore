using System;
using System.IO;

class GroceryStore
{
	static void Main(String[] args)
	{
		//initialize these to recieve text file input
		int reqNum = 0;
		string productIn;
		
		foreach (string line in File.ReadLines("TestOrder.txt"))
		{
			string[] words = line.Split(' '); //split the line into type and number
			
			if(int.TryParse(words[1], out int numOut)) //check that the second part is a valid integer
			{
				if(numOut < 1) //check if the number is 0 or a negative
				{
					Console.WriteLine("This is not a valid number to order" + System.Environment.NewLine);
					continue;
				}

				reqNum = numOut;
				productIn = words[0];

                //initialize variables for the switch statement
                float price = 0.0f;
                int total3 = 0;
                int total4 = 0;
                int total5 = 0;
                int total9 = 0;
                int total10 = 0;
                int total15 = 0;

				//primary processing calls and then output, doubles as checking for correct product names
				switch(productIn)
				{
					case "SH3":
						var SlicedHamOut = SlicedHam(reqNum);

						price = SlicedHamOut.priceOut; //item1
						total5 = SlicedHamOut.pack5; //item2
						total3 = SlicedHamOut.pack3; //item3

						Console.WriteLine("The total price is: $" + price + System.Environment.NewLine
										 + "3-Pack x " + total3 + System.Environment.NewLine
										 + "5-Pack x " + total5 + System.Environment.NewLine); //this can and will give results of z-Pack x 0, this is a little weird, but avoids a bunch of checks
						break;
					case "YT2":
						var YoghurtOut = Yoghurt(reqNum);
						
						price = YoghurtOut.priceOut; //item1
						total15 = YoghurtOut.pack15; //item2
						total10 = YoghurtOut.pack10; //item3
						total4 = YoghurtOut.pack4; //item4

                        Console.WriteLine("The total price is: $" + price + System.Environment.NewLine
										 + "4-Pack x " + total4 + System.Environment.NewLine
										 + "10-Pack x " + total10 + System.Environment.NewLine
										 + "15-Pack x " + total15 + System.Environment.NewLine); //this can and will give results of z-Pack x 0, this is a little weird, but avoids a bunch of nested checks
						break;
					case "TR":
						var ToiletRollsOut = ToiletRolls(reqNum);
						
						price = ToiletRollsOut.priceOut; //item1
						total9 = ToiletRollsOut.pack9; //item2
						total5 = ToiletRollsOut.pack5; //item3
						total3 = ToiletRollsOut.pack3; //item4

                        Console.WriteLine("The total price is: $" + price + System.Environment.NewLine
										 + "3-Pack x " + total3 + System.Environment.NewLine
										 + "5-Pack x " + total5 + System.Environment.NewLine
										 + "9-Pack x " + total9 + System.Environment.NewLine); //this can and will give results of z-Pack x 0, this is a little weird, but avoids a bunch of nested checks
						break;
					default: //if the input is not a valid product, you'll end up here
						Console.WriteLine("Product Type Invalid" + System.Environment.NewLine);
						break;
				}
			}
			else
			{
				Console.WriteLine("The requested number could not be determined, try checking if the input is in the format 'ITEM' then 'NUMBER'" + System.Environment.NewLine);
			}
		}
	}

	private static (float priceOut, int pack15, int pack10, int pack4) Yoghurt(int num)
	{
		//number of each pack needed
		int count4 = 0;
		int count10 = 0;
		int count15 = 0;
		int totalCount = 0;
		int rem = 0;

		//skip any calculations if the requested number is exactly the same as a pack size
		if(num == 15)
		{
			return(13.95f,1,0,0);
		}
		else if(num == 10)
		{
			return(9.95f,0,1,0);
		}
		else if(num == 4)
		{
			return(4.95f,0,0,1);
		}

		if(num < 10) //skip the largest pack size
		{
			count10 = num/10;
			rem = num%10;
			//any leftover should be put into the next biggest pack to save on packs
			if(rem > 4)
			{
				count10 += 1;
			}
			else if(rem > 0)
			{
				count4 += 1;
			}
			totalCount = count4+count10;
			if(totalCount*4 > num)
			{
				//reset the counts that were used
				count10 = 0;
				count4 = 0;

				count4 = num/4;
				rem = num%4;
				if(rem > 0)
				{
					count4 += 1;
				}
			}
		}
		else //bulk of the calculations
		{
			count15 = num/15;
			//any leftover should be put into the next biggest pack to save on packs
			rem = num%15;
			if(rem > 10)
			{
				count15 += 1;
			}
			else if(rem > 4)
			{
				count10 += 1;
			}
			else if(rem > 0)
			{
				count4 += 1;
			}

			//check if a solution exists for the same or less packs with less excess sent out
			totalCount = count4+count10+count15;
			if(totalCount*10 > num)
			{
				//reset the counts
				count15 = 0;
				count10 = 0;
				count4 = 0;

				count10 = num/10;
				rem = num%10;
				//leftover as above
				if(rem > 4)
				{
					count10 += 1;
				}
				else if(rem > 0)
				{
					count4 += 1;
				}

				//check again for a smaller solution
				totalCount = count4+count10;
				if(totalCount*4 > num)
				{
					//reset the counts that were used, since the largest was ignored before
					count10 = 0;
					count4 = 0;

					count4 = num/4;
					rem = num%4;
					if(rem > 0)
					{
						count4 += 1;
					}
				}
			}
		}
		//calculate the total price
		float priceTotal = count15*13.95f + count10*9.95f + count4*4.95f;
		return(priceTotal,count15,count10,count4);
	}

	private static (float priceOut, int pack9, int pack5, int pack3) ToiletRolls(int num)
	{
		//number of each pack needed
		int count3 = 0;
		int count5 = 0;
		int count9 = 0;
		int totalCount = 0;
		int rem = 0;

		//skip any calculations if the requested number is exactly the same as a pack size
		if(num == 9)
		{
			return(7.99f,1,0,0);
		}
		else if(num == 5)
		{
			return(4.45f,0,1,0);
		}
		else if(num == 3)
		{
			return(2.95f,0,0,1);
		}

		if(num < 5) //skip the largest pack size
		{
			count5 = num/5;
			rem = num%5;
			//any leftover should be put into the next biggest pack to save on packs
			if(rem > 3)
			{
				count5 += 1;
			}
			else if(rem > 0)
			{
				count3 += 1;
			}
			totalCount = count3+count5;
			if(totalCount*3 > num)
			{
				//reset the counts that were used
				count5 = 0;
				count3 = 0;

				count3 = num/3;
				rem = num%3;
				if(rem > 0)
				{
					count3 += 1;
				}
			}
		}
		else //bulk of the calculations
		{
			count9 = num/9;
			//any leftover should be put into the next biggest pack to save on packs
			rem = num%9;
			if(rem > 5)
			{
				count9 += 1;
			}
			else if(rem > 3)
			{
				count5 += 1;
			}
			else if(rem > 0)
			{
				count3 += 1;
			}

			//check if a solution exists for the same or less packs with less excess sent out
			totalCount = count3+count5+count9;
			if(totalCount*5 > num)
			{
				//reset the counts
				count9 = 0;
				count5 = 0;
				count3 = 0;

				count5 = num/5;
				rem = num%5;
				//leftover as above
				if(rem > 3)
				{
					count5 += 1;
				}
				else if(rem > 0)
				{
					count3 += 1;
				}

				//check again for a smaller solution
				totalCount = count3+count5;
				if(totalCount*3 > num)
				{
					//reset the counts that were used, since the largest was ignored before
					count5 = 0;
					count3 = 0;

					count3 = num/3;
					rem = num%3;
					if(rem > 0)
					{
						count3 += 1;
					}
				}
			}
		}
		//calculate the total price
		float priceTotal = count9*7.99f + count5*4.45f + count3*2.95f;
		return(priceTotal,count9,count5,count3);
	}

	private static (float priceOut, int pack5, int pack3) SlicedHam(int num)
	{
		//number of each pack needed
		int count3 = 0;
		int count5 = 0;
		int totalCount = 0;
		int rem = 0;

		//skip any calculations if the requested number is exactly the same as a pack size
		if(num == 5)
		{
			return(4.49f,1,0);
		}
		else if(num == 3)
		{
			return(2.99f,0,1);
		}

		if(num < 3) //if less than the smallest size, just give the smallest size
		{
			return(2.99f,0,1);
		}
		else //bulk of the calculations
		{
			count5 = num/5;
			//any leftover should be put into the next biggest pack to save on packs
			rem = num%5;
			if(rem > 3)
			{
				count5 += 1;
			}
			else if(rem > 0)
			{
				count3 += 1;
			}

			//check if a solution exists for the same or less packs with less excess sent out
			totalCount = count3+count5;
			if(totalCount*3 > num)
			{
				//reset the counts
				count5 = 0;
				count3 = 0;

				count3 = num/3;
				rem = num%3;
				//leftover as above
				if(rem > 0)
				{
					count3 += 1;
				}
			}
		}
		//calculate the total price
		float priceTotal = count5*4.49f + count3*2.99f;
		return(priceTotal,count5,count3);
	}
}