using BLL;

namespace DAL.DTO;

public static class GameStateMapper
{
    public static GameStateDto ToDto(GameState state) => new()
    {
        Id = state.Id,
        Board = TwoDimensionalIntoJaggedArray(state.GetBoard()),
        Config = state.Config,
        IsCurrentPlayerDark = state.IsCurrentPlayerDark,
        SavedAtUtc = DateTime.UtcNow,
    };

    public static GameState ToDomain(GameStateDto dto) => new GameState
    (
        dto.Id,
        JaggedIntoTwoDimensionalArray(dto.Board),
        dto.Config,
        dto.IsCurrentPlayerDark,
        dto.SavedAtUtc
    );

    private static T[][] TwoDimensionalIntoJaggedArray<T>(T[,] inputArray)
    {
        var jaggedArray = new T[inputArray.GetLength(0)][];

        for (var x = 0; x < inputArray.GetLength(0); x++)
        {
            var array = new T[inputArray.GetLength(1)];
            for (var y = 0; y < inputArray.GetLength(1); y++)
            {
                array[y] = inputArray[x, y];
            }

            jaggedArray[x] = array;
        }

        return jaggedArray;
    }

    private static T[,] JaggedIntoTwoDimensionalArray<T>(T[][] inputArray)
    {
        var twoDimensionalArray = new T[inputArray.Length, inputArray[0].Length];

        for (var x = 0; x < inputArray.Length; x++)
        {
            for (var y = 0; y < inputArray[x].Length; y++)
            {
                twoDimensionalArray[x, y] = inputArray[x][y];
            }
        }

        return twoDimensionalArray;
    }
}