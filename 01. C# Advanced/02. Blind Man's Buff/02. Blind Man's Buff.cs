int[] dimensions = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();
int rows = dimensions[0];
int cols = dimensions[1];
char[,] playground = new char[rows, cols];
int playerRow = -1;
int playerCol = -1;
int movesMade = 0;
int touchedOponentsCount = 0;
int oponentsCount = 0;
for (int i = 0; i < rows; i++)
{
    string[] elements = Console.ReadLine().Split(" ");
    for (int j = 0; j < cols; j++)
    {
        playground[i, j] = char.Parse(elements[j]);
        if (playground[i, j] == 'B')
        {
            playerRow = i;
            playerCol = j;
        }
        else if (playground[i, j] == 'P')
        {
            oponentsCount++;
        }
    }
}
string command;
while ((command = Console.ReadLine()) != "Finish")
{
    if (oponentsCount == 0)
    {
        playground[playerRow, playerCol] = 'B';
        break;
    }
    playground[playerRow, playerCol] = '-';

    if (command == "up")
    {
        playerRow--;
        if (playerRow >= 0)
        {
            if (playground[playerRow, playerCol] == 'O')
            {
                playerRow++;
            }
            else if (playground[playerRow, playerCol] == 'P')
            {
                movesMade++;
                oponentsCount--;
                touchedOponentsCount++;
            }
            else if (playground[playerRow, playerCol] == '-')
            {
                movesMade++;
            }
        }
        else
        {
            playerRow = 0;
        }
    }
    else if (command == "down")
    {
        playerRow++;
        if (playerRow < rows)
        {
            if (playground[playerRow, playerCol] == 'O')
            {
                playerRow--;
            }
            else if (playground[playerRow, playerCol] == 'P')
            {
                movesMade++;
                oponentsCount--;
                touchedOponentsCount++;
            }
            else if (playground[playerRow, playerCol] == '-')
            {
                movesMade++;
            }
        }
        else
        {
            playerRow = rows - 1;
        }
    }
    else if (command == "left")
    {
        playerCol--;
        if (playerCol >= 0)
        {
            if (playground[playerRow, playerCol] == 'O')
            {
                playerCol++;
            }
            else if (playground[playerRow, playerCol] == 'P')
            {
                movesMade++;
                oponentsCount--;
                touchedOponentsCount++;
            }
            else if (playground[playerRow, playerCol] == '-')
            {
                movesMade++;
            }
        }
        else
        {
            playerCol = 0;
        }
    }
    else if (command == "right")
    {
        playerCol++;
        if (playerCol < cols)
        {
            if (playground[playerRow, playerCol] == 'O')
            {
                playerCol--;
            }
            else if (playground[playerRow, playerCol] == 'P')
            {
                movesMade++;
                oponentsCount--;
                touchedOponentsCount++;
            }
            else if (playground[playerRow, playerCol] == '-')
            {
                movesMade++;
            }
        }
        else
        {
            playerCol = cols - 1;
        }
    }
    playground[playerRow, playerCol] = 'B';
}
Console.WriteLine("Game over!");
Console.WriteLine($"Touched opponents: {touchedOponentsCount} Moves made: {movesMade}");
