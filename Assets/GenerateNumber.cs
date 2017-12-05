using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateNumber : MonoBehaviour {

    public void Randomize()
    {
        Text label = GetComponent<Text>();
        int random = Random.Range(0, 9999);
        int prize = 0;
        int[] reels = new int[4];

        // set win ranges (also used for reels)
        int[,] ranges = {
            { 0, 0 },
            { 1000, 500 },
            { 2500, 1000 },
            { 5000, 10000 },
            { 6000, 15000 },
            { 8000, 20000 },
            { 9500, 50000 },
            { 10000, 50000 }
        };

        // get winnings
        for (int x = 0; x < ranges.GetLength(0); x++) {
            if (ranges[x+1, 0] > random && random >= ranges[x, 0]) {
                prize = ranges[x, 1];
                reels = processReels(ranges[x, 0], ranges[x, 1], random);
                break;
            }
        }
        
        label.text = "Prize: $" + prize.ToString() + ".00           Number: " + random;

        string[] convertedReels = placeholderReels(reels);
        label.text += "\n" + convertedReels[0].ToString() + convertedReels[1].ToString() + convertedReels[2].ToString() + convertedReels[3].ToString();
    }

    // get reels handled? 
    private int[] processReels(int floor, int prize, int roll) {
        int[] reels = { 0, 0, 0, 0 };
        
        // This will format any number into 4 digits so we don't pass in character arrays that aren't 4 long...this kills the loop
        char[] randomChunked = roll.ToString("0000").ToCharArray();

        for (int x = 0; x < randomChunked.Length; x++) {
            if (prize == 0) {
                if (x == 0) {
                    if (randomChunked[x] == '0')
                        reels[0] = Random.Range(1, 9);
                }
                else
                    reels[x] = int.Parse(randomChunked[x].ToString());
            }
            else if (prize == 500 && x == 0) {
                reels[x] = 0;
            }
            else if (prize == 1000 && (x == 0 || x == 1)) {
                reels[x] = 0;
            }
            else if (prize == 10000 && x < 3) {
                reels[x] = 1;
            }
            else if (prize == 15000 && x < 3) {
                reels[x] = 4;
            }
            else if (prize == 20000 && x < 3) {
                reels[x] = 7;
            }
            else if (prize == 50000) {
                reels[x] = 9;
            }
            else
                reels[x] = int.Parse(randomChunked[x].ToString());
        }

        return reels;
    }

    private string[] placeholderReels(int[] reelNumbers) {
        string[] reels = new string[4];

        for (int x = 0; x < reelNumbers.Length; x++)
        {
            switch (reelNumbers[x])
            {
                case 0:
                    reels[x] = "Cherry ";
                    break;
                case 1:
                    reels[x] = "Clover ";
                    break;
                case 2:
                    reels[x] = "Lemon ";
                    break;
                case 3:
                    reels[x] = "Orange ";
                    break;
                case 4:
                    reels[x] = "Bell ";
                    break;
                case 5:
                    reels[x] = "Grapes ";
                    break;
                case 6:
                    reels[x] = "Banana ";
                    break;
                case 7:
                    reels[x] = "Bar ";
                    break;
                case 8:
                    reels[x] = "Horseshoe ";
                    break;
                case 9:
                    reels[x] = "Seven ";
                    break;
            }
        }

        return reels;
    }
}
